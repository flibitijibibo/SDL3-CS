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

    internal enum ReturnedCharPtrMemoryOwner
    {
        Unknown,
        SDL,
        Caller,
    }

    internal struct DelegateDefinition
    {
        public string ReturnType { get; set; }
        public (string, string)[] Parameters { get; set; }
    }

    internal static readonly Dictionary<(string, string), PointerParameterIntent> PointerParametersIntents = new()
    {
        { ("SDL_ReportAssertion", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_assert.h:245:45
        { ("SDL_GetAssertionHandler", "puserdata"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_assert.h:489:50
        { ("SDL_CompareAndSwapAtomicInt", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:348:34
        { ("SDL_SetAtomicInt", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:368:33
        { ("SDL_GetAtomicInt", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:385:33
        { ("SDL_AddAtomicInt", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:406:33
        { ("SDL_CompareAndSwapAtomicU32", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:490:34
        { ("SDL_SetAtomicU32", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:510:36
        { ("SDL_GetAtomicU32", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:527:36
        { ("SDL_CompareAndSwapAtomicPointer", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:548:34
        { ("SDL_SetAtomicPointer", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:567:36
        { ("SDL_GetAtomicPointer", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_atomic.h:585:36
        { ("SDL_WaitThread", "status"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_thread.h:423:34
        { ("SDL_ShouldInit", "state"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mutex.h:864:34
        { ("SDL_ShouldQuit", "state"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mutex.h:885:34
        { ("SDL_SetInitialized", "state"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mutex.h:904:34
        { ("SDL_OpenIO", "iface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:402:44
        { ("SDL_LoadFile_IO", "datasize"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:651:36
        { ("SDL_LoadFile", "datasize"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:671:36
        { ("SDL_ReadU8", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:690:34
        { ("SDL_ReadS8", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:702:34
        { ("SDL_ReadU16LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:718:34
        { ("SDL_ReadS16LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:734:34
        { ("SDL_ReadU16BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:750:34
        { ("SDL_ReadS16BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:766:34
        { ("SDL_ReadU32LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:782:34
        { ("SDL_ReadS32LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:798:34
        { ("SDL_ReadU32BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:814:34
        { ("SDL_ReadS32BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:830:34
        { ("SDL_ReadU64LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:846:34
        { ("SDL_ReadS64LE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:862:34
        { ("SDL_ReadU64BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:878:34
        { ("SDL_ReadS64BE", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_iostream.h:894:34
        { ("SDL_GetAudioPlaybackDevices", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:461:49
        { ("SDL_GetAudioRecordingDevices", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:490:49
        { ("SDL_GetAudioDeviceFormat", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:542:34
        { ("SDL_GetAudioDeviceFormat", "sample_frames"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:542:34
        { ("SDL_GetAudioDeviceChannelMap", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:565:35
        { ("SDL_OpenAudioDevice", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:641:47
        { ("SDL_BindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:838:34
        { ("SDL_UnbindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:879:34
        { ("SDL_CreateAudioStream", "src_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:937:47
        { ("SDL_CreateAudioStream", "dst_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:937:47
        { ("SDL_GetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:966:34
        { ("SDL_GetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:966:34
        { ("SDL_SetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:997:34
        { ("SDL_SetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:997:34
        { ("SDL_GetAudioStreamInputChannelMap", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1111:35
        { ("SDL_GetAudioStreamOutputChannelMap", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1135:35
        { ("SDL_SetAudioStreamInputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1186:34
        { ("SDL_SetAudioStreamOutputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1233:34
        { ("SDL_OpenAudioDeviceStream", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1707:47
        { ("SDL_LoadWAV_IO", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV_IO", "audio_buf"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV_IO", "audio_len"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_LoadWAV", "audio_buf"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_LoadWAV", "audio_len"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_MixAudio", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1951:34
        { ("SDL_MixAudio", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1951:34
        { ("SDL_ConvertAudioSamples", "src_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "src_data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_len"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_GetMasksForPixelFormat", "bpp"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Rmask"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Gmask"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Bmask"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Amask"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_SetPaletteColors", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:863:34
        { ("SDL_SetPaletteColors", "colors"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:863:34
        { ("SDL_DestroyPalette", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:877:34
        { ("SDL_MapRGB", "format"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:915:36
        { ("SDL_MapRGB", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:915:36
        { ("SDL_MapRGBA", "format"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:954:36
        { ("SDL_MapRGBA", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:954:36
        { ("SDL_GetRGB", "format"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGBA", "format"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_RectToFRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:126:23
        { ("SDL_RectToFRect", "frect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:126:23
        { ("SDL_PointInRect", "p"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:155:23
        { ("SDL_PointInRect", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:155:23
        { ("SDL_RectEmpty", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:179:23
        { ("SDL_RectsEqual", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:203:23
        { ("SDL_RectsEqual", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:203:23
        { ("SDL_HasRectIntersection", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:224:34
        { ("SDL_HasRectIntersection", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:224:34
        { ("SDL_GetRectIntersection", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectIntersection", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectIntersection", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectUnion", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectUnion", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectUnion", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectEnclosingPoints", "points"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectEnclosingPoints", "clip"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectEnclosingPoints", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectAndLineIntersection", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "X1"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "Y1"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "X2"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "Y2"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_PointInRectFloat", "p"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:320:23
        { ("SDL_PointInRectFloat", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:320:23
        { ("SDL_RectEmptyFloat", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:344:23
        { ("SDL_RectsEqualEpsilon", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:374:23
        { ("SDL_RectsEqualEpsilon", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:374:23
        { ("SDL_RectsEqualFloat", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:409:23
        { ("SDL_RectsEqualFloat", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:409:23
        { ("SDL_HasRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:427:34
        { ("SDL_HasRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:427:34
        { ("SDL_GetRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectIntersectionFloat", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectUnionFloat", "A"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectUnionFloat", "B"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectUnionFloat", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectEnclosingPointsFloat", "points"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectEnclosingPointsFloat", "clip"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectEnclosingPointsFloat", "result"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectAndLineIntersectionFloat", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "X1"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "Y1"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "X2"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "Y2"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_DestroySurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:183:34
        { ("SDL_GetSurfaceProperties", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:211:46
        { ("SDL_SetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:233:34
        { ("SDL_GetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:250:44
        { ("SDL_CreateSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:278:43
        { ("SDL_SetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:295:34
        { ("SDL_SetSurfacePalette", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:295:34
        { ("SDL_GetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:308:43
        { ("SDL_AddSurfaceAlternateImage", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:333:34
        { ("SDL_AddSurfaceAlternateImage", "image"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:333:34
        { ("SDL_SurfaceHasAlternateImages", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:347:34
        { ("SDL_GetSurfaceImages", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:372:44
        { ("SDL_GetSurfaceImages", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:372:44
        { ("SDL_RemoveSurfaceAlternateImages", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:388:34
        { ("SDL_LockSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:411:34
        { ("SDL_UnlockSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:422:34
        { ("SDL_SaveBMP_IO", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:483:34
        { ("SDL_SaveBMP", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:504:34
        { ("SDL_SetSurfaceRLE", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:523:34
        { ("SDL_SurfaceHasRLE", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:537:34
        { ("SDL_SetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:561:34
        { ("SDL_SurfaceHasColorKey", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:576:34
        { ("SDL_GetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:596:34
        { ("SDL_GetSurfaceColorKey", "key"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:596:34
        { ("SDL_SetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:619:34
        { ("SDL_GetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_SetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:657:34
        { ("SDL_GetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:672:34
        { ("SDL_GetSurfaceAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:672:34
        { ("SDL_SetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:690:34
        { ("SDL_GetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:704:34
        { ("SDL_SetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:725:34
        { ("SDL_SetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:725:34
        { ("SDL_GetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:744:34
        { ("SDL_GetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:744:34
        { ("SDL_FlipSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:756:34
        { ("SDL_DuplicateSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:774:43
        { ("SDL_ScaleSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:793:43
        { ("SDL_ConvertSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:819:43
        { ("SDL_ConvertSurfaceAndColorspace", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:846:43
        { ("SDL_ConvertSurfaceAndColorspace", "palette"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:846:43
        { ("SDL_PremultiplySurfaceAlpha", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:932:34
        { ("SDL_ClearSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:952:34
        { ("SDL_FillSurfaceRect", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:977:34
        { ("SDL_FillSurfaceRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:977:34
        { ("SDL_FillSurfaceRects", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1002:34
        { ("SDL_FillSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1002:34
        { ("SDL_BlitSurface", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurfaceUnchecked", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceScaled", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceUncheckedScaled", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceTiled", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiledWithScale", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurface9Grid", "src"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "dst"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_MapSurfaceRGB", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1276:36
        { ("SDL_MapSurfaceRGBA", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1307:36
        { ("SDL_ReadSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_WriteSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1381:34
        { ("SDL_WriteSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1401:34
        { ("SDL_GetCameras", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:183:44
        { ("SDL_GetCameraSupportedFormats", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:222:47
        { ("SDL_OpenCamera", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:302:42
        { ("SDL_GetCameraFormat", "spec"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:388:34
        { ("SDL_AcquireCameraFrame", "timestampNS"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:431:43
        { ("SDL_ReleaseCameraFrame", "frame"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:459:34
        { ("SDL_GetClipboardData", "size"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_clipboard.h:227:36
        { ("SDL_GetClipboardMimeTypes", "num_mime_types"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_clipboard.h:256:37
        { ("SDL_GetDisplays", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:415:45
        { ("SDL_GetDisplayBounds", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:486:34
        { ("SDL_GetDisplayUsableBounds", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:510:34
        { ("SDL_GetFullscreenDisplayModes", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:580:48
        { ("SDL_GetClosestFullscreenDisplayMode", "mode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:609:34
        { ("SDL_GetDisplayForPoint", "point"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:661:43
        { ("SDL_GetDisplayForRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:676:43
        { ("SDL_SetWindowFullscreenMode", "mode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:763:34
        { ("SDL_GetWindowICCProfile", "size"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:790:36
        { ("SDL_GetWindows", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:816:43
        { ("SDL_SetWindowIcon", "icon"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1366:34
        { ("SDL_GetWindowPosition", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1429:34
        { ("SDL_GetWindowPosition", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1429:34
        { ("SDL_GetWindowSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1486:34
        { ("SDL_GetWindowSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1486:34
        { ("SDL_GetWindowSafeArea", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1506:34
        { ("SDL_GetWindowAspectRatio", "min_aspect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1562:34
        { ("SDL_GetWindowAspectRatio", "max_aspect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1562:34
        { ("SDL_GetWindowBordersSize", "top"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "left"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "bottom"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "right"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowSizeInPixels", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1615:34
        { ("SDL_GetWindowSizeInPixels", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1615:34
        { ("SDL_GetWindowMinimumSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1649:34
        { ("SDL_GetWindowMinimumSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1649:34
        { ("SDL_GetWindowMaximumSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1683:34
        { ("SDL_GetWindowMaximumSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1683:34
        { ("SDL_GetWindowSurfaceVSync", "vsync"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2006:34
        { ("SDL_UpdateWindowSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2052:34
        { ("SDL_SetWindowMouseRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2169:34
        { ("SDL_SetWindowShape", "shape"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2396:34
        { ("SDL_GL_GetAttribute", "value"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2648:34
        { ("SDL_GL_GetSwapInterval", "interval"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_video.h:2813:34
        { ("SDL_ShowOpenFileDialog", "filters"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_dialog.h:154:34
        { ("SDL_ShowSaveFileDialog", "filters"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_dialog.h:209:34
        { ("SDL_GetPowerInfo", "seconds"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_power.h:85:44
        { ("SDL_GetPowerInfo", "percent"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_power.h:85:44
        { ("SDL_GetSensors", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_sensor.h:151:44
        { ("SDL_GetSensorData", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_sensor.h:280:34
        { ("SDL_GetJoysticks", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:215:46
        { ("SDL_AttachVirtualJoystick", "desc"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:482:44
        { ("SDL_SendJoystickVirtualSensorData", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:635:34
        { ("SDL_GetJoystickGUIDInfo", "vendor"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "product"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "version"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "crc16"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickAxisInitialState", "state"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:1013:34
        { ("SDL_GetJoystickBall", "dx"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:1034:34
        { ("SDL_GetJoystickBall", "dy"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:1034:34
        { ("SDL_GetJoystickPowerInfo", "percent"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:1200:44
        { ("SDL_GetGamepadMappings", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:403:37
        { ("SDL_GetGamepads", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:482:46
        { ("SDL_GetGamepadPowerInfo", "percent"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:936:44
        { ("SDL_GetGamepadBindings", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1011:51
        { ("SDL_GetGamepadTouchpadFinger", "down"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "pressure"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadSensorData", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1340:34
        { ("SDL_GetKeyboards", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:89:46
        { ("SDL_GetKeyboardState", "numkeys"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:144:42
        { ("SDL_SetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:499:34
        { ("SDL_GetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:518:34
        { ("SDL_GetTextInputArea", "cursor"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:518:34
        { ("SDL_GetMice", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:167:43
        { ("SDL_GetMouseState", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:213:50
        { ("SDL_GetMouseState", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:213:50
        { ("SDL_GetGlobalMouseState", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:243:50
        { ("SDL_GetGlobalMouseState", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:243:50
        { ("SDL_GetRelativeMouseState", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:262:50
        { ("SDL_GetRelativeMouseState", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:262:50
        { ("SDL_CreateCursor", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateCursor", "mask"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateColorCursor", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:460:42
        { ("SDL_GetTouchDevices", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_touch.h:93:43
        { ("SDL_GetTouchFingers", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_touch.h:129:43
        { ("SDL_PeepEvents", "events"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1047:33
        { ("SDL_PollEvent", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1178:34
        { ("SDL_WaitEvent", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1200:34
        { ("SDL_WaitEventTimeout", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1228:34
        { ("SDL_PushEvent", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1262:34
        { ("SDL_GetEventFilter", "userdata"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1348:34
        { ("SDL_GetWindowFromEvent", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_events.h:1465:42
        { ("SDL_GetPathInfo", "info"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_filesystem.h:413:34
        { ("SDL_GlobDirectory", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_filesystem.h:446:37
        { ("SDL_CreateGPUComputePipeline", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:1946:53
        { ("SDL_CreateGPUGraphicsPipeline", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:1965:54
        { ("SDL_CreateGPUSampler", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:1984:45
        { ("SDL_CreateGPUShader", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2053:44
        { ("SDL_CreateGPUTexture", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2088:45
        { ("SDL_CreateGPUBuffer", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2122:44
        { ("SDL_CreateGPUTransferBuffer", "createinfo"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2144:52
        { ("SDL_BeginGPURenderPass", "color_target_infos"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2493:48
        { ("SDL_BeginGPURenderPass", "depth_stencil_target_info"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2493:48
        { ("SDL_SetGPUViewport", "viewport"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2521:34
        { ("SDL_SetGPUScissor", "scissor"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2533:34
        { ("SDL_BindGPUVertexBuffers", "bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2576:34
        { ("SDL_BindGPUIndexBuffer", "binding"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2593:34
        { ("SDL_BindGPUVertexSamplers", "texture_sampler_bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2612:34
        { ("SDL_BindGPUVertexStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2631:34
        { ("SDL_BindGPUVertexStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2650:34
        { ("SDL_BindGPUFragmentSamplers", "texture_sampler_bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2670:34
        { ("SDL_BindGPUFragmentStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2689:34
        { ("SDL_BindGPUFragmentStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2708:34
        { ("SDL_BeginGPUComputePass", "storage_texture_bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2866:49
        { ("SDL_BeginGPUComputePass", "storage_buffer_bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2866:49
        { ("SDL_BindGPUComputeSamplers", "texture_sampler_bindings"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2899:34
        { ("SDL_BindGPUComputeStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2918:34
        { ("SDL_BindGPUComputeStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:2937:34
        { ("SDL_UploadToGPUTexture", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3071:34
        { ("SDL_UploadToGPUTexture", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3071:34
        { ("SDL_UploadToGPUBuffer", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3093:34
        { ("SDL_UploadToGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3093:34
        { ("SDL_CopyGPUTextureToTexture", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3116:34
        { ("SDL_CopyGPUTextureToTexture", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3116:34
        { ("SDL_CopyGPUBufferToBuffer", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3142:34
        { ("SDL_CopyGPUBufferToBuffer", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3142:34
        { ("SDL_DownloadFromGPUTexture", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3162:34
        { ("SDL_DownloadFromGPUTexture", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3162:34
        { ("SDL_DownloadFromGPUBuffer", "source"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3179:34
        { ("SDL_DownloadFromGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3179:34
        { ("SDL_BlitGPUTexture", "info"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3218:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture_width"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture_height"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_WaitForGPUFences", "fences"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:3466:34
        { ("SDL_GetHaptics", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:943:44
        { ("SDL_HapticEffectSupported", "effect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:1167:34
        { ("SDL_CreateHapticEffect", "effect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:1184:33
        { ("SDL_UpdateHapticEffect", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:1206:34
        { ("SDL_hid_free_enumeration", "devs"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:252:34
        { ("SDL_hid_write", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:311:33
        { ("SDL_hid_read_timeout", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:332:33
        { ("SDL_hid_read", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:353:33
        { ("SDL_hid_send_feature_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:397:33
        { ("SDL_hid_get_feature_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:420:33
        { ("SDL_hid_get_input_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:443:33
        { ("SDL_hid_get_report_descriptor", "buf"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:535:33
        { ("SDL_GetPreferredLocales", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_locale.h:101:43
        { ("SDL_GetLogOutputFunction", "userdata"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_log.h:490:34
        { ("SDL_ShowMessageBox", "messageboxdata"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_messagebox.h:164:34
        { ("SDL_ShowMessageBox", "buttonid"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_messagebox.h:164:34
        { ("SDL_ReadProcess", "datasize"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_process.h:283:36
        { ("SDL_ReadProcess", "exitcode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_process.h:283:36
        { ("SDL_WaitProcess", "exitcode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_process.h:383:34
        { ("SDL_CreateWindowAndRenderer", "window"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:194:34
        { ("SDL_CreateWindowAndRenderer", "renderer"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:194:34
        { ("SDL_CreateSoftwareRenderer", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:312:44
        { ("SDL_GetRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:479:34
        { ("SDL_GetRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:479:34
        { ("SDL_GetCurrentRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:501:34
        { ("SDL_GetCurrentRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:501:34
        { ("SDL_CreateTextureFromSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:555:43
        { ("SDL_GetTextureSize", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:848:34
        { ("SDL_GetTextureSize", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:848:34
        { ("SDL_GetTextureColorMod", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorMod", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorMod", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorModFloat", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureColorModFloat", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureColorModFloat", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1018:34
        { ("SDL_GetTextureAlphaModFloat", "alpha"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1036:34
        { ("SDL_GetTextureScaleMode", "scaleMode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1107:34
        { ("SDL_UpdateTexture", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1141:34
        { ("SDL_UpdateYUVTexture", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Yplane"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Uplane"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Vplane"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateNVTexture", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_UpdateNVTexture", "Yplane"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_UpdateNVTexture", "UVplane"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_LockTexture", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTexture", "pixels"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTexture", "pitch"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTextureToSurface", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1278:34
        { ("SDL_LockTextureToSurface", "surface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1278:34
        { ("SDL_GetRenderLogicalPresentation", "w"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentation", "h"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentation", "mode"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentationRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1422:34
        { ("SDL_RenderCoordinatesFromWindow", "x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1442:34
        { ("SDL_RenderCoordinatesFromWindow", "y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1442:34
        { ("SDL_RenderCoordinatesToWindow", "window_x"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1464:34
        { ("SDL_RenderCoordinatesToWindow", "window_y"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1464:34
        { ("SDL_ConvertEventToRenderCoordinates", "event"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1485:34
        { ("SDL_SetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1509:34
        { ("SDL_GetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1526:34
        { ("SDL_GetRenderSafeArea", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1568:34
        { ("SDL_SetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1586:34
        { ("SDL_GetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1604:34
        { ("SDL_GetRenderScale", "scaleX"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1662:34
        { ("SDL_GetRenderScale", "scaleY"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1662:34
        { ("SDL_GetRenderDrawColor", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColorFloat", "r"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "g"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "b"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "a"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderColorScale", "scale"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1800:34
        { ("SDL_RenderPoints", "points"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1888:34
        { ("SDL_RenderLines", "points"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1925:34
        { ("SDL_RenderRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1942:34
        { ("SDL_RenderRects", "rects"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1960:34
        { ("SDL_RenderFillRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1978:34
        { ("SDL_RenderFillRects", "rects"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:1996:34
        { ("SDL_RenderTexture", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2018:34
        { ("SDL_RenderTexture", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2018:34
        { ("SDL_RenderTextureRotated", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureRotated", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureRotated", "center"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureTiled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2076:34
        { ("SDL_RenderTextureTiled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2076:34
        { ("SDL_RenderTexture9Grid", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2110:34
        { ("SDL_RenderTexture9Grid", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2110:34
        { ("SDL_RenderGeometry", "vertices"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2134:34
        { ("SDL_RenderGeometry", "indices"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2134:34
        { ("SDL_RenderGeometryRaw", "xy"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderGeometryRaw", "color"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderGeometryRaw", "uv"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderReadPixels", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2193:43
        { ("SDL_GetRenderVSync", "vsync"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_render.h:2426:34
        { ("SDL_OpenStorage", "iface"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_storage.h:215:43
        { ("SDL_GetStorageFileSize", "length"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_storage.h:263:34
        { ("SDL_GetStoragePathInfo", "info"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_storage.h:398:34
        { ("SDL_GlobStorageDirectory", "count"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_storage.h:448:37
        { ("SDL_GetDateTimeLocalePreferences", "dateFormat"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:103:34
        { ("SDL_GetDateTimeLocalePreferences", "timeFormat"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:103:34
        { ("SDL_TimeToDateTime", "dt"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:131:34
        { ("SDL_DateTimeToTime", "dt"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:146:34
        { ("SDL_TimeToWindows", "dwLowDateTime"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:162:34
        { ("SDL_TimeToWindows", "dwHighDateTime"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_time.h:162:34
    };

    internal static readonly Dictionary<string, ReturnedCharPtrMemoryOwner> ReturnedCharPtrMemoryOwners = new()
    {
        { "SDL_GetError", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_error.h:112:42
        { "SDL_GetStringProperty", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_properties.h:400:42
        { "SDL_GetThreadName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_thread.h:338:42
        { "SDL_GetAudioDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:415:42
        { "SDL_GetCurrentAudioDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:432:42
        { "SDL_GetAudioDeviceName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:507:42
        { "SDL_GetAudioFormatName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_audio.h:1994:42
        { "SDL_GetPixelFormatName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_pixels.h:767:42
        { "SDL_GetCameraDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:150:42
        { "SDL_GetCurrentCameraDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:166:42
        { "SDL_GetCameraName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_camera.h:237:42
        { "SDL_GetClipboardText", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_clipboard.h:74:36
        { "SDL_GetPrimarySelectionText", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_clipboard.h:117:36
        { "SDL_GetVideoDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_video.h:376:42
        { "SDL_GetCurrentVideoDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_video.h:393:42
        { "SDL_GetDisplayName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_video.h:469:42
        { "SDL_GetWindowTitle", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_video.h:1344:42
        { "SDL_GetSensorNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_sensor.h:163:42
        { "SDL_GetSensorName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_sensor.h:233:42
        { "SDL_GetJoystickNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:231:42
        { "SDL_GetJoystickPathForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:247:42
        { "SDL_GetJoystickName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:678:42
        { "SDL_GetJoystickPath", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:691:42
        { "SDL_GetJoystickSerial", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_joystick.h:806:42
        { "SDL_GetGamepadMappingForGUID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:418:36
        { "SDL_GetGamepadMapping", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:437:36
        { "SDL_GetGamepadNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:512:42
        { "SDL_GetGamepadPathForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:528:42
        { "SDL_GetGamepadMappingForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:658:36
        { "SDL_GetGamepadName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:756:42
        { "SDL_GetGamepadPath", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:770:42
        { "SDL_GetGamepadSerial", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:891:42
        { "SDL_GetGamepadStringForType", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1054:42
        { "SDL_GetGamepadStringForAxis", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1090:42
        { "SDL_GetGamepadStringForButton", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1163:42
        { "SDL_GetGamepadAppleSFSymbolsNameForButton", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1449:42
        { "SDL_GetGamepadAppleSFSymbolsNameForAxis", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gamepad.h:1462:42
        { "SDL_GetKeyboardNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:104:42
        { "SDL_GetScancodeName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:268:42
        { "SDL_GetKeyName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_keyboard.h:299:42
        { "SDL_GetMouseNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_mouse.h:182:42
        { "SDL_GetTouchDeviceName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_touch.h:104:42
        { "SDL_GetBasePath", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_filesystem.h:80:42
        { "SDL_GetPrefPath", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_filesystem.h:135:36
        { "SDL_GetUserFolder", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_filesystem.h:217:42
        { "SDL_GetGPUDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:1874:42
        { "SDL_GetGPUDeviceDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_gpu.h:1884:42
        { "SDL_GetHapticNameForID", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:960:42
        { "SDL_GetHapticName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_haptic.h:1022:42
        { "SDL_GetHint", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_hints.h:4151:41
        { "SDL_GetAppMetadataProperty", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_init.h:352:42
        { "SDL_GetPlatform", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_platform.h:56:42
        { "SDL_GetRenderDriver", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_render.h:172:42
        { "SDL_GetRendererName", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_render.h:354:42
        { "SDL_GetRevision", ReturnedCharPtrMemoryOwner.Unknown }, // /usr/local/include/SDL3/SDL_version.h:172:42
    };

    internal static readonly Dictionary<string, DelegateDefinition> DelegateDefinitions = new()
    {
        { "SDL_AssertionHandler", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_assert.h:423:35
        { "SDL_CleanupPropertyCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_properties.h:187:24
        { "SDL_EnumeratePropertiesCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_properties.h:499:24
        { "SDL_ThreadFunction", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_thread.h:113:24
        { "SDL_TLSDestructorCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_thread.h:487:24
        { "SDL_AudioStreamCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_audio.h:1527:24
        { "SDL_AudioPostmixCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_audio.h:1744:24
        { "SDL_ClipboardDataCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_clipboard.h:154:31
        { "SDL_ClipboardCleanupCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_clipboard.h:166:24
        { "SDL_EGLAttribArrayCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_video.h:246:34
        { "SDL_EGLIntArrayCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_video.h:247:31
        { "SDL_HitTest", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_video.h:2328:37
        { "SDL_DialogFileCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_dialog.h:97:24
        { "SDL_EventFilter", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_events.h:1283:24
        { "SDL_EnumerateDirectoryCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_filesystem.h:302:41
        { "SDL_HintCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_hints.h:4189:23
        { "SDL_AppInit_func", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_init.h:96:33
        { "SDL_AppIterate_func", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_init.h:97:33
        { "SDL_AppEvent_func", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_init.h:98:33
        { "SDL_AppQuit_func", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_init.h:99:24
        { "SDL_LogOutputFunction", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_log.h:474:24
        { "SDL_X11EventHook", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_system.h:133:24
        { "SDL_TimerCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_timer.h:158:26
        { "SDL_NSTimerCallback", new DelegateDefinition { ReturnType = "WARN_PLACEHOLDER", Parameters = [] } }, // /usr/local/include/SDL3/SDL_timer.h:220:26
    };

    internal static readonly Dictionary<string, string[]> FlagEnumDefinitions = new()
    {
        {
            "SDL_SurfaceFlags", [
                "SDL_SURFACE_PREALLOCATED",
                "SDL_SURFACE_LOCK_NEEDED",
                "SDL_SURFACE_LOCKED",
                "SDL_SURFACE_SIMD_ALIGNED",
            ]
        }, // ./include/SDL3/SDL_surface.h:52:16
        {
            "SDL_WindowFlags", [
                "SDL_WINDOW_FULLSCREEN",
                "SDL_WINDOW_OPENGL",
                "SDL_WINDOW_OCCLUDED",
                "SDL_WINDOW_HIDDEN",
                "SDL_WINDOW_BORDERLESS",
                "SDL_WINDOW_RESIZABLE",
                "SDL_WINDOW_MINIMIZED",
                "SDL_WINDOW_MAXIMIZED",
                "SDL_WINDOW_MOUSE_GRABBED",
                "SDL_WINDOW_INPUT_FOCUS",
                "SDL_WINDOW_MOUSE_FOCUS",
                "SDL_WINDOW_EXTERNAL",
                "SDL_WINDOW_MODAL",
                "SDL_WINDOW_HIGH_PIXEL_DENSITY",
                "SDL_WINDOW_MOUSE_CAPTURE",
                "SDL_WINDOW_MOUSE_RELATIVE_MODE",
                "SDL_WINDOW_ALWAYS_ON_TOP",
                "SDL_WINDOW_UTILITY",
                "SDL_WINDOW_TOOLTIP",
                "SDL_WINDOW_POPUP_MENU",
                "SDL_WINDOW_KEYBOARD_GRABBED",
                // unused bits between "KeyboardGrabbed" and "Vulkan"
                "SDL_WINDOW_VULKAN = 0x1000_0000",
                "SDL_WINDOW_METAL = 0x2000_0000",
                "SDL_WINDOW_TRANSPARENT = 0x4000_0000",
                "SDL_WINDOW_NOT_FOCUSABLE = 0x0_8000_0000",
            ]
        }, // ./include/SDL3/SDL_video.h:158:16
        {
            "SDL_MouseButtonFlags", [
                "SDL_BUTTON_LMASK",
                "SDL_BUTTON_MMASK",
                "SDL_BUTTON_RMASK",
                "SDL_BUTTON_X1MASK",
                "SDL_BUTTON_X2MASK",
            ]
        }, // ./include/SDL3/SDL_mouse.h:118:16
        {
            "SDL_PenInputFlags", [
                "SDL_PEN_INPUT_DOWN",
                "SDL_PEN_INPUT_BUTTON_1",
                "SDL_PEN_INPUT_BUTTON_2",
                "SDL_PEN_INPUT_BUTTON_3",
                "SDL_PEN_INPUT_BUTTON_4",
                "SDL_PEN_INPUT_BUTTON_5",
                "SDL_PEN_INPUT_ERASER_TIP = 0x4000_0000",
            ]
        }, // ./include/SDL3/SDL_pen.h:68:16
        {
            "SDL_GlobFlags", [
                "SDL_GLOB_CASEINSENSITIVE",
            ]
        }, // ./include/SDL3/SDL_filesystem.h:261:16
        {
            "SDL_GPUTextureUsageFlags", [
                "SDL_GPU_TEXTUREUSAGE_SAMPLER",
                "SDL_GPU_TEXTUREUSAGE_COLOR_TARGET",
                "SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET",
                "SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ",
                "SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ",
                "SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE",
            ]
        }, // ./include/SDL3/SDL_gpu.h:231:16
        {
            "SDL_GPUBufferUsageFlags", [
                "SDL_GPU_BUFFERUSAGE_VERTEX",
                "SDL_GPU_BUFFERUSAGE_INDEX",
                "SDL_GPU_BUFFERUSAGE_INDIRECT",
                "SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ",
                "SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ",
                "SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE",
            ]
        }, // ./include/SDL3/SDL_gpu.h:266:16
        {
            "SDL_GPUColorComponentFlags", [
                "SDL_GPU_COLORCOMPONENT_R",
                "SDL_GPU_COLORCOMPONENT_G",
                "SDL_GPU_COLORCOMPONENT_B",
                "SDL_GPU_COLORCOMPONENT_A",
            ]
        }, // ./include/SDL3/SDL_gpu.h:428:15
        {
            "SDL_InitFlags", [
                "SDL_INIT_TIMER = 0x1",
                "SDL_INIT_AUDIO = 0x10",
                "SDL_INIT_VIDEO = 0x20",
                "SDL_INIT_JOYSTICK = 0x200",
                "SDL_INIT_HAPTIC = 0x1000",
                "SDL_INIT_GAMEPAD = 0x2000",
                "SDL_INIT_EVENTS = 0x4000",
                "SDL_INIT_SENSOR = 0x0_8000",
                "SDL_INIT_CAMERA = 0x1_0000",
            ]
        }, // ./include/SDL3/SDL_init.h:58:16
        {
            "SDL_MessageBoxFlags", [
                "SDL_MESSAGEBOX_ERROR = 0x10",
                "SDL_MESSAGEBOX_WARNING = 0x20",
                "SDL_MESSAGEBOX_INFORMATION = 0x40",
                "SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x080",
                "SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x100",
            ]
        }, // ./include/SDL3/SDL_messagebox.h:48:16
        {
            "SDL_MessageBoxButtonFlags", [
                "SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT",
                "SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT",
            ]
        }, // ./include/SDL3/SDL_messagebox.h:61:16
        {
            "SDL_Keymod", [
                "SDL_KMOD_NONE = 0x0000",
                "SDL_KMOD_LSHIFT = 0x0001",
                "SDL_KMOD_RSHIFT = 0x0002",
                "SDL_KMOD_LCTRL = 0x0040",
                "SDL_KMOD_RCTRL = 0x0080",
                "SDL_KMOD_LALT = 0x0100",
                "SDL_KMOD_RALT = 0x0200",
                "SDL_KMOD_LGUI = 0x0400",
                "SDL_KMOD_RGUI = 0x0800",
                "SDL_KMOD_NUM = 0x1000",
                "SDL_KMOD_CAPS = 0x2000",
                "SDL_KMOD_MODE = 0x4000",
                "SDL_KMOD_SCROLL = 0x8000",
                "SDL_KMOD_CTRL = SDL_KMOD_LCTRL | SDL_KMOD_RCTRL",
                "SDL_KMOD_SHIFT = SDL_KMOD_LSHIFT | SDL_KMOD_RSHIFT",
                "SDL_KMOD_ALT = SDL_KMOD_RALT | SDL_KMOD_LALT",
                "SDL_KMOD_GUI = SDL_KMOD_RGUI | SDL_KMOD_LGUI",
            ]
        }, // ../SDL3/SDL_keycode.h:306:16
    };

    internal static readonly string[] DeniedTypes = [];
}
