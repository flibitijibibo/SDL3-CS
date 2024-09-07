namespace GenerateBindings;

internal static class UserProvidedData
{
    internal enum PointerParameterIntent
    {
        Unknown,
        Ref,
        Out,
        Array,
    }

    internal struct DelegateDefinition
    {
        public string ReturnType { get; set; }
        public (string, string)[] Parameters { get; set; }
    }

    internal static readonly Dictionary<(string, string), PointerParameterIntent> PointerParametersIntents = new()
    {
        { ("SDL_BeginGPURenderPass", "colorAttachmentInfos"), PointerParameterIntent.Array },
        { ("SDL_BeginGPURenderPass", "depthStencilAttachmentInfo"), PointerParameterIntent.Ref },
        { ("SDL_ReportAssertion", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_assert.h:245:45
        { ("SDL_ReportAssertion", "func"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_assert.h:245:45
        { ("SDL_ReportAssertion", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_assert.h:245:45
        { ("SDL_GetAssertionHandler", "puserdata"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_assert.h:489:50
        { ("SDL_AtomicCompareAndSwap", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:347:38
        { ("SDL_AtomicSet", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:367:33
        { ("SDL_AtomicGet", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:384:33
        { ("SDL_AtomicAdd", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:405:33
        { ("SDL_AtomicCompareAndSwapPointer", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:461:38
        { ("SDL_AtomicSetPointer", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:480:36
        { ("SDL_AtomicGetPointer", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_atomic.h:498:36
        { ("SDL_SetError", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_error.h:66:38
        { ("SDL_SetPointerPropertyWithCleanup", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:218:38
        { ("SDL_SetPointerProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:241:38
        { ("SDL_SetStringProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:261:38
        { ("SDL_SetStringProperty", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:261:38
        { ("SDL_SetNumberProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:278:38
        { ("SDL_SetFloatProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:295:38
        { ("SDL_SetBooleanProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:312:38
        { ("SDL_HasProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:327:38
        { ("SDL_GetPropertyType", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:343:46
        { ("SDL_GetPointerProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:376:36
        { ("SDL_GetStringProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:400:42
        { ("SDL_GetStringProperty", "default_value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:400:42
        { ("SDL_GetNumberProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:422:36
        { ("SDL_GetFloatProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:444:35
        { ("SDL_GetBooleanProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:466:38
        { ("SDL_ClearProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_properties.h:480:38
        { ("SDL_IOFromFile", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:239:44
        { ("SDL_IOFromFile", "mode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:239:44
        { ("SDL_OpenIO", "iface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:368:44
        { ("SDL_IOprintf", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:548:36
        { ("SDL_LoadFile_IO", "datasize"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:588:36
        { ("SDL_LoadFile", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:608:36
        { ("SDL_LoadFile", "datasize"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:608:36
        { ("SDL_ReadU8", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:627:38
        { ("SDL_ReadS8", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:639:38
        { ("SDL_ReadU16LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:655:38
        { ("SDL_ReadS16LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:671:38
        { ("SDL_ReadU16BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:687:38
        { ("SDL_ReadS16BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:703:38
        { ("SDL_ReadU32LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:719:38
        { ("SDL_ReadS32LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:735:38
        { ("SDL_ReadU32BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:751:38
        { ("SDL_ReadS32BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:767:38
        { ("SDL_ReadU64LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:783:38
        { ("SDL_ReadS64LE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:799:38
        { ("SDL_ReadU64BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:815:38
        { ("SDL_ReadS64BE", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_iostream.h:831:38
        { ("SDL_CreateThreadRuntime", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_thread.h:305:42
        { ("SDL_WaitThread", "status"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_thread.h:424:34
        { ("SDL_GetAudioPlaybackDevices", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:461:49
        { ("SDL_GetAudioRecordingDevices", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:490:49
        { ("SDL_GetAudioDeviceFormat", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:542:38
        { ("SDL_GetAudioDeviceFormat", "sample_frames"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:542:38
        { ("SDL_GetAudioDeviceChannelMap", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:565:35
        { ("SDL_OpenAudioDevice", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:641:47
        { ("SDL_BindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:838:38
        { ("SDL_UnbindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:879:34
        { ("SDL_CreateAudioStream", "src_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:937:47
        { ("SDL_CreateAudioStream", "dst_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:937:47
        { ("SDL_GetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:966:38
        { ("SDL_GetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:966:38
        { ("SDL_SetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:997:38
        { ("SDL_SetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:997:38
        { ("SDL_GetAudioStreamInputChannelMap", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1111:35
        { ("SDL_GetAudioStreamOutputChannelMap", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1135:35
        { ("SDL_SetAudioStreamInputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1186:38
        { ("SDL_SetAudioStreamOutputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1233:38
        { ("SDL_OpenAudioDeviceStream", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1707:47
        { ("SDL_LoadWAV_IO", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV_IO", "audio_buf"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV_IO", "audio_len"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1915:38
        { ("SDL_LoadWAV", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1915:38
        { ("SDL_LoadWAV", "audio_buf"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1915:38
        { ("SDL_LoadWAV", "audio_len"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1915:38
        { ("SDL_MixAudio", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1951:38
        { ("SDL_MixAudio", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1951:38
        { ("SDL_ConvertAudioSamples", "src_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "src_data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_len"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_audio.h:1981:38
        { ("SDL_GetMasksForPixelFormat", "bpp"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:772:38
        { ("SDL_GetMasksForPixelFormat", "Rmask"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:772:38
        { ("SDL_GetMasksForPixelFormat", "Gmask"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:772:38
        { ("SDL_GetMasksForPixelFormat", "Bmask"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:772:38
        { ("SDL_GetMasksForPixelFormat", "Amask"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:772:38
        { ("SDL_SetPaletteColors", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:848:38
        { ("SDL_SetPaletteColors", "colors"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:848:38
        { ("SDL_DestroyPalette", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:862:34
        { ("SDL_MapRGB", "format"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:900:36
        { ("SDL_MapRGB", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:900:36
        { ("SDL_MapRGBA", "format"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:939:36
        { ("SDL_MapRGBA", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:939:36
        { ("SDL_GetRGB", "format"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:967:34
        { ("SDL_GetRGB", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:967:34
        { ("SDL_GetRGB", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:967:34
        { ("SDL_GetRGB", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:967:34
        { ("SDL_GetRGB", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:967:34
        { ("SDL_GetRGBA", "format"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_GetRGBA", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_GetRGBA", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_GetRGBA", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_GetRGBA", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_GetRGBA", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_pixels.h:999:34
        { ("SDL_RectToFRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:126:23
        { ("SDL_RectToFRect", "frect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:126:23
        { ("SDL_PointInRect", "p"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:155:27
        { ("SDL_PointInRect", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:155:27
        { ("SDL_RectEmpty", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:179:27
        { ("SDL_RectsEqual", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:203:27
        { ("SDL_RectsEqual", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:203:27
        { ("SDL_HasRectIntersection", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:224:38
        { ("SDL_HasRectIntersection", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:224:38
        { ("SDL_GetRectIntersection", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectIntersection", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectIntersection", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectUnion", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectUnion", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectUnion", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectEnclosingPoints", "points"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectEnclosingPoints", "clip"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectEnclosingPoints", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectAndLineIntersection", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "X1"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "Y1"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "X2"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "Y2"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:294:38
        { ("SDL_PointInRectFloat", "p"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:320:27
        { ("SDL_PointInRectFloat", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:320:27
        { ("SDL_RectEmptyFloat", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:344:27
        { ("SDL_RectsEqualEpsilon", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:374:27
        { ("SDL_RectsEqualEpsilon", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:374:27
        { ("SDL_RectsEqualFloat", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:409:27
        { ("SDL_RectsEqualFloat", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:409:27
        { ("SDL_HasRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:427:38
        { ("SDL_HasRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:427:38
        { ("SDL_GetRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectIntersectionFloat", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectUnionFloat", "A"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectUnionFloat", "B"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectUnionFloat", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectEnclosingPointsFloat", "points"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectEnclosingPointsFloat", "clip"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectEnclosingPointsFloat", "result"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectAndLineIntersectionFloat", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "X1"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "Y1"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "X2"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "Y2"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_rect.h:499:38
        { ("SDL_DestroySurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:183:34
        { ("SDL_GetSurfaceProperties", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:211:46
        { ("SDL_SetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:233:38
        { ("SDL_GetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:250:44
        { ("SDL_CreateSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:278:43
        { ("SDL_SetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:295:38
        { ("SDL_SetSurfacePalette", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:295:38
        { ("SDL_GetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:308:43
        { ("SDL_AddSurfaceAlternateImage", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:333:38
        { ("SDL_AddSurfaceAlternateImage", "image"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:333:38
        { ("SDL_SurfaceHasAlternateImages", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:348:38
        { ("SDL_GetSurfaceImages", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:373:44
        { ("SDL_GetSurfaceImages", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:373:44
        { ("SDL_RemoveSurfaceAlternateImages", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:389:34
        { ("SDL_LockSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:412:38
        { ("SDL_UnlockSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:423:34
        { ("SDL_LoadBMP", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:461:43
        { ("SDL_SaveBMP_IO", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:484:38
        { ("SDL_SaveBMP", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:505:38
        { ("SDL_SaveBMP", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:505:38
        { ("SDL_SetSurfaceRLE", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:525:38
        { ("SDL_SurfaceHasRLE", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:539:38
        { ("SDL_SetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:564:38
        { ("SDL_SurfaceHasColorKey", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:579:38
        { ("SDL_GetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:599:38
        { ("SDL_GetSurfaceColorKey", "key"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:599:38
        { ("SDL_SetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:622:38
        { ("SDL_GetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:640:38
        { ("SDL_SetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:660:38
        { ("SDL_GetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:675:38
        { ("SDL_GetSurfaceAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:675:38
        { ("SDL_SetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:693:38
        { ("SDL_GetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:707:38
        { ("SDL_SetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:728:38
        { ("SDL_SetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:728:38
        { ("SDL_GetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:747:38
        { ("SDL_GetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:747:38
        { ("SDL_FlipSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:759:38
        { ("SDL_DuplicateSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:777:43
        { ("SDL_ScaleSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:796:43
        { ("SDL_ConvertSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:822:43
        { ("SDL_ConvertSurfaceAndColorspace", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:849:43
        { ("SDL_ConvertSurfaceAndColorspace", "palette"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:849:43
        { ("SDL_PremultiplySurfaceAlpha", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:935:38
        { ("SDL_ClearSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:955:38
        { ("SDL_FillSurfaceRect", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:980:38
        { ("SDL_FillSurfaceRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:980:38
        { ("SDL_FillSurfaceRects", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1005:38
        { ("SDL_FillSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1005:38
        { ("SDL_BlitSurface", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurfaceUnchecked", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceScaled", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceUncheckedScaled", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceTiled", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiledWithScale", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurface9Grid", "src"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "dst"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1249:38
        { ("SDL_MapSurfaceRGB", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1279:36
        { ("SDL_MapSurfaceRGBA", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1310:36
        { ("SDL_ReadSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1361:38
        { ("SDL_WriteSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1384:38
        { ("SDL_WriteSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_surface.h:1404:38
        { ("SDL_GetDisplays", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:415:45
        { ("SDL_GetDisplayBounds", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:486:38
        { ("SDL_GetDisplayUsableBounds", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:510:38
        { ("SDL_GetFullscreenDisplayModes", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:580:48
        { ("SDL_GetClosestFullscreenDisplayMode", "mode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:609:38
        { ("SDL_GetDisplayForPoint", "point"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:661:43
        { ("SDL_GetDisplayForRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:676:43
        { ("SDL_SetWindowFullscreenMode", "mode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:763:38
        { ("SDL_GetWindowICCProfile", "size"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:790:36
        { ("SDL_GetWindows", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:816:43
        { ("SDL_CreateWindow", "title"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:899:42
        { ("SDL_SetWindowTitle", "title"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1337:38
        { ("SDL_SetWindowIcon", "icon"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1372:38
        { ("SDL_GetWindowPosition", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1435:38
        { ("SDL_GetWindowPosition", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1435:38
        { ("SDL_GetWindowSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1492:38
        { ("SDL_GetWindowSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1492:38
        { ("SDL_GetWindowSafeArea", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1512:38
        { ("SDL_GetWindowAspectRatio", "min_aspect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1568:38
        { ("SDL_GetWindowAspectRatio", "max_aspect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1568:38
        { ("SDL_GetWindowBordersSize", "top"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1602:38
        { ("SDL_GetWindowBordersSize", "left"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1602:38
        { ("SDL_GetWindowBordersSize", "bottom"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1602:38
        { ("SDL_GetWindowBordersSize", "right"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1602:38
        { ("SDL_GetWindowSizeInPixels", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1620:38
        { ("SDL_GetWindowSizeInPixels", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1620:38
        { ("SDL_GetWindowMinimumSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1654:38
        { ("SDL_GetWindowMinimumSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1654:38
        { ("SDL_GetWindowMaximumSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1688:38
        { ("SDL_GetWindowMaximumSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:1688:38
        { ("SDL_GetWindowSurfaceVSync", "vsync"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2013:38
        { ("SDL_UpdateWindowSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2059:38
        { ("SDL_SetWindowMouseRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2176:38
        { ("SDL_SetWindowShape", "shape"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2381:38
        { ("SDL_GL_LoadLibrary", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2487:38
        { ("SDL_GL_GetProcAddress", "proc"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2540:49
        { ("SDL_EGL_GetProcAddress", "proc"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2557:49
        { ("SDL_GL_ExtensionSupported", "extension"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2587:38
        { ("SDL_GL_GetAttribute", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2634:38
        { ("SDL_GL_GetSwapInterval", "interval"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_video.h:2799:38
        { ("SDL_GetCameras", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:180:44
        { ("SDL_GetCameraSupportedFormats", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:219:47
        { ("SDL_OpenCamera", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:299:42
        { ("SDL_GetCameraFormat", "spec"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:385:38
        { ("SDL_AcquireCameraFrame", "timestampNS"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:428:43
        { ("SDL_ReleaseCameraFrame", "frame"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_camera.h:456:34
        { ("SDL_SetClipboardText", "text"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:57:38
        { ("SDL_SetPrimarySelectionText", "text"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:100:38
        { ("SDL_SetClipboardData", "mime_types"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:197:38
        { ("SDL_GetClipboardData", "mime_type"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:228:36
        { ("SDL_GetClipboardData", "size"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:228:36
        { ("SDL_HasClipboardData", "mime_type"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_clipboard.h:242:38
        { ("SDL_ShowOpenFileDialog", "filters"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_dialog.h:153:34
        { ("SDL_ShowOpenFileDialog", "default_location"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_dialog.h:153:34
        { ("SDL_ShowSaveFileDialog", "filters"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_dialog.h:208:34
        { ("SDL_ShowSaveFileDialog", "default_location"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_dialog.h:208:34
        { ("SDL_ShowOpenFolderDialog", "default_location"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_dialog.h:256:34
        { ("SDL_GUIDToString", "pszGUID"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_guid.h:77:34
        { ("SDL_StringToGUID", "pchGUID"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_guid.h:93:38
        { ("SDL_GetPowerInfo", "seconds"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_power.h:85:44
        { ("SDL_GetPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_power.h:85:44
        { ("SDL_GetSensors", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_sensor.h:156:44
        { ("SDL_GetSensorData", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_sensor.h:285:38
        { ("SDL_GetJoysticks", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:214:46
        { ("SDL_AttachVirtualJoystick", "desc"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:468:44
        { ("SDL_SendJoystickVirtualSensorData", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:622:38
        { ("SDL_GetJoystickGUIDInfo", "vendor"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:824:34
        { ("SDL_GetJoystickGUIDInfo", "product"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:824:34
        { ("SDL_GetJoystickGUIDInfo", "version"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:824:34
        { ("SDL_GetJoystickGUIDInfo", "crc16"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:824:34
        { ("SDL_GetJoystickAxisInitialState", "state"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:1001:38
        { ("SDL_GetJoystickBall", "dx"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:1022:38
        { ("SDL_GetJoystickBall", "dy"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:1022:38
        { ("SDL_GetJoystickPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_joystick.h:1188:44
        { ("SDL_AddGamepadMapping", "mapping"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:313:33
        { ("SDL_AddGamepadMappingsFromFile", "file"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:375:33
        { ("SDL_GetGamepadMappings", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:401:37
        { ("SDL_SetGamepadMapping", "mapping"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:453:38
        { ("SDL_GetGamepads", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:480:46
        { ("SDL_GetGamepadPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:934:44
        { ("SDL_GetGamepadBindings", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1010:51
        { ("SDL_GetGamepadTypeFromString", "str"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1039:45
        { ("SDL_GetGamepadAxisFromString", "str"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1075:45
        { ("SDL_GetGamepadButtonFromString", "str"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1148:47
        { ("SDL_GetGamepadTouchpadFinger", "state"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1267:38
        { ("SDL_GetGamepadTouchpadFinger", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1267:38
        { ("SDL_GetGamepadTouchpadFinger", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1267:38
        { ("SDL_GetGamepadTouchpadFinger", "pressure"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1267:38
        { ("SDL_GetGamepadSensorData", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gamepad.h:1339:38
        { ("SDL_GetKeyboards", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:86:46
        { ("SDL_GetKeyboardState", "numkeys"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:141:43
        { ("SDL_SetScancodeName", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:241:38
        { ("SDL_GetScancodeFromName", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:280:42
        { ("SDL_GetKeyFromName", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:311:41
        { ("SDL_SetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:496:38
        { ("SDL_GetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:515:38
        { ("SDL_GetTextInputArea", "cursor"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_keyboard.h:515:38
        { ("SDL_GetMice", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:166:43
        { ("SDL_GetMouseState", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:212:50
        { ("SDL_GetMouseState", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:212:50
        { ("SDL_GetGlobalMouseState", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:242:50
        { ("SDL_GetGlobalMouseState", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:242:50
        { ("SDL_GetRelativeMouseState", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:261:50
        { ("SDL_GetRelativeMouseState", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:261:50
        { ("SDL_CreateCursor", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateCursor", "mask"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateColorCursor", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_mouse.h:460:42
        { ("SDL_GetTouchDevices", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_touch.h:94:43
        { ("SDL_GetTouchFingers", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_touch.h:130:43
        { ("SDL_PeepEvents", "events"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1051:33
        { ("SDL_PollEvent", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1183:38
        { ("SDL_WaitEvent", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1205:38
        { ("SDL_WaitEventTimeout", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1233:38
        { ("SDL_PushEvent", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1267:38
        { ("SDL_GetEventFilter", "userdata"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1353:38
        { ("SDL_GetWindowFromEvent", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_events.h:1470:42
        { ("SDL_GetPrefPath", "org"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:135:36
        { ("SDL_GetPrefPath", "app"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:135:36
        { ("SDL_CreateDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:274:38
        { ("SDL_EnumerateDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:297:38
        { ("SDL_RemovePath", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:308:38
        { ("SDL_RenamePath", "oldpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:320:38
        { ("SDL_RenamePath", "newpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:320:38
        { ("SDL_CopyFile", "oldpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:332:38
        { ("SDL_CopyFile", "newpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:332:38
        { ("SDL_GetPathInfo", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:345:38
        { ("SDL_GetPathInfo", "info"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:345:38
        { ("SDL_GlobDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:378:37
        { ("SDL_GlobDirectory", "pattern"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:378:37
        { ("SDL_GlobDirectory", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_filesystem.h:378:37
        { ("SDL_CreateGPUDevice", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:961:44
        { ("SDL_CreateGPUComputePipeline", "computePipelineCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1076:53
        { ("SDL_CreateGPUGraphicsPipeline", "pipelineCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1094:54
        { ("SDL_CreateGPUSampler", "samplerCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1113:45
        { ("SDL_CreateGPUShader", "shaderCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1160:44
        { ("SDL_CreateGPUTexture", "textureCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1195:45
        { ("SDL_CreateGPUBuffer", "bufferCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1223:44
        { ("SDL_CreateGPUTransferBuffer", "transferBufferCreateInfo"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1244:52
        { ("SDL_SetGPUBufferName", "text"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1261:34
        { ("SDL_SetGPUTextureName", "text"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1277:34
        { ("SDL_InsertGPUDebugLabel", "text"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1292:34
        { ("SDL_PushGPUDebugGroup", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1317:34
        { ("SDL_SetGPUViewport", "viewport"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1636:34
        { ("SDL_SetGPUScissor", "scissor"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1648:34
        { ("SDL_BindGPUVertexBuffers", "pBindings"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1664:34
        { ("SDL_BindGPUIndexBuffer", "pBinding"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1682:34
        { ("SDL_BindGPUVertexSamplers", "textureSamplerBindings"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1700:34
        { ("SDL_BindGPUVertexStorageTextures", "storageTextures"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1719:34
        { ("SDL_BindGPUVertexStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1738:34
        { ("SDL_BindGPUFragmentSamplers", "textureSamplerBindings"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1757:34
        { ("SDL_BindGPUFragmentStorageTextures", "storageTextures"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1776:34
        { ("SDL_BindGPUFragmentStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1795:34
        { ("SDL_BeginGPUComputePass", "storageTextureBindings"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1950:49
        { ("SDL_BeginGPUComputePass", "storageBufferBindings"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1950:49
        { ("SDL_BindGPUComputeStorageTextures", "storageTextures"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:1982:34
        { ("SDL_BindGPUComputeStorageBuffers", "storageBuffers"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2001:34
        { ("SDL_UploadToGPUTexture", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2134:34
        { ("SDL_UploadToGPUTexture", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2134:34
        { ("SDL_UploadToGPUBuffer", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2156:34
        { ("SDL_UploadToGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2156:34
        { ("SDL_CopyGPUTextureToTexture", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2179:34
        { ("SDL_CopyGPUTextureToTexture", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2179:34
        { ("SDL_CopyGPUBufferToBuffer", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2205:34
        { ("SDL_CopyGPUBufferToBuffer", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2205:34
        { ("SDL_DownloadFromGPUTexture", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2225:34
        { ("SDL_DownloadFromGPUTexture", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2225:34
        { ("SDL_DownloadFromGPUBuffer", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2242:34
        { ("SDL_DownloadFromGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2242:34
        { ("SDL_BlitGPUTexture", "source"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2286:34
        { ("SDL_BlitGPUTexture", "destination"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2286:34
        { ("SDL_AcquireGPUSwapchainTexture", "pWidth"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2437:45
        { ("SDL_AcquireGPUSwapchainTexture", "pHeight"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2437:45
        { ("SDL_WaitForGPUFences", "pFences"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_gpu.h:2515:34
        { ("SDL_GetHaptics", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_haptic.h:944:44
        { ("SDL_HapticEffectSupported", "effect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_haptic.h:1168:38
        { ("SDL_CreateHapticEffect", "effect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_haptic.h:1185:33
        { ("SDL_UpdateHapticEffect", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_haptic.h:1207:38
        { ("SDL_hid_free_enumeration", "devs"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:252:34
        { ("SDL_hid_open", "serial_number"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:270:46
        { ("SDL_hid_open_path", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:284:46
        { ("SDL_hid_write", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:311:33
        { ("SDL_hid_read_timeout", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:332:33
        { ("SDL_hid_read", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:353:33
        { ("SDL_hid_send_feature_report", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:397:33
        { ("SDL_hid_get_feature_report", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:420:33
        { ("SDL_hid_get_input_report", "data"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:443:33
        { ("SDL_hid_get_manufacturer_string", "string"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:467:33
        { ("SDL_hid_get_product_string", "string"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:480:33
        { ("SDL_hid_get_serial_number_string", "string"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:493:33
        { ("SDL_hid_get_indexed_string", "string"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:507:33
        { ("SDL_hid_get_report_descriptor", "buf"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hidapi.h:535:33
        { ("SDL_SetHintWithPriority", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4176:38
        { ("SDL_SetHintWithPriority", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4176:38
        { ("SDL_SetHint", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4198:38
        { ("SDL_SetHint", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4198:38
        { ("SDL_ResetHint", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4218:38
        { ("SDL_GetHint", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4253:41
        { ("SDL_GetHintBoolean", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4270:38
        { ("SDL_AddHintCallback", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4312:38
        { ("SDL_RemoveHintCallback", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_hints.h:4328:34
        { ("SDL_SetAppMetadata", "appname"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:260:38
        { ("SDL_SetAppMetadata", "appversion"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:260:38
        { ("SDL_SetAppMetadata", "appidentifier"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:260:38
        { ("SDL_SetAppMetadataProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:323:38
        { ("SDL_SetAppMetadataProperty", "value"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:323:38
        { ("SDL_GetAppMetadataProperty", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_init.h:354:42
        { ("SDL_LoadObject", "sofile"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_loadso.h:67:36
        { ("SDL_LoadFunction", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_loadso.h:93:49
        { ("SDL_GetPreferredLocales", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_locale.h:101:43
        { ("SDL_SetLogPriorityPrefix", "prefix"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:203:38
        { ("SDL_Log", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:223:34
        { ("SDL_LogVerbose", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:244:34
        { ("SDL_LogDebug", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:265:34
        { ("SDL_LogInfo", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:286:34
        { ("SDL_LogWarn", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:307:34
        { ("SDL_LogError", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:328:34
        { ("SDL_LogCritical", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:349:34
        { ("SDL_LogMessage", "fmt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:371:34
        { ("SDL_GetLogOutputFunction", "userdata"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_log.h:425:34
        { ("SDL_ShowMessageBox", "messageboxdata"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_messagebox.h:164:38
        { ("SDL_ShowMessageBox", "buttonid"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_messagebox.h:164:38
        { ("SDL_ShowSimpleMessageBox", "title"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_messagebox.h:206:38
        { ("SDL_ShowSimpleMessageBox", "message"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_messagebox.h:206:38
        { ("SDL_OpenURL", "url"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_misc.h:70:38
        { ("SDL_CreateWindowAndRenderer", "title"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:185:38
        { ("SDL_CreateWindowAndRenderer", "window"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:185:38
        { ("SDL_CreateWindowAndRenderer", "renderer"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:185:38
        { ("SDL_CreateRenderer", "name"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:215:44
        { ("SDL_CreateSoftwareRenderer", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:297:44
        { ("SDL_GetRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:454:38
        { ("SDL_GetRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:454:38
        { ("SDL_GetCurrentRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:474:38
        { ("SDL_GetCurrentRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:474:38
        { ("SDL_CreateTextureFromSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:522:43
        { ("SDL_GetTextureSize", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:809:38
        { ("SDL_GetTextureSize", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:809:38
        { ("SDL_GetTextureColorMod", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:883:38
        { ("SDL_GetTextureColorMod", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:883:38
        { ("SDL_GetTextureColorMod", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:883:38
        { ("SDL_GetTextureColorModFloat", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:901:38
        { ("SDL_GetTextureColorModFloat", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:901:38
        { ("SDL_GetTextureColorModFloat", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:901:38
        { ("SDL_GetTextureAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:965:38
        { ("SDL_GetTextureAlphaModFloat", "alpha"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:981:38
        { ("SDL_GetTextureScaleMode", "scaleMode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1044:38
        { ("SDL_UpdateTexture", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1076:38
        { ("SDL_UpdateYUVTexture", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1106:38
        { ("SDL_UpdateYUVTexture", "Yplane"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1106:38
        { ("SDL_UpdateYUVTexture", "Uplane"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1106:38
        { ("SDL_UpdateYUVTexture", "Vplane"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1106:38
        { ("SDL_UpdateNVTexture", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1136:38
        { ("SDL_UpdateNVTexture", "Yplane"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1136:38
        { ("SDL_UpdateNVTexture", "UVplane"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1136:38
        { ("SDL_LockTexture", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1169:38
        { ("SDL_LockTexture", "pixels"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1169:38
        { ("SDL_LockTexture", "pitch"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1169:38
        { ("SDL_LockTextureToSurface", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1205:38
        { ("SDL_LockTextureToSurface", "surface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1205:38
        { ("SDL_GetRenderLogicalPresentation", "w"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1309:38
        { ("SDL_GetRenderLogicalPresentation", "h"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1309:38
        { ("SDL_GetRenderLogicalPresentation", "mode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1309:38
        { ("SDL_GetRenderLogicalPresentation", "scale_mode"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1309:38
        { ("SDL_GetRenderLogicalPresentationRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1329:38
        { ("SDL_RenderCoordinatesFromWindow", "x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1347:38
        { ("SDL_RenderCoordinatesFromWindow", "y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1347:38
        { ("SDL_RenderCoordinatesToWindow", "window_x"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1367:38
        { ("SDL_RenderCoordinatesToWindow", "window_y"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1367:38
        { ("SDL_ConvertEventToRenderCoordinates", "event"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1386:38
        { ("SDL_SetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1402:38
        { ("SDL_GetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1417:38
        { ("SDL_GetRenderSafeArea", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1455:38
        { ("SDL_SetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1471:38
        { ("SDL_GetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1487:38
        { ("SDL_GetRenderScale", "scaleX"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1539:38
        { ("SDL_GetRenderScale", "scaleY"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1539:38
        { ("SDL_GetRenderDrawColor", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1607:38
        { ("SDL_GetRenderDrawColor", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1607:38
        { ("SDL_GetRenderDrawColor", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1607:38
        { ("SDL_GetRenderDrawColor", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1607:38
        { ("SDL_GetRenderDrawColorFloat", "r"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1629:38
        { ("SDL_GetRenderDrawColorFloat", "g"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1629:38
        { ("SDL_GetRenderDrawColorFloat", "b"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1629:38
        { ("SDL_GetRenderDrawColorFloat", "a"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1629:38
        { ("SDL_GetRenderColorScale", "scale"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1665:38
        { ("SDL_RenderPoints", "points"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1743:38
        { ("SDL_RenderLines", "points"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1776:38
        { ("SDL_RenderRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1791:38
        { ("SDL_RenderRects", "rects"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1807:38
        { ("SDL_RenderFillRect", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1823:38
        { ("SDL_RenderFillRects", "rects"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1839:38
        { ("SDL_RenderTexture", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1859:38
        { ("SDL_RenderTexture", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1859:38
        { ("SDL_RenderTextureRotated", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1885:38
        { ("SDL_RenderTextureRotated", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1885:38
        { ("SDL_RenderTextureRotated", "center"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1885:38
        { ("SDL_RenderTextureTiled", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1913:38
        { ("SDL_RenderTextureTiled", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1913:38
        { ("SDL_RenderTexture9Grid", "srcrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1945:38
        { ("SDL_RenderTexture9Grid", "dstrect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1945:38
        { ("SDL_RenderGeometry", "vertices"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1967:38
        { ("SDL_RenderGeometry", "indices"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1967:38
        { ("SDL_RenderGeometryRaw", "xy"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1997:38
        { ("SDL_RenderGeometryRaw", "color"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1997:38
        { ("SDL_RenderGeometryRaw", "uv"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:1997:38
        { ("SDL_RenderReadPixels", "rect"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:2022:43
        { ("SDL_GetRenderVSync", "vsync"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_render.h:2239:38
        { ("SDL_OpenTitleStorage", "override"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:119:43
        { ("SDL_OpenUserStorage", "org"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:145:43
        { ("SDL_OpenUserStorage", "app"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:145:43
        { ("SDL_OpenFileStorage", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:169:43
        { ("SDL_OpenStorage", "iface"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:193:43
        { ("SDL_GetStorageFileSize", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:241:38
        { ("SDL_GetStorageFileSize", "length"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:241:38
        { ("SDL_ReadStorageFile", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:260:38
        { ("SDL_WriteStorageFile", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:278:38
        { ("SDL_CreateStorageDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:292:38
        { ("SDL_EnumerateStorageDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:312:38
        { ("SDL_RemoveStoragePath", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:326:38
        { ("SDL_RenameStoragePath", "oldpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:341:38
        { ("SDL_RenameStoragePath", "newpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:341:38
        { ("SDL_CopyStorageFile", "oldpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:356:38
        { ("SDL_CopyStorageFile", "newpath"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:356:38
        { ("SDL_GetStoragePathInfo", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:372:38
        { ("SDL_GetStoragePathInfo", "info"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:372:38
        { ("SDL_GlobStorageDirectory", "path"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:422:37
        { ("SDL_GlobStorageDirectory", "pattern"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:422:37
        { ("SDL_GlobStorageDirectory", "count"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_storage.h:422:37
        { ("SDL_GetDateTimeLocalePreferences", "dateFormat"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:103:38
        { ("SDL_GetDateTimeLocalePreferences", "timeFormat"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:103:38
        { ("SDL_TimeToDateTime", "dt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:131:38
        { ("SDL_DateTimeToTime", "dt"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:146:38
        { ("SDL_TimeToWindows", "dwLowDateTime"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:162:34
        { ("SDL_TimeToWindows", "dwHighDateTime"), PointerParameterIntent.Unknown }, // ./include/SDL3/SDL_time.h:162:34
    };

    internal static readonly Dictionary<string, DelegateDefinition> DelegateDefinitions = new()
    {
        {
            "SDL_EnumeratePropertiesCallback",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "userdata"), ("IntPtr", "props"), ("char*", "name")] }
        },
        {
            "SDL_AssertionHandler",
            new DelegateDefinition { ReturnType = "SDL_AssertState", Parameters = [("ref SDL_AssertData", "data"), ("IntPtr", "userdata")] }
        }, // ./include/SDL3/SDL_assert.h:423:35
        {
            "SDL_CleanupPropertyCallback",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "userdata"), ("IntPtr", "value")] }
        }, // ./include/SDL3/SDL_properties.h:187:24
        {
            "SDL_ThreadFunction",
            new DelegateDefinition { ReturnType = "int", Parameters = [("IntPtr", "data")] }
        }, // ./include/SDL3/SDL_thread.h:114:24
        {
            "SDL_TLSDestructorCallback",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "value")] }
        }, // ./include/SDL3/SDL_thread.h:488:24
        {
            "SDL_AudioStreamCallback",
            new DelegateDefinition
            {
                ReturnType = "void",
                Parameters = [("IntPtr", "userdata"), ("IntPtr", "stream"), ("int", "additionalAmount"), ("int", "totalAmount")],
            }
        }, // ./include/SDL3/SDL_audio.h:1527:24
        {
            "SDL_AudioPostmixCallback",
            new DelegateDefinition
            {
                ReturnType = "void",
                Parameters = [("IntPtr", "userdata"), ("ref SDL_AudioSpec", "spec"), ("ref float", "buffer"), ("int", "buflen")],
            }
        }, // ./include/SDL3/SDL_audio.h:1744:24
        {
            "SDL_EGLAttribArrayCallback", new DelegateDefinition { ReturnType = "IntPtr", Parameters = [] }
        }, // ./include/SDL3/SDL_video.h:246:34
        {
            "SDL_EGLIntArrayCallback", new DelegateDefinition { ReturnType = "int", Parameters = [] }
        }, // ./include/SDL3/SDL_video.h:247:31
        {
            "SDL_HitTest",
            new DelegateDefinition
            {
                ReturnType = "SDL_HitTestResult",
                Parameters = [("IntPtr", "win"), ("ref SDL_Point", "area"), ("IntPtr", "data")],
            }
        }, // ./include/SDL3/SDL_video.h:2313:37
        {
            "SDL_ClipboardDataCallback",
            new DelegateDefinition { ReturnType = "IntPtr", Parameters = [("IntPtr", "userdata"), ("IntPtr", "mimeType"), ("UIntPtr", "size")] }
        }, // ./include/SDL3/SDL_clipboard.h:155:31 // TODO: string marshalling (mimeType)
        {
            "SDL_ClipboardCleanupCallback", new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "userdata")] }
        }, // ./include/SDL3/SDL_clipboard.h:167:24
        {
            "SDL_DialogFileCallback",
            new DelegateDefinition
            {
                ReturnType = "void",
                Parameters = [("IntPtr", "userdata"), ("IntPtr", "filelist"), ("int", "filter")],
            }
        }, // ./include/SDL3/SDL_dialog.h:96:24
        {
            "SDL_EventFilter",
            new DelegateDefinition { ReturnType = "bool", Parameters = [("IntPtr", "userdata"), ("ref SDL_Event", "@event")] }
        }, // ./include/SDL3/SDL_events.h:1288:28
        {
            "SDL_EnumerateDirectoryCallback",
            new DelegateDefinition { ReturnType = "int", Parameters = [("IntPtr", "userdata"), ("IntPtr", "dirname"), ("IntPtr", "fname")] }
        }, // ./include/SDL3/SDL_filesystem.h:280:23 // TODO: string marshalling (dirname, fname)
        {
            "SDL_HintCallback",
            new DelegateDefinition
            {
                ReturnType = "void",
                Parameters = [("IntPtr", "userdata"), ("IntPtr", "name"), ("IntPtr", "oldValue"), ("IntPtr", "newValue")],
            }
        }, // ./include/SDL3/SDL_hints.h:4291:23 // TODO: string marshalling (name, oldValue newValue)
        {
            "SDL_AppInit_func",
            new DelegateDefinition
            {
                ReturnType = "SDL_AppResult",
                Parameters = [("ref IntPtr", "appstate"), ("int", "argc"), ("IntPtr", "argv")],
            }
        }, // ./include/SDL3/SDL_init.h:97:33 // TODO: string(s?) marshalling (argv array)
        {
            "SDL_AppIterate_func",
            new DelegateDefinition { ReturnType = "SDL_AppResult", Parameters = [("IntPtr", "appstate")] }
        }, // ./include/SDL3/SDL_init.h:98:33
        {
            "SDL_AppEvent_func",
            new DelegateDefinition { ReturnType = "SDL_AppResult", Parameters = [("IntPtr", "appstate"), ("ref SDL_Event", "@event")] }
        }, // ./include/SDL3/SDL_init.h:99:33
        {
            "SDL_AppQuit_func",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "appstate")] }
        }, // ./include/SDL3/SDL_init.h:100:24
        {
            "SDL_LogOutputFunction",
            new DelegateDefinition
            {
                ReturnType = "void",
                Parameters = [("IntPtr", "userdata"), ("int", "category"), ("SDL_LogPriority", "priority"), ("IntPtr", "message")],
            }
        }, // ./include/SDL3/SDL_log.h:411:24 // TODO: string marshalling (message)
        {
            "SDL_X11EventHook",
            new DelegateDefinition { ReturnType = "bool", Parameters = [("IntPtr", "userdata"), ("IntPtr", "xevent")] }
        }, // ./include/SDL3/SDL_system.h:139:28
        {
            "SDL_TimerCallback",
            new DelegateDefinition { ReturnType = "uint", Parameters = [("IntPtr", "userdata"), ("uint", "timerID"), ("uint", "interval")] }
        }, // ./include/SDL3/SDL_timer.h:158:26
        {
            "SDL_NSTimerCallback",
            new DelegateDefinition { ReturnType = "ulong", Parameters = [("IntPtr", "userdata"), ("uint", "timerID"), ("ulong", "interval")] }
        }, // ./include/SDL3/SDL_timer.h:222:2
    };

    internal static readonly Dictionary<string, string[]> FlagEnumDefinitions = new()
    {
        {
            "SDL_GPUTextureUsageFlags", [
                "Sampler",
                "ColorTarget",
                "DepthStencilTarget",
                "GraphicsStorageRead",
                "ComputeStorageRead",
                "ComputeStorageWrite",
            ]
        }, // ./include/SDL3/SDL_gpu.h:231:16
        {
            "SDL_SurfaceFlags", [
                "Preallocated",
                "LockNeeded",
                "Locked",
                "SIMDAligned",
            ]
        }, // ./include/SDL3/SDL_surface.h:52:16
        {
            "SDL_WindowFlags", [
                "Fullscreen",
                "OpenGL",
                "Occluded",
                "Hidden",
                "Borderless",
                "Resizable",
                "Minimized",
                "Maximized",
                "MouseGrabbed",
                "InputFocus",
                "MouseFocus",
                "External",
                "Modal",
                "HighPixelDensity",
                "MouseCapture",
                "MouseRelativeMode",
                "AlwaysOnTop",
                "Utility",
                "Tooltip",
                "PopupMenu",
                "KeyboardGrabbed",
                // unused bits between "KeyboardGrabbed" and "Vulkan"
                "Vulkan = 0x1000_0000",
                "Metal = 0x2000_0000",
                "Transparent = 0x4000_0000",
                "NotFocusable = 0x0_8000_0000",
            ]
        }, // ./include/SDL3/SDL_video.h:158:16
        {
            "SDL_MouseButtonFlags", [
                "LMask",
                "MMask",
                "RMask",
                "X1Mask",
                "X2Mask",
            ]
        }, // ./include/SDL3/SDL_mouse.h:118:16
        {
            "SDL_PenInputFlags", [
                "Down",
                "Button1",
                "Button2",
                "Button3",
                "Button4",
                "Button5",
                "EraserTip = 0x4000_0000",
            ]
        }, // ./include/SDL3/SDL_pen.h:68:16
        {
            "SDL_GlobFlags", [
                "CaseInsensitive",
            ]
        }, // ./include/SDL3/SDL_filesystem.h:261:16
        {
            "SDL_GPUBufferUsageFlags", [
                "Vertex",
                "Index",
                "Indirect",
                "GraphicsStorageRead",
                "ComputeStorageRead",
                "ComputeStorageWrite",
            ]
        }, // ./include/SDL3/SDL_gpu.h:266:16
        {
            "SDL_GPUColorComponentFlags", [
                "Red",
                "Green",
                "Blue",
                "Alpha",
            ]
        }, // ./include/SDL3/SDL_gpu.h:428:15
        {
            "SDL_InitFlags", [
                "Timer = 0x1",
                "Audio = 0x10",
                "Video = 0x20",
                "Joystick = 0x200",
                "Haptic = 0x1000",
                "Gamepad = 0x2000",
                "Events = 0x4000",
                "Sensor = 0x0_8000",
                "Camera = 0x1_0000",
            ]
        }, // ./include/SDL3/SDL_init.h:58:16
        {
            "SDL_MessageBoxFlags", [
                "Error = 0x10",
                "Warning = 0x20",
                "Information = 0x40",
                "ButtonsLeftToRight = 0x080",
                "ButtonsRightToLeft = 0x100",
            ]
        }, // ./include/SDL3/SDL_messagebox.h:48:16
        {
            "SDL_MessageBoxButtonFlags", [
                "ReturnKeyDefault",
                "EscapeKeyDefault",
            ]
        }, // ./include/SDL3/SDL_messagebox.h:61:16
    };

    internal static readonly string[] DeniedTypes =
    [
        "alloca",
    ];
}
