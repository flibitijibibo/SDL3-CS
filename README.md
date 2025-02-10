This is SDL3-CS, C# bindings for SDL3.

License
-------
SDL3 and SDL3-CS are released under the zlib license. See LICENSE for details.

About SDL3
----------
For more information about SDL3, visit the SDL wiki:

https://wiki.libsdl.org/SDL3/FrontPage

About the C# bindings
---------------------
The bindings are auto-generated from the GenerateBindings subproject.
The generator depends on JSON output from the c2ffi project: https://github.com/rpav/c2ffi

SDL3.Legacy.cs contains legacy bindings that will work with any .NET project that supports at least C# language version 4.

SDL3.Core.cs contains CoreCLR-specific bindings that will only work with .NET 8+. These bindings may provide improved performance if you are able to use them.

SDL3-CS is pure function-by-function interop with the C headers.
You are encouraged to write your own wrapper objects if you care about "appropriate" C# style.

The SDL headers themselves do not provide enough information to generate complete C# bindings.
If you update the bindings, search "WARN_" in generated files for unhandled definitions or those that require manual intervention.

## Bindings Generation Instructions
### Generate ffi.json
You need to have a bash compatible terminal (like gitbash), the SDL3 repository
and [c2ffi](https://github.com/rpav/c2ffi).

Also, the SDL3 includes needs to be available trough the environment variable
`INCLUDE` so `c2ffi` can find them.

You might need to compile c2ffi, so first keep note of the branch that you 
need, it needs to match a installed LLVM version, the compilation will fail
with linker errors if you use a LLVM version different than the branch you are
using.

All commands assume your working dir is the root of this repository.
The SDL3 repo and c2ffi binary can be anywhere.

After installing or compiling c2ffi, you can use the command

```
./generate_json.sh /path/to/c2ffi
```

Path to c2ffi is optional if it is available in your `PATH`

### Generate the Bindings
Once the json file is updated, you may compile `GenerateBindings` with
```
dotnet build GenerateBindings.sln
```
Once you compile `GenerateBindings` you can run it with the belwo command to update SDL3-CS.Core.
```
GenerateBindings/bin/Debug/net8.0/GenerateBindings /path/to/the/root/of/sdl3/repo/ --core
```

If you want to update SDL3-CS.Legacy, just remove the `--core` argument.

```
GenerateBindings/bin/Debug/net8.0/GenerateBindings /path/to/the/root/of/sdl3/repo/
```
