namespace GenerateBindings;

internal static class UserProvidedData
{
    internal enum PointerParameterIntent
    {
        Unknown,
        IntPtr,
        Ref,
        Out,
        Array,
        Pointer
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
        { ("SDL_ReportAssertion", "data"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_assert.h:245:45
        { ("SDL_GetAssertionHandler", "puserdata"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_assert.h:489:50
        { ("SDL_CompareAndSwapAtomicInt", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:348:34
        { ("SDL_SetAtomicInt", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:368:33
        { ("SDL_GetAtomicInt", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:385:33
        { ("SDL_AddAtomicInt", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:406:33
        { ("SDL_CompareAndSwapAtomicU32", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:490:34
        { ("SDL_SetAtomicU32", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:510:36
        { ("SDL_GetAtomicU32", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:527:36
        { ("SDL_CompareAndSwapAtomicPointer", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:548:34
        { ("SDL_SetAtomicPointer", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:567:36
        { ("SDL_GetAtomicPointer", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_atomic.h:585:36
        { ("SDL_WaitThread", "status"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_thread.h:423:34
        { ("SDL_ShouldInit", "state"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_mutex.h:864:34
        { ("SDL_ShouldQuit", "state"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_mutex.h:885:34
        { ("SDL_SetInitialized", "state"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_mutex.h:904:34
        { ("SDL_OpenIO", "iface"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_iostream.h:402:44
        { ("SDL_LoadFile_IO", "datasize"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:651:36
        { ("SDL_LoadFile", "datasize"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:671:36
        { ("SDL_ReadU8", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:690:34
        { ("SDL_ReadS8", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:702:34
        { ("SDL_ReadU16LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:718:34
        { ("SDL_ReadS16LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:734:34
        { ("SDL_ReadU16BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:750:34
        { ("SDL_ReadS16BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:766:34
        { ("SDL_ReadU32LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:782:34
        { ("SDL_ReadS32LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:798:34
        { ("SDL_ReadU32BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:814:34
        { ("SDL_ReadS32BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:830:34
        { ("SDL_ReadU64LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:846:34
        { ("SDL_ReadS64LE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:862:34
        { ("SDL_ReadU64BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:878:34
        { ("SDL_ReadS64BE", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_iostream.h:894:34
        { ("SDL_GetAudioPlaybackDevices", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:461:49
        { ("SDL_GetAudioRecordingDevices", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:490:49
        { ("SDL_GetAudioDeviceFormat", "spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:542:34
        { ("SDL_GetAudioDeviceFormat", "sample_frames"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:542:34
        { ("SDL_GetAudioDeviceChannelMap", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:565:35
        { ("SDL_OpenAudioDevice", "spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:641:47
        { ("SDL_BindAudioStreams", "streams"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_audio.h:838:34
        { ("SDL_UnbindAudioStreams", "streams"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_audio.h:879:34
        { ("SDL_CreateAudioStream", "src_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:937:47
        { ("SDL_CreateAudioStream", "dst_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:937:47
        { ("SDL_GetAudioStreamFormat", "src_spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:966:34
        { ("SDL_GetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:966:34
        { ("SDL_SetAudioStreamFormat", "src_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:997:34
        { ("SDL_SetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:997:34
        { ("SDL_GetAudioStreamInputChannelMap", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1111:35
        { ("SDL_GetAudioStreamOutputChannelMap", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1135:35
        { ("SDL_SetAudioStreamInputChannelMap", "chmap"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_audio.h:1186:34
        { ("SDL_SetAudioStreamOutputChannelMap", "chmap"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_audio.h:1233:34
        { ("SDL_OpenAudioDeviceStream", "spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:1707:47
        { ("SDL_LoadWAV_IO", "spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV_IO", "audio_buf"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV_IO", "audio_len"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1879:34
        { ("SDL_LoadWAV", "spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_LoadWAV", "audio_buf"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_LoadWAV", "audio_len"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1915:34
        { ("SDL_MixAudio", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_audio.h:1951:34
        { ("SDL_MixAudio", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_audio.h:1951:34
        { ("SDL_ConvertAudioSamples", "src_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "src_data"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_data"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_ConvertAudioSamples", "dst_len"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_audio.h:1981:34
        { ("SDL_GetMasksForPixelFormat", "bpp"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Rmask"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Gmask"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Bmask"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_GetMasksForPixelFormat", "Amask"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:787:34
        { ("SDL_SetPaletteColors", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:863:34
        { ("SDL_SetPaletteColors", "colors"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_pixels.h:863:34
        { ("SDL_DestroyPalette", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:877:34
        { ("SDL_MapRGB", "format"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:915:36
        { ("SDL_MapRGB", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:915:36
        { ("SDL_MapRGBA", "format"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:954:36
        { ("SDL_MapRGBA", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:954:36
        { ("SDL_GetRGB", "format"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGB", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:982:34
        { ("SDL_GetRGBA", "format"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_GetRGBA", "a"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_pixels.h:1014:34
        { ("SDL_RectToFRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:126:23
        { ("SDL_RectToFRect", "frect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:126:23
        { ("SDL_PointInRect", "p"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:155:23
        { ("SDL_PointInRect", "r"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:155:23
        { ("SDL_RectEmpty", "r"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:179:23
        { ("SDL_RectsEqual", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:203:23
        { ("SDL_RectsEqual", "b"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:203:23
        { ("SDL_HasRectIntersection", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:224:34
        { ("SDL_HasRectIntersection", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:224:34
        { ("SDL_GetRectIntersection", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectIntersection", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectIntersection", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:241:34
        { ("SDL_GetRectUnion", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectUnion", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectUnion", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:255:34
        { ("SDL_GetRectEnclosingPoints", "points"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectEnclosingPoints", "clip"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectEnclosingPoints", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:274:34
        { ("SDL_GetRectAndLineIntersection", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "X1"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "Y1"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "X2"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_GetRectAndLineIntersection", "Y2"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:294:34
        { ("SDL_PointInRectFloat", "p"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:320:23
        { ("SDL_PointInRectFloat", "r"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:320:23
        { ("SDL_RectEmptyFloat", "r"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:344:23
        { ("SDL_RectsEqualEpsilon", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:374:23
        { ("SDL_RectsEqualEpsilon", "b"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:374:23
        { ("SDL_RectsEqualFloat", "a"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:409:23
        { ("SDL_RectsEqualFloat", "b"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:409:23
        { ("SDL_HasRectIntersectionFloat", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:427:34
        { ("SDL_HasRectIntersectionFloat", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:427:34
        { ("SDL_GetRectIntersectionFloat", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectIntersectionFloat", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectIntersectionFloat", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:444:34
        { ("SDL_GetRectUnionFloat", "A"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectUnionFloat", "B"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectUnionFloat", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:458:34
        { ("SDL_GetRectEnclosingPointsFloat", "points"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectEnclosingPointsFloat", "clip"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectEnclosingPointsFloat", "result"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_rect.h:478:34
        { ("SDL_GetRectAndLineIntersectionFloat", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "X1"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "Y1"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "X2"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_GetRectAndLineIntersectionFloat", "Y2"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_rect.h:499:34
        { ("SDL_DestroySurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:183:34
        { ("SDL_GetSurfaceProperties", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:211:46
        { ("SDL_SetSurfaceColorspace", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:233:34
        { ("SDL_GetSurfaceColorspace", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:250:44
        { ("SDL_CreateSurfacePalette", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:278:43
        { ("SDL_SetSurfacePalette", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:295:34
        { ("SDL_SetSurfacePalette", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:295:34
        { ("SDL_GetSurfacePalette", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:308:43
        { ("SDL_AddSurfaceAlternateImage", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:333:34
        { ("SDL_AddSurfaceAlternateImage", "image"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:333:34
        { ("SDL_SurfaceHasAlternateImages", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:347:34
        { ("SDL_GetSurfaceImages", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:372:44
        { ("SDL_GetSurfaceImages", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:372:44
        { ("SDL_RemoveSurfaceAlternateImages", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:388:34
        { ("SDL_LockSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:411:34
        { ("SDL_UnlockSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:422:34
        { ("SDL_SaveBMP_IO", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:483:34
        { ("SDL_SaveBMP", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:504:34
        { ("SDL_SetSurfaceRLE", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:523:34
        { ("SDL_SurfaceHasRLE", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:537:34
        { ("SDL_SetSurfaceColorKey", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:561:34
        { ("SDL_SurfaceHasColorKey", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:576:34
        { ("SDL_GetSurfaceColorKey", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:596:34
        { ("SDL_GetSurfaceColorKey", "key"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:596:34
        { ("SDL_SetSurfaceColorMod", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:619:34
        { ("SDL_GetSurfaceColorMod", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_GetSurfaceColorMod", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:637:34
        { ("SDL_SetSurfaceAlphaMod", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:657:34
        { ("SDL_GetSurfaceAlphaMod", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:672:34
        { ("SDL_GetSurfaceAlphaMod", "alpha"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:672:34
        { ("SDL_SetSurfaceBlendMode", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:690:34
        { ("SDL_GetSurfaceBlendMode", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:704:34
        { ("SDL_SetSurfaceClipRect", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:725:34
        { ("SDL_SetSurfaceClipRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_surface.h:725:34
        { ("SDL_GetSurfaceClipRect", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:744:34
        { ("SDL_GetSurfaceClipRect", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:744:34
        { ("SDL_FlipSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:756:34
        { ("SDL_DuplicateSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:774:43
        { ("SDL_ScaleSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:793:43
        { ("SDL_ConvertSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:819:43
        { ("SDL_ConvertSurfaceAndColorspace", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:846:43
        { ("SDL_ConvertSurfaceAndColorspace", "palette"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:846:43
        { ("SDL_PremultiplySurfaceAlpha", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:932:34
        { ("SDL_ClearSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:952:34
        { ("SDL_FillSurfaceRect", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:977:34
        { ("SDL_FillSurfaceRect", "rect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:977:34
        { ("SDL_FillSurfaceRects", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1002:34
        { ("SDL_FillSurfaceRects", "rects"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_surface.h:1002:34
        { ("SDL_BlitSurface", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurface", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1077:34
        { ("SDL_BlitSurfaceUnchecked", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceUnchecked", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1102:34
        { ("SDL_BlitSurfaceScaled", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceScaled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1127:34
        { ("SDL_BlitSurfaceUncheckedScaled", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceUncheckedScaled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1153:34
        { ("SDL_BlitSurfaceTiled", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiled", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1179:34
        { ("SDL_BlitSurfaceTiledWithScale", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurfaceTiledWithScale", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1209:34
        { ("SDL_BlitSurface9Grid", "src"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "srcrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "dst"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_BlitSurface9Grid", "dstrect"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_surface.h:1246:34
        { ("SDL_MapSurfaceRGB", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1276:36
        { ("SDL_MapSurfaceRGBA", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1307:36
        { ("SDL_ReadSurfacePixel", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixel", "a"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1334:34
        { ("SDL_ReadSurfacePixelFloat", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_ReadSurfacePixelFloat", "a"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_surface.h:1358:34
        { ("SDL_WriteSurfacePixel", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1381:34
        { ("SDL_WriteSurfacePixelFloat", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_surface.h:1401:34
        { ("SDL_GetCameras", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_camera.h:183:44
        { ("SDL_GetCameraSupportedFormats", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_camera.h:222:47
        { ("SDL_OpenCamera", "spec"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_camera.h:302:42
        { ("SDL_GetCameraFormat", "spec"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_camera.h:388:34
        { ("SDL_AcquireCameraFrame", "timestampNS"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_camera.h:431:43
        { ("SDL_ReleaseCameraFrame", "frame"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_camera.h:459:34
        { ("SDL_GetClipboardData", "size"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_clipboard.h:227:36
        { ("SDL_GetClipboardMimeTypes", "num_mime_types"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_clipboard.h:256:37
        { ("SDL_GetDisplays", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:415:45
        { ("SDL_GetDisplayBounds", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:486:34
        { ("SDL_GetDisplayUsableBounds", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:510:34
        { ("SDL_GetFullscreenDisplayModes", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:580:48
        { ("SDL_GetClosestFullscreenDisplayMode", "mode"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:609:34
        { ("SDL_GetDisplayForPoint", "point"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_video.h:661:43
        { ("SDL_GetDisplayForRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_video.h:676:43
        { ("SDL_SetWindowFullscreenMode", "mode"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_video.h:763:34
        { ("SDL_GetWindowICCProfile", "size"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:790:36
        { ("SDL_GetWindows", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:816:43
        { ("SDL_SetWindowIcon", "icon"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_video.h:1366:34
        { ("SDL_GetWindowPosition", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1429:34
        { ("SDL_GetWindowPosition", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1429:34
        { ("SDL_GetWindowSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1486:34
        { ("SDL_GetWindowSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1486:34
        { ("SDL_GetWindowSafeArea", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1506:34
        { ("SDL_GetWindowAspectRatio", "min_aspect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1562:34
        { ("SDL_GetWindowAspectRatio", "max_aspect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1562:34
        { ("SDL_GetWindowBordersSize", "top"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "left"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "bottom"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowBordersSize", "right"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1597:34
        { ("SDL_GetWindowSizeInPixels", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1615:34
        { ("SDL_GetWindowSizeInPixels", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1615:34
        { ("SDL_GetWindowMinimumSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1649:34
        { ("SDL_GetWindowMinimumSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1649:34
        { ("SDL_GetWindowMaximumSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1683:34
        { ("SDL_GetWindowMaximumSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:1683:34
        { ("SDL_GetWindowSurfaceVSync", "vsync"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:2006:34
        { ("SDL_UpdateWindowSurfaceRects", "rects"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_video.h:2052:34
        { ("SDL_SetWindowMouseRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_video.h:2169:34
        { ("SDL_SetWindowShape", "shape"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_video.h:2396:34
        { ("SDL_GL_GetAttribute", "value"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:2648:34
        { ("SDL_GL_GetSwapInterval", "interval"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_video.h:2813:34
        { ("SDL_ShowOpenFileDialog", "filters"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_dialog.h:154:34
        { ("SDL_ShowSaveFileDialog", "filters"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_dialog.h:209:34
        { ("SDL_GetPowerInfo", "seconds"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_power.h:85:44
        { ("SDL_GetPowerInfo", "percent"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_power.h:85:44
        { ("SDL_GetSensors", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_sensor.h:151:44
        { ("SDL_GetSensorData", "data"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_sensor.h:280:34
        { ("SDL_GetJoysticks", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:215:46
        { ("SDL_AttachVirtualJoystick", "desc"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_joystick.h:482:44
        { ("SDL_SendJoystickVirtualSensorData", "data"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_joystick.h:635:34
        { ("SDL_GetJoystickGUIDInfo", "vendor"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "product"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "version"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "crc16"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickAxisInitialState", "state"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:1013:34
        { ("SDL_GetJoystickBall", "dx"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:1034:34
        { ("SDL_GetJoystickBall", "dy"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:1034:34
        { ("SDL_GetJoystickPowerInfo", "percent"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_joystick.h:1200:44
        { ("SDL_GetGamepadMappings", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:403:37
        { ("SDL_GetGamepads", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:482:46
        { ("SDL_GetGamepadPowerInfo", "percent"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:936:44
        { ("SDL_GetGamepadBindings", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:1011:51
        { ("SDL_GetGamepadTouchpadFinger", "down"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadTouchpadFinger", "pressure"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gamepad.h:1268:34
        { ("SDL_GetGamepadSensorData", "data"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gamepad.h:1340:34
        { ("SDL_GetKeyboards", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_keyboard.h:89:46
        { ("SDL_GetKeyboardState", "numkeys"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_keyboard.h:144:42
        { ("SDL_SetTextInputArea", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_keyboard.h:499:34
        { ("SDL_GetTextInputArea", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_keyboard.h:518:34
        { ("SDL_GetTextInputArea", "cursor"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_keyboard.h:518:34
        { ("SDL_GetMice", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:167:43
        { ("SDL_GetMouseState", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:213:50
        { ("SDL_GetMouseState", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:213:50
        { ("SDL_GetGlobalMouseState", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:243:50
        { ("SDL_GetGlobalMouseState", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:243:50
        { ("SDL_GetRelativeMouseState", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:262:50
        { ("SDL_GetRelativeMouseState", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_mouse.h:262:50
        { ("SDL_CreateCursor", "data"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateCursor", "mask"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_mouse.h:429:42
        { ("SDL_CreateColorCursor", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_mouse.h:460:42
        { ("SDL_GetTouchDevices", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_touch.h:93:43
        { ("SDL_GetTouchFingers", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_touch.h:129:43
        { ("SDL_PeepEvents", "events"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_events.h:1047:33
        { ("SDL_PollEvent", "event"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_events.h:1178:34
        { ("SDL_WaitEvent", "event"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_events.h:1200:34
        { ("SDL_WaitEventTimeout", "event"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_events.h:1228:34
        { ("SDL_PushEvent", "event"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_events.h:1262:34
        { ("SDL_GetEventFilter", "userdata"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_events.h:1348:34
        { ("SDL_GetWindowFromEvent", "event"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_events.h:1465:42
        { ("SDL_GetPathInfo", "info"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_filesystem.h:413:34
        { ("SDL_GlobDirectory", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_filesystem.h:446:37
        { ("SDL_CreateGPUComputePipeline", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:1946:53
        { ("SDL_CreateGPUGraphicsPipeline", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:1965:54
        { ("SDL_CreateGPUSampler", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:1984:45
        { ("SDL_CreateGPUShader", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2053:44
        { ("SDL_CreateGPUTexture", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2088:45
        { ("SDL_CreateGPUBuffer", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2122:44
        { ("SDL_CreateGPUTransferBuffer", "createinfo"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2144:52
        { ("SDL_BeginGPURenderPass", "color_target_infos"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2493:48
        { ("SDL_BeginGPURenderPass", "depth_stencil_target_info"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2493:48
        { ("SDL_SetGPUViewport", "viewport"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2521:34
        { ("SDL_SetGPUScissor", "scissor"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2533:34
        { ("SDL_BindGPUVertexBuffers", "bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2576:34
        { ("SDL_BindGPUIndexBuffer", "binding"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:2593:34
        { ("SDL_BindGPUVertexSamplers", "texture_sampler_bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2612:34
        { ("SDL_BindGPUVertexStorageTextures", "storage_textures"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2631:34
        { ("SDL_BindGPUVertexStorageBuffers", "storage_buffers"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2650:34
        { ("SDL_BindGPUFragmentSamplers", "texture_sampler_bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2670:34
        { ("SDL_BindGPUFragmentStorageTextures", "storage_textures"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2689:34
        { ("SDL_BindGPUFragmentStorageBuffers", "storage_buffers"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2708:34
        { ("SDL_BeginGPUComputePass", "storage_texture_bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2866:49
        { ("SDL_BeginGPUComputePass", "storage_buffer_bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2866:49
        { ("SDL_BindGPUComputeSamplers", "texture_sampler_bindings"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2899:34
        { ("SDL_BindGPUComputeStorageTextures", "storage_textures"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2918:34
        { ("SDL_BindGPUComputeStorageBuffers", "storage_buffers"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:2937:34
        { ("SDL_UploadToGPUTexture", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3071:34
        { ("SDL_UploadToGPUTexture", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3071:34
        { ("SDL_UploadToGPUBuffer", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3093:34
        { ("SDL_UploadToGPUBuffer", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3093:34
        { ("SDL_CopyGPUTextureToTexture", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3116:34
        { ("SDL_CopyGPUTextureToTexture", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3116:34
        { ("SDL_CopyGPUBufferToBuffer", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3142:34
        { ("SDL_CopyGPUBufferToBuffer", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3142:34
        { ("SDL_DownloadFromGPUTexture", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3162:34
        { ("SDL_DownloadFromGPUTexture", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3162:34
        { ("SDL_DownloadFromGPUBuffer", "source"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3179:34
        { ("SDL_DownloadFromGPUBuffer", "destination"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3179:34
        { ("SDL_BlitGPUTexture", "info"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_gpu.h:3218:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture_width"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_AcquireGPUSwapchainTexture", "swapchain_texture_height"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_gpu.h:3380:34
        { ("SDL_WaitForGPUFences", "fences"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_gpu.h:3466:34
        { ("SDL_GetHaptics", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_haptic.h:943:44
        { ("SDL_HapticEffectSupported", "effect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_haptic.h:1167:34
        { ("SDL_CreateHapticEffect", "effect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_haptic.h:1184:33
        { ("SDL_UpdateHapticEffect", "data"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_haptic.h:1206:34
        { ("SDL_hid_free_enumeration", "devs"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:252:34
        { ("SDL_hid_write", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:311:33
        { ("SDL_hid_read_timeout", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:332:33
        { ("SDL_hid_read", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:353:33
        { ("SDL_hid_send_feature_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:397:33
        { ("SDL_hid_get_feature_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:420:33
        { ("SDL_hid_get_input_report", "data"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:443:33
        { ("SDL_hid_get_report_descriptor", "buf"), PointerParameterIntent.Unknown }, // /usr/local/include/SDL3/SDL_hidapi.h:535:33
        { ("SDL_GetPreferredLocales", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_locale.h:101:43
        { ("SDL_GetLogOutputFunction", "userdata"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_log.h:490:34
        { ("SDL_ShowMessageBox", "messageboxdata"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_messagebox.h:164:34
        { ("SDL_ShowMessageBox", "buttonid"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_messagebox.h:164:34
        { ("SDL_ReadProcess", "datasize"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_process.h:283:36
        { ("SDL_ReadProcess", "exitcode"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_process.h:283:36
        { ("SDL_WaitProcess", "exitcode"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_process.h:383:34
        { ("SDL_CreateWindowAndRenderer", "window"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:194:34
        { ("SDL_CreateWindowAndRenderer", "renderer"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:194:34
        { ("SDL_CreateSoftwareRenderer", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:312:44
        { ("SDL_GetRenderOutputSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:479:34
        { ("SDL_GetRenderOutputSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:479:34
        { ("SDL_GetCurrentRenderOutputSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:501:34
        { ("SDL_GetCurrentRenderOutputSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:501:34
        { ("SDL_CreateTextureFromSurface", "surface"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:555:43
        { ("SDL_GetTextureSize", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:848:34
        { ("SDL_GetTextureSize", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:848:34
        { ("SDL_GetTextureColorMod", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorMod", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorMod", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:928:34
        { ("SDL_GetTextureColorModFloat", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureColorModFloat", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureColorModFloat", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:948:34
        { ("SDL_GetTextureAlphaMod", "alpha"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1018:34
        { ("SDL_GetTextureAlphaModFloat", "alpha"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1036:34
        { ("SDL_GetTextureScaleMode", "scaleMode"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1107:34
        { ("SDL_UpdateTexture", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1141:34
        { ("SDL_UpdateYUVTexture", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Yplane"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Uplane"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateYUVTexture", "Vplane"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:1173:34
        { ("SDL_UpdateNVTexture", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_UpdateNVTexture", "Yplane"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_UpdateNVTexture", "UVplane"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:1205:34
        { ("SDL_LockTexture", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTexture", "pixels"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTexture", "pitch"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1240:34
        { ("SDL_LockTextureToSurface", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1278:34
        { ("SDL_LockTextureToSurface", "surface"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1278:34
        { ("SDL_GetRenderLogicalPresentation", "w"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentation", "h"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentation", "mode"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1400:34
        { ("SDL_GetRenderLogicalPresentationRect", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1422:34
        { ("SDL_RenderCoordinatesFromWindow", "x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1442:34
        { ("SDL_RenderCoordinatesFromWindow", "y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1442:34
        { ("SDL_RenderCoordinatesToWindow", "window_x"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1464:34
        { ("SDL_RenderCoordinatesToWindow", "window_y"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1464:34
        { ("SDL_ConvertEventToRenderCoordinates", "event"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1485:34
        { ("SDL_SetRenderViewport", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1509:34
        { ("SDL_GetRenderViewport", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1526:34
        { ("SDL_GetRenderSafeArea", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1568:34
        { ("SDL_SetRenderClipRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1586:34
        { ("SDL_GetRenderClipRect", "rect"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1604:34
        { ("SDL_GetRenderScale", "scaleX"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1662:34
        { ("SDL_GetRenderScale", "scaleY"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1662:34
        { ("SDL_GetRenderDrawColor", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColor", "a"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1736:34
        { ("SDL_GetRenderDrawColorFloat", "r"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "g"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "b"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderDrawColorFloat", "a"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1760:34
        { ("SDL_GetRenderColorScale", "scale"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:1800:34
        { ("SDL_RenderPoints", "points"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:1888:34
        { ("SDL_RenderLines", "points"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:1925:34
        { ("SDL_RenderRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1942:34
        { ("SDL_RenderRects", "rects"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:1960:34
        { ("SDL_RenderFillRect", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:1978:34
        { ("SDL_RenderFillRects", "rects"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:1996:34
        { ("SDL_RenderTexture", "srcrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2018:34
        { ("SDL_RenderTexture", "dstrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2018:34
        { ("SDL_RenderTextureRotated", "srcrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureRotated", "dstrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureRotated", "center"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2046:34
        { ("SDL_RenderTextureTiled", "srcrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2076:34
        { ("SDL_RenderTextureTiled", "dstrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2076:34
        { ("SDL_RenderTexture9Grid", "srcrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2110:34
        { ("SDL_RenderTexture9Grid", "dstrect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2110:34
        { ("SDL_RenderGeometry", "vertices"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:2134:34
        { ("SDL_RenderGeometry", "indices"), PointerParameterIntent.Array }, // /usr/local/include/SDL3/SDL_render.h:2134:34
        { ("SDL_RenderGeometryRaw", "xy"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderGeometryRaw", "color"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderGeometryRaw", "uv"), PointerParameterIntent.IntPtr }, // /usr/local/include/SDL3/SDL_render.h:2166:34
        { ("SDL_RenderReadPixels", "rect"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_render.h:2193:43
        { ("SDL_GetRenderVSync", "vsync"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_render.h:2426:34
        { ("SDL_OpenStorage", "iface"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_storage.h:215:43
        { ("SDL_GetStorageFileSize", "length"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_storage.h:263:34
        { ("SDL_GetStoragePathInfo", "info"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_storage.h:398:34
        { ("SDL_GlobStorageDirectory", "count"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_storage.h:448:37
        { ("SDL_GetDateTimeLocalePreferences", "dateFormat"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_time.h:103:34
        { ("SDL_GetDateTimeLocalePreferences", "timeFormat"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_time.h:103:34
        { ("SDL_TimeToDateTime", "dt"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_time.h:131:34
        { ("SDL_DateTimeToTime", "dt"), PointerParameterIntent.Ref }, // /usr/local/include/SDL3/SDL_time.h:146:34
        { ("SDL_TimeToWindows", "dwLowDateTime"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_time.h:162:34
        { ("SDL_TimeToWindows", "dwHighDateTime"), PointerParameterIntent.Out }, // /usr/local/include/SDL3/SDL_time.h:162:34
    };

    internal static readonly Dictionary<string, ReturnedCharPtrMemoryOwner> ReturnedCharPtrMemoryOwners = new()
    {
        { "SDL_GetError", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_error.h:112:42
        { "SDL_GetStringProperty", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_properties.h:400:42
        { "SDL_GetThreadName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_thread.h:338:42
        { "SDL_GetAudioDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_audio.h:415:42
        { "SDL_GetCurrentAudioDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_audio.h:432:42
        { "SDL_GetAudioDeviceName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_audio.h:507:42
        { "SDL_GetAudioFormatName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_audio.h:1994:42
        { "SDL_GetPixelFormatName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_pixels.h:767:42
        { "SDL_GetCameraDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_camera.h:150:42
        { "SDL_GetCurrentCameraDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_camera.h:166:42
        { "SDL_GetCameraName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_camera.h:237:42
        { "SDL_GetClipboardText", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_clipboard.h:74:36
        { "SDL_GetPrimarySelectionText", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_clipboard.h:117:36
        { "SDL_GetVideoDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_video.h:376:42
        { "SDL_GetCurrentVideoDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_video.h:393:42
        { "SDL_GetDisplayName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_video.h:469:42
        { "SDL_GetWindowTitle", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_video.h:1344:42
        { "SDL_GetSensorNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_sensor.h:163:42
        { "SDL_GetSensorName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_sensor.h:233:42
        { "SDL_GetJoystickNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_joystick.h:231:42
        { "SDL_GetJoystickPathForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_joystick.h:247:42
        { "SDL_GetJoystickName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_joystick.h:678:42
        { "SDL_GetJoystickPath", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_joystick.h:691:42
        { "SDL_GetJoystickSerial", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_joystick.h:806:42
        { "SDL_GetGamepadMappingForGUID", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_gamepad.h:418:36
        { "SDL_GetGamepadMapping", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_gamepad.h:437:36
        { "SDL_GetGamepadNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:512:42
        { "SDL_GetGamepadPathForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:528:42
        { "SDL_GetGamepadMappingForID", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_gamepad.h:658:36
        { "SDL_GetGamepadName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:756:42
        { "SDL_GetGamepadPath", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:770:42
        { "SDL_GetGamepadSerial", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:891:42
        { "SDL_GetGamepadStringForType", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:1054:42
        { "SDL_GetGamepadStringForAxis", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:1090:42
        { "SDL_GetGamepadStringForButton", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:1163:42
        { "SDL_GetGamepadAppleSFSymbolsNameForButton", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:1449:42
        { "SDL_GetGamepadAppleSFSymbolsNameForAxis", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gamepad.h:1462:42
        { "SDL_GetKeyboardNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_keyboard.h:104:42
        { "SDL_GetScancodeName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_keyboard.h:268:42
        { "SDL_GetKeyName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_keyboard.h:299:42
        { "SDL_GetMouseNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_mouse.h:182:42
        { "SDL_GetTouchDeviceName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_touch.h:104:42
        { "SDL_GetBasePath", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_filesystem.h:80:42
        { "SDL_GetPrefPath", ReturnedCharPtrMemoryOwner.Caller }, // /usr/local/include/SDL3/SDL_filesystem.h:135:36
        { "SDL_GetUserFolder", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_filesystem.h:217:42
        { "SDL_GetGPUDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gpu.h:1874:42
        { "SDL_GetGPUDeviceDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_gpu.h:1884:42
        { "SDL_GetHapticNameForID", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_haptic.h:960:42
        { "SDL_GetHapticName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_haptic.h:1022:42
        { "SDL_GetHint", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_hints.h:4151:41
        { "SDL_GetAppMetadataProperty", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_init.h:352:42
        { "SDL_GetPlatform", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_platform.h:56:42
        { "SDL_GetRenderDriver", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_render.h:172:42
        { "SDL_GetRendererName", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_render.h:354:42
        { "SDL_GetRevision", ReturnedCharPtrMemoryOwner.SDL }, // /usr/local/include/SDL3/SDL_version.h:172:42
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
