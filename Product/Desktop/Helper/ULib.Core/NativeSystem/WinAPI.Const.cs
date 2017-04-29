using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NativeSystem.API
{
	public partial class WinAPI
	{
		#region VirtualAllocEx
		[Flags]
		public enum AllocationType
		{
			Commit = 0x1000,
			Reserve = 0x2000,
			Decommit = 0x4000,
			Release = 0x8000,
			Reset = 0x80000,
			Physical = 0x400000,
			TopDown = 0x100000,
			WriteWatch = 0x200000,
			LargePages = 0x20000000
		}

		[Flags]
		public enum MemoryProtection
		{
			Execute = 0x10,
			ExecuteRead = 0x20,
			ExecuteReadWrite = 0x40,
			ExecuteWriteCopy = 0x80,
			NoAccess = 0x01,
			ReadOnly = 0x02,
			ReadWrite = 0x04,
			WriteCopy = 0x08,
			GuardModifierflag = 0x100,
			NoCacheModifierflag = 0x200,
			WriteCombineModifierflag = 0x400
		}
		#endregion VirtualAllocEx

		#region OpenProcess
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VMOperation = 0x00000008,
			VMRead = 0x00000010,
			VMWrite = 0x00000020,
			DupHandle = 0x00000040,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			Synchronize = 0x00100000
		}

		#endregion OpenProcess

		#region ExitWindowsEx

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TokPriv1Luid
		{
			public int Count;
			public long Luid;
			public int Attr;
		}

		public const int SE_PRIVILEGE_ENABLED = 0x00000002;
		public const int TOKEN_QUERY = 0x00000008;
		public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
		// from Win32 header file: reason.h
		public const uint SHTDN_REASON_MAJOR_APPLICATION = 0x00040000;
		public const uint SHTDN_REASON_MINOR_INSTALLATION = 0x00000002;
		public const uint SHTDN_REASON_FLAG_PLANNED = 0x80000000;

		// from Win32 header file: winuser.h
		public const int EWX_LOGOFF = 0x00000000;
		public const int EWX_SHUTDOWN = 0x00000001;
		public const int EWX_REBOOT = 0x00000002;
		public const int EWX_FORCE = 0x00000004;
		public const int EWX_POWEROFF = 0x00000008;
		public const int EWX_FORCEIFHUNG = 0x00000010;

		#endregion ExitWindowsEx

		#region Privilege Constants
		public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
		public const string SE_DEBUG_NAME = "SeDebugPrivilege";
		public const string SE_SECURITY_NAME = "SeSecurityPrivilege";
		public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";

		#endregion Privilege Constants

		#region SetSystemTime
		[StructLayout(LayoutKind.Sequential)]
		public class SystemTime
		{
			public ushort year;
			public ushort month;
			public ushort dayofweek;
			public ushort day;
			public ushort hour;
			public ushort minute;
			public ushort second;
			public ushort milliseconds;
		}
		#endregion SetSystemTime

		#region MoveFile
		[Flags]
		public enum MoveFileFlags
		{
			MOVEFILE_REPLACE_EXISTING = 0x00000001,
			MOVEFILE_COPY_ALLOWED = 0x00000002,
			MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
			MOVEFILE_WRITE_THROUGH = 0x00000008,
			MOVEFILE_CREATE_HARDLINK = 0x00000010,
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
		}
		#endregion MoveFile

		#region System State
		public const int WM_SYSCOMMAND = 0x0112;
		public const int SC_SCREENSAVE = 0xF140;
		public const int SC_MONITORPOWER = 0xF170;
		#endregion

		#region IO
		//values from Winuser.h in Microsoft SDK.

		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
		/// </summary>
		public const int WH_MOUSE_LL = 14;

		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
		/// </summary>
		public const int WH_KEYBOARD_LL = 13;

		/// <summary>
		/// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure.
		/// </summary>
		public const int WH_MOUSE = 7;

		/// <summary>
		/// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.
		/// </summary>
		public const int WH_KEYBOARD = 2;

		/// <summary>
		/// The WM_MOUSEMOVE message is posted to a window when the cursor moves.
		/// </summary>
		public const int WM_MOUSEMOVE = 0x200;

		/// <summary>
		/// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button
		/// </summary>
		public const int WM_LBUTTONDOWN = 0x201;

		/// <summary>
		/// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
		/// </summary>
		public const int WM_RBUTTONDOWN = 0x204;

		/// <summary>
		/// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button
		/// </summary>
		public const int WM_MBUTTONDOWN = 0x207;

		/// <summary>
		/// The WM_LBUTTONUP message is posted when the user releases the left mouse button
		/// </summary>
		public const int WM_LBUTTONUP = 0x202;

		/// <summary>
		/// The WM_RBUTTONUP message is posted when the user releases the right mouse button
		/// </summary>
		public const int WM_RBUTTONUP = 0x205;

		/// <summary>
		/// The WM_MBUTTONUP message is posted when the user releases the middle mouse button
		/// </summary>
		public const int WM_MBUTTONUP = 0x208;

		/// <summary>
		/// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button
		/// </summary>
		public const int WM_LBUTTONDBLCLK = 0x203;

		/// <summary>
		/// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button
		/// </summary>
		public const int WM_RBUTTONDBLCLK = 0x206;

		/// <summary>
		/// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
		/// </summary>
		public const int WM_MBUTTONDBLCLK = 0x209;

		/// <summary>
		/// The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel.
		/// </summary>
		public const int WM_MOUSEWHEEL = 0x020A;

		/// <summary>
		/// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem
		/// key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
		/// </summary>
		public const int WM_KEYDOWN = 0x100;

		/// <summary>
		/// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem
		/// key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed,
		/// or a keyboard key that is pressed when a window has the keyboard focus.
		/// </summary>
		public const int WM_KEYUP = 0x101;

		/// <summary>
		/// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user
		/// presses the F10 key (which activates the menu bar) or holds down the ALT key and then
		/// presses another key. It also occurs when no window currently has the keyboard focus;
		/// in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that
		/// receives the message can distinguish between these two contexts by checking the context
		/// code in the lParam parameter.
		/// </summary>
		public const int WM_SYSKEYDOWN = 0x104;

		/// <summary>
		/// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user
		/// releases a key that was pressed while the ALT key was held down. It also occurs when no
		/// window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent
		/// to the active window. The window that receives the message can distinguish between
		/// these two contexts by checking the context code in the lParam parameter.
		/// </summary>
		public const int WM_SYSKEYUP = 0x105;

		public const byte VK_SHIFT = 0x10;
		public const byte VK_CAPITAL = 0x14;
		public const byte VK_NUMLOCK = 0x90;
		/// <summary>
		/// The Point structure defines the X- and Y- coordinates of a point.
		/// </summary>
		/// <remarks>
		/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
		/// </remarks>
		[StructLayout(LayoutKind.Sequential)]
		public struct Point
		{
			/// <summary>
			/// Specifies the X-coordinate of the point.
			/// </summary>
			public int X;
			/// <summary>
			/// Specifies the Y-coordinate of the point.
			/// </summary>
			public int Y;
		}

		/// <summary>
		/// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct MouseLLHookStruct
		{
			/// <summary>
			/// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates.
			/// </summary>
			public Point Point;
			/// <summary>
			/// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta.
			/// The low-order word is reserved. A positive value indicates that the wheel was rotated forward,
			/// away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
			/// One wheel click is defined as WHEEL_DELTA, which is 120.
			///If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
			/// or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released,
			/// and the low-order word is reserved. This value can be one or more of the following values. Otherwise, MouseData is not used.
			///XBUTTON1
			///The first X button was pressed or released.
			///XBUTTON2
			///The second X button was pressed or released.
			/// </summary>
			public int MouseData;
			/// <summary>
			/// Specifies the event-injected flag. An application can use the following value to test the mouse Flags. Value Purpose
			///LLMHF_INJECTED Test the event-injected flag.  
			///0
			///Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
			///1-15
			///Reserved.
			/// </summary>
			public int Flags;
			/// <summary>
			/// Specifies the Time stamp for this message.
			/// </summary>
			public int Time;
			/// <summary>
			/// Specifies extra information associated with the message.
			/// </summary>
			public int ExtraInfo;
		}

		/// <summary>
		/// The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event.
		/// </summary>
		/// <remarks>
		/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
		/// </remarks>
		[StructLayout(LayoutKind.Sequential)]
		public struct KeyboardHookStruct
		{
			/// <summary>
			/// Specifies a virtual-key code. The code must be a value in the range 1 to 254.
			/// </summary>
			public int VirtualKeyCode;
			/// <summary>
			/// Specifies a hardware scan code for the key.
			/// </summary>
			public int ScanCode;
			/// <summary>
			/// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
			/// </summary>
			public int Flags;
			/// <summary>
			/// Specifies the Time stamp for this message.
			/// </summary>
			public int Time;
			/// <summary>
			/// Specifies extra information associated with the message.
			/// </summary>
			public int ExtraInfo;
		}

		#endregion IO
	}



}