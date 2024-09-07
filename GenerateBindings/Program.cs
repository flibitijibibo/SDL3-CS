using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace GenerateBindings;

internal static class Program
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

    private static readonly List<string> DefinedTypes = new();
    private static readonly Dictionary<string, RawFFIEntry> TypedefMap = new();
    private static readonly StructDefinitionType StructDefinition = new();

    private static int Main(string[] args)
    {
        // PARSE INPUT

        if (args.Length > 0)
        {
            Console.WriteLine("usage: SDL3_CS_SDL_REPO_ROOT=<sdl-repo-root-dir> GenerateBindings");
            return 1;
        }

        var sdlDir = new DirectoryInfo(Environment.GetEnvironmentVariable("SDL3_CS_SDL_REPO_ROOT") ?? "MISSING_ENV_VAR");
        var sdlBindingsDir = new FileInfo(Path.Combine(AppContext.BaseDirectory, "../../../../SDL3/"));
        var outputDir = sdlBindingsDir;
        var sdlBindingsProjectFile = new FileInfo(Path.Combine(sdlBindingsDir.FullName, "SDL3.csproj"));
        var c2ffiConfigTemplateFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "config_extract.json.template"));
        var c2ffiConfigFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "config_extract.json"));
        var ffiJsonFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "ffi.json"));
        var ffiJsonIntermediateDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "intermediate_ffis"));
        var sdlTargetCommitFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "sdl_last_targeted_commit"));

#if WINDOWS
        var c2ffiExe = new FileInfo(Path.Combine(AppContext.BaseDirectory, path2: "c2ffi.exe"));
        var gitExe = FindInPath("git.exe");
        var dotnetExe = FindInPath("dotnet.exe");
#else
        var c2ffiExe = new FileInfo(Path.Combine(AppContext.BaseDirectory, "c2ffi"));
        var gitExe = FindInPath("git");
        var dotnetExe = FindInPath("dotnet");
