to generate c# bindings for SDL3, run GenerateBindings
- currently copies the ffi.json from GenerateBindings/assets instead of generating its own
- search "WARN_" in generated SDL3.cs for unhandled definitions or those that require manual intervention
- set "Ref" or "Array" for pointer parameter intentions in the `ParametersIntents` dictionary
