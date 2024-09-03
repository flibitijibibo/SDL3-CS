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
        Array,
    }

    private static readonly Dictionary<(string, string), PointerParameterIntent> ParametersIntents = new()
    {
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

    private static readonly string[] DENIED_TYPES =
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

        Dictionary<string, RawFFIEntry> typedefMap = new();
        foreach (var entry in ffiData)
        {
            if ((entry.Header == null) || !Path.GetFileName(entry.Header).StartsWith("SDL_"))
            {
                continue;
            }

            if ((entry.Tag == "typedef") && entry.Name!.StartsWith("SDL_"))
            {
                typedefMap[entry.Name!] = entry.Type!;
            }
        }

        var definitions = new StringBuilder();
        var unknownPointerParameters = new StringBuilder();
        unknownPointerParameters.Append("new pointer parameters:\n\n");
        var currentSourceFile = "";
        var definedTypes = new List<string>();

        foreach (var entry in ffiData)
        {
            if (DENIED_TYPES.Contains(entry.Name))
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

            if (entry.Tag == "enum")
            {
                definitions.Append($"public enum {entry.Name!}\n{{\n");
                definedTypes.Add(entry.Name!);

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

                definitions.Append("[StructLayout(LayoutKind.Sequential)]\n");
                definitions.Append($"public struct {entry.Name!}\n{{\n");
                definedTypes.Add(entry.Name!);

                foreach (var field in entry.Fields!)
                {
                    var name = SanitizeNames(field.Name!);
                    var fieldTypedef = GetTypeFromTypedefMap(type: field.Type!, typedefMap);
                    var type = CSharpTypeFromFFI(fieldTypedef, definedTypes, TypeContext.StructField);
                    if (type == "UNION")
                    {
                        type = $"UNION_{entry.Name}_{field.Name}";
                        definitions.Append($"// public {type} {name}; // TODO: unhandled union\n");
                    }
                    else
                    {
                        definitions.Append($"public {type} {name};\n");
                    }
                }

                definitions.Append("}\n\n");
            }

            else if (entry.Tag == "function")
            {
                definitions.Append("[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]\n");
                var returnTypedef = GetTypeFromTypedefMap(type: entry.ReturnType!, typedefMap);
                var returnType = CSharpTypeFromFFI(returnTypedef, definedTypes, TypeContext.Return);
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

                    var parameterTypedef = GetTypeFromTypedefMap(type: parameter.Type!, typedefMap);

                    string type;
                    if ((parameter.Type!.Tag == ":pointer") && definedTypes.Contains(parameter.Type!.Type!.Tag))
                    {
                        if (ParametersIntents.TryGetValue(key: (entry.Name!, parameter.Name!), value: out var intent))
                        {
                            type = intent switch
                            {
                                PointerParameterIntent.Ref   => $"ref {parameter.Type!.Type!.Tag}",
                                PointerParameterIntent.Array => $"{parameter.Type!.Type!.Tag}*",
                                _                            => $"ref {parameter.Type!.Type!.Tag}",
                            };
                            if (intent == PointerParameterIntent.Unknown)
                            {
                                containsUnknownRef = true;
                            }
                        }
                        else
                        {
                            type = $"ref {parameter.Type!.Type!.Tag}";
                            containsUnknownRef = true;
                            unknownPointerParameters.Append($"{{ (\"{entry.Name!}\", \"{parameter.Name!}\"), PointerParameterIntent.Unknown }},\n");
                        }
                    }
                    else
                    {
                        type = CSharpTypeFromFFI(parameterTypedef, definedTypes, TypeContext.Parameter);
                    }

                    var name = SanitizeNames(parameter.Name!);
                    definitions.Append($"{type} {name}");
                }

                definitions.Append(");\t");
                if (containsUnknownRef)
                {
                    definitions.Append("// SDL_POINTER: check for array usage");
                }

                definitions.Append("\n\n");
            }
        }


        File.WriteAllText(
            path: Path.Combine(outputDir.FullName, "SDL3.cs"),
            contents: CompileBindingsCSharp(definitions.ToString())
        );

        RunProcess(dotnetExe, args: $"format {sdlBindingsProjectFile}");
        Console.Write(unknownPointerParameters.ToString());

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

    private static RawFFIEntry GetTypeFromTypedefMap(RawFFIEntry type, Dictionary<string, RawFFIEntry> typedefMap)
    {
        if (type.Tag.StartsWith("SDL_") && typedefMap.TryGetValue(type.Tag, value: out var value))
        {
            return value;
        }

        return type;
    }

    private static string CSharpTypeFromFFI(RawFFIEntry type, List<string> definedTypes, TypeContext context)
    {
        if ((type.Tag == ":pointer") && definedTypes.Contains(type.Type!.Tag))
        {
            return context switch
            {
                TypeContext.Parameter   => $"ref {type.Type!.Tag}",
                TypeContext.StructField => $"{type.Type!.Tag}*",
                TypeContext.Return      => "IntPtr",
                _                       => "IntPtr",
            };
        }

        // TODO: should annotate these results with comments.
        // e.g. SDL_bool is a typedef of int, in a function signatures "int value" can be very misleading
        return type.Tag switch
        {
            ":_Bool"            => "bool",
            ":int"              => "int",
            ":long"             => "long", // TODO: platform-dependent?
            ":unsigned-short"   => "ushort", // TODO: platform-dependent?
            ":unsigned-int"     => "uint",
            ":unsigned-long"    => "ulong", // TODO: platform-dependent?
            ":float"            => "float",
            ":double"           => "double",
            "Uint8"             => "byte",
            "Uint16"            => "UInt16",
            "Uint32"            => "UInt32",
            "Uint64"            => "UInt64",
            "Sint8"             => "sbyte",
            "Sint16"            => "Int16",
            "Sint32"            => "Int32",
            "Sint64"            => "Int64",
            "size_t"            => "UIntPtr",
            ":void"             => "void",
            ":pointer"          => "IntPtr",
            ":function-pointer" => "IntPtr", // TODO: no idea
            "va_list"           => "IntPtr", // TODO: almost certainly wrong
            ":enum"             => type.Name!,
            ":struct"           => type.Name!,
            ":array"            => $"{CSharpTypeFromFFI(type: type.Type!, definedTypes, context)}[]", // TODO: probably wrong
            "union"             => "UNION",
            _                   => type.Tag,
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
}
