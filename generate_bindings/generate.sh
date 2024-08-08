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
GENERATE_DIR="${ROOT_DIR}/generated"

TMP_DIR="$(mktemp -d)"
echo "building in temporary directory ${TMP_DIR}..."

C2FFI_CONFIG="${TMP_DIR}/config_extract.json"
C2FFI_OUTPUT_DIR="${TMP_DIR}/output"
C2FFI_OUTPUT_FILEPATH="${C2FFI_OUTPUT_DIR}/cross-platform-ffi.json"
mkdir C2FFI_OUTPUT_DIR

sed -e "s@TEMPLATE_SDL_PATH@${SDL_DIR}@g" -e "s@TEMPLATE_OUTPUT_DIR@${C2FFI_OUTPUT_DIR}@g" "${SCR_DIR}/config_extract.json.template" > $C2FFI_CONFIG

$C2FFI extract --config $C2FFI_CONFIG
$C2FFI merge --inputDirectoryPath $C2FFI_OUTPUT_DIR --outputFilePath $C2FFI_OUTPUT_FILEPATH

# run c2cs --config
C2CS_CONFIG="${TMP_DIR}/config_generate_cs.json"
sed -e "s@TEMPLATE_C2FFI_OUTPUT@${C2FFI_OUTPUT_FILEPATH}@g" -e "s@TEMPLATE_GENERATE_DIR@${GENERATE_DIR}@g" "${SCR_DIR}/config_generate_cs.json.template" > $C2CS_CONFIG

$C2CS generate --config $C2CS_CONFIG

cat $C2CS_CONFIG

# clean up
