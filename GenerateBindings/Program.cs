using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GenerateBindings;

internal static partial class Program
{
    private enum TypeContext
    {
        StructField,
        FunctionData,
    }

    private class StructDefinitionType
    {
        public bool ContainsUnion { get; set; }
        public List<(uint, string)> OffsetFields { get; } = new();
        public Dictionary<string, RawFFIEntry> InternalStructs { get; } = new();

        public void Reset()
        {
            ContainsUnion = false;
            OffsetFields.Clear();
            InternalStructs.Clear();
        }
    }

    private class FunctionSignatureType
    {
        public enum ReturnIntentType
        {
            None,
            String,
            Array,
        }

        public string Name { get; set; } = "";
        public string ReturnType { get; set; } = "";
        public ReturnIntentType ReturnIntent = ReturnIntentType.None;
        public List<(string, string)> ParameterTypesNames { get; } = new();
        public List<string> HeapAllocatedStringParams { get; } = new();
        public StringBuilder ParameterString { get; } = new();

        public bool RequiresStringMarshalling => (ReturnIntent == ReturnIntentType.String) || (HeapAllocatedStringParams.Count > 0);

        public void Reset()
        {
            Name = "";
            ReturnType = "";
            ReturnIntent = ReturnIntentType.None;
            ParameterTypesNames.Clear();
            HeapAllocatedStringParams.Clear();
            ParameterString.Clear();
        }
    }

    private static readonly List<string> DefinedTypes = new();
    private static readonly Dictionary<string, RawFFIEntry> TypedefMap = new();
    private static readonly HashSet<string> UnusedUserProvidedTypes = new();

    private static readonly StructDefinitionType StructDefinition = new();
    private static readonly FunctionSignatureType FunctionSignature = new();

    private static bool CoreMode = false;

    private static bool CheckCoreMode(string[] args)
    {
        return args.Contains("--core");
    }

    private static DirectoryInfo GetSDL3Directory(string[] args)
    {
        return new DirectoryInfo(args[0]);
    }

    private static int Main(string[] args)
    {
        // PARSE INPUT
        if (args.Length < 1)
        {
            Console.WriteLine("usage: GenerateBindings <sdl-repo-root-dir> [--core]");
            Console.WriteLine("sdl-repo-root-dir: The root directory of SDL3 code.");
            Console.WriteLine("--core: Bindgen for .NET Core. If this is not set, will bindgen for .NET Framework.");
            return 1;
        }

        CoreMode = CheckCoreMode(args);

        var sdlProjectName = CoreMode ? "SDL3.Core.csproj" : "SDL3.Legacy.csproj";

        var sdlDir = GetSDL3Directory(args);
        var sdlBindingsDir = new FileInfo(Path.Combine(AppContext.BaseDirectory, "../../../../SDL3/"));
        var sdlBindingsProjectFile = new FileInfo(Path.Combine(sdlBindingsDir.FullName, sdlProjectName));
        var ffiJsonFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "ffi.json"));

#if WINDOWS
        var dotnetExe = FindInPath("dotnet.exe");
#else
        var dotnetExe = FindInPath("dotnet");
