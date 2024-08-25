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

        if (args.Length != 1)
        {
            Console.WriteLine("usage: GenerateBindings <sdl-repo-root-dir>");
            return 1;
        }

        var sdlDir = new DirectoryInfo(args[0]);
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

        if (!sdlDir.Exists)
        {
            Console.WriteLine($"ERROR: specified sdl dir {sdlDir.FullName} does not exist!");
            return 1;
        }

        // BUILD FFI.JSON

        var isFFIJsonUpToDate = false;
        if (gitExe.Exists && ffiJsonFile.Exists && sdlTargetCommitFile.Exists)
        {
            Console.WriteLine("checking if ffi.json is up-to-date...");
            var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
            if (commit == File.ReadAllText(sdlTargetCommitFile.FullName))
            {
                Console.WriteLine($"ffi.json is up-to-date (SDL commit {commit.Trim()})!! skipping c2ffi...");
                isFFIJsonUpToDate = true;
            }
        }

        if (!isFFIJsonUpToDate)
        {
            if (ffiJsonIntermediateDir.Exists)
            {
                foreach (var file in ffiJsonIntermediateDir.GetFiles())
                {
                    file.Delete();
                }
            }
            else
            {
                ffiJsonIntermediateDir.Create();
            }

            if (c2ffiConfigFile.Exists)
            {
                c2ffiConfigFile.Delete();
            }

            using (var writer = c2ffiConfigFile.CreateText())
            {
                using (var reader = c2ffiConfigTemplateFile.OpenText())
                {
                    while (!reader.EndOfStream)
                        writer.WriteLine(
                            reader.ReadLine()!
                                .Replace("TEMPLATE_SDL_PATH", sdlDir.FullName)
                                .Replace("TEMPLATE_OUTPUT_DIR", ffiJsonIntermediateDir.FullName)
                        );
                }
            }

            RunProcess(c2ffiExe, args: $"extract --config {c2ffiConfigFile.FullName}");
            RunProcess(c2ffiExe, args: $"merge --inputDirectoryPath {ffiJsonIntermediateDir.FullName} --outputFilePath {ffiJsonFile.FullName}");

            if (gitExe.Exists)
            {
                var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
                File.WriteAllText(sdlTargetCommitFile.FullName, commit);
            }
        }

        // PARSE FFI.JSON

        var ffiData = JsonSerializer.Deserialize<FFIData>(File.ReadAllText(ffiJsonFile.FullName));
        if (ffiData == null)
        {
            Console.WriteLine($"failed to read ffi.json file {ffiJsonFile.FullName}!!");
            return 1;
        }

        var sdlEnums = new StringBuilder();
        foreach (var (enumName, enumData) in ffiData.Enums)
        {
            sdlEnums.Append($"public {enumName}\n{{\n");

            foreach (var enumValue in enumData.Values)
            {
                sdlEnums.Append($"{enumValue.Name} = {enumValue.Value},\n");
            }

            sdlEnums.Append("}\n\n");
        }

        File.WriteAllText(
            path: Path.Combine(outputDir.FullName, "SDL3.cs"),
            contents: CompileBindingsCSharp(
                enums: sdlEnums.ToString(),
                "",
                ""
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
namespace SDL3;

public static class SDL
{{
    {enums}
    {structs}
    {functions}
}}
";
    }
}
