// See https://aka.ms/new-console-template for more information

// in a .targets file, copy c2ffi and the template to build dir
// grab sdl dir and target dir from provided arguments

// build a struct of exes at the start of the program. add a path searcher

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

var envPath = Environment.GetEnvironmentVariable("PATH");
if (envPath != null)
    foreach (var envPathDir in envPath.Split(Path.PathSeparator))
    {
        Console.WriteLine(envPathDir);
    }