#endif

        foreach (var key in UserProvidedData.PointerFunctionDataIntents.Keys)
        {
            UnusedUserProvidedTypes.Add(key.Item1);
        }

        foreach (var key in UserProvidedData.ReturnedCharPtrMemoryOwners.Keys)
        {
            UnusedUserProvidedTypes.Add(key);
        }

        foreach (var key in UserProvidedData.ReturnedArrayCountParamNames.Keys)
        {
            UnusedUserProvidedTypes.Add(key);
        }

        foreach (var key in UserProvidedData.DelegateDefinitions.Keys)
        {
            UnusedUserProvidedTypes.Add(key);
        }

        foreach (var key in UserProvidedData.FlagEnumDefinitions.Keys)
        {
            UnusedUserProvidedTypes.Add(key);
        }

        RawFFIEntry[]? ffiData = JsonSerializer.Deserialize<RawFFIEntry[]>(File.ReadAllText(ffiJsonFile.FullName));
        if (ffiData == null)
        {
            Console.WriteLine($"failed to read ffi.json file {ffiJsonFile.FullName}!!");
            return 1;
        }

        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if ((entry.Tag == "typedef") && entry.Name!.StartsWith("SDL_"))
            {
                TypedefMap[entry.Name!] = entry.Type!;
            }
        }

        var definitions = new StringBuilder();
        var unknownPointerFunctionData = new StringBuilder();
        var unknownReturnedCharPtrMemoryOwners = new StringBuilder();
        var unknownReturnedArrayCountParamNames = new StringBuilder();
        var undefinedFunctionPointers = new StringBuilder();
        var unpopulatedFlagDefinitions = new StringBuilder();
        var currentSourceFile = "";
        var propsDefinitions = new Dictionary<string, string>();
        var hintsDefinitions = new Dictionary<string, string>();
        var inlinedFunctionNames = new List<string>();

        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if (Path.GetFileName(entry.Header).StartsWith("SDL_stdinc.h") &&
                !((entry.Name == "SDL_malloc") || (entry.Name == "SDL_free")))
            {
                continue;
            }

            if (UserProvidedData.DeniedTypes.Contains(entry.Name))
            {
                continue;
            }

            var headerFile = entry.Header.Split(":")[0];
            if (currentSourceFile != headerFile)
            {
                definitions.Append($"// {headerFile}\n\n");
                currentSourceFile = headerFile;

                hintsDefinitions.Clear();
                propsDefinitions.Clear();
                inlinedFunctionNames.Clear();

                var isHintsHeader = currentSourceFile.EndsWith("SDL_hints.h");

                string headerName = currentSourceFile.Substring(currentSourceFile.LastIndexOf('/') + 1);
                IEnumerable<string> fileLines = File.ReadLines(Path.Combine(sdlDir.FullName, $"include/SDL3/{headerName}"));
                foreach (var line in fileLines)
                {
                    if (isHintsHeader)
                    {
                        var hintMatch = HintDefinitionRegex().Match(line);
                        if (hintMatch.Success)
                        {
                            hintsDefinitions[hintMatch.Groups["hintName"].Value] = hintMatch.Groups["value"].Value;
                        }
                    }

                    // Extract SDL_PROP_ #define's.  Note that SDL_thread.h currently has some duplicate entries.
                    var propMatch = PropDefinitionRegex().Match(line);
                    if (propMatch.Success)
                    {
                        propsDefinitions[propMatch.Groups["propName"].Value] = propMatch.Groups["value"].Value;
                    }

                    var inlinedFunctionMatch = InlinedFunctionRegex().Match(line);
                    if (inlinedFunctionMatch.Success)
                    {
                        inlinedFunctionNames.Add(inlinedFunctionMatch.Groups["functionName"].Value);
                    }
                }

                if (hintsDefinitions.Count > 0)
                {
                    foreach (KeyValuePair<string, string> definition in hintsDefinitions)
                    {
                        definitions.Append($"public const string {definition.Key} = \"{definition.Value}\";\n");
                    }

                    definitions.Append('\n');
                }

                if (propsDefinitions.Count > 0)
                {
                    foreach (KeyValuePair<string, string> definition in propsDefinitions)
                    {
                        definitions.Append($"public const string {definition.Key} = \"{definition.Value}\";\n");
                    }

                    definitions.Append('\n');
                }
            }

            if (entry.Tag == "enum")
            {
                definitions.Append($"public enum {entry.Name!}\n{{\n");
                DefinedTypes.Add(entry.Name!);

                foreach (var enumValue in entry.Fields!)
                {
                    definitions.Append($"{enumValue.Name} = {(int) enumValue.Value!},\n");
                }

                definitions.Append("}\n\n");
            }

            else if (entry.Tag == "typedef")
            {
                if (entry.Type!.Tag == "function-pointer")
                {
                    if (UserProvidedData.DelegateDefinitions.TryGetValue(key: entry.Name!, value: out var delegateDefinition))
                    {
                        UnusedUserProvidedTypes.Remove(entry.Name!);

                        if (delegateDefinition.ReturnType == "WARN_PLACEHOLDER")
                        {
                            definitions.Append("// ");
                        }
                        else
                        {
                            definitions.Append("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]\n");
                            DefinedTypes.Add(entry.Name!);
                        }

                        definitions.Append($"public delegate {delegateDefinition.ReturnType} {entry.Name}(");

                        var initialParam = true;
                        foreach (var (paramType, paramName) in delegateDefinition.Parameters)
                        {
                            if (initialParam == false)
                            {
                                definitions.Append(", ");
                            }
                            else
                            {
                                initialParam = false;
                            }

                            definitions.Append($"{paramType} {paramName}");
                        }

                        definitions.Append(");\n\n");
                    }
                    else
                    {
                        definitions.Append(
                            $"// public static delegate RETURN {entry.Name}(PARAMS) // WARN_UNDEFINED_FUNCTION_POINTER: {entry.Header}\n\n"
                        );
                        undefinedFunctionPointers.Append(
                            $"{{ \"{entry.Name}\", new DelegateDefinition {{ ReturnType = \"WARN_PLACEHOLDER\", Parameters = [] }} }}, // {entry.Header}\n"
                        );
                    }
                }
                else if (entry.Name == "SDL_Keycode")
                {
                    var enumType = CSharpTypeFromFFI(type: entry.Type!, TypeContext.StructField);
                    definitions.Append($"public enum {entry.Name} : {enumType}\n{{\n");
                    definitions.Append("SDLK_SCANCODE_MASK = 0x40000000,\n");

                    IEnumerable<string> hintsFileLines = File.ReadLines(Path.Combine(sdlDir.FullName, "include/SDL3/SDL_keycode.h"));

                    foreach (var line in hintsFileLines)
                    {
                        var match = KeycodeDefinitionRegex().Match(line);
                        if (match.Success)
                        {
                            definitions.Append($"{match.Groups["keycodeName"].Value} = {match.Groups["value"].Value},\n");
                        }
                    }

                    definitions.Append("}\n\n");
                }
                else if (entry.Name != null && IsFlagType(entry.Name))
                {
                    definitions.Append("[Flags]\n");
                    var enumType = CSharpTypeFromFFI(type: entry.Type!, TypeContext.StructField);
                    definitions.Append($"public enum {entry.Name} : {enumType}\n{{\n");

                    if (!UserProvidedData.FlagEnumDefinitions.TryGetValue(entry.Name, value: out var enumValues))
                    {
                        unpopulatedFlagDefinitions.Append($"{{ \"{entry.Name}\", [ ] }}, // {entry.Header}\n");
                        definitions.Append("// WARN_UNPOPULATED_FLAG_ENUM\n");
                    }
                    else if (enumValues.Length == 0)
                    {
                        UnusedUserProvidedTypes.Remove(entry.Name!);

                        definitions.Append("// WARN_UNPOPULATED_FLAG_ENUM\n");
                    }
                    else
                    {
                        UnusedUserProvidedTypes.Remove(entry.Name!);

                        for (var i = 0; i < enumValues.Length; i++)
                        {
                            var enumEntry = enumValues[i];
                            if (enumEntry.Contains('='))
                            {
                                definitions.Append($"{enumEntry},\n");
                            }
                            else
                            {
                                definitions.Append($"{enumValues[i]} = 0x{BigInteger.Pow(value: 2, i):X},\n");
                            }
                        }
                    }

                    definitions.Append("}\n\n");
                }
            }

            else if ((entry.Tag == "struct") || (entry.Tag == "union"))
            {
                TypedefMap[entry.Name!] = entry;
                if (entry.Fields!.Length == 0)
                {
                    continue;
                }

                DefinedTypes.Add(entry.Name!);
                ConstructStruct(structName: entry.Name!, entry, definitions);

                while (StructDefinition.InternalStructs.Count > 0)
                {
                    var internalStructs = new Dictionary<string, RawFFIEntry>(StructDefinition.InternalStructs);
                    foreach (var (internalStructName, internalStructEntry) in internalStructs)
                    {
                        ConstructStruct(internalStructName, internalStructEntry, definitions);
                    }
                }
            }

            else if (entry.Tag == "function")
            {
                if (inlinedFunctionNames.Contains(entry.Name!))
                {
                    continue;
                }

                var hasVarArgs = false;
                foreach (var parameter in entry.Parameters!)
                {
                    if (parameter.Type!.Tag == "va_list")
                    {
                        hasVarArgs = true;
                        break;
                    }
                }

                if (hasVarArgs)
                {
                    continue;
                }

                FunctionSignature.Reset();

                FunctionSignature.Name = entry.Name!;

                var functionComponents = new RawFFIEntry[entry.Parameters!.Length + 1];
                functionComponents[0] = entry.ReturnType!;
                Array.Copy(entry.Parameters!, 0, functionComponents, 1, entry.Parameters!.Length);

                var containsUnknownRef = false;

                foreach (var component in functionComponents)
                {
                    var isReturn = component == entry.ReturnType!;
                    var componentName = isReturn ? "__return" : component.Name!;
                    var componentType = isReturn ? component : component.Type!;
                    string typeName;

                    if (!CoreMode && isReturn)
                    {
                        var returnTypedef = GetTypeFromTypedefMap(type: componentType);
                        typeName = CSharpTypeFromFFI(returnTypedef, TypeContext.FunctionData);
                        if (typeName == "UTF8_STRING")
                        {
                            FunctionSignature.ReturnIntent = FunctionSignatureType.ReturnIntentType.String;
                        }

                        UnusedUserProvidedTypes.Remove(entry.Name!); // assume the core bindings have some use for this definition
                    }
                    else if ((componentType.Tag == "pointer") && IsDefinedType(componentType.Type?.Tag))
                    {
                        var subtype = GetTypeFromTypedefMap(type: componentType.Type!);
                        var subtypeName = CSharpTypeFromFFI(subtype, TypeContext.FunctionData);

                        if (subtypeName == "UTF8_STRING") // pointer to an array; give up
                        {
                            typeName = "IntPtr";
                        }
                        else if (subtypeName == "char")
                        {
                            typeName = "UTF8_STRING";
                            if (isReturn)
                            {
                                FunctionSignature.ReturnIntent = FunctionSignatureType.ReturnIntentType.String;
                            }
                            else
                            {
                                FunctionSignature.HeapAllocatedStringParams.Add(SanitizeName(componentName));
                            }
                        }
                        else if (UserProvidedData.PointerFunctionDataIntents.TryGetValue(key: (FunctionSignature.Name, componentName), value: out var intent))
                        {
                            if (subtypeName == "FUNCTION_POINTER")
                            {
                                subtypeName = componentType.Type!.Tag;
                            }

                            UnusedUserProvidedTypes.Remove(entry.Name!);

                            if (
                                isReturn &&
                                intent
                                    is UserProvidedData.PointerFunctionDataIntent.Ref
                                    or UserProvidedData.PointerFunctionDataIntent.In
                                    or UserProvidedData.PointerFunctionDataIntent.Out
                                    or UserProvidedData.PointerFunctionDataIntent.OutArray
                            )
                            {
                                Console.WriteLine($"{FunctionSignature.Name}: intent `{intent}` unsupported for return types; falling back to IntPtr");
                                typeName = "IntPtr";
                            }
                            else
                            {
                                switch (intent)
                                {
                                    case UserProvidedData.PointerFunctionDataIntent.IntPtr:
                                        typeName = "IntPtr";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.Ref:
                                        typeName = $"ref {subtypeName}";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.In:
                                        typeName = CoreMode ? $"in {subtypeName}" : $"ref {subtypeName}";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.Out:
                                        typeName = $"out {subtypeName}";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.Array:
                                        typeName = CoreMode ? $"Span<{subtypeName}>" : $"{subtypeName}[]";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.OutArray:
                                        typeName = CoreMode ? $"Span<{subtypeName}>" : $"[Out] {subtypeName}[]";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.Pointer:
                                        typeName = $"{subtypeName}*";
                                        break;
                                    case UserProvidedData.PointerFunctionDataIntent.Unknown:
                                    default:
                                        typeName = "IntPtr";
                                        containsUnknownRef = true;
                                        break;
                                }
                            }

                            if (CoreMode && isReturn && (intent == UserProvidedData.PointerFunctionDataIntent.Array))
                            {
                                FunctionSignature.ReturnIntent = FunctionSignatureType.ReturnIntentType.Array;
                            }
                        }
                        else
                        {
                            typeName = isReturn ? $"{subtypeName}*" : $"ref {subtypeName}";
                            containsUnknownRef = true;
                            unknownPointerFunctionData.Append(
                                $"{{ (\"{entry.Name!}\", \"{componentName}\"), PointerFunctionDataIntent.Unknown }}, // {entry.Header}\n"
                            );
                        }
                    }
                    else
                    {
                        var foundTypedef = GetTypeFromTypedefMap(componentType);
                        typeName = CSharpTypeFromFFI(foundTypedef, TypeContext.FunctionData);
                        if (typeName == "FUNCTION_POINTER")
                        {
                            if (componentType.Tag == "SDL_FunctionPointer")
                            {
                                typeName = "IntPtr";
                            }
                            else
                            {
                                typeName = $"{componentType.Tag}";
                            }
                        }
                    }

                    if (isReturn)
                    {
                        FunctionSignature.ReturnType = typeName;
                        if (FunctionSignature.ReturnType == "FUNCTION_POINTER")
                        {
                            FunctionSignature.ReturnType = "IntPtr";
                        }
                    }
                    else
                    {
                        FunctionSignature.ParameterTypesNames.Add((typeName, SanitizeName(componentName)));
                    }
                }

                foreach (var (type, name) in FunctionSignature.ParameterTypesNames)
                {
                    if (FunctionSignature.ParameterString.Length > 0)
                    {
                        FunctionSignature.ParameterString.Append(", ");
                    }

                    FunctionSignature.ParameterString.Append($"{type} {name}");
                }

                if (!CoreMode && FunctionSignature.RequiresStringMarshalling)
                {
                    definitions.Append(
                        $"[DllImport(nativeLibName, EntryPoint = \"{FunctionSignature.Name}\", CallingConvention = CallingConvention.Cdecl)]\n"
                    );
                    definitions.Append(
                        $"private static extern {FunctionSignature.ReturnType.Replace("UTF8_STRING", "IntPtr")} INTERNAL_{FunctionSignature.Name}("
                    );

                    definitions.Append(FunctionSignature.ParameterString.ToString().Replace("UTF8_STRING", "byte*"));
                    definitions.Append(");");

                    if (containsUnknownRef)
                    {
                        definitions.Append(" // WARN_UNKNOWN_POINTER_PARAMETER");
                    }

                    definitions.Append('\n');

                    definitions.Append($"public static {FunctionSignature.ReturnType.Replace("UTF8_STRING", "string")} {FunctionSignature.Name}(");
                    definitions.Append(FunctionSignature.ParameterString.ToString().Replace("UTF8_STRING", "string"));
                    definitions.Append(")\n{\n");

                    foreach (var stringParam in FunctionSignature.HeapAllocatedStringParams)
                    {
                        definitions.Append($"var {stringParam}UTF8 = EncodeAsUTF8({stringParam});\n");
                    }

                    if (FunctionSignature.HeapAllocatedStringParams.Count == 0)
                    {
                        definitions.Append("return ");
                    }
                    else if (FunctionSignature.ReturnType != "void")
                    {
                        definitions.Append("var result = ");
                    }

                    if (FunctionSignature.ReturnIntent == FunctionSignatureType.ReturnIntentType.String)
                    {
                        definitions.Append("DecodeFromUTF8(");
                    }

                    definitions.Append($"INTERNAL_{FunctionSignature.Name}(");
                    var isInitialParameter = true;
                    foreach (var (typeName, name) in FunctionSignature.ParameterTypesNames)
                    {
                        if (!isInitialParameter)
                        {
                            definitions.Append(", ");
                        }

                        isInitialParameter = false;

                        if (typeName.StartsWith("ref"))
                        {
                            definitions.Append("ref ");
                        }
                        else if (typeName.StartsWith("out"))
                        {
                            definitions.Append("out ");
                        }

                        if (FunctionSignature.HeapAllocatedStringParams.Contains(name))
                        {
                            definitions.Append($"{name}UTF8");
                        }
                        else
                        {
                            definitions.Append(name);
                        }
                    }

                    var unknownMemoryOwner = false;
                    if (FunctionSignature.ReturnIntent == FunctionSignatureType.ReturnIntentType.String)
                    {
                        definitions.Append(')');

                        if (UserProvidedData.ReturnedCharPtrMemoryOwners.TryGetValue(FunctionSignature.Name, value: out var memoryOwner))
                        {
                            UnusedUserProvidedTypes.Remove(FunctionSignature.Name);
                            unknownMemoryOwner = memoryOwner == UserProvidedData.ReturnedCharPtrMemoryOwner.Unknown;

                            if (memoryOwner == UserProvidedData.ReturnedCharPtrMemoryOwner.Caller)
                            {
                                definitions.Append(", shouldFree: true");
                            }
                        }
                        else
                        {
                            unknownMemoryOwner = true;
                            unknownReturnedCharPtrMemoryOwners.Append(
                                $"{{ \"{FunctionSignature.Name!}\", ReturnedCharPtrMemoryOwner.Unknown }}, // {entry.Header}\n"
                            );
                        }
                    }

                    definitions.Append(");");

                    if (unknownMemoryOwner)
                    {
                        definitions.Append(" // WARN_UNKNOWN_RETURNED_CHAR_PTR_MEMORY_OWNER");
                    }

                    definitions.Append('\n');

                    if (FunctionSignature.HeapAllocatedStringParams.Count > 0)
                    {
                        definitions.Append('\n');
                        foreach (var stringParam in FunctionSignature.HeapAllocatedStringParams)
                        {
                            definitions.Append($"SDL_free((IntPtr){stringParam}UTF8);\n");
                        }

                        if (FunctionSignature.ReturnType != "void")
                        {
                            definitions.Append("return result;\n");
                        }
                    }

                    definitions.Append("}\n\n");
                }
                else
                {
                    if (CoreMode)
                    {
                        if (FunctionSignature.RequiresStringMarshalling)
                        {
                            definitions.Append("[LibraryImport(nativeLibName, StringMarshalling = StringMarshalling.Utf8)]");
                        }
                        else
                        {
                            definitions.Append("[LibraryImport(nativeLibName)]\n");
                        }

                        definitions.Append($"[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]\n");

                        // Handle array marshalling
                        if (FunctionSignature.ReturnIntent == FunctionSignatureType.ReturnIntentType.Array)
                        {
                            if (UserProvidedData.ReturnedArrayCountParamNames.TryGetValue(FunctionSignature.Name, value: out var countParamName))
                            {
                                UnusedUserProvidedTypes.Remove(FunctionSignature.Name);
                                definitions.Append($"[return: MarshalUsing(CountElementName = \"{countParamName}\")]\n");
                            }
                            else
                            {
                                unknownReturnedArrayCountParamNames.Append(
                                    $"{{ \"{FunctionSignature.Name!}\", \"WARN_MISSING_COUNT_PARAM_NAME\" }}, // {entry.Header}\n"
                                );
                            }
                        }

                        // Handle string marshalling
                        else if (FunctionSignature.ReturnIntent == FunctionSignatureType.ReturnIntentType.String)
                        {
                            if (UserProvidedData.ReturnedCharPtrMemoryOwners.TryGetValue(FunctionSignature.Name, value: out var memoryOwner))
                            {
                                UnusedUserProvidedTypes.Remove(FunctionSignature.Name);
                                if (memoryOwner == UserProvidedData.ReturnedCharPtrMemoryOwner.Caller)
                                {
                                    definitions.Append("[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]\n");
                                }
                                else
                                {
                                    definitions.Append("[return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]\n");
                                }
                            }
                            else
                            {
                                unknownReturnedCharPtrMemoryOwners.Append(
                                    $"{{ \"{FunctionSignature.Name!}\", ReturnedCharPtrMemoryOwner.Unknown }}, // {entry.Header}\n"
                                );
                            }
                        }

                        definitions.Append($"public static partial {FunctionSignature.ReturnType.Replace("UTF8_STRING", "string")} {entry.Name}(");
                    }
                    else
                    {
                        definitions.Append("[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]\n");
                        definitions.Append($"public static extern {FunctionSignature.ReturnType} {entry.Name!}(");
                    }

                    definitions.Append(FunctionSignature.ParameterString.ToString().Replace("UTF8_STRING", "string"));

                    definitions.Append("); ");
                    if (containsUnknownRef)
                    {
                        definitions.Append("// WARN_UNKNOWN_POINTER_PARAMETER");
                    }

                    definitions.Append("\n\n");
                }
            }
        }

        var outputFilename = CoreMode ? "SDL3.Core.cs" : "SDL3.Legacy.cs";

        File.WriteAllText(
            path: Path.Combine(sdlBindingsDir.FullName, outputFilename),
            contents: CompileBindingsCSharp(definitions.ToString())
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");
        if (unknownPointerFunctionData.Length > 0)
        {
            Console.Write($"new pointer parameters (add these to `PointerFunctionDataIntents` in UserProvidedData.cs:\n{unknownPointerFunctionData}\n");
        }

        if (unknownReturnedCharPtrMemoryOwners.Length > 0)
        {
            Console.Write(
                $"new returned char pointers (add these to `ReturnedCharPtrMemoryOwners` in UserProvidedData.cs:\n{unknownReturnedCharPtrMemoryOwners}\n"
            );
        }

        if (unknownReturnedArrayCountParamNames.Length > 0)
        {
            Console.Write(
                $"new returned arrays (add these to `ReturnedArrayCountParamNames` in UserProvidedData.cs and specify the name of the param that contains the element count:\n{unknownReturnedArrayCountParamNames}\n"
            );
        }

        if (undefinedFunctionPointers.Length > 0)
        {
            Console.Write(
                $"new undefined function pointers (add these to `DelegateDefinitions` in UserProvidedData.cs:\n{undefinedFunctionPointers}\n"
            );
        }

        if (unpopulatedFlagDefinitions.Length > 0)
        {
            Console.Write($"new unpopulated flag enums (add these to `FlagEnumDefinitions` in UserProvidedData.cs:\n{unpopulatedFlagDefinitions}\n");
        }

        if (UnusedUserProvidedTypes.Count > 0)
        {
            Console.Write("unused definitions in UserProvidedData.cs:\n");
            foreach (var definition in UnusedUserProvidedTypes)
            {
                Console.Write($"{definition}\n");
            }
        }

        return 0;
    }

    private static FileInfo FindInPath(string exeName)
    {
        var envPath = Environment.GetEnvironmentVariable("PATH");
        if (envPath != null)
        {
            foreach (var envPathDir in envPath.Split(Path.PathSeparator))
            {
                var path = Path.Combine(envPathDir, exeName);
                if (File.Exists(path))
                {
                    return new FileInfo(path);
                }
            }
        }

        return new FileInfo("");
    }

    private static Process RunProcess(FileInfo exe, string args, bool redirectStdOut = false, DirectoryInfo? workingDir = null)
    {
        var process = new Process();
        process.StartInfo.FileName = exe.FullName;
        process.StartInfo.Arguments = args;
        process.StartInfo.RedirectStandardOutput = redirectStdOut;
        process.StartInfo.WorkingDirectory = workingDir?.FullName ?? AppContext.BaseDirectory;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new SystemException($@"process `{exe.FullName} {args}` failed!!\n");
        }

        return process;
    }

    private static string CompileBindingsCSharp(string definitions)
    {
        string output;
        if (CoreMode)
        {
            output = @"// NOTE: This file is auto-generated.
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.CompilerServices;
using System.Text;

namespace SDL3;

public static unsafe partial class SDL
{
    // Custom marshaller for SDL-owned strings returned by SDL.
    [CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(SDLOwnedStringMarshaller))]
    public static unsafe class SDLOwnedStringMarshaller
    {
        /// <summary>
        /// Converts an unmanaged string to a managed version.
        /// </summary>
        /// <returns>A managed string.</returns>
        public static string ConvertToManaged(byte* unmanaged)
            => Marshal.PtrToStringUTF8((IntPtr)unmanaged);
    }

    // Custom marshaller for caller-owned strings returned by SDL.
    [CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(CallerOwnedStringMarshaller))]
    public static unsafe class CallerOwnedStringMarshaller
    {
        /// <summary>
        /// Converts an unmanaged string to a managed version.
        /// </summary>
        /// <returns>A managed string.</returns>
        public static string ConvertToManaged(byte* unmanaged)
            => Marshal.PtrToStringUTF8((IntPtr)unmanaged);

        /// <summary>
        /// Free the memory for a specified unmanaged string.
        /// </summary>
        public static void Free(byte* unmanaged)
            => SDL_free((IntPtr)unmanaged);
    }

    // Taken from https://github.com/ppy/SDL3-CS
    // C# bools are not blittable, so we need this workaround
    public readonly record struct SDLBool
    {
        private readonly byte value;

        internal const byte FALSE_VALUE = 0;
        internal const byte TRUE_VALUE = 1;

        internal SDLBool(byte value)
        {
            this.value = value;
        }

        public static implicit operator bool(SDLBool b)
        {
            return b.value != FALSE_VALUE;
        }

        public static implicit operator SDLBool(bool b)
        {
            return new SDLBool(b ? TRUE_VALUE : FALSE_VALUE);
        }

        public bool Equals(SDLBool other)
        {
            return other.value == value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
";
        }
        else
        {
            output = @"// NOTE: This file is auto-generated.
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL3
{

public static unsafe class SDL
{
    private static byte* EncodeAsUTF8(string str)
    {
        if (str == null)
        {
            return (byte*) 0;
        }

        var size = (str.Length * 4) + 1;
        var buffer = (byte*) SDL_malloc((UIntPtr) size);
        fixed (char* strPtr = str)
        {
            Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, size);
        }

        return buffer;
    }

    private static string DecodeFromUTF8(IntPtr ptr, bool shouldFree = false)
    {
        if (ptr == IntPtr.Zero)
        {
            return null;
        }

        var end = (byte*) ptr;
        while (*end != 0)
        {
            end++;
        }

        var result = new string(
            (sbyte*) ptr,
            0,
            (int) (end - (byte*)ptr),
            System.Text.Encoding.UTF8
        );

        if (shouldFree)
        {
            SDL_free(ptr);
        }

        return result;
    }

    // Taken from https://github.com/ppy/SDL3-CS
    // C# bools are not blittable, so we need this workaround
    public struct SDLBool
    {
        private readonly byte value;

        internal const byte FALSE_VALUE = 0;
        internal const byte TRUE_VALUE = 1;

        internal SDLBool(byte value)
        {
            this.value = value;
        }

        public static implicit operator bool(SDLBool b)
        {
            return b.value != FALSE_VALUE;
        }

        public static implicit operator SDLBool(bool b)
        {
            return new SDLBool(b ? TRUE_VALUE : FALSE_VALUE);
        }

        public bool Equals(SDLBool other)
        {
            return other.value == value;
        }

        public override bool Equals(object rhs)
        {
            if (rhs is bool)
            {
                return Equals((SDLBool)(bool)rhs);
            }
            else if (rhs is SDLBool)
            {
                return Equals((SDLBool)rhs);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
";
        }

        output += $@"
    private const string nativeLibName = ""SDL3"";

    {definitions}
}}
";

        if (!CoreMode)
        {
            output += "}";
        }

        return output;
    }

    private static RawFFIEntry GetTypeFromTypedefMap(RawFFIEntry type)
    {
        if (type.Tag.StartsWith("SDL_"))
        {
            // preserve flag types
            if (IsFlagType(type.Tag))
            {
                return type;
            }

            if (TypedefMap.TryGetValue(type.Tag, value: out var value))
            {
                return value;
            }
        }

        return type;
    }

    private static string CSharpTypeFromFFI(RawFFIEntry type, TypeContext context)
    {
        if ((type.Tag == "pointer") && IsDefinedType(type.Type!.Tag))
        {
            var subtype = GetTypeFromTypedefMap(type.Type!);
            var subtypeName = CSharpTypeFromFFI(subtype, context);

            if (subtypeName == "char")
            {
                return context == TypeContext.StructField ? "byte*" : "UTF8_STRING";
            }

            return context switch
            {
                TypeContext.StructField => $"{subtypeName}*",
                _                       => "IntPtr",
            };
        }

        return type.Tag switch
        {
            "_Bool"            => "SDLBool",
            "Sint8"            => "sbyte",
            "Sint16"           => "short",
            "int"              => "int",
            "Sint32"           => "int",
            "long"             => "long",
            "Sint64"           => "long",
            "Uint8"            => "byte",
            "unsigned-short"   => "ushort",
            "Uint16"           => "ushort",
            "unsigned-int"     => "uint",
            "Uint32"           => "uint",
            "unsigned-long"    => "ulong",
            "Uint64"           => "ulong",
            "float"            => "float",
            "double"           => "double",
            "size_t"           => "UIntPtr",
            "wchar_t"          => "char",
            "unsigned-char"    => "byte",
            "void"             => "void",
            "pointer"          => "IntPtr",
            "function-pointer" => "FUNCTION_POINTER",
            "enum"             => type.Name!,
            "struct"           => type.Name!,
            "array"            => "INLINE_ARRAY",
            "union"            => type.Name!,
            _                  => type.Tag,
        };
    }

    private static string SanitizeName(string unsanitizedName)
    {
        return unsanitizedName switch
        {
            "internal" => "@internal",
            "event"    => "@event",
            "override" => "@override",
            "base"     => "@base",
            "lock"     => "@lock",
            "string"   => "@string",
            ""         => "_",
            _          => unsanitizedName,
        };
    }

    private static bool IsDefinedType(string? typeName)
    {
        if (typeName == null) return false;

        return
            (typeName != "void") && (
                !typeName.StartsWith("SDL_") // assume no SDL prefix == std library or primitive typename
                || DefinedTypes.Contains(typeName)
            );
    }

    private static void ConstructStruct(string structName, RawFFIEntry entry, StringBuilder definitions)
    {
        StructDefinition.Reset();
        ConstructStructFields(entry, typePrefix: $"{structName}_");

        if (StructDefinition.ContainsUnion)
        {
            definitions.Append("[StructLayout(LayoutKind.Explicit)]\n");
            definitions.Append($"public struct {structName}\n{{\n");

            foreach (var (offset, field) in StructDefinition.OffsetFields)
            {
                definitions.Append($"[FieldOffset({offset})]\n");
                definitions.Append($"{field}\n");
            }

            definitions.Append("}\n\n");
        }
        else
        {
            definitions.Append("[StructLayout(LayoutKind.Sequential)]\n");
            definitions.Append($"public struct {structName}\n{{\n");

            foreach (var (offset, field) in StructDefinition.OffsetFields)
            {
                definitions.Append($"{field}\n");
            }

            definitions.Append("}\n\n");
        }
    }

    private static void ConstructStructFields(
        RawFFIEntry entry,
        uint byteOffset = 0,
        string typePrefix = "",
        string namePrefix = ""
    )
    {
        if (entry.Tag == "union")
        {
            StructDefinition.ContainsUnion = true;
        }

        foreach (var field in entry.Fields!)
        {
            var fieldName = SanitizeName($"{namePrefix}{field.Name!}");
            var fieldTypedef = GetTypeFromTypedefMap(type: field.Type!);
            var fieldTypeName = CSharpTypeFromFFI(fieldTypedef, TypeContext.StructField);
            if ((fieldTypeName == "") && (fieldTypedef.Tag == "union"))
            {
                ConstructStructFields(
                    fieldTypedef,
                    byteOffset: byteOffset + (uint) field.BitOffset! / 8,
                    typePrefix,
                    namePrefix: $"{fieldName}_"
                );
            }
            else if ((fieldTypeName == "") && (fieldTypedef.Tag == "struct"))
            {
                var internalStructName = $"INTERNAL_{typePrefix}{fieldName}";
                StructDefinition.InternalStructs.Add(internalStructName, fieldTypedef);
                StructDefinition.OffsetFields.Add(
                    (
                        byteOffset + (uint) field.BitOffset! / 8,
                        $"public {internalStructName} {fieldName};"
                    )
                );
            }
            else if (fieldTypeName == "INLINE_ARRAY")
            {
                var elementTypeName = CSharpTypeFromFFI(type: fieldTypedef.Type!, TypeContext.StructField);
                if (elementTypeName.StartsWith("SDL_")) // fixed buffers only work on primitives
                {
                    var elementByteSize = GetTypeFromTypedefMap(fieldTypedef.Type!).BitSize ?? 0 / 8;
                    for (var i = 0; i < fieldTypedef.Size; i++)
                    {
                        StructDefinition.OffsetFields.Add(
                            (
                                byteOffset + (uint) (elementByteSize * i) + (uint) field.BitOffset! / 8,
                                $"public {elementTypeName} {fieldName}{i};"
                            )
                        );
                    }
                }
                else
                {
                    StructDefinition.OffsetFields.Add(
                        (
                            byteOffset + (uint) field.BitOffset! / 8,
                            $"public fixed {elementTypeName} {fieldName}[{fieldTypedef.Size}];"
                        )
                    );
                }
            }
            else if (fieldTypeName == "FUNCTION_POINTER")
            {
                string context;
                if (field.Type!.Tag == "function-pointer")
                {
                    context = "WARN_ANONYMOUS_FUNCTION_POINTER";
                }
                else
                {
                    context = $"{field.Type!.Tag}";
                }

                StructDefinition.OffsetFields.Add(
                    (
                        byteOffset + (uint) field.BitOffset! / 8,
                        $"public IntPtr {fieldName}; // {context}"
                    )
                );
            }
            else
            {
                StructDefinition.OffsetFields.Add(
                    (
                        byteOffset + (uint) field.BitOffset! / 8,
                        $"public {fieldTypeName} {fieldName};"
                    )
                );
            }
        }
    }

    private static bool IsFlagType(string name)
    {
        return name.EndsWith("Flags") || UserProvidedData.FlagTypes.Contains(name);
    }

    [GeneratedRegex(@"#define\s+(?<hintName>SDL_HINT_[A-Z0-9_]+)\s+""(?<value>.+)""")]
    private static partial Regex HintDefinitionRegex();

    [GeneratedRegex(@"#define\s+(?<keycodeName>SDLK_[A-Z0-9_]+)\s+(?<value>0x[0-9a-f]+u)")]
    private static partial Regex KeycodeDefinitionRegex();

    [GeneratedRegex(@"#define\s+(?<propName>SDL_PROP_[A-Z0-9_]+)\s+""(?<value>[^""]*)""")]
    private static partial Regex PropDefinitionRegex();

    [GeneratedRegex(@"(SDL_FORCE_INLINE|SDLMAIN_DECLSPEC).*\s+(?<functionName>[a-zA-Z0-9_]+)\(.*\)")]
    private static partial Regex InlinedFunctionRegex();
}
