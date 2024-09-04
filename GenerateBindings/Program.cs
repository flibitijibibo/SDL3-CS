// See https://aka.ms/new-console-template for more information

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

using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GenerateBindings;

internal static class Program
{
    private enum TypeContext
    {
        StructField,
        Return,
        Parameter,
    }

    private enum PointerParameterIntent
    {
        Unknown,
        Ref,
        Out,
        Array,
    }

    private struct DelegateDefinition
    {
        public string ReturnType { get; set; }
        public (string, string)[] Parameters { get; set; }
    }

    private static readonly Dictionary<(string, string), PointerParameterIntent> PointerParametersIntents = new()
    {
        { ("SDL_getenv", "name"), PointerParameterIntent.Unknown },
        { ("SDL_setenv", "name"), PointerParameterIntent.Unknown },
        { ("SDL_setenv", "value"), PointerParameterIntent.Unknown },
        { ("SDL_unsetenv", "name"), PointerParameterIntent.Unknown },
        { ("SDL_wcslen", "wstr"), PointerParameterIntent.Unknown },
        { ("SDL_wcsnlen", "wstr"), PointerParameterIntent.Unknown },
        { ("SDL_wcslcpy", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_wcslcpy", "src"), PointerParameterIntent.Unknown },
        { ("SDL_wcslcat", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_wcslcat", "src"), PointerParameterIntent.Unknown },
        { ("SDL_wcsdup", "wstr"), PointerParameterIntent.Unknown },
        { ("SDL_wcsstr", "haystack"), PointerParameterIntent.Unknown },
        { ("SDL_wcsstr", "needle"), PointerParameterIntent.Unknown },
        { ("SDL_wcsnstr", "haystack"), PointerParameterIntent.Unknown },
        { ("SDL_wcsnstr", "needle"), PointerParameterIntent.Unknown },
        { ("SDL_wcscmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_wcscmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_wcsncmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_wcsncmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_wcscasecmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_wcscasecmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_wcsncasecmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_wcsncasecmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_wcstol", "str"), PointerParameterIntent.Unknown },
        { ("SDL_wcstol", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strlen", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strnlen", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strlcpy", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_strlcpy", "src"), PointerParameterIntent.Unknown },
        { ("SDL_utf8strlcpy", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_utf8strlcpy", "src"), PointerParameterIntent.Unknown },
        { ("SDL_strlcat", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_strlcat", "src"), PointerParameterIntent.Unknown },
        { ("SDL_strdup", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strndup", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strrev", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strupr", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strlwr", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strchr", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strrchr", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strstr", "haystack"), PointerParameterIntent.Unknown },
        { ("SDL_strstr", "needle"), PointerParameterIntent.Unknown },
        { ("SDL_strnstr", "haystack"), PointerParameterIntent.Unknown },
        { ("SDL_strnstr", "needle"), PointerParameterIntent.Unknown },
        { ("SDL_strcasestr", "haystack"), PointerParameterIntent.Unknown },
        { ("SDL_strcasestr", "needle"), PointerParameterIntent.Unknown },
        { ("SDL_strtok_r", "s1"), PointerParameterIntent.Unknown },
        { ("SDL_strtok_r", "s2"), PointerParameterIntent.Unknown },
        { ("SDL_strtok_r", "saveptr"), PointerParameterIntent.Unknown },
        { ("SDL_utf8strlen", "str"), PointerParameterIntent.Unknown },
        { ("SDL_utf8strnlen", "str"), PointerParameterIntent.Unknown },
        { ("SDL_itoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_uitoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_ltoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_ultoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_lltoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_ulltoa", "str"), PointerParameterIntent.Unknown },
        { ("SDL_atoi", "str"), PointerParameterIntent.Unknown },
        { ("SDL_atof", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtol", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtol", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strtoul", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtoul", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strtoll", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtoll", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strtoull", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtoull", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strtod", "str"), PointerParameterIntent.Unknown },
        { ("SDL_strtod", "endp"), PointerParameterIntent.Unknown },
        { ("SDL_strcmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_strcmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_strncmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_strncmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_strcasecmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_strcasecmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_strncasecmp", "str1"), PointerParameterIntent.Unknown },
        { ("SDL_strncasecmp", "str2"), PointerParameterIntent.Unknown },
        { ("SDL_StepUTF8", "pstr"), PointerParameterIntent.Unknown },
        { ("SDL_StepUTF8", "pslen"), PointerParameterIntent.Unknown },
        { ("SDL_UCS4ToUTF8", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_sscanf", "text"), PointerParameterIntent.Unknown },
        { ("SDL_sscanf", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_snprintf", "text"), PointerParameterIntent.Unknown },
        { ("SDL_snprintf", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_swprintf", "text"), PointerParameterIntent.Unknown },
        { ("SDL_swprintf", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_asprintf", "strp"), PointerParameterIntent.Unknown },
        { ("SDL_asprintf", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_rand_r", "state"), PointerParameterIntent.Unknown },
        { ("SDL_randf_r", "state"), PointerParameterIntent.Unknown },
        { ("SDL_rand_bits_r", "state"), PointerParameterIntent.Unknown },
        { ("SDL_modf", "y"), PointerParameterIntent.Unknown },
        { ("SDL_modff", "y"), PointerParameterIntent.Unknown },
        { ("SDL_iconv_open", "tocode"), PointerParameterIntent.Unknown },
        { ("SDL_iconv_open", "fromcode"), PointerParameterIntent.Unknown },
        { ("SDL_iconv", "inbuf"), PointerParameterIntent.Unknown },
        { ("SDL_iconv", "inbytesleft"), PointerParameterIntent.Unknown },
        { ("SDL_iconv", "outbuf"), PointerParameterIntent.Unknown },
        { ("SDL_iconv", "outbytesleft"), PointerParameterIntent.Unknown },
        { ("SDL_iconv_string", "tocode"), PointerParameterIntent.Unknown },
        { ("SDL_iconv_string", "fromcode"), PointerParameterIntent.Unknown },
        { ("SDL_iconv_string", "inbuf"), PointerParameterIntent.Unknown },
        { ("SDL_size_mul_check_overflow", "ret"), PointerParameterIntent.Unknown },
        { ("SDL_size_mul_check_overflow_builtin", "ret"), PointerParameterIntent.Unknown },
        { ("SDL_size_add_check_overflow", "ret"), PointerParameterIntent.Unknown },
        { ("SDL_size_add_check_overflow_builtin", "ret"), PointerParameterIntent.Unknown },
        { ("SDL_ReportAssertion", "func"), PointerParameterIntent.Unknown },
        { ("SDL_ReportAssertion", "file"), PointerParameterIntent.Unknown },
        { ("SDL_GetAssertionHandler", "puserdata"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicCompareAndSwapPointer", "a"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicSetPointer", "a"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicGetPointer", "a"), PointerParameterIntent.Unknown },
        { ("SDL_SetError", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_SetPointerPropertyWithCleanup", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetPointerProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetStringProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetStringProperty", "value"), PointerParameterIntent.Unknown },
        { ("SDL_SetNumberProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetFloatProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetBooleanProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_HasProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetPropertyType", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetPointerProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetStringProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetStringProperty", "default_value"), PointerParameterIntent.Unknown },
        { ("SDL_GetNumberProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetFloatProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetBooleanProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_ClearProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_IOFromFile", "file"), PointerParameterIntent.Unknown },
        { ("SDL_IOFromFile", "mode"), PointerParameterIntent.Unknown },
        { ("SDL_IOprintf", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LoadFile_IO", "datasize"), PointerParameterIntent.Unknown },
        { ("SDL_LoadFile", "file"), PointerParameterIntent.Unknown },
        { ("SDL_LoadFile", "datasize"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU8", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS8", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU16LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS16LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU16BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS16BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU32LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS32LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU32BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS32BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU64LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS64LE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadU64BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ReadS64BE", "value"), PointerParameterIntent.Unknown },
        { ("SDL_CreateThreadRuntime", "name"), PointerParameterIntent.Unknown },
        { ("SDL_WaitThread", "status"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioPlaybackDevices", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioRecordingDevices", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioDeviceFormat", "sample_frames"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioDeviceChannelMap", "count"), PointerParameterIntent.Unknown },
        { ("SDL_BindAudioStreams", "streams"), PointerParameterIntent.Unknown },
        { ("SDL_UnbindAudioStreams", "streams"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioStreamInputChannelMap", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioStreamOutputChannelMap", "count"), PointerParameterIntent.Unknown },
        { ("SDL_SetAudioStreamInputChannelMap", "chmap"), PointerParameterIntent.Unknown },
        { ("SDL_SetAudioStreamOutputChannelMap", "chmap"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV_IO", "audio_buf"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV_IO", "audio_len"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV", "path"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV", "audio_buf"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV", "audio_len"), PointerParameterIntent.Unknown },
        { ("SDL_MixAudio", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_MixAudio", "src"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertAudioSamples", "src_data"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertAudioSamples", "dst_data"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertAudioSamples", "dst_len"), PointerParameterIntent.Unknown },
        { ("SDL_GetMasksForPixelFormat", "bpp"), PointerParameterIntent.Unknown },
        { ("SDL_GetMasksForPixelFormat", "Rmask"), PointerParameterIntent.Unknown },
        { ("SDL_GetMasksForPixelFormat", "Gmask"), PointerParameterIntent.Unknown },
        { ("SDL_GetMasksForPixelFormat", "Bmask"), PointerParameterIntent.Unknown },
        { ("SDL_GetMasksForPixelFormat", "Amask"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGB", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGB", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGB", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "a"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersection", "X1"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersection", "Y1"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersection", "X2"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersection", "Y2"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersectionFloat", "X1"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersectionFloat", "Y1"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersectionFloat", "X2"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersectionFloat", "Y2"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceImages", "count"), PointerParameterIntent.Unknown },
        { ("SDL_LoadBMP", "file"), PointerParameterIntent.Unknown },
        { ("SDL_SaveBMP", "file"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorKey", "key"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorMod", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorMod", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorMod", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceAlphaMod", "alpha"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixel", "r"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixel", "g"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixel", "b"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixel", "a"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixelFloat", "r"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixelFloat", "g"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixelFloat", "b"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixelFloat", "a"), PointerParameterIntent.Unknown },
        { ("SDL_GetDisplays", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetFullscreenDisplayModes", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowICCProfile", "size"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindows", "count"), PointerParameterIntent.Unknown },
        { ("SDL_CreateWindow", "title"), PointerParameterIntent.Unknown },
        { ("SDL_SetWindowTitle", "title"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowPosition", "x"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowPosition", "y"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowAspectRatio", "min_aspect"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowAspectRatio", "max_aspect"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowBordersSize", "top"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowBordersSize", "left"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowBordersSize", "bottom"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowBordersSize", "right"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSizeInPixels", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSizeInPixels", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowMinimumSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowMinimumSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowMaximumSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowMaximumSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSurfaceVSync", "vsync"), PointerParameterIntent.Unknown },
        { ("SDL_GL_LoadLibrary", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GL_GetProcAddress", "proc"), PointerParameterIntent.Unknown },
        { ("SDL_EGL_GetProcAddress", "proc"), PointerParameterIntent.Unknown },
        { ("SDL_GL_ExtensionSupported", "extension"), PointerParameterIntent.Unknown },
        { ("SDL_GL_GetAttribute", "value"), PointerParameterIntent.Unknown },
        { ("SDL_GL_GetSwapInterval", "interval"), PointerParameterIntent.Unknown },
        { ("SDL_GetCameras", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetCameraSupportedFormats", "count"), PointerParameterIntent.Unknown },
        { ("SDL_AcquireCameraFrame", "timestampNS"), PointerParameterIntent.Unknown },
        { ("SDL_SetClipboardText", "text"), PointerParameterIntent.Unknown },
        { ("SDL_SetPrimarySelectionText", "text"), PointerParameterIntent.Unknown },
        { ("SDL_SetClipboardData", "mime_types"), PointerParameterIntent.Unknown },
        { ("SDL_GetClipboardData", "mime_type"), PointerParameterIntent.Unknown },
        { ("SDL_GetClipboardData", "size"), PointerParameterIntent.Unknown },
        { ("SDL_HasClipboardData", "mime_type"), PointerParameterIntent.Unknown },
        { ("SDL_ShowOpenFileDialog", "default_location"), PointerParameterIntent.Unknown },
        { ("SDL_ShowSaveFileDialog", "default_location"), PointerParameterIntent.Unknown },
        { ("SDL_ShowOpenFolderDialog", "default_location"), PointerParameterIntent.Unknown },
        { ("SDL_GUIDToString", "pszGUID"), PointerParameterIntent.Unknown },
        { ("SDL_StringToGUID", "pchGUID"), PointerParameterIntent.Unknown },
        { ("SDL_GetPowerInfo", "seconds"), PointerParameterIntent.Unknown },
        { ("SDL_GetPowerInfo", "percent"), PointerParameterIntent.Unknown },
        { ("SDL_GetSensors", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetSensorData", "data"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoysticks", "count"), PointerParameterIntent.Unknown },
        { ("SDL_SendJoystickVirtualSensorData", "data"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickGUIDInfo", "vendor"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickGUIDInfo", "product"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickGUIDInfo", "version"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickGUIDInfo", "crc16"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickAxisInitialState", "state"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickBall", "dx"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickBall", "dy"), PointerParameterIntent.Unknown },
        { ("SDL_GetJoystickPowerInfo", "percent"), PointerParameterIntent.Unknown },
        { ("SDL_AddGamepadMapping", "mapping"), PointerParameterIntent.Unknown },
        { ("SDL_AddGamepadMappingsFromFile", "file"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadMappings", "count"), PointerParameterIntent.Unknown },
        { ("SDL_SetGamepadMapping", "mapping"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepads", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadPowerInfo", "percent"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadBindings", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadTypeFromString", "str"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadAxisFromString", "str"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadButtonFromString", "str"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadTouchpadFinger", "state"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadTouchpadFinger", "x"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadTouchpadFinger", "y"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadTouchpadFinger", "pressure"), PointerParameterIntent.Unknown },
        { ("SDL_GetGamepadSensorData", "data"), PointerParameterIntent.Unknown },
        { ("SDL_GetKeyboards", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetKeyboardState", "numkeys"), PointerParameterIntent.Unknown },
        { ("SDL_SetScancodeName", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetScancodeFromName", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetKeyFromName", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextInputArea", "cursor"), PointerParameterIntent.Unknown },
        { ("SDL_GetMice", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetMouseState", "x"), PointerParameterIntent.Unknown },
        { ("SDL_GetMouseState", "y"), PointerParameterIntent.Unknown },
        { ("SDL_GetGlobalMouseState", "x"), PointerParameterIntent.Unknown },
        { ("SDL_GetGlobalMouseState", "y"), PointerParameterIntent.Unknown },
        { ("SDL_GetRelativeMouseState", "x"), PointerParameterIntent.Unknown },
        { ("SDL_GetRelativeMouseState", "y"), PointerParameterIntent.Unknown },
        { ("SDL_CreateCursor", "data"), PointerParameterIntent.Unknown },
        { ("SDL_CreateCursor", "mask"), PointerParameterIntent.Unknown },
        { ("SDL_GetTouchDevices", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetTouchFingers", "count"), PointerParameterIntent.Unknown },
        { ("SDL_GetEventFilter", "userdata"), PointerParameterIntent.Unknown },
        { ("SDL_GetPrefPath", "org"), PointerParameterIntent.Unknown },
        { ("SDL_GetPrefPath", "app"), PointerParameterIntent.Unknown },
        { ("SDL_CreateDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_EnumerateDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_RemovePath", "path"), PointerParameterIntent.Unknown },
        { ("SDL_RenamePath", "oldpath"), PointerParameterIntent.Unknown },
        { ("SDL_RenamePath", "newpath"), PointerParameterIntent.Unknown },
        { ("SDL_CopyFile", "oldpath"), PointerParameterIntent.Unknown },
        { ("SDL_CopyFile", "newpath"), PointerParameterIntent.Unknown },
        { ("SDL_GetPathInfo", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GlobDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GlobDirectory", "pattern"), PointerParameterIntent.Unknown },
        { ("SDL_GlobDirectory", "count"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUDevice", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetGPUBufferName", "text"), PointerParameterIntent.Unknown },
        { ("SDL_SetGPUTextureName", "text"), PointerParameterIntent.Unknown },
        { ("SDL_InsertGPUDebugLabel", "text"), PointerParameterIntent.Unknown },
        { ("SDL_PushGPUDebugGroup", "name"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUVertexStorageTextures", "storageTextures"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUVertexStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUFragmentStorageTextures", "storageTextures"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUFragmentStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUComputeStorageTextures", "storageTextures"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUComputeStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown },
        { ("SDL_AcquireGPUSwapchainTexture", "pWidth"), PointerParameterIntent.Unknown },
        { ("SDL_AcquireGPUSwapchainTexture", "pHeight"), PointerParameterIntent.Unknown },
        { ("SDL_WaitForGPUFences", "pFences"), PointerParameterIntent.Unknown },
        { ("SDL_GetHaptics", "count"), PointerParameterIntent.Unknown },
        { ("SDL_hid_open", "serial_number"), PointerParameterIntent.Unknown },
        { ("SDL_hid_open_path", "path"), PointerParameterIntent.Unknown },
        { ("SDL_hid_write", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_read_timeout", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_read", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_send_feature_report", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_feature_report", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_input_report", "data"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_manufacturer_string", "string"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_product_string", "string"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_serial_number_string", "string"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_indexed_string", "string"), PointerParameterIntent.Unknown },
        { ("SDL_hid_get_report_descriptor", "buf"), PointerParameterIntent.Unknown },
        { ("SDL_SetHintWithPriority", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetHintWithPriority", "value"), PointerParameterIntent.Unknown },
        { ("SDL_SetHint", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetHint", "value"), PointerParameterIntent.Unknown },
        { ("SDL_ResetHint", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetHint", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetHintBoolean", "name"), PointerParameterIntent.Unknown },
        { ("SDL_AddHintCallback", "name"), PointerParameterIntent.Unknown },
        { ("SDL_RemoveHintCallback", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetAppMetadata", "appname"), PointerParameterIntent.Unknown },
        { ("SDL_SetAppMetadata", "appversion"), PointerParameterIntent.Unknown },
        { ("SDL_SetAppMetadata", "appidentifier"), PointerParameterIntent.Unknown },
        { ("SDL_SetAppMetadataProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_SetAppMetadataProperty", "value"), PointerParameterIntent.Unknown },
        { ("SDL_GetAppMetadataProperty", "name"), PointerParameterIntent.Unknown },
        { ("SDL_LoadObject", "sofile"), PointerParameterIntent.Unknown },
        { ("SDL_LoadFunction", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetPreferredLocales", "count"), PointerParameterIntent.Unknown },
        { ("SDL_SetLogPriorityPrefix", "prefix"), PointerParameterIntent.Unknown },
        { ("SDL_Log", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogVerbose", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogDebug", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogInfo", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogWarn", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogError", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogCritical", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_LogMessage", "fmt"), PointerParameterIntent.Unknown },
        { ("SDL_GetLogOutputFunction", "userdata"), PointerParameterIntent.Unknown },
        { ("SDL_ShowMessageBox", "buttonid"), PointerParameterIntent.Unknown },
        { ("SDL_ShowSimpleMessageBox", "title"), PointerParameterIntent.Unknown },
        { ("SDL_ShowSimpleMessageBox", "message"), PointerParameterIntent.Unknown },
        { ("SDL_OpenURL", "url"), PointerParameterIntent.Unknown },
        { ("SDL_CreateWindowAndRenderer", "title"), PointerParameterIntent.Unknown },
        { ("SDL_CreateWindowAndRenderer", "window"), PointerParameterIntent.Unknown },
        { ("SDL_CreateWindowAndRenderer", "renderer"), PointerParameterIntent.Unknown },
        { ("SDL_CreateRenderer", "name"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderOutputSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderOutputSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetCurrentRenderOutputSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetCurrentRenderOutputSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureSize", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureSize", "h"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorMod", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorMod", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorMod", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorModFloat", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorModFloat", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureColorModFloat", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureAlphaMod", "alpha"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureAlphaModFloat", "alpha"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateYUVTexture", "Yplane"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateYUVTexture", "Uplane"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateYUVTexture", "Vplane"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateNVTexture", "Yplane"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateNVTexture", "UVplane"), PointerParameterIntent.Unknown },
        { ("SDL_LockTexture", "pixels"), PointerParameterIntent.Unknown },
        { ("SDL_LockTexture", "pitch"), PointerParameterIntent.Unknown },
        { ("SDL_LockTextureToSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderLogicalPresentation", "w"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderLogicalPresentation", "h"), PointerParameterIntent.Unknown },
        { ("SDL_RenderCoordinatesFromWindow", "x"), PointerParameterIntent.Unknown },
        { ("SDL_RenderCoordinatesFromWindow", "y"), PointerParameterIntent.Unknown },
        { ("SDL_RenderCoordinatesToWindow", "window_x"), PointerParameterIntent.Unknown },
        { ("SDL_RenderCoordinatesToWindow", "window_y"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderScale", "scaleX"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderScale", "scaleY"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColor", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColor", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColor", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColor", "a"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColorFloat", "r"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColorFloat", "g"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColorFloat", "b"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderDrawColorFloat", "a"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderColorScale", "scale"), PointerParameterIntent.Unknown },
        { ("SDL_RenderGeometry", "indices"), PointerParameterIntent.Unknown },
        { ("SDL_RenderGeometryRaw", "xy"), PointerParameterIntent.Unknown },
        { ("SDL_RenderGeometryRaw", "uv"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderVSync", "vsync"), PointerParameterIntent.Unknown },
        { ("SDL_OpenTitleStorage", "override"), PointerParameterIntent.Unknown },
        { ("SDL_OpenUserStorage", "org"), PointerParameterIntent.Unknown },
        { ("SDL_OpenUserStorage", "app"), PointerParameterIntent.Unknown },
        { ("SDL_OpenFileStorage", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GetStorageFileSize", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GetStorageFileSize", "length"), PointerParameterIntent.Unknown },
        { ("SDL_ReadStorageFile", "path"), PointerParameterIntent.Unknown },
        { ("SDL_WriteStorageFile", "path"), PointerParameterIntent.Unknown },
        { ("SDL_CreateStorageDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_EnumerateStorageDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_RemoveStoragePath", "path"), PointerParameterIntent.Unknown },
        { ("SDL_RenameStoragePath", "oldpath"), PointerParameterIntent.Unknown },
        { ("SDL_RenameStoragePath", "newpath"), PointerParameterIntent.Unknown },
        { ("SDL_CopyStorageFile", "oldpath"), PointerParameterIntent.Unknown },
        { ("SDL_CopyStorageFile", "newpath"), PointerParameterIntent.Unknown },
        { ("SDL_GetStoragePathInfo", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GlobStorageDirectory", "path"), PointerParameterIntent.Unknown },
        { ("SDL_GlobStorageDirectory", "pattern"), PointerParameterIntent.Unknown },
        { ("SDL_GlobStorageDirectory", "count"), PointerParameterIntent.Unknown },
        { ("SDL_TimeToWindows", "dwLowDateTime"), PointerParameterIntent.Unknown },
        { ("SDL_TimeToWindows", "dwHighDateTime"), PointerParameterIntent.Unknown },
        { ("SDL_ReportAssertion", "data"), PointerParameterIntent.Ref },
        { ("SDL_AtomicCompareAndSwap", "a"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicSet", "a"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicGet", "a"), PointerParameterIntent.Unknown },
        { ("SDL_AtomicAdd", "a"), PointerParameterIntent.Unknown },
        { ("SDL_OpenIO", "iface"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioDeviceFormat", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_OpenAudioDevice", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_CreateAudioStream", "src_spec"), PointerParameterIntent.Unknown },
        { ("SDL_CreateAudioStream", "dst_spec"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown },
        { ("SDL_GetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown },
        { ("SDL_SetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown },
        { ("SDL_SetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown },
        { ("SDL_OpenAudioDeviceStream", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV_IO", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_LoadWAV", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertAudioSamples", "src_spec"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertAudioSamples", "dst_spec"), PointerParameterIntent.Unknown },
        { ("SDL_SetPaletteColors", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_SetPaletteColors", "colors"), PointerParameterIntent.Unknown },
        { ("SDL_DestroyPalette", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_MapRGB", "format"), PointerParameterIntent.Unknown },
        { ("SDL_MapRGB", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_MapRGBA", "format"), PointerParameterIntent.Unknown },
        { ("SDL_MapRGBA", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGB", "format"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGB", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "format"), PointerParameterIntent.Unknown },
        { ("SDL_GetRGBA", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_RectToFRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_RectToFRect", "frect"), PointerParameterIntent.Unknown },
        { ("SDL_PointInRect", "p"), PointerParameterIntent.Unknown },
        { ("SDL_PointInRect", "r"), PointerParameterIntent.Unknown },
        { ("SDL_RectEmpty", "r"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqual", "a"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqual", "b"), PointerParameterIntent.Unknown },
        { ("SDL_HasRectIntersection", "A"), PointerParameterIntent.Unknown },
        { ("SDL_HasRectIntersection", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersection", "A"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersection", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersection", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnion", "A"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnion", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnion", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPoints", "points"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPoints", "clip"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPoints", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersection", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_PointInRectFloat", "p"), PointerParameterIntent.Unknown },
        { ("SDL_PointInRectFloat", "r"), PointerParameterIntent.Unknown },
        { ("SDL_RectEmptyFloat", "r"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqualEpsilon", "a"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqualEpsilon", "b"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqualFloat", "a"), PointerParameterIntent.Unknown },
        { ("SDL_RectsEqualFloat", "b"), PointerParameterIntent.Unknown },
        { ("SDL_HasRectIntersectionFloat", "A"), PointerParameterIntent.Unknown },
        { ("SDL_HasRectIntersectionFloat", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersectionFloat", "A"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersectionFloat", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectIntersectionFloat", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnionFloat", "A"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnionFloat", "B"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectUnionFloat", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPointsFloat", "points"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPointsFloat", "clip"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectEnclosingPointsFloat", "result"), PointerParameterIntent.Unknown },
        { ("SDL_GetRectAndLineIntersectionFloat", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_DestroySurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceProperties", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_CreateSurfacePalette", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfacePalette", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfacePalette", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfacePalette", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_AddSurfaceAlternateImage", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_AddSurfaceAlternateImage", "image"), PointerParameterIntent.Unknown },
        { ("SDL_SurfaceHasAlternateImages", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceImages", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_RemoveSurfaceAlternateImages", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_LockSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_UnlockSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SaveBMP_IO", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SaveBMP", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceRLE", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SurfaceHasRLE", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SurfaceHasColorKey", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_SetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_FlipSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_DuplicateSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ScaleSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertSurfaceAndColorspace", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ConvertSurfaceAndColorspace", "palette"), PointerParameterIntent.Unknown },
        { ("SDL_PremultiplySurfaceAlpha", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ClearSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_FillSurfaceRect", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_FillSurfaceRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_FillSurfaceRects", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_FillSurfaceRects", "rects"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUnchecked", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUnchecked", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUnchecked", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUnchecked", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceScaled", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceScaled", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceScaled", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceScaled", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUncheckedScaled", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUncheckedScaled", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUncheckedScaled", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceUncheckedScaled", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiled", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiled", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiled", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiled", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiledWithScale", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiledWithScale", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiledWithScale", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurfaceTiledWithScale", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface9Grid", "src"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface9Grid", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface9Grid", "dst"), PointerParameterIntent.Unknown },
        { ("SDL_BlitSurface9Grid", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_MapSurfaceRGB", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_MapSurfaceRGBA", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixel", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_ReadSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_WriteSurfacePixel", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_WriteSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetDisplayBounds", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetDisplayUsableBounds", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetClosestFullscreenDisplayMode", "mode"), PointerParameterIntent.Unknown },
        { ("SDL_GetDisplayForPoint", "point"), PointerParameterIntent.Unknown },
        { ("SDL_GetDisplayForRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_SetWindowFullscreenMode", "mode"), PointerParameterIntent.Unknown },
        { ("SDL_SetWindowIcon", "icon"), PointerParameterIntent.Unknown },
        { ("SDL_GetWindowSafeArea", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateWindowSurfaceRects", "rects"), PointerParameterIntent.Unknown },
        { ("SDL_SetWindowMouseRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_SetWindowShape", "shape"), PointerParameterIntent.Unknown },
        { ("SDL_OpenCamera", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_GetCameraFormat", "spec"), PointerParameterIntent.Unknown },
        { ("SDL_ReleaseCameraFrame", "frame"), PointerParameterIntent.Unknown },
        { ("SDL_ShowOpenFileDialog", "filters"), PointerParameterIntent.Unknown },
        { ("SDL_ShowSaveFileDialog", "filters"), PointerParameterIntent.Unknown },
        { ("SDL_AttachVirtualJoystick", "desc"), PointerParameterIntent.Unknown },
        { ("SDL_SetTextInputArea", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextInputArea", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_CreateColorCursor", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetPathInfo", "info"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUComputePipeline", "computePipelineCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUGraphicsPipeline", "pipelineCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUSampler", "samplerCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUShader", "shaderCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUTexture", "textureCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUBuffer", "bufferCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_CreateGPUTransferBuffer", "transferBufferCreateInfo"), PointerParameterIntent.Unknown },
        { ("SDL_BeginGPURenderPass", "colorAttachmentInfos"), PointerParameterIntent.Array },
        { ("SDL_BeginGPURenderPass", "depthStencilAttachmentInfo"), PointerParameterIntent.Ref },
        { ("SDL_SetGPUViewport", "viewport"), PointerParameterIntent.Unknown },
        { ("SDL_SetGPUScissor", "scissor"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUVertexBuffers", "pBindings"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUIndexBuffer", "pBinding"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUVertexSamplers", "textureSamplerBindings"), PointerParameterIntent.Unknown },
        { ("SDL_BindGPUFragmentSamplers", "textureSamplerBindings"), PointerParameterIntent.Unknown },
        { ("SDL_BeginGPUComputePass", "storageTextureBindings"), PointerParameterIntent.Unknown },
        { ("SDL_BeginGPUComputePass", "storageBufferBindings"), PointerParameterIntent.Unknown },
        { ("SDL_UploadToGPUTexture", "source"), PointerParameterIntent.Unknown },
        { ("SDL_UploadToGPUTexture", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_UploadToGPUBuffer", "source"), PointerParameterIntent.Unknown },
        { ("SDL_UploadToGPUBuffer", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_CopyGPUTextureToTexture", "source"), PointerParameterIntent.Unknown },
        { ("SDL_CopyGPUTextureToTexture", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_CopyGPUBufferToBuffer", "source"), PointerParameterIntent.Unknown },
        { ("SDL_CopyGPUBufferToBuffer", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_DownloadFromGPUTexture", "source"), PointerParameterIntent.Unknown },
        { ("SDL_DownloadFromGPUTexture", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_DownloadFromGPUBuffer", "source"), PointerParameterIntent.Unknown },
        { ("SDL_DownloadFromGPUBuffer", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_BlitGPUTexture", "source"), PointerParameterIntent.Unknown },
        { ("SDL_BlitGPUTexture", "destination"), PointerParameterIntent.Unknown },
        { ("SDL_hid_free_enumeration", "devs"), PointerParameterIntent.Unknown },
        { ("SDL_ShowMessageBox", "messageboxdata"), PointerParameterIntent.Unknown },
        { ("SDL_CreateSoftwareRenderer", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_CreateTextureFromSurface", "surface"), PointerParameterIntent.Unknown },
        { ("SDL_GetTextureScaleMode", "scaleMode"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateTexture", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateYUVTexture", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_UpdateNVTexture", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_LockTexture", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_LockTextureToSurface", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderLogicalPresentation", "mode"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderLogicalPresentation", "scale_mode"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderLogicalPresentationRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_SetRenderViewport", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderViewport", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderSafeArea", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_SetRenderClipRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_GetRenderClipRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderPoints", "points"), PointerParameterIntent.Unknown },
        { ("SDL_RenderLines", "points"), PointerParameterIntent.Unknown },
        { ("SDL_RenderRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderRects", "rects"), PointerParameterIntent.Unknown },
        { ("SDL_RenderFillRect", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderFillRects", "rects"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTexture", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTexture", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTextureRotated", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTextureRotated", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTextureRotated", "center"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTextureTiled", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTextureTiled", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTexture9Grid", "srcrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderTexture9Grid", "dstrect"), PointerParameterIntent.Unknown },
        { ("SDL_RenderGeometry", "vertices"), PointerParameterIntent.Unknown },
        { ("SDL_RenderGeometryRaw", "color"), PointerParameterIntent.Unknown },
        { ("SDL_RenderReadPixels", "rect"), PointerParameterIntent.Unknown },
        { ("SDL_OpenStorage", "iface"), PointerParameterIntent.Unknown },
        { ("SDL_GetStoragePathInfo", "info"), PointerParameterIntent.Unknown },
        { ("SDL_GetDateTimeLocalePreferences", "dateFormat"), PointerParameterIntent.Unknown },
        { ("SDL_GetDateTimeLocalePreferences", "timeFormat"), PointerParameterIntent.Unknown },
        { ("SDL_TimeToDateTime", "dt"), PointerParameterIntent.Unknown },
        { ("SDL_DateTimeToTime", "dt"), PointerParameterIntent.Unknown },
    };

    private static Dictionary<string, DelegateDefinition> DelegateDefinitions = new()
    {
        { "SDL_EnumeratePropertiesCallback", new DelegateDefinition { ReturnType = "void", Parameters = [] } },
    };

    private static readonly List<string> DefinedTypes = new();
    private static readonly Dictionary<string, RawFFIEntry> TypedefMap = new();

    private static readonly string[] DeniedTypes =
    [
        "alloca",
    ];

    private static int Main(string[] args)
    {
        // PARSE INPUT

        if (args.Length > 0)
        {
            Console.WriteLine("usage: SDL3_CS_SDL_REPO_ROOT=<sdl-repo-root-dir> GenerateBindings");
            return 1;
        }

        var sdlDir = new DirectoryInfo(Environment.GetEnvironmentVariable("SDL3_CS_SDL_REPO_ROOT") ?? "MISSING_ENV_VAR");
        var sdlBindingsDir = new FileInfo(Path.Combine(AppContext.BaseDirectory, "../../../../SDL3/"));
        var outputDir = new FileInfo(Path.Combine(sdlBindingsDir.FullName, "generated"));
        var sdlBindingsProjectFile = new FileInfo(Path.Combine(sdlBindingsDir.FullName, "SDL3.csproj"));
        var c2ffiConfigTemplateFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "config_extract.json.template"));
        var c2ffiConfigFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "config_extract.json"));
        var ffiJsonFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "ffi.json"));
        var ffiJsonIntermediateDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "intermediate_ffis"));
        var sdlTargetCommitFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "sdl_last_targeted_commit"));

#if WINDOWS
        var c2ffiExe = new FileInfo(Path.Combine(AppContext.BaseDirectory, path2: "c2ffi.exe"));
        var gitExe = FindInPath("git.exe");
        var dotnetExe = FindInPath("dotnet.exe");
#else
        var c2ffiExe = new FileInfo(Path.Combine(AppContext.BaseDirectory, "c2ffi"));
        var gitExe = FindInPath("git");
        var dotnetExe = FindInPath("dotnet");
#endif

        // BUILD FFI.JSON

        // if (!sdlDir.Exists)
        // {
        //     Console.WriteLine($"ERROR: sdl dir `{sdlDir.FullName}` does not exist!");
        //     return 1;
        // }
        //
        // var isFFIJsonUpToDate = false;
        // if (gitExe.Exists && ffiJsonFile.Exists && sdlTargetCommitFile.Exists)
        // {
        //     Console.WriteLine("checking if ffi.json is up-to-date...");
        //     var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
        //     if (commit == File.ReadAllText(sdlTargetCommitFile.FullName))
        //     {
        //         Console.WriteLine($"ffi.json is up-to-date (SDL commit {commit.Trim()})!! skipping c2ffi...");
        //         isFFIJsonUpToDate = true;
        //     }
        // }
        //
        // if (!isFFIJsonUpToDate)
        // {
        //     if (ffiJsonIntermediateDir.Exists)
        //     {
        //         foreach (var file in ffiJsonIntermediateDir.GetFiles())
        //         {
        //             file.Delete();
        //         }
        //     }
        //     else
        //     {
        //         ffiJsonIntermediateDir.Create();
        //     }
        //
        //     if (c2ffiConfigFile.Exists)
        //     {
        //         c2ffiConfigFile.Delete();
        //     }
        //
        //     using (var writer = c2ffiConfigFile.CreateText())
        //     {
        //         using (var reader = c2ffiConfigTemplateFile.OpenText())
        //         {
        //             while (!reader.EndOfStream)
        //                 writer.WriteLine(
        //                     reader.ReadLine()!
        //                         .Replace("TEMPLATE_SDL_PATH", sdlDir.FullName)
        //                         .Replace("TEMPLATE_OUTPUT_DIR", ffiJsonIntermediateDir.FullName)
        //                 );
        //         }
        //     }
        //
        //     RunProcess(c2ffiExe, args: $"extract --config {c2ffiConfigFile.FullName}");
        //     RunProcess(c2ffiExe, args: $"merge --inputDirectoryPath {ffiJsonIntermediateDir.FullName} --outputFilePath {ffiJsonFile.FullName}");
        //
        //     if (gitExe.Exists)
        //     {
        //         var commit = RunProcess(gitExe, "show -s --format=\"%H\"", workingDir: sdlDir, redirectStdOut: true).StandardOutput.ReadToEnd();
        //         File.WriteAllText(sdlTargetCommitFile.FullName, commit);
        //     }
        // }

        // PARSE FFI.JSON

        RawFFIEntry[]? ffiData = JsonSerializer.Deserialize<RawFFIEntry[]>(File.ReadAllText(ffiJsonFile.FullName));
        if (ffiData == null)
        {
            Console.WriteLine($"failed to read ffi.json file {ffiJsonFile.FullName}!!");
            return 1;
        }

        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if ((entry.Tag == "typedef") && entry.Name!.StartsWith("SDL_"))
            {
                TypedefMap[entry.Name!] = entry.Type!;
            }
        }

        var definitions = new StringBuilder();
        var unknownPointerParameters = new StringBuilder();
        var currentSourceFile = "";

        foreach (var entry in ffiData)
        {
            if (DeniedTypes.Contains(entry.Name))
            {
                continue;
            }

            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            var headerFile = entry.Header.Split(":")[0];
            if (currentSourceFile != headerFile)
            {
                definitions.Append($"// {headerFile}\n\n");
                currentSourceFile = headerFile;
            }

            if (entry.Tag == "typedef")
            {
                if (entry.Type!.Tag == "function-pointer")
                {
                    definitions.Append(
                        $"// public static delegate RETURN {entry.Name}(PARAMS)\t// WARN_UNDEFINED_FUNCTION_POINTER: {entry.Header}\n\n"
                    );
                }
            }

            if (entry.Tag == "enum")
            {
                definitions.Append($"public enum {entry.Name!}\n{{\n");
                DefinedTypes.Add(entry.Name!);

                foreach (var enumValue in entry.Fields!)
                {
                    definitions.Append($"{enumValue.Name} = {(int) enumValue.Value!},\n");
                }

                definitions.Append("}\n\n");
            }

            else if (entry.Tag == "struct")
            {
                if (entry.Fields!.Length == 0)
                {
                    continue;
                }

                DefinedTypes.Add(entry.Name!);

                var hasUnionFields = false;
                foreach (var field in entry.Fields!)
                {
                    if (field.Type!.Tag == "union")
                    {
                        hasUnionFields = true;
                        break;
                    }
                }

                if (hasUnionFields)
                {
                    var internalStructs = new Dictionary<string, RawFFIEntry>();

                    definitions.Append("[StructLayout(LayoutKind.Explicit)]\n");
                    definitions.Append($"public struct {entry.Name!}\n{{\n");

                    foreach (var field in entry.Fields!)
                    {
                        var fieldName = SanitizeNames(field.Name!);
                        var fieldTypedef = GetTypeFromTypedefMap(type: field.Type!);
                        var fieldTypeName = CSharpTypeFromFFI(fieldTypedef, TypeContext.StructField);

                        if (fieldTypeName == "UNION")
                        {
                            foreach (var unionField in fieldTypedef.Fields!)
                            {
                                var unionFieldName = SanitizeNames(unionField.Name!);
                                var unionFieldTypedef = GetTypeFromTypedefMap(type: unionField.Type!);
                                var unionFieldTypeName = CSharpTypeFromFFI(unionFieldTypedef, TypeContext.StructField);

                                if ((unionFieldTypeName == "") && (unionFieldTypedef.Tag == "struct"))
                                {
                                    unionFieldTypeName = $"INTERNAL_{entry.Name!}_{fieldName}_{unionFieldName}";
                                    internalStructs.Add(unionFieldTypeName, unionFieldTypedef);
                                }

                                definitions.Append($"[FieldOffset({field.BitOffset / 8})]\n");
                                definitions.Append($"public {unionFieldTypeName} {fieldName}_{unionFieldName};\n");
                            }
                        }
                        else if (fieldTypeName == "INLINE_ARRAY")
                        {
                            definitions.Append($"[FieldOffset({field.BitOffset / 8})]\n");
                            var elementType = CSharpTypeFromFFI(type: fieldTypedef.Type!, TypeContext.StructField);
                            for (var i = 0; i < fieldTypedef.Size; i++) definitions.Append($"public {elementType} {fieldName}{i};\n");
                        }
                        else if (fieldTypeName == "FUNCTION_POINTER")
                        {
                            definitions.Append($"[FieldOffset({field.BitOffset / 8})]\n");
                            fieldTypeName = "IntPtr";
                            definitions.Append($"public {fieldTypeName} {fieldName};");
                            if (field.Type!.Tag == "function-pointer")
                            {
                                definitions.Append("\t// WARN_ANONYMOUS_FUNCTION_POINTER");
                            }
                            else
                            {
                                definitions.Append($"\t// {field.Type!.Tag}");
                            }

                            definitions.Append('\n');
                        }
                        else
                        {
                            definitions.Append($"[FieldOffset({field.BitOffset / 8})]\n");
                            definitions.Append($"public {fieldTypeName} {fieldName};\n");
                        }
                    }

                    definitions.Append("}\n\n");

                    foreach (var (name, structDef) in internalStructs)
                    {
                        definitions.Append("[StructLayout(LayoutKind.Sequential)]\n");
                        definitions.Append($"public struct {name}\n{{\n");

                        foreach (var field in structDef.Fields!)
                        {
                            var fieldName = SanitizeNames(field.Name!);
                            var fieldTypedef = GetTypeFromTypedefMap(type: field.Type!);
                            var type = CSharpTypeFromFFI(fieldTypedef, TypeContext.StructField);
                            definitions.Append($"public {type} {fieldName};\n");
                        }

                        definitions.Append("}\n\n");
                    }
                }
                else
                {
                    definitions.Append("[StructLayout(LayoutKind.Sequential)]\n");
                    definitions.Append($"public struct {entry.Name!}\n{{\n");

                    foreach (var field in entry.Fields!)
                    {
                        var name = SanitizeNames(field.Name!);
                        var fieldTypedef = GetTypeFromTypedefMap(type: field.Type!);
                        var type = CSharpTypeFromFFI(fieldTypedef, TypeContext.StructField);

                        if (type == "INLINE_ARRAY")
                        {
                            var elementType = CSharpTypeFromFFI(type: fieldTypedef.Type!, TypeContext.StructField);
                            for (var i = 0; i < fieldTypedef.Size; i++) definitions.Append($"public {elementType} {name}{i};\n");
                        }
                        else if (type == "UNION")
                        {
                            type = $"UNION_{entry.Name}_{field.Name}";
                            definitions.Append($"// public {type} {name}; // WARN_UNHANDLED_UNION\n");
                        }
                        else if (type == "FUNCTION_POINTER")
                        {
                            type = "IntPtr";
                            definitions.Append($"public {type} {name};");
                            if (field.Type!.Tag == "function-pointer")
                            {
                                definitions.Append("\t// WARN_ANONYMOUS_FUNCTION_POINTER");
                            }
                            else
                            {
                                definitions.Append($"\t// {field.Type!.Tag}");
                            }

                            definitions.Append('\n');
                        }
                        else
                        {
                            definitions.Append($"public {type} {name};\n");
                        }
                    }

                    definitions.Append("}\n\n");
                }
            }

            else if (entry.Tag == "function")
            {
                var hasVarArgs = false;
                foreach (var parameter in entry.Parameters!)
                {
                    if (parameter.Type!.Tag == "va_list")
                    {
                        hasVarArgs = true;
                        break;
                    }
                }

                if (hasVarArgs)
                {
                    continue;
                }

                definitions.Append("[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]\n");
                var returnTypedef = GetTypeFromTypedefMap(type: entry.ReturnType!);
                var returnType = CSharpTypeFromFFI(returnTypedef, TypeContext.Return);
                if (returnType == "FUNCTION_POINTER")
                {
                    returnType = "IntPtr";
                }

                definitions.Append($"public static extern {returnType} {entry.Name!}(");

                var initialParameter = true;
                var containsUnknownRef = false;
                foreach (var parameter in entry.Parameters!)
                {
                    if (!initialParameter)
                    {
                        definitions.Append(", ");
                    }
                    else
                    {
                        initialParameter = false;
                    }

                    var parameterTypedef = GetTypeFromTypedefMap(type: parameter.Type!);

                    string typeName;
                    if ((parameter.Type!.Tag == "pointer") && IsDefinedType(parameter.Type!.Type!.Tag))
                    {
                        var subtype = GetTypeFromTypedefMap(type: parameter.Type!.Type!);
                        var subtypeName = CSharpTypeFromFFI(subtype, TypeContext.Parameter);
                        if (PointerParametersIntents.TryGetValue(key: (entry.Name!, parameter.Name!), value: out var intent))
                        {
                            typeName = intent switch
                            {
                                PointerParameterIntent.Ref   => $"ref {subtypeName}",
                                PointerParameterIntent.Out   => $"out {subtypeName}",
                                PointerParameterIntent.Array => $"{subtypeName}*",
                                _                            => $"ref {subtypeName}",
                            };
                            if (intent == PointerParameterIntent.Unknown)
                            {
                                containsUnknownRef = true;
                            }
                        }
                        else
                        {
                            typeName = $"ref {subtypeName}";
                            containsUnknownRef = true;
                            unknownPointerParameters.Append(
                                $"{{ (\"{entry.Name!}\", \"{parameter.Name!}\"), PointerParameterIntent.Unknown }},\n"
                            );
                        }
                    }
                    else
                    {
                        typeName = CSharpTypeFromFFI(parameterTypedef, TypeContext.Parameter);
                        if (typeName == "FUNCTION_POINTER")
                        {
                            typeName = $"/* {parameter.Type!.Tag} */ IntPtr";
                        }
                    }

                    var name = SanitizeNames(parameter.Name!);
                    definitions.Append($"{typeName} {name}");
                }

                definitions.Append(");\t");
                if (containsUnknownRef)
                {
                    definitions.Append("// WARN_UNKNOWN_POINTER_PARAMETER: check for array usage");
                }

                definitions.Append("\n\n");
            }
        }

        File.WriteAllText(
            path: Path.Combine(outputDir.FullName, "SDL3.cs"),
            contents: CompileBindingsCSharp(definitions.ToString())
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");
        if (unknownPointerParameters.Length > 0)
        {
            Console.Write($"new pointer parameters (add these to `ParametersIntents` dictionary:\n\n{unknownPointerParameters}");
        }

        return 0;
    }

    private static FileInfo FindInPath(string exeName)
    {
        var envPath = Environment.GetEnvironmentVariable("PATH");
        if (envPath != null)
        {
            foreach (var envPathDir in envPath.Split(Path.PathSeparator))
            {
                var path = Path.Combine(envPathDir, exeName);
                if (File.Exists(path))
                {
                    return new FileInfo(path);
                }
            }
        }

        return new FileInfo("");
    }

    private static Process RunProcess(FileInfo exe, string args, bool redirectStdOut = false, DirectoryInfo? workingDir = null)
    {
        var process = new Process();
        process.StartInfo.FileName = exe.FullName;
        process.StartInfo.Arguments = args;
        process.StartInfo.RedirectStandardOutput = redirectStdOut;
        process.StartInfo.WorkingDirectory = workingDir?.FullName ?? AppContext.BaseDirectory;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new SystemException($@"process `{exe.FullName} {args}` failed!!\n");
        }

        return process;
    }

    private static string CompileBindingsCSharp(string definitions)
    {
        return $@"
using System.Runtime.InteropServices;

namespace SDL3;

public static unsafe class SDL
{{
    private const string nativeLibName = ""SDL3"";

    {definitions}
}}
";
    }

    private static RawFFIEntry GetTypeFromTypedefMap(RawFFIEntry type)
    {
        if (type.Tag.StartsWith("SDL_") && TypedefMap.TryGetValue(type.Tag, value: out var value))
        {
            return value;
        }

        return type;
    }

    private static string CSharpTypeFromFFI(RawFFIEntry type, TypeContext context)
    {
        if ((type.Tag == "pointer") && IsDefinedType(type.Type!.Tag))
        {
            var subtype = GetTypeFromTypedefMap(type.Type!);
            var subtypeName = CSharpTypeFromFFI(subtype, context);

            return context switch
            {
                TypeContext.StructField => $"{subtypeName}*",
                _                       => "IntPtr",
            };
        }

        return type.Tag switch
        {
            "_Bool"            => "bool",
            "int"              => "int",
            "long"             => "long",
            "unsigned-short"   => "ushort",
            "unsigned-int"     => "uint",
            "unsigned-long"    => "ulong",
            "float"            => "float",
            "double"           => "double",
            "Uint8"            => "byte",
            "Uint16"           => "UInt16",
            "Uint32"           => "UInt32",
            "Uint64"           => "UInt64",
            "Sint8"            => "sbyte",
            "Sint16"           => "Int16",
            "Sint32"           => "Int32",
            "Sint64"           => "Int64",
            "size_t"           => "UIntPtr",
            "wchar_t"          => "char", // TODO: doubt it's this easy (stdinc + sdl_hidapi)
            "unsigned-char"    => "byte", // TODO: confirm this (sdl_hidapi)
            "void"             => "void",
            "pointer"          => "IntPtr",
            "function-pointer" => "FUNCTION_POINTER",
            "enum"             => type.Name!,
            "struct"           => type.Name!,
            "array"            => "INLINE_ARRAY",
            "union"            => "UNION",
            _                  => type.Tag,
        };
    }

    private static string SanitizeNames(string unsanitizedName)
    {
        return unsanitizedName switch
        {
            "internal" => "@internal",
            "event"    => "@event",
            "override" => "@override",
            "base"     => "@base",
            "lock"     => "@lock",
            "string"   => "@string",
            ""         => "_",
            _          => unsanitizedName,
        };
    }

    private static bool IsDefinedType(string typeName)
    {
        return
            (typeName != "void") && (
                !typeName.StartsWith("SDL_") // assume no SDL prefix == std library or primitive typename
                || DefinedTypes.Contains(typeName)
            );
    }
}
