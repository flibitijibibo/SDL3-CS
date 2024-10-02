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

SDL3-CS is a pure port of the C headers.
You are encouraged to write your own wrapper objects if you care about "appropriate" C# style.

The SDL headers themselves do not provide enough information to generate complete C# bindings.
If you update the bindings, search "WARN_" in generated files for unhandled definitions or those that require manual intervention.
