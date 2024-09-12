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
        { ("SDL_ReportAssertion", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_assert.h:245:45
        { ("SDL_GetAssertionHandler", "puserdata"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_assert.h:489:50
        { ("SDL_AtomicCompareAndSwap", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:347:38
        { ("SDL_AtomicSet", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:367:33
        { ("SDL_AtomicGet", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:384:33
        { ("SDL_AtomicAdd", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:405:33
        { ("SDL_AtomicCompareAndSwapPointer", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:461:38
        { ("SDL_AtomicSetPointer", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:480:36
        { ("SDL_AtomicGetPointer", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_atomic.h:498:36
        { ("SDL_OpenIO", "iface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:383:44
        { ("SDL_LoadFile_IO", "datasize"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:603:36
        { ("SDL_LoadFile", "datasize"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:623:36
        { ("SDL_ReadU8", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:642:38
        { ("SDL_ReadS8", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:654:38
        { ("SDL_ReadU16LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:670:38
        { ("SDL_ReadS16LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:686:38
        { ("SDL_ReadU16BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:702:38
        { ("SDL_ReadS16BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:718:38
        { ("SDL_ReadU32LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:734:38
        { ("SDL_ReadS32LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:750:38
        { ("SDL_ReadU32BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:766:38
        { ("SDL_ReadS32BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:782:38
        { ("SDL_ReadU64LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:798:38
        { ("SDL_ReadS64LE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:814:38
        { ("SDL_ReadU64BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:830:38
        { ("SDL_ReadS64BE", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_iostream.h:846:38
        { ("SDL_GetAudioPlaybackDevices", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:461:49
        { ("SDL_GetAudioRecordingDevices", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:490:49
        { ("SDL_GetAudioDeviceFormat", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:542:38
        { ("SDL_GetAudioDeviceFormat", "sample_frames"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:542:38
        { ("SDL_GetAudioDeviceChannelMap", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:565:35
        { ("SDL_OpenAudioDevice", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:641:47
        { ("SDL_BindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:838:38
        { ("SDL_UnbindAudioStreams", "streams"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:879:34
        { ("SDL_CreateAudioStream", "src_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:937:47
        { ("SDL_CreateAudioStream", "dst_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:937:47
        { ("SDL_GetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:966:38
        { ("SDL_GetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:966:38
        { ("SDL_SetAudioStreamFormat", "src_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:997:38
        { ("SDL_SetAudioStreamFormat", "dst_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:997:38
        { ("SDL_GetAudioStreamInputChannelMap", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1111:35
        { ("SDL_GetAudioStreamOutputChannelMap", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1135:35
        { ("SDL_SetAudioStreamInputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1186:38
        { ("SDL_SetAudioStreamOutputChannelMap", "chmap"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1233:38
        { ("SDL_OpenAudioDeviceStream", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1707:47
        { ("SDL_LoadWAV_IO", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV_IO", "audio_buf"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV_IO", "audio_len"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1879:38
        { ("SDL_LoadWAV", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1915:38
        { ("SDL_LoadWAV", "audio_buf"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1915:38
        { ("SDL_LoadWAV", "audio_len"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1915:38
        { ("SDL_MixAudio", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1951:38
        { ("SDL_MixAudio", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1951:38
        { ("SDL_ConvertAudioSamples", "src_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "src_data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1981:38
        { ("SDL_ConvertAudioSamples", "dst_len"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_audio.h:1981:38
        { ("SDL_GetMasksForPixelFormat", "bpp"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:792:38
        { ("SDL_GetMasksForPixelFormat", "Rmask"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:792:38
        { ("SDL_GetMasksForPixelFormat", "Gmask"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:792:38
        { ("SDL_GetMasksForPixelFormat", "Bmask"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:792:38
        { ("SDL_GetMasksForPixelFormat", "Amask"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:792:38
        { ("SDL_SetPaletteColors", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:868:38
        { ("SDL_SetPaletteColors", "colors"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:868:38
        { ("SDL_DestroyPalette", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:882:34
        { ("SDL_MapRGB", "format"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:920:36
        { ("SDL_MapRGB", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:920:36
        { ("SDL_MapRGBA", "format"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:959:36
        { ("SDL_MapRGBA", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:959:36
        { ("SDL_GetRGB", "format"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:987:34
        { ("SDL_GetRGB", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:987:34
        { ("SDL_GetRGB", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:987:34
        { ("SDL_GetRGB", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:987:34
        { ("SDL_GetRGB", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:987:34
        { ("SDL_GetRGBA", "format"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_GetRGBA", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_GetRGBA", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_GetRGBA", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_GetRGBA", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_GetRGBA", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_pixels.h:1019:34
        { ("SDL_RectToFRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:126:23
        { ("SDL_RectToFRect", "frect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:126:23
        { ("SDL_PointInRect", "p"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:155:27
        { ("SDL_PointInRect", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:155:27
        { ("SDL_RectEmpty", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:179:27
        { ("SDL_RectsEqual", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:203:27
        { ("SDL_RectsEqual", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:203:27
        { ("SDL_HasRectIntersection", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:224:38
        { ("SDL_HasRectIntersection", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:224:38
        { ("SDL_GetRectIntersection", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectIntersection", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectIntersection", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:241:38
        { ("SDL_GetRectUnion", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectUnion", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectUnion", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:255:38
        { ("SDL_GetRectEnclosingPoints", "points"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectEnclosingPoints", "clip"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectEnclosingPoints", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:274:38
        { ("SDL_GetRectAndLineIntersection", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "X1"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "Y1"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "X2"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:294:38
        { ("SDL_GetRectAndLineIntersection", "Y2"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:294:38
        { ("SDL_PointInRectFloat", "p"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:320:27
        { ("SDL_PointInRectFloat", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:320:27
        { ("SDL_RectEmptyFloat", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:344:27
        { ("SDL_RectsEqualEpsilon", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:374:27
        { ("SDL_RectsEqualEpsilon", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:374:27
        { ("SDL_RectsEqualFloat", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:409:27
        { ("SDL_RectsEqualFloat", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:409:27
        { ("SDL_HasRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:427:38
        { ("SDL_HasRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:427:38
        { ("SDL_GetRectIntersectionFloat", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectIntersectionFloat", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectIntersectionFloat", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:444:38
        { ("SDL_GetRectUnionFloat", "A"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectUnionFloat", "B"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectUnionFloat", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:458:38
        { ("SDL_GetRectEnclosingPointsFloat", "points"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectEnclosingPointsFloat", "clip"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectEnclosingPointsFloat", "result"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:478:38
        { ("SDL_GetRectAndLineIntersectionFloat", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "X1"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "Y1"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "X2"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:499:38
        { ("SDL_GetRectAndLineIntersectionFloat", "Y2"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_rect.h:499:38
        { ("SDL_DestroySurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:183:34
        { ("SDL_GetSurfaceProperties", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:211:46
        { ("SDL_SetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:233:38
        { ("SDL_GetSurfaceColorspace", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:250:44
        { ("SDL_CreateSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:278:43
        { ("SDL_SetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:295:38
        { ("SDL_SetSurfacePalette", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:295:38
        { ("SDL_GetSurfacePalette", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:308:43
        { ("SDL_AddSurfaceAlternateImage", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:333:38
        { ("SDL_AddSurfaceAlternateImage", "image"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:333:38
        { ("SDL_SurfaceHasAlternateImages", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:348:38
        { ("SDL_GetSurfaceImages", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:373:44
        { ("SDL_GetSurfaceImages", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:373:44
        { ("SDL_RemoveSurfaceAlternateImages", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:389:34
        { ("SDL_LockSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:412:38
        { ("SDL_UnlockSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:423:34
        { ("SDL_SaveBMP_IO", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:484:38
        { ("SDL_SaveBMP", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:505:38
        { ("SDL_SetSurfaceRLE", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:525:38
        { ("SDL_SurfaceHasRLE", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:539:38
        { ("SDL_SetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:564:38
        { ("SDL_SurfaceHasColorKey", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:579:38
        { ("SDL_GetSurfaceColorKey", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:599:38
        { ("SDL_GetSurfaceColorKey", "key"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:599:38
        { ("SDL_SetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:622:38
        { ("SDL_GetSurfaceColorMod", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:640:38
        { ("SDL_GetSurfaceColorMod", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:640:38
        { ("SDL_SetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:660:38
        { ("SDL_GetSurfaceAlphaMod", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:675:38
        { ("SDL_GetSurfaceAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:675:38
        { ("SDL_SetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:693:38
        { ("SDL_GetSurfaceBlendMode", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:707:38
        { ("SDL_SetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:728:38
        { ("SDL_SetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:728:38
        { ("SDL_GetSurfaceClipRect", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:747:38
        { ("SDL_GetSurfaceClipRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:747:38
        { ("SDL_FlipSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:759:38
        { ("SDL_DuplicateSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:777:43
        { ("SDL_ScaleSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:796:43
        { ("SDL_ConvertSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:822:43
        { ("SDL_ConvertSurfaceAndColorspace", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:849:43
        { ("SDL_ConvertSurfaceAndColorspace", "palette"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:849:43
        { ("SDL_PremultiplySurfaceAlpha", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:935:38
        { ("SDL_ClearSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:955:38
        { ("SDL_FillSurfaceRect", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:980:38
        { ("SDL_FillSurfaceRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:980:38
        { ("SDL_FillSurfaceRects", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1005:38
        { ("SDL_FillSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1005:38
        { ("SDL_BlitSurface", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurface", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1080:38
        { ("SDL_BlitSurfaceUnchecked", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceUnchecked", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1105:38
        { ("SDL_BlitSurfaceScaled", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceScaled", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1130:38
        { ("SDL_BlitSurfaceUncheckedScaled", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceUncheckedScaled", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1156:38
        { ("SDL_BlitSurfaceTiled", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiled", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1182:38
        { ("SDL_BlitSurfaceTiledWithScale", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurfaceTiledWithScale", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1212:38
        { ("SDL_BlitSurface9Grid", "src"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "dst"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1249:38
        { ("SDL_BlitSurface9Grid", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1249:38
        { ("SDL_MapSurfaceRGB", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1279:36
        { ("SDL_MapSurfaceRGBA", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1310:36
        { ("SDL_ReadSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixel", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1337:38
        { ("SDL_ReadSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1361:38
        { ("SDL_ReadSurfacePixelFloat", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1361:38
        { ("SDL_WriteSurfacePixel", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1384:38
        { ("SDL_WriteSurfacePixelFloat", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_surface.h:1404:38
        { ("SDL_GetCameras", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:183:44
        { ("SDL_GetCameraSupportedFormats", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:222:47
        { ("SDL_OpenCamera", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:302:42
        { ("SDL_GetCameraFormat", "spec"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:388:38
        { ("SDL_AcquireCameraFrame", "timestampNS"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:431:43
        { ("SDL_ReleaseCameraFrame", "frame"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_camera.h:459:34
        { ("SDL_SetClipboardData", "mime_types"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_clipboard.h:197:38
        { ("SDL_GetClipboardData", "size"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_clipboard.h:228:36
        { ("SDL_GetDisplays", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:415:45
        { ("SDL_GetDisplayBounds", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:486:38
        { ("SDL_GetDisplayUsableBounds", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:510:38
        { ("SDL_GetFullscreenDisplayModes", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:580:48
        { ("SDL_GetClosestFullscreenDisplayMode", "mode"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:609:38
        { ("SDL_GetDisplayForPoint", "point"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:661:43
        { ("SDL_GetDisplayForRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:676:43
        { ("SDL_SetWindowFullscreenMode", "mode"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:763:38
        { ("SDL_GetWindowICCProfile", "size"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:790:36
        { ("SDL_GetWindows", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:816:43
        { ("SDL_SetWindowIcon", "icon"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1366:38
        { ("SDL_GetWindowPosition", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1429:38
        { ("SDL_GetWindowPosition", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1429:38
        { ("SDL_GetWindowSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1486:38
        { ("SDL_GetWindowSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1486:38
        { ("SDL_GetWindowSafeArea", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1506:38
        { ("SDL_GetWindowAspectRatio", "min_aspect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1562:38
        { ("SDL_GetWindowAspectRatio", "max_aspect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1562:38
        { ("SDL_GetWindowBordersSize", "top"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1596:38
        { ("SDL_GetWindowBordersSize", "left"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1596:38
        { ("SDL_GetWindowBordersSize", "bottom"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1596:38
        { ("SDL_GetWindowBordersSize", "right"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1596:38
        { ("SDL_GetWindowSizeInPixels", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1614:38
        { ("SDL_GetWindowSizeInPixels", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1614:38
        { ("SDL_GetWindowMinimumSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1648:38
        { ("SDL_GetWindowMinimumSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1648:38
        { ("SDL_GetWindowMaximumSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1682:38
        { ("SDL_GetWindowMaximumSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:1682:38
        { ("SDL_GetWindowSurfaceVSync", "vsync"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2007:38
        { ("SDL_UpdateWindowSurfaceRects", "rects"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2053:38
        { ("SDL_SetWindowMouseRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2170:38
        { ("SDL_SetWindowShape", "shape"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2399:38
        { ("SDL_GL_GetAttribute", "value"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2652:38
        { ("SDL_GL_GetSwapInterval", "interval"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_video.h:2817:38
        { ("SDL_ShowOpenFileDialog", "filters"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_dialog.h:154:34
        { ("SDL_ShowSaveFileDialog", "filters"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_dialog.h:209:34
        { ("SDL_GetPowerInfo", "seconds"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_power.h:85:44
        { ("SDL_GetPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_power.h:85:44
        { ("SDL_GetSensors", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_sensor.h:151:44
        { ("SDL_GetSensorData", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_sensor.h:280:38
        { ("SDL_GetJoysticks", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:214:46
        { ("SDL_AttachVirtualJoystick", "desc"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:481:44
        { ("SDL_SendJoystickVirtualSensorData", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:635:38
        { ("SDL_GetJoystickGUIDInfo", "vendor"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "product"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "version"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickGUIDInfo", "crc16"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:837:34
        { ("SDL_GetJoystickAxisInitialState", "state"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:1014:38
        { ("SDL_GetJoystickBall", "dx"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:1035:38
        { ("SDL_GetJoystickBall", "dy"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:1035:38
        { ("SDL_GetJoystickPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_joystick.h:1201:44
        { ("SDL_GetGamepadMappings", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:403:37
        { ("SDL_GetGamepads", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:482:46
        { ("SDL_GetGamepadPowerInfo", "percent"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:936:44
        { ("SDL_GetGamepadBindings", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:1012:51
        { ("SDL_GetGamepadTouchpadFinger", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:1269:38
        { ("SDL_GetGamepadTouchpadFinger", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:1269:38
        { ("SDL_GetGamepadTouchpadFinger", "pressure"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:1269:38
        { ("SDL_GetGamepadSensorData", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gamepad.h:1341:38
        { ("SDL_GetKeyboards", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_keyboard.h:89:46
        { ("SDL_GetKeyboardState", "numkeys"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_keyboard.h:144:46
        { ("SDL_SetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_keyboard.h:499:38
        { ("SDL_GetTextInputArea", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_keyboard.h:518:38
        { ("SDL_GetTextInputArea", "cursor"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_keyboard.h:518:38
        { ("SDL_GetMice", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:167:43
        { ("SDL_GetMouseState", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:213:50
        { ("SDL_GetMouseState", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:213:50
        { ("SDL_GetGlobalMouseState", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:243:50
        { ("SDL_GetGlobalMouseState", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:243:50
        { ("SDL_GetRelativeMouseState", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:262:50
        { ("SDL_GetRelativeMouseState", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:262:50
        { ("SDL_CreateCursor", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:430:42
        { ("SDL_CreateCursor", "mask"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:430:42
        { ("SDL_CreateColorCursor", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_mouse.h:461:42
        { ("SDL_GetTouchDevices", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_touch.h:93:43
        { ("SDL_GetTouchFingers", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_touch.h:129:43
        { ("SDL_PeepEvents", "events"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1045:33
        { ("SDL_PollEvent", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1177:38
        { ("SDL_WaitEvent", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1199:38
        { ("SDL_WaitEventTimeout", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1227:38
        { ("SDL_PushEvent", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1261:38
        { ("SDL_GetEventFilter", "userdata"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1347:38
        { ("SDL_GetWindowFromEvent", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_events.h:1464:42
        { ("SDL_GetPathInfo", "info"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_filesystem.h:345:38
        { ("SDL_GlobDirectory", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_filesystem.h:378:37
        { ("SDL_CreateGPUComputePipeline", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1820:53
        { ("SDL_CreateGPUGraphicsPipeline", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1838:54
        { ("SDL_CreateGPUSampler", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1856:45
        { ("SDL_CreateGPUShader", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1924:44
        { ("SDL_CreateGPUTexture", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1958:45
        { ("SDL_CreateGPUBuffer", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:1991:44
        { ("SDL_CreateGPUTransferBuffer", "createinfo"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2012:52
        { ("SDL_BeginGPURenderPass", "color_target_infos"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2360:48
        { ("SDL_BeginGPURenderPass", "depth_stencil_target_info"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2360:48
        { ("SDL_SetGPUViewport", "viewport"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2388:34
        { ("SDL_SetGPUScissor", "scissor"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2400:34
        { ("SDL_BindGPUVertexBuffers", "bindings"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2443:34
        { ("SDL_BindGPUIndexBuffer", "binding"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2460:34
        { ("SDL_BindGPUVertexSamplers", "texture_sampler_bindings"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2479:34
        { ("SDL_BindGPUVertexStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2498:34
        { ("SDL_BindGPUVertexStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2517:34
        { ("SDL_BindGPUFragmentSamplers", "texture_sampler_bindings"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2537:34
        { ("SDL_BindGPUFragmentStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2556:34
        { ("SDL_BindGPUFragmentStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2575:34
        { ("SDL_BeginGPUComputePass", "storage_texture_bindings"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2730:49
        { ("SDL_BeginGPUComputePass", "storage_buffer_bindings"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2730:49
        { ("SDL_BindGPUComputeStorageTextures", "storage_textures"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2762:34
        { ("SDL_BindGPUComputeStorageBuffers", "storage_buffers"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2781:34
        { ("SDL_UploadToGPUTexture", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2915:34
        { ("SDL_UploadToGPUTexture", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2915:34
        { ("SDL_UploadToGPUBuffer", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2937:34
        { ("SDL_UploadToGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2937:34
        { ("SDL_CopyGPUTextureToTexture", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2960:34
        { ("SDL_CopyGPUTextureToTexture", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2960:34
        { ("SDL_CopyGPUBufferToBuffer", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2986:34
        { ("SDL_CopyGPUBufferToBuffer", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:2986:34
        { ("SDL_DownloadFromGPUTexture", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3006:34
        { ("SDL_DownloadFromGPUTexture", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3006:34
        { ("SDL_DownloadFromGPUBuffer", "source"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3023:34
        { ("SDL_DownloadFromGPUBuffer", "destination"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3023:34
        { ("SDL_BlitGPUTexture", "info"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3062:34
        { ("SDL_AcquireGPUSwapchainTexture", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3210:45
        { ("SDL_AcquireGPUSwapchainTexture", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3210:45
        { ("SDL_WaitForGPUFences", "fences"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_gpu.h:3288:34
        { ("SDL_GetHaptics", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_haptic.h:943:44
        { ("SDL_HapticEffectSupported", "effect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_haptic.h:1167:38
        { ("SDL_CreateHapticEffect", "effect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_haptic.h:1184:33
        { ("SDL_UpdateHapticEffect", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_haptic.h:1206:38
        { ("SDL_hid_free_enumeration", "devs"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:252:34
        { ("SDL_hid_write", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:311:33
        { ("SDL_hid_read_timeout", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:332:33
        { ("SDL_hid_read", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:353:33
        { ("SDL_hid_send_feature_report", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:397:33
        { ("SDL_hid_get_feature_report", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:420:33
        { ("SDL_hid_get_input_report", "data"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:443:33
        { ("SDL_hid_get_report_descriptor", "buf"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_hidapi.h:535:33
        { ("SDL_GetPreferredLocales", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_locale.h:101:43
        { ("SDL_GetLogOutputFunction", "userdata"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_log.h:424:34
        { ("SDL_ShowMessageBox", "messageboxdata"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_messagebox.h:164:38
        { ("SDL_ShowMessageBox", "buttonid"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_messagebox.h:164:38
        { ("SDL_CreateWindowAndRenderer", "window"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:188:38
        { ("SDL_CreateWindowAndRenderer", "renderer"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:188:38
        { ("SDL_CreateSoftwareRenderer", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:300:44
        { ("SDL_GetRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:457:38
        { ("SDL_GetRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:457:38
        { ("SDL_GetCurrentRenderOutputSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:477:38
        { ("SDL_GetCurrentRenderOutputSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:477:38
        { ("SDL_CreateTextureFromSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:527:43
        { ("SDL_GetTextureSize", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:814:38
        { ("SDL_GetTextureSize", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:814:38
        { ("SDL_GetTextureColorMod", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:888:38
        { ("SDL_GetTextureColorMod", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:888:38
        { ("SDL_GetTextureColorMod", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:888:38
        { ("SDL_GetTextureColorModFloat", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:906:38
        { ("SDL_GetTextureColorModFloat", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:906:38
        { ("SDL_GetTextureColorModFloat", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:906:38
        { ("SDL_GetTextureAlphaMod", "alpha"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:970:38
        { ("SDL_GetTextureAlphaModFloat", "alpha"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:986:38
        { ("SDL_GetTextureScaleMode", "scaleMode"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1049:38
        { ("SDL_UpdateTexture", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1081:38
        { ("SDL_UpdateYUVTexture", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1111:38
        { ("SDL_UpdateYUVTexture", "Yplane"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1111:38
        { ("SDL_UpdateYUVTexture", "Uplane"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1111:38
        { ("SDL_UpdateYUVTexture", "Vplane"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1111:38
        { ("SDL_UpdateNVTexture", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1141:38
        { ("SDL_UpdateNVTexture", "Yplane"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1141:38
        { ("SDL_UpdateNVTexture", "UVplane"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1141:38
        { ("SDL_LockTexture", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1174:38
        { ("SDL_LockTexture", "pixels"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1174:38
        { ("SDL_LockTexture", "pitch"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1174:38
        { ("SDL_LockTextureToSurface", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1210:38
        { ("SDL_LockTextureToSurface", "surface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1210:38
        { ("SDL_GetRenderLogicalPresentation", "w"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1314:38
        { ("SDL_GetRenderLogicalPresentation", "h"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1314:38
        { ("SDL_GetRenderLogicalPresentation", "mode"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1314:38
        { ("SDL_GetRenderLogicalPresentation", "scale_mode"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1314:38
        { ("SDL_GetRenderLogicalPresentationRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1334:38
        { ("SDL_RenderCoordinatesFromWindow", "x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1352:38
        { ("SDL_RenderCoordinatesFromWindow", "y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1352:38
        { ("SDL_RenderCoordinatesToWindow", "window_x"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1372:38
        { ("SDL_RenderCoordinatesToWindow", "window_y"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1372:38
        { ("SDL_ConvertEventToRenderCoordinates", "event"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1391:38
        { ("SDL_SetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1407:38
        { ("SDL_GetRenderViewport", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1422:38
        { ("SDL_GetRenderSafeArea", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1460:38
        { ("SDL_SetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1476:38
        { ("SDL_GetRenderClipRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1492:38
        { ("SDL_GetRenderScale", "scaleX"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1544:38
        { ("SDL_GetRenderScale", "scaleY"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1544:38
        { ("SDL_GetRenderDrawColor", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1612:38
        { ("SDL_GetRenderDrawColor", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1612:38
        { ("SDL_GetRenderDrawColor", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1612:38
        { ("SDL_GetRenderDrawColor", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1612:38
        { ("SDL_GetRenderDrawColorFloat", "r"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1634:38
        { ("SDL_GetRenderDrawColorFloat", "g"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1634:38
        { ("SDL_GetRenderDrawColorFloat", "b"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1634:38
        { ("SDL_GetRenderDrawColorFloat", "a"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1634:38
        { ("SDL_GetRenderColorScale", "scale"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1670:38
        { ("SDL_RenderPoints", "points"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1748:38
        { ("SDL_RenderLines", "points"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1781:38
        { ("SDL_RenderRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1796:38
        { ("SDL_RenderRects", "rects"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1812:38
        { ("SDL_RenderFillRect", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1828:38
        { ("SDL_RenderFillRects", "rects"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1844:38
        { ("SDL_RenderTexture", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1864:38
        { ("SDL_RenderTexture", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1864:38
        { ("SDL_RenderTextureRotated", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1890:38
        { ("SDL_RenderTextureRotated", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1890:38
        { ("SDL_RenderTextureRotated", "center"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1890:38
        { ("SDL_RenderTextureTiled", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1918:38
        { ("SDL_RenderTextureTiled", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1918:38
        { ("SDL_RenderTexture9Grid", "srcrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1950:38
        { ("SDL_RenderTexture9Grid", "dstrect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1950:38
        { ("SDL_RenderGeometry", "vertices"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1972:38
        { ("SDL_RenderGeometry", "indices"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:1972:38
        { ("SDL_RenderGeometryRaw", "xy"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:2002:38
        { ("SDL_RenderGeometryRaw", "color"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:2002:38
        { ("SDL_RenderGeometryRaw", "uv"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:2002:38
        { ("SDL_RenderReadPixels", "rect"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:2027:43
        { ("SDL_GetRenderVSync", "vsync"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_render.h:2244:38
        { ("SDL_OpenStorage", "iface"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_storage.h:215:43
        { ("SDL_GetStorageFileSize", "length"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_storage.h:263:38
        { ("SDL_GetStoragePathInfo", "info"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_storage.h:394:38
        { ("SDL_GlobStorageDirectory", "count"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_storage.h:444:37
        { ("SDL_WaitThread", "status"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_thread.h:423:34
        { ("SDL_GetDateTimeLocalePreferences", "dateFormat"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:103:38
        { ("SDL_GetDateTimeLocalePreferences", "timeFormat"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:103:38
        { ("SDL_TimeToDateTime", "dt"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:131:38
        { ("SDL_DateTimeToTime", "dt"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:146:38
        { ("SDL_TimeToWindows", "dwLowDateTime"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:162:34
        { ("SDL_TimeToWindows", "dwHighDateTime"), PointerParameterIntent.Unknown }, // ../SDL3/SDL_time.h:162:34
    };

    internal static readonly Dictionary<string, DelegateDefinition> DelegateDefinitions = new()
    {
        {
            "SDL_AssertionHandler",
            new DelegateDefinition { ReturnType = "SDL_AssertState", Parameters = [("ref SDL_AssertData", "data"), ("IntPtr", "userdata")] }
        }, // ./include/SDL3/SDL_assert.h:423:35
        {
            "SDL_CleanupPropertyCallback",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "userdata"), ("IntPtr", "value")] }
        }, // ./include/SDL3/SDL_properties.h:187:24
        {
            "SDL_EnumeratePropertiesCallback",
            new DelegateDefinition { ReturnType = "void", Parameters = [("IntPtr", "userdata"), ("IntPtr", "props"), ("char*", "name")] }
        }, // ./include/SDL3/SDL_properties.h:499:24
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
    };

    internal static readonly string[] DeniedTypes = [];
}
