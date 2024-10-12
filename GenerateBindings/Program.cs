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
        Return,
        Parameter,
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
        public string Name { get; set; } = "";
        public string ReturnType { get; set; } = "";
        public List<(string, string)> ParameterTypesNames { get; } = new();
        public List<string> HeapAllocatedStringParams { get; } = new();
        public StringBuilder ParameterString { get; } = new();

        public void Reset()
        {
            Name = "";
            ReturnType = "";
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

    private static DirectoryInfo? GetSDLWikiDirectory(string[] args)
    {
        for (var i = 1; i < args.Length; i++)
        {
            if (args[i] == "--wiki-repo-root-dir")
            {
                return new DirectoryInfo(args[i + 1]);
            }
            if (args[i].StartsWith("--wiki-repo-root-dir="))
            {
                return new DirectoryInfo(args[i]["--wiki-repo-root-dir=".Length..]);
            }
        }

        return null;
    }

    private static int Main(string[] args)
    {
        // PARSE INPUT
        if (args.Length < 1)
        {
            Console.WriteLine("usage: GenerateBindings <sdl-repo-root-dir> [--core] " +
                              "[--wiki-repo-root-dir=<wiki-repo-root-dir>]");
            Console.WriteLine("sdl-repo-root-dir: The root directory of SDL3 code.");
            Console.WriteLine("--core: Bindgen for .NET Core. If this is not set, will bindgen for .NET Framework.");
            Console.WriteLine(
                "--wiki-repo-root-dir=<wiki-repo-root-dir>: The root directory of the libsdl-org/sdlwiki repository." +
                " If set, this will be used to generate documentation comments.");
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
        var wikiParser = new WikiDocsParser(GetSDLWikiDirectory(args));

        // PARSE FFI.JSON

        foreach (var key in UserProvidedData.PointerParametersIntents.Keys)
        {
            UnusedUserProvidedTypes.Add(key.Item1);
        }

        foreach (var key in UserProvidedData.ReturnedCharPtrMemoryOwners.Keys)
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
        var unknownPointerParameters = new StringBuilder();
        var unknownReturnedCharPtrMemoryOwners = new StringBuilder();
        var undefinedFunctionPointers = new StringBuilder();
        var unpopulatedFlagDefinitions = new StringBuilder();
        var currentSourceFile = "";

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

                if (currentSourceFile.EndsWith("SDL_hints.h"))
                {
                    IEnumerable<string> hintsFileLines = File.ReadLines(Path.Combine(sdlDir.FullName, "include/SDL3/SDL_hints.h"));
                    foreach (var line in hintsFileLines)
                    {
                        var match = HintDefinitionRegex().Match(line);
                        if (match.Success)
                        {
                            var hintName = match.Groups["hintName"].Value;
                            wikiParser.Preload(hintName, definitions);
                            definitions.Append($"public const string {hintName} = \"{match.Groups["value"].Value}\";\n");
                        }
                    }

                    definitions.Append('\n');
                }
            }

            if (entry.Tag == "enum")
            {
                wikiParser.Preload(entry.Name, definitions);
                definitions.Append($"public enum {entry.Name!}\n{{\n");
                DefinedTypes.Add(entry.Name!);

                foreach (var enumValue in entry.Fields!)
                {
                    wikiParser.RegisterContainingType(enumValue.Name, entry.Name);
                    wikiParser.Preload(enumValue.Name, definitions, false);
                    definitions.Append($"{enumValue.Name} = {(int) enumValue.Value!},\n");
                }

                definitions.Append("}\n\n");
            }

            else if (entry.Tag == "typedef")
            {
                if (entry.Type!.Tag == "function-pointer")
                {
                    wikiParser.Preload(entry.Name, definitions);
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
                    wikiParser.Preload(entry.Name, definitions);
                    var enumType = CSharpTypeFromFFI(type: entry.Type!, TypeContext.StructField);
                    definitions.Append($"public enum {entry.Name} : {enumType}\n{{\n");
                    definitions.Append("SDLK_SCANCODE_MASK = 0x40000000,\n");

                    IEnumerable<string> hintsFileLines = File.ReadLines(Path.Combine(sdlDir.FullName, "include/SDL3/SDL_keycode.h"));

                    foreach (var line in hintsFileLines)
                    {
                        var match = KeycodeDefinitionRegex().Match(line);
                        if (match.Success)
                        {
                            var enumEntry = match.Groups["keycodeName"].Value;
                            wikiParser.RegisterContainingType(enumEntry, entry.Name);
                            // wikiParser.Preload(enumEntry, definitions, false);
                            definitions.Append($"{enumEntry} = {match.Groups["value"].Value},\n");
                        }
                    }

                    definitions.Append("}\n\n");
                }
                else if (entry.Name != null && IsFlagType(entry.Name))
                {
                    wikiParser.Preload(entry.Name, definitions);
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
                            wikiParser.RegisterContainingType(enumEntry, entry.Name);
                            wikiParser.Preload(enumEntry, definitions, false);
                            if (enumEntry.Contains('='))
                            {
                                definitions.Append($"{enumEntry},\n");
                            }
                            else
                            {
                                definitions.Append($"{enumEntry} = 0x{BigInteger.Pow(value: 2, i):X},\n");
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

                wikiParser.Preload(entry.Name, definitions);
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

                var returnTypedef = GetTypeFromTypedefMap(type: entry.ReturnType!);
                FunctionSignature.ReturnType = CSharpTypeFromFFI(returnTypedef, TypeContext.Return);
                if (FunctionSignature.ReturnType == "FUNCTION_POINTER")
                {
                    FunctionSignature.ReturnType = "IntPtr";
                }

                var containsUnknownRef = false;
                foreach (var parameter in entry.Parameters!)
                {
                    var parameterTypedef = GetTypeFromTypedefMap(type: parameter.Type!);

                    var name = SanitizeName(parameter.Name!);
                    string typeName;

                    if ((parameter.Type!.Tag == "pointer") && IsDefinedType(parameter.Type!.Type!.Tag))
                    {
                        var subtype = GetTypeFromTypedefMap(type: parameter.Type!.Type!);
                        var subtypeName = CSharpTypeFromFFI(subtype, TypeContext.Parameter);

                        if (subtypeName == "UTF8_STRING") // pointer to an array; give up
                        {
                            typeName = "IntPtr";
                        }
                        else if (subtypeName == "char")
                        {
                            typeName = "UTF8_STRING";
                            FunctionSignature.HeapAllocatedStringParams.Add(name);
                        }
                        else if (UserProvidedData.PointerParametersIntents.TryGetValue(key: (entry.Name!, parameter.Name!), value: out var intent))
                        {
                            if (subtypeName == "FUNCTION_POINTER")
                            {
                                subtypeName = parameter.Type!.Type!.Tag;
                            }

                            UnusedUserProvidedTypes.Remove(entry.Name!);

                            switch (intent)
                            {
                                case UserProvidedData.PointerParameterIntent.IntPtr:
                                    typeName = "IntPtr";
                                    break;
                                case UserProvidedData.PointerParameterIntent.Ref:
                                    typeName = $"ref {subtypeName}";
                                    break;
								case UserProvidedData.PointerParameterIntent.In:
									typeName = CoreMode ? $"in {subtypeName}" : $"ref {subtypeName}";
                                    break;
                                case UserProvidedData.PointerParameterIntent.Out:
                                    typeName = $"out {subtypeName}";
                                    break;
                                case UserProvidedData.PointerParameterIntent.Array:
									typeName = CoreMode ? $"Span<{subtypeName}>" : $"{subtypeName}[]";
                                    break;
                                case UserProvidedData.PointerParameterIntent.Pointer:
									typeName = CoreMode ? $"Span<{subtypeName}>" : $"{subtypeName}*";
                                    break;
                                case UserProvidedData.PointerParameterIntent.Unknown:
                                default:
                                    typeName = "IntPtr";
                                    containsUnknownRef = true;
                                    break;
                            }
                        }
                        else
                        {
                            typeName = $"ref {subtypeName}";
                            containsUnknownRef = true;
                            unknownPointerParameters.Append(
                                $"{{ (\"{entry.Name!}\", \"{parameter.Name!}\"), PointerParameterIntent.Unknown }}, // {entry.Header}\n"
                            );
                        }
                    }
                    else
                    {
                        typeName = CSharpTypeFromFFI(parameterTypedef, TypeContext.Parameter);
                        if (typeName == "FUNCTION_POINTER")
                        {
                            if (parameter.Type!.Tag == "SDL_FunctionPointer")
                            {
                                typeName = "IntPtr";
                            }
                            else
                            {
                                typeName = $"{parameter.Type!.Tag}";
                            }
                        }
                    }

                    FunctionSignature.ParameterTypesNames.Add((typeName, name));
                }

                foreach (var (type, name) in FunctionSignature.ParameterTypesNames)
                {
                    if (FunctionSignature.ParameterString.Length > 0)
                    {
                        FunctionSignature.ParameterString.Append(", ");
                    }

                    FunctionSignature.ParameterString.Append($"{type} {name}");
                }

                if (!CoreMode && ((FunctionSignature.HeapAllocatedStringParams.Count > 0) || (FunctionSignature.ReturnType == "UTF8_STRING")))
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

                    wikiParser.Preload(entry.Name, definitions);
                    definitions.Append($"public static {FunctionSignature.ReturnType.Replace("UTF8_STRING", "string")} {FunctionSignature.Name}(");
                    definitions.Append(FunctionSignature.ParameterString.ToString().Replace("UTF8_STRING", "string"));
                    definitions.Append(")\n{\n");

                    foreach (var stringParam in FunctionSignature.HeapAllocatedStringParams)
                    {
                        definitions.Append($"var {stringParam}UTF8 = EncodeAsUTF8({stringParam});\n");
                    }

                    var returnsCharPtr = FunctionSignature.ReturnType == "UTF8_STRING";
                    if (FunctionSignature.HeapAllocatedStringParams.Count == 0)
                    {
                        definitions.Append("return ");
                    }
                    else if (FunctionSignature.ReturnType != "void")
                    {
                        definitions.Append("var result = ");
                    }

                    if (returnsCharPtr)
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
                    if (returnsCharPtr)
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
                        wikiParser.Preload(entry.Name, definitions);
                        if ((FunctionSignature.HeapAllocatedStringParams.Count > 0) || (FunctionSignature.ReturnType == "UTF8_STRING"))
                        {
                            definitions.Append("[LibraryImport(nativeLibName, StringMarshalling = StringMarshalling.Utf8)]");
                        }
                        else
                        {
                            definitions.Append("[LibraryImport(nativeLibName)]\n");
                        }
                        definitions.Append($"[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]\n");

                        // Handle string marshalling
                        if (FunctionSignature.ReturnType == "UTF8_STRING")
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

                        definitions.Append($"public static partial {FunctionSignature.ReturnType.ToString().Replace("UTF8_STRING", "string")} {entry.Name}(");
                    }
                    else
                    {
                        wikiParser.Preload(entry.Name, definitions);
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

        wikiParser.ProcessDocComments(definitions);

        var outputFilename = CoreMode ? "SDL3.Core.cs" : "SDL3.Legacy.cs";

        File.WriteAllText(
            path: Path.Combine(sdlBindingsDir.FullName, outputFilename),
            contents: CompileBindingsCSharp(definitions.ToString())
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");
        if (unknownPointerParameters.Length > 0)
        {
            Console.Write($"new pointer parameters (add these to `PointerParametersIntents` in UserProvidedData.cs:\n{unknownPointerParameters}\n");
        }

        if (unknownReturnedCharPtrMemoryOwners.Length > 0)
        {
            Console.Write(
                $"new returned char pointers (add these to `ReturnedCharPtrMemoryOwners` in UserProvidedData.cs:\n{unknownReturnedCharPtrMemoryOwners}\n"
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

    private static bool IsDefinedType(string typeName)
    {
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
}
