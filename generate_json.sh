#!/bin/sh
if [ ! -z $1 ]; then
    c2ffi_executable=$1
elif ! command -v c2ffi 2>&1 >/dev/null; then
    echo "c2ffi could not be found in PATH"
    exit 1
else
    c2ffi_executable="c2ffi"
fi

cat > tmp.c <<- EOM
#include <SDL3/SDL.h>
#include <SDL3/SDL_main.h>
EOM

$c2ffi_executable tmp.c -o GenerateBindings/assets/ffi.json
rm tmp.c
