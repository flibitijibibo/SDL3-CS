// NOTE: This file is auto-generated.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL3
{

	public static unsafe class SDL
	{
		private const string nativeLibName = "SDL3";

		private static byte* EncodeAsUTF8(string str)
		{
			if (str == null)
			{
				return (byte*) 0;
			}

			var size = (str.Length * 4) + 1;
			var buffer = (byte*) SDL_malloc((UIntPtr) size);
			fixed (char* strPtr = str)
			{
				Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, size);
			}

			return buffer;
		}

		private static string DecodeFromUTF8(IntPtr ptr, bool shouldFree = false)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}

			var end = (byte*) ptr;
			while (*end != 0)
			{
				end++;
			}

			var result = new string(
				(sbyte*) ptr,
				0,
				(int) (end - (byte*) ptr),
				System.Text.Encoding.UTF8
			);

			if (shouldFree)
			{
				SDL_free(ptr);
			}

			return result;
		}

		// /usr/local/include/SDL3/SDL_stdinc.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_malloc(UIntPtr size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_free(IntPtr mem);

		// /usr/local/include/SDL3/SDL_assert.h

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
			public byte always_ignore;
			public uint trigger_count;
			public byte* condition;
			public byte* filename;
			public int linenum;
			public byte* function;
			public SDL_AssertData* next;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ReportAssertion", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_AssertState INTERNAL_SDL_ReportAssertion(ref SDL_AssertData data, byte* func, byte* file, int line);
		public static SDL_AssertState SDL_ReportAssertion(ref SDL_AssertData data, string func, string file, int line)
		{
			var funcUTF8 = EncodeAsUTF8(func);
			var fileUTF8 = EncodeAsUTF8(file);
			var result = INTERNAL_SDL_ReportAssertion(ref data, funcUTF8, fileUTF8, line);

			SDL_free((IntPtr) funcUTF8);
			SDL_free((IntPtr) fileUTF8);
			return result;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL_AssertState SDL_AssertionHandler(SDL_AssertData* data, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetAssertionHandler(SDL_AssertionHandler handler, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDefaultAssertionHandler();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAssertionHandler(out IntPtr puserdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAssertionReport();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetAssertionReport();

		// /usr/local/include/SDL3/SDL_atomic.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_TryLockSpinlock(IntPtr @lock);

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
		public static extern byte SDL_CompareAndSwapAtomicInt(ref SDL_AtomicInt a, int oldval, int newval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetAtomicInt(ref SDL_AtomicInt a, int v);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAtomicInt(ref SDL_AtomicInt a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AddAtomicInt(ref SDL_AtomicInt a, int v);

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_AtomicU32
		{
			public uint value;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CompareAndSwapAtomicU32(ref SDL_AtomicU32 a, uint oldval, uint newval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_SetAtomicU32(ref SDL_AtomicU32 a, uint v);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAtomicU32(ref SDL_AtomicU32 a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CompareAndSwapAtomicPointer(ref IntPtr a, IntPtr oldval, IntPtr newval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SetAtomicPointer(ref IntPtr a, IntPtr v);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAtomicPointer(ref IntPtr a);

		// /usr/local/include/SDL3/SDL_endian.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_SwapFloat(float x);

		// /usr/local/include/SDL3/SDL_error.h

		[DllImport(nativeLibName, EntryPoint = "SDL_SetError", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetError(byte* fmt);
		public static byte SDL_SetError(string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			var result = INTERNAL_SDL_SetError(fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_OutOfMemory();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetError();
		public static string SDL_GetError()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetError());
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClearError();

		// /usr/local/include/SDL3/SDL_properties.h

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
		public static extern byte SDL_CopyProperties(uint src, uint dst);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LockProperties(uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockProperties(uint props);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_CleanupPropertyCallback(IntPtr userdata, IntPtr value);

		[DllImport(nativeLibName, EntryPoint = "SDL_SetPointerPropertyWithCleanup", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetPointerPropertyWithCleanup(uint props, byte* name, IntPtr value, SDL_CleanupPropertyCallback cleanup, IntPtr userdata);
		public static byte SDL_SetPointerPropertyWithCleanup(uint props, string name, IntPtr value, SDL_CleanupPropertyCallback cleanup, IntPtr userdata)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetPointerPropertyWithCleanup(props, nameUTF8, value, cleanup, userdata);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetPointerProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetPointerProperty(uint props, byte* name, IntPtr value);
		public static byte SDL_SetPointerProperty(uint props, string name, IntPtr value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetPointerProperty(props, nameUTF8, value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetStringProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetStringProperty(uint props, byte* name, byte* value);
		public static byte SDL_SetStringProperty(uint props, string name, string value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var valueUTF8 = EncodeAsUTF8(value);
			var result = INTERNAL_SDL_SetStringProperty(props, nameUTF8, valueUTF8);

			SDL_free((IntPtr) nameUTF8);
			SDL_free((IntPtr) valueUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetNumberProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetNumberProperty(uint props, byte* name, long value);
		public static byte SDL_SetNumberProperty(uint props, string name, long value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetNumberProperty(props, nameUTF8, value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetFloatProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetFloatProperty(uint props, byte* name, float value);
		public static byte SDL_SetFloatProperty(uint props, string name, float value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetFloatProperty(props, nameUTF8, value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetBooleanProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetBooleanProperty(uint props, byte* name, byte value);
		public static byte SDL_SetBooleanProperty(uint props, string name, byte value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetBooleanProperty(props, nameUTF8, value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_HasProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_HasProperty(uint props, byte* name);
		public static byte SDL_HasProperty(uint props, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_HasProperty(props, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPropertyType", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_PropertyType INTERNAL_SDL_GetPropertyType(uint props, byte* name);
		public static SDL_PropertyType SDL_GetPropertyType(uint props, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetPropertyType(props, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPointerProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetPointerProperty(uint props, byte* name, IntPtr default_value);
		public static IntPtr SDL_GetPointerProperty(uint props, string name, IntPtr default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetPointerProperty(props, nameUTF8, default_value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetStringProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetStringProperty(uint props, byte* name, byte* default_value);
		public static string SDL_GetStringProperty(uint props, string name, string default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var default_valueUTF8 = EncodeAsUTF8(default_value);
			var result = DecodeFromUTF8(INTERNAL_SDL_GetStringProperty(props, nameUTF8, default_valueUTF8));

			SDL_free((IntPtr) nameUTF8);
			SDL_free((IntPtr) default_valueUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetNumberProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern long INTERNAL_SDL_GetNumberProperty(uint props, byte* name, long default_value);
		public static long SDL_GetNumberProperty(uint props, string name, long default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetNumberProperty(props, nameUTF8, default_value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetFloatProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern float INTERNAL_SDL_GetFloatProperty(uint props, byte* name, float default_value);
		public static float SDL_GetFloatProperty(uint props, string name, float default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetFloatProperty(props, nameUTF8, default_value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetBooleanProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GetBooleanProperty(uint props, byte* name, byte default_value);
		public static byte SDL_GetBooleanProperty(uint props, string name, byte default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetBooleanProperty(props, nameUTF8, default_value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ClearProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_ClearProperty(uint props, byte* name);
		public static byte SDL_ClearProperty(uint props, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_ClearProperty(props, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_EnumeratePropertiesCallback(IntPtr userdata, uint props, byte* name);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_EnumerateProperties(uint props, SDL_EnumeratePropertiesCallback callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyProperties(uint props);

		// /usr/local/include/SDL3/SDL_thread.h

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
		private static extern IntPtr INTERNAL_SDL_CreateThreadRuntime(SDL_ThreadFunction fn, byte* name, IntPtr data, IntPtr pfnBeginThread, IntPtr pfnEndThread);
		public static IntPtr SDL_CreateThreadRuntime(SDL_ThreadFunction fn, string name, IntPtr data, IntPtr pfnBeginThread, IntPtr pfnEndThread)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_CreateThreadRuntime(fn, nameUTF8, data, pfnBeginThread, pfnEndThread);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateThreadWithPropertiesRuntime(uint props, IntPtr pfnBeginThread, IntPtr pfnEndThread);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetThreadName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetThreadName(IntPtr thread);
		public static string SDL_GetThreadName(IntPtr thread)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetThreadName(thread));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetCurrentThreadID();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetThreadID(IntPtr thread);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetThreadPriority(SDL_ThreadPriority priority);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WaitThread(IntPtr thread, IntPtr status);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DetachThread(IntPtr thread);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTLS(IntPtr id);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_TLSDestructorCallback(IntPtr value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTLS(IntPtr id, IntPtr value, SDL_TLSDestructorCallback destructor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CleanupTLS();

		// /usr/local/include/SDL3/SDL_mutex.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateMutex();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockMutex(IntPtr mutex);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_TryLockMutex(IntPtr mutex);

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
		public static extern byte SDL_TryLockRWLockForReading(IntPtr rwlock);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_TryLockRWLockForWriting(IntPtr rwlock);

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
		public static extern byte SDL_TryWaitSemaphore(IntPtr sem);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitSemaphoreTimeout(IntPtr sem, int timeoutMS);

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
		public static extern byte SDL_WaitConditionTimeout(IntPtr cond, IntPtr mutex, int timeoutMS);

		public enum SDL_InitStatus
		{
			SDL_INIT_STATUS_UNINITIALIZED = 0,
			SDL_INIT_STATUS_INITIALIZING = 1,
			SDL_INIT_STATUS_INITIALIZED = 2,
			SDL_INIT_STATUS_UNINITIALIZING = 3,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_InitState
		{
			public SDL_AtomicInt status;
			public ulong thread;
			public IntPtr reserved;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShouldInit(ref SDL_InitState state);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShouldQuit(ref SDL_InitState state);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetInitialized(ref SDL_InitState state, byte initialized);

		// /usr/local/include/SDL3/SDL_iostream.h

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
			public IntPtr flush; // WARN_ANONYMOUS_FUNCTION_POINTER
			public IntPtr close; // WARN_ANONYMOUS_FUNCTION_POINTER
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_IOFromFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_IOFromFile(byte* file, byte* mode);
		public static IntPtr SDL_IOFromFile(string file, string mode)
		{
			var fileUTF8 = EncodeAsUTF8(file);
			var modeUTF8 = EncodeAsUTF8(mode);
			var result = INTERNAL_SDL_IOFromFile(fileUTF8, modeUTF8);

			SDL_free((IntPtr) fileUTF8);
			SDL_free((IntPtr) modeUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromMem(IntPtr mem, UIntPtr size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromConstMem(IntPtr mem, UIntPtr size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromDynamicMem();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenIO(ref SDL_IOStreamInterface iface, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CloseIO(IntPtr context);

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

			SDL_free((IntPtr) fmtUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FlushIO(IntPtr context);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadFile_IO(IntPtr src, out UIntPtr datasize, byte closeio);

		[DllImport(nativeLibName, EntryPoint = "SDL_LoadFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadFile(byte* file, out UIntPtr datasize);
		public static IntPtr SDL_LoadFile(string file, out UIntPtr datasize)
		{
			var fileUTF8 = EncodeAsUTF8(file);
			var result = INTERNAL_SDL_LoadFile(fileUTF8, out datasize);

			SDL_free((IntPtr) fileUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU8(IntPtr src, out byte value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS8(IntPtr src, out sbyte value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU16LE(IntPtr src, out ushort value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS16LE(IntPtr src, out short value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU16BE(IntPtr src, out ushort value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS16BE(IntPtr src, out short value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU32LE(IntPtr src, out uint value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS32LE(IntPtr src, out int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU32BE(IntPtr src, out uint value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS32BE(IntPtr src, out int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU64LE(IntPtr src, out ulong value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS64LE(IntPtr src, out long value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU64BE(IntPtr src, out ulong value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadS64BE(IntPtr src, out long value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU8(IntPtr dst, byte value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS8(IntPtr dst, sbyte value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU16LE(IntPtr dst, ushort value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS16LE(IntPtr dst, short value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU16BE(IntPtr dst, ushort value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS16BE(IntPtr dst, short value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU32LE(IntPtr dst, uint value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS32LE(IntPtr dst, int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU32BE(IntPtr dst, uint value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS32BE(IntPtr dst, int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU64LE(IntPtr dst, ulong value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS64LE(IntPtr dst, long value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteU64BE(IntPtr dst, ulong value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteS64BE(IntPtr dst, long value);

		// /usr/local/include/SDL3/SDL_audio.h

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetAudioDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetAudioDriver(int index);
		public static string SDL_GetAudioDriver(int index)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetAudioDriver(index));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetCurrentAudioDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetCurrentAudioDriver();
		public static string SDL_GetCurrentAudioDriver()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetCurrentAudioDriver());
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioPlaybackDevices(out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioRecordingDevices(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetAudioDeviceName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetAudioDeviceName(uint devid);
		public static string SDL_GetAudioDeviceName(uint devid)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetAudioDeviceName(devid));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetAudioDeviceFormat(uint devid, out SDL_AudioSpec spec, out int sample_frames);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioDeviceChannelMap(uint devid, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_OpenAudioDevice(uint devid, ref SDL_AudioSpec spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PauseAudioDevice(uint dev);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ResumeAudioDevice(uint dev);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_AudioDevicePaused(uint dev);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioDeviceGain(uint devid);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioDeviceGain(uint devid, float gain);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudioDevice(uint devid);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BindAudioStreams(uint devid, IntPtr[] streams, int num_streams);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BindAudioStream(uint devid, IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnbindAudioStreams(IntPtr[] streams, int num_streams);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnbindAudioStream(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAudioStreamDevice(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateAudioStream(ref SDL_AudioSpec src_spec, ref SDL_AudioSpec dst_spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAudioStreamProperties(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetAudioStreamFormat(IntPtr stream, out SDL_AudioSpec src_spec, out SDL_AudioSpec dst_spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamFormat(IntPtr stream, ref SDL_AudioSpec src_spec, ref SDL_AudioSpec dst_spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioStreamFrequencyRatio(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamFrequencyRatio(IntPtr stream, float ratio);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioStreamGain(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamGain(IntPtr stream, float gain);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioStreamInputChannelMap(IntPtr stream, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioStreamOutputChannelMap(IntPtr stream, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamInputChannelMap(IntPtr stream, int[] chmap, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamOutputChannelMap(IntPtr stream, int[] chmap, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PutAudioStreamData(IntPtr stream, IntPtr buf, int len);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamData(IntPtr stream, IntPtr buf, int len);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamAvailable(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamQueued(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FlushAudioStream(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClearAudioStream(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PauseAudioStreamDevice(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ResumeAudioStreamDevice(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LockAudioStream(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UnlockAudioStream(IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioStreamCallback(IntPtr userdata, IntPtr stream, int additional_amount, int total_amount);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamGetCallback(IntPtr stream, SDL_AudioStreamCallback callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioStreamPutCallback(IntPtr stream, SDL_AudioStreamCallback callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyAudioStream(IntPtr stream);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenAudioDeviceStream(uint devid, ref SDL_AudioSpec spec, SDL_AudioStreamCallback callback, IntPtr userdata);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioPostmixCallback(IntPtr userdata, SDL_AudioSpec* spec, float* buffer, int buflen);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetAudioPostmixCallback(uint devid, SDL_AudioPostmixCallback callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LoadWAV_IO(IntPtr src, byte closeio, out SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len);

		[DllImport(nativeLibName, EntryPoint = "SDL_LoadWAV", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_LoadWAV(byte* path, out SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len);
		public static byte SDL_LoadWAV(string path, out SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_LoadWAV(pathUTF8, out spec, out audio_buf, out audio_len);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_MixAudio(IntPtr dst, IntPtr src, SDL_AudioFormat format, uint len, float volume);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ConvertAudioSamples(ref SDL_AudioSpec src_spec, IntPtr src_data, int src_len, ref SDL_AudioSpec dst_spec, IntPtr dst_data, out int dst_len);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetAudioFormatName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetAudioFormatName(SDL_AudioFormat format);
		public static string SDL_GetAudioFormatName(SDL_AudioFormat format)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetAudioFormatName(format));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);

		// /usr/local/include/SDL3/SDL_bits.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_MostSignificantBitIndex32(uint x);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasExactlyOneBitSet32(uint x);

		// /usr/local/include/SDL3/SDL_blendmode.h

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

		// /usr/local/include/SDL3/SDL_pixels.h

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
			SDL_PIXELFORMAT_RGBA32 = 376840196,
			SDL_PIXELFORMAT_ARGB32 = 377888772,
			SDL_PIXELFORMAT_BGRA32 = 372645892,
			SDL_PIXELFORMAT_ABGR32 = 373694468,
			SDL_PIXELFORMAT_RGBX32 = 374740996,
			SDL_PIXELFORMAT_XRGB32 = 375789572,
			SDL_PIXELFORMAT_BGRX32 = 370546692,
			SDL_PIXELFORMAT_XBGR32 = 371595268,
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
			SDL_COLORSPACE_RGB_DEFAULT = 301991328,
			SDL_COLORSPACE_YUV_DEFAULT = 570426566,
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
			public fixed byte padding[2];
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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPixelFormatName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetPixelFormatName(SDL_PixelFormat format);
		public static string SDL_GetPixelFormatName(SDL_PixelFormat format)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetPixelFormatName(format));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetMasksForPixelFormat(SDL_PixelFormat format, out int bpp, out uint Rmask, out uint Gmask, out uint Bmask, out uint Amask);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetPixelFormatDetails(SDL_PixelFormat format);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreatePalette(int ncolors);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetPaletteColors(IntPtr palette, SDL_Color[] colors, int firstcolor, int ncolors);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyPalette(IntPtr palette);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGB(IntPtr format, IntPtr palette, byte r, byte g, byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGBA(IntPtr format, IntPtr palette, byte r, byte g, byte b, byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGB(uint pixel, IntPtr format, IntPtr palette, out byte r, out byte g, out byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGBA(uint pixel, IntPtr format, IntPtr palette, out byte r, out byte g, out byte b, out byte a);

		// /usr/local/include/SDL3/SDL_rect.h

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
		public static extern void SDL_RectToFRect(ref SDL_Rect rect, out SDL_FRect frect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PointInRect(ref SDL_Point p, ref SDL_Rect r);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RectEmpty(ref SDL_Rect r);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RectsEqual(ref SDL_Rect a, ref SDL_Rect b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasRectIntersection(ref SDL_Rect A, ref SDL_Rect B);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectIntersection(ref SDL_Rect A, ref SDL_Rect B, out SDL_Rect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectUnion(ref SDL_Rect A, ref SDL_Rect B, out SDL_Rect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectEnclosingPoints(SDL_Point[] points, int count, ref SDL_Rect clip, out SDL_Rect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectAndLineIntersection(ref SDL_Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PointInRectFloat(ref SDL_FPoint p, ref SDL_FRect r);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RectEmptyFloat(ref SDL_FRect r);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RectsEqualEpsilon(ref SDL_FRect a, ref SDL_FRect b, float epsilon);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RectsEqualFloat(ref SDL_FRect a, ref SDL_FRect b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasRectIntersectionFloat(ref SDL_FRect A, ref SDL_FRect B);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectIntersectionFloat(ref SDL_FRect A, ref SDL_FRect B, out SDL_FRect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectUnionFloat(ref SDL_FRect A, ref SDL_FRect B, out SDL_FRect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectEnclosingPointsFloat(SDL_FPoint[] points, int count, ref SDL_FRect clip, out SDL_FRect result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRectAndLineIntersectionFloat(ref SDL_FRect rect, ref float X1, ref float Y1, ref float X2, ref float Y2);

		// /usr/local/include/SDL3/SDL_surface.h

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
			public SDL_SurfaceFlags flags;
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
		public static extern void SDL_DestroySurface(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSurfaceProperties(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceColorspace(IntPtr surface, SDL_Colorspace colorspace);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_Colorspace SDL_GetSurfaceColorspace(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSurfacePalette(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfacePalette(IntPtr surface, IntPtr palette);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSurfacePalette(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_AddSurfaceAlternateImage(IntPtr surface, IntPtr image);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SurfaceHasAlternateImages(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSurfaceImages(IntPtr surface, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RemoveSurfaceAlternateImages(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LockSurface(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSurface(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadBMP_IO(IntPtr src, byte closeio);

		[DllImport(nativeLibName, EntryPoint = "SDL_LoadBMP", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadBMP(byte* file);
		public static IntPtr SDL_LoadBMP(string file)
		{
			var fileUTF8 = EncodeAsUTF8(file);
			var result = INTERNAL_SDL_LoadBMP(fileUTF8);

			SDL_free((IntPtr) fileUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SaveBMP_IO(IntPtr surface, IntPtr dst, byte closeio);

		[DllImport(nativeLibName, EntryPoint = "SDL_SaveBMP", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SaveBMP(IntPtr surface, byte* file);
		public static byte SDL_SaveBMP(IntPtr surface, string file)
		{
			var fileUTF8 = EncodeAsUTF8(file);
			var result = INTERNAL_SDL_SaveBMP(surface, fileUTF8);

			SDL_free((IntPtr) fileUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceRLE(IntPtr surface, byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SurfaceHasRLE(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceColorKey(IntPtr surface, byte enabled, uint key);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SurfaceHasColorKey(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSurfaceColorKey(IntPtr surface, out uint key);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceAlphaMod(IntPtr surface, byte alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceBlendMode(IntPtr surface, uint blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSurfaceBlendMode(IntPtr surface, IntPtr blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetSurfaceClipRect(IntPtr surface, ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSurfaceClipRect(IntPtr surface, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FlipSurface(IntPtr surface, SDL_FlipMode flip);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_DuplicateSurface(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ScaleSurface(IntPtr surface, int width, int height, SDL_ScaleMode scaleMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurface(IntPtr surface, SDL_PixelFormat format);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurfaceAndColorspace(IntPtr surface, SDL_PixelFormat format, IntPtr palette, SDL_Colorspace colorspace, uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, uint src_properties, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, uint dst_properties, IntPtr dst, int dst_pitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch, byte linear);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PremultiplySurfaceAlpha(IntPtr surface, byte linear);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClearSurface(IntPtr surface, float r, float g, float b, float a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FillSurfaceRect(IntPtr dst, IntPtr rect, uint color); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FillSurfaceRects(IntPtr dst, SDL_Rect[] rects, int count, uint color);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurfaceUnchecked(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurfaceScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurfaceUncheckedScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, SDL_ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurfaceTiled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurfaceTiledWithScale(IntPtr src, IntPtr srcrect, float scale, SDL_ScaleMode scaleMode, IntPtr dst, IntPtr dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_BlitSurface9Grid(IntPtr src, IntPtr srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, IntPtr dst, IntPtr dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapSurfaceRGB(IntPtr surface, byte r, byte g, byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapSurfaceRGBA(IntPtr surface, byte r, byte g, byte b, byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadSurfacePixel(IntPtr surface, int x, int y, out byte r, out byte g, out byte b, out byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadSurfacePixelFloat(IntPtr surface, int x, int y, out float r, out float g, out float b, out float a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteSurfacePixel(IntPtr surface, int x, int y, byte r, byte g, byte b, byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WriteSurfacePixelFloat(IntPtr surface, int x, int y, float r, float g, float b, float a);

		// /usr/local/include/SDL3/SDL_camera.h

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetCameraDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetCameraDriver(int index);
		public static string SDL_GetCameraDriver(int index)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetCameraDriver(index));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetCurrentCameraDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetCurrentCameraDriver();
		public static string SDL_GetCurrentCameraDriver()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetCurrentCameraDriver());
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCameras(out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCameraSupportedFormats(uint devid, out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetCameraName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetCameraName(uint instance_id);
		public static string SDL_GetCameraName(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetCameraName(instance_id));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_CameraPosition SDL_GetCameraPosition(uint instance_id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenCamera(uint instance_id, ref SDL_CameraSpec spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCameraPermissionState(IntPtr camera);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetCameraID(IntPtr camera);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetCameraProperties(IntPtr camera);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetCameraFormat(IntPtr camera, out SDL_CameraSpec spec);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AcquireCameraFrame(IntPtr camera, out ulong timestampNS);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseCameraFrame(IntPtr camera, IntPtr frame);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseCamera(IntPtr camera);

		// /usr/local/include/SDL3/SDL_clipboard.h

		[DllImport(nativeLibName, EntryPoint = "SDL_SetClipboardText", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetClipboardText(byte* text);
		public static byte SDL_SetClipboardText(string text)
		{
			var textUTF8 = EncodeAsUTF8(text);
			var result = INTERNAL_SDL_SetClipboardText(textUTF8);

			SDL_free((IntPtr) textUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardText", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetClipboardText();
		public static string SDL_GetClipboardText()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetClipboardText(), shouldFree: true);
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasClipboardText();

		[DllImport(nativeLibName, EntryPoint = "SDL_SetPrimarySelectionText", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetPrimarySelectionText(byte* text);
		public static byte SDL_SetPrimarySelectionText(string text)
		{
			var textUTF8 = EncodeAsUTF8(text);
			var result = INTERNAL_SDL_SetPrimarySelectionText(textUTF8);

			SDL_free((IntPtr) textUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPrimarySelectionText", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetPrimarySelectionText();
		public static string SDL_GetPrimarySelectionText()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetPrimarySelectionText(), shouldFree: true);
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasPrimarySelectionText();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDL_ClipboardDataCallback(IntPtr userdata, byte* mime_type, IntPtr size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_ClipboardCleanupCallback(IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetClipboardData(SDL_ClipboardDataCallback callback, SDL_ClipboardCleanupCallback cleanup, IntPtr userdata, IntPtr mime_types, UIntPtr num_mime_types);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClearClipboardData();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardData", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetClipboardData(byte* mime_type, out UIntPtr size);
		public static IntPtr SDL_GetClipboardData(string mime_type, out UIntPtr size)
		{
			var mime_typeUTF8 = EncodeAsUTF8(mime_type);
			var result = INTERNAL_SDL_GetClipboardData(mime_typeUTF8, out size);

			SDL_free((IntPtr) mime_typeUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_HasClipboardData", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_HasClipboardData(byte* mime_type);
		public static byte SDL_HasClipboardData(string mime_type)
		{
			var mime_typeUTF8 = EncodeAsUTF8(mime_type);
			var result = INTERNAL_SDL_HasClipboardData(mime_typeUTF8);

			SDL_free((IntPtr) mime_typeUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetClipboardMimeTypes(out UIntPtr num_mime_types);

		// /usr/local/include/SDL3/SDL_cpuinfo.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumLogicalCPUCores();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCPUCacheLineSize();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasAltiVec();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasMMX();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasSSE();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasSSE2();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasSSE3();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasSSE41();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasSSE42();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasAVX();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasAVX2();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasAVX512F();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasARMSIMD();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasNEON();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasLSX();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasLASX();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSystemRAM();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern UIntPtr SDL_GetSIMDAlignment();

		// /usr/local/include/SDL3/SDL_video.h

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
		public delegate IntPtr SDL_EGLIntArrayCallback();

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetVideoDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetVideoDriver(int index);
		public static string SDL_GetVideoDriver(int index)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetVideoDriver(index));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetCurrentVideoDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetCurrentVideoDriver();
		public static string SDL_GetCurrentVideoDriver()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetCurrentVideoDriver());
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_SystemTheme SDL_GetSystemTheme();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDisplays(out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetPrimaryDisplay();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayProperties(uint displayID);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetDisplayName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetDisplayName(uint displayID);
		public static string SDL_GetDisplayName(uint displayID)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetDisplayName(displayID));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetDisplayBounds(uint displayID, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetDisplayUsableBounds(uint displayID, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetDisplayContentScale(uint displayID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetFullscreenDisplayModes(uint displayID, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refresh_rate, byte include_high_density_modes, out SDL_DisplayMode mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDesktopDisplayMode(uint displayID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCurrentDisplayMode(uint displayID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForPoint(ref SDL_Point point);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForRect(ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowPixelDensity(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowDisplayScale(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowFullscreenMode(IntPtr window, ref SDL_DisplayMode mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFullscreenMode(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowICCProfile(IntPtr window, out UIntPtr size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_PixelFormat SDL_GetWindowPixelFormat(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindows(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_CreateWindow", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_CreateWindow(byte* title, int w, int h, SDL_WindowFlags flags);
		public static IntPtr SDL_CreateWindow(string title, int w, int h, SDL_WindowFlags flags)
		{
			var titleUTF8 = EncodeAsUTF8(title);
			var result = INTERNAL_SDL_CreateWindow(titleUTF8, w, h, flags);

			SDL_free((IntPtr) titleUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreatePopupWindow(IntPtr parent, int offset_x, int offset_y, int w, int h, SDL_WindowFlags flags);

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
		public static extern SDL_WindowFlags SDL_GetWindowFlags(IntPtr window);

		[DllImport(nativeLibName, EntryPoint = "SDL_SetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetWindowTitle(IntPtr window, byte* title);
		public static byte SDL_SetWindowTitle(IntPtr window, string title)
		{
			var titleUTF8 = EncodeAsUTF8(title);
			var result = INTERNAL_SDL_SetWindowTitle(window, titleUTF8);

			SDL_free((IntPtr) titleUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetWindowTitle(IntPtr window);
		public static string SDL_GetWindowTitle(IntPtr window)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetWindowTitle(window));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowIcon(IntPtr window, IntPtr icon);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowPosition(IntPtr window, int x, int y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowPosition(IntPtr window, out int x, out int y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowSize(IntPtr window, int w, int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowSize(IntPtr window, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowSafeArea(IntPtr window, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowAspectRatio(IntPtr window, float min_aspect, float max_aspect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowAspectRatio(IntPtr window, out float min_aspect, out float max_aspect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowSizeInPixels(IntPtr window, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowMinimumSize(IntPtr window, int min_w, int min_h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowMinimumSize(IntPtr window, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowMaximumSize(IntPtr window, int max_w, int max_h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowMaximumSize(IntPtr window, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowBordered(IntPtr window, byte bordered);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowResizable(IntPtr window, byte resizable);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowAlwaysOnTop(IntPtr window, byte on_top);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShowWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HideWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RaiseWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_MaximizeWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_MinimizeWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RestoreWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowFullscreen(IntPtr window, byte fullscreen);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SyncWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WindowHasSurface(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowSurface(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowSurfaceVSync(IntPtr window, int vsync);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowSurfaceVSync(IntPtr window, out int vsync);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateWindowSurface(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateWindowSurfaceRects(IntPtr window, SDL_Rect[] rects, int numrects);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_DestroyWindowSurface(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowKeyboardGrab(IntPtr window, byte grabbed);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowMouseGrab(IntPtr window, byte grabbed);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowKeyboardGrab(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowMouseGrab(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGrabbedWindow();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowMouseRect(IntPtr window, ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowMouseRect(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowOpacity(IntPtr window, float opacity);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowOpacity(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowParent(IntPtr window, IntPtr parent);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowModal(IntPtr window, byte modal);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowFocusable(IntPtr window, byte focusable);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShowWindowSystemMenu(IntPtr window, int x, int y);

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
		public delegate SDL_HitTestResult SDL_HitTest(IntPtr win, SDL_Point* area, IntPtr data);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowHitTest(IntPtr window, SDL_HitTest callback, IntPtr callback_data);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowShape(IntPtr window, IntPtr shape);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FlashWindow(IntPtr window, SDL_FlashOperation operation);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ScreenSaverEnabled();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_EnableScreenSaver();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_DisableScreenSaver();

		[DllImport(nativeLibName, EntryPoint = "SDL_GL_LoadLibrary", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GL_LoadLibrary(byte* path);
		public static byte SDL_GL_LoadLibrary(string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_GL_LoadLibrary(pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GL_GetProcAddress(byte* proc);
		public static IntPtr SDL_GL_GetProcAddress(string proc)
		{
			var procUTF8 = EncodeAsUTF8(proc);
			var result = INTERNAL_SDL_GL_GetProcAddress(procUTF8);

			SDL_free((IntPtr) procUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_EGL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_EGL_GetProcAddress(byte* proc);
		public static IntPtr SDL_EGL_GetProcAddress(string proc)
		{
			var procUTF8 = EncodeAsUTF8(proc);
			var result = INTERNAL_SDL_EGL_GetProcAddress(procUTF8);

			SDL_free((IntPtr) procUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_UnloadLibrary();

		[DllImport(nativeLibName, EntryPoint = "SDL_GL_ExtensionSupported", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GL_ExtensionSupported(byte* extension);
		public static byte SDL_GL_ExtensionSupported(string extension)
		{
			var extensionUTF8 = EncodeAsUTF8(extension);
			var result = INTERNAL_SDL_GL_ExtensionSupported(extensionUTF8);

			SDL_free((IntPtr) extensionUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_ResetAttributes();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_SetAttribute(SDL_GLattr attr, int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_GetAttribute(SDL_GLattr attr, out int value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_CreateContext(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_MakeCurrent(IntPtr window, IntPtr context);

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
		public static extern void SDL_EGL_SetAttributeCallbacks(SDL_EGLAttribArrayCallback platformAttribCallback, SDL_EGLIntArrayCallback surfaceAttribCallback, SDL_EGLIntArrayCallback contextAttribCallback);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_SetSwapInterval(int interval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_GetSwapInterval(out int interval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_SwapWindow(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GL_DestroyContext(IntPtr context);

		// /usr/local/include/SDL3/SDL_dialog.h

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DialogFileFilter
		{
			public byte* name;
			public byte* pattern;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_DialogFileCallback(IntPtr userdata, IntPtr filelist, int filter);

		[DllImport(nativeLibName, EntryPoint = "SDL_ShowOpenFileDialog", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_ShowOpenFileDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL_DialogFileFilter[] filters, int nfilters, byte* default_location, byte allow_many);
		public static void SDL_ShowOpenFileDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL_DialogFileFilter[] filters, int nfilters, string default_location, byte allow_many)
		{
			var default_locationUTF8 = EncodeAsUTF8(default_location);
			INTERNAL_SDL_ShowOpenFileDialog(callback, userdata, window, filters, nfilters, default_locationUTF8, allow_many);

			SDL_free((IntPtr) default_locationUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ShowSaveFileDialog", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_ShowSaveFileDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL_DialogFileFilter[] filters, int nfilters, byte* default_location);
		public static void SDL_ShowSaveFileDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL_DialogFileFilter[] filters, int nfilters, string default_location)
		{
			var default_locationUTF8 = EncodeAsUTF8(default_location);
			INTERNAL_SDL_ShowSaveFileDialog(callback, userdata, window, filters, nfilters, default_locationUTF8);

			SDL_free((IntPtr) default_locationUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ShowOpenFolderDialog", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_ShowOpenFolderDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, byte* default_location, byte allow_many);
		public static void SDL_ShowOpenFolderDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, string default_location, byte allow_many)
		{
			var default_locationUTF8 = EncodeAsUTF8(default_location);
			INTERNAL_SDL_ShowOpenFolderDialog(callback, userdata, window, default_locationUTF8, allow_many);

			SDL_free((IntPtr) default_locationUTF8);
		}

		// /usr/local/include/SDL3/SDL_guid.h

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GUID
		{
			public fixed byte data[16];
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GUIDToString", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_GUIDToString(SDL_GUID guid, byte* pszGUID, int cbGUID);
		public static void SDL_GUIDToString(SDL_GUID guid, string pszGUID, int cbGUID)
		{
			var pszGUIDUTF8 = EncodeAsUTF8(pszGUID);
			INTERNAL_SDL_GUIDToString(guid, pszGUIDUTF8, cbGUID);

			SDL_free((IntPtr) pszGUIDUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_StringToGUID", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_GUID INTERNAL_SDL_StringToGUID(byte* pchGUID);
		public static SDL_GUID SDL_StringToGUID(string pchGUID)
		{
			var pchGUIDUTF8 = EncodeAsUTF8(pchGUID);
			var result = INTERNAL_SDL_StringToGUID(pchGUIDUTF8);

			SDL_free((IntPtr) pchGUIDUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_power.h

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
		public static extern SDL_PowerState SDL_GetPowerInfo(out int seconds, out int percent);

		// /usr/local/include/SDL3/SDL_sensor.h

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
		public static extern IntPtr SDL_GetSensors(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetSensorNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetSensorNameForID(uint instance_id);
		public static string SDL_GetSensorNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetSensorNameForID(instance_id));
		}

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetSensorName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetSensorName(IntPtr sensor);
		public static string SDL_GetSensorName(IntPtr sensor)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetSensorName(sensor));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_SensorType SDL_GetSensorType(IntPtr sensor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSensorNonPortableType(IntPtr sensor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSensorID(IntPtr sensor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetSensorData(IntPtr sensor, float* data, int num_values);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseSensor(IntPtr sensor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateSensors();

		// /usr/local/include/SDL3/SDL_joystick.h

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
			SDL_JOYSTICK_TYPE_COUNT = 10,
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
		public static extern byte SDL_HasJoystick();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetJoysticks(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetJoystickNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetJoystickNameForID(uint instance_id);
		public static string SDL_GetJoystickNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetJoystickNameForID(instance_id));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetJoystickPathForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetJoystickPathForID(uint instance_id);
		public static string SDL_GetJoystickPathForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetJoystickPathForID(instance_id));
		}

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
			public fixed ushort padding[3];
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
			public fixed ushort padding2[2];
			public uint button_mask;
			public uint axis_mask;
			public byte* name;
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
		public static extern uint SDL_AttachVirtualJoystick(ref SDL_VirtualJoystickDesc desc);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_DetachVirtualJoystick(uint instance_id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_IsJoystickVirtual(uint instance_id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickVirtualButton(IntPtr joystick, int button, byte down);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, byte value);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, byte down, float x, float y, float pressure);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SendJoystickVirtualSensorData(IntPtr joystick, SDL_SensorType type, ulong sensor_timestamp, float* data, int num_values);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetJoystickProperties(IntPtr joystick);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetJoystickName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetJoystickName(IntPtr joystick);
		public static string SDL_GetJoystickName(IntPtr joystick)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetJoystickName(joystick));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetJoystickPath", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetJoystickPath(IntPtr joystick);
		public static string SDL_GetJoystickPath(IntPtr joystick)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetJoystickPath(joystick));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetJoystickPlayerIndex(IntPtr joystick);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickPlayerIndex(IntPtr joystick, int player_index);

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetJoystickSerial", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetJoystickSerial(IntPtr joystick);
		public static string SDL_GetJoystickSerial(IntPtr joystick)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetJoystickSerial(joystick));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickType SDL_GetJoystickType(IntPtr joystick);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetJoystickGUIDInfo(SDL_GUID guid, out ushort vendor, out ushort product, out ushort version, out ushort crc16);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickConnected(IntPtr joystick);

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
		public static extern void SDL_SetJoystickEventsEnabled(byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickEventsEnabled();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateJoysticks();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GetJoystickAxis(IntPtr joystick, int axis);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetJoystickHat(IntPtr joystick, int hat);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetJoystickButton(IntPtr joystick, int button);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RumbleJoystick(IntPtr joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RumbleJoystickTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseJoystick(IntPtr joystick);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, out int percent);

		// /usr/local/include/SDL3/SDL_gamepad.h

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
			SDL_GAMEPAD_TYPE_COUNT = 11,
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
			SDL_GAMEPAD_BUTTON_COUNT = 26,
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
			SDL_GAMEPAD_AXIS_COUNT = 6,
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

			SDL_free((IntPtr) mappingUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AddGamepadMappingsFromIO(IntPtr src, byte closeio);

		[DllImport(nativeLibName, EntryPoint = "SDL_AddGamepadMappingsFromFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_AddGamepadMappingsFromFile(byte* file);
		public static int SDL_AddGamepadMappingsFromFile(string file)
		{
			var fileUTF8 = EncodeAsUTF8(file);
			var result = INTERNAL_SDL_AddGamepadMappingsFromFile(fileUTF8);

			SDL_free((IntPtr) fileUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReloadGamepadMappings();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadMappings(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadMappingForGUID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMappingForGUID(SDL_GUID guid);
		public static string SDL_GetGamepadMappingForGUID(SDL_GUID guid)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadMappingForGUID(guid), shouldFree: true);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadMapping", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMapping(IntPtr gamepad);
		public static string SDL_GetGamepadMapping(IntPtr gamepad)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadMapping(gamepad), shouldFree: true);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetGamepadMapping", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetGamepadMapping(uint instance_id, byte* mapping);
		public static byte SDL_SetGamepadMapping(uint instance_id, string mapping)
		{
			var mappingUTF8 = EncodeAsUTF8(mapping);
			var result = INTERNAL_SDL_SetGamepadMapping(instance_id, mappingUTF8);

			SDL_free((IntPtr) mappingUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasGamepad();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepads(out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_IsGamepad(uint instance_id);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadNameForID(uint instance_id);
		public static string SDL_GetGamepadNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadNameForID(instance_id));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadPathForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadPathForID(uint instance_id);
		public static string SDL_GetGamepadPathForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadPathForID(instance_id));
		}

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadMappingForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMappingForID(uint instance_id);
		public static string SDL_GetGamepadMappingForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadMappingForID(instance_id), shouldFree: true);
		}

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadName(IntPtr gamepad);
		public static string SDL_GetGamepadName(IntPtr gamepad)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadName(gamepad));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadPath", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadPath(IntPtr gamepad);
		public static string SDL_GetGamepadPath(IntPtr gamepad)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadPath(gamepad));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GamepadType SDL_GetGamepadType(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GamepadType SDL_GetRealGamepadType(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetGamepadPlayerIndex(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetGamepadPlayerIndex(IntPtr gamepad, int player_index);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadVendor(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProduct(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProductVersion(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadSerial", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadSerial(IntPtr gamepad);
		public static string SDL_GetGamepadSerial(IntPtr gamepad)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadSerial(gamepad));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, out int percent);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadConnected(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGamepadEventsEnabled(byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadEventsEnabled();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadBindings(IntPtr gamepad, out int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateGamepads();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadTypeFromString", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_GamepadType INTERNAL_SDL_GetGamepadTypeFromString(byte* str);
		public static SDL_GamepadType SDL_GetGamepadTypeFromString(string str)
		{
			var strUTF8 = EncodeAsUTF8(str);
			var result = INTERNAL_SDL_GetGamepadTypeFromString(strUTF8);

			SDL_free((IntPtr) strUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadStringForType", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForType(SDL_GamepadType type);
		public static string SDL_GetGamepadStringForType(SDL_GamepadType type)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadStringForType(type));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadAxisFromString", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_GamepadAxis INTERNAL_SDL_GetGamepadAxisFromString(byte* str);
		public static SDL_GamepadAxis SDL_GetGamepadAxisFromString(string str)
		{
			var strUTF8 = EncodeAsUTF8(str);
			var result = INTERNAL_SDL_GetGamepadAxisFromString(strUTF8);

			SDL_free((IntPtr) strUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadStringForAxis", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);
		public static string SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadStringForAxis(axis));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadHasAxis(IntPtr gamepad, SDL_GamepadAxis axis);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GetGamepadAxis(IntPtr gamepad, SDL_GamepadAxis axis);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadButtonFromString", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_GamepadButton INTERNAL_SDL_GetGamepadButtonFromString(byte* str);
		public static SDL_GamepadButton SDL_GetGamepadButtonFromString(string str)
		{
			var strUTF8 = EncodeAsUTF8(str);
			var result = INTERNAL_SDL_GetGamepadButtonFromString(strUTF8);

			SDL_free((IntPtr) strUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadStringForButton", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForButton(SDL_GamepadButton button);
		public static string SDL_GetGamepadStringForButton(SDL_GamepadButton button)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadStringForButton(button));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadHasButton(IntPtr gamepad, SDL_GamepadButton button);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetGamepadButton(IntPtr gamepad, SDL_GamepadButton button);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GamepadButtonLabel SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, SDL_GamepadButton button);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGamepadTouchpads(IntPtr gamepad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out byte down, out float x, out float y, out float pressure);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadHasSensor(IntPtr gamepad, SDL_SensorType type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetGamepadSensorEnabled(IntPtr gamepad, SDL_SensorType type, byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GamepadSensorEnabled(IntPtr gamepad, SDL_SensorType type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SDL_SensorType type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetGamepadSensorData(IntPtr gamepad, SDL_SensorType type, float* data, int num_values);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RumbleGamepad(IntPtr gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseGamepad(IntPtr gamepad);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForButton", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL_GamepadButton button);
		public static string SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL_GamepadButton button)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad, button));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForAxis", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL_GamepadAxis axis);
		public static string SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL_GamepadAxis axis)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis));
		}

		// /usr/local/include/SDL3/SDL_scancode.h

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
			SDL_SCANCODE_COUNT = 512,
		}

		// /usr/local/include/SDL3/SDL_keycode.h

		public enum SDL_Keycode : uint
		{
			SDLK_SCANCODE_MASK = 0x4000_0000,
			SDLK_UNKNOWN = 0x00000000u,
			SDLK_RETURN = 0x0000000du,
			SDLK_ESCAPE = 0x0000001bu,
			SDLK_BACKSPACE = 0x00000008u,
			SDLK_TAB = 0x00000009u,
			SDLK_SPACE = 0x00000020u,
			SDLK_EXCLAIM = 0x00000021u,
			SDLK_DBLAPOSTROPHE = 0x00000022u,
			SDLK_HASH = 0x00000023u,
			SDLK_DOLLAR = 0x00000024u,
			SDLK_PERCENT = 0x00000025u,
			SDLK_AMPERSAND = 0x00000026u,
			SDLK_APOSTROPHE = 0x00000027u,
			SDLK_LEFTPAREN = 0x00000028u,
			SDLK_RIGHTPAREN = 0x00000029u,
			SDLK_ASTERISK = 0x0000002au,
			SDLK_PLUS = 0x0000002bu,
			SDLK_COMMA = 0x0000002cu,
			SDLK_MINUS = 0x0000002du,
			SDLK_PERIOD = 0x0000002eu,
			SDLK_SLASH = 0x0000002fu,
			SDLK_0 = 0x00000030u,
			SDLK_1 = 0x00000031u,
			SDLK_2 = 0x00000032u,
			SDLK_3 = 0x00000033u,
			SDLK_4 = 0x00000034u,
			SDLK_5 = 0x00000035u,
			SDLK_6 = 0x00000036u,
			SDLK_7 = 0x00000037u,
			SDLK_8 = 0x00000038u,
			SDLK_9 = 0x00000039u,
			SDLK_COLON = 0x0000003au,
			SDLK_SEMICOLON = 0x0000003bu,
			SDLK_LESS = 0x0000003cu,
			SDLK_EQUALS = 0x0000003du,
			SDLK_GREATER = 0x0000003eu,
			SDLK_QUESTION = 0x0000003fu,
			SDLK_AT = 0x00000040u,
			SDLK_LEFTBRACKET = 0x0000005bu,
			SDLK_BACKSLASH = 0x0000005cu,
			SDLK_RIGHTBRACKET = 0x0000005du,
			SDLK_CARET = 0x0000005eu,
			SDLK_UNDERSCORE = 0x0000005fu,
			SDLK_GRAVE = 0x00000060u,
			SDLK_A = 0x00000061u,
			SDLK_B = 0x00000062u,
			SDLK_C = 0x00000063u,
			SDLK_D = 0x00000064u,
			SDLK_E = 0x00000065u,
			SDLK_F = 0x00000066u,
			SDLK_G = 0x00000067u,
			SDLK_H = 0x00000068u,
			SDLK_I = 0x00000069u,
			SDLK_J = 0x0000006au,
			SDLK_K = 0x0000006bu,
			SDLK_L = 0x0000006cu,
			SDLK_M = 0x0000006du,
			SDLK_N = 0x0000006eu,
			SDLK_O = 0x0000006fu,
			SDLK_P = 0x00000070u,
			SDLK_Q = 0x00000071u,
			SDLK_R = 0x00000072u,
			SDLK_S = 0x00000073u,
			SDLK_T = 0x00000074u,
			SDLK_U = 0x00000075u,
			SDLK_V = 0x00000076u,
			SDLK_W = 0x00000077u,
			SDLK_X = 0x00000078u,
			SDLK_Y = 0x00000079u,
			SDLK_Z = 0x0000007au,
			SDLK_LEFTBRACE = 0x0000007bu,
			SDLK_PIPE = 0x0000007cu,
			SDLK_RIGHTBRACE = 0x0000007du,
			SDLK_TILDE = 0x0000007eu,
			SDLK_DELETE = 0x0000007fu,
			SDLK_PLUSMINUS = 0x000000b1u,
			SDLK_CAPSLOCK = 0x40000039u,
			SDLK_F1 = 0x4000003au,
			SDLK_F2 = 0x4000003bu,
			SDLK_F3 = 0x4000003cu,
			SDLK_F4 = 0x4000003du,
			SDLK_F5 = 0x4000003eu,
			SDLK_F6 = 0x4000003fu,
			SDLK_F7 = 0x40000040u,
			SDLK_F8 = 0x40000041u,
			SDLK_F9 = 0x40000042u,
			SDLK_F10 = 0x40000043u,
			SDLK_F11 = 0x40000044u,
			SDLK_F12 = 0x40000045u,
			SDLK_PRINTSCREEN = 0x40000046u,
			SDLK_SCROLLLOCK = 0x40000047u,
			SDLK_PAUSE = 0x40000048u,
			SDLK_INSERT = 0x40000049u,
			SDLK_HOME = 0x4000004au,
			SDLK_PAGEUP = 0x4000004bu,
			SDLK_END = 0x4000004du,
			SDLK_PAGEDOWN = 0x4000004eu,
			SDLK_RIGHT = 0x4000004fu,
			SDLK_LEFT = 0x40000050u,
			SDLK_DOWN = 0x40000051u,
			SDLK_UP = 0x40000052u,
			SDLK_NUMLOCKCLEAR = 0x40000053u,
			SDLK_KP_DIVIDE = 0x40000054u,
			SDLK_KP_MULTIPLY = 0x40000055u,
			SDLK_KP_MINUS = 0x40000056u,
			SDLK_KP_PLUS = 0x40000057u,
			SDLK_KP_ENTER = 0x40000058u,
			SDLK_KP_1 = 0x40000059u,
			SDLK_KP_2 = 0x4000005au,
			SDLK_KP_3 = 0x4000005bu,
			SDLK_KP_4 = 0x4000005cu,
			SDLK_KP_5 = 0x4000005du,
			SDLK_KP_6 = 0x4000005eu,
			SDLK_KP_7 = 0x4000005fu,
			SDLK_KP_8 = 0x40000060u,
			SDLK_KP_9 = 0x40000061u,
			SDLK_KP_0 = 0x40000062u,
			SDLK_KP_PERIOD = 0x40000063u,
			SDLK_APPLICATION = 0x40000065u,
			SDLK_POWER = 0x40000066u,
			SDLK_KP_EQUALS = 0x40000067u,
			SDLK_F13 = 0x40000068u,
			SDLK_F14 = 0x40000069u,
			SDLK_F15 = 0x4000006au,
			SDLK_F16 = 0x4000006bu,
			SDLK_F17 = 0x4000006cu,
			SDLK_F18 = 0x4000006du,
			SDLK_F19 = 0x4000006eu,
			SDLK_F20 = 0x4000006fu,
			SDLK_F21 = 0x40000070u,
			SDLK_F22 = 0x40000071u,
			SDLK_F23 = 0x40000072u,
			SDLK_F24 = 0x40000073u,
			SDLK_EXECUTE = 0x40000074u,
			SDLK_HELP = 0x40000075u,
			SDLK_MENU = 0x40000076u,
			SDLK_SELECT = 0x40000077u,
			SDLK_STOP = 0x40000078u,
			SDLK_AGAIN = 0x40000079u,
			SDLK_UNDO = 0x4000007au,
			SDLK_CUT = 0x4000007bu,
			SDLK_COPY = 0x4000007cu,
			SDLK_PASTE = 0x4000007du,
			SDLK_FIND = 0x4000007eu,
			SDLK_MUTE = 0x4000007fu,
			SDLK_VOLUMEUP = 0x40000080u,
			SDLK_VOLUMEDOWN = 0x40000081u,
			SDLK_KP_COMMA = 0x40000085u,
			SDLK_KP_EQUALSAS400 = 0x40000086u,
			SDLK_ALTERASE = 0x40000099u,
			SDLK_SYSREQ = 0x4000009au,
			SDLK_CANCEL = 0x4000009bu,
			SDLK_CLEAR = 0x4000009cu,
			SDLK_PRIOR = 0x4000009du,
			SDLK_RETURN2 = 0x4000009eu,
			SDLK_SEPARATOR = 0x4000009fu,
			SDLK_OUT = 0x400000a0u,
			SDLK_OPER = 0x400000a1u,
			SDLK_CLEARAGAIN = 0x400000a2u,
			SDLK_CRSEL = 0x400000a3u,
			SDLK_EXSEL = 0x400000a4u,
			SDLK_KP_00 = 0x400000b0u,
			SDLK_KP_000 = 0x400000b1u,
			SDLK_THOUSANDSSEPARATOR = 0x400000b2u,
			SDLK_DECIMALSEPARATOR = 0x400000b3u,
			SDLK_CURRENCYUNIT = 0x400000b4u,
			SDLK_CURRENCYSUBUNIT = 0x400000b5u,
			SDLK_KP_LEFTPAREN = 0x400000b6u,
			SDLK_KP_RIGHTPAREN = 0x400000b7u,
			SDLK_KP_LEFTBRACE = 0x400000b8u,
			SDLK_KP_RIGHTBRACE = 0x400000b9u,
			SDLK_KP_TAB = 0x400000bau,
			SDLK_KP_BACKSPACE = 0x400000bbu,
			SDLK_KP_A = 0x400000bcu,
			SDLK_KP_B = 0x400000bdu,
			SDLK_KP_C = 0x400000beu,
			SDLK_KP_D = 0x400000bfu,
			SDLK_KP_E = 0x400000c0u,
			SDLK_KP_F = 0x400000c1u,
			SDLK_KP_XOR = 0x400000c2u,
			SDLK_KP_POWER = 0x400000c3u,
			SDLK_KP_PERCENT = 0x400000c4u,
			SDLK_KP_LESS = 0x400000c5u,
			SDLK_KP_GREATER = 0x400000c6u,
			SDLK_KP_AMPERSAND = 0x400000c7u,
			SDLK_KP_DBLAMPERSAND = 0x400000c8u,
			SDLK_KP_VERTICALBAR = 0x400000c9u,
			SDLK_KP_DBLVERTICALBAR = 0x400000cau,
			SDLK_KP_COLON = 0x400000cbu,
			SDLK_KP_HASH = 0x400000ccu,
			SDLK_KP_SPACE = 0x400000cdu,
			SDLK_KP_AT = 0x400000ceu,
			SDLK_KP_EXCLAM = 0x400000cfu,
			SDLK_KP_MEMSTORE = 0x400000d0u,
			SDLK_KP_MEMRECALL = 0x400000d1u,
			SDLK_KP_MEMCLEAR = 0x400000d2u,
			SDLK_KP_MEMADD = 0x400000d3u,
			SDLK_KP_MEMSUBTRACT = 0x400000d4u,
			SDLK_KP_MEMMULTIPLY = 0x400000d5u,
			SDLK_KP_MEMDIVIDE = 0x400000d6u,
			SDLK_KP_PLUSMINUS = 0x400000d7u,
			SDLK_KP_CLEAR = 0x400000d8u,
			SDLK_KP_CLEARENTRY = 0x400000d9u,
			SDLK_KP_BINARY = 0x400000dau,
			SDLK_KP_OCTAL = 0x400000dbu,
			SDLK_KP_DECIMAL = 0x400000dcu,
			SDLK_KP_HEXADECIMAL = 0x400000ddu,
			SDLK_LCTRL = 0x400000e0u,
			SDLK_LSHIFT = 0x400000e1u,
			SDLK_LALT = 0x400000e2u,
			SDLK_LGUI = 0x400000e3u,
			SDLK_RCTRL = 0x400000e4u,
			SDLK_RSHIFT = 0x400000e5u,
			SDLK_RALT = 0x400000e6u,
			SDLK_RGUI = 0x400000e7u,
			SDLK_MODE = 0x40000101u,
			SDLK_SLEEP = 0x40000102u,
			SDLK_WAKE = 0x40000103u,
			SDLK_CHANNEL_INCREMENT = 0x40000104u,
			SDLK_CHANNEL_DECREMENT = 0x40000105u,
			SDLK_MEDIA_PLAY = 0x40000106u,
			SDLK_MEDIA_PAUSE = 0x40000107u,
			SDLK_MEDIA_RECORD = 0x40000108u,
			SDLK_MEDIA_FAST_FORWARD = 0x40000109u,
			SDLK_MEDIA_REWIND = 0x4000010au,
			SDLK_MEDIA_NEXT_TRACK = 0x4000010bu,
			SDLK_MEDIA_PREVIOUS_TRACK = 0x4000010cu,
			SDLK_MEDIA_STOP = 0x4000010du,
			SDLK_MEDIA_EJECT = 0x4000010eu,
			SDLK_MEDIA_PLAY_PAUSE = 0x4000010fu,
			SDLK_MEDIA_SELECT = 0x40000110u,
			SDLK_AC_NEW = 0x40000111u,
			SDLK_AC_OPEN = 0x40000112u,
			SDLK_AC_CLOSE = 0x40000113u,
			SDLK_AC_EXIT = 0x40000114u,
			SDLK_AC_SAVE = 0x40000115u,
			SDLK_AC_PRINT = 0x40000116u,
			SDLK_AC_PROPERTIES = 0x40000117u,
			SDLK_AC_SEARCH = 0x40000118u,
			SDLK_AC_HOME = 0x40000119u,
			SDLK_AC_BACK = 0x4000011au,
			SDLK_AC_FORWARD = 0x4000011bu,
			SDLK_AC_STOP = 0x4000011cu,
			SDLK_AC_REFRESH = 0x4000011du,
			SDLK_AC_BOOKMARKS = 0x4000011eu,
			SDLK_SOFTLEFT = 0x4000011fu,
			SDLK_SOFTRIGHT = 0x40000120u,
			SDLK_CALL = 0x40000121u,
			SDLK_ENDCALL = 0x40000122u,
		}

		[Flags]
		public enum SDL_Keymod : ushort
		{
			SDL_KMOD_NONE = 0x0000,
			SDL_KMOD_LSHIFT = 0x0001,
			SDL_KMOD_RSHIFT = 0x0002,
			SDL_KMOD_LCTRL = 0x0040,
			SDL_KMOD_RCTRL = 0x0080,
			SDL_KMOD_LALT = 0x0100,
			SDL_KMOD_RALT = 0x0200,
			SDL_KMOD_LGUI = 0x0400,
			SDL_KMOD_RGUI = 0x0800,
			SDL_KMOD_NUM = 0x1000,
			SDL_KMOD_CAPS = 0x2000,
			SDL_KMOD_MODE = 0x4000,
			SDL_KMOD_SCROLL = 0x8000,
			SDL_KMOD_CTRL = SDL_KMOD_LCTRL | SDL_KMOD_RCTRL,
			SDL_KMOD_SHIFT = SDL_KMOD_LSHIFT | SDL_KMOD_RSHIFT,
			SDL_KMOD_ALT = SDL_KMOD_RALT | SDL_KMOD_LALT,
			SDL_KMOD_GUI = SDL_KMOD_RGUI | SDL_KMOD_LGUI,
		}

		// /usr/local/include/SDL3/SDL_keyboard.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasKeyboard();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboards(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetKeyboardNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetKeyboardNameForID(uint instance_id);
		public static string SDL_GetKeyboardNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetKeyboardNameForID(instance_id));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardFocus();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardState(out int numkeys);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetKeyboard();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetModState();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetModState(ushort modstate);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetKeyFromScancode(SDL_Scancode scancode, ushort modstate, byte key_event);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_Scancode SDL_GetScancodeFromKey(uint key, IntPtr modstate);

		[DllImport(nativeLibName, EntryPoint = "SDL_SetScancodeName", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetScancodeName(SDL_Scancode scancode, byte* name);
		public static byte SDL_SetScancodeName(SDL_Scancode scancode, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_SetScancodeName(scancode, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetScancodeName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetScancodeName(SDL_Scancode scancode);
		public static string SDL_GetScancodeName(SDL_Scancode scancode)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetScancodeName(scancode));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetScancodeFromName", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL_Scancode INTERNAL_SDL_GetScancodeFromName(byte* name);
		public static SDL_Scancode SDL_GetScancodeFromName(string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetScancodeFromName(nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetKeyName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetKeyName(uint key);
		public static string SDL_GetKeyName(uint key)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetKeyName(key));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetKeyFromName", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint INTERNAL_SDL_GetKeyFromName(byte* name);
		public static uint SDL_GetKeyFromName(string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetKeyFromName(nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StartTextInput(IntPtr window);

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
		public static extern byte SDL_StartTextInputWithProperties(IntPtr window, uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_TextInputActive(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StopTextInput(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClearComposition(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextInputArea(IntPtr window, ref SDL_Rect rect, int cursor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextInputArea(IntPtr window, out SDL_Rect rect, out int cursor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasScreenKeyboardSupport();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ScreenKeyboardShown(IntPtr window);

		// /usr/local/include/SDL3/SDL_mouse.h

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
			SDL_SYSTEM_CURSOR_COUNT = 20,
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
		public static extern byte SDL_HasMouse();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMice(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetMouseNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetMouseNameForID(uint instance_id);
		public static string SDL_GetMouseNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetMouseNameForID(instance_id));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMouseFocus();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_MouseButtonFlags SDL_GetMouseState(out float x, out float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WarpMouseInWindow(IntPtr window, float x, float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WarpMouseGlobal(float x, float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetWindowRelativeMouseMode(IntPtr window, byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetWindowRelativeMouseMode(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CaptureMouse(byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateCursor(IntPtr data, IntPtr mask, int w, int h, int hot_x, int hot_y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSystemCursor(SDL_SystemCursor id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetCursor(IntPtr cursor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCursor();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDefaultCursor();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyCursor(IntPtr cursor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShowCursor();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HideCursor();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CursorVisible();

		// /usr/local/include/SDL3/SDL_pen.h

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
			SDL_PEN_AXIS_COUNT = 7,
		}

		// /usr/local/include/SDL3/SDL_touch.h

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
		public static extern IntPtr SDL_GetTouchDevices(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetTouchDeviceName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetTouchDeviceName(ulong touchID);
		public static string SDL_GetTouchDeviceName(ulong touchID)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetTouchDeviceName(touchID));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTouchFingers(ulong touchID, out int count);

		// /usr/local/include/SDL3/SDL_events.h

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
			public byte down;
			public byte repeat;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_TextEditingEvent
		{
			public SDL_EventType type;
			public uint reserved;
			public ulong timestamp;
			public uint windowID;
			public byte* text;
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
			public byte** candidates;
			public int num_candidates;
			public int selected_candidate;
			public byte horizontal;
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
			public byte* text;
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
			public SDL_MouseButtonFlags state;
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
			public byte down;
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
			public byte down;
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
			public byte down;
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
			public fixed float data[3];
			public ulong sensor_timestamp;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_AudioDeviceEvent
		{
			public SDL_EventType type;
			public uint reserved;
			public ulong timestamp;
			public uint which;
			public byte recording;
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
			public SDL_PenInputFlags pen_state;
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
			public SDL_PenInputFlags pen_state;
			public float x;
			public float y;
			public byte eraser;
			public byte down;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_PenButtonEvent
		{
			public SDL_EventType type;
			public uint reserved;
			public ulong timestamp;
			public uint windowID;
			public uint which;
			public SDL_PenInputFlags pen_state;
			public float x;
			public float y;
			public byte button;
			public byte down;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_PenAxisEvent
		{
			public SDL_EventType type;
			public uint reserved;
			public ulong timestamp;
			public uint windowID;
			public uint which;
			public SDL_PenInputFlags pen_state;
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
			public byte* source;
			public byte* data;
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
			public fixed float data[6];
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
			public fixed byte padding[128];
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
		public static extern int SDL_PeepEvents(SDL_Event[] events, int numevents, SDL_EventAction action, uint minType, uint maxType);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasEvent(uint type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HasEvents(uint minType, uint maxType);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvent(uint type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvents(uint minType, uint maxType);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PollEvent(out SDL_Event @event);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitEvent(out SDL_Event @event);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitEventTimeout(out SDL_Event @event, int timeoutMS);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PushEvent(ref SDL_Event @event);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool SDL_EventFilter(IntPtr userdata, SDL_Event* evt);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventFilter(SDL_EventFilter filter, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetEventFilter(out SDL_EventFilter filter, out IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_AddEventWatch(SDL_EventFilter filter, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RemoveEventWatch(SDL_EventFilter filter, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FilterEvents(SDL_EventFilter filter, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventEnabled(uint type, byte enabled);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_EventEnabled(uint type);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_RegisterEvents(int numevents);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFromEvent(ref SDL_Event @event);

		// /usr/local/include/SDL3/SDL_filesystem.h

		[DllImport(nativeLibName, EntryPoint = "SDL_GetBasePath", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetBasePath();
		public static string SDL_GetBasePath()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetBasePath());
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPrefPath", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetPrefPath(byte* org, byte* app);
		public static string SDL_GetPrefPath(string org, string app)
		{
			var orgUTF8 = EncodeAsUTF8(org);
			var appUTF8 = EncodeAsUTF8(app);
			var result = DecodeFromUTF8(INTERNAL_SDL_GetPrefPath(orgUTF8, appUTF8), shouldFree: true);

			SDL_free((IntPtr) orgUTF8);
			SDL_free((IntPtr) appUTF8);
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
			SDL_FOLDER_COUNT = 11,
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetUserFolder", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetUserFolder(SDL_Folder folder);
		public static string SDL_GetUserFolder(SDL_Folder folder)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetUserFolder(folder));
		}

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
		private static extern byte INTERNAL_SDL_CreateDirectory(byte* path);
		public static byte SDL_CreateDirectory(string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_CreateDirectory(pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		public enum SDL_EnumerationResult
		{
			SDL_ENUM_CONTINUE = 0,
			SDL_ENUM_SUCCESS = 1,
			SDL_ENUM_FAILURE = 2,
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL_EnumerationResult SDL_EnumerateDirectoryCallback(IntPtr userdata, byte* dirname, byte* fname);

		[DllImport(nativeLibName, EntryPoint = "SDL_EnumerateDirectory", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_EnumerateDirectory(byte* path, SDL_EnumerateDirectoryCallback callback, IntPtr userdata);
		public static byte SDL_EnumerateDirectory(string path, SDL_EnumerateDirectoryCallback callback, IntPtr userdata)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_EnumerateDirectory(pathUTF8, callback, userdata);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_RemovePath", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_RemovePath(byte* path);
		public static byte SDL_RemovePath(string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_RemovePath(pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_RenamePath", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_RenamePath(byte* oldpath, byte* newpath);
		public static byte SDL_RenamePath(string oldpath, string newpath)
		{
			var oldpathUTF8 = EncodeAsUTF8(oldpath);
			var newpathUTF8 = EncodeAsUTF8(newpath);
			var result = INTERNAL_SDL_RenamePath(oldpathUTF8, newpathUTF8);

			SDL_free((IntPtr) oldpathUTF8);
			SDL_free((IntPtr) newpathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_CopyFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_CopyFile(byte* oldpath, byte* newpath);
		public static byte SDL_CopyFile(string oldpath, string newpath)
		{
			var oldpathUTF8 = EncodeAsUTF8(oldpath);
			var newpathUTF8 = EncodeAsUTF8(newpath);
			var result = INTERNAL_SDL_CopyFile(oldpathUTF8, newpathUTF8);

			SDL_free((IntPtr) oldpathUTF8);
			SDL_free((IntPtr) newpathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPathInfo", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GetPathInfo(byte* path, out SDL_PathInfo info);
		public static byte SDL_GetPathInfo(string path, out SDL_PathInfo info)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_GetPathInfo(pathUTF8, out info);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GlobDirectory", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GlobDirectory(byte* path, byte* pattern, SDL_GlobFlags flags, out int count);
		public static IntPtr SDL_GlobDirectory(string path, string pattern, SDL_GlobFlags flags, out int count)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var patternUTF8 = EncodeAsUTF8(pattern);
			var result = INTERNAL_SDL_GlobDirectory(pathUTF8, patternUTF8, flags, out count);

			SDL_free((IntPtr) pathUTF8);
			SDL_free((IntPtr) patternUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_gpu.h

		public enum SDL_GPUPrimitiveType
		{
			SDL_GPU_PRIMITIVETYPE_TRIANGLELIST = 0,
			SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP = 1,
			SDL_GPU_PRIMITIVETYPE_LINELIST = 2,
			SDL_GPU_PRIMITIVETYPE_LINESTRIP = 3,
			SDL_GPU_PRIMITIVETYPE_POINTLIST = 4,
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
			SDL_GPU_STOREOP_RESOLVE = 2,
			SDL_GPU_STOREOP_RESOLVE_AND_STORE = 3,
		}

		public enum SDL_GPUIndexElementSize
		{
			SDL_GPU_INDEXELEMENTSIZE_16BIT = 0,
			SDL_GPU_INDEXELEMENTSIZE_32BIT = 1,
		}

		public enum SDL_GPUTextureFormat
		{
			SDL_GPU_TEXTUREFORMAT_INVALID = 0,
			SDL_GPU_TEXTUREFORMAT_A8_UNORM = 1,
			SDL_GPU_TEXTUREFORMAT_R8_UNORM = 2,
			SDL_GPU_TEXTUREFORMAT_R8G8_UNORM = 3,
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM = 4,
			SDL_GPU_TEXTUREFORMAT_R16_UNORM = 5,
			SDL_GPU_TEXTUREFORMAT_R16G16_UNORM = 6,
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM = 7,
			SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM = 8,
			SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM = 9,
			SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM = 10,
			SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM = 11,
			SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM = 12,
			SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM = 13,
			SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM = 14,
			SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM = 15,
			SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM = 16,
			SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM = 17,
			SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM = 18,
			SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT = 19,
			SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT = 20,
			SDL_GPU_TEXTUREFORMAT_R8_SNORM = 21,
			SDL_GPU_TEXTUREFORMAT_R8G8_SNORM = 22,
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM = 23,
			SDL_GPU_TEXTUREFORMAT_R16_SNORM = 24,
			SDL_GPU_TEXTUREFORMAT_R16G16_SNORM = 25,
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM = 26,
			SDL_GPU_TEXTUREFORMAT_R16_FLOAT = 27,
			SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT = 28,
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT = 29,
			SDL_GPU_TEXTUREFORMAT_R32_FLOAT = 30,
			SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT = 31,
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT = 32,
			SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT = 33,
			SDL_GPU_TEXTUREFORMAT_R8_UINT = 34,
			SDL_GPU_TEXTUREFORMAT_R8G8_UINT = 35,
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT = 36,
			SDL_GPU_TEXTUREFORMAT_R16_UINT = 37,
			SDL_GPU_TEXTUREFORMAT_R16G16_UINT = 38,
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT = 39,
			SDL_GPU_TEXTUREFORMAT_R32_UINT = 40,
			SDL_GPU_TEXTUREFORMAT_R32G32_UINT = 41,
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_UINT = 42,
			SDL_GPU_TEXTUREFORMAT_R8_INT = 43,
			SDL_GPU_TEXTUREFORMAT_R8G8_INT = 44,
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT = 45,
			SDL_GPU_TEXTUREFORMAT_R16_INT = 46,
			SDL_GPU_TEXTUREFORMAT_R16G16_INT = 47,
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT = 48,
			SDL_GPU_TEXTUREFORMAT_R32_INT = 49,
			SDL_GPU_TEXTUREFORMAT_R32G32_INT = 50,
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_INT = 51,
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB = 52,
			SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB = 53,
			SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB = 54,
			SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB = 55,
			SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB = 56,
			SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB = 57,
			SDL_GPU_TEXTUREFORMAT_D16_UNORM = 58,
			SDL_GPU_TEXTUREFORMAT_D24_UNORM = 59,
			SDL_GPU_TEXTUREFORMAT_D32_FLOAT = 60,
			SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT = 61,
			SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT = 62,
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
			SDL_GPU_TEXTURETYPE_CUBE_ARRAY = 4,
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
			SDL_GPU_VERTEXELEMENTFORMAT_INVALID = 0,
			SDL_GPU_VERTEXELEMENTFORMAT_INT = 1,
			SDL_GPU_VERTEXELEMENTFORMAT_INT2 = 2,
			SDL_GPU_VERTEXELEMENTFORMAT_INT3 = 3,
			SDL_GPU_VERTEXELEMENTFORMAT_INT4 = 4,
			SDL_GPU_VERTEXELEMENTFORMAT_UINT = 5,
			SDL_GPU_VERTEXELEMENTFORMAT_UINT2 = 6,
			SDL_GPU_VERTEXELEMENTFORMAT_UINT3 = 7,
			SDL_GPU_VERTEXELEMENTFORMAT_UINT4 = 8,
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT = 9,
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2 = 10,
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3 = 11,
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4 = 12,
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE2 = 13,
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE4 = 14,
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2 = 15,
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4 = 16,
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM = 17,
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM = 18,
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM = 19,
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM = 20,
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT2 = 21,
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT4 = 22,
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT2 = 23,
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT4 = 24,
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM = 25,
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM = 26,
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM = 27,
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM = 28,
			SDL_GPU_VERTEXELEMENTFORMAT_HALF2 = 29,
			SDL_GPU_VERTEXELEMENTFORMAT_HALF4 = 30,
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
			SDL_GPU_COMPAREOP_INVALID = 0,
			SDL_GPU_COMPAREOP_NEVER = 1,
			SDL_GPU_COMPAREOP_LESS = 2,
			SDL_GPU_COMPAREOP_EQUAL = 3,
			SDL_GPU_COMPAREOP_LESS_OR_EQUAL = 4,
			SDL_GPU_COMPAREOP_GREATER = 5,
			SDL_GPU_COMPAREOP_NOT_EQUAL = 6,
			SDL_GPU_COMPAREOP_GREATER_OR_EQUAL = 7,
			SDL_GPU_COMPAREOP_ALWAYS = 8,
		}

		public enum SDL_GPUStencilOp
		{
			SDL_GPU_STENCILOP_INVALID = 0,
			SDL_GPU_STENCILOP_KEEP = 1,
			SDL_GPU_STENCILOP_ZERO = 2,
			SDL_GPU_STENCILOP_REPLACE = 3,
			SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP = 4,
			SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP = 5,
			SDL_GPU_STENCILOP_INVERT = 6,
			SDL_GPU_STENCILOP_INCREMENT_AND_WRAP = 7,
			SDL_GPU_STENCILOP_DECREMENT_AND_WRAP = 8,
		}

		public enum SDL_GPUBlendOp
		{
			SDL_GPU_BLENDOP_INVALID = 0,
			SDL_GPU_BLENDOP_ADD = 1,
			SDL_GPU_BLENDOP_SUBTRACT = 2,
			SDL_GPU_BLENDOP_REVERSE_SUBTRACT = 3,
			SDL_GPU_BLENDOP_MIN = 4,
			SDL_GPU_BLENDOP_MAX = 5,
		}

		public enum SDL_GPUBlendFactor
		{
			SDL_GPU_BLENDFACTOR_INVALID = 0,
			SDL_GPU_BLENDFACTOR_ZERO = 1,
			SDL_GPU_BLENDFACTOR_ONE = 2,
			SDL_GPU_BLENDFACTOR_SRC_COLOR = 3,
			SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 4,
			SDL_GPU_BLENDFACTOR_DST_COLOR = 5,
			SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR = 6,
			SDL_GPU_BLENDFACTOR_SRC_ALPHA = 7,
			SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 8,
			SDL_GPU_BLENDFACTOR_DST_ALPHA = 9,
			SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 10,
			SDL_GPU_BLENDFACTOR_CONSTANT_COLOR = 11,
			SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR = 12,
			SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE = 13,
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
			public SDL_GPUCompareOp compare_op;
			public float min_lod;
			public float max_lod;
			public byte enable_anisotropy;
			public byte enable_compare;
			public byte padding1;
			public byte padding2;
			public uint props;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUVertexBufferDescription
		{
			public uint slot;
			public uint pitch;
			public SDL_GPUVertexInputRate input_rate;
			public uint instance_step_rate;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUVertexAttribute
		{
			public uint location;
			public uint buffer_slot;
			public SDL_GPUVertexElementFormat format;
			public uint offset;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUVertexInputState
		{
			public SDL_GPUVertexBufferDescription* vertex_buffer_descriptions;
			public uint num_vertex_buffers;
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
			public SDL_GPUBlendFactor src_color_blendfactor;
			public SDL_GPUBlendFactor dst_color_blendfactor;
			public SDL_GPUBlendOp color_blend_op;
			public SDL_GPUBlendFactor src_alpha_blendfactor;
			public SDL_GPUBlendFactor dst_alpha_blendfactor;
			public SDL_GPUBlendOp alpha_blend_op;
			public SDL_GPUColorComponentFlags color_write_mask;
			public byte enable_blend;
			public byte enable_color_write_mask;
			public byte padding2;
			public byte padding3;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUShaderCreateInfo
		{
			public UIntPtr code_size;
			public byte* code;
			public byte* entrypoint;
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
			public SDL_GPUTextureUsageFlags usage;
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
			public SDL_GPUBufferUsageFlags usage;
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
			public float depth_bias_constant_factor;
			public float depth_bias_clamp;
			public float depth_bias_slope_factor;
			public byte enable_depth_bias;
			public byte enable_depth_clip;
			public byte padding1;
			public byte padding2;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUMultisampleState
		{
			public SDL_GPUSampleCount sample_count;
			public uint sample_mask;
			public byte enable_mask;
			public byte padding1;
			public byte padding2;
			public byte padding3;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUDepthStencilState
		{
			public SDL_GPUCompareOp compare_op;
			public SDL_GPUStencilOpState back_stencil_state;
			public SDL_GPUStencilOpState front_stencil_state;
			public byte compare_mask;
			public byte write_mask;
			public byte enable_depth_test;
			public byte enable_depth_write;
			public byte enable_stencil_test;
			public byte padding1;
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
		public struct SDL_GPUGraphicsPipelineTargetInfo
		{
			public SDL_GPUColorTargetDescription* color_target_descriptions;
			public uint num_color_targets;
			public SDL_GPUTextureFormat depth_stencil_format;
			public byte has_depth_stencil_target;
			public byte padding1;
			public byte padding2;
			public byte padding3;
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
			public SDL_GPUGraphicsPipelineTargetInfo target_info;
			public uint props;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUComputePipelineCreateInfo
		{
			public UIntPtr code_size;
			public byte* code;
			public byte* entrypoint;
			public uint format;
			public uint num_samplers;
			public uint num_readonly_storage_textures;
			public uint num_readonly_storage_buffers;
			public uint num_readwrite_storage_textures;
			public uint num_readwrite_storage_buffers;
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
			public IntPtr resolve_texture;
			public uint resolve_mip_level;
			public uint resolve_layer;
			public byte cycle;
			public byte cycle_resolve_texture;
			public byte padding1;
			public byte padding2;
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
			public byte cycle;
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
			public byte cycle;
			public byte padding1;
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
		public struct SDL_GPUStorageBufferReadWriteBinding
		{
			public IntPtr buffer;
			public byte cycle;
			public byte padding1;
			public byte padding2;
			public byte padding3;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GPUStorageTextureReadWriteBinding
		{
			public IntPtr texture;
			public uint mip_level;
			public uint layer;
			public byte cycle;
			public byte padding1;
			public byte padding2;
			public byte padding3;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GPUSupportsShaderFormats", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GPUSupportsShaderFormats(uint format_flags, byte* name);
		public static byte SDL_GPUSupportsShaderFormats(uint format_flags, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GPUSupportsShaderFormats(format_flags, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GPUSupportsProperties(uint props);

		[DllImport(nativeLibName, EntryPoint = "SDL_CreateGPUDevice", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_CreateGPUDevice(uint format_flags, byte debug_mode, byte* name);
		public static IntPtr SDL_CreateGPUDevice(uint format_flags, byte debug_mode, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_CreateGPUDevice(format_flags, debug_mode, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUDeviceWithProperties(uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyGPUDevice(IntPtr device);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGPUDrivers();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGPUDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGPUDriver(int index);
		public static string SDL_GetGPUDriver(int index)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGPUDriver(index));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetGPUDeviceDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetGPUDeviceDriver(IntPtr device);
		public static string SDL_GetGPUDeviceDriver(IntPtr device)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetGPUDeviceDriver(device));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGPUShaderFormats(IntPtr device);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUComputePipeline(IntPtr device, ref SDL_GPUComputePipelineCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUGraphicsPipeline(IntPtr device, ref SDL_GPUGraphicsPipelineCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUSampler(IntPtr device, ref SDL_GPUSamplerCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUShader(IntPtr device, ref SDL_GPUShaderCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUTexture(IntPtr device, ref SDL_GPUTextureCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUBuffer(IntPtr device, ref SDL_GPUBufferCreateInfo createinfo);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUTransferBuffer(IntPtr device, ref SDL_GPUTransferBufferCreateInfo createinfo);

		[DllImport(nativeLibName, EntryPoint = "SDL_SetGPUBufferName", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, byte* text);
		public static void SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, string text)
		{
			var textUTF8 = EncodeAsUTF8(text);
			INTERNAL_SDL_SetGPUBufferName(device, buffer, textUTF8);

			SDL_free((IntPtr) textUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetGPUTextureName", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_SetGPUTextureName(IntPtr device, IntPtr texture, byte* text);
		public static void SDL_SetGPUTextureName(IntPtr device, IntPtr texture, string text)
		{
			var textUTF8 = EncodeAsUTF8(text);
			INTERNAL_SDL_SetGPUTextureName(device, texture, textUTF8);

			SDL_free((IntPtr) textUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_InsertGPUDebugLabel", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_InsertGPUDebugLabel(IntPtr command_buffer, byte* text);
		public static void SDL_InsertGPUDebugLabel(IntPtr command_buffer, string text)
		{
			var textUTF8 = EncodeAsUTF8(text);
			INTERNAL_SDL_InsertGPUDebugLabel(command_buffer, textUTF8);

			SDL_free((IntPtr) textUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_PushGPUDebugGroup", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_PushGPUDebugGroup(IntPtr command_buffer, byte* name);
		public static void SDL_PushGPUDebugGroup(IntPtr command_buffer, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			INTERNAL_SDL_PushGPUDebugGroup(command_buffer, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
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
		public static extern IntPtr SDL_BeginGPURenderPass(IntPtr command_buffer, SDL_GPUColorTargetInfo[] color_target_infos, uint num_color_targets, ref SDL_GPUDepthStencilTargetInfo depth_stencil_target_info);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUGraphicsPipeline(IntPtr render_pass, IntPtr graphics_pipeline);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUViewport(IntPtr render_pass, ref SDL_GPUViewport viewport);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUScissor(IntPtr render_pass, ref SDL_Rect scissor);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUBlendConstants(IntPtr render_pass, SDL_FColor blend_constants);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUStencilReference(IntPtr render_pass, byte reference);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexBuffers(IntPtr render_pass, uint first_slot, SDL_GPUBufferBinding[] bindings, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUIndexBuffer(IntPtr render_pass, ref SDL_GPUBufferBinding binding, SDL_GPUIndexElementSize index_element_size);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexSamplers(IntPtr render_pass, uint first_slot, SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexStorageTextures(IntPtr render_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexStorageBuffers(IntPtr render_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentSamplers(IntPtr render_pass, uint first_slot, SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentStorageTextures(IntPtr render_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentStorageBuffers(IntPtr render_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUIndexedPrimitives(IntPtr render_pass, uint num_indices, uint num_instances, uint first_index, int vertex_offset, uint first_instance);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUPrimitives(IntPtr render_pass, uint num_vertices, uint num_instances, uint first_vertex, uint first_instance);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUIndexedPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPURenderPass(IntPtr render_pass);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_BeginGPUComputePass(IntPtr command_buffer, SDL_GPUStorageTextureReadWriteBinding[] storage_texture_bindings, uint num_storage_texture_bindings, SDL_GPUStorageBufferReadWriteBinding[] storage_buffer_bindings, uint num_storage_buffer_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputePipeline(IntPtr compute_pass, IntPtr compute_pipeline);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeSamplers(IntPtr compute_pass, uint first_slot, SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeStorageTextures(IntPtr compute_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeStorageBuffers(IntPtr compute_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DispatchGPUCompute(IntPtr compute_pass, uint groupcount_x, uint groupcount_y, uint groupcount_z);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DispatchGPUComputeIndirect(IntPtr compute_pass, IntPtr buffer, uint offset);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPUComputePass(IntPtr compute_pass);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_MapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer, byte cycle);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnmapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_BeginGPUCopyPass(IntPtr command_buffer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UploadToGPUTexture(IntPtr copy_pass, ref SDL_GPUTextureTransferInfo source, ref SDL_GPUTextureRegion destination, byte cycle);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UploadToGPUBuffer(IntPtr copy_pass, ref SDL_GPUTransferBufferLocation source, ref SDL_GPUBufferRegion destination, byte cycle);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CopyGPUTextureToTexture(IntPtr copy_pass, ref SDL_GPUTextureLocation source, ref SDL_GPUTextureLocation destination, uint w, uint h, uint d, byte cycle);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CopyGPUBufferToBuffer(IntPtr copy_pass, ref SDL_GPUBufferLocation source, ref SDL_GPUBufferLocation destination, uint size, byte cycle);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DownloadFromGPUTexture(IntPtr copy_pass, ref SDL_GPUTextureRegion source, ref SDL_GPUTextureTransferInfo destination);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DownloadFromGPUBuffer(IntPtr copy_pass, ref SDL_GPUBufferRegion source, ref SDL_GPUTransferBufferLocation destination);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPUCopyPass(IntPtr copy_pass);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GenerateMipmapsForGPUTexture(IntPtr command_buffer, IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BlitGPUTexture(IntPtr command_buffer, ref SDL_GPUBlitInfo info);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, SDL_GPUSwapchainComposition swapchain_composition);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, SDL_GPUPresentMode present_mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ClaimWindowForGPUDevice(IntPtr device, IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetGPUSwapchainParameters(IntPtr device, IntPtr window, SDL_GPUSwapchainComposition swapchain_composition, SDL_GPUPresentMode present_mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_AcquireGPUSwapchainTexture(IntPtr command_buffer, IntPtr window, out IntPtr swapchain_texture, out uint swapchain_texture_width, out uint swapchain_texture_height);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SubmitGPUCommandBuffer(IntPtr command_buffer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SubmitGPUCommandBufferAndAcquireFence(IntPtr command_buffer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitForGPUIdle(IntPtr device);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitForGPUFences(IntPtr device, byte wait_all, IntPtr[] fences, uint num_fences);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_QueryGPUFence(IntPtr device, IntPtr fence);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUFence(IntPtr device, IntPtr fence);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GPUTextureSupportsFormat(IntPtr device, SDL_GPUTextureFormat format, SDL_GPUTextureType type, SDL_GPUTextureUsageFlags usage);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GPUTextureSupportsSampleCount(IntPtr device, SDL_GPUTextureFormat format, SDL_GPUSampleCount sample_count);

		// /usr/local/include/SDL3/SDL_haptic.h

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_HapticDirection
		{
			public byte type;
			public fixed int dir[3];
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
			public fixed ushort right_sat[3];
			public fixed ushort left_sat[3];
			public fixed short right_coeff[3];
			public fixed short left_coeff[3];
			public fixed ushort deadband[3];
			public fixed short center[3];
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
		public static extern IntPtr SDL_GetHaptics(out int count);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetHapticNameForID", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetHapticNameForID(uint instance_id);
		public static string SDL_GetHapticNameForID(uint instance_id)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetHapticNameForID(instance_id));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenHaptic(uint instance_id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetHapticFromID(uint instance_id);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetHapticID(IntPtr haptic);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetHapticName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetHapticName(IntPtr haptic);
		public static string SDL_GetHapticName(IntPtr haptic)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetHapticName(haptic));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_IsMouseHaptic();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenHapticFromMouse();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_IsJoystickHaptic(IntPtr joystick);

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
		public static extern byte SDL_HapticEffectSupported(IntPtr haptic, ref SDL_HapticEffect effect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_CreateHapticEffect(IntPtr haptic, ref SDL_HapticEffect effect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateHapticEffect(IntPtr haptic, int effect, ref SDL_HapticEffect data);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RunHapticEffect(IntPtr haptic, int effect, uint iterations);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StopHapticEffect(IntPtr haptic, int effect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyHapticEffect(IntPtr haptic, int effect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetHapticEffectStatus(IntPtr haptic, int effect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetHapticGain(IntPtr haptic, int gain);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetHapticAutocenter(IntPtr haptic, int autocenter);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PauseHaptic(IntPtr haptic);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ResumeHaptic(IntPtr haptic);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StopHapticEffects(IntPtr haptic);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_HapticRumbleSupported(IntPtr haptic);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_InitHapticRumble(IntPtr haptic);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_PlayHapticRumble(IntPtr haptic, float strength, uint length);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StopHapticRumble(IntPtr haptic);

		// /usr/local/include/SDL3/SDL_hidapi.h

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
			public byte* path;
			public ushort vendor_id;
			public ushort product_id;
			public byte* serial_number;
			public ushort release_number;
			public byte* manufacturer_string;
			public byte* product_string;
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
		public static extern void SDL_hid_free_enumeration(IntPtr devs); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_open", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_hid_open(ushort vendor_id, ushort product_id, byte* serial_number);
		public static IntPtr SDL_hid_open(ushort vendor_id, ushort product_id, string serial_number)
		{
			var serial_numberUTF8 = EncodeAsUTF8(serial_number);
			var result = INTERNAL_SDL_hid_open(vendor_id, product_id, serial_numberUTF8);

			SDL_free((IntPtr) serial_numberUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_open_path", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_hid_open_path(byte* path);
		public static IntPtr SDL_hid_open_path(string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_hid_open_path(pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_write(IntPtr dev, IntPtr data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_read_timeout(IntPtr dev, IntPtr data, UIntPtr length, int milliseconds); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_read(IntPtr dev, IntPtr data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_send_feature_report(IntPtr dev, IntPtr data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_feature_report(IntPtr dev, IntPtr data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_input_report(IntPtr dev, IntPtr data, UIntPtr length); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_close(IntPtr dev);

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_get_manufacturer_string", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_hid_get_manufacturer_string(IntPtr dev, byte* @string, UIntPtr maxlen);
		public static int SDL_hid_get_manufacturer_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			var @stringUTF8 = EncodeAsUTF8(@string);
			var result = INTERNAL_SDL_hid_get_manufacturer_string(dev, @stringUTF8, maxlen);

			SDL_free((IntPtr) @stringUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_get_product_string", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_hid_get_product_string(IntPtr dev, byte* @string, UIntPtr maxlen);
		public static int SDL_hid_get_product_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			var @stringUTF8 = EncodeAsUTF8(@string);
			var result = INTERNAL_SDL_hid_get_product_string(dev, @stringUTF8, maxlen);

			SDL_free((IntPtr) @stringUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_get_serial_number_string", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_hid_get_serial_number_string(IntPtr dev, byte* @string, UIntPtr maxlen);
		public static int SDL_hid_get_serial_number_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			var @stringUTF8 = EncodeAsUTF8(@string);
			var result = INTERNAL_SDL_hid_get_serial_number_string(dev, @stringUTF8, maxlen);

			SDL_free((IntPtr) @stringUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_hid_get_indexed_string", CallingConvention = CallingConvention.Cdecl)]
		private static extern int INTERNAL_SDL_hid_get_indexed_string(IntPtr dev, int string_index, byte* @string, UIntPtr maxlen);
		public static int SDL_hid_get_indexed_string(IntPtr dev, int string_index, string @string, UIntPtr maxlen)
		{
			var @stringUTF8 = EncodeAsUTF8(@string);
			var result = INTERNAL_SDL_hid_get_indexed_string(dev, string_index, @stringUTF8, maxlen);

			SDL_free((IntPtr) @stringUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_hid_get_device_info(IntPtr dev);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_report_descriptor(IntPtr dev, IntPtr buf, UIntPtr buf_size); // WARN_UNKNOWN_POINTER_PARAMETER

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_hid_ble_scan(byte active);

		// /usr/local/include/SDL3/SDL_hints.h

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
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM_HORI = "SDL_JOYSTICK_HIDAPI_STEAM_HORI";
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
		private static extern byte INTERNAL_SDL_SetHintWithPriority(byte* name, byte* value, SDL_HintPriority priority);
		public static byte SDL_SetHintWithPriority(string name, string value, SDL_HintPriority priority)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var valueUTF8 = EncodeAsUTF8(value);
			var result = INTERNAL_SDL_SetHintWithPriority(nameUTF8, valueUTF8, priority);

			SDL_free((IntPtr) nameUTF8);
			SDL_free((IntPtr) valueUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetHint(byte* name, byte* value);
		public static byte SDL_SetHint(string name, string value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var valueUTF8 = EncodeAsUTF8(value);
			var result = INTERNAL_SDL_SetHint(nameUTF8, valueUTF8);

			SDL_free((IntPtr) nameUTF8);
			SDL_free((IntPtr) valueUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ResetHint", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_ResetHint(byte* name);
		public static byte SDL_ResetHint(string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_ResetHint(nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetHints();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetHint", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetHint(byte* name);
		public static string SDL_GetHint(string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = DecodeFromUTF8(INTERNAL_SDL_GetHint(nameUTF8));

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetHintBoolean", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GetHintBoolean(byte* name, byte default_value);
		public static byte SDL_GetHintBoolean(string name, byte default_value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_GetHintBoolean(nameUTF8, default_value);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_HintCallback(IntPtr userdata, byte* name, byte* oldValue, byte* newValue);

		[DllImport(nativeLibName, EntryPoint = "SDL_AddHintCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_AddHintCallback(byte* name, SDL_HintCallback callback, IntPtr userdata);
		public static byte SDL_AddHintCallback(string name, SDL_HintCallback callback, IntPtr userdata)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_AddHintCallback(nameUTF8, callback, userdata);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_RemoveHintCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_RemoveHintCallback(byte* name, SDL_HintCallback callback, IntPtr userdata);
		public static void SDL_RemoveHintCallback(string name, SDL_HintCallback callback, IntPtr userdata)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			INTERNAL_SDL_RemoveHintCallback(nameUTF8, callback, userdata);

			SDL_free((IntPtr) nameUTF8);
		}

		// /usr/local/include/SDL3/SDL_init.h

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
		public delegate SDL_AppResult SDL_AppInit_func(IntPtr appstate, int argc, IntPtr argv);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL_AppResult SDL_AppIterate_func(IntPtr appstate);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL_AppResult SDL_AppEvent_func(IntPtr appstate, SDL_Event* evt);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AppQuit_func(IntPtr appstate, SDL_AppResult result);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_Init(SDL_InitFlags flags);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_InitSubSystem(SDL_InitFlags flags);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_QuitSubSystem(SDL_InitFlags flags);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL_InitFlags SDL_WasInit(SDL_InitFlags flags);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Quit();

		[DllImport(nativeLibName, EntryPoint = "SDL_SetAppMetadata", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetAppMetadata(byte* appname, byte* appversion, byte* appidentifier);
		public static byte SDL_SetAppMetadata(string appname, string appversion, string appidentifier)
		{
			var appnameUTF8 = EncodeAsUTF8(appname);
			var appversionUTF8 = EncodeAsUTF8(appversion);
			var appidentifierUTF8 = EncodeAsUTF8(appidentifier);
			var result = INTERNAL_SDL_SetAppMetadata(appnameUTF8, appversionUTF8, appidentifierUTF8);

			SDL_free((IntPtr) appnameUTF8);
			SDL_free((IntPtr) appversionUTF8);
			SDL_free((IntPtr) appidentifierUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_SetAppMetadataProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_SetAppMetadataProperty(byte* name, byte* value);
		public static byte SDL_SetAppMetadataProperty(string name, string value)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var valueUTF8 = EncodeAsUTF8(value);
			var result = INTERNAL_SDL_SetAppMetadataProperty(nameUTF8, valueUTF8);

			SDL_free((IntPtr) nameUTF8);
			SDL_free((IntPtr) valueUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetAppMetadataProperty", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetAppMetadataProperty(byte* name);
		public static string SDL_GetAppMetadataProperty(string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = DecodeFromUTF8(INTERNAL_SDL_GetAppMetadataProperty(nameUTF8));

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_loadso.h

		[DllImport(nativeLibName, EntryPoint = "SDL_LoadObject", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadObject(byte* sofile);
		public static IntPtr SDL_LoadObject(string sofile)
		{
			var sofileUTF8 = EncodeAsUTF8(sofile);
			var result = INTERNAL_SDL_LoadObject(sofileUTF8);

			SDL_free((IntPtr) sofileUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LoadFunction", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_LoadFunction(IntPtr handle, byte* name);
		public static IntPtr SDL_LoadFunction(IntPtr handle, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_LoadFunction(handle, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnloadObject(IntPtr handle);

		// /usr/local/include/SDL3/SDL_locale.h

		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Locale
		{
			public byte* language;
			public byte* country;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetPreferredLocales(out int count);

		// /usr/local/include/SDL3/SDL_log.h

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
			SDL_LOG_PRIORITY_INVALID = 0,
			SDL_LOG_PRIORITY_TRACE = 1,
			SDL_LOG_PRIORITY_VERBOSE = 2,
			SDL_LOG_PRIORITY_DEBUG = 3,
			SDL_LOG_PRIORITY_INFO = 4,
			SDL_LOG_PRIORITY_WARN = 5,
			SDL_LOG_PRIORITY_ERROR = 6,
			SDL_LOG_PRIORITY_CRITICAL = 7,
			SDL_LOG_PRIORITY_COUNT = 8,
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
		private static extern byte INTERNAL_SDL_SetLogPriorityPrefix(SDL_LogPriority priority, byte* prefix);
		public static byte SDL_SetLogPriorityPrefix(SDL_LogPriority priority, string prefix)
		{
			var prefixUTF8 = EncodeAsUTF8(prefix);
			var result = INTERNAL_SDL_SetLogPriorityPrefix(priority, prefixUTF8);

			SDL_free((IntPtr) prefixUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_Log", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_Log(byte* fmt);
		public static void SDL_Log(string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_Log(fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogTrace", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogTrace(int category, byte* fmt);
		public static void SDL_LogTrace(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogTrace(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogVerbose", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogVerbose(int category, byte* fmt);
		public static void SDL_LogVerbose(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogVerbose(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogDebug", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogDebug(int category, byte* fmt);
		public static void SDL_LogDebug(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogDebug(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogInfo", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogInfo(int category, byte* fmt);
		public static void SDL_LogInfo(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogInfo(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogWarn", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogWarn(int category, byte* fmt);
		public static void SDL_LogWarn(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogWarn(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogError", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogError(int category, byte* fmt);
		public static void SDL_LogError(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogError(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogCritical", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogCritical(int category, byte* fmt);
		public static void SDL_LogCritical(int category, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogCritical(category, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_LogMessage", CallingConvention = CallingConvention.Cdecl)]
		private static extern void INTERNAL_SDL_LogMessage(int category, SDL_LogPriority priority, byte* fmt);
		public static void SDL_LogMessage(int category, SDL_LogPriority priority, string fmt)
		{
			var fmtUTF8 = EncodeAsUTF8(fmt);
			INTERNAL_SDL_LogMessage(category, priority, fmtUTF8);

			SDL_free((IntPtr) fmtUTF8);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_LogOutputFunction(IntPtr userdata, int category, SDL_LogPriority priority, byte* message);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetLogOutputFunction(out SDL_LogOutputFunction callback, out IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetLogOutputFunction(SDL_LogOutputFunction callback, IntPtr userdata);

		// /usr/local/include/SDL3/SDL_messagebox.h

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
			public SDL_MessageBoxButtonFlags flags;
			public int buttonID;
			public byte* text;
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
			SDL_MESSAGEBOX_COLOR_COUNT = 5,
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
			public SDL_MessageBoxFlags flags;
			public IntPtr window;
			public byte* title;
			public byte* message;
			public int numbuttons;
			public SDL_MessageBoxButtonData* buttons;
			public SDL_MessageBoxColorScheme* colorScheme;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ShowMessageBox(ref SDL_MessageBoxData messageboxdata, out int buttonid);

		[DllImport(nativeLibName, EntryPoint = "SDL_ShowSimpleMessageBox", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags flags, byte* title, byte* message, IntPtr window);
		public static byte SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags flags, string title, string message, IntPtr window)
		{
			var titleUTF8 = EncodeAsUTF8(title);
			var messageUTF8 = EncodeAsUTF8(message);
			var result = INTERNAL_SDL_ShowSimpleMessageBox(flags, titleUTF8, messageUTF8, window);

			SDL_free((IntPtr) titleUTF8);
			SDL_free((IntPtr) messageUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_metal.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_CreateView(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Metal_DestroyView(IntPtr view);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_GetLayer(IntPtr view);

		// /usr/local/include/SDL3/SDL_misc.h

		[DllImport(nativeLibName, EntryPoint = "SDL_OpenURL", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_OpenURL(byte* url);
		public static byte SDL_OpenURL(string url)
		{
			var urlUTF8 = EncodeAsUTF8(url);
			var result = INTERNAL_SDL_OpenURL(urlUTF8);

			SDL_free((IntPtr) urlUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_platform.h

		[DllImport(nativeLibName, EntryPoint = "SDL_GetPlatform", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetPlatform();
		public static string SDL_GetPlatform()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetPlatform());
		}

		// /usr/local/include/SDL3/SDL_process.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateProcess(IntPtr args, byte pipe_stdio);

		public enum SDL_ProcessIO
		{
			SDL_PROCESS_STDIO_INHERITED = 0,
			SDL_PROCESS_STDIO_NULL = 1,
			SDL_PROCESS_STDIO_APP = 2,
			SDL_PROCESS_STDIO_REDIRECT = 3,
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateProcessWithProperties(uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetProcessProperties(IntPtr process);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ReadProcess(IntPtr process, out UIntPtr datasize, out int exitcode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetProcessInput(IntPtr process);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetProcessOutput(IntPtr process);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_KillProcess(IntPtr process, byte force);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_WaitProcess(IntPtr process, byte block, out int exitcode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyProcess(IntPtr process);

		// /usr/local/include/SDL3/SDL_render.h

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

		[DllImport(nativeLibName, EntryPoint = "SDL_GetRenderDriver", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetRenderDriver(int index);
		public static string SDL_GetRenderDriver(int index)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetRenderDriver(index));
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_CreateWindowAndRenderer", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_CreateWindowAndRenderer(byte* title, int width, int height, SDL_WindowFlags window_flags, out IntPtr window, out IntPtr renderer);
		public static byte SDL_CreateWindowAndRenderer(string title, int width, int height, SDL_WindowFlags window_flags, out IntPtr window, out IntPtr renderer)
		{
			var titleUTF8 = EncodeAsUTF8(title);
			var result = INTERNAL_SDL_CreateWindowAndRenderer(titleUTF8, width, height, window_flags, out window, out renderer);

			SDL_free((IntPtr) titleUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_CreateRenderer(IntPtr window, byte* name);
		public static IntPtr SDL_CreateRenderer(IntPtr window, string name)
		{
			var nameUTF8 = EncodeAsUTF8(name);
			var result = INTERNAL_SDL_CreateRenderer(window, nameUTF8);

			SDL_free((IntPtr) nameUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRendererWithProperties(uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderer(IntPtr window);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderWindow(IntPtr renderer);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetRendererName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetRendererName(IntPtr renderer);
		public static string SDL_GetRendererName(IntPtr renderer)
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetRendererName(renderer));
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetRendererProperties(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderOutputSize(IntPtr renderer, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetCurrentRenderOutputSize(IntPtr renderer, out int w, out int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTexture(IntPtr renderer, SDL_PixelFormat format, SDL_TextureAccess access, int w, int h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureWithProperties(IntPtr renderer, uint props);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetTextureProperties(IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRendererFromTexture(IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureSize(IntPtr texture, out float w, out float h);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureColorModFloat(IntPtr texture, float r, float g, float b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureColorModFloat(IntPtr texture, out float r, out float g, out float b);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureAlphaMod(IntPtr texture, byte alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureAlphaModFloat(IntPtr texture, float alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureAlphaMod(IntPtr texture, out byte alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureAlphaModFloat(IntPtr texture, out float alpha);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureBlendMode(IntPtr texture, uint blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureBlendMode(IntPtr texture, IntPtr blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetTextureScaleMode(IntPtr texture, SDL_ScaleMode scaleMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetTextureScaleMode(IntPtr texture, out SDL_ScaleMode scaleMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateTexture(IntPtr texture, ref SDL_Rect rect, IntPtr pixels, int pitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateYUVTexture(IntPtr texture, ref SDL_Rect rect, IntPtr Yplane, int Ypitch, IntPtr Uplane, int Upitch, IntPtr Vplane, int Vpitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_UpdateNVTexture(IntPtr texture, ref SDL_Rect rect, IntPtr Yplane, int Ypitch, IntPtr UVplane, int UVpitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LockTexture(IntPtr texture, ref SDL_Rect rect, out IntPtr pixels, out int pitch);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_LockTextureToSurface(IntPtr texture, ref SDL_Rect rect, out IntPtr surface);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockTexture(IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderLogicalPresentation(IntPtr renderer, int w, int h, SDL_RendererLogicalPresentation mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderLogicalPresentation(IntPtr renderer, out int w, out int h, out SDL_RendererLogicalPresentation mode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderLogicalPresentationRect(IntPtr renderer, out SDL_FRect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderCoordinatesFromWindow(IntPtr renderer, float window_x, float window_y, out float x, out float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderCoordinatesToWindow(IntPtr renderer, float x, float y, out float window_x, out float window_y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ConvertEventToRenderCoordinates(IntPtr renderer, ref SDL_Event @event);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderViewport(IntPtr renderer, ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderViewport(IntPtr renderer, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderViewportSet(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderSafeArea(IntPtr renderer, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderClipRect(IntPtr renderer, ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderClipRect(IntPtr renderer, out SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderClipEnabled(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderScale(IntPtr renderer, float scaleX, float scaleY);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderScale(IntPtr renderer, out float scaleX, out float scaleY);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderDrawColorFloat(IntPtr renderer, float r, float g, float b, float a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderDrawColorFloat(IntPtr renderer, out float r, out float g, out float b, out float a);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderColorScale(IntPtr renderer, float scale);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderColorScale(IntPtr renderer, out float scale);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderDrawBlendMode(IntPtr renderer, uint blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderDrawBlendMode(IntPtr renderer, IntPtr blendMode);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderClear(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderPoint(IntPtr renderer, float x, float y);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderPoints(IntPtr renderer, SDL_FPoint[] points, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderLine(IntPtr renderer, float x1, float y1, float x2, float y2);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderLines(IntPtr renderer, SDL_FPoint[] points, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderRect(IntPtr renderer, ref SDL_FRect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderRects(IntPtr renderer, SDL_FRect[] rects, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderFillRect(IntPtr renderer, ref SDL_FRect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderFillRects(IntPtr renderer, SDL_FRect[] rects, int count);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderTexture(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, ref SDL_FRect dstrect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderTextureRotated(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, ref SDL_FRect dstrect, double angle, ref SDL_FPoint center, SDL_FlipMode flip);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderTextureTiled(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, float scale, ref SDL_FRect dstrect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderTexture9Grid(IntPtr renderer, IntPtr texture, ref SDL_FRect srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, ref SDL_FRect dstrect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderGeometry(IntPtr renderer, IntPtr texture, SDL_Vertex[] vertices, int num_vertices, int[] indices, int num_indices);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderGeometryRaw(IntPtr renderer, IntPtr texture, IntPtr xy, int xy_stride, IntPtr color, int color_stride, IntPtr uv, int uv_stride, int num_vertices, IntPtr indices, int num_indices, int size_indices);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderReadPixels(IntPtr renderer, ref SDL_Rect rect);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RenderPresent(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyTexture(IntPtr texture);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyRenderer(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_FlushRenderer(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderMetalLayer(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderMetalCommandEncoder(IntPtr renderer);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_AddVulkanRenderSemaphores(IntPtr renderer, uint wait_stage_mask, long wait_semaphore, long signal_semaphore);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetRenderVSync(IntPtr renderer, int vsync);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetRenderVSync(IntPtr renderer, out int vsync);

		// /usr/local/include/SDL3/SDL_storage.h

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

			SDL_free((IntPtr) @overrideUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_OpenUserStorage", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_OpenUserStorage(byte* org, byte* app, uint props);
		public static IntPtr SDL_OpenUserStorage(string org, string app, uint props)
		{
			var orgUTF8 = EncodeAsUTF8(org);
			var appUTF8 = EncodeAsUTF8(app);
			var result = INTERNAL_SDL_OpenUserStorage(orgUTF8, appUTF8, props);

			SDL_free((IntPtr) orgUTF8);
			SDL_free((IntPtr) appUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_OpenFileStorage", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_OpenFileStorage(byte* path);
		public static IntPtr SDL_OpenFileStorage(string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_OpenFileStorage(pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenStorage(ref SDL_StorageInterface iface, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_CloseStorage(IntPtr storage);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_StorageReady(IntPtr storage);

		[DllImport(nativeLibName, EntryPoint = "SDL_GetStorageFileSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GetStorageFileSize(IntPtr storage, byte* path, out ulong length);
		public static byte SDL_GetStorageFileSize(IntPtr storage, string path, out ulong length)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_GetStorageFileSize(storage, pathUTF8, out length);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_ReadStorageFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_ReadStorageFile(IntPtr storage, byte* path, IntPtr destination, ulong length);
		public static byte SDL_ReadStorageFile(IntPtr storage, string path, IntPtr destination, ulong length)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_ReadStorageFile(storage, pathUTF8, destination, length);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_WriteStorageFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_WriteStorageFile(IntPtr storage, byte* path, IntPtr source, ulong length);
		public static byte SDL_WriteStorageFile(IntPtr storage, string path, IntPtr source, ulong length)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_WriteStorageFile(storage, pathUTF8, source, length);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_CreateStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_CreateStorageDirectory(IntPtr storage, byte* path);
		public static byte SDL_CreateStorageDirectory(IntPtr storage, string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_CreateStorageDirectory(storage, pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_EnumerateStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_EnumerateStorageDirectory(IntPtr storage, byte* path, SDL_EnumerateDirectoryCallback callback, IntPtr userdata);
		public static byte SDL_EnumerateStorageDirectory(IntPtr storage, string path, SDL_EnumerateDirectoryCallback callback, IntPtr userdata)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_EnumerateStorageDirectory(storage, pathUTF8, callback, userdata);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_RemoveStoragePath", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_RemoveStoragePath(IntPtr storage, byte* path);
		public static byte SDL_RemoveStoragePath(IntPtr storage, string path)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_RemoveStoragePath(storage, pathUTF8);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_RenameStoragePath", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_RenameStoragePath(IntPtr storage, byte* oldpath, byte* newpath);
		public static byte SDL_RenameStoragePath(IntPtr storage, string oldpath, string newpath)
		{
			var oldpathUTF8 = EncodeAsUTF8(oldpath);
			var newpathUTF8 = EncodeAsUTF8(newpath);
			var result = INTERNAL_SDL_RenameStoragePath(storage, oldpathUTF8, newpathUTF8);

			SDL_free((IntPtr) oldpathUTF8);
			SDL_free((IntPtr) newpathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_CopyStorageFile", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_CopyStorageFile(IntPtr storage, byte* oldpath, byte* newpath);
		public static byte SDL_CopyStorageFile(IntPtr storage, string oldpath, string newpath)
		{
			var oldpathUTF8 = EncodeAsUTF8(oldpath);
			var newpathUTF8 = EncodeAsUTF8(newpath);
			var result = INTERNAL_SDL_CopyStorageFile(storage, oldpathUTF8, newpathUTF8);

			SDL_free((IntPtr) oldpathUTF8);
			SDL_free((IntPtr) newpathUTF8);
			return result;
		}

		[DllImport(nativeLibName, EntryPoint = "SDL_GetStoragePathInfo", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte INTERNAL_SDL_GetStoragePathInfo(IntPtr storage, byte* path, out SDL_PathInfo info);
		public static byte SDL_GetStoragePathInfo(IntPtr storage, string path, out SDL_PathInfo info)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var result = INTERNAL_SDL_GetStoragePathInfo(storage, pathUTF8, out info);

			SDL_free((IntPtr) pathUTF8);
			return result;
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetStorageSpaceRemaining(IntPtr storage);

		[DllImport(nativeLibName, EntryPoint = "SDL_GlobStorageDirectory", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GlobStorageDirectory(IntPtr storage, byte* path, byte* pattern, SDL_GlobFlags flags, out int count);
		public static IntPtr SDL_GlobStorageDirectory(IntPtr storage, string path, string pattern, SDL_GlobFlags flags, out int count)
		{
			var pathUTF8 = EncodeAsUTF8(path);
			var patternUTF8 = EncodeAsUTF8(pattern);
			var result = INTERNAL_SDL_GlobStorageDirectory(storage, pathUTF8, patternUTF8, flags, out count);

			SDL_free((IntPtr) pathUTF8);
			SDL_free((IntPtr) patternUTF8);
			return result;
		}

		// /usr/local/include/SDL3/SDL_system.h

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool SDL_X11EventHook(IntPtr userdata, IntPtr xevent);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetX11EventHook(SDL_X11EventHook callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetLinuxThreadPriority(long threadID, int priority);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_SetLinuxThreadPriorityAndPolicy(long threadID, int sdlPriority, int schedPolicy);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_IsTablet();

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

		// /usr/local/include/SDL3/SDL_time.h

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
		public static extern byte SDL_GetDateTimeLocalePreferences(out SDL_DateFormat dateFormat, out SDL_TimeFormat timeFormat);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetCurrentTime(IntPtr ticks);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_TimeToDateTime(long ticks, out SDL_DateTime dt, byte localTime);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_DateTimeToTime(ref SDL_DateTime dt, IntPtr ticks);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDaysInMonth(int year, int month);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDayOfYear(int year, int month, int day);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDayOfWeek(int year, int month, int day);

		// /usr/local/include/SDL3/SDL_timer.h

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
		public static extern uint SDL_AddTimer(uint interval, SDL_TimerCallback callback, IntPtr userdata);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong SDL_NSTimerCallback(IntPtr userdata, uint timerID, ulong interval);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_AddTimerNS(ulong interval, SDL_NSTimerCallback callback, IntPtr userdata);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_RemoveTimer(uint id);

		// /usr/local/include/SDL3/SDL_version.h

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetVersion();

		[DllImport(nativeLibName, EntryPoint = "SDL_GetRevision", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr INTERNAL_SDL_GetRevision();
		public static string SDL_GetRevision()
		{
			return DecodeFromUTF8(INTERNAL_SDL_GetRevision());
		}

		// /usr/local/include/SDL3/SDL_main.h

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDL_main_func(int argc, IntPtr argv);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_main(int argc, IntPtr argv);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetMainReady();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RunApp(int argc, IntPtr argv, SDL_main_func mainFunction, IntPtr reserved);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_EnterAppMainCallbacks(int argc, IntPtr argv, SDL_AppInit_func appinit, SDL_AppIterate_func appiter, SDL_AppEvent_func appevent, SDL_AppQuit_func appquit);


	}
}