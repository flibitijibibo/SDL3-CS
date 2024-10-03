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

SDL3.Framework.cs contains legacy bindings that will work with any .NET project that supports at least C# language version 4.

SDL3.Core.cs contains CoreCLR-specific bindings that will only work with .NET 7+. These bindings may provide improved performance if you are able to use them.

SDL3-CS is pure function-by-function interop with the C headers.
You are encouraged to write your own wrapper objects if you care about "appropriate" C# style.

The SDL headers themselves do not provide enough information to generate complete C# bindings.
If you update the bindings, search "WARN_" in generated files for unhandled definitions or those that require manual intervention.
