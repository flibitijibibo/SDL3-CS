#!/usr/bin/env bash

if [[ $# -ne 2 || ! -d $1 || ! -d $2 ]]; then
    echo 'usage: `generate.sh <executables subdir> <path to sdl repo root>`'
    exit 0
fi

EXES_DIR=$1
C2FFI="${EXES_DIR}/c2ffi"
C2CS="${EXES_DIR}/c2cs"

if [[ ! -e $C2FFI ]]; then
    C2FFI="${C2FFI}.exe"

    if [[ ! -e $C2FFI ]]; then
        echo 'c2ffi executable missing!'
        exit 1
    fi
fi

if [[ ! -e $C2CS ]]; then
    C2CS="${C2CS}.exe"

    if [[ ! -e $C2CS ]]; then
        echo 'c2cs executable missing!'
        exit 1
    fi
fi


SDL_DIR=$2
SCR_DIR="$(dirname $0)"
ROOT_DIR="${SCR_DIR}/.."
TMP_DIR="$(mktemp -d)"
C2FFI_CONFIG="${TMP_DIR}/config_extract.json"

# rewrite the config-extract template

# run c2ffi extract
# run c2ffi merge
# run c2cs --config
# clean up
