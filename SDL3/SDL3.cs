// NOTE: This file is auto-generated.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL3;

public static unsafe class SDL
{
    private const string nativeLibName = "SDL3";

    internal static int SizeAsUTF8(string str)
    {
        if (str == null)
        {
            return 0;
        }

        return (str.Length * 4) + 1;
    }

    internal static byte* EncodeAsUTF8(string str)
    {
        if (str == null)
        {
            return (byte*)0;
        }

        var size = (str.Length * 4) + 1;
        var buffer = (byte*)SDL_malloc((UIntPtr)size);
        fixed (char* strPtr = str)
        {
            Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, size);
        }

        return buffer;
    }

    // ../SDL3/SDL_stdinc.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_malloc(UIntPtr size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_free(IntPtr mem);

    // ../SDL3/SDL_assert.h

    public enum SDL_AssertState
    {
        SDL_ASSERTION_RETRY = 0,
        SDL_ASSERTION_BREAK = 1,
        SDL_ASSERTION_ABORT = 2,
        SDL_ASSERTION_IGNORE = 3,
        SDL_ASSERTION_ALWAYS_IGNORE = 4,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AssertData
    {
        public bool always_ignore;
        public uint trigger_count;
        public char* condition;
        public char* filename;
        public int linenum;
        public char* function;
        public SDL_AssertData* next;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ReportAssertion", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_AssertState INTERNAL_SDL_ReportAssertion(ref SDL_AssertData data, byte* func, byte* file, int line); // WARN_UNKNOWN_POINTER_PARAMETER
    public static SDL_AssertState SDL_ReportAssertion(ref SDL_AssertData data, string func, string file, int line)
    {
        var funcUTF8 = EncodeAsUTF8(func);
        var fileUTF8 = EncodeAsUTF8(file);

        var result = INTERNAL_SDL_ReportAssertion(ref data, funcUTF8, fileUTF8, line);

        SDL_free((IntPtr)funcUTF8);
        SDL_free((IntPtr)fileUTF8);

        return result;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_AssertState SDL_AssertionHandler(ref SDL_AssertData data, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetAssertionHandler(/* SDL_AssertionHandler */ IntPtr handler, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetDefaultAssertionHandler();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAssertionHandler(ref IntPtr puserdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAssertionReport();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ResetAssertionReport();

    // ../SDL3/SDL_atomic.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TryLockSpinlock(IntPtr @lock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_LockSpinlock(IntPtr @lock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockSpinlock(IntPtr @lock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_MemoryBarrierReleaseFunction();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_MemoryBarrierAcquireFunction();

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AtomicInt
    {
        public int value;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AtomicCompareAndSwap(ref SDL_AtomicInt a, int oldval, int newval); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_AtomicSet(ref SDL_AtomicInt a, int v); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_AtomicGet(ref SDL_AtomicInt a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_AtomicAdd(ref SDL_AtomicInt a, int v); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AtomicCompareAndSwapPointer(ref IntPtr a, IntPtr oldval, IntPtr newval); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_AtomicSetPointer(ref IntPtr a, IntPtr v); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_AtomicGetPointer(ref IntPtr a); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_endian.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_SwapFloat(float x);

    // ../SDL3/SDL_error.h

    [DllImport(nativeLibName, EntryPoint = "SDL_SetError", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetError(byte* fmt);
    public static bool SDL_SetError(string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        var result = INTERNAL_SDL_SetError(fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_OutOfMemory();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetError();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClearError();

    // ../SDL3/SDL_mutex.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateMutex();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_LockMutex(IntPtr mutex);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TryLockMutex(IntPtr mutex);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockMutex(IntPtr mutex);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyMutex(IntPtr mutex);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateRWLock();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_LockRWLockForReading(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_LockRWLockForWriting(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TryLockRWLockForReading(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TryLockRWLockForWriting(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockRWLock(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyRWLock(IntPtr rwlock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSemaphore(uint initial_value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroySemaphore(IntPtr sem);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WaitSemaphore(IntPtr sem);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TryWaitSemaphore(IntPtr sem);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WaitSemaphoreTimeout(IntPtr sem, int timeoutMS);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SignalSemaphore(IntPtr sem);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetSemaphoreValue(IntPtr sem);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateCondition();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyCondition(IntPtr cond);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SignalCondition(IntPtr cond);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BroadcastCondition(IntPtr cond);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WaitCondition(IntPtr cond, IntPtr mutex);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WaitConditionTimeout(IntPtr cond, IntPtr mutex, int timeoutMS);

    // ../SDL3/SDL_properties.h

    public enum SDL_PropertyType
    {
        SDL_PROPERTY_TYPE_INVALID = 0,
        SDL_PROPERTY_TYPE_POINTER = 1,
        SDL_PROPERTY_TYPE_STRING = 2,
        SDL_PROPERTY_TYPE_NUMBER = 3,
        SDL_PROPERTY_TYPE_FLOAT = 4,
        SDL_PROPERTY_TYPE_BOOLEAN = 5,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetGlobalProperties();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_CreateProperties();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_CopyProperties(uint src, uint dst);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LockProperties(uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockProperties(uint props);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_CleanupPropertyCallback(IntPtr userdata, IntPtr value);

    [DllImport(nativeLibName, EntryPoint = "SDL_SetPointerPropertyWithCleanup", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetPointerPropertyWithCleanup(uint props, byte* name, IntPtr value, /* SDL_CleanupPropertyCallback */ IntPtr cleanup, IntPtr userdata);
    public static bool SDL_SetPointerPropertyWithCleanup(uint props, string name, IntPtr value, /* SDL_CleanupPropertyCallback */ IntPtr cleanup, IntPtr userdata)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetPointerPropertyWithCleanup(props, nameUTF8, value, cleanup, userdata);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetPointerProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetPointerProperty(uint props, byte* name, IntPtr value);
    public static bool SDL_SetPointerProperty(uint props, string name, IntPtr value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetPointerProperty(props, nameUTF8, value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetStringProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetStringProperty(uint props, byte* name, byte* value);
    public static bool SDL_SetStringProperty(uint props, string name, string value)
    {
        var nameUTF8 = EncodeAsUTF8(name);
        var valueUTF8 = EncodeAsUTF8(value);

        var result = INTERNAL_SDL_SetStringProperty(props, nameUTF8, valueUTF8);

        SDL_free((IntPtr)nameUTF8);
        SDL_free((IntPtr)valueUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetNumberProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetNumberProperty(uint props, byte* name, long value);
    public static bool SDL_SetNumberProperty(uint props, string name, long value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetNumberProperty(props, nameUTF8, value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetFloatProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetFloatProperty(uint props, byte* name, float value);
    public static bool SDL_SetFloatProperty(uint props, string name, float value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetFloatProperty(props, nameUTF8, value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetBooleanProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetBooleanProperty(uint props, byte* name, bool value);
    public static bool SDL_SetBooleanProperty(uint props, string name, bool value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetBooleanProperty(props, nameUTF8, value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_HasProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_HasProperty(uint props, byte* name);
    public static bool SDL_HasProperty(uint props, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_HasProperty(props, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetPropertyType", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_PropertyType INTERNAL_SDL_GetPropertyType(uint props, byte* name);
    public static SDL_PropertyType SDL_GetPropertyType(uint props, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetPropertyType(props, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetPointerProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetPointerProperty(uint props, byte* name, IntPtr default_value);
    public static IntPtr SDL_GetPointerProperty(uint props, string name, IntPtr default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetPointerProperty(props, nameUTF8, default_value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetStringProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetStringProperty(uint props, byte* name, byte* default_value);
    public static IntPtr SDL_GetStringProperty(uint props, string name, string default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);
        var default_valueUTF8 = EncodeAsUTF8(default_value);

        var result = INTERNAL_SDL_GetStringProperty(props, nameUTF8, default_valueUTF8);

        SDL_free((IntPtr)nameUTF8);
        SDL_free((IntPtr)default_valueUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetNumberProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern long INTERNAL_SDL_GetNumberProperty(uint props, byte* name, long default_value);
    public static long SDL_GetNumberProperty(uint props, string name, long default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetNumberProperty(props, nameUTF8, default_value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetFloatProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern float INTERNAL_SDL_GetFloatProperty(uint props, byte* name, float default_value);
    public static float SDL_GetFloatProperty(uint props, string name, float default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetFloatProperty(props, nameUTF8, default_value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetBooleanProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GetBooleanProperty(uint props, byte* name, bool default_value);
    public static bool SDL_GetBooleanProperty(uint props, string name, bool default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetBooleanProperty(props, nameUTF8, default_value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ClearProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_ClearProperty(uint props, byte* name);
    public static bool SDL_ClearProperty(uint props, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_ClearProperty(props, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_EnumeratePropertiesCallback(IntPtr userdata, IntPtr props, char* name);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_EnumerateProperties(uint props, /* SDL_EnumeratePropertiesCallback */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyProperties(uint props);

    // ../SDL3/SDL_iostream.h

    public enum SDL_IOStatus
    {
        SDL_IO_STATUS_READY = 0,
        SDL_IO_STATUS_ERROR = 1,
        SDL_IO_STATUS_EOF = 2,
        SDL_IO_STATUS_NOT_READY = 3,
        SDL_IO_STATUS_READONLY = 4,
        SDL_IO_STATUS_WRITEONLY = 5,
    }

    public enum SDL_IOWhence
    {
        SDL_IO_SEEK_SET = 0,
        SDL_IO_SEEK_CUR = 1,
        SDL_IO_SEEK_END = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_IOStreamInterface
    {
        public uint version;
        public IntPtr size; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr seek; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr read; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr write; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr close; // WARN_ANONYMOUS_FUNCTION_POINTER
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_IOFromFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_IOFromFile(byte* file, byte* mode);
    public static IntPtr SDL_IOFromFile(string file, string mode)
    {
        var fileUTF8 = EncodeAsUTF8(file);
        var modeUTF8 = EncodeAsUTF8(mode);

        var result = INTERNAL_SDL_IOFromFile(fileUTF8, modeUTF8);

        SDL_free((IntPtr)fileUTF8);
        SDL_free((IntPtr)modeUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_IOFromMem(IntPtr mem, UIntPtr size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_IOFromConstMem(IntPtr mem, UIntPtr size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_IOFromDynamicMem();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenIO(ref SDL_IOStreamInterface iface, IntPtr userdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_CloseIO(IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetIOProperties(IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_IOStatus SDL_GetIOStatus(IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern long SDL_GetIOSize(IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern long SDL_SeekIO(IntPtr context, long offset, SDL_IOWhence whence);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern long SDL_TellIO(IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern UIntPtr SDL_ReadIO(IntPtr context, IntPtr ptr, UIntPtr size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern UIntPtr SDL_WriteIO(IntPtr context, IntPtr ptr, UIntPtr size);

    [DllImport(nativeLibName, EntryPoint = "SDL_IOprintf", CallingConvention = CallingConvention.Cdecl)]
    private static extern UIntPtr INTERNAL_SDL_IOprintf(IntPtr context, byte* fmt);
    public static UIntPtr SDL_IOprintf(IntPtr context, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        var result = INTERNAL_SDL_IOprintf(context, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_LoadFile_IO(IntPtr src, ref UIntPtr datasize, bool closeio); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_LoadFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_LoadFile(byte* file, ref UIntPtr datasize); // WARN_UNKNOWN_POINTER_PARAMETER
    public static IntPtr SDL_LoadFile(string file, ref UIntPtr datasize)
    {
        var fileUTF8 = EncodeAsUTF8(file);

        var result = INTERNAL_SDL_LoadFile(fileUTF8, ref datasize);

        SDL_free((IntPtr)fileUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU8(IntPtr src, ref byte value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS8(IntPtr src, ref sbyte value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU16LE(IntPtr src, ref ushort value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS16LE(IntPtr src, ref short value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU16BE(IntPtr src, ref ushort value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS16BE(IntPtr src, ref short value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU32LE(IntPtr src, ref uint value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS32LE(IntPtr src, ref int value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU32BE(IntPtr src, ref uint value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS32BE(IntPtr src, ref int value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU64LE(IntPtr src, ref ulong value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS64LE(IntPtr src, ref long value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadU64BE(IntPtr src, ref ulong value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadS64BE(IntPtr src, ref long value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU8(IntPtr dst, byte value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS8(IntPtr dst, sbyte value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU16LE(IntPtr dst, ushort value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS16LE(IntPtr dst, short value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU16BE(IntPtr dst, ushort value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS16BE(IntPtr dst, short value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU32LE(IntPtr dst, uint value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS32LE(IntPtr dst, int value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU32BE(IntPtr dst, uint value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS32BE(IntPtr dst, int value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU64LE(IntPtr dst, ulong value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS64LE(IntPtr dst, long value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteU64BE(IntPtr dst, ulong value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteS64BE(IntPtr dst, long value);

    // ../SDL3/SDL_audio.h

    public enum SDL_AudioFormat
    {
        SDL_AUDIO_UNKNOWN = 0,
        SDL_AUDIO_U8 = 8,
        SDL_AUDIO_S8 = 32776,
        SDL_AUDIO_S16LE = 32784,
        SDL_AUDIO_S16BE = 36880,
        SDL_AUDIO_S32LE = 32800,
        SDL_AUDIO_S32BE = 36896,
        SDL_AUDIO_F32LE = 33056,
        SDL_AUDIO_F32BE = 37152,
        SDL_AUDIO_S16 = 32784,
        SDL_AUDIO_S32 = 32800,
        SDL_AUDIO_F32 = 33056,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioSpec
    {
        public SDL_AudioFormat format;
        public int channels;
        public int freq;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumAudioDrivers();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioDriver(int index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCurrentAudioDriver();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioPlaybackDevices(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioRecordingDevices(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioDeviceName(uint devid);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetAudioDeviceFormat(uint devid, ref SDL_AudioSpec spec, ref int sample_frames); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioDeviceChannelMap(uint devid, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_OpenAudioDevice(uint devid, ref SDL_AudioSpec spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PauseAudioDevice(uint dev);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ResumeAudioDevice(uint dev);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AudioDevicePaused(uint dev);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetAudioDeviceGain(uint devid);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioDeviceGain(uint devid, float gain);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseAudioDevice(uint devid);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BindAudioStreams(uint devid, ref IntPtr streams, int num_streams); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BindAudioStream(uint devid, IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnbindAudioStreams(ref IntPtr streams, int num_streams); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnbindAudioStream(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetAudioStreamDevice(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateAudioStream(ref SDL_AudioSpec src_spec, ref SDL_AudioSpec dst_spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetAudioStreamProperties(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetAudioStreamFormat(IntPtr stream, ref SDL_AudioSpec src_spec, ref SDL_AudioSpec dst_spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamFormat(IntPtr stream, ref SDL_AudioSpec src_spec, ref SDL_AudioSpec dst_spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetAudioStreamFrequencyRatio(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamFrequencyRatio(IntPtr stream, float ratio);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetAudioStreamGain(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamGain(IntPtr stream, float gain);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioStreamInputChannelMap(IntPtr stream, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioStreamOutputChannelMap(IntPtr stream, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamInputChannelMap(IntPtr stream, ref int chmap, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamOutputChannelMap(IntPtr stream, ref int chmap, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PutAudioStreamData(IntPtr stream, IntPtr buf, int len);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetAudioStreamData(IntPtr stream, IntPtr buf, int len);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetAudioStreamAvailable(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetAudioStreamQueued(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FlushAudioStream(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClearAudioStream(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PauseAudioStreamDevice(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ResumeAudioStreamDevice(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LockAudioStream(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UnlockAudioStream(IntPtr stream);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_AudioStreamCallback(IntPtr userdata, IntPtr stream, int additionalAmount, int totalAmount);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamGetCallback(IntPtr stream, /* SDL_AudioStreamCallback */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioStreamPutCallback(IntPtr stream, /* SDL_AudioStreamCallback */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyAudioStream(IntPtr stream);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenAudioDeviceStream(uint devid, ref SDL_AudioSpec spec, /* SDL_AudioStreamCallback */ IntPtr callback, IntPtr userdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_AudioPostmixCallback(IntPtr userdata, ref SDL_AudioSpec spec, ref float buffer, int buflen);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetAudioPostmixCallback(uint devid, /* SDL_AudioPostmixCallback */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LoadWAV_IO(IntPtr src, bool closeio, ref SDL_AudioSpec spec, ref IntPtr audio_buf, ref uint audio_len); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_LoadWAV", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_LoadWAV(byte* path, ref SDL_AudioSpec spec, ref IntPtr audio_buf, ref uint audio_len); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_LoadWAV(string path, ref SDL_AudioSpec spec, ref IntPtr audio_buf, ref uint audio_len)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_LoadWAV(pathUTF8, ref spec, ref audio_buf, ref audio_len);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_MixAudio(ref byte dst, ref byte src, SDL_AudioFormat format, uint len, float volume); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ConvertAudioSamples(ref SDL_AudioSpec src_spec, ref byte src_data, int src_len, ref SDL_AudioSpec dst_spec, ref IntPtr dst_data, ref int dst_len); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetAudioFormatName(SDL_AudioFormat format);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);

    // ../SDL3/SDL_bits.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_MostSignificantBitIndex32(uint x);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasExactlyOneBitSet32(uint x);

    // ../SDL3/SDL_blendmode.h

    public enum SDL_BlendOperation
    {
        SDL_BLENDOPERATION_ADD = 1,
        SDL_BLENDOPERATION_SUBTRACT = 2,
        SDL_BLENDOPERATION_REV_SUBTRACT = 3,
        SDL_BLENDOPERATION_MINIMUM = 4,
        SDL_BLENDOPERATION_MAXIMUM = 5,
    }

    public enum SDL_BlendFactor
    {
        SDL_BLENDFACTOR_ZERO = 1,
        SDL_BLENDFACTOR_ONE = 2,
        SDL_BLENDFACTOR_SRC_COLOR = 3,
        SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 4,
        SDL_BLENDFACTOR_SRC_ALPHA = 5,
        SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 6,
        SDL_BLENDFACTOR_DST_COLOR = 7,
        SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR = 8,
        SDL_BLENDFACTOR_DST_ALPHA = 9,
        SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 10,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_ComposeCustomBlendMode(SDL_BlendFactor srcColorFactor, SDL_BlendFactor dstColorFactor, SDL_BlendOperation colorOperation, SDL_BlendFactor srcAlphaFactor, SDL_BlendFactor dstAlphaFactor, SDL_BlendOperation alphaOperation);

    // ../SDL3/SDL_pixels.h

    public enum SDL_PixelType
    {
        SDL_PIXELTYPE_UNKNOWN = 0,
        SDL_PIXELTYPE_INDEX1 = 1,
        SDL_PIXELTYPE_INDEX4 = 2,
        SDL_PIXELTYPE_INDEX8 = 3,
        SDL_PIXELTYPE_PACKED8 = 4,
        SDL_PIXELTYPE_PACKED16 = 5,
        SDL_PIXELTYPE_PACKED32 = 6,
        SDL_PIXELTYPE_ARRAYU8 = 7,
        SDL_PIXELTYPE_ARRAYU16 = 8,
        SDL_PIXELTYPE_ARRAYU32 = 9,
        SDL_PIXELTYPE_ARRAYF16 = 10,
        SDL_PIXELTYPE_ARRAYF32 = 11,
        SDL_PIXELTYPE_INDEX2 = 12,
    }

    public enum SDL_BitmapOrder
    {
        SDL_BITMAPORDER_NONE = 0,
        SDL_BITMAPORDER_4321 = 1,
        SDL_BITMAPORDER_1234 = 2,
    }

    public enum SDL_PackedOrder
    {
        SDL_PACKEDORDER_NONE = 0,
        SDL_PACKEDORDER_XRGB = 1,
        SDL_PACKEDORDER_RGBX = 2,
        SDL_PACKEDORDER_ARGB = 3,
        SDL_PACKEDORDER_RGBA = 4,
        SDL_PACKEDORDER_XBGR = 5,
        SDL_PACKEDORDER_BGRX = 6,
        SDL_PACKEDORDER_ABGR = 7,
        SDL_PACKEDORDER_BGRA = 8,
    }

    public enum SDL_ArrayOrder
    {
        SDL_ARRAYORDER_NONE = 0,
        SDL_ARRAYORDER_RGB = 1,
        SDL_ARRAYORDER_RGBA = 2,
        SDL_ARRAYORDER_ARGB = 3,
        SDL_ARRAYORDER_BGR = 4,
        SDL_ARRAYORDER_BGRA = 5,
        SDL_ARRAYORDER_ABGR = 6,
    }

    public enum SDL_PackedLayout
    {
        SDL_PACKEDLAYOUT_NONE = 0,
        SDL_PACKEDLAYOUT_332 = 1,
        SDL_PACKEDLAYOUT_4444 = 2,
        SDL_PACKEDLAYOUT_1555 = 3,
        SDL_PACKEDLAYOUT_5551 = 4,
        SDL_PACKEDLAYOUT_565 = 5,
        SDL_PACKEDLAYOUT_8888 = 6,
        SDL_PACKEDLAYOUT_2101010 = 7,
        SDL_PACKEDLAYOUT_1010102 = 8,
    }

    public enum SDL_PixelFormat
    {
        SDL_PIXELFORMAT_UNKNOWN = 0,
        SDL_PIXELFORMAT_INDEX1LSB = 286261504,
        SDL_PIXELFORMAT_INDEX1MSB = 287310080,
        SDL_PIXELFORMAT_INDEX2LSB = 470811136,
        SDL_PIXELFORMAT_INDEX2MSB = 471859712,
        SDL_PIXELFORMAT_INDEX4LSB = 303039488,
        SDL_PIXELFORMAT_INDEX4MSB = 304088064,
        SDL_PIXELFORMAT_INDEX8 = 318769153,
        SDL_PIXELFORMAT_RGB332 = 336660481,
        SDL_PIXELFORMAT_XRGB4444 = 353504258,
        SDL_PIXELFORMAT_XBGR4444 = 357698562,
        SDL_PIXELFORMAT_XRGB1555 = 353570562,
        SDL_PIXELFORMAT_XBGR1555 = 357764866,
        SDL_PIXELFORMAT_ARGB4444 = 355602434,
        SDL_PIXELFORMAT_RGBA4444 = 356651010,
        SDL_PIXELFORMAT_ABGR4444 = 359796738,
        SDL_PIXELFORMAT_BGRA4444 = 360845314,
        SDL_PIXELFORMAT_ARGB1555 = 355667970,
        SDL_PIXELFORMAT_RGBA5551 = 356782082,
        SDL_PIXELFORMAT_ABGR1555 = 359862274,
        SDL_PIXELFORMAT_BGRA5551 = 360976386,
        SDL_PIXELFORMAT_RGB565 = 353701890,
        SDL_PIXELFORMAT_BGR565 = 357896194,
        SDL_PIXELFORMAT_RGB24 = 386930691,
        SDL_PIXELFORMAT_BGR24 = 390076419,
        SDL_PIXELFORMAT_XRGB8888 = 370546692,
        SDL_PIXELFORMAT_RGBX8888 = 371595268,
        SDL_PIXELFORMAT_XBGR8888 = 374740996,
        SDL_PIXELFORMAT_BGRX8888 = 375789572,
        SDL_PIXELFORMAT_ARGB8888 = 372645892,
        SDL_PIXELFORMAT_RGBA8888 = 373694468,
        SDL_PIXELFORMAT_ABGR8888 = 376840196,
        SDL_PIXELFORMAT_BGRA8888 = 377888772,
        SDL_PIXELFORMAT_XRGB2101010 = 370614276,
        SDL_PIXELFORMAT_XBGR2101010 = 374808580,
        SDL_PIXELFORMAT_ARGB2101010 = 372711428,
        SDL_PIXELFORMAT_ABGR2101010 = 376905732,
        SDL_PIXELFORMAT_RGB48 = 403714054,
        SDL_PIXELFORMAT_BGR48 = 406859782,
        SDL_PIXELFORMAT_RGBA64 = 404766728,
        SDL_PIXELFORMAT_ARGB64 = 405815304,
        SDL_PIXELFORMAT_BGRA64 = 407912456,
        SDL_PIXELFORMAT_ABGR64 = 408961032,
        SDL_PIXELFORMAT_RGB48_FLOAT = 437268486,
        SDL_PIXELFORMAT_BGR48_FLOAT = 440414214,
        SDL_PIXELFORMAT_RGBA64_FLOAT = 438321160,
        SDL_PIXELFORMAT_ARGB64_FLOAT = 439369736,
        SDL_PIXELFORMAT_BGRA64_FLOAT = 441466888,
        SDL_PIXELFORMAT_ABGR64_FLOAT = 442515464,
        SDL_PIXELFORMAT_RGB96_FLOAT = 454057996,
        SDL_PIXELFORMAT_BGR96_FLOAT = 457203724,
        SDL_PIXELFORMAT_RGBA128_FLOAT = 455114768,
        SDL_PIXELFORMAT_ARGB128_FLOAT = 456163344,
        SDL_PIXELFORMAT_BGRA128_FLOAT = 458260496,
        SDL_PIXELFORMAT_ABGR128_FLOAT = 459309072,
        SDL_PIXELFORMAT_YV12 = 842094169,
        SDL_PIXELFORMAT_IYUV = 1448433993,
        SDL_PIXELFORMAT_YUY2 = 844715353,
        SDL_PIXELFORMAT_UYVY = 1498831189,
        SDL_PIXELFORMAT_YVYU = 1431918169,
        SDL_PIXELFORMAT_NV12 = 842094158,
        SDL_PIXELFORMAT_NV21 = 825382478,
        SDL_PIXELFORMAT_P010 = 808530000,
        SDL_PIXELFORMAT_EXTERNAL_OES = 542328143,
    }

    public enum SDL_ColorType
    {
        SDL_COLOR_TYPE_UNKNOWN = 0,
        SDL_COLOR_TYPE_RGB = 1,
        SDL_COLOR_TYPE_YCBCR = 2,
    }

    public enum SDL_ColorRange
    {
        SDL_COLOR_RANGE_UNKNOWN = 0,
        SDL_COLOR_RANGE_LIMITED = 1,
        SDL_COLOR_RANGE_FULL = 2,
    }

    public enum SDL_ColorPrimaries
    {
        SDL_COLOR_PRIMARIES_UNKNOWN = 0,
        SDL_COLOR_PRIMARIES_BT709 = 1,
        SDL_COLOR_PRIMARIES_UNSPECIFIED = 2,
        SDL_COLOR_PRIMARIES_BT470M = 4,
        SDL_COLOR_PRIMARIES_BT470BG = 5,
        SDL_COLOR_PRIMARIES_BT601 = 6,
        SDL_COLOR_PRIMARIES_SMPTE240 = 7,
        SDL_COLOR_PRIMARIES_GENERIC_FILM = 8,
        SDL_COLOR_PRIMARIES_BT2020 = 9,
        SDL_COLOR_PRIMARIES_XYZ = 10,
        SDL_COLOR_PRIMARIES_SMPTE431 = 11,
        SDL_COLOR_PRIMARIES_SMPTE432 = 12,
        SDL_COLOR_PRIMARIES_EBU3213 = 22,
        SDL_COLOR_PRIMARIES_CUSTOM = 31,
    }

    public enum SDL_TransferCharacteristics
    {
        SDL_TRANSFER_CHARACTERISTICS_UNKNOWN = 0,
        SDL_TRANSFER_CHARACTERISTICS_BT709 = 1,
        SDL_TRANSFER_CHARACTERISTICS_UNSPECIFIED = 2,
        SDL_TRANSFER_CHARACTERISTICS_GAMMA22 = 4,
        SDL_TRANSFER_CHARACTERISTICS_GAMMA28 = 5,
        SDL_TRANSFER_CHARACTERISTICS_BT601 = 6,
        SDL_TRANSFER_CHARACTERISTICS_SMPTE240 = 7,
        SDL_TRANSFER_CHARACTERISTICS_LINEAR = 8,
        SDL_TRANSFER_CHARACTERISTICS_LOG100 = 9,
        SDL_TRANSFER_CHARACTERISTICS_LOG100_SQRT10 = 10,
        SDL_TRANSFER_CHARACTERISTICS_IEC61966 = 11,
        SDL_TRANSFER_CHARACTERISTICS_BT1361 = 12,
        SDL_TRANSFER_CHARACTERISTICS_SRGB = 13,
        SDL_TRANSFER_CHARACTERISTICS_BT2020_10BIT = 14,
        SDL_TRANSFER_CHARACTERISTICS_BT2020_12BIT = 15,
        SDL_TRANSFER_CHARACTERISTICS_PQ = 16,
        SDL_TRANSFER_CHARACTERISTICS_SMPTE428 = 17,
        SDL_TRANSFER_CHARACTERISTICS_HLG = 18,
        SDL_TRANSFER_CHARACTERISTICS_CUSTOM = 31,
    }

    public enum SDL_MatrixCoefficients
    {
        SDL_MATRIX_COEFFICIENTS_IDENTITY = 0,
        SDL_MATRIX_COEFFICIENTS_BT709 = 1,
        SDL_MATRIX_COEFFICIENTS_UNSPECIFIED = 2,
        SDL_MATRIX_COEFFICIENTS_FCC = 4,
        SDL_MATRIX_COEFFICIENTS_BT470BG = 5,
        SDL_MATRIX_COEFFICIENTS_BT601 = 6,
        SDL_MATRIX_COEFFICIENTS_SMPTE240 = 7,
        SDL_MATRIX_COEFFICIENTS_YCGCO = 8,
        SDL_MATRIX_COEFFICIENTS_BT2020_NCL = 9,
        SDL_MATRIX_COEFFICIENTS_BT2020_CL = 10,
        SDL_MATRIX_COEFFICIENTS_SMPTE2085 = 11,
        SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_NCL = 12,
        SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_CL = 13,
        SDL_MATRIX_COEFFICIENTS_ICTCP = 14,
        SDL_MATRIX_COEFFICIENTS_CUSTOM = 31,
    }

    public enum SDL_ChromaLocation
    {
        SDL_CHROMA_LOCATION_NONE = 0,
        SDL_CHROMA_LOCATION_LEFT = 1,
        SDL_CHROMA_LOCATION_CENTER = 2,
        SDL_CHROMA_LOCATION_TOPLEFT = 3,
    }

    public enum SDL_Colorspace
    {
        SDL_COLORSPACE_UNKNOWN = 0,
        SDL_COLORSPACE_SRGB = 301991328,
        SDL_COLORSPACE_SRGB_LINEAR = 301991168,
        SDL_COLORSPACE_HDR10 = 301999616,
        SDL_COLORSPACE_JPEG = 570426566,
        SDL_COLORSPACE_BT601_LIMITED = 554703046,
        SDL_COLORSPACE_BT601_FULL = 571480262,
        SDL_COLORSPACE_BT709_LIMITED = 554697761,
        SDL_COLORSPACE_BT709_FULL = 571474977,
        SDL_COLORSPACE_BT2020_LIMITED = 554706441,
        SDL_COLORSPACE_BT2020_FULL = 571483657,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Color
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FColor
    {
        public float r;
        public float g;
        public float b;
        public float a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Palette
    {
        public int ncolors;
        public SDL_Color* colors;
        public uint version;
        public int refcount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PixelFormatDetails
    {
        public SDL_PixelFormat format;
        public byte bits_per_pixel;
        public byte bytes_per_pixel;
        public byte padding0;
        public byte padding1;
        public uint Rmask;
        public uint Gmask;
        public uint Bmask;
        public uint Amask;
        public byte Rbits;
        public byte Gbits;
        public byte Bbits;
        public byte Abits;
        public byte Rshift;
        public byte Gshift;
        public byte Bshift;
        public byte Ashift;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetPixelFormatName(SDL_PixelFormat format);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetMasksForPixelFormat(SDL_PixelFormat format, ref int bpp, ref uint Rmask, ref uint Gmask, ref uint Bmask, ref uint Amask); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetPixelFormatDetails(SDL_PixelFormat format);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreatePalette(int ncolors);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetPaletteColors(ref SDL_Palette palette, ref SDL_Color colors, int firstcolor, int ncolors); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyPalette(ref SDL_Palette palette); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_MapRGB(ref SDL_PixelFormatDetails format, ref SDL_Palette palette, byte r, byte g, byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_MapRGBA(ref SDL_PixelFormatDetails format, ref SDL_Palette palette, byte r, byte g, byte b, byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GetRGB(uint pixel, ref SDL_PixelFormatDetails format, ref SDL_Palette palette, ref byte r, ref byte g, ref byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GetRGBA(uint pixel, ref SDL_PixelFormatDetails format, ref SDL_Palette palette, ref byte r, ref byte g, ref byte b, ref byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_rect.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Point
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FPoint
    {
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Rect
    {
        public int x;
        public int y;
        public int w;
        public int h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FRect
    {
        public float x;
        public float y;
        public float w;
        public float h;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_RectToFRect(ref SDL_Rect rect, ref SDL_FRect frect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PointInRect(ref SDL_Point p, ref SDL_Rect r); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RectEmpty(ref SDL_Rect r); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RectsEqual(ref SDL_Rect a, ref SDL_Rect b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasRectIntersection(ref SDL_Rect A, ref SDL_Rect B); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectIntersection(ref SDL_Rect A, ref SDL_Rect B, ref SDL_Rect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectUnion(ref SDL_Rect A, ref SDL_Rect B, ref SDL_Rect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectEnclosingPoints(ref SDL_Point points, int count, ref SDL_Rect clip, ref SDL_Rect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectAndLineIntersection(ref SDL_Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PointInRectFloat(ref SDL_FPoint p, ref SDL_FRect r); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RectEmptyFloat(ref SDL_FRect r); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RectsEqualEpsilon(ref SDL_FRect a, ref SDL_FRect b, float epsilon); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RectsEqualFloat(ref SDL_FRect a, ref SDL_FRect b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasRectIntersectionFloat(ref SDL_FRect A, ref SDL_FRect B); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectIntersectionFloat(ref SDL_FRect A, ref SDL_FRect B, ref SDL_FRect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectUnionFloat(ref SDL_FRect A, ref SDL_FRect B, ref SDL_FRect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectEnclosingPointsFloat(ref SDL_FPoint points, int count, ref SDL_FRect clip, ref SDL_FRect result); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRectAndLineIntersectionFloat(ref SDL_FRect rect, ref float X1, ref float Y1, ref float X2, ref float Y2); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_surface.h

    [Flags]
    public enum SDL_SurfaceFlags : uint
    {
        SDL_SURFACE_PREALLOCATED = 0x1,
        SDL_SURFACE_LOCK_NEEDED = 0x2,
        SDL_SURFACE_LOCKED = 0x4,
        SDL_SURFACE_SIMD_ALIGNED = 0x08,
    }

    public enum SDL_ScaleMode
    {
        SDL_SCALEMODE_NEAREST = 0,
        SDL_SCALEMODE_LINEAR = 1,
    }

    public enum SDL_FlipMode
    {
        SDL_FLIP_NONE = 0,
        SDL_FLIP_HORIZONTAL = 1,
        SDL_FLIP_VERTICAL = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Surface
    {
        public uint flags;
        public SDL_PixelFormat format;
        public int w;
        public int h;
        public int pitch;
        public IntPtr pixels;
        public int refcount;
        public IntPtr @internal;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSurface(int width, int height, SDL_PixelFormat format);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSurfaceFrom(int width, int height, SDL_PixelFormat format, IntPtr pixels, int pitch);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroySurface(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetSurfaceProperties(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceColorspace(ref SDL_Surface surface, SDL_Colorspace colorspace); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_Colorspace SDL_GetSurfaceColorspace(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSurfacePalette(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfacePalette(ref SDL_Surface surface, ref SDL_Palette palette); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSurfacePalette(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AddSurfaceAlternateImage(ref SDL_Surface surface, ref SDL_Surface image); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SurfaceHasAlternateImages(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSurfaceImages(ref SDL_Surface surface, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_RemoveSurfaceAlternateImages(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LockSurface(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockSurface(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_LoadBMP_IO(IntPtr src, bool closeio);

    [DllImport(nativeLibName, EntryPoint = "SDL_LoadBMP", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_LoadBMP(byte* file);
    public static IntPtr SDL_LoadBMP(string file)
    {
        var fileUTF8 = EncodeAsUTF8(file);

        var result = INTERNAL_SDL_LoadBMP(fileUTF8);

        SDL_free((IntPtr)fileUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SaveBMP_IO(ref SDL_Surface surface, IntPtr dst, bool closeio); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_SaveBMP", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SaveBMP(ref SDL_Surface surface, byte* file); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_SaveBMP(ref SDL_Surface surface, string file)
    {
        var fileUTF8 = EncodeAsUTF8(file);

        var result = INTERNAL_SDL_SaveBMP(ref surface, fileUTF8);

        SDL_free((IntPtr)fileUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceRLE(ref SDL_Surface surface, bool enabled); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SurfaceHasRLE(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceColorKey(ref SDL_Surface surface, bool enabled, uint key); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SurfaceHasColorKey(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSurfaceColorKey(ref SDL_Surface surface, ref uint key); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceColorMod(ref SDL_Surface surface, byte r, byte g, byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSurfaceColorMod(ref SDL_Surface surface, ref byte r, ref byte g, ref byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceAlphaMod(ref SDL_Surface surface, byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSurfaceAlphaMod(ref SDL_Surface surface, ref byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceBlendMode(ref SDL_Surface surface, uint blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSurfaceBlendMode(ref SDL_Surface surface, IntPtr blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetSurfaceClipRect(ref SDL_Surface surface, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSurfaceClipRect(ref SDL_Surface surface, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FlipSurface(ref SDL_Surface surface, SDL_FlipMode flip); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_DuplicateSurface(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_ScaleSurface(ref SDL_Surface surface, int width, int height, SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_ConvertSurface(ref SDL_Surface surface, SDL_PixelFormat format); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_ConvertSurfaceAndColorspace(ref SDL_Surface surface, SDL_PixelFormat format, ref SDL_Palette palette, SDL_Colorspace colorspace, uint props); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, uint src_properties, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, uint dst_properties, IntPtr dst, int dst_pitch);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch, bool linear);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PremultiplySurfaceAlpha(ref SDL_Surface surface, bool linear); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClearSurface(ref SDL_Surface surface, float r, float g, float b, float a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FillSurfaceRect(ref SDL_Surface dst, ref SDL_Rect rect, uint color); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FillSurfaceRects(ref SDL_Surface dst, ref SDL_Rect rects, int count, uint color); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurface(ref SDL_Surface src, ref SDL_Rect srcrect, ref SDL_Surface dst, ref SDL_Rect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurfaceUnchecked(ref SDL_Surface src, ref SDL_Rect srcrect, ref SDL_Surface dst, ref SDL_Rect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurfaceScaled(ref SDL_Surface src, ref SDL_Rect srcrect, ref SDL_Surface dst, ref SDL_Rect dstrect, SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurfaceUncheckedScaled(ref SDL_Surface src, ref SDL_Rect srcrect, ref SDL_Surface dst, ref SDL_Rect dstrect, SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurfaceTiled(ref SDL_Surface src, ref SDL_Rect srcrect, ref SDL_Surface dst, ref SDL_Rect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurfaceTiledWithScale(ref SDL_Surface src, ref SDL_Rect srcrect, float scale, SDL_ScaleMode scaleMode, ref SDL_Surface dst, ref SDL_Rect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_BlitSurface9Grid(ref SDL_Surface src, ref SDL_Rect srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, ref SDL_Surface dst, ref SDL_Rect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_MapSurfaceRGB(ref SDL_Surface surface, byte r, byte g, byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_MapSurfaceRGBA(ref SDL_Surface surface, byte r, byte g, byte b, byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadSurfacePixel(ref SDL_Surface surface, int x, int y, ref byte r, ref byte g, ref byte b, ref byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReadSurfacePixelFloat(ref SDL_Surface surface, int x, int y, ref float r, ref float g, ref float b, ref float a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteSurfacePixel(ref SDL_Surface surface, int x, int y, byte r, byte g, byte b, byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WriteSurfacePixelFloat(ref SDL_Surface surface, int x, int y, float r, float g, float b, float a); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_camera.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CameraSpec
    {
        public SDL_PixelFormat format;
        public SDL_Colorspace colorspace;
        public int width;
        public int height;
        public int framerate_numerator;
        public int framerate_denominator;
    }

    public enum SDL_CameraPosition
    {
        SDL_CAMERA_POSITION_UNKNOWN = 0,
        SDL_CAMERA_POSITION_FRONT_FACING = 1,
        SDL_CAMERA_POSITION_BACK_FACING = 2,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumCameraDrivers();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCameraDriver(int index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCurrentCameraDriver();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCameras(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCameraSupportedFormats(uint devid, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCameraName(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_CameraPosition SDL_GetCameraPosition(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenCamera(uint instance_id, ref SDL_CameraSpec spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetCameraPermissionState(IntPtr camera);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetCameraID(IntPtr camera);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetCameraProperties(IntPtr camera);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetCameraFormat(IntPtr camera, ref SDL_CameraSpec spec); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_AcquireCameraFrame(IntPtr camera, ref ulong timestampNS); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseCameraFrame(IntPtr camera, ref SDL_Surface frame); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseCamera(IntPtr camera);

    // ../SDL3/SDL_clipboard.h

    [DllImport(nativeLibName, EntryPoint = "SDL_SetClipboardText", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetClipboardText(byte* text);
    public static bool SDL_SetClipboardText(string text)
    {
        var textUTF8 = EncodeAsUTF8(text);

        var result = INTERNAL_SDL_SetClipboardText(textUTF8);

        SDL_free((IntPtr)textUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetClipboardText();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasClipboardText();

    [DllImport(nativeLibName, EntryPoint = "SDL_SetPrimarySelectionText", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetPrimarySelectionText(byte* text);
    public static bool SDL_SetPrimarySelectionText(string text)
    {
        var textUTF8 = EncodeAsUTF8(text);

        var result = INTERNAL_SDL_SetPrimarySelectionText(textUTF8);

        SDL_free((IntPtr)textUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetPrimarySelectionText();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasPrimarySelectionText();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SDL_ClipboardDataCallback(IntPtr userdata, IntPtr mimeType, UIntPtr size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_ClipboardCleanupCallback(IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetClipboardData(/* SDL_ClipboardDataCallback */ IntPtr callback, /* SDL_ClipboardCleanupCallback */ IntPtr cleanup, IntPtr userdata, ref IntPtr mime_types, UIntPtr num_mime_types); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClearClipboardData();

    [DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardData", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetClipboardData(byte* mime_type, ref UIntPtr size); // WARN_UNKNOWN_POINTER_PARAMETER
    public static IntPtr SDL_GetClipboardData(string mime_type, ref UIntPtr size)
    {
        var mime_typeUTF8 = EncodeAsUTF8(mime_type);

        var result = INTERNAL_SDL_GetClipboardData(mime_typeUTF8, ref size);

        SDL_free((IntPtr)mime_typeUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_HasClipboardData", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_HasClipboardData(byte* mime_type);
    public static bool SDL_HasClipboardData(string mime_type)
    {
        var mime_typeUTF8 = EncodeAsUTF8(mime_type);

        var result = INTERNAL_SDL_HasClipboardData(mime_typeUTF8);

        SDL_free((IntPtr)mime_typeUTF8);

        return result;
    }

    // ../SDL3/SDL_cpuinfo.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetCPUCount();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetCPUCacheLineSize();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasAltiVec();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasMMX();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasSSE();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasSSE2();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasSSE3();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasSSE41();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasSSE42();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasAVX();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasAVX2();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasAVX512F();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasARMSIMD();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasNEON();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasLSX();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasLASX();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetSystemRAM();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern UIntPtr SDL_GetSIMDAlignment();

    // ../SDL3/SDL_video.h

    public enum SDL_SystemTheme
    {
        SDL_SYSTEM_THEME_UNKNOWN = 0,
        SDL_SYSTEM_THEME_LIGHT = 1,
        SDL_SYSTEM_THEME_DARK = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DisplayMode
    {
        public uint displayID;
        public SDL_PixelFormat format;
        public int w;
        public int h;
        public float pixel_density;
        public float refresh_rate;
        public int refresh_rate_numerator;
        public int refresh_rate_denominator;
        public IntPtr @internal;
    }

    public enum SDL_DisplayOrientation
    {
        SDL_ORIENTATION_UNKNOWN = 0,
        SDL_ORIENTATION_LANDSCAPE = 1,
        SDL_ORIENTATION_LANDSCAPE_FLIPPED = 2,
        SDL_ORIENTATION_PORTRAIT = 3,
        SDL_ORIENTATION_PORTRAIT_FLIPPED = 4,
    }

    [Flags]
    public enum SDL_WindowFlags : ulong
    {
        SDL_WINDOW_FULLSCREEN = 0x1,
        SDL_WINDOW_OPENGL = 0x2,
        SDL_WINDOW_OCCLUDED = 0x4,
        SDL_WINDOW_HIDDEN = 0x08,
        SDL_WINDOW_BORDERLESS = 0x10,
        SDL_WINDOW_RESIZABLE = 0x20,
        SDL_WINDOW_MINIMIZED = 0x40,
        SDL_WINDOW_MAXIMIZED = 0x080,
        SDL_WINDOW_MOUSE_GRABBED = 0x100,
        SDL_WINDOW_INPUT_FOCUS = 0x200,
        SDL_WINDOW_MOUSE_FOCUS = 0x400,
        SDL_WINDOW_EXTERNAL = 0x0800,
        SDL_WINDOW_MODAL = 0x1000,
        SDL_WINDOW_HIGH_PIXEL_DENSITY = 0x2000,
        SDL_WINDOW_MOUSE_CAPTURE = 0x4000,
        SDL_WINDOW_MOUSE_RELATIVE_MODE = 0x08000,
        SDL_WINDOW_ALWAYS_ON_TOP = 0x10000,
        SDL_WINDOW_UTILITY = 0x20000,
        SDL_WINDOW_TOOLTIP = 0x40000,
        SDL_WINDOW_POPUP_MENU = 0x080000,
        SDL_WINDOW_KEYBOARD_GRABBED = 0x100000,
        SDL_WINDOW_VULKAN = 0x1000_0000,
        SDL_WINDOW_METAL = 0x2000_0000,
        SDL_WINDOW_TRANSPARENT = 0x4000_0000,
        SDL_WINDOW_NOT_FOCUSABLE = 0x0_8000_0000,
    }

    public enum SDL_FlashOperation
    {
        SDL_FLASH_CANCEL = 0,
        SDL_FLASH_BRIEFLY = 1,
        SDL_FLASH_UNTIL_FOCUSED = 2,
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SDL_EGLAttribArrayCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SDL_EGLIntArrayCallback();

    public enum SDL_GLattr
    {
        SDL_GL_RED_SIZE = 0,
        SDL_GL_GREEN_SIZE = 1,
        SDL_GL_BLUE_SIZE = 2,
        SDL_GL_ALPHA_SIZE = 3,
        SDL_GL_BUFFER_SIZE = 4,
        SDL_GL_DOUBLEBUFFER = 5,
        SDL_GL_DEPTH_SIZE = 6,
        SDL_GL_STENCIL_SIZE = 7,
        SDL_GL_ACCUM_RED_SIZE = 8,
        SDL_GL_ACCUM_GREEN_SIZE = 9,
        SDL_GL_ACCUM_BLUE_SIZE = 10,
        SDL_GL_ACCUM_ALPHA_SIZE = 11,
        SDL_GL_STEREO = 12,
        SDL_GL_MULTISAMPLEBUFFERS = 13,
        SDL_GL_MULTISAMPLESAMPLES = 14,
        SDL_GL_ACCELERATED_VISUAL = 15,
        SDL_GL_RETAINED_BACKING = 16,
        SDL_GL_CONTEXT_MAJOR_VERSION = 17,
        SDL_GL_CONTEXT_MINOR_VERSION = 18,
        SDL_GL_CONTEXT_FLAGS = 19,
        SDL_GL_CONTEXT_PROFILE_MASK = 20,
        SDL_GL_SHARE_WITH_CURRENT_CONTEXT = 21,
        SDL_GL_FRAMEBUFFER_SRGB_CAPABLE = 22,
        SDL_GL_CONTEXT_RELEASE_BEHAVIOR = 23,
        SDL_GL_CONTEXT_RESET_NOTIFICATION = 24,
        SDL_GL_CONTEXT_NO_ERROR = 25,
        SDL_GL_FLOATBUFFERS = 26,
        SDL_GL_EGL_PLATFORM = 27,
    }

    public enum SDL_GLprofile
    {
        SDL_GL_CONTEXT_PROFILE_CORE = 1,
        SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 2,
        SDL_GL_CONTEXT_PROFILE_ES = 4,
    }

    public enum SDL_GLcontextFlag
    {
        SDL_GL_CONTEXT_DEBUG_FLAG = 1,
        SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 2,
        SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG = 4,
        SDL_GL_CONTEXT_RESET_ISOLATION_FLAG = 8,
    }

    public enum SDL_GLcontextReleaseFlag
    {
        SDL_GL_CONTEXT_RELEASE_BEHAVIOR_NONE = 0,
        SDL_GL_CONTEXT_RELEASE_BEHAVIOR_FLUSH = 1,
    }

    public enum SDL_GLContextResetNotification
    {
        SDL_GL_CONTEXT_RESET_NO_NOTIFICATION = 0,
        SDL_GL_CONTEXT_RESET_LOSE_CONTEXT = 1,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumVideoDrivers();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetVideoDriver(int index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCurrentVideoDriver();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_SystemTheme SDL_GetSystemTheme();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetDisplays(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetPrimaryDisplay();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetDisplayProperties(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetDisplayName(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetDisplayBounds(uint displayID, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetDisplayUsableBounds(uint displayID, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetDisplayContentScale(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetFullscreenDisplayModes(uint displayID, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refresh_rate, bool include_high_density_modes, ref SDL_DisplayMode mode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetDesktopDisplayMode(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCurrentDisplayMode(uint displayID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetDisplayForPoint(ref SDL_Point point); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetDisplayForRect(ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetDisplayForWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetWindowPixelDensity(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetWindowDisplayScale(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowFullscreenMode(IntPtr window, ref SDL_DisplayMode mode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowFullscreenMode(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowICCProfile(IntPtr window, ref UIntPtr size); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_PixelFormat SDL_GetWindowPixelFormat(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindows(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateWindow", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_CreateWindow(byte* title, int w, int h, ulong flags);
    public static IntPtr SDL_CreateWindow(string title, int w, int h, ulong flags)
    {
        var titleUTF8 = EncodeAsUTF8(title);

        var result = INTERNAL_SDL_CreateWindow(titleUTF8, w, h, flags);

        SDL_free((IntPtr)titleUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreatePopupWindow(IntPtr parent, int offset_x, int offset_y, int w, int h, ulong flags);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateWindowWithProperties(uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetWindowID(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowFromID(uint id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowParent(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetWindowProperties(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetWindowFlags(IntPtr window);

    [DllImport(nativeLibName, EntryPoint = "SDL_SetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetWindowTitle(IntPtr window, byte* title);
    public static bool SDL_SetWindowTitle(IntPtr window, string title)
    {
        var titleUTF8 = EncodeAsUTF8(title);

        var result = INTERNAL_SDL_SetWindowTitle(window, titleUTF8);

        SDL_free((IntPtr)titleUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowTitle(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowIcon(IntPtr window, ref SDL_Surface icon); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowPosition(IntPtr window, int x, int y);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowPosition(IntPtr window, ref int x, ref int y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowSize(IntPtr window, int w, int h);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowSize(IntPtr window, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowSafeArea(IntPtr window, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowAspectRatio(IntPtr window, float min_aspect, float max_aspect);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowAspectRatio(IntPtr window, ref float min_aspect, ref float max_aspect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowBordersSize(IntPtr window, ref int top, ref int left, ref int bottom, ref int right); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowSizeInPixels(IntPtr window, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowMinimumSize(IntPtr window, int min_w, int min_h);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowMinimumSize(IntPtr window, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowMaximumSize(IntPtr window, int max_w, int max_h);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowMaximumSize(IntPtr window, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowBordered(IntPtr window, bool bordered);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowResizable(IntPtr window, bool resizable);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowAlwaysOnTop(IntPtr window, bool on_top);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ShowWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HideWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RaiseWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_MaximizeWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_MinimizeWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RestoreWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowFullscreen(IntPtr window, bool fullscreen);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SyncWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WindowHasSurface(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowSurface(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowSurfaceVSync(IntPtr window, int vsync);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowSurfaceVSync(IntPtr window, ref int vsync); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateWindowSurface(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateWindowSurfaceRects(IntPtr window, ref SDL_Rect rects, int numrects); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_DestroyWindowSurface(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowKeyboardGrab(IntPtr window, bool grabbed);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowMouseGrab(IntPtr window, bool grabbed);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowKeyboardGrab(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowMouseGrab(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGrabbedWindow();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowMouseRect(IntPtr window, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowMouseRect(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowOpacity(IntPtr window, float opacity);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetWindowOpacity(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowParent(IntPtr window, IntPtr parent);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowModal(IntPtr window, bool modal);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowFocusable(IntPtr window, bool focusable);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ShowWindowSystemMenu(IntPtr window, int x, int y);

    public enum SDL_HitTestResult
    {
        SDL_HITTEST_NORMAL = 0,
        SDL_HITTEST_DRAGGABLE = 1,
        SDL_HITTEST_RESIZE_TOPLEFT = 2,
        SDL_HITTEST_RESIZE_TOP = 3,
        SDL_HITTEST_RESIZE_TOPRIGHT = 4,
        SDL_HITTEST_RESIZE_RIGHT = 5,
        SDL_HITTEST_RESIZE_BOTTOMRIGHT = 6,
        SDL_HITTEST_RESIZE_BOTTOM = 7,
        SDL_HITTEST_RESIZE_BOTTOMLEFT = 8,
        SDL_HITTEST_RESIZE_LEFT = 9,
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_HitTestResult SDL_HitTest(IntPtr win, ref SDL_Point area, IntPtr data);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowHitTest(IntPtr window, /* SDL_HitTest */ IntPtr callback, IntPtr callback_data);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowShape(IntPtr window, ref SDL_Surface shape); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FlashWindow(IntPtr window, SDL_FlashOperation operation);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ScreenSaverEnabled();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_EnableScreenSaver();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_DisableScreenSaver();

    [DllImport(nativeLibName, EntryPoint = "SDL_GL_LoadLibrary", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GL_LoadLibrary(byte* path);
    public static bool SDL_GL_LoadLibrary(string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_GL_LoadLibrary(pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GL_GetProcAddress(byte* proc);
    public static IntPtr SDL_GL_GetProcAddress(string proc)
    {
        var procUTF8 = EncodeAsUTF8(proc);

        var result = INTERNAL_SDL_GL_GetProcAddress(procUTF8);

        SDL_free((IntPtr)procUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_EGL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_EGL_GetProcAddress(byte* proc);
    public static IntPtr SDL_EGL_GetProcAddress(string proc)
    {
        var procUTF8 = EncodeAsUTF8(proc);

        var result = INTERNAL_SDL_EGL_GetProcAddress(procUTF8);

        SDL_free((IntPtr)procUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GL_UnloadLibrary();

    [DllImport(nativeLibName, EntryPoint = "SDL_GL_ExtensionSupported", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GL_ExtensionSupported(byte* extension);
    public static bool SDL_GL_ExtensionSupported(string extension)
    {
        var extensionUTF8 = EncodeAsUTF8(extension);

        var result = INTERNAL_SDL_GL_ExtensionSupported(extensionUTF8);

        SDL_free((IntPtr)extensionUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GL_ResetAttributes();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_SetAttribute(SDL_GLattr attr, int value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_GetAttribute(SDL_GLattr attr, ref int value); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GL_CreateContext(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_MakeCurrent(IntPtr window, IntPtr context);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GL_GetCurrentWindow();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GL_GetCurrentContext();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_EGL_GetCurrentDisplay();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_EGL_GetCurrentConfig();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_EGL_GetWindowSurface(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_EGL_SetAttributeCallbacks(/* SDL_EGLAttribArrayCallback */ IntPtr platformAttribCallback, /* SDL_EGLIntArrayCallback */ IntPtr surfaceAttribCallback, /* SDL_EGLIntArrayCallback */ IntPtr contextAttribCallback);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_SetSwapInterval(int interval);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_GetSwapInterval(ref int interval); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_SwapWindow(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GL_DestroyContext(IntPtr context);

    // ../SDL3/SDL_dialog.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DialogFileFilter
    {
        public char* name;
        public char* pattern;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_DialogFileCallback(IntPtr userdata, IntPtr filelist, int filter);

    [DllImport(nativeLibName, EntryPoint = "SDL_ShowOpenFileDialog", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_ShowOpenFileDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, ref SDL_DialogFileFilter filters, int nfilters, byte* default_location, bool allow_many); // WARN_UNKNOWN_POINTER_PARAMETER
    public static void SDL_ShowOpenFileDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, ref SDL_DialogFileFilter filters, int nfilters, string default_location, bool allow_many)
    {
        var default_locationUTF8 = EncodeAsUTF8(default_location);

        INTERNAL_SDL_ShowOpenFileDialog(callback, userdata, window, ref filters, nfilters, default_locationUTF8, allow_many);

        SDL_free((IntPtr)default_locationUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ShowSaveFileDialog", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_ShowSaveFileDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, ref SDL_DialogFileFilter filters, int nfilters, byte* default_location); // WARN_UNKNOWN_POINTER_PARAMETER
    public static void SDL_ShowSaveFileDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, ref SDL_DialogFileFilter filters, int nfilters, string default_location)
    {
        var default_locationUTF8 = EncodeAsUTF8(default_location);

        INTERNAL_SDL_ShowSaveFileDialog(callback, userdata, window, ref filters, nfilters, default_locationUTF8);

        SDL_free((IntPtr)default_locationUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ShowOpenFolderDialog", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_ShowOpenFolderDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, byte* default_location, bool allow_many);
    public static void SDL_ShowOpenFolderDialog(/* SDL_DialogFileCallback */ IntPtr callback, IntPtr userdata, IntPtr window, string default_location, bool allow_many)
    {
        var default_locationUTF8 = EncodeAsUTF8(default_location);

        INTERNAL_SDL_ShowOpenFolderDialog(callback, userdata, window, default_locationUTF8, allow_many);

        SDL_free((IntPtr)default_locationUTF8);

    }

    // ../SDL3/SDL_guid.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GUID
    {
        public byte data0;
        public byte data1;
        public byte data2;
        public byte data3;
        public byte data4;
        public byte data5;
        public byte data6;
        public byte data7;
        public byte data8;
        public byte data9;
        public byte data10;
        public byte data11;
        public byte data12;
        public byte data13;
        public byte data14;
        public byte data15;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GUIDToString", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_GUIDToString(SDL_GUID guid, byte* pszGUID, int cbGUID);
    public static void SDL_GUIDToString(SDL_GUID guid, string pszGUID, int cbGUID)
    {
        var pszGUIDUTF8 = EncodeAsUTF8(pszGUID);

        INTERNAL_SDL_GUIDToString(guid, pszGUIDUTF8, cbGUID);

        SDL_free((IntPtr)pszGUIDUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_StringToGUID", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_GUID INTERNAL_SDL_StringToGUID(byte* pchGUID);
    public static SDL_GUID SDL_StringToGUID(string pchGUID)
    {
        var pchGUIDUTF8 = EncodeAsUTF8(pchGUID);

        var result = INTERNAL_SDL_StringToGUID(pchGUIDUTF8);

        SDL_free((IntPtr)pchGUIDUTF8);

        return result;
    }

    // ../SDL3/SDL_power.h

    public enum SDL_PowerState
    {
        SDL_POWERSTATE_ERROR = -1,
        SDL_POWERSTATE_UNKNOWN = 0,
        SDL_POWERSTATE_ON_BATTERY = 1,
        SDL_POWERSTATE_NO_BATTERY = 2,
        SDL_POWERSTATE_CHARGING = 3,
        SDL_POWERSTATE_CHARGED = 4,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_PowerState SDL_GetPowerInfo(ref int seconds, ref int percent); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_sensor.h

    public enum SDL_SensorType
    {
        SDL_SENSOR_INVALID = -1,
        SDL_SENSOR_UNKNOWN = 0,
        SDL_SENSOR_ACCEL = 1,
        SDL_SENSOR_GYRO = 2,
        SDL_SENSOR_ACCEL_L = 3,
        SDL_SENSOR_GYRO_L = 4,
        SDL_SENSOR_ACCEL_R = 5,
        SDL_SENSOR_GYRO_R = 6,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSensors(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSensorNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_SensorType SDL_GetSensorTypeForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetSensorNonPortableTypeForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenSensor(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSensorFromID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetSensorProperties(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetSensorName(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_SensorType SDL_GetSensorType(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetSensorNonPortableType(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetSensorID(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetSensorData(IntPtr sensor, ref float data, int num_values); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseSensor(IntPtr sensor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UpdateSensors();

    // ../SDL3/SDL_joystick.h

    public enum SDL_JoystickType
    {
        SDL_JOYSTICK_TYPE_UNKNOWN = 0,
        SDL_JOYSTICK_TYPE_GAMEPAD = 1,
        SDL_JOYSTICK_TYPE_WHEEL = 2,
        SDL_JOYSTICK_TYPE_ARCADE_STICK = 3,
        SDL_JOYSTICK_TYPE_FLIGHT_STICK = 4,
        SDL_JOYSTICK_TYPE_DANCE_PAD = 5,
        SDL_JOYSTICK_TYPE_GUITAR = 6,
        SDL_JOYSTICK_TYPE_DRUM_KIT = 7,
        SDL_JOYSTICK_TYPE_ARCADE_PAD = 8,
        SDL_JOYSTICK_TYPE_THROTTLE = 9,
    }

    public enum SDL_JoystickConnectionState
    {
        SDL_JOYSTICK_CONNECTION_INVALID = -1,
        SDL_JOYSTICK_CONNECTION_UNKNOWN = 0,
        SDL_JOYSTICK_CONNECTION_WIRED = 1,
        SDL_JOYSTICK_CONNECTION_WIRELESS = 2,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_LockJoysticks();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockJoysticks();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasJoystick();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoysticks(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickPathForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetJoystickPlayerIndexForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GUID SDL_GetJoystickGUIDForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickVendorForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickProductForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickProductVersionForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_JoystickType SDL_GetJoystickTypeForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenJoystick(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickFromID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickFromPlayerIndex(int player_index);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_VirtualJoystickTouchpadDesc
    {
        public ushort nfingers;
        public ushort padding0;
        public ushort padding1;
        public ushort padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_VirtualJoystickSensorDesc
    {
        public SDL_SensorType type;
        public float rate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_VirtualJoystickDesc
    {
        public uint version;
        public ushort type;
        public ushort padding;
        public ushort vendor_id;
        public ushort product_id;
        public ushort naxes;
        public ushort nbuttons;
        public ushort nballs;
        public ushort nhats;
        public ushort ntouchpads;
        public ushort nsensors;
        public ushort padding20;
        public ushort padding21;
        public uint button_mask;
        public uint axis_mask;
        public char* name;
        public SDL_VirtualJoystickTouchpadDesc* touchpads;
        public SDL_VirtualJoystickSensorDesc* sensors;
        public IntPtr userdata;
        public IntPtr Update; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr SetPlayerIndex; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr Rumble; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr RumbleTriggers; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr SetLED; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr SendEffect; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr SetSensorsEnabled; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr Cleanup; // WARN_ANONYMOUS_FUNCTION_POINTER
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_AttachVirtualJoystick(ref SDL_VirtualJoystickDesc desc); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_DetachVirtualJoystick(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_IsJoystickVirtual(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickVirtualButton(IntPtr joystick, int button, bool down);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, byte value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, bool down, float x, float y, float pressure);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SendJoystickVirtualSensorData(IntPtr joystick, SDL_SensorType type, ulong sensor_timestamp, ref float data, int num_values); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetJoystickProperties(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickName(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickPath(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetJoystickPlayerIndex(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickPlayerIndex(IntPtr joystick, int player_index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GUID SDL_GetJoystickGUID(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickVendor(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickProduct(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickProductVersion(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetJoystickSerial(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_JoystickType SDL_GetJoystickType(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GetJoystickGUIDInfo(SDL_GUID guid, ref ushort vendor, ref ushort product, ref ushort version, ref ushort crc16); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_JoystickConnected(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetJoystickID(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumJoystickAxes(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumJoystickBalls(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumJoystickHats(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumJoystickButtons(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetJoystickEventsEnabled(bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_JoystickEventsEnabled();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UpdateJoysticks();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern short SDL_GetJoystickAxis(IntPtr joystick, int axis);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, ref short state); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetJoystickBall(IntPtr joystick, int ball, ref int dx, ref int dy); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte SDL_GetJoystickHat(IntPtr joystick, int hat);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetJoystickButton(IntPtr joystick, int button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RumbleJoystick(IntPtr joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RumbleJoystickTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseJoystick(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, ref int percent); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_gamepad.h

    public enum SDL_GamepadType
    {
        SDL_GAMEPAD_TYPE_UNKNOWN = 0,
        SDL_GAMEPAD_TYPE_STANDARD = 1,
        SDL_GAMEPAD_TYPE_XBOX360 = 2,
        SDL_GAMEPAD_TYPE_XBOXONE = 3,
        SDL_GAMEPAD_TYPE_PS3 = 4,
        SDL_GAMEPAD_TYPE_PS4 = 5,
        SDL_GAMEPAD_TYPE_PS5 = 6,
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO = 7,
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT = 8,
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT = 9,
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR = 10,
        SDL_GAMEPAD_TYPE_MAX = 11,
    }

    public enum SDL_GamepadButton
    {
        SDL_GAMEPAD_BUTTON_INVALID = -1,
        SDL_GAMEPAD_BUTTON_SOUTH = 0,
        SDL_GAMEPAD_BUTTON_EAST = 1,
        SDL_GAMEPAD_BUTTON_WEST = 2,
        SDL_GAMEPAD_BUTTON_NORTH = 3,
        SDL_GAMEPAD_BUTTON_BACK = 4,
        SDL_GAMEPAD_BUTTON_GUIDE = 5,
        SDL_GAMEPAD_BUTTON_START = 6,
        SDL_GAMEPAD_BUTTON_LEFT_STICK = 7,
        SDL_GAMEPAD_BUTTON_RIGHT_STICK = 8,
        SDL_GAMEPAD_BUTTON_LEFT_SHOULDER = 9,
        SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER = 10,
        SDL_GAMEPAD_BUTTON_DPAD_UP = 11,
        SDL_GAMEPAD_BUTTON_DPAD_DOWN = 12,
        SDL_GAMEPAD_BUTTON_DPAD_LEFT = 13,
        SDL_GAMEPAD_BUTTON_DPAD_RIGHT = 14,
        SDL_GAMEPAD_BUTTON_MISC1 = 15,
        SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1 = 16,
        SDL_GAMEPAD_BUTTON_LEFT_PADDLE1 = 17,
        SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2 = 18,
        SDL_GAMEPAD_BUTTON_LEFT_PADDLE2 = 19,
        SDL_GAMEPAD_BUTTON_TOUCHPAD = 20,
        SDL_GAMEPAD_BUTTON_MISC2 = 21,
        SDL_GAMEPAD_BUTTON_MISC3 = 22,
        SDL_GAMEPAD_BUTTON_MISC4 = 23,
        SDL_GAMEPAD_BUTTON_MISC5 = 24,
        SDL_GAMEPAD_BUTTON_MISC6 = 25,
        SDL_GAMEPAD_BUTTON_MAX = 26,
    }

    public enum SDL_GamepadButtonLabel
    {
        SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN = 0,
        SDL_GAMEPAD_BUTTON_LABEL_A = 1,
        SDL_GAMEPAD_BUTTON_LABEL_B = 2,
        SDL_GAMEPAD_BUTTON_LABEL_X = 3,
        SDL_GAMEPAD_BUTTON_LABEL_Y = 4,
        SDL_GAMEPAD_BUTTON_LABEL_CROSS = 5,
        SDL_GAMEPAD_BUTTON_LABEL_CIRCLE = 6,
        SDL_GAMEPAD_BUTTON_LABEL_SQUARE = 7,
        SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE = 8,
    }

    public enum SDL_GamepadAxis
    {
        SDL_GAMEPAD_AXIS_INVALID = -1,
        SDL_GAMEPAD_AXIS_LEFTX = 0,
        SDL_GAMEPAD_AXIS_LEFTY = 1,
        SDL_GAMEPAD_AXIS_RIGHTX = 2,
        SDL_GAMEPAD_AXIS_RIGHTY = 3,
        SDL_GAMEPAD_AXIS_LEFT_TRIGGER = 4,
        SDL_GAMEPAD_AXIS_RIGHT_TRIGGER = 5,
        SDL_GAMEPAD_AXIS_MAX = 6,
    }

    public enum SDL_GamepadBindingType
    {
        SDL_GAMEPAD_BINDTYPE_NONE = 0,
        SDL_GAMEPAD_BINDTYPE_BUTTON = 1,
        SDL_GAMEPAD_BINDTYPE_AXIS = 2,
        SDL_GAMEPAD_BINDTYPE_HAT = 3,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_GamepadBinding
    {
        [FieldOffset(0)]
        public SDL_GamepadBindingType input_type;
        [FieldOffset(4)]
        public int input_button;
        [FieldOffset(4)]
        public INTERNAL_SDL_GamepadBinding_input_axis input_axis;
        [FieldOffset(4)]
        public INTERNAL_SDL_GamepadBinding_input_hat input_hat;
        [FieldOffset(16)]
        public SDL_GamepadBindingType output_type;
        [FieldOffset(20)]
        public SDL_GamepadButton output_button;
        [FieldOffset(20)]
        public INTERNAL_SDL_GamepadBinding_output_axis output_axis;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTERNAL_SDL_GamepadBinding_input_axis
    {
        public int axis;
        public int axis_min;
        public int axis_max;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTERNAL_SDL_GamepadBinding_input_hat
    {
        public int hat;
        public int hat_mask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTERNAL_SDL_GamepadBinding_output_axis
    {
        public SDL_GamepadAxis axis;
        public int axis_min;
        public int axis_max;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_AddGamepadMapping", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_AddGamepadMapping(byte* mapping);
    public static int SDL_AddGamepadMapping(string mapping)
    {
        var mappingUTF8 = EncodeAsUTF8(mapping);

        var result = INTERNAL_SDL_AddGamepadMapping(mappingUTF8);

        SDL_free((IntPtr)mappingUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_AddGamepadMappingsFromIO(IntPtr src, bool closeio);

    [DllImport(nativeLibName, EntryPoint = "SDL_AddGamepadMappingsFromFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_AddGamepadMappingsFromFile(byte* file);
    public static int SDL_AddGamepadMappingsFromFile(string file)
    {
        var fileUTF8 = EncodeAsUTF8(file);

        var result = INTERNAL_SDL_AddGamepadMappingsFromFile(fileUTF8);

        SDL_free((IntPtr)fileUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ReloadGamepadMappings();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadMappings(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadMappingForGUID(SDL_GUID guid);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadMapping(IntPtr gamepad);

    [DllImport(nativeLibName, EntryPoint = "SDL_SetGamepadMapping", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetGamepadMapping(uint instance_id, byte* mapping);
    public static bool SDL_SetGamepadMapping(uint instance_id, string mapping)
    {
        var mappingUTF8 = EncodeAsUTF8(mapping);

        var result = INTERNAL_SDL_SetGamepadMapping(instance_id, mappingUTF8);

        SDL_free((IntPtr)mappingUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasGamepad();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepads(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_IsGamepad(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadPathForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetGamepadPlayerIndexForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GUID SDL_GetGamepadGUIDForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadVendorForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadProductForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadProductVersionForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadType SDL_GetGamepadTypeForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadType SDL_GetRealGamepadTypeForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadMappingForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenGamepad(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadFromID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadFromPlayerIndex(int player_index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetGamepadProperties(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetGamepadID(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadName(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadPath(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadType SDL_GetGamepadType(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadType SDL_GetRealGamepadType(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetGamepadPlayerIndex(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetGamepadPlayerIndex(IntPtr gamepad, int player_index);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadVendor(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadProduct(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadProductVersion(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadSerial(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, ref int percent); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadConnected(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetGamepadEventsEnabled(bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadEventsEnabled();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadBindings(IntPtr gamepad, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UpdateGamepads();

    [DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadTypeFromString", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_GamepadType INTERNAL_SDL_GetGamepadTypeFromString(byte* str);
    public static SDL_GamepadType SDL_GetGamepadTypeFromString(string str)
    {
        var strUTF8 = EncodeAsUTF8(str);

        var result = INTERNAL_SDL_GetGamepadTypeFromString(strUTF8);

        SDL_free((IntPtr)strUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadStringForType(SDL_GamepadType type);

    [DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadAxisFromString", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_GamepadAxis INTERNAL_SDL_GetGamepadAxisFromString(byte* str);
    public static SDL_GamepadAxis SDL_GetGamepadAxisFromString(string str)
    {
        var strUTF8 = EncodeAsUTF8(str);

        var result = INTERNAL_SDL_GetGamepadAxisFromString(strUTF8);

        SDL_free((IntPtr)strUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadHasAxis(IntPtr gamepad, SDL_GamepadAxis axis);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern short SDL_GetGamepadAxis(IntPtr gamepad, SDL_GamepadAxis axis);

    [DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadButtonFromString", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_GamepadButton INTERNAL_SDL_GetGamepadButtonFromString(byte* str);
    public static SDL_GamepadButton SDL_GetGamepadButtonFromString(string str)
    {
        var strUTF8 = EncodeAsUTF8(str);

        var result = INTERNAL_SDL_GetGamepadButtonFromString(strUTF8);

        SDL_free((IntPtr)strUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadStringForButton(SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadHasButton(IntPtr gamepad, SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetGamepadButton(IntPtr gamepad, SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadButtonLabel SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumGamepadTouchpads(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, IntPtr down, ref float x, ref float y, ref float pressure); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadHasSensor(IntPtr gamepad, SDL_SensorType type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetGamepadSensorEnabled(IntPtr gamepad, SDL_SensorType type, bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GamepadSensorEnabled(IntPtr gamepad, SDL_SensorType type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SDL_SensorType type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetGamepadSensorData(IntPtr gamepad, SDL_SensorType type, ref float data, int num_values); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RumbleGamepad(IntPtr gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseGamepad(IntPtr gamepad);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL_GamepadButton button);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL_GamepadAxis axis);

    // ../SDL3/SDL_scancode.h

    public enum SDL_Scancode
    {
        SDL_SCANCODE_UNKNOWN = 0,
        SDL_SCANCODE_A = 4,
        SDL_SCANCODE_B = 5,
        SDL_SCANCODE_C = 6,
        SDL_SCANCODE_D = 7,
        SDL_SCANCODE_E = 8,
        SDL_SCANCODE_F = 9,
        SDL_SCANCODE_G = 10,
        SDL_SCANCODE_H = 11,
        SDL_SCANCODE_I = 12,
        SDL_SCANCODE_J = 13,
        SDL_SCANCODE_K = 14,
        SDL_SCANCODE_L = 15,
        SDL_SCANCODE_M = 16,
        SDL_SCANCODE_N = 17,
        SDL_SCANCODE_O = 18,
        SDL_SCANCODE_P = 19,
        SDL_SCANCODE_Q = 20,
        SDL_SCANCODE_R = 21,
        SDL_SCANCODE_S = 22,
        SDL_SCANCODE_T = 23,
        SDL_SCANCODE_U = 24,
        SDL_SCANCODE_V = 25,
        SDL_SCANCODE_W = 26,
        SDL_SCANCODE_X = 27,
        SDL_SCANCODE_Y = 28,
        SDL_SCANCODE_Z = 29,
        SDL_SCANCODE_1 = 30,
        SDL_SCANCODE_2 = 31,
        SDL_SCANCODE_3 = 32,
        SDL_SCANCODE_4 = 33,
        SDL_SCANCODE_5 = 34,
        SDL_SCANCODE_6 = 35,
        SDL_SCANCODE_7 = 36,
        SDL_SCANCODE_8 = 37,
        SDL_SCANCODE_9 = 38,
        SDL_SCANCODE_0 = 39,
        SDL_SCANCODE_RETURN = 40,
        SDL_SCANCODE_ESCAPE = 41,
        SDL_SCANCODE_BACKSPACE = 42,
        SDL_SCANCODE_TAB = 43,
        SDL_SCANCODE_SPACE = 44,
        SDL_SCANCODE_MINUS = 45,
        SDL_SCANCODE_EQUALS = 46,
        SDL_SCANCODE_LEFTBRACKET = 47,
        SDL_SCANCODE_RIGHTBRACKET = 48,
        SDL_SCANCODE_BACKSLASH = 49,
        SDL_SCANCODE_NONUSHASH = 50,
        SDL_SCANCODE_SEMICOLON = 51,
        SDL_SCANCODE_APOSTROPHE = 52,
        SDL_SCANCODE_GRAVE = 53,
        SDL_SCANCODE_COMMA = 54,
        SDL_SCANCODE_PERIOD = 55,
        SDL_SCANCODE_SLASH = 56,
        SDL_SCANCODE_CAPSLOCK = 57,
        SDL_SCANCODE_F1 = 58,
        SDL_SCANCODE_F2 = 59,
        SDL_SCANCODE_F3 = 60,
        SDL_SCANCODE_F4 = 61,
        SDL_SCANCODE_F5 = 62,
        SDL_SCANCODE_F6 = 63,
        SDL_SCANCODE_F7 = 64,
        SDL_SCANCODE_F8 = 65,
        SDL_SCANCODE_F9 = 66,
        SDL_SCANCODE_F10 = 67,
        SDL_SCANCODE_F11 = 68,
        SDL_SCANCODE_F12 = 69,
        SDL_SCANCODE_PRINTSCREEN = 70,
        SDL_SCANCODE_SCROLLLOCK = 71,
        SDL_SCANCODE_PAUSE = 72,
        SDL_SCANCODE_INSERT = 73,
        SDL_SCANCODE_HOME = 74,
        SDL_SCANCODE_PAGEUP = 75,
        SDL_SCANCODE_DELETE = 76,
        SDL_SCANCODE_END = 77,
        SDL_SCANCODE_PAGEDOWN = 78,
        SDL_SCANCODE_RIGHT = 79,
        SDL_SCANCODE_LEFT = 80,
        SDL_SCANCODE_DOWN = 81,
        SDL_SCANCODE_UP = 82,
        SDL_SCANCODE_NUMLOCKCLEAR = 83,
        SDL_SCANCODE_KP_DIVIDE = 84,
        SDL_SCANCODE_KP_MULTIPLY = 85,
        SDL_SCANCODE_KP_MINUS = 86,
        SDL_SCANCODE_KP_PLUS = 87,
        SDL_SCANCODE_KP_ENTER = 88,
        SDL_SCANCODE_KP_1 = 89,
        SDL_SCANCODE_KP_2 = 90,
        SDL_SCANCODE_KP_3 = 91,
        SDL_SCANCODE_KP_4 = 92,
        SDL_SCANCODE_KP_5 = 93,
        SDL_SCANCODE_KP_6 = 94,
        SDL_SCANCODE_KP_7 = 95,
        SDL_SCANCODE_KP_8 = 96,
        SDL_SCANCODE_KP_9 = 97,
        SDL_SCANCODE_KP_0 = 98,
        SDL_SCANCODE_KP_PERIOD = 99,
        SDL_SCANCODE_NONUSBACKSLASH = 100,
        SDL_SCANCODE_APPLICATION = 101,
        SDL_SCANCODE_POWER = 102,
        SDL_SCANCODE_KP_EQUALS = 103,
        SDL_SCANCODE_F13 = 104,
        SDL_SCANCODE_F14 = 105,
        SDL_SCANCODE_F15 = 106,
        SDL_SCANCODE_F16 = 107,
        SDL_SCANCODE_F17 = 108,
        SDL_SCANCODE_F18 = 109,
        SDL_SCANCODE_F19 = 110,
        SDL_SCANCODE_F20 = 111,
        SDL_SCANCODE_F21 = 112,
        SDL_SCANCODE_F22 = 113,
        SDL_SCANCODE_F23 = 114,
        SDL_SCANCODE_F24 = 115,
        SDL_SCANCODE_EXECUTE = 116,
        SDL_SCANCODE_HELP = 117,
        SDL_SCANCODE_MENU = 118,
        SDL_SCANCODE_SELECT = 119,
        SDL_SCANCODE_STOP = 120,
        SDL_SCANCODE_AGAIN = 121,
        SDL_SCANCODE_UNDO = 122,
        SDL_SCANCODE_CUT = 123,
        SDL_SCANCODE_COPY = 124,
        SDL_SCANCODE_PASTE = 125,
        SDL_SCANCODE_FIND = 126,
        SDL_SCANCODE_MUTE = 127,
        SDL_SCANCODE_VOLUMEUP = 128,
        SDL_SCANCODE_VOLUMEDOWN = 129,
        SDL_SCANCODE_KP_COMMA = 133,
        SDL_SCANCODE_KP_EQUALSAS400 = 134,
        SDL_SCANCODE_INTERNATIONAL1 = 135,
        SDL_SCANCODE_INTERNATIONAL2 = 136,
        SDL_SCANCODE_INTERNATIONAL3 = 137,
        SDL_SCANCODE_INTERNATIONAL4 = 138,
        SDL_SCANCODE_INTERNATIONAL5 = 139,
        SDL_SCANCODE_INTERNATIONAL6 = 140,
        SDL_SCANCODE_INTERNATIONAL7 = 141,
        SDL_SCANCODE_INTERNATIONAL8 = 142,
        SDL_SCANCODE_INTERNATIONAL9 = 143,
        SDL_SCANCODE_LANG1 = 144,
        SDL_SCANCODE_LANG2 = 145,
        SDL_SCANCODE_LANG3 = 146,
        SDL_SCANCODE_LANG4 = 147,
        SDL_SCANCODE_LANG5 = 148,
        SDL_SCANCODE_LANG6 = 149,
        SDL_SCANCODE_LANG7 = 150,
        SDL_SCANCODE_LANG8 = 151,
        SDL_SCANCODE_LANG9 = 152,
        SDL_SCANCODE_ALTERASE = 153,
        SDL_SCANCODE_SYSREQ = 154,
        SDL_SCANCODE_CANCEL = 155,
        SDL_SCANCODE_CLEAR = 156,
        SDL_SCANCODE_PRIOR = 157,
        SDL_SCANCODE_RETURN2 = 158,
        SDL_SCANCODE_SEPARATOR = 159,
        SDL_SCANCODE_OUT = 160,
        SDL_SCANCODE_OPER = 161,
        SDL_SCANCODE_CLEARAGAIN = 162,
        SDL_SCANCODE_CRSEL = 163,
        SDL_SCANCODE_EXSEL = 164,
        SDL_SCANCODE_KP_00 = 176,
        SDL_SCANCODE_KP_000 = 177,
        SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
        SDL_SCANCODE_DECIMALSEPARATOR = 179,
        SDL_SCANCODE_CURRENCYUNIT = 180,
        SDL_SCANCODE_CURRENCYSUBUNIT = 181,
        SDL_SCANCODE_KP_LEFTPAREN = 182,
        SDL_SCANCODE_KP_RIGHTPAREN = 183,
        SDL_SCANCODE_KP_LEFTBRACE = 184,
        SDL_SCANCODE_KP_RIGHTBRACE = 185,
        SDL_SCANCODE_KP_TAB = 186,
        SDL_SCANCODE_KP_BACKSPACE = 187,
        SDL_SCANCODE_KP_A = 188,
        SDL_SCANCODE_KP_B = 189,
        SDL_SCANCODE_KP_C = 190,
        SDL_SCANCODE_KP_D = 191,
        SDL_SCANCODE_KP_E = 192,
        SDL_SCANCODE_KP_F = 193,
        SDL_SCANCODE_KP_XOR = 194,
        SDL_SCANCODE_KP_POWER = 195,
        SDL_SCANCODE_KP_PERCENT = 196,
        SDL_SCANCODE_KP_LESS = 197,
        SDL_SCANCODE_KP_GREATER = 198,
        SDL_SCANCODE_KP_AMPERSAND = 199,
        SDL_SCANCODE_KP_DBLAMPERSAND = 200,
        SDL_SCANCODE_KP_VERTICALBAR = 201,
        SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
        SDL_SCANCODE_KP_COLON = 203,
        SDL_SCANCODE_KP_HASH = 204,
        SDL_SCANCODE_KP_SPACE = 205,
        SDL_SCANCODE_KP_AT = 206,
        SDL_SCANCODE_KP_EXCLAM = 207,
        SDL_SCANCODE_KP_MEMSTORE = 208,
        SDL_SCANCODE_KP_MEMRECALL = 209,
        SDL_SCANCODE_KP_MEMCLEAR = 210,
        SDL_SCANCODE_KP_MEMADD = 211,
        SDL_SCANCODE_KP_MEMSUBTRACT = 212,
        SDL_SCANCODE_KP_MEMMULTIPLY = 213,
        SDL_SCANCODE_KP_MEMDIVIDE = 214,
        SDL_SCANCODE_KP_PLUSMINUS = 215,
        SDL_SCANCODE_KP_CLEAR = 216,
        SDL_SCANCODE_KP_CLEARENTRY = 217,
        SDL_SCANCODE_KP_BINARY = 218,
        SDL_SCANCODE_KP_OCTAL = 219,
        SDL_SCANCODE_KP_DECIMAL = 220,
        SDL_SCANCODE_KP_HEXADECIMAL = 221,
        SDL_SCANCODE_LCTRL = 224,
        SDL_SCANCODE_LSHIFT = 225,
        SDL_SCANCODE_LALT = 226,
        SDL_SCANCODE_LGUI = 227,
        SDL_SCANCODE_RCTRL = 228,
        SDL_SCANCODE_RSHIFT = 229,
        SDL_SCANCODE_RALT = 230,
        SDL_SCANCODE_RGUI = 231,
        SDL_SCANCODE_MODE = 257,
        SDL_SCANCODE_SLEEP = 258,
        SDL_SCANCODE_WAKE = 259,
        SDL_SCANCODE_CHANNEL_INCREMENT = 260,
        SDL_SCANCODE_CHANNEL_DECREMENT = 261,
        SDL_SCANCODE_MEDIA_PLAY = 262,
        SDL_SCANCODE_MEDIA_PAUSE = 263,
        SDL_SCANCODE_MEDIA_RECORD = 264,
        SDL_SCANCODE_MEDIA_FAST_FORWARD = 265,
        SDL_SCANCODE_MEDIA_REWIND = 266,
        SDL_SCANCODE_MEDIA_NEXT_TRACK = 267,
        SDL_SCANCODE_MEDIA_PREVIOUS_TRACK = 268,
        SDL_SCANCODE_MEDIA_STOP = 269,
        SDL_SCANCODE_MEDIA_EJECT = 270,
        SDL_SCANCODE_MEDIA_PLAY_PAUSE = 271,
        SDL_SCANCODE_MEDIA_SELECT = 272,
        SDL_SCANCODE_AC_NEW = 273,
        SDL_SCANCODE_AC_OPEN = 274,
        SDL_SCANCODE_AC_CLOSE = 275,
        SDL_SCANCODE_AC_EXIT = 276,
        SDL_SCANCODE_AC_SAVE = 277,
        SDL_SCANCODE_AC_PRINT = 278,
        SDL_SCANCODE_AC_PROPERTIES = 279,
        SDL_SCANCODE_AC_SEARCH = 280,
        SDL_SCANCODE_AC_HOME = 281,
        SDL_SCANCODE_AC_BACK = 282,
        SDL_SCANCODE_AC_FORWARD = 283,
        SDL_SCANCODE_AC_STOP = 284,
        SDL_SCANCODE_AC_REFRESH = 285,
        SDL_SCANCODE_AC_BOOKMARKS = 286,
        SDL_SCANCODE_SOFTLEFT = 287,
        SDL_SCANCODE_SOFTRIGHT = 288,
        SDL_SCANCODE_CALL = 289,
        SDL_SCANCODE_ENDCALL = 290,
        SDL_SCANCODE_RESERVED = 400,
        SDL_NUM_SCANCODES = 512,
    }

    // ../SDL3/SDL_keycode.h

    // ../SDL3/SDL_keyboard.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasKeyboard();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetKeyboards(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetKeyboardNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetKeyboardFocus();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetKeyboardState(ref int numkeys); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ResetKeyboard();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort SDL_GetModState();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetModState(ushort modstate);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetKeyFromScancode(SDL_Scancode scancode, ushort modstate, bool key_event);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_Scancode SDL_GetScancodeFromKey(uint key, IntPtr modstate);

    [DllImport(nativeLibName, EntryPoint = "SDL_SetScancodeName", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetScancodeName(SDL_Scancode scancode, byte* name);
    public static bool SDL_SetScancodeName(SDL_Scancode scancode, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_SetScancodeName(scancode, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetScancodeName(SDL_Scancode scancode);

    [DllImport(nativeLibName, EntryPoint = "SDL_GetScancodeFromName", CallingConvention = CallingConvention.Cdecl)]
    private static extern SDL_Scancode INTERNAL_SDL_GetScancodeFromName(byte* name);
    public static SDL_Scancode SDL_GetScancodeFromName(string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetScancodeFromName(nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetKeyName(uint key);

    [DllImport(nativeLibName, EntryPoint = "SDL_GetKeyFromName", CallingConvention = CallingConvention.Cdecl)]
    private static extern uint INTERNAL_SDL_GetKeyFromName(byte* name);
    public static uint SDL_GetKeyFromName(string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetKeyFromName(nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StartTextInput(IntPtr window);

    public enum SDL_TextInputType
    {
        SDL_TEXTINPUT_TYPE_TEXT = 0,
        SDL_TEXTINPUT_TYPE_TEXT_NAME = 1,
        SDL_TEXTINPUT_TYPE_TEXT_EMAIL = 2,
        SDL_TEXTINPUT_TYPE_TEXT_USERNAME = 3,
        SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN = 4,
        SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE = 5,
        SDL_TEXTINPUT_TYPE_NUMBER = 6,
        SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN = 7,
        SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE = 8,
    }

    public enum SDL_Capitalization
    {
        SDL_CAPITALIZE_NONE = 0,
        SDL_CAPITALIZE_SENTENCES = 1,
        SDL_CAPITALIZE_WORDS = 2,
        SDL_CAPITALIZE_LETTERS = 3,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StartTextInputWithProperties(IntPtr window, uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TextInputActive(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StopTextInput(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClearComposition(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextInputArea(IntPtr window, ref SDL_Rect rect, int cursor); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextInputArea(IntPtr window, ref SDL_Rect rect, ref int cursor); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasScreenKeyboardSupport();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ScreenKeyboardShown(IntPtr window);

    // ../SDL3/SDL_mouse.h

    public enum SDL_SystemCursor
    {
        SDL_SYSTEM_CURSOR_DEFAULT = 0,
        SDL_SYSTEM_CURSOR_TEXT = 1,
        SDL_SYSTEM_CURSOR_WAIT = 2,
        SDL_SYSTEM_CURSOR_CROSSHAIR = 3,
        SDL_SYSTEM_CURSOR_PROGRESS = 4,
        SDL_SYSTEM_CURSOR_NWSE_RESIZE = 5,
        SDL_SYSTEM_CURSOR_NESW_RESIZE = 6,
        SDL_SYSTEM_CURSOR_EW_RESIZE = 7,
        SDL_SYSTEM_CURSOR_NS_RESIZE = 8,
        SDL_SYSTEM_CURSOR_MOVE = 9,
        SDL_SYSTEM_CURSOR_NOT_ALLOWED = 10,
        SDL_SYSTEM_CURSOR_POINTER = 11,
        SDL_SYSTEM_CURSOR_NW_RESIZE = 12,
        SDL_SYSTEM_CURSOR_N_RESIZE = 13,
        SDL_SYSTEM_CURSOR_NE_RESIZE = 14,
        SDL_SYSTEM_CURSOR_E_RESIZE = 15,
        SDL_SYSTEM_CURSOR_SE_RESIZE = 16,
        SDL_SYSTEM_CURSOR_S_RESIZE = 17,
        SDL_SYSTEM_CURSOR_SW_RESIZE = 18,
        SDL_SYSTEM_CURSOR_W_RESIZE = 19,
        SDL_NUM_SYSTEM_CURSORS = 20,
    }

    public enum SDL_MouseWheelDirection
    {
        SDL_MOUSEWHEEL_NORMAL = 0,
        SDL_MOUSEWHEEL_FLIPPED = 1,
    }

    [Flags]
    public enum SDL_MouseButtonFlags : uint
    {
        SDL_BUTTON_LMASK = 0x1,
        SDL_BUTTON_MMASK = 0x2,
        SDL_BUTTON_RMASK = 0x4,
        SDL_BUTTON_X1MASK = 0x08,
        SDL_BUTTON_X2MASK = 0x10,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasMouse();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetMice(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetMouseNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetMouseFocus();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetMouseState(ref float x, ref float y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetGlobalMouseState(ref float x, ref float y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetRelativeMouseState(ref float x, ref float y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WarpMouseInWindow(IntPtr window, float x, float y);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WarpMouseGlobal(float x, float y);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetWindowRelativeMouseMode(IntPtr window, bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetWindowRelativeMouseMode(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_CaptureMouse(bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateCursor(ref byte data, ref byte mask, int w, int h, int hot_x, int hot_y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateColorCursor(ref SDL_Surface surface, int hot_x, int hot_y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSystemCursor(SDL_SystemCursor id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetCursor(IntPtr cursor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetCursor();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetDefaultCursor();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyCursor(IntPtr cursor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ShowCursor();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HideCursor();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_CursorVisible();

    // ../SDL3/SDL_pen.h

    [Flags]
    public enum SDL_PenInputFlags : uint
    {
        SDL_PEN_INPUT_DOWN = 0x1,
        SDL_PEN_INPUT_BUTTON_1 = 0x2,
        SDL_PEN_INPUT_BUTTON_2 = 0x4,
        SDL_PEN_INPUT_BUTTON_3 = 0x08,
        SDL_PEN_INPUT_BUTTON_4 = 0x10,
        SDL_PEN_INPUT_BUTTON_5 = 0x20,
        SDL_PEN_INPUT_ERASER_TIP = 0x4000_0000,
    }

    public enum SDL_PenAxis
    {
        SDL_PEN_AXIS_PRESSURE = 0,
        SDL_PEN_AXIS_XTILT = 1,
        SDL_PEN_AXIS_YTILT = 2,
        SDL_PEN_AXIS_DISTANCE = 3,
        SDL_PEN_AXIS_ROTATION = 4,
        SDL_PEN_AXIS_SLIDER = 5,
        SDL_PEN_AXIS_TANGENTIAL_PRESSURE = 6,
        SDL_PEN_NUM_AXES = 7,
    }

    // ../SDL3/SDL_touch.h

    public enum SDL_TouchDeviceType
    {
        SDL_TOUCH_DEVICE_INVALID = -1,
        SDL_TOUCH_DEVICE_DIRECT = 0,
        SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE = 1,
        SDL_TOUCH_DEVICE_INDIRECT_RELATIVE = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Finger
    {
        public ulong id;
        public float x;
        public float y;
        public float pressure;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetTouchDevices(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetTouchDeviceName(ulong touchID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetTouchFingers(ulong touchID, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_events.h

    public enum SDL_EventType
    {
        SDL_EVENT_FIRST = 0,
        SDL_EVENT_QUIT = 256,
        SDL_EVENT_TERMINATING = 257,
        SDL_EVENT_LOW_MEMORY = 258,
        SDL_EVENT_WILL_ENTER_BACKGROUND = 259,
        SDL_EVENT_DID_ENTER_BACKGROUND = 260,
        SDL_EVENT_WILL_ENTER_FOREGROUND = 261,
        SDL_EVENT_DID_ENTER_FOREGROUND = 262,
        SDL_EVENT_LOCALE_CHANGED = 263,
        SDL_EVENT_SYSTEM_THEME_CHANGED = 264,
        SDL_EVENT_DISPLAY_ORIENTATION = 337,
        SDL_EVENT_DISPLAY_ADDED = 338,
        SDL_EVENT_DISPLAY_REMOVED = 339,
        SDL_EVENT_DISPLAY_MOVED = 340,
        SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED = 341,
        SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED = 342,
        SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED = 343,
        SDL_EVENT_DISPLAY_FIRST = 337,
        SDL_EVENT_DISPLAY_LAST = 343,
        SDL_EVENT_WINDOW_SHOWN = 514,
        SDL_EVENT_WINDOW_HIDDEN = 515,
        SDL_EVENT_WINDOW_EXPOSED = 516,
        SDL_EVENT_WINDOW_MOVED = 517,
        SDL_EVENT_WINDOW_RESIZED = 518,
        SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED = 519,
        SDL_EVENT_WINDOW_METAL_VIEW_RESIZED = 520,
        SDL_EVENT_WINDOW_MINIMIZED = 521,
        SDL_EVENT_WINDOW_MAXIMIZED = 522,
        SDL_EVENT_WINDOW_RESTORED = 523,
        SDL_EVENT_WINDOW_MOUSE_ENTER = 524,
        SDL_EVENT_WINDOW_MOUSE_LEAVE = 525,
        SDL_EVENT_WINDOW_FOCUS_GAINED = 526,
        SDL_EVENT_WINDOW_FOCUS_LOST = 527,
        SDL_EVENT_WINDOW_CLOSE_REQUESTED = 528,
        SDL_EVENT_WINDOW_HIT_TEST = 529,
        SDL_EVENT_WINDOW_ICCPROF_CHANGED = 530,
        SDL_EVENT_WINDOW_DISPLAY_CHANGED = 531,
        SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED = 532,
        SDL_EVENT_WINDOW_SAFE_AREA_CHANGED = 533,
        SDL_EVENT_WINDOW_OCCLUDED = 534,
        SDL_EVENT_WINDOW_ENTER_FULLSCREEN = 535,
        SDL_EVENT_WINDOW_LEAVE_FULLSCREEN = 536,
        SDL_EVENT_WINDOW_DESTROYED = 537,
        SDL_EVENT_WINDOW_HDR_STATE_CHANGED = 538,
        SDL_EVENT_WINDOW_FIRST = 514,
        SDL_EVENT_WINDOW_LAST = 538,
        SDL_EVENT_KEY_DOWN = 768,
        SDL_EVENT_KEY_UP = 769,
        SDL_EVENT_TEXT_EDITING = 770,
        SDL_EVENT_TEXT_INPUT = 771,
        SDL_EVENT_KEYMAP_CHANGED = 772,
        SDL_EVENT_KEYBOARD_ADDED = 773,
        SDL_EVENT_KEYBOARD_REMOVED = 774,
        SDL_EVENT_TEXT_EDITING_CANDIDATES = 775,
        SDL_EVENT_MOUSE_MOTION = 1024,
        SDL_EVENT_MOUSE_BUTTON_DOWN = 1025,
        SDL_EVENT_MOUSE_BUTTON_UP = 1026,
        SDL_EVENT_MOUSE_WHEEL = 1027,
        SDL_EVENT_MOUSE_ADDED = 1028,
        SDL_EVENT_MOUSE_REMOVED = 1029,
        SDL_EVENT_JOYSTICK_AXIS_MOTION = 1536,
        SDL_EVENT_JOYSTICK_BALL_MOTION = 1537,
        SDL_EVENT_JOYSTICK_HAT_MOTION = 1538,
        SDL_EVENT_JOYSTICK_BUTTON_DOWN = 1539,
        SDL_EVENT_JOYSTICK_BUTTON_UP = 1540,
        SDL_EVENT_JOYSTICK_ADDED = 1541,
        SDL_EVENT_JOYSTICK_REMOVED = 1542,
        SDL_EVENT_JOYSTICK_BATTERY_UPDATED = 1543,
        SDL_EVENT_JOYSTICK_UPDATE_COMPLETE = 1544,
        SDL_EVENT_GAMEPAD_AXIS_MOTION = 1616,
        SDL_EVENT_GAMEPAD_BUTTON_DOWN = 1617,
        SDL_EVENT_GAMEPAD_BUTTON_UP = 1618,
        SDL_EVENT_GAMEPAD_ADDED = 1619,
        SDL_EVENT_GAMEPAD_REMOVED = 1620,
        SDL_EVENT_GAMEPAD_REMAPPED = 1621,
        SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN = 1622,
        SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION = 1623,
        SDL_EVENT_GAMEPAD_TOUCHPAD_UP = 1624,
        SDL_EVENT_GAMEPAD_SENSOR_UPDATE = 1625,
        SDL_EVENT_GAMEPAD_UPDATE_COMPLETE = 1626,
        SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED = 1627,
        SDL_EVENT_FINGER_DOWN = 1792,
        SDL_EVENT_FINGER_UP = 1793,
        SDL_EVENT_FINGER_MOTION = 1794,
        SDL_EVENT_CLIPBOARD_UPDATE = 2304,
        SDL_EVENT_DROP_FILE = 4096,
        SDL_EVENT_DROP_TEXT = 4097,
        SDL_EVENT_DROP_BEGIN = 4098,
        SDL_EVENT_DROP_COMPLETE = 4099,
        SDL_EVENT_DROP_POSITION = 4100,
        SDL_EVENT_AUDIO_DEVICE_ADDED = 4352,
        SDL_EVENT_AUDIO_DEVICE_REMOVED = 4353,
        SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED = 4354,
        SDL_EVENT_SENSOR_UPDATE = 4608,
        SDL_EVENT_PEN_PROXIMITY_IN = 4864,
        SDL_EVENT_PEN_PROXIMITY_OUT = 4865,
        SDL_EVENT_PEN_DOWN = 4866,
        SDL_EVENT_PEN_UP = 4867,
        SDL_EVENT_PEN_BUTTON_DOWN = 4868,
        SDL_EVENT_PEN_BUTTON_UP = 4869,
        SDL_EVENT_PEN_MOTION = 4870,
        SDL_EVENT_PEN_AXIS = 4871,
        SDL_EVENT_CAMERA_DEVICE_ADDED = 5120,
        SDL_EVENT_CAMERA_DEVICE_REMOVED = 5121,
        SDL_EVENT_CAMERA_DEVICE_APPROVED = 5122,
        SDL_EVENT_CAMERA_DEVICE_DENIED = 5123,
        SDL_EVENT_RENDER_TARGETS_RESET = 8192,
        SDL_EVENT_RENDER_DEVICE_RESET = 8193,
        SDL_EVENT_POLL_SENTINEL = 32512,
        SDL_EVENT_USER = 32768,
        SDL_EVENT_LAST = 65535,
        SDL_EVENT_ENUM_PADDING = 2147483647,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CommonEvent
    {
        public uint type;
        public uint reserved;
        public ulong timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DisplayEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint displayID;
        public int data1;
        public int data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_WindowEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public int data1;
        public int data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_KeyboardDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_KeyboardEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public SDL_Scancode scancode;
        public uint key;
        public ushort mod;
        public ushort raw;
        public bool down;
        public bool repeat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextEditingEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public char* text;
        public int start;
        public int length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextEditingCandidatesEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public char** candidates;
        public int num_candidates;
        public int selected_candidate;
        public bool horizontal;
        public byte padding1;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextInputEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public char* text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseMotionEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public uint state;
        public float x;
        public float y;
        public float xrel;
        public float yrel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseButtonEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public byte button;
        public bool down;
        public byte clicks;
        public byte padding;
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseWheelEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public float x;
        public float y;
        public SDL_MouseWheelDirection direction;
        public float mouse_x;
        public float mouse_y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyAxisEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte axis;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public short value;
        public ushort padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyBallEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte ball;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public short xrel;
        public short yrel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyHatEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte hat;
        public byte value;
        public byte padding1;
        public byte padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyButtonEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte button;
        public bool down;
        public byte padding1;
        public byte padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyBatteryEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public SDL_PowerState state;
        public int percent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadAxisEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte axis;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public short value;
        public ushort padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadButtonEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public byte button;
        public bool down;
        public byte padding1;
        public byte padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadTouchpadEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public int touchpad;
        public int finger;
        public float x;
        public float y;
        public float pressure;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadSensorEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public int sensor;
        public float data0;
        public float data1;
        public float data2;
        public ulong sensor_timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public bool recording;
        public byte padding1;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CameraDeviceEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TouchFingerEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public ulong touchID;
        public ulong fingerID;
        public float x;
        public float y;
        public float dx;
        public float dy;
        public float pressure;
        public uint windowID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenProximityEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenMotionEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public uint pen_state;
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenTouchEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public uint pen_state;
        public float x;
        public float y;
        public bool eraser;
        public bool down;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenButtonEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public uint pen_state;
        public float x;
        public float y;
        public byte button;
        public bool down;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenAxisEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public uint which;
        public uint pen_state;
        public float x;
        public float y;
        public SDL_PenAxis axis;
        public float value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DropEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public float x;
        public float y;
        public char* source;
        public char* data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_ClipboardEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_SensorEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
        public uint which;
        public float data0;
        public float data1;
        public float data2;
        public float data3;
        public float data4;
        public float data5;
        public ulong sensor_timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_QuitEvent
    {
        public SDL_EventType type;
        public uint reserved;
        public ulong timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_UserEvent
    {
        public uint type;
        public uint reserved;
        public ulong timestamp;
        public uint windowID;
        public int code;
        public IntPtr data1;
        public IntPtr data2;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_Event
    {
        [FieldOffset(0)]
        public uint type;
        [FieldOffset(0)]
        public SDL_CommonEvent common;
        [FieldOffset(0)]
        public SDL_DisplayEvent display;
        [FieldOffset(0)]
        public SDL_WindowEvent window;
        [FieldOffset(0)]
        public SDL_KeyboardDeviceEvent kdevice;
        [FieldOffset(0)]
        public SDL_KeyboardEvent key;
        [FieldOffset(0)]
        public SDL_TextEditingEvent edit;
        [FieldOffset(0)]
        public SDL_TextEditingCandidatesEvent edit_candidates;
        [FieldOffset(0)]
        public SDL_TextInputEvent text;
        [FieldOffset(0)]
        public SDL_MouseDeviceEvent mdevice;
        [FieldOffset(0)]
        public SDL_MouseMotionEvent motion;
        [FieldOffset(0)]
        public SDL_MouseButtonEvent button;
        [FieldOffset(0)]
        public SDL_MouseWheelEvent wheel;
        [FieldOffset(0)]
        public SDL_JoyDeviceEvent jdevice;
        [FieldOffset(0)]
        public SDL_JoyAxisEvent jaxis;
        [FieldOffset(0)]
        public SDL_JoyBallEvent jball;
        [FieldOffset(0)]
        public SDL_JoyHatEvent jhat;
        [FieldOffset(0)]
        public SDL_JoyButtonEvent jbutton;
        [FieldOffset(0)]
        public SDL_JoyBatteryEvent jbattery;
        [FieldOffset(0)]
        public SDL_GamepadDeviceEvent gdevice;
        [FieldOffset(0)]
        public SDL_GamepadAxisEvent gaxis;
        [FieldOffset(0)]
        public SDL_GamepadButtonEvent gbutton;
        [FieldOffset(0)]
        public SDL_GamepadTouchpadEvent gtouchpad;
        [FieldOffset(0)]
        public SDL_GamepadSensorEvent gsensor;
        [FieldOffset(0)]
        public SDL_AudioDeviceEvent adevice;
        [FieldOffset(0)]
        public SDL_CameraDeviceEvent cdevice;
        [FieldOffset(0)]
        public SDL_SensorEvent sensor;
        [FieldOffset(0)]
        public SDL_QuitEvent quit;
        [FieldOffset(0)]
        public SDL_UserEvent user;
        [FieldOffset(0)]
        public SDL_TouchFingerEvent tfinger;
        [FieldOffset(0)]
        public SDL_PenProximityEvent pproximity;
        [FieldOffset(0)]
        public SDL_PenTouchEvent ptouch;
        [FieldOffset(0)]
        public SDL_PenMotionEvent pmotion;
        [FieldOffset(0)]
        public SDL_PenButtonEvent pbutton;
        [FieldOffset(0)]
        public SDL_PenAxisEvent paxis;
        [FieldOffset(0)]
        public SDL_DropEvent drop;
        [FieldOffset(0)]
        public SDL_ClipboardEvent clipboard;
        [FieldOffset(0)]
        public byte padding0;
        [FieldOffset(0)]
        public byte padding1;
        [FieldOffset(0)]
        public byte padding2;
        [FieldOffset(0)]
        public byte padding3;
        [FieldOffset(0)]
        public byte padding4;
        [FieldOffset(0)]
        public byte padding5;
        [FieldOffset(0)]
        public byte padding6;
        [FieldOffset(0)]
        public byte padding7;
        [FieldOffset(0)]
        public byte padding8;
        [FieldOffset(0)]
        public byte padding9;
        [FieldOffset(0)]
        public byte padding10;
        [FieldOffset(0)]
        public byte padding11;
        [FieldOffset(0)]
        public byte padding12;
        [FieldOffset(0)]
        public byte padding13;
        [FieldOffset(0)]
        public byte padding14;
        [FieldOffset(0)]
        public byte padding15;
        [FieldOffset(0)]
        public byte padding16;
        [FieldOffset(0)]
        public byte padding17;
        [FieldOffset(0)]
        public byte padding18;
        [FieldOffset(0)]
        public byte padding19;
        [FieldOffset(0)]
        public byte padding20;
        [FieldOffset(0)]
        public byte padding21;
        [FieldOffset(0)]
        public byte padding22;
        [FieldOffset(0)]
        public byte padding23;
        [FieldOffset(0)]
        public byte padding24;
        [FieldOffset(0)]
        public byte padding25;
        [FieldOffset(0)]
        public byte padding26;
        [FieldOffset(0)]
        public byte padding27;
        [FieldOffset(0)]
        public byte padding28;
        [FieldOffset(0)]
        public byte padding29;
        [FieldOffset(0)]
        public byte padding30;
        [FieldOffset(0)]
        public byte padding31;
        [FieldOffset(0)]
        public byte padding32;
        [FieldOffset(0)]
        public byte padding33;
        [FieldOffset(0)]
        public byte padding34;
        [FieldOffset(0)]
        public byte padding35;
        [FieldOffset(0)]
        public byte padding36;
        [FieldOffset(0)]
        public byte padding37;
        [FieldOffset(0)]
        public byte padding38;
        [FieldOffset(0)]
        public byte padding39;
        [FieldOffset(0)]
        public byte padding40;
        [FieldOffset(0)]
        public byte padding41;
        [FieldOffset(0)]
        public byte padding42;
        [FieldOffset(0)]
        public byte padding43;
        [FieldOffset(0)]
        public byte padding44;
        [FieldOffset(0)]
        public byte padding45;
        [FieldOffset(0)]
        public byte padding46;
        [FieldOffset(0)]
        public byte padding47;
        [FieldOffset(0)]
        public byte padding48;
        [FieldOffset(0)]
        public byte padding49;
        [FieldOffset(0)]
        public byte padding50;
        [FieldOffset(0)]
        public byte padding51;
        [FieldOffset(0)]
        public byte padding52;
        [FieldOffset(0)]
        public byte padding53;
        [FieldOffset(0)]
        public byte padding54;
        [FieldOffset(0)]
        public byte padding55;
        [FieldOffset(0)]
        public byte padding56;
        [FieldOffset(0)]
        public byte padding57;
        [FieldOffset(0)]
        public byte padding58;
        [FieldOffset(0)]
        public byte padding59;
        [FieldOffset(0)]
        public byte padding60;
        [FieldOffset(0)]
        public byte padding61;
        [FieldOffset(0)]
        public byte padding62;
        [FieldOffset(0)]
        public byte padding63;
        [FieldOffset(0)]
        public byte padding64;
        [FieldOffset(0)]
        public byte padding65;
        [FieldOffset(0)]
        public byte padding66;
        [FieldOffset(0)]
        public byte padding67;
        [FieldOffset(0)]
        public byte padding68;
        [FieldOffset(0)]
        public byte padding69;
        [FieldOffset(0)]
        public byte padding70;
        [FieldOffset(0)]
        public byte padding71;
        [FieldOffset(0)]
        public byte padding72;
        [FieldOffset(0)]
        public byte padding73;
        [FieldOffset(0)]
        public byte padding74;
        [FieldOffset(0)]
        public byte padding75;
        [FieldOffset(0)]
        public byte padding76;
        [FieldOffset(0)]
        public byte padding77;
        [FieldOffset(0)]
        public byte padding78;
        [FieldOffset(0)]
        public byte padding79;
        [FieldOffset(0)]
        public byte padding80;
        [FieldOffset(0)]
        public byte padding81;
        [FieldOffset(0)]
        public byte padding82;
        [FieldOffset(0)]
        public byte padding83;
        [FieldOffset(0)]
        public byte padding84;
        [FieldOffset(0)]
        public byte padding85;
        [FieldOffset(0)]
        public byte padding86;
        [FieldOffset(0)]
        public byte padding87;
        [FieldOffset(0)]
        public byte padding88;
        [FieldOffset(0)]
        public byte padding89;
        [FieldOffset(0)]
        public byte padding90;
        [FieldOffset(0)]
        public byte padding91;
        [FieldOffset(0)]
        public byte padding92;
        [FieldOffset(0)]
        public byte padding93;
        [FieldOffset(0)]
        public byte padding94;
        [FieldOffset(0)]
        public byte padding95;
        [FieldOffset(0)]
        public byte padding96;
        [FieldOffset(0)]
        public byte padding97;
        [FieldOffset(0)]
        public byte padding98;
        [FieldOffset(0)]
        public byte padding99;
        [FieldOffset(0)]
        public byte padding100;
        [FieldOffset(0)]
        public byte padding101;
        [FieldOffset(0)]
        public byte padding102;
        [FieldOffset(0)]
        public byte padding103;
        [FieldOffset(0)]
        public byte padding104;
        [FieldOffset(0)]
        public byte padding105;
        [FieldOffset(0)]
        public byte padding106;
        [FieldOffset(0)]
        public byte padding107;
        [FieldOffset(0)]
        public byte padding108;
        [FieldOffset(0)]
        public byte padding109;
        [FieldOffset(0)]
        public byte padding110;
        [FieldOffset(0)]
        public byte padding111;
        [FieldOffset(0)]
        public byte padding112;
        [FieldOffset(0)]
        public byte padding113;
        [FieldOffset(0)]
        public byte padding114;
        [FieldOffset(0)]
        public byte padding115;
        [FieldOffset(0)]
        public byte padding116;
        [FieldOffset(0)]
        public byte padding117;
        [FieldOffset(0)]
        public byte padding118;
        [FieldOffset(0)]
        public byte padding119;
        [FieldOffset(0)]
        public byte padding120;
        [FieldOffset(0)]
        public byte padding121;
        [FieldOffset(0)]
        public byte padding122;
        [FieldOffset(0)]
        public byte padding123;
        [FieldOffset(0)]
        public byte padding124;
        [FieldOffset(0)]
        public byte padding125;
        [FieldOffset(0)]
        public byte padding126;
        [FieldOffset(0)]
        public byte padding127;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_PumpEvents();

    public enum SDL_EventAction
    {
        SDL_ADDEVENT = 0,
        SDL_PEEKEVENT = 1,
        SDL_GETEVENT = 2,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_PeepEvents(ref SDL_Event events, int numevents, SDL_EventAction action, uint minType, uint maxType); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasEvent(uint type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HasEvents(uint minType, uint maxType);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_FlushEvent(uint type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_FlushEvents(uint minType, uint maxType);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PollEvent(ref SDL_Event @event); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WaitEvent(ref SDL_Event @event); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WaitEventTimeout(ref SDL_Event @event, int timeoutMS); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PushEvent(ref SDL_Event @event); // WARN_UNKNOWN_POINTER_PARAMETER

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool SDL_EventFilter(IntPtr userdata, ref SDL_Event @event);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetEventFilter(/* SDL_EventFilter */ IntPtr filter, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetEventFilter(IntPtr filter, ref IntPtr userdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AddEventWatch(/* SDL_EventFilter */ IntPtr filter, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_RemoveEventWatch(/* SDL_EventFilter */ IntPtr filter, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_FilterEvents(/* SDL_EventFilter */ IntPtr filter, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetEventEnabled(uint type, bool enabled);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_EventEnabled(uint type);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_RegisterEvents(int numevents);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetWindowFromEvent(ref SDL_Event @event); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_filesystem.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetBasePath();

    [DllImport(nativeLibName, EntryPoint = "SDL_GetPrefPath", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetPrefPath(byte* org, byte* app);
    public static IntPtr SDL_GetPrefPath(string org, string app)
    {
        var orgUTF8 = EncodeAsUTF8(org);
        var appUTF8 = EncodeAsUTF8(app);

        var result = INTERNAL_SDL_GetPrefPath(orgUTF8, appUTF8);

        SDL_free((IntPtr)orgUTF8);
        SDL_free((IntPtr)appUTF8);

        return result;
    }

    public enum SDL_Folder
    {
        SDL_FOLDER_HOME = 0,
        SDL_FOLDER_DESKTOP = 1,
        SDL_FOLDER_DOCUMENTS = 2,
        SDL_FOLDER_DOWNLOADS = 3,
        SDL_FOLDER_MUSIC = 4,
        SDL_FOLDER_PICTURES = 5,
        SDL_FOLDER_PUBLICSHARE = 6,
        SDL_FOLDER_SAVEDGAMES = 7,
        SDL_FOLDER_SCREENSHOTS = 8,
        SDL_FOLDER_TEMPLATES = 9,
        SDL_FOLDER_VIDEOS = 10,
        SDL_FOLDER_TOTAL = 11,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetUserFolder(SDL_Folder folder);

    public enum SDL_PathType
    {
        SDL_PATHTYPE_NONE = 0,
        SDL_PATHTYPE_FILE = 1,
        SDL_PATHTYPE_DIRECTORY = 2,
        SDL_PATHTYPE_OTHER = 3,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PathInfo
    {
        public SDL_PathType type;
        public ulong size;
        public long create_time;
        public long modify_time;
        public long access_time;
    }

    [Flags]
    public enum SDL_GlobFlags : uint
    {
        SDL_GLOB_CASEINSENSITIVE = 0x1,
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_CreateDirectory(byte* path);
    public static bool SDL_CreateDirectory(string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_CreateDirectory(pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SDL_EnumerateDirectoryCallback(IntPtr userdata, IntPtr dirname, IntPtr fname);

    [DllImport(nativeLibName, EntryPoint = "SDL_EnumerateDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_EnumerateDirectory(byte* path, /* SDL_EnumerateDirectoryCallback */ IntPtr callback, IntPtr userdata);
    public static bool SDL_EnumerateDirectory(string path, /* SDL_EnumerateDirectoryCallback */ IntPtr callback, IntPtr userdata)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_EnumerateDirectory(pathUTF8, callback, userdata);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_RemovePath", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_RemovePath(byte* path);
    public static bool SDL_RemovePath(string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_RemovePath(pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_RenamePath", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_RenamePath(byte* oldpath, byte* newpath);
    public static bool SDL_RenamePath(string oldpath, string newpath)
    {
        var oldpathUTF8 = EncodeAsUTF8(oldpath);
        var newpathUTF8 = EncodeAsUTF8(newpath);

        var result = INTERNAL_SDL_RenamePath(oldpathUTF8, newpathUTF8);

        SDL_free((IntPtr)oldpathUTF8);
        SDL_free((IntPtr)newpathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CopyFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_CopyFile(byte* oldpath, byte* newpath);
    public static bool SDL_CopyFile(string oldpath, string newpath)
    {
        var oldpathUTF8 = EncodeAsUTF8(oldpath);
        var newpathUTF8 = EncodeAsUTF8(newpath);

        var result = INTERNAL_SDL_CopyFile(oldpathUTF8, newpathUTF8);

        SDL_free((IntPtr)oldpathUTF8);
        SDL_free((IntPtr)newpathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetPathInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GetPathInfo(byte* path, ref SDL_PathInfo info); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_GetPathInfo(string path, ref SDL_PathInfo info)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_GetPathInfo(pathUTF8, ref info);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GlobDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GlobDirectory(byte* path, byte* pattern, uint flags, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER
    public static IntPtr SDL_GlobDirectory(string path, string pattern, uint flags, ref int count)
    {
        var pathUTF8 = EncodeAsUTF8(path);
        var patternUTF8 = EncodeAsUTF8(pattern);

        var result = INTERNAL_SDL_GlobDirectory(pathUTF8, patternUTF8, flags, ref count);

        SDL_free((IntPtr)pathUTF8);
        SDL_free((IntPtr)patternUTF8);

        return result;
    }

    // ../SDL3/SDL_gpu.h

    public enum SDL_GPUPrimitiveType
    {
        SDL_GPU_PRIMITIVETYPE_POINTLIST = 0,
        SDL_GPU_PRIMITIVETYPE_LINELIST = 1,
        SDL_GPU_PRIMITIVETYPE_LINESTRIP = 2,
        SDL_GPU_PRIMITIVETYPE_TRIANGLELIST = 3,
        SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP = 4,
    }

    public enum SDL_GPULoadOp
    {
        SDL_GPU_LOADOP_LOAD = 0,
        SDL_GPU_LOADOP_CLEAR = 1,
        SDL_GPU_LOADOP_DONT_CARE = 2,
    }

    public enum SDL_GPUStoreOp
    {
        SDL_GPU_STOREOP_STORE = 0,
        SDL_GPU_STOREOP_DONT_CARE = 1,
    }

    public enum SDL_GPUIndexElementSize
    {
        SDL_GPU_INDEXELEMENTSIZE_16BIT = 0,
        SDL_GPU_INDEXELEMENTSIZE_32BIT = 1,
    }

    public enum SDL_GPUTextureFormat
    {
        SDL_GPU_TEXTUREFORMAT_INVALID = -1,
        SDL_GPU_TEXTUREFORMAT_A8_UNORM = 0,
        SDL_GPU_TEXTUREFORMAT_R8_UNORM = 1,
        SDL_GPU_TEXTUREFORMAT_R8G8_UNORM = 2,
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM = 3,
        SDL_GPU_TEXTUREFORMAT_R16_UNORM = 4,
        SDL_GPU_TEXTUREFORMAT_R16G16_UNORM = 5,
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM = 6,
        SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM = 7,
        SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM = 8,
        SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM = 9,
        SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM = 10,
        SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM = 11,
        SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM = 12,
        SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM = 13,
        SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM = 14,
        SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM = 15,
        SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM = 16,
        SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM = 17,
        SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT = 18,
        SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT = 19,
        SDL_GPU_TEXTUREFORMAT_R8_SNORM = 20,
        SDL_GPU_TEXTUREFORMAT_R8G8_SNORM = 21,
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM = 22,
        SDL_GPU_TEXTUREFORMAT_R16_SNORM = 23,
        SDL_GPU_TEXTUREFORMAT_R16G16_SNORM = 24,
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM = 25,
        SDL_GPU_TEXTUREFORMAT_R16_FLOAT = 26,
        SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT = 27,
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT = 28,
        SDL_GPU_TEXTUREFORMAT_R32_FLOAT = 29,
        SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT = 30,
        SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT = 31,
        SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT = 32,
        SDL_GPU_TEXTUREFORMAT_R8_UINT = 33,
        SDL_GPU_TEXTUREFORMAT_R8G8_UINT = 34,
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT = 35,
        SDL_GPU_TEXTUREFORMAT_R16_UINT = 36,
        SDL_GPU_TEXTUREFORMAT_R16G16_UINT = 37,
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT = 38,
        SDL_GPU_TEXTUREFORMAT_R8_INT = 39,
        SDL_GPU_TEXTUREFORMAT_R8G8_INT = 40,
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT = 41,
        SDL_GPU_TEXTUREFORMAT_R16_INT = 42,
        SDL_GPU_TEXTUREFORMAT_R16G16_INT = 43,
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT = 44,
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB = 45,
        SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB = 46,
        SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB = 47,
        SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB = 48,
        SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB = 49,
        SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB = 50,
        SDL_GPU_TEXTUREFORMAT_D16_UNORM = 51,
        SDL_GPU_TEXTUREFORMAT_D24_UNORM = 52,
        SDL_GPU_TEXTUREFORMAT_D32_FLOAT = 53,
        SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT = 54,
        SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT = 55,
    }

    [Flags]
    public enum SDL_GPUTextureUsageFlags : uint
    {
        SDL_GPU_TEXTUREUSAGE_SAMPLER = 0x1,
        SDL_GPU_TEXTUREUSAGE_COLOR_TARGET = 0x2,
        SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET = 0x4,
        SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ = 0x08,
        SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ = 0x10,
        SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE = 0x20,
    }

    public enum SDL_GPUTextureType
    {
        SDL_GPU_TEXTURETYPE_2D = 0,
        SDL_GPU_TEXTURETYPE_2D_ARRAY = 1,
        SDL_GPU_TEXTURETYPE_3D = 2,
        SDL_GPU_TEXTURETYPE_CUBE = 3,
    }

    public enum SDL_GPUSampleCount
    {
        SDL_GPU_SAMPLECOUNT_1 = 0,
        SDL_GPU_SAMPLECOUNT_2 = 1,
        SDL_GPU_SAMPLECOUNT_4 = 2,
        SDL_GPU_SAMPLECOUNT_8 = 3,
    }

    public enum SDL_GPUCubeMapFace
    {
        SDL_GPU_CUBEMAPFACE_POSITIVEX = 0,
        SDL_GPU_CUBEMAPFACE_NEGATIVEX = 1,
        SDL_GPU_CUBEMAPFACE_POSITIVEY = 2,
        SDL_GPU_CUBEMAPFACE_NEGATIVEY = 3,
        SDL_GPU_CUBEMAPFACE_POSITIVEZ = 4,
        SDL_GPU_CUBEMAPFACE_NEGATIVEZ = 5,
    }

    [Flags]
    public enum SDL_GPUBufferUsageFlags : uint
    {
        SDL_GPU_BUFFERUSAGE_VERTEX = 0x1,
        SDL_GPU_BUFFERUSAGE_INDEX = 0x2,
        SDL_GPU_BUFFERUSAGE_INDIRECT = 0x4,
        SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ = 0x08,
        SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ = 0x10,
        SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE = 0x20,
    }

    public enum SDL_GPUTransferBufferUsage
    {
        SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD = 0,
        SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD = 1,
    }

    public enum SDL_GPUShaderStage
    {
        SDL_GPU_SHADERSTAGE_VERTEX = 0,
        SDL_GPU_SHADERSTAGE_FRAGMENT = 1,
    }

    public enum SDL_GPUVertexElementFormat
    {
        SDL_GPU_VERTEXELEMENTFORMAT_INT = 0,
        SDL_GPU_VERTEXELEMENTFORMAT_INT2 = 1,
        SDL_GPU_VERTEXELEMENTFORMAT_INT3 = 2,
        SDL_GPU_VERTEXELEMENTFORMAT_INT4 = 3,
        SDL_GPU_VERTEXELEMENTFORMAT_UINT = 4,
        SDL_GPU_VERTEXELEMENTFORMAT_UINT2 = 5,
        SDL_GPU_VERTEXELEMENTFORMAT_UINT3 = 6,
        SDL_GPU_VERTEXELEMENTFORMAT_UINT4 = 7,
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT = 8,
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2 = 9,
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3 = 10,
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4 = 11,
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE2 = 12,
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE4 = 13,
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2 = 14,
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4 = 15,
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM = 16,
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM = 17,
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM = 18,
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM = 19,
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT2 = 20,
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT4 = 21,
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT2 = 22,
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT4 = 23,
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM = 24,
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM = 25,
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM = 26,
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM = 27,
        SDL_GPU_VERTEXELEMENTFORMAT_HALF2 = 28,
        SDL_GPU_VERTEXELEMENTFORMAT_HALF4 = 29,
    }

    public enum SDL_GPUVertexInputRate
    {
        SDL_GPU_VERTEXINPUTRATE_VERTEX = 0,
        SDL_GPU_VERTEXINPUTRATE_INSTANCE = 1,
    }

    public enum SDL_GPUFillMode
    {
        SDL_GPU_FILLMODE_FILL = 0,
        SDL_GPU_FILLMODE_LINE = 1,
    }

    public enum SDL_GPUCullMode
    {
        SDL_GPU_CULLMODE_NONE = 0,
        SDL_GPU_CULLMODE_FRONT = 1,
        SDL_GPU_CULLMODE_BACK = 2,
    }

    public enum SDL_GPUFrontFace
    {
        SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE = 0,
        SDL_GPU_FRONTFACE_CLOCKWISE = 1,
    }

    public enum SDL_GPUCompareOp
    {
        SDL_GPU_COMPAREOP_NEVER = 0,
        SDL_GPU_COMPAREOP_LESS = 1,
        SDL_GPU_COMPAREOP_EQUAL = 2,
        SDL_GPU_COMPAREOP_LESS_OR_EQUAL = 3,
        SDL_GPU_COMPAREOP_GREATER = 4,
        SDL_GPU_COMPAREOP_NOT_EQUAL = 5,
        SDL_GPU_COMPAREOP_GREATER_OR_EQUAL = 6,
        SDL_GPU_COMPAREOP_ALWAYS = 7,
    }

    public enum SDL_GPUStencilOp
    {
        SDL_GPU_STENCILOP_KEEP = 0,
        SDL_GPU_STENCILOP_ZERO = 1,
        SDL_GPU_STENCILOP_REPLACE = 2,
        SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP = 3,
        SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP = 4,
        SDL_GPU_STENCILOP_INVERT = 5,
        SDL_GPU_STENCILOP_INCREMENT_AND_WRAP = 6,
        SDL_GPU_STENCILOP_DECREMENT_AND_WRAP = 7,
    }

    public enum SDL_GPUBlendOp
    {
        SDL_GPU_BLENDOP_ADD = 0,
        SDL_GPU_BLENDOP_SUBTRACT = 1,
        SDL_GPU_BLENDOP_REVERSE_SUBTRACT = 2,
        SDL_GPU_BLENDOP_MIN = 3,
        SDL_GPU_BLENDOP_MAX = 4,
    }

    public enum SDL_GPUBlendFactor
    {
        SDL_GPU_BLENDFACTOR_ZERO = 0,
        SDL_GPU_BLENDFACTOR_ONE = 1,
        SDL_GPU_BLENDFACTOR_SRC_COLOR = 2,
        SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 3,
        SDL_GPU_BLENDFACTOR_DST_COLOR = 4,
        SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR = 5,
        SDL_GPU_BLENDFACTOR_SRC_ALPHA = 6,
        SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 7,
        SDL_GPU_BLENDFACTOR_DST_ALPHA = 8,
        SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 9,
        SDL_GPU_BLENDFACTOR_CONSTANT_COLOR = 10,
        SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR = 11,
        SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE = 12,
    }

    [Flags]
    public enum SDL_GPUColorComponentFlags : byte
    {
        SDL_GPU_COLORCOMPONENT_R = 0x1,
        SDL_GPU_COLORCOMPONENT_G = 0x2,
        SDL_GPU_COLORCOMPONENT_B = 0x4,
        SDL_GPU_COLORCOMPONENT_A = 0x08,
    }

    public enum SDL_GPUFilter
    {
        SDL_GPU_FILTER_NEAREST = 0,
        SDL_GPU_FILTER_LINEAR = 1,
    }

    public enum SDL_GPUSamplerMipmapMode
    {
        SDL_GPU_SAMPLERMIPMAPMODE_NEAREST = 0,
        SDL_GPU_SAMPLERMIPMAPMODE_LINEAR = 1,
    }

    public enum SDL_GPUSamplerAddressMode
    {
        SDL_GPU_SAMPLERADDRESSMODE_REPEAT = 0,
        SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT = 1,
        SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE = 2,
    }

    public enum SDL_GPUPresentMode
    {
        SDL_GPU_PRESENTMODE_VSYNC = 0,
        SDL_GPU_PRESENTMODE_IMMEDIATE = 1,
        SDL_GPU_PRESENTMODE_MAILBOX = 2,
    }

    public enum SDL_GPUSwapchainComposition
    {
        SDL_GPU_SWAPCHAINCOMPOSITION_SDR = 0,
        SDL_GPU_SWAPCHAINCOMPOSITION_SDR_LINEAR = 1,
        SDL_GPU_SWAPCHAINCOMPOSITION_HDR_EXTENDED_LINEAR = 2,
        SDL_GPU_SWAPCHAINCOMPOSITION_HDR10_ST2048 = 3,
    }

    public enum SDL_GPUDriver
    {
        SDL_GPU_DRIVER_INVALID = -1,
        SDL_GPU_DRIVER_PRIVATE = 0,
        SDL_GPU_DRIVER_VULKAN = 1,
        SDL_GPU_DRIVER_D3D11 = 2,
        SDL_GPU_DRIVER_D3D12 = 3,
        SDL_GPU_DRIVER_METAL = 4,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUViewport
    {
        public float x;
        public float y;
        public float w;
        public float h;
        public float min_depth;
        public float max_depth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureTransferInfo
    {
        public IntPtr transfer_buffer;
        public uint offset;
        public uint pixels_per_row;
        public uint rows_per_layer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTransferBufferLocation
    {
        public IntPtr transfer_buffer;
        public uint offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureLocation
    {
        public IntPtr texture;
        public uint mip_level;
        public uint layer;
        public uint x;
        public uint y;
        public uint z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureRegion
    {
        public IntPtr texture;
        public uint mip_level;
        public uint layer;
        public uint x;
        public uint y;
        public uint z;
        public uint w;
        public uint h;
        public uint d;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBlitRegion
    {
        public IntPtr texture;
        public uint mip_level;
        public uint layer_or_depth_plane;
        public uint x;
        public uint y;
        public uint w;
        public uint h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferLocation
    {
        public IntPtr buffer;
        public uint offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferRegion
    {
        public IntPtr buffer;
        public uint offset;
        public uint size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndirectDrawCommand
    {
        public uint num_vertices;
        public uint num_instances;
        public uint first_vertex;
        public uint first_instance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndexedIndirectDrawCommand
    {
        public uint num_indices;
        public uint num_instances;
        public uint first_index;
        public int vertex_offset;
        public uint first_instance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndirectDispatchCommand
    {
        public uint groupcount_x;
        public uint groupcount_y;
        public uint groupcount_z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUSamplerCreateInfo
    {
        public SDL_GPUFilter min_filter;
        public SDL_GPUFilter mag_filter;
        public SDL_GPUSamplerMipmapMode mipmap_mode;
        public SDL_GPUSamplerAddressMode address_mode_u;
        public SDL_GPUSamplerAddressMode address_mode_v;
        public SDL_GPUSamplerAddressMode address_mode_w;
        public float mip_lod_bias;
        public float max_anisotropy;
        public bool enable_anisotropy;
        public bool enable_compare;
        public byte padding1;
        public byte padding2;
        public SDL_GPUCompareOp compare_op;
        public float min_lod;
        public float max_lod;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexBinding
    {
        public uint index;
        public uint pitch;
        public SDL_GPUVertexInputRate input_rate;
        public uint instance_step_rate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexAttribute
    {
        public uint location;
        public uint binding_index;
        public SDL_GPUVertexElementFormat format;
        public uint offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexInputState
    {
        public SDL_GPUVertexBinding* vertex_bindings;
        public uint num_vertex_bindings;
        public SDL_GPUVertexAttribute* vertex_attributes;
        public uint num_vertex_attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStencilOpState
    {
        public SDL_GPUStencilOp fail_op;
        public SDL_GPUStencilOp pass_op;
        public SDL_GPUStencilOp depth_fail_op;
        public SDL_GPUCompareOp compare_op;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetBlendState
    {
        public bool enable_blend;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public SDL_GPUBlendFactor src_color_blendfactor;
        public SDL_GPUBlendFactor dst_color_blendfactor;
        public SDL_GPUBlendOp color_blend_op;
        public SDL_GPUBlendFactor src_alpha_blendfactor;
        public SDL_GPUBlendFactor dst_alpha_blendfactor;
        public SDL_GPUBlendOp alpha_blend_op;
        public byte color_write_mask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUShaderCreateInfo
    {
        public UIntPtr code_size;
        public byte* code;
        public char* entrypoint;
        public uint format;
        public SDL_GPUShaderStage stage;
        public uint num_samplers;
        public uint num_storage_textures;
        public uint num_storage_buffers;
        public uint num_uniform_buffers;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureCreateInfo
    {
        public SDL_GPUTextureType type;
        public SDL_GPUTextureFormat format;
        public uint usage;
        public uint width;
        public uint height;
        public uint layer_count_or_depth;
        public uint num_levels;
        public SDL_GPUSampleCount sample_count;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferCreateInfo
    {
        public uint usage;
        public uint size;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTransferBufferCreateInfo
    {
        public SDL_GPUTransferBufferUsage usage;
        public uint size;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPURasterizerState
    {
        public SDL_GPUFillMode fill_mode;
        public SDL_GPUCullMode cull_mode;
        public SDL_GPUFrontFace front_face;
        public bool enable_depth_bias;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public float depth_bias_constant_factor;
        public float depth_bias_clamp;
        public float depth_bias_slope_factor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUMultisampleState
    {
        public SDL_GPUSampleCount sample_count;
        public uint sample_mask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUDepthStencilState
    {
        public bool enable_depth_test;
        public bool enable_depth_write;
        public bool enable_stencil_test;
        public byte padding1;
        public SDL_GPUCompareOp compare_op;
        public SDL_GPUStencilOpState back_stencil_state;
        public SDL_GPUStencilOpState front_stencil_state;
        public byte compare_mask;
        public byte write_mask;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetDescription
    {
        public SDL_GPUTextureFormat format;
        public SDL_GPUColorTargetBlendState blend_state;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GpuGraphicsPipelineTargetInfo
    {
        public SDL_GPUColorTargetDescription* color_target_descriptions;
        public uint num_color_targets;
        public bool has_depth_stencil_target;
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public SDL_GPUTextureFormat depth_stencil_format;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUGraphicsPipelineCreateInfo
    {
        public IntPtr vertex_shader;
        public IntPtr fragment_shader;
        public SDL_GPUVertexInputState vertex_input_state;
        public SDL_GPUPrimitiveType primitive_type;
        public SDL_GPURasterizerState rasterizer_state;
        public SDL_GPUMultisampleState multisample_state;
        public SDL_GPUDepthStencilState depth_stencil_state;
        public SDL_GpuGraphicsPipelineTargetInfo target_info;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUComputePipelineCreateInfo
    {
        public UIntPtr code_size;
        public byte* code;
        public char* entrypoint;
        public uint format;
        public uint num_readonly_storage_textures;
        public uint num_readonly_storage_buffers;
        public uint num_writeonly_storage_textures;
        public uint num_writeonly_storage_buffers;
        public uint num_uniform_buffers;
        public uint threadcount_x;
        public uint threadcount_y;
        public uint threadcount_z;
        public uint props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetInfo
    {
        public IntPtr texture;
        public uint mip_level;
        public uint layer_or_depth_plane;
        public SDL_FColor clear_color;
        public SDL_GPULoadOp load_op;
        public SDL_GPUStoreOp store_op;
        public bool cycle;
        public byte padding1;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUDepthStencilTargetInfo
    {
        public IntPtr texture;
        public float clear_depth;
        public SDL_GPULoadOp load_op;
        public SDL_GPUStoreOp store_op;
        public SDL_GPULoadOp stencil_load_op;
        public SDL_GPUStoreOp stencil_store_op;
        public bool cycle;
        public byte clear_stencil;
        public byte padding1;
        public byte padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBlitInfo
    {
        public SDL_GPUBlitRegion source;
        public SDL_GPUBlitRegion destination;
        public SDL_GPULoadOp load_op;
        public SDL_FColor clear_color;
        public SDL_FlipMode flip_mode;
        public SDL_GPUFilter filter;
        public bool cycle;
        public byte padding;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferBinding
    {
        public IntPtr buffer;
        public uint offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureSamplerBinding
    {
        public IntPtr texture;
        public IntPtr sampler;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStorageBufferWriteOnlyBinding
    {
        public IntPtr buffer;
        public bool cycle;
        public byte padding1;
        public byte padding2;
        public byte padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStorageTextureWriteOnlyBinding
    {
        public IntPtr texture;
        public uint mip_level;
        public uint layer;
        public bool cycle;
        public byte padding1;
        public byte padding2;
        public byte padding3;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateGPUDevice", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_CreateGPUDevice(uint format_flags, bool debug_mode, byte* name);
    public static IntPtr SDL_CreateGPUDevice(uint format_flags, bool debug_mode, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_CreateGPUDevice(format_flags, debug_mode, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUDeviceWithProperties(uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyGPUDevice(IntPtr device);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GPUDriver SDL_GetGPUDriver(IntPtr device);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUComputePipeline(IntPtr device, ref SDL_GPUComputePipelineCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUGraphicsPipeline(IntPtr device, ref SDL_GPUGraphicsPipelineCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUSampler(IntPtr device, ref SDL_GPUSamplerCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUShader(IntPtr device, ref SDL_GPUShaderCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUTexture(IntPtr device, ref SDL_GPUTextureCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUBuffer(IntPtr device, ref SDL_GPUBufferCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateGPUTransferBuffer(IntPtr device, ref SDL_GPUTransferBufferCreateInfo createinfo); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_SetGPUBufferName", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, byte* text);
    public static void SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, string text)
    {
        var textUTF8 = EncodeAsUTF8(text);

        INTERNAL_SDL_SetGPUBufferName(device, buffer, textUTF8);

        SDL_free((IntPtr)textUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetGPUTextureName", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_SetGPUTextureName(IntPtr device, IntPtr texture, byte* text);
    public static void SDL_SetGPUTextureName(IntPtr device, IntPtr texture, string text)
    {
        var textUTF8 = EncodeAsUTF8(text);

        INTERNAL_SDL_SetGPUTextureName(device, texture, textUTF8);

        SDL_free((IntPtr)textUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_InsertGPUDebugLabel", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_InsertGPUDebugLabel(IntPtr command_buffer, byte* text);
    public static void SDL_InsertGPUDebugLabel(IntPtr command_buffer, string text)
    {
        var textUTF8 = EncodeAsUTF8(text);

        INTERNAL_SDL_InsertGPUDebugLabel(command_buffer, textUTF8);

        SDL_free((IntPtr)textUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_PushGPUDebugGroup", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_PushGPUDebugGroup(IntPtr command_buffer, byte* name);
    public static void SDL_PushGPUDebugGroup(IntPtr command_buffer, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        INTERNAL_SDL_PushGPUDebugGroup(command_buffer, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_PopGPUDebugGroup(IntPtr command_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUTexture(IntPtr device, IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUSampler(IntPtr device, IntPtr sampler);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUBuffer(IntPtr device, IntPtr buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUComputePipeline(IntPtr device, IntPtr compute_pipeline);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUShader(IntPtr device, IntPtr shader);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUGraphicsPipeline(IntPtr device, IntPtr graphics_pipeline);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_AcquireGPUCommandBuffer(IntPtr device);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_PushGPUVertexUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_PushGPUFragmentUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_PushGPUComputeUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_BeginGPURenderPass(IntPtr command_buffer, ref SDL_GPUColorTargetInfo color_target_infos, uint num_color_targets, ref SDL_GPUDepthStencilTargetInfo depth_stencil_target_info); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUGraphicsPipeline(IntPtr render_pass, IntPtr graphics_pipeline);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetGPUViewport(IntPtr render_pass, ref SDL_GPUViewport viewport); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetGPUScissor(IntPtr render_pass, ref SDL_Rect scissor); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetGPUBlendConstants(IntPtr render_pass, SDL_FColor blend_constants);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetGPUStencilReference(IntPtr render_pass, byte reference);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUVertexBuffers(IntPtr render_pass, uint first_binding, ref SDL_GPUBufferBinding bindings, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUIndexBuffer(IntPtr render_pass, ref SDL_GPUBufferBinding binding, SDL_GPUIndexElementSize index_element_size); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUVertexSamplers(IntPtr render_pass, uint first_slot, ref SDL_GPUTextureSamplerBinding texture_sampler_bindings, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUVertexStorageTextures(IntPtr render_pass, uint first_slot, ref IntPtr storage_textures, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUVertexStorageBuffers(IntPtr render_pass, uint first_slot, ref IntPtr storage_buffers, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUFragmentSamplers(IntPtr render_pass, uint first_slot, ref SDL_GPUTextureSamplerBinding texture_sampler_bindings, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUFragmentStorageTextures(IntPtr render_pass, uint first_slot, ref IntPtr storage_textures, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUFragmentStorageBuffers(IntPtr render_pass, uint first_slot, ref IntPtr storage_buffers, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DrawGPUIndexedPrimitives(IntPtr render_pass, uint num_indices, uint num_instances, uint first_index, int vertex_offset, uint first_instance);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DrawGPUPrimitives(IntPtr render_pass, uint num_vertices, uint num_instances, uint first_vertex, uint first_instance);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DrawGPUPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count, uint pitch);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DrawGPUIndexedPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count, uint pitch);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_EndGPURenderPass(IntPtr render_pass);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_BeginGPUComputePass(IntPtr command_buffer, ref SDL_GPUStorageTextureWriteOnlyBinding storage_texture_bindings, uint num_storage_texture_bindings, ref SDL_GPUStorageBufferWriteOnlyBinding storage_buffer_bindings, uint num_storage_buffer_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUComputePipeline(IntPtr compute_pass, IntPtr compute_pipeline);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUComputeStorageTextures(IntPtr compute_pass, uint first_slot, ref IntPtr storage_textures, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BindGPUComputeStorageBuffers(IntPtr compute_pass, uint first_slot, ref IntPtr storage_buffers, uint num_bindings); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DispatchGPUCompute(IntPtr compute_pass, uint groupcount_x, uint groupcount_y, uint groupcount_z);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DispatchGPUComputeIndirect(IntPtr compute_pass, IntPtr buffer, uint offset);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_EndGPUComputePass(IntPtr compute_pass);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_MapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer, bool cycle);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnmapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_BeginGPUCopyPass(IntPtr command_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UploadToGPUTexture(IntPtr copy_pass, ref SDL_GPUTextureTransferInfo source, ref SDL_GPUTextureRegion destination, bool cycle); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UploadToGPUBuffer(IntPtr copy_pass, ref SDL_GPUTransferBufferLocation source, ref SDL_GPUBufferRegion destination, bool cycle); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CopyGPUTextureToTexture(IntPtr copy_pass, ref SDL_GPUTextureLocation source, ref SDL_GPUTextureLocation destination, uint w, uint h, uint d, bool cycle); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CopyGPUBufferToBuffer(IntPtr copy_pass, ref SDL_GPUBufferLocation source, ref SDL_GPUBufferLocation destination, uint size, bool cycle); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DownloadFromGPUTexture(IntPtr copy_pass, ref SDL_GPUTextureRegion source, ref SDL_GPUTextureTransferInfo destination); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DownloadFromGPUBuffer(IntPtr copy_pass, ref SDL_GPUBufferRegion source, ref SDL_GPUTransferBufferLocation destination); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_EndGPUCopyPass(IntPtr copy_pass);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GenerateMipmapsForGPUTexture(IntPtr command_buffer, IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_BlitGPUTexture(IntPtr command_buffer, ref SDL_GPUBlitInfo info); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, SDL_GPUSwapchainComposition swapchain_composition);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, SDL_GPUPresentMode present_mode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ClaimWindowForGPUDevice(IntPtr device, IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetGPUSwapchainParameters(IntPtr device, IntPtr window, SDL_GPUSwapchainComposition swapchain_composition, SDL_GPUPresentMode present_mode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_AcquireGPUSwapchainTexture(IntPtr command_buffer, IntPtr window, ref uint w, ref uint h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SubmitGPUCommandBuffer(IntPtr command_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_SubmitGPUCommandBufferAndAcquireFence(IntPtr command_buffer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WaitForGPUIdle(IntPtr device);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WaitForGPUFences(IntPtr device, bool wait_all, ref IntPtr fences, uint num_fences); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_QueryGPUFence(IntPtr device, IntPtr fence);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ReleaseGPUFence(IntPtr device, IntPtr fence);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GPUTextureSupportsFormat(IntPtr device, SDL_GPUTextureFormat format, SDL_GPUTextureType type, uint usage);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GPUTextureSupportsSampleCount(IntPtr device, SDL_GPUTextureFormat format, SDL_GPUSampleCount sample_count);

    // ../SDL3/SDL_haptic.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticDirection
    {
        public byte type;
        public int dir0;
        public int dir1;
        public int dir2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticConstant
    {
        public ushort type;
        public SDL_HapticDirection direction;
        public uint length;
        public ushort delay;
        public ushort button;
        public ushort interval;
        public short level;
        public ushort attack_length;
        public ushort attack_level;
        public ushort fade_length;
        public ushort fade_level;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticPeriodic
    {
        public ushort type;
        public SDL_HapticDirection direction;
        public uint length;
        public ushort delay;
        public ushort button;
        public ushort interval;
        public ushort period;
        public short magnitude;
        public short offset;
        public ushort phase;
        public ushort attack_length;
        public ushort attack_level;
        public ushort fade_length;
        public ushort fade_level;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticCondition
    {
        public ushort type;
        public SDL_HapticDirection direction;
        public uint length;
        public ushort delay;
        public ushort button;
        public ushort interval;
        public ushort right_sat0;
        public ushort right_sat1;
        public ushort right_sat2;
        public ushort left_sat0;
        public ushort left_sat1;
        public ushort left_sat2;
        public short right_coeff0;
        public short right_coeff1;
        public short right_coeff2;
        public short left_coeff0;
        public short left_coeff1;
        public short left_coeff2;
        public ushort deadband0;
        public ushort deadband1;
        public ushort deadband2;
        public short center0;
        public short center1;
        public short center2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticRamp
    {
        public ushort type;
        public SDL_HapticDirection direction;
        public uint length;
        public ushort delay;
        public ushort button;
        public ushort interval;
        public short start;
        public short end;
        public ushort attack_length;
        public ushort attack_level;
        public ushort fade_length;
        public ushort fade_level;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticLeftRight
    {
        public ushort type;
        public uint length;
        public ushort large_magnitude;
        public ushort small_magnitude;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_HapticCustom
    {
        public ushort type;
        public SDL_HapticDirection direction;
        public uint length;
        public ushort delay;
        public ushort button;
        public ushort interval;
        public byte channels;
        public ushort period;
        public ushort samples;
        public ushort* data;
        public ushort attack_length;
        public ushort attack_level;
        public ushort fade_length;
        public ushort fade_level;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_HapticEffect
    {
        [FieldOffset(0)]
        public ushort type;
        [FieldOffset(0)]
        public SDL_HapticConstant constant;
        [FieldOffset(0)]
        public SDL_HapticPeriodic periodic;
        [FieldOffset(0)]
        public SDL_HapticCondition condition;
        [FieldOffset(0)]
        public SDL_HapticRamp ramp;
        [FieldOffset(0)]
        public SDL_HapticLeftRight leftright;
        [FieldOffset(0)]
        public SDL_HapticCustom custom;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetHaptics(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetHapticNameForID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenHaptic(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetHapticFromID(uint instance_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetHapticID(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetHapticName(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_IsMouseHaptic();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenHapticFromMouse();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_IsJoystickHaptic(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenHapticFromJoystick(IntPtr joystick);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CloseHaptic(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetMaxHapticEffects(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetMaxHapticEffectsPlaying(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetHapticFeatures(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumHapticAxes(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HapticEffectSupported(IntPtr haptic, ref SDL_HapticEffect effect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_CreateHapticEffect(IntPtr haptic, ref SDL_HapticEffect effect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateHapticEffect(IntPtr haptic, int effect, ref SDL_HapticEffect data); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RunHapticEffect(IntPtr haptic, int effect, uint iterations);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StopHapticEffect(IntPtr haptic, int effect);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyHapticEffect(IntPtr haptic, int effect);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetHapticEffectStatus(IntPtr haptic, int effect);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetHapticGain(IntPtr haptic, int gain);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetHapticAutocenter(IntPtr haptic, int autocenter);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PauseHaptic(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ResumeHaptic(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StopHapticEffects(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_HapticRumbleSupported(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_InitHapticRumble(IntPtr haptic);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_PlayHapticRumble(IntPtr haptic, float strength, uint length);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StopHapticRumble(IntPtr haptic);

    // ../SDL3/SDL_hidapi.h

    public enum SDL_hid_bus_type
    {
        SDL_HID_API_BUS_UNKNOWN = 0,
        SDL_HID_API_BUS_USB = 1,
        SDL_HID_API_BUS_BLUETOOTH = 2,
        SDL_HID_API_BUS_I2C = 3,
        SDL_HID_API_BUS_SPI = 4,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_hid_device_info
    {
        public char* path;
        public ushort vendor_id;
        public ushort product_id;
        public char* serial_number;
        public ushort release_number;
        public char* manufacturer_string;
        public char* product_string;
        public ushort usage_page;
        public ushort usage;
        public int interface_number;
        public int interface_class;
        public int interface_subclass;
        public int interface_protocol;
        public SDL_hid_bus_type bus_type;
        public SDL_hid_device_info* next;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_init();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_exit();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_hid_device_change_count();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_hid_enumerate(ushort vendor_id, ushort product_id);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_hid_free_enumeration(ref SDL_hid_device_info devs); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_open", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_hid_open(ushort vendor_id, ushort product_id, byte* serial_number);
    public static IntPtr SDL_hid_open(ushort vendor_id, ushort product_id, string serial_number)
    {
        var serial_numberUTF8 = EncodeAsUTF8(serial_number);

        var result = INTERNAL_SDL_hid_open(vendor_id, product_id, serial_numberUTF8);

        SDL_free((IntPtr)serial_numberUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_open_path", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_hid_open_path(byte* path);
    public static IntPtr SDL_hid_open_path(string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_hid_open_path(pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_write(IntPtr dev, ref byte data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_read_timeout(IntPtr dev, ref byte data, UIntPtr length, int milliseconds); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_read(IntPtr dev, ref byte data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_send_feature_report(IntPtr dev, ref byte data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_get_feature_report(IntPtr dev, ref byte data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_get_input_report(IntPtr dev, ref byte data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_close(IntPtr dev);

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_get_manufacturer_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_hid_get_manufacturer_string(IntPtr dev, byte* @string, UIntPtr maxlen);
    public static int SDL_hid_get_manufacturer_string(IntPtr dev, string @string, UIntPtr maxlen)
    {
        var @stringUTF8 = EncodeAsUTF8(@string);

        var result = INTERNAL_SDL_hid_get_manufacturer_string(dev, @stringUTF8, maxlen);

        SDL_free((IntPtr)@stringUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_get_product_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_hid_get_product_string(IntPtr dev, byte* @string, UIntPtr maxlen);
    public static int SDL_hid_get_product_string(IntPtr dev, string @string, UIntPtr maxlen)
    {
        var @stringUTF8 = EncodeAsUTF8(@string);

        var result = INTERNAL_SDL_hid_get_product_string(dev, @stringUTF8, maxlen);

        SDL_free((IntPtr)@stringUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_get_serial_number_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_hid_get_serial_number_string(IntPtr dev, byte* @string, UIntPtr maxlen);
    public static int SDL_hid_get_serial_number_string(IntPtr dev, string @string, UIntPtr maxlen)
    {
        var @stringUTF8 = EncodeAsUTF8(@string);

        var result = INTERNAL_SDL_hid_get_serial_number_string(dev, @stringUTF8, maxlen);

        SDL_free((IntPtr)@stringUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_hid_get_indexed_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int INTERNAL_SDL_hid_get_indexed_string(IntPtr dev, int string_index, byte* @string, UIntPtr maxlen);
    public static int SDL_hid_get_indexed_string(IntPtr dev, int string_index, string @string, UIntPtr maxlen)
    {
        var @stringUTF8 = EncodeAsUTF8(@string);

        var result = INTERNAL_SDL_hid_get_indexed_string(dev, string_index, @stringUTF8, maxlen);

        SDL_free((IntPtr)@stringUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_hid_get_device_info(IntPtr dev);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_hid_get_report_descriptor(IntPtr dev, ref byte buf, UIntPtr buf_size); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_hid_ble_scan(bool active);

    // ../SDL3/SDL_hints.h

    public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
    public const string SDL_HINT_ANDROID_ALLOW_RECREATE_ACTIVITY = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";
    public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";
    public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";
    public const string SDL_HINT_APP_ID = "SDL_APP_ID";
    public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";
    public const string SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";
    public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
    public const string SDL_HINT_AUDIO_ALSA_DEFAULT_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_DEVICE";
    public const string SDL_HINT_AUDIO_CATEGORY = "SDL_AUDIO_CATEGORY";
    public const string SDL_HINT_AUDIO_CHANNELS = "SDL_AUDIO_CHANNELS";
    public const string SDL_HINT_AUDIO_DEVICE_APP_ICON_NAME = "SDL_AUDIO_DEVICE_APP_ICON_NAME";
    public const string SDL_HINT_AUDIO_DEVICE_SAMPLE_FRAMES = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";
    public const string SDL_HINT_AUDIO_DISK_INPUT_FILE = "SDL_AUDIO_DISK_INPUT_FILE";
    public const string SDL_HINT_AUDIO_DISK_OUTPUT_FILE = "SDL_AUDIO_DISK_OUTPUT_FILE";
    public const string SDL_HINT_AUDIO_DISK_TIMESCALE = "SDL_AUDIO_DISK_TIMESCALE";
    public const string SDL_HINT_AUDIO_DRIVER = "SDL_AUDIO_DRIVER";
    public const string SDL_HINT_AUDIO_DUMMY_TIMESCALE = "SDL_AUDIO_DUMMY_TIMESCALE";
    public const string SDL_HINT_AUDIO_FORMAT = "SDL_AUDIO_FORMAT";
    public const string SDL_HINT_AUDIO_FREQUENCY = "SDL_AUDIO_FREQUENCY";
    public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";
    public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";
    public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";
    public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT = "SDL_BMP_SAVE_LEGACY_FORMAT";
    public const string SDL_HINT_CAMERA_DRIVER = "SDL_CAMERA_DRIVER";
    public const string SDL_HINT_CPU_FEATURE_MASK = "SDL_CPU_FEATURE_MASK";
    public const string SDL_HINT_JOYSTICK_DIRECTINPUT = "SDL_JOYSTICK_DIRECTINPUT";
    public const string SDL_HINT_FILE_DIALOG_DRIVER = "SDL_FILE_DIALOG_DRIVER";
    public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";
    public const string SDL_HINT_EMSCRIPTEN_ASYNCIFY = "SDL_EMSCRIPTEN_ASYNCIFY";
    public const string SDL_HINT_EMSCRIPTEN_CANVAS_SELECTOR = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
    public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
    public const string SDL_HINT_ENABLE_SCREEN_KEYBOARD = "SDL_ENABLE_SCREEN_KEYBOARD";
    public const string SDL_HINT_EVDEV_DEVICES = "SDL_EVDEV_DEVICES";
    public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";
    public const string SDL_HINT_FORCE_RAISEWINDOW = "SDL_FORCE_RAISEWINDOW";
    public const string SDL_HINT_FRAMEBUFFER_ACCELERATION = "SDL_FRAMEBUFFER_ACCELERATION";
    public const string SDL_HINT_GAMECONTROLLERCONFIG = "SDL_GAMECONTROLLERCONFIG";
    public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";
    public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";
    public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
    public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
    public const string SDL_HINT_GAMECONTROLLER_SENSOR_FUSION = "SDL_GAMECONTROLLER_SENSOR_FUSION";
    public const string SDL_HINT_GDK_TEXTINPUT_DEFAULT_TEXT = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
    public const string SDL_HINT_GDK_TEXTINPUT_DESCRIPTION = "SDL_GDK_TEXTINPUT_DESCRIPTION";
    public const string SDL_HINT_GDK_TEXTINPUT_MAX_LENGTH = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
    public const string SDL_HINT_GDK_TEXTINPUT_SCOPE = "SDL_GDK_TEXTINPUT_SCOPE";
    public const string SDL_HINT_GDK_TEXTINPUT_TITLE = "SDL_GDK_TEXTINPUT_TITLE";
    public const string SDL_HINT_HIDAPI_LIBUSB = "SDL_HIDAPI_LIBUSB";
    public const string SDL_HINT_HIDAPI_LIBUSB_WHITELIST = "SDL_HIDAPI_LIBUSB_WHITELIST";
    public const string SDL_HINT_HIDAPI_UDEV = "SDL_HIDAPI_UDEV";
    public const string SDL_HINT_GPU_DRIVER = "SDL_GPU_DRIVER";
    public const string SDL_HINT_HIDAPI_ENUMERATE_ONLY_CONTROLLERS = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
    public const string SDL_HINT_HIDAPI_IGNORE_DEVICES = "SDL_HIDAPI_IGNORE_DEVICES";
    public const string SDL_HINT_IME_IMPLEMENTED_UI = "SDL_IME_IMPLEMENTED_UI";
    public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";
    public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
    public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES = "SDL_JOYSTICK_BLACKLIST_DEVICES";
    public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";
    public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
    public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_GAMEINPUT = "SDL_JOYSTICK_GAMEINPUT";
    public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES = "SDL_JOYSTICK_GAMECUBE_DEVICES";
    public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";
    public const string SDL_HINT_JOYSTICK_HIDAPI_COMBINE_JOY_CONS = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_JOYCON_HOME_LED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";
    public const string SDL_HINT_JOYSTICK_HIDAPI_NINTENDO_CLASSIC = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS3 = "SDL_JOYSTICK_HIDAPI_PS3";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SHIELD = "SDL_JOYSTICK_HIDAPI_SHIELD";
    public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAMDECK = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_WII = "SDL_JOYSTICK_HIDAPI_WII";
    public const string SDL_HINT_JOYSTICK_HIDAPI_WII_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_WIRELESS = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    public const string SDL_HINT_JOYSTICK_IOKIT = "SDL_JOYSTICK_IOKIT";
    public const string SDL_HINT_JOYSTICK_LINUX_CLASSIC = "SDL_JOYSTICK_LINUX_CLASSIC";
    public const string SDL_HINT_JOYSTICK_LINUX_DEADZONES = "SDL_JOYSTICK_LINUX_DEADZONES";
    public const string SDL_HINT_JOYSTICK_LINUX_DIGITAL_HATS = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
    public const string SDL_HINT_JOYSTICK_LINUX_HAT_DEADZONES = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
    public const string SDL_HINT_JOYSTICK_MFI = "SDL_JOYSTICK_MFI";
    public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";
    public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    public const string SDL_HINT_JOYSTICK_ROG_CHAKRAM = "SDL_JOYSTICK_ROG_CHAKRAM";
    public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";
    public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES = "SDL_JOYSTICK_THROTTLE_DEVICES";
    public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES_EXCLUDED = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_WGI = "SDL_JOYSTICK_WGI";
    public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES = "SDL_JOYSTICK_WHEEL_DEVICES";
    public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES_EXCLUDED = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    public const string SDL_HINT_JOYSTICK_ZERO_CENTERED_DEVICES = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
    public const string SDL_HINT_KEYCODE_OPTIONS = "SDL_KEYCODE_OPTIONS";
    public const string SDL_HINT_KMSDRM_DEVICE_INDEX = "SDL_KMSDRM_DEVICE_INDEX";
    public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    public const string SDL_HINT_LOGGING = "SDL_LOGGING";
    public const string SDL_HINT_MAC_BACKGROUND_APP = "SDL_MAC_BACKGROUND_APP";
    public const string SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    public const string SDL_HINT_MAC_OPENGL_ASYNC_DISPATCH = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
    public const string SDL_HINT_MAIN_CALLBACK_RATE = "SDL_MAIN_CALLBACK_RATE";
    public const string SDL_HINT_MOUSE_AUTO_CAPTURE = "SDL_MOUSE_AUTO_CAPTURE";
    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    public const string SDL_HINT_MOUSE_EMULATE_WARP_WITH_RELATIVE = "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE";
    public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
    public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    public const string SDL_HINT_MOUSE_RELATIVE_MODE_CENTER = "SDL_MOUSE_RELATIVE_MODE_CENTER";
    public const string SDL_HINT_MOUSE_RELATIVE_MODE_WARP = "SDL_MOUSE_RELATIVE_MODE_WARP";
    public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    public const string SDL_HINT_MOUSE_RELATIVE_SYSTEM_SCALE = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
    public const string SDL_HINT_MOUSE_RELATIVE_WARP_MOTION = "SDL_MOUSE_RELATIVE_WARP_MOTION";
    public const string SDL_HINT_MOUSE_RELATIVE_CURSOR_VISIBLE = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
    public const string SDL_HINT_MOUSE_RELATIVE_CLIP_INTERVAL = "SDL_MOUSE_RELATIVE_CLIP_INTERVAL";
    public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";
    public const string SDL_HINT_MUTE_CONSOLE_KEYBOARD = "SDL_MUTE_CONSOLE_KEYBOARD";
    public const string SDL_HINT_NO_SIGNAL_HANDLERS = "SDL_NO_SIGNAL_HANDLERS";
    public const string SDL_HINT_OPENGL_LIBRARY = "SDL_OPENGL_LIBRARY";
    public const string SDL_HINT_OPENGL_ES_DRIVER = "SDL_OPENGL_ES_DRIVER";
    public const string SDL_HINT_ORIENTATIONS = "SDL_ORIENTATIONS";
    public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";
    public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";
    public const string SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
    public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE = "SDL_RENDER_DIRECT3D_THREADSAFE";
    public const string SDL_HINT_RENDER_DIRECT3D11_DEBUG = "SDL_RENDER_DIRECT3D11_DEBUG";
    public const string SDL_HINT_RENDER_VULKAN_DEBUG = "SDL_RENDER_VULKAN_DEBUG";
    public const string SDL_HINT_RENDER_GPU_DEBUG = "SDL_RENDER_GPU_DEBUG";
    public const string SDL_HINT_RENDER_GPU_LOW_POWER = "SDL_RENDER_GPU_LOW_POWER";
    public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";
    public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";
    public const string SDL_HINT_RENDER_METAL_PREFER_LOW_POWER_DEVICE = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";
    public const string SDL_HINT_RENDER_VSYNC = "SDL_RENDER_VSYNC";
    public const string SDL_HINT_RETURN_KEY_HIDES_IME = "SDL_RETURN_KEY_HIDES_IME";
    public const string SDL_HINT_ROG_GAMEPAD_MICE = "SDL_ROG_GAMEPAD_MICE";
    public const string SDL_HINT_ROG_GAMEPAD_MICE_EXCLUDED = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
    public const string SDL_HINT_RPI_VIDEO_LAYER = "SDL_RPI_VIDEO_LAYER";
    public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    public const string SDL_HINT_SHUTDOWN_DBUS_ON_QUIT = "SDL_SHUTDOWN_DBUS_ON_QUIT";
    public const string SDL_HINT_STORAGE_TITLE_DRIVER = "SDL_STORAGE_TITLE_DRIVER";
    public const string SDL_HINT_STORAGE_USER_DRIVER = "SDL_STORAGE_USER_DRIVER";
    public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
    public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";
    public const string SDL_HINT_TIMER_RESOLUTION = "SDL_TIMER_RESOLUTION";
    public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";
    public const string SDL_HINT_TRACKPAD_IS_TOUCH_ONLY = "SDL_TRACKPAD_IS_TOUCH_ONLY";
    public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";
    public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER = "SDL_VIDEO_ALLOW_SCREENSAVER";
    public const string SDL_HINT_VIDEO_DOUBLE_BUFFER = "SDL_VIDEO_DOUBLE_BUFFER";
    public const string SDL_HINT_VIDEO_DRIVER = "SDL_VIDEO_DRIVER";
    public const string SDL_HINT_VIDEO_DUMMY_SAVE_FRAMES = "SDL_VIDEO_DUMMY_SAVE_FRAMES";
    public const string SDL_HINT_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
    public const string SDL_HINT_VIDEO_FORCE_EGL = "SDL_VIDEO_FORCE_EGL";
    public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
    public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    public const string SDL_HINT_VIDEO_OFFSCREEN_SAVE_FRAMES = "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES";
    public const string SDL_HINT_VIDEO_SYNC_WINDOW_OPERATIONS = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
    public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
    public const string SDL_HINT_VIDEO_WAYLAND_MODE_EMULATION = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
    public const string SDL_HINT_VIDEO_WAYLAND_MODE_SCALING = "SDL_VIDEO_WAYLAND_MODE_SCALING";
    public const string SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
    public const string SDL_HINT_VIDEO_WAYLAND_SCALE_TO_DISPLAY = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER = "SDL_VIDEO_WIN_D3DCOMPILER";
    public const string SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
    public const string SDL_HINT_VIDEO_X11_NET_WM_PING = "SDL_VIDEO_X11_NET_WM_PING";
    public const string SDL_HINT_VIDEO_X11_NODIRECTCOLOR = "SDL_VIDEO_X11_NODIRECTCOLOR";
    public const string SDL_HINT_VIDEO_X11_SCALING_FACTOR = "SDL_VIDEO_X11_SCALING_FACTOR";
    public const string SDL_HINT_VIDEO_X11_VISUALID = "SDL_VIDEO_X11_VISUALID";
    public const string SDL_HINT_VIDEO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";
    public const string SDL_HINT_VIDEO_X11_XRANDR = "SDL_VIDEO_X11_XRANDR";
    public const string SDL_HINT_VITA_ENABLE_BACK_TOUCH = "SDL_VITA_ENABLE_BACK_TOUCH";
    public const string SDL_HINT_VITA_ENABLE_FRONT_TOUCH = "SDL_VITA_ENABLE_FRONT_TOUCH";
    public const string SDL_HINT_VITA_MODULE_PATH = "SDL_VITA_MODULE_PATH";
    public const string SDL_HINT_VITA_PVR_INIT = "SDL_VITA_PVR_INIT";
    public const string SDL_HINT_VITA_RESOLUTION = "SDL_VITA_RESOLUTION";
    public const string SDL_HINT_VITA_PVR_OPENGL = "SDL_VITA_PVR_OPENGL";
    public const string SDL_HINT_VITA_TOUCH_MOUSE_DEVICE = "SDL_VITA_TOUCH_MOUSE_DEVICE";
    public const string SDL_HINT_VULKAN_DISPLAY = "SDL_VULKAN_DISPLAY";
    public const string SDL_HINT_VULKAN_LIBRARY = "SDL_VULKAN_LIBRARY";
    public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";
    public const string SDL_HINT_WAVE_CHUNK_LIMIT = "SDL_WAVE_CHUNK_LIMIT";
    public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";
    public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";
    public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_RAISED = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
    public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_SHOWN = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
    public const string SDL_HINT_WINDOW_ALLOW_TOPMOST = "SDL_WINDOW_ALLOW_TOPMOST";
    public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
    public const string SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
    public const string SDL_HINT_WINDOWS_ENABLE_MENU_MNEMONICS = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
    public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
    public const string SDL_HINT_WINDOWS_GAMEINPUT = "SDL_WINDOWS_GAMEINPUT";
    public const string SDL_HINT_WINDOWS_RAW_KEYBOARD = "SDL_WINDOWS_RAW_KEYBOARD";
    public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";
    public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";
    public const string SDL_HINT_WINDOWS_ERASE_BACKGROUND_MODE = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
    public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    public const string SDL_HINT_X11_WINDOW_TYPE = "SDL_X11_WINDOW_TYPE";
    public const string SDL_HINT_X11_XCB_LIBRARY = "SDL_X11_XCB_LIBRARY";
    public const string SDL_HINT_XINPUT_ENABLED = "SDL_XINPUT_ENABLED";
    public const string SDL_HINT_ASSERT = "SDL_ASSERT";

    public enum SDL_HintPriority
    {
        SDL_HINT_DEFAULT = 0,
        SDL_HINT_NORMAL = 1,
        SDL_HINT_OVERRIDE = 2,
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetHintWithPriority", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetHintWithPriority(byte* name, byte* value, SDL_HintPriority priority);
    public static bool SDL_SetHintWithPriority(string name, string value, SDL_HintPriority priority)
    {
        var nameUTF8 = EncodeAsUTF8(name);
        var valueUTF8 = EncodeAsUTF8(value);

        var result = INTERNAL_SDL_SetHintWithPriority(nameUTF8, valueUTF8, priority);

        SDL_free((IntPtr)nameUTF8);
        SDL_free((IntPtr)valueUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetHint(byte* name, byte* value);
    public static bool SDL_SetHint(string name, string value)
    {
        var nameUTF8 = EncodeAsUTF8(name);
        var valueUTF8 = EncodeAsUTF8(value);

        var result = INTERNAL_SDL_SetHint(nameUTF8, valueUTF8);

        SDL_free((IntPtr)nameUTF8);
        SDL_free((IntPtr)valueUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ResetHint", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_ResetHint(byte* name);
    public static bool SDL_ResetHint(string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_ResetHint(nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ResetHints();

    [DllImport(nativeLibName, EntryPoint = "SDL_GetHint", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetHint(byte* name);
    public static IntPtr SDL_GetHint(string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetHint(nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetHintBoolean", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GetHintBoolean(byte* name, bool default_value);
    public static bool SDL_GetHintBoolean(string name, bool default_value)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetHintBoolean(nameUTF8, default_value);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_HintCallback(IntPtr userdata, IntPtr name, IntPtr oldValue, IntPtr newValue);

    [DllImport(nativeLibName, EntryPoint = "SDL_AddHintCallback", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_AddHintCallback(byte* name, /* SDL_HintCallback */ IntPtr callback, IntPtr userdata);
    public static bool SDL_AddHintCallback(string name, /* SDL_HintCallback */ IntPtr callback, IntPtr userdata)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_AddHintCallback(nameUTF8, callback, userdata);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_RemoveHintCallback", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_RemoveHintCallback(byte* name, /* SDL_HintCallback */ IntPtr callback, IntPtr userdata);
    public static void SDL_RemoveHintCallback(string name, /* SDL_HintCallback */ IntPtr callback, IntPtr userdata)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        INTERNAL_SDL_RemoveHintCallback(nameUTF8, callback, userdata);

        SDL_free((IntPtr)nameUTF8);

    }

    // ../SDL3/SDL_init.h

    [Flags]
    public enum SDL_InitFlags : uint
    {
        SDL_INIT_TIMER = 0x1,
        SDL_INIT_AUDIO = 0x10,
        SDL_INIT_VIDEO = 0x20,
        SDL_INIT_JOYSTICK = 0x200,
        SDL_INIT_HAPTIC = 0x1000,
        SDL_INIT_GAMEPAD = 0x2000,
        SDL_INIT_EVENTS = 0x4000,
        SDL_INIT_SENSOR = 0x0_8000,
        SDL_INIT_CAMERA = 0x1_0000,
    }

    public enum SDL_AppResult
    {
        SDL_APP_CONTINUE = 0,
        SDL_APP_SUCCESS = 1,
        SDL_APP_FAILURE = 2,
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_AppResult SDL_AppInit_func(ref IntPtr appstate, int argc, IntPtr argv);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_AppResult SDL_AppIterate_func(IntPtr appstate);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_AppResult SDL_AppEvent_func(IntPtr appstate, ref SDL_Event @event);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_AppQuit_func(IntPtr appstate);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_Init(uint flags);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_InitSubSystem(uint flags);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_QuitSubSystem(uint flags);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_WasInit(uint flags);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_Quit();

    [DllImport(nativeLibName, EntryPoint = "SDL_SetAppMetadata", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetAppMetadata(byte* appname, byte* appversion, byte* appidentifier);
    public static bool SDL_SetAppMetadata(string appname, string appversion, string appidentifier)
    {
        var appnameUTF8 = EncodeAsUTF8(appname);
        var appversionUTF8 = EncodeAsUTF8(appversion);
        var appidentifierUTF8 = EncodeAsUTF8(appidentifier);

        var result = INTERNAL_SDL_SetAppMetadata(appnameUTF8, appversionUTF8, appidentifierUTF8);

        SDL_free((IntPtr)appnameUTF8);
        SDL_free((IntPtr)appversionUTF8);
        SDL_free((IntPtr)appidentifierUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_SetAppMetadataProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetAppMetadataProperty(byte* name, byte* value);
    public static bool SDL_SetAppMetadataProperty(string name, string value)
    {
        var nameUTF8 = EncodeAsUTF8(name);
        var valueUTF8 = EncodeAsUTF8(value);

        var result = INTERNAL_SDL_SetAppMetadataProperty(nameUTF8, valueUTF8);

        SDL_free((IntPtr)nameUTF8);
        SDL_free((IntPtr)valueUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetAppMetadataProperty", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GetAppMetadataProperty(byte* name);
    public static IntPtr SDL_GetAppMetadataProperty(string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_GetAppMetadataProperty(nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    // ../SDL3/SDL_loadso.h

    [DllImport(nativeLibName, EntryPoint = "SDL_LoadObject", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_LoadObject(byte* sofile);
    public static IntPtr SDL_LoadObject(string sofile)
    {
        var sofileUTF8 = EncodeAsUTF8(sofile);

        var result = INTERNAL_SDL_LoadObject(sofileUTF8);

        SDL_free((IntPtr)sofileUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LoadFunction", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_LoadFunction(IntPtr handle, byte* name);
    public static IntPtr SDL_LoadFunction(IntPtr handle, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_LoadFunction(handle, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnloadObject(IntPtr handle);

    // ../SDL3/SDL_locale.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Locale
    {
        public char* language;
        public char* country;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetPreferredLocales(ref int count); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_log.h

    public enum SDL_LogCategory
    {
        SDL_LOG_CATEGORY_APPLICATION = 0,
        SDL_LOG_CATEGORY_ERROR = 1,
        SDL_LOG_CATEGORY_ASSERT = 2,
        SDL_LOG_CATEGORY_SYSTEM = 3,
        SDL_LOG_CATEGORY_AUDIO = 4,
        SDL_LOG_CATEGORY_VIDEO = 5,
        SDL_LOG_CATEGORY_RENDER = 6,
        SDL_LOG_CATEGORY_INPUT = 7,
        SDL_LOG_CATEGORY_TEST = 8,
        SDL_LOG_CATEGORY_GPU = 9,
        SDL_LOG_CATEGORY_RESERVED2 = 10,
        SDL_LOG_CATEGORY_RESERVED3 = 11,
        SDL_LOG_CATEGORY_RESERVED4 = 12,
        SDL_LOG_CATEGORY_RESERVED5 = 13,
        SDL_LOG_CATEGORY_RESERVED6 = 14,
        SDL_LOG_CATEGORY_RESERVED7 = 15,
        SDL_LOG_CATEGORY_RESERVED8 = 16,
        SDL_LOG_CATEGORY_RESERVED9 = 17,
        SDL_LOG_CATEGORY_RESERVED10 = 18,
        SDL_LOG_CATEGORY_CUSTOM = 19,
    }

    public enum SDL_LogPriority
    {
        SDL_LOG_PRIORITY_VERBOSE = 1,
        SDL_LOG_PRIORITY_DEBUG = 2,
        SDL_LOG_PRIORITY_INFO = 3,
        SDL_LOG_PRIORITY_WARN = 4,
        SDL_LOG_PRIORITY_ERROR = 5,
        SDL_LOG_PRIORITY_CRITICAL = 6,
        SDL_NUM_LOG_PRIORITIES = 7,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetLogPriorities(SDL_LogPriority priority);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetLogPriority(int category, SDL_LogPriority priority);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SDL_LogPriority SDL_GetLogPriority(int category);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_ResetLogPriorities();

    [DllImport(nativeLibName, EntryPoint = "SDL_SetLogPriorityPrefix", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_SetLogPriorityPrefix(SDL_LogPriority priority, byte* prefix);
    public static bool SDL_SetLogPriorityPrefix(SDL_LogPriority priority, string prefix)
    {
        var prefixUTF8 = EncodeAsUTF8(prefix);

        var result = INTERNAL_SDL_SetLogPriorityPrefix(priority, prefixUTF8);

        SDL_free((IntPtr)prefixUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_Log", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_Log(byte* fmt);
    public static void SDL_Log(string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_Log(fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogVerbose", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogVerbose(int category, byte* fmt);
    public static void SDL_LogVerbose(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogVerbose(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogDebug", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogDebug(int category, byte* fmt);
    public static void SDL_LogDebug(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogDebug(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogInfo(int category, byte* fmt);
    public static void SDL_LogInfo(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogInfo(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogWarn", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogWarn(int category, byte* fmt);
    public static void SDL_LogWarn(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogWarn(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogError", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogError(int category, byte* fmt);
    public static void SDL_LogError(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogError(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogCritical", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogCritical(int category, byte* fmt);
    public static void SDL_LogCritical(int category, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogCritical(category, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [DllImport(nativeLibName, EntryPoint = "SDL_LogMessage", CallingConvention = CallingConvention.Cdecl)]
    private static extern void INTERNAL_SDL_LogMessage(int category, SDL_LogPriority priority, byte* fmt);
    public static void SDL_LogMessage(int category, SDL_LogPriority priority, string fmt)
    {
        var fmtUTF8 = EncodeAsUTF8(fmt);

        INTERNAL_SDL_LogMessage(category, priority, fmtUTF8);

        SDL_free((IntPtr)fmtUTF8);

    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_LogOutputFunction(IntPtr userdata, int category, SDL_LogPriority priority, IntPtr message);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GetLogOutputFunction(IntPtr callback, ref IntPtr userdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetLogOutputFunction(/* SDL_LogOutputFunction */ IntPtr callback, IntPtr userdata);

    // ../SDL3/SDL_messagebox.h

    [Flags]
    public enum SDL_MessageBoxFlags : uint
    {
        SDL_MESSAGEBOX_ERROR = 0x10,
        SDL_MESSAGEBOX_WARNING = 0x20,
        SDL_MESSAGEBOX_INFORMATION = 0x40,
        SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x080,
        SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x100,
    }

    [Flags]
    public enum SDL_MessageBoxButtonFlags : uint
    {
        SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x1,
        SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxButtonData
    {
        public uint flags;
        public int buttonID;
        public char* text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxColor
    {
        public byte r;
        public byte g;
        public byte b;
    }

    public enum SDL_MessageBoxColorType
    {
        SDL_MESSAGEBOX_COLOR_BACKGROUND = 0,
        SDL_MESSAGEBOX_COLOR_TEXT = 1,
        SDL_MESSAGEBOX_COLOR_BUTTON_BORDER = 2,
        SDL_MESSAGEBOX_COLOR_BUTTON_BACKGROUND = 3,
        SDL_MESSAGEBOX_COLOR_BUTTON_SELECTED = 4,
        SDL_MESSAGEBOX_COLOR_MAX = 5,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxColorScheme
    {
        public SDL_MessageBoxColor colors0;
        public SDL_MessageBoxColor colors1;
        public SDL_MessageBoxColor colors2;
        public SDL_MessageBoxColor colors3;
        public SDL_MessageBoxColor colors4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxData
    {
        public uint flags;
        public IntPtr window;
        public char* title;
        public char* message;
        public int numbuttons;
        public SDL_MessageBoxButtonData* buttons;
        public SDL_MessageBoxColorScheme* colorScheme;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ShowMessageBox(ref SDL_MessageBoxData messageboxdata, ref int buttonid); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, EntryPoint = "SDL_ShowSimpleMessageBox", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_ShowSimpleMessageBox(uint flags, byte* title, byte* message, IntPtr window);
    public static bool SDL_ShowSimpleMessageBox(uint flags, string title, string message, IntPtr window)
    {
        var titleUTF8 = EncodeAsUTF8(title);
        var messageUTF8 = EncodeAsUTF8(message);

        var result = INTERNAL_SDL_ShowSimpleMessageBox(flags, titleUTF8, messageUTF8, window);

        SDL_free((IntPtr)titleUTF8);
        SDL_free((IntPtr)messageUTF8);

        return result;
    }

    // ../SDL3/SDL_metal.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_Metal_CreateView(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_Metal_DestroyView(IntPtr view);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_Metal_GetLayer(IntPtr view);

    // ../SDL3/SDL_misc.h

    [DllImport(nativeLibName, EntryPoint = "SDL_OpenURL", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_OpenURL(byte* url);
    public static bool SDL_OpenURL(string url)
    {
        var urlUTF8 = EncodeAsUTF8(url);

        var result = INTERNAL_SDL_OpenURL(urlUTF8);

        SDL_free((IntPtr)urlUTF8);

        return result;
    }

    // ../SDL3/SDL_platform.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetPlatform();

    // ../SDL3/SDL_render.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Vertex
    {
        public SDL_FPoint position;
        public SDL_FColor color;
        public SDL_FPoint tex_coord;
    }

    public enum SDL_TextureAccess
    {
        SDL_TEXTUREACCESS_STATIC = 0,
        SDL_TEXTUREACCESS_STREAMING = 1,
        SDL_TEXTUREACCESS_TARGET = 2,
    }

    public enum SDL_RendererLogicalPresentation
    {
        SDL_LOGICAL_PRESENTATION_DISABLED = 0,
        SDL_LOGICAL_PRESENTATION_STRETCH = 1,
        SDL_LOGICAL_PRESENTATION_LETTERBOX = 2,
        SDL_LOGICAL_PRESENTATION_OVERSCAN = 3,
        SDL_LOGICAL_PRESENTATION_INTEGER_SCALE = 4,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetNumRenderDrivers();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderDriver(int index);

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateWindowAndRenderer", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_CreateWindowAndRenderer(byte* title, int width, int height, ulong window_flags, ref IntPtr window, ref IntPtr renderer); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_CreateWindowAndRenderer(string title, int width, int height, ulong window_flags, ref IntPtr window, ref IntPtr renderer)
    {
        var titleUTF8 = EncodeAsUTF8(title);

        var result = INTERNAL_SDL_CreateWindowAndRenderer(titleUTF8, width, height, window_flags, ref window, ref renderer);

        SDL_free((IntPtr)titleUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_CreateRenderer(IntPtr window, byte* name);
    public static IntPtr SDL_CreateRenderer(IntPtr window, string name)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_CreateRenderer(window, nameUTF8);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateRendererWithProperties(uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateSoftwareRenderer(ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderer(IntPtr window);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderWindow(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRendererName(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetRendererProperties(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderOutputSize(IntPtr renderer, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetCurrentRenderOutputSize(IntPtr renderer, ref int w, ref int h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateTexture(IntPtr renderer, SDL_PixelFormat format, SDL_TextureAccess access, int w, int h);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, ref SDL_Surface surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateTextureWithProperties(IntPtr renderer, uint props);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GetTextureProperties(IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRendererFromTexture(IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureSize(IntPtr texture, ref float w, ref float h); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureColorModFloat(IntPtr texture, float r, float g, float b);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureColorMod(IntPtr texture, ref byte r, ref byte g, ref byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureColorModFloat(IntPtr texture, ref float r, ref float g, ref float b); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureAlphaMod(IntPtr texture, byte alpha);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureAlphaModFloat(IntPtr texture, float alpha);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureAlphaMod(IntPtr texture, ref byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureAlphaModFloat(IntPtr texture, ref float alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureBlendMode(IntPtr texture, uint blendMode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureBlendMode(IntPtr texture, IntPtr blendMode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTextureScaleMode(IntPtr texture, SDL_ScaleMode scaleMode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetTextureScaleMode(IntPtr texture, ref SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateTexture(IntPtr texture, ref SDL_Rect rect, IntPtr pixels, int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateYUVTexture(IntPtr texture, ref SDL_Rect rect, ref byte Yplane, int Ypitch, ref byte Uplane, int Upitch, ref byte Vplane, int Vpitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_UpdateNVTexture(IntPtr texture, ref SDL_Rect rect, ref byte Yplane, int Ypitch, ref byte UVplane, int UVpitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LockTexture(IntPtr texture, ref SDL_Rect rect, ref IntPtr pixels, ref int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_LockTextureToSurface(IntPtr texture, ref SDL_Rect rect, ref IntPtr surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_UnlockTexture(IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderLogicalPresentation(IntPtr renderer, int w, int h, SDL_RendererLogicalPresentation mode, SDL_ScaleMode scale_mode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderLogicalPresentation(IntPtr renderer, ref int w, ref int h, ref SDL_RendererLogicalPresentation mode, ref SDL_ScaleMode scale_mode); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderLogicalPresentationRect(IntPtr renderer, ref SDL_FRect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderCoordinatesFromWindow(IntPtr renderer, float window_x, float window_y, ref float x, ref float y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderCoordinatesToWindow(IntPtr renderer, float x, float y, ref float window_x, ref float window_y); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_ConvertEventToRenderCoordinates(IntPtr renderer, ref SDL_Event @event); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderViewport(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderViewport(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderViewportSet(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderSafeArea(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderClipRect(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderClipRect(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderClipEnabled(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderScale(IntPtr renderer, float scaleX, float scaleY);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderScale(IntPtr renderer, ref float scaleX, ref float scaleY); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderDrawColorFloat(IntPtr renderer, float r, float g, float b, float a);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderDrawColor(IntPtr renderer, ref byte r, ref byte g, ref byte b, ref byte a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderDrawColorFloat(IntPtr renderer, ref float r, ref float g, ref float b, ref float a); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderColorScale(IntPtr renderer, float scale);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderColorScale(IntPtr renderer, ref float scale); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderDrawBlendMode(IntPtr renderer, uint blendMode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderDrawBlendMode(IntPtr renderer, IntPtr blendMode);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderClear(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderPoint(IntPtr renderer, float x, float y);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderPoints(IntPtr renderer, ref SDL_FPoint points, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderLine(IntPtr renderer, float x1, float y1, float x2, float y2);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderLines(IntPtr renderer, ref SDL_FPoint points, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderRect(IntPtr renderer, ref SDL_FRect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderRects(IntPtr renderer, ref SDL_FRect rects, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderFillRect(IntPtr renderer, ref SDL_FRect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderFillRects(IntPtr renderer, ref SDL_FRect rects, int count); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderTexture(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, ref SDL_FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderTextureRotated(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, ref SDL_FRect dstrect, double angle, ref SDL_FPoint center, SDL_FlipMode flip); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderTextureTiled(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, float scale, ref SDL_FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderTexture9Grid(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, ref SDL_FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderGeometry(IntPtr renderer, IntPtr texture, ref SDL_Vertex vertices, int num_vertices, ref int indices, int num_indices); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderGeometryRaw(IntPtr renderer, IntPtr texture, ref float xy, int xy_stride, ref SDL_FColor color, int color_stride, ref float uv, int uv_stride, int num_vertices, IntPtr indices, int num_indices, int size_indices); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_RenderReadPixels(IntPtr renderer, ref SDL_Rect rect); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RenderPresent(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyTexture(IntPtr texture);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyRenderer(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_FlushRenderer(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderMetalLayer(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRenderMetalCommandEncoder(IntPtr renderer);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_AddVulkanRenderSemaphores(IntPtr renderer, uint wait_stage_mask, long wait_semaphore, long signal_semaphore);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetRenderVSync(IntPtr renderer, int vsync);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetRenderVSync(IntPtr renderer, ref int vsync); // WARN_UNKNOWN_POINTER_PARAMETER

    // ../SDL3/SDL_storage.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_StorageInterface
    {
        public uint version;
        public IntPtr close; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr ready; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr enumerate; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr info; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr read_file; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr write_file; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr mkdir; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr remove; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr rename; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr copy; // WARN_ANONYMOUS_FUNCTION_POINTER
        public IntPtr space_remaining; // WARN_ANONYMOUS_FUNCTION_POINTER
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_OpenTitleStorage", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_OpenTitleStorage(byte* @override, uint props);
    public static IntPtr SDL_OpenTitleStorage(string @override, uint props)
    {
        var @overrideUTF8 = EncodeAsUTF8(@override);

        var result = INTERNAL_SDL_OpenTitleStorage(@overrideUTF8, props);

        SDL_free((IntPtr)@overrideUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_OpenUserStorage", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_OpenUserStorage(byte* org, byte* app, uint props);
    public static IntPtr SDL_OpenUserStorage(string org, string app, uint props)
    {
        var orgUTF8 = EncodeAsUTF8(org);
        var appUTF8 = EncodeAsUTF8(app);

        var result = INTERNAL_SDL_OpenUserStorage(orgUTF8, appUTF8, props);

        SDL_free((IntPtr)orgUTF8);
        SDL_free((IntPtr)appUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_OpenFileStorage", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_OpenFileStorage(byte* path);
    public static IntPtr SDL_OpenFileStorage(string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_OpenFileStorage(pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_OpenStorage(ref SDL_StorageInterface iface, IntPtr userdata); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_CloseStorage(IntPtr storage);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_StorageReady(IntPtr storage);

    [DllImport(nativeLibName, EntryPoint = "SDL_GetStorageFileSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GetStorageFileSize(IntPtr storage, byte* path, ref ulong length); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_GetStorageFileSize(IntPtr storage, string path, ref ulong length)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_GetStorageFileSize(storage, pathUTF8, ref length);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_ReadStorageFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_ReadStorageFile(IntPtr storage, byte* path, IntPtr destination, ulong length);
    public static bool SDL_ReadStorageFile(IntPtr storage, string path, IntPtr destination, ulong length)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_ReadStorageFile(storage, pathUTF8, destination, length);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_WriteStorageFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_WriteStorageFile(IntPtr storage, byte* path, IntPtr source, ulong length);
    public static bool SDL_WriteStorageFile(IntPtr storage, string path, IntPtr source, ulong length)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_WriteStorageFile(storage, pathUTF8, source, length);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_CreateStorageDirectory(IntPtr storage, byte* path);
    public static bool SDL_CreateStorageDirectory(IntPtr storage, string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_CreateStorageDirectory(storage, pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_EnumerateStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_EnumerateStorageDirectory(IntPtr storage, byte* path, /* SDL_EnumerateDirectoryCallback */ IntPtr callback, IntPtr userdata);
    public static bool SDL_EnumerateStorageDirectory(IntPtr storage, string path, /* SDL_EnumerateDirectoryCallback */ IntPtr callback, IntPtr userdata)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_EnumerateStorageDirectory(storage, pathUTF8, callback, userdata);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_RemoveStoragePath", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_RemoveStoragePath(IntPtr storage, byte* path);
    public static bool SDL_RemoveStoragePath(IntPtr storage, string path)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_RemoveStoragePath(storage, pathUTF8);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_RenameStoragePath", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_RenameStoragePath(IntPtr storage, byte* oldpath, byte* newpath);
    public static bool SDL_RenameStoragePath(IntPtr storage, string oldpath, string newpath)
    {
        var oldpathUTF8 = EncodeAsUTF8(oldpath);
        var newpathUTF8 = EncodeAsUTF8(newpath);

        var result = INTERNAL_SDL_RenameStoragePath(storage, oldpathUTF8, newpathUTF8);

        SDL_free((IntPtr)oldpathUTF8);
        SDL_free((IntPtr)newpathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_CopyStorageFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_CopyStorageFile(IntPtr storage, byte* oldpath, byte* newpath);
    public static bool SDL_CopyStorageFile(IntPtr storage, string oldpath, string newpath)
    {
        var oldpathUTF8 = EncodeAsUTF8(oldpath);
        var newpathUTF8 = EncodeAsUTF8(newpath);

        var result = INTERNAL_SDL_CopyStorageFile(storage, oldpathUTF8, newpathUTF8);

        SDL_free((IntPtr)oldpathUTF8);
        SDL_free((IntPtr)newpathUTF8);

        return result;
    }

    [DllImport(nativeLibName, EntryPoint = "SDL_GetStoragePathInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool INTERNAL_SDL_GetStoragePathInfo(IntPtr storage, byte* path, ref SDL_PathInfo info); // WARN_UNKNOWN_POINTER_PARAMETER
    public static bool SDL_GetStoragePathInfo(IntPtr storage, string path, ref SDL_PathInfo info)
    {
        var pathUTF8 = EncodeAsUTF8(path);

        var result = INTERNAL_SDL_GetStoragePathInfo(storage, pathUTF8, ref info);

        SDL_free((IntPtr)pathUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetStorageSpaceRemaining(IntPtr storage);

    [DllImport(nativeLibName, EntryPoint = "SDL_GlobStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_GlobStorageDirectory(IntPtr storage, byte* path, byte* pattern, uint flags, ref int count); // WARN_UNKNOWN_POINTER_PARAMETER
    public static IntPtr SDL_GlobStorageDirectory(IntPtr storage, string path, string pattern, uint flags, ref int count)
    {
        var pathUTF8 = EncodeAsUTF8(path);
        var patternUTF8 = EncodeAsUTF8(pattern);

        var result = INTERNAL_SDL_GlobStorageDirectory(storage, pathUTF8, patternUTF8, flags, ref count);

        SDL_free((IntPtr)pathUTF8);
        SDL_free((IntPtr)patternUTF8);

        return result;
    }

    // ../SDL3/SDL_system.h

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool SDL_X11EventHook(IntPtr userdata, IntPtr xevent);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_SetX11EventHook(/* SDL_X11EventHook */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetLinuxThreadPriority(long threadID, int priority);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetLinuxThreadPriorityAndPolicy(long threadID, int sdlPriority, int schedPolicy);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_IsTablet();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationWillTerminate();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationDidReceiveMemoryWarning();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationWillEnterBackground();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationDidEnterBackground();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationWillEnterForeground();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_OnApplicationDidEnterForeground();

    // ../SDL3/SDL_thread.h

    public enum SDL_ThreadPriority
    {
        SDL_THREAD_PRIORITY_LOW = 0,
        SDL_THREAD_PRIORITY_NORMAL = 1,
        SDL_THREAD_PRIORITY_HIGH = 2,
        SDL_THREAD_PRIORITY_TIME_CRITICAL = 3,
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SDL_ThreadFunction(IntPtr data);

    [DllImport(nativeLibName, EntryPoint = "SDL_CreateThreadRuntime", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr INTERNAL_SDL_CreateThreadRuntime(/* SDL_ThreadFunction */ IntPtr fn, byte* name, IntPtr data, /* SDL_FunctionPointer */ IntPtr pfnBeginThread, /* SDL_FunctionPointer */ IntPtr pfnEndThread);
    public static IntPtr SDL_CreateThreadRuntime(/* SDL_ThreadFunction */ IntPtr fn, string name, IntPtr data, /* SDL_FunctionPointer */ IntPtr pfnBeginThread, /* SDL_FunctionPointer */ IntPtr pfnEndThread)
    {
        var nameUTF8 = EncodeAsUTF8(name);

        var result = INTERNAL_SDL_CreateThreadRuntime(fn, nameUTF8, data, pfnBeginThread, pfnEndThread);

        SDL_free((IntPtr)nameUTF8);

        return result;
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateThreadWithPropertiesRuntime(uint props, /* SDL_FunctionPointer */ IntPtr pfnBeginThread, /* SDL_FunctionPointer */ IntPtr pfnEndThread);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetThreadName(IntPtr thread);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetCurrentThreadID();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetThreadID(IntPtr thread);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetThreadPriority(SDL_ThreadPriority priority);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_WaitThread(IntPtr thread, ref int status); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DetachThread(IntPtr thread);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetTLS(IntPtr id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_TLSDestructorCallback(IntPtr value);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_SetTLS(IntPtr id, IntPtr value, /* SDL_TLSDestructorCallback */ IntPtr destructor);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_CleanupTLS();

    // ../SDL3/SDL_time.h

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DateTime
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int nanosecond;
        public int day_of_week;
        public int utc_offset;
    }

    public enum SDL_DateFormat
    {
        SDL_DATE_FORMAT_YYYYMMDD = 0,
        SDL_DATE_FORMAT_DDMMYYYY = 1,
        SDL_DATE_FORMAT_MMDDYYYY = 2,
    }

    public enum SDL_TimeFormat
    {
        SDL_TIME_FORMAT_24HR = 0,
        SDL_TIME_FORMAT_12HR = 1,
    }

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetDateTimeLocalePreferences(ref SDL_DateFormat dateFormat, ref SDL_TimeFormat timeFormat); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_GetCurrentTime(IntPtr ticks);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_TimeToDateTime(long ticks, ref SDL_DateTime dt, bool localTime); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_DateTimeToTime(ref SDL_DateTime dt, IntPtr ticks); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_TimeToWindows(long ticks, ref uint dwLowDateTime, ref uint dwHighDateTime); // WARN_UNKNOWN_POINTER_PARAMETER

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetDaysInMonth(int year, int month);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetDayOfYear(int year, int month, int day);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetDayOfWeek(int year, int month, int day);

    // ../SDL3/SDL_timer.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetTicks();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetTicksNS();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetPerformanceCounter();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong SDL_GetPerformanceFrequency();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_Delay(uint ms);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DelayNS(ulong ns);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint SDL_TimerCallback(IntPtr userdata, uint timerID, uint interval);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_AddTimer(uint interval, /* SDL_TimerCallback */ IntPtr callback, IntPtr userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong SDL_NSTimerCallback(IntPtr userdata, uint timerID, ulong interval);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_AddTimerNS(ulong interval, /* SDL_NSTimerCallback */ IntPtr callback, IntPtr userdata);

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SDL_RemoveTimer(uint id);

    // ../SDL3/SDL_version.h

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GetVersion();

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_GetRevision();


}
