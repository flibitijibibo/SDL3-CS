to generate c# bindings for SDL3:
- if they aren't already in the `executables/` dir:
    - install https://github.com/bottlenoselabs/c2cs/blob/main/docs/README.md
    - install https://github.com/bottlenoselabs/c2ffi?tab=readme-ov-file#getting-started
    - put em in a dir under `executables`
- run `generate_bindings/generate.sh <executables subdir> <path to SDL project root>`

