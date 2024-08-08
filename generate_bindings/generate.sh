#!/usr/bin/env bash

if [[ $# -ne 1 || ! -d $1 ]]; then
    echo 'usage: `generate.sh <path to sdl repo root>`'
    exit 0
fi

SDL_DIR=$1

pushd "$(dirname $0)/.." 1> /dev/null
ROOT_DIR=`pwd`
popd 1> /dev/null

SCRIPT_DIR="${ROOT_DIR}/generate_bindings"

SYSTEM_OS=`uname`
if [[ $SYSTEM_OS = "Linux" ]]; then
    C2FFI="${SCRIPT_DIR}/executables/linux-x64/c2ffi"
    C2CS="${SCRIPT_DIR}/executables/linux-x64/c2cs"
elif [[ $SYSTEM_OS = "Darwin" ]]; then
    C2FFI="${SCRIPT_DIR}/executables/osx-arm64/c2ffi"
    C2CS="${SCRIPT_DIR}/executables/osx-arm64/c2cs"
else # assume it's cygwin or something
    C2FFI="${SCRIPT_DIR}/executables/win-x64/c2ffi.exe"
    C2CS="${SCRIPT_DIR}/executables/win-x64/c2cs.exe"
fi

GENERATE_DIR="${ROOT_DIR}/src/generated"

TMP_DIR="$(mktemp -d)"
echo "building in temporary directory ${TMP_DIR}..."

C2FFI_CONFIG="${TMP_DIR}/config_extract.json"
C2FFI_OUTPUT_DIR="${TMP_DIR}/output"
C2FFI_OUTPUT_FILEPATH="${C2FFI_OUTPUT_DIR}/cross-platform-ffi.json"
mkdir $C2FFI_OUTPUT_DIR

sed -e "s@TEMPLATE_SDL_PATH@${SDL_DIR}@g" -e "s@TEMPLATE_OUTPUT_DIR@${C2FFI_OUTPUT_DIR}@g" "${SCRIPT_DIR}/config_extract.json.template" > $C2FFI_CONFIG

$C2FFI extract --config $C2FFI_CONFIG
$C2FFI merge --inputDirectoryPath $C2FFI_OUTPUT_DIR --outputFilePath $C2FFI_OUTPUT_FILEPATH

# run c2cs --config
C2CS_CONFIG="${TMP_DIR}/config_generate_cs.json"
sed -e "s@TEMPLATE_C2FFI_OUTPUT@${C2FFI_OUTPUT_FILEPATH}@g" -e "s@TEMPLATE_GENERATE_DIR@${GENERATE_DIR}@g" "${SCRIPT_DIR}/config_generate_cs.json.template" > $C2CS_CONFIG

$C2CS generate --config $C2CS_CONFIG

cat $C2CS_CONFIG

# clean up
