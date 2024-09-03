// See https://aka.ms/new-console-template for more information

// if c2ffi output and SDL commit file are present
// check provided SDL dir to see whether commit file matches
// if no match, run c2ffi and capture SDL commit in build dir
// else
// run c2ffi and capture SDL commit in build dir

// parse c2ffi json into structured data
// write data out via streamwriter in sane order (enums, structs, functions, alphabetical?)
// simple methods for spitting out typedefs and signatures (BuildFunctionSignature, BuildStructDef etc)
// write out to destination dir under "generated" dir
// run dotnet fmt?

using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GenerateBindings;

internal static class Program
{
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
        var outputDir = new FileInfo(Path.Combine(sdlBindingsDir.FullName, "generated"));
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

        Dictionary<string, RawFFIEntry> typedefMap = new();
        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if ((entry.Tag == "typedef") && entry.Name!.StartsWith("SDL_"))
            {
                typedefMap[entry.Name!] = entry.Type!;
            }
        }

        var sdlEnums = new StringBuilder();
        var sdlStructs = new StringBuilder();
        var sdlFunctions = new StringBuilder();

        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if (entry.Tag == "enum")
            {
                sdlEnums.Append($"public enum {entry.Name!}\n{{\n");

                foreach (var enumValue in entry.Fields!)
                {
                    sdlEnums.Append($"{enumValue.Name} = {(int) enumValue.Value!},\n");
                }

                sdlEnums.Append("}\n\n");
            }

            else if (entry.Tag == "struct")
            {
                sdlStructs.Append("[StructLayout(LayoutKind.Sequential)]\n");
                sdlStructs.Append($"public struct {entry.Name!}\n{{\n");

                foreach (var field in entry.Fields!)
                {
                    var name = SanitizeNames(field.Name!);
                    var type = CSharpTypeFromFFI(type: field.Type!, typedefMap);
                    if (type == "UNION")
                    {
                        type = $"UNION_{entry.Name}_{field.Name}";
                        sdlStructs.Append($"// public {type} {name}; // TODO: unhandled union\n");
                    }
                    else
                    {
                        sdlStructs.Append($"public {type} {name};\n");
                    }
                }

                sdlStructs.Append("}\n\n");
            }

            else if (entry.Tag == "function")
            {
                sdlFunctions.Append("[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]\n");
                var returnType = CSharpTypeFromFFI(type: entry.ReturnType!, typedefMap);
                sdlFunctions.Append($"public static extern {returnType} {entry.Name!}(");

                var initialParameter = true;
                foreach (var parameter in entry.Parameters!)
                {
                    if (!initialParameter)
                    {
                        sdlFunctions.Append(", ");
                    }
                    else
                    {
                        initialParameter = false;
                    }

                    var type = CSharpTypeFromFFI(type: parameter.Type!, typedefMap);
                    var name = SanitizeNames(parameter.Name!);
                    sdlFunctions.Append($"{type} {name}");
                }

                sdlFunctions.Append(");\n\n");
            }
        }


        File.WriteAllText(
            path: Path.Combine(outputDir.FullName, "SDL3.cs"),
            contents: CompileBindingsCSharp(
                enums: sdlEnums.ToString(),
                structs: sdlStructs.ToString(),
                functions: sdlFunctions.ToString()
            )
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");

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

    private static string CompileBindingsCSharp(string enums, string structs, string functions)
    {
        return $@"
using System.Runtime.InteropServices;

namespace SDL3;

public static class SDL
{{
    private const string nativeLibName = ""SDL3"";

    {enums}
    {structs}
    {functions}
}}
";
    }

    private static string CSharpTypeFromFFI(RawFFIEntry type, Dictionary<string, RawFFIEntry> typedefMap)
    {
        // TODO: should annotate these results with comments.
        // e.g. SDL_bool is a typedef of int, in a function signatures "int value" can be very misleading
        if (type.Tag.StartsWith("SDL_") && typedefMap.TryGetValue(type.Tag, value: out var value))
        {
            type = value;
        }

        return type.Tag switch
        {
            ":_Bool"            => "bool",
            ":int"              => "int",
            ":long"             => "long", // TODO: platform-dependent?
            ":unsigned-short"   => "ushort", // TODO: platform-dependent?
            ":unsigned-int"     => "uint",
            ":unsigned-long"    => "ulong", // TODO: platform-dependent?
            ":float"            => "float",
            ":double"           => "double",
            "Uint8"             => "byte",
            "Uint16"            => "UInt16",
            "Uint32"            => "UInt32",
            "Uint64"            => "UInt64",
            "Sint8"             => "sbyte",
            "Sint16"            => "Int16",
            "Sint32"            => "Int32",
            "Sint64"            => "Int64",
            "size_t"            => "UInt32", // TODO: i think this is platform-dependent
            ":void"             => "void",
            ":pointer"          => "IntPtr", // TODO: "pointer to what" is available in the metadata; include in a comment
            ":function-pointer" => "IntPtr", // TODO: no idea
            "va_list"           => "IntPtr", // TODO: almost certainly wrong
            ":enum"             => type.Name!,
            ":struct"           => type.Name!,
            ":array"            => $"{CSharpTypeFromFFI(type: type.Type!, typedefMap)}[]", // TODO: probably wrong
            "union"             => "UNION",
            _                   => type.Tag,
        };
    }

    private static string SanitizeNames(string unsanitizedName)
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
}