#endif

        // BUILD FFI.JSON

        // if (!sdlDir.Exists)
        // {
        //     Console.WriteLine($"ERROR: sdl dir `{sdlDir.FullName}` does not exist!");
        //     return 1;
        // }
        //
        // var isFFIJsonUpToDate = false;
        // if (gitExe.Exists && ffiJsonFile.Exists && sdlTargetCommitFile.Exists)
        // {
        //     Console.WriteLine("checking if ffi.json is up-to-date...");
        //     var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
        //     if (commit == File.ReadAllText(sdlTargetCommitFile.FullName))
        //     {
        //         Console.WriteLine($"ffi.json is up-to-date (SDL commit {commit.Trim()})!! skipping c2ffi...");
        //         isFFIJsonUpToDate = true;
        //     }
        // }
        //
        // if (!isFFIJsonUpToDate)
        // {
        //     if (ffiJsonIntermediateDir.Exists)
        //     {
        //         foreach (var file in ffiJsonIntermediateDir.GetFiles())
        //         {
        //             file.Delete();
        //         }
        //     }
        //     else
        //     {
        //         ffiJsonIntermediateDir.Create();
        //     }
        //
        //     if (c2ffiConfigFile.Exists)
        //     {
        //         c2ffiConfigFile.Delete();
        //     }
        //
        //     using (var writer = c2ffiConfigFile.CreateText())
        //     {
        //         using (var reader = c2ffiConfigTemplateFile.OpenText())
        //         {
        //             while (!reader.EndOfStream)
        //                 writer.WriteLine(
        //                     reader.ReadLine()!
        //                         .Replace("TEMPLATE_SDL_PATH", sdlDir.FullName)
        //                         .Replace("TEMPLATE_OUTPUT_DIR", ffiJsonIntermediateDir.FullName)
        //                 );
        //         }
        //     }
        //
        //     RunProcess(c2ffiExe, args: $"extract --config {c2ffiConfigFile.FullName}");
        //     RunProcess(c2ffiExe, args: $"merge --inputDirectoryPath {ffiJsonIntermediateDir.FullName} --outputFilePath {ffiJsonFile.FullName}");
        //
        //     if (gitExe.Exists)
        //     {
        //         var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
        //         File.WriteAllText(sdlTargetCommitFile.FullName, commit);
        //     }
        // }

        // PARSE FFI.JSON

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
        var undefinedFunctionPointers = new StringBuilder();
        var unpopulatedFlagDefinitions = new StringBuilder();
        var currentSourceFile = "";

        foreach (var entry in ffiData)
        {
            if ((entry.Header == null)
                || !Path.GetFileName(entry.Header).StartsWith("SDL_")
                || Path.GetFileName(entry.Header).StartsWith("SDL_stdinc.h"))
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
                        if (delegateDefinition.ReturnType == "WARN_PLACEHOLDER")
                        {
                            definitions.Append("// ");
                        }
                        else
                        {
                            definitions.Append("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]\n");
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
                            $"// public static delegate RETURN {entry.Name}(PARAMS)\t// WARN_UNDEFINED_FUNCTION_POINTER: {entry.Header}\n\n"
                        );
                        undefinedFunctionPointers.Append(
                            $"{{ \"{entry.Name}\", new DelegateDefinition {{ ReturnType = \"WARN_PLACEHOLDER\", Parameters = [] }} }},\t// {entry.Header}\n"
                        );
                    }
                }
                else if ((entry.Name != null) && entry.Name.EndsWith("Flags"))
                {
                    definitions.Append("[Flags]\n");
                    var enumType = CSharpTypeFromFFI(type: entry.Type!, TypeContext.StructField);
                    definitions.Append($"public enum {entry.Name} : {enumType}\n{{\n");

                    if (!UserProvidedData.FlagEnumDefinitions.TryGetValue(entry.Name, value: out var enumValues))
                    {
                        unpopulatedFlagDefinitions.Append($"{{ \"{entry.Name}\", [ ] }},\t// {entry.Header}\n");
                        definitions.Append("// WARN_UNPOPULATED_FLAG_ENUM\n");
                    }
                    else if (enumValues.Length == 0)
                    {
                        definitions.Append("// WARN_UNPOPULATED_FLAG_ENUM\n");
                    }
                    else
                    {
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

                definitions.Append("[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]\n");
                var returnTypedef = GetTypeFromTypedefMap(type: entry.ReturnType!);
                var returnType = CSharpTypeFromFFI(returnTypedef, TypeContext.Return);
                if (returnType == "FUNCTION_POINTER")
                {
                    returnType = "IntPtr";
                }

                definitions.Append($"public static extern {returnType} {entry.Name!}(");

                var initialParameter = true;
                var containsUnknownRef = false;
                foreach (var parameter in entry.Parameters!)
                {
                    if (!initialParameter)
                    {
                        definitions.Append(", ");
                    }
                    else
                    {
                        initialParameter = false;
                    }

                    var parameterTypedef = GetTypeFromTypedefMap(type: parameter.Type!);

                    string typeName;
                    if ((parameter.Type!.Tag == "pointer") && IsDefinedType(parameter.Type!.Type!.Tag))
                    {
                        var subtype = GetTypeFromTypedefMap(type: parameter.Type!.Type!);
                        var subtypeName = CSharpTypeFromFFI(subtype, TypeContext.Parameter);
                        if (UserProvidedData.PointerParametersIntents.TryGetValue(key: (entry.Name!, parameter.Name!), value: out var intent))
                        {
                            typeName = intent switch
                            {
                                UserProvidedData.PointerParameterIntent.Ref   => $"ref {subtypeName}",
                                UserProvidedData.PointerParameterIntent.Out   => $"out {subtypeName}",
                                UserProvidedData.PointerParameterIntent.Array => $"{subtypeName}*",
                                _                                             => $"ref {subtypeName}",
                            };
                            if (intent == UserProvidedData.PointerParameterIntent.Unknown)
                            {
                                containsUnknownRef = true;
                            }
                        }
                        else
                        {
                            typeName = $"ref {subtypeName}";
                            containsUnknownRef = true;
                            unknownPointerParameters.Append(
                                $"{{ (\"{entry.Name!}\", \"{parameter.Name!}\"), PointerParameterIntent.Unknown }},\t// {entry.Header}\n"
                            );
                        }
                    }
                    else
                    {
                        typeName = CSharpTypeFromFFI(parameterTypedef, TypeContext.Parameter);
                        if (typeName == "FUNCTION_POINTER")
                        {
                            typeName = $"/* {parameter.Type!.Tag} */ IntPtr";
                        }
                    }

                    var name = SanitizeName(parameter.Name!);
                    definitions.Append($"{typeName} {name}");
                }

                definitions.Append(");\t");
                if (containsUnknownRef)
                {
                    definitions.Append("// WARN_UNKNOWN_POINTER_PARAMETER: check for array usage");
                }

                definitions.Append("\n\n");
            }
        }

        File.WriteAllText(
            path: Path.Combine(outputDir.FullName, "SDL3.cs"),
            contents: CompileBindingsCSharp(definitions.ToString())
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");
        if (unknownPointerParameters.Length > 0)
        {
            Console.Write($"new pointer parameters (add these to `PointerParametersIntents` in UserProvidedData.cs:\n\n{unknownPointerParameters}");
        }

        if (undefinedFunctionPointers.Length > 0)
        {
            Console.Write(
                $"new undefined function pointers (add these to `DelegateDefinitions` in UserProvidedData.cs:\n\n{undefinedFunctionPointers}"
            );
        }

        if (unpopulatedFlagDefinitions.Length > 0)
        {
            Console.Write($"new unpopulated flag enums (add these to `FlagEnumDefinitions` in UserProvidedData.cs:\n\n{unpopulatedFlagDefinitions}");
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
        return $@"// NOTE: This file is auto-generated.

using System;
using System.Runtime.InteropServices;

namespace SDL3;

public static unsafe class SDL
{{
    private const string nativeLibName = ""SDL3"";

    {definitions}
}}
";
    }

    private static RawFFIEntry GetTypeFromTypedefMap(RawFFIEntry type)
    {
        if (type.Tag.StartsWith("SDL_") && TypedefMap.TryGetValue(type.Tag, value: out var value))
        {
            return value;
        }

        return type;
    }

    private static string CSharpTypeFromFFI(RawFFIEntry type, TypeContext context)
    {
        if ((type.Tag == "pointer") && IsDefinedType(type.Type!.Tag))
        {
            var subtype = GetTypeFromTypedefMap(type.Type!);
            var subtypeName = CSharpTypeFromFFI(subtype, context);

            return context switch
            {
                TypeContext.StructField => $"{subtypeName}*",
                _                       => "IntPtr",
            };
        }

        return type.Tag switch
        {
            "_Bool"            => "bool",
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
            "wchar_t"          => "char", // TODO: doubt it's this easy (sdl_hidapi)
            "unsigned-char"    => "byte", // TODO: confirm this (sdl_hidapi)
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
                for (var i = 0; i < fieldTypedef.Size; i++)
                {
                    StructDefinition.OffsetFields.Add(
                        (
                            byteOffset + (uint) field.BitOffset! / 8, // TODO: maybe use MarshalAs thing
                            $"public {elementTypeName} {fieldName}{i};"
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
                        $"public IntPtr {fieldName};\t// {context}"
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
}
