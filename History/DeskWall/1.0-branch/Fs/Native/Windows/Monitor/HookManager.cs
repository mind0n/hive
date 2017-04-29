using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Fs.Native.Windows.API;

namespace Fs.Native.Windows.Monitor
{
	/// <summary>
	/// This class monitors all mouse activities globally (also outside of the application)
	/// and provides appropriate events.
	/// </summary>
	public static class HookManager
	{
		#region Monitors
		private static Behavior _lastBehavior;
		public static Behavior LastBehavior
		{
			get
			{
				return _lastBehavior;
			}
		}
		public struct Behavior
		{
			public DateTime Moment;
			public int BehaviorType;
			public bool IsWithinDiff(DateTime moment, double duration)
			{
				TimeSpan ts = moment - Moment;
				double n = ts.TotalMilliseconds;
				if (n >= duration)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		private static void UpdateLastBehavior(int UserBehavior)
		{
			_lastBehavior.Moment = DateTime.Now;
			_lastBehavior.BehaviorType = UserBehavior;
		}

		#endregion Monitors

		#region Windows hook processing

		//##############################################################################
		#region Mouse hook processing

		/// <summary>
		/// This field is not objectively needed but we need to keep a reference on a delegate which will be
		/// passed to unmanaged code. To avoid GC to clean it up.
		/// When passing delegates to unmanaged code, they must be kept alive by the managed application
		/// until it is guaranteed that they will never be called.
		/// </summary>
		private static WinAPI.HookProc s_MouseDelegate;

		/// <summary>
		/// Stores the handle to the mouse hook procedure.
		/// </summary>
		private static int s_MouseHookHandle;

		private static int m_OldX;
		private static int m_OldY;

		/// <summary>
		/// A callback function which will be called every Time a mouse activity detected.
		/// </summary>
		/// <param name="nCode">
		/// [in] Specifies whether the hook procedure must process the message.
		/// If nCode is HC_ACTION, the hook procedure must process the message.
		/// If nCode is less than zero, the hook procedure must pass the message to the
		/// CallNextHookEx function without further processing and must return the
		/// value returned by CallNextHookEx.
		/// </param>
		/// <param name="wParam">
		/// [in] Specifies whether the message was sent by the current thread.
		/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
		/// </param>
		/// <param name="lParam">
		/// [in] Pointer to a CWPSTRUCT structure that contains details about the message.
		/// </param>
		/// <returns>
		/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
		/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
		/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
		/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
		/// procedure does not call CallNextHookEx, the return value should be zero.
		/// </returns>
		private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				//Marshall the data from callback.
				WinAPI.MouseLLHookStruct mouseHookStruct = (WinAPI.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(WinAPI.MouseLLHookStruct));

				//detect button clicked
				MouseButtons button = MouseButtons.None;
				short mouseDelta = 0;
				int clickCount = 0;
				bool mouseDown = false;
				bool mouseUp = false;

				switch (wParam)
				{
					case WinAPI.WM_LBUTTONDOWN:
						mouseDown = true;
						button = MouseButtons.Left;
						clickCount = 1;
						break;
					case WinAPI.WM_LBUTTONUP:
						mouseUp = true;
						button = MouseButtons.Left;
						clickCount = 1;
						break;
					case WinAPI.WM_LBUTTONDBLCLK:
						button = MouseButtons.Left;
						clickCount = 2;
						break;
					case WinAPI.WM_RBUTTONDOWN:
						mouseDown = true;
						button = MouseButtons.Right;
						clickCount = 1;
						break;
					case WinAPI.WM_RBUTTONUP:
						mouseUp = true;
						button = MouseButtons.Right;
						clickCount = 1;
						break;
					case WinAPI.WM_RBUTTONDBLCLK:
						button = MouseButtons.Right;
						clickCount = 2;
						break;
					case WinAPI.WM_MOUSEWHEEL:
						//If the message is WM_MOUSEWHEEL, the high-order word of MouseData member is the wheel delta.
						//One wheel click is defined as WHEEL_DELTA, which is 120.
						//(value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
						mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);

						//TODO: X BUTTONS (I havent them so was unable to test)
						//If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
						//or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released,
						//and the low-order word is reserved. This value can be one or more of the following values.
						//Otherwise, MouseData is not used.
						break;
				}

				//generate event
				MouseEventExtArgs e = new MouseEventExtArgs(
				button,
				clickCount,
				mouseHookStruct.Point.X,
				mouseHookStruct.Point.Y,
				mouseDelta);

				//Mouse up
				if (s_MouseUp != null && mouseUp)
				{
					s_MouseUp.Invoke(null, e);
				}

				//Mouse down
				if (s_MouseDown != null && mouseDown)
				{
					s_MouseDown.Invoke(null, e);
				}

				//If someone listens to click and a click is heppened
				if (s_MouseClick != null && clickCount > 0)
				{
					s_MouseClick.Invoke(null, e);
				}

				//If someone listens to click and a click is heppened
				if (s_MouseClickExt != null && clickCount > 0)
				{
					s_MouseClickExt.Invoke(null, e);
				}

				//If someone listens to double click and a click is heppened
				if (s_MouseDoubleClick != null && clickCount == 2)
				{
					s_MouseDoubleClick.Invoke(null, e);
				}

				//Wheel was moved
				if (s_MouseWheel != null && mouseDelta != 0)
				{
					s_MouseWheel.Invoke(null, e);
				}

				//If someone listens to move and there was a change in coordinates raise move event
				if ((s_MouseMove != null || s_MouseMoveExt != null) && (m_OldX != mouseHookStruct.Point.X || m_OldY != mouseHookStruct.Point.Y))
				{
					m_OldX = mouseHookStruct.Point.X;
					m_OldY = mouseHookStruct.Point.Y;
					if (s_MouseMove != null)
					{
						s_MouseMove.Invoke(null, e);
					}

					if (s_MouseMoveExt != null)
					{
						s_MouseMoveExt.Invoke(null, e);
					}
				}

				if (e.Handled)
				{
					return -1;
				}
			}
			UpdateLastBehavior(wParam);
			//call next hook
			return WinAPI.CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
		}

		private static void EnsureSubscribedToGlobalMouseEvents()
		{
			// install Mouse hook only if it is not installed and must be installed
			if (s_MouseHookHandle == 0)
			{
				//See comment of this field. To avoid GC to clean it up.
				s_MouseDelegate = MouseHookProc;
				//install hook
				s_MouseHookHandle = WinAPI.SetWindowsHookEx(
				WinAPI.WH_MOUSE_LL,
				s_MouseDelegate,
				Marshal.GetHINSTANCE(
				Assembly.GetExecutingAssembly().GetModules()[0]),
				0);
				//If SetWindowsHookEx fails.
				if (s_MouseHookHandle == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//do cleanup

					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}

		private static void TryUnsubscribeFromGlobalMouseEvents()
		{
			//if no subsribers are registered unsubsribe from hook
			if (s_MouseClick == null &&
			s_MouseDown == null &&
			s_MouseMove == null &&
			s_MouseUp == null &&
			s_MouseClickExt == null &&
			s_MouseMoveExt == null &&
			s_MouseWheel == null)
			{
				ForceUnsunscribeFromGlobalMouseEvents();
			}
		}

		private static void ForceUnsunscribeFromGlobalMouseEvents()
		{
			if (s_MouseHookHandle != 0)
			{
				//uninstall hook
				int result = WinAPI.UnhookWindowsHookEx(s_MouseHookHandle);
				//reset invalid handle
				s_MouseHookHandle = 0;
				//Free up for GC
				s_MouseDelegate = null;
				//if failed and exception must be thrown
				if (result == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}

		#endregion

		#region System messages hook processing
		//##############################################################################
		/// <summary>
		/// Stores the handle to the System messages hook procedure.
		/// </summary>
		private static int s_SysMessageHookHandle;

		/// <summary>
		/// This field is not objectively needed but we need to keep a reference on a delegate which will be
		/// passed to unmanaged code. To avoid GC to clean it up.
		/// When passing delegates to unmanaged code, they must be kept alive by the managed application
		/// until it is guaranteed that they will never be called.
		/// </summary>
		private static WinAPI.HookProc s_SysMessageDelegate;

		/// <summary>
		/// A callback function which will be called every Time a keyboard activity detected.
		/// </summary>
		/// <param name="nCode">
		/// [in] Specifies whether the hook procedure must process the message.
		/// If nCode is HC_ACTION, the hook procedure must process the message.
		/// If nCode is less than zero, the hook procedure must pass the message to the
		/// CallNextHookEx function without further processing and must return the
		/// value returned by CallNextHookEx.
		/// </param>
		/// <param name="wParam">
		/// [in] Specifies whether the message was sent by the current thread.
		/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
		/// </param>
		/// <param name="lParam">
		/// [in] Pointer to a CWPSTRUCT structure that contains details about the message.
		/// </param>
		/// <returns>
		/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
		/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
		/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
		/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
		/// procedure does not call CallNextHookEx, the return value should be zero.
		/// </returns>
		private static int SysMessageHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			if (nCode == WinAPI.SC_SCREENSAVE)
			{
				return -1;
			}
			else
			{
				return WinAPI.CallNextHookEx(s_SysMessageHookHandle, nCode, wParam, lParam);
			}
		}
		private static void EnsureSubscribedToGlobalSysMessageEvents()
		{
			// install System Messages hook only if it is not installed and must be installed
			if (s_SysMessageHookHandle == 0)
			{
				//See comment of this field. To avoid GC to clean it up.
				s_SysMessageDelegate = SysMessageHookProc;
				//install hook
				s_SysMessageHookHandle = WinAPI.SetWindowsHookEx(
				WinAPI.SC_SCREENSAVE,
				s_SysMessageDelegate,
				Marshal.GetHINSTANCE(
				Assembly.GetExecutingAssembly().GetModules()[0]),
				0);
				//If SetWindowsHookEx fails.
				if (s_SysMessageHookHandle == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//do cleanup

					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}
		private static void TryUnsubscribeFromGlobalSysMessageEvents()
		{
			//if no subsribers are registered unsubsribe from hook
			ForceUnsunscribeFromGlobalSysMessageEvents();
		}

		private static void ForceUnsunscribeFromGlobalSysMessageEvents()
		{
			if (s_KeyboardHookHandle != 0)
			{
				//uninstall hook
				int result = WinAPI.UnhookWindowsHookEx(s_KeyboardHookHandle);
				//reset invalid handle
				s_KeyboardHookHandle = 0;
				//Free up for GC
				s_KeyboardDelegate = null;
				//if failed and exception must be thrown
				if (result == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}
		#endregion

		//##############################################################################
		#region Keyboard hook processing


		/// <summary>
		/// This field is not objectively needed but we need to keep a reference on a delegate which will be
		/// passed to unmanaged code. To avoid GC to clean it up.
		/// When passing delegates to unmanaged code, they must be kept alive by the managed application
		/// until it is guaranteed that they will never be called.
		/// </summary>
		private static WinAPI.HookProc s_KeyboardDelegate;

		/// <summary>
		/// Stores the handle to the Keyboard hook procedure.
		/// </summary>
		private static int s_KeyboardHookHandle;

		/// <summary>
		/// A callback function which will be called every Time a keyboard activity detected.
		/// </summary>
		/// <param name="nCode">
		/// [in] Specifies whether the hook procedure must process the message.
		/// If nCode is HC_ACTION, the hook procedure must process the message.
		/// If nCode is less than zero, the hook procedure must pass the message to the
		/// CallNextHookEx function without further processing and must return the
		/// value returned by CallNextHookEx.
		/// </param>
		/// <param name="wParam">
		/// [in] Specifies whether the message was sent by the current thread.
		/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
		/// </param>
		/// <param name="lParam">
		/// [in] Pointer to a CWPSTRUCT structure that contains details about the message.
		/// </param>
		/// <returns>
		/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
		/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
		/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
		/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
		/// procedure does not call CallNextHookEx, the return value should be zero.
		/// </returns>
		private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			//indicates if any of underlaing events set e.Handled flag
			bool handled = false;

			if (nCode >= 0)
			{
				//read structure KeyboardHookStruct at lParam
				WinAPI.KeyboardHookStruct MyKeyboardHookStruct = (WinAPI.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(WinAPI.KeyboardHookStruct));
				//raise KeyDown
				if (s_KeyDown != null && (wParam == WinAPI.WM_KEYDOWN || wParam == WinAPI.WM_SYSKEYDOWN))
				{
					Keys keyData = (Keys)MyKeyboardHookStruct.VirtualKeyCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					int nkey = e.KeyValue;
					if (nkey == 160 || nkey == 161 || e.Shift)
					{
						KeyboardStatus.Shift = true;
					}
					if (nkey == 162 || nkey == 163 || e.Control)
					{
						KeyboardStatus.Ctrl = true;
					}
					if (nkey == 164 || nkey == 165 || e.Alt)
					{
						KeyboardStatus.Alt = true;
					}
					s_KeyDown.Invoke(null, e);
					handled = e.Handled;
				}

				// raise KeyPress
				if (s_KeyPress != null && wParam == WinAPI.WM_KEYDOWN)
				{
					bool isDownShift = ((WinAPI.GetKeyState(WinAPI.VK_SHIFT) & 0x80) == 0x80 ? true : false);
					bool isDownCapslock = (WinAPI.GetKeyState(WinAPI.VK_CAPITAL) != 0 ? true : false);

					byte[] keyState = new byte[256];
					WinAPI.GetKeyboardState(keyState);
					byte[] inBuffer = new byte[2];
					if (WinAPI.ToAscii(MyKeyboardHookStruct.VirtualKeyCode,
					MyKeyboardHookStruct.ScanCode,
					keyState,
					inBuffer,
					MyKeyboardHookStruct.Flags) == 1)
					{
						char key = (char)inBuffer[0];
						if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
						KeyPressEventArgs e = new KeyPressEventArgs(key);
						s_KeyPress.Invoke(null, e);
						handled = handled || e.Handled;
					}
				}

				// raise KeyUp
				if (s_KeyUp != null && (wParam == WinAPI.WM_KEYUP || wParam == WinAPI.WM_SYSKEYUP))
				{
					Keys keyData = (Keys)MyKeyboardHookStruct.VirtualKeyCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					int nkey = e.KeyValue;
					if (nkey == 160 || nkey == 161 || e.Shift)
					{
						KeyboardStatus.Shift = false;
					}
					if (nkey == 162 || nkey == 163 || e.Control)
					{
						KeyboardStatus.Ctrl = false;
					}
					if (nkey == 164 || nkey == 165 || e.Alt)
					{
						KeyboardStatus.Alt = false;
					}

					s_KeyUp.Invoke(null, e);
					handled = handled || e.Handled;
				}

			}
			UpdateLastBehavior(wParam);
			//if event handled in application do not handoff to other listeners
			if (handled)
				return -1;

			//forward to other application
			return WinAPI.CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
		}
		private static void EnsureSubscribedToGlobalKeyboardEvents()
		{
			// install Keyboard hook only if it is not installed and must be installed
			if (s_KeyboardHookHandle == 0)
			{
				//See comment of this field. To avoid GC to clean it up.
				s_KeyboardDelegate = KeyboardHookProc;
				//install hook
				s_KeyboardHookHandle = WinAPI.SetWindowsHookEx(
				WinAPI.WH_KEYBOARD_LL,
				s_KeyboardDelegate,
				Marshal.GetHINSTANCE(
				Assembly.GetExecutingAssembly().GetModules()[0]),
				0);
				//If SetWindowsHookEx fails.
				if (s_KeyboardHookHandle == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//do cleanup

					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}

		private static void TryUnsubscribeFromGlobalKeyboardEvents()
		{
			//if no subsribers are registered unsubsribe from hook
			if (s_KeyDown == null &&
			s_KeyUp == null &&
			s_KeyPress == null)
			{
				ForceUnsunscribeFromGlobalKeyboardEvents();
			}
		}

		private static void ForceUnsunscribeFromGlobalKeyboardEvents()
		{
			if (s_KeyboardHookHandle != 0)
			{
				//uninstall hook
				int result = WinAPI.UnhookWindowsHookEx(s_KeyboardHookHandle);
				//reset invalid handle
				s_KeyboardHookHandle = 0;
				//Free up for GC
				s_KeyboardDelegate = null;
				//if failed and exception must be thrown
				if (result == 0)
				{
					//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set.
					int errorCode = Marshal.GetLastWin32Error();
					//Initializes and throws a new instance of the Win32Exception class with the specified error.
					throw new Win32Exception(errorCode);
				}
			}
		}

		#endregion Keyboard hook processing

		#endregion Windows hook processing

		#region Windows events
		//################################################################
		#region Mouse events

		private static event MouseEventHandler s_MouseMove;

		/// <summary>
		/// Occurs when the mouse pointer is moved.
		/// </summary>
		public static event MouseEventHandler MouseMove
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseMove += value;
			}

			remove
			{
				s_MouseMove -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event EventHandler<MouseEventExtArgs> s_MouseMoveExt;

		/// <summary>
		/// Occurs when the mouse pointer is moved.
		/// </summary>
		/// <remarks>
		/// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to
		/// supress further processing of mouse movement in other applications.
		/// </remarks>
		public static event EventHandler<MouseEventExtArgs> MouseMoveExt
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseMoveExt += value;
			}

			remove
			{

				s_MouseMoveExt -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event MouseEventHandler s_MouseClick;

		/// <summary>
		/// Occurs when a click was performed by the mouse.
		/// </summary>
		public static event MouseEventHandler MouseClick
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseClick += value;
			}
			remove
			{
				s_MouseClick -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event EventHandler<MouseEventExtArgs> s_MouseClickExt;

		/// <summary>
		/// Occurs when a click was performed by the mouse.
		/// </summary>
		/// <remarks>
		/// This event provides extended arguments of type <see cref="MouseEventArgs"/> enabling you to
		/// supress further processing of mouse click in other applications.
		/// </remarks>
		public static event EventHandler<MouseEventExtArgs> MouseClickExt
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseClickExt += value;
			}
			remove
			{
				s_MouseClickExt -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event MouseEventHandler s_MouseDown;

		/// <summary>
		/// Occurs when the mouse a mouse button is pressed.
		/// </summary>
		public static event MouseEventHandler MouseDown
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseDown += value;
			}
			remove
			{
				s_MouseDown -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event MouseEventHandler s_MouseUp;

		/// <summary>
		/// Occurs when a mouse button is released.
		/// </summary>
		public static event MouseEventHandler MouseUp
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseUp += value;
			}
			remove
			{
				s_MouseUp -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		private static event MouseEventHandler s_MouseWheel;

		/// <summary>
		/// Occurs when the mouse wheel moves.
		/// </summary>
		public static event MouseEventHandler MouseWheel
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				s_MouseWheel += value;
			}
			remove
			{
				s_MouseWheel -= value;
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}


		private static event MouseEventHandler s_MouseDoubleClick;

		//The double click event will not be provided directly from hook.
		//To fire the double click event wee need to monitor mouse up event and when it occures
		//Two times during the time interval which is defined in Windows as a doble click time
		//we fire this event.

		/// <summary>
		/// Occurs when a double clicked was performed by the mouse.
		/// </summary>
		public static event MouseEventHandler MouseDoubleClick
		{
			add
			{
				EnsureSubscribedToGlobalMouseEvents();
				if (s_MouseDoubleClick == null)
				{
					//We create a timer to monitor interval between two clicks
					//s_DoubleClickTimer = new Timer
					//{
					//    //This interval will be set to the value we retrive from windows. This is a windows setting from contro planel.
					//    Interval = WinAPI.GetDoubleClickTime(),
					//    //We do not start timer yet. It will be start when the click occures.
					//    Enabled = false
					//};
					//We define the callback function for the timer
					s_DoubleClickTimer.Tick += DoubleClickTimeElapsed;
					//We start to monitor mouse up event.
					MouseUp += OnMouseUp;
				}
				s_MouseDoubleClick += value;
			}
			remove
			{
				if (s_MouseDoubleClick != null)
				{
					s_MouseDoubleClick -= value;
					if (s_MouseDoubleClick == null)
					{
						//Stop monitoring mouse up
						MouseUp -= OnMouseUp;
						//Dispose the timer
						s_DoubleClickTimer.Tick -= DoubleClickTimeElapsed;
						s_DoubleClickTimer = null;
					}
				}
				TryUnsubscribeFromGlobalMouseEvents();
			}
		}

		//This field remembers mouse button pressed because in addition to the short interval it must be also the same button.
		private static MouseButtons s_PrevClickedButton;
		//The timer to monitor time interval between two clicks.
		private static Timer s_DoubleClickTimer;

		private static void DoubleClickTimeElapsed(object sender, EventArgs e)
		{
			//Timer is alapsed and no second click ocuured
			s_DoubleClickTimer.Enabled = false;
			s_PrevClickedButton = MouseButtons.None;
		}

		/// <summary>
		/// This method is designed to monitor mouse clicks in order to fire a double click event if interval between
		/// clicks was short enaugh.
		/// </summary>
		/// <param name="sender">Is always null</param>
		/// <param name="e">Some information about click heppened.</param>
		private static void OnMouseUp(object sender, MouseEventArgs e)
		{
			//This should not heppen
			if (e.Clicks < 1) { return; }
			//If the secon click heppened on the same button
			if (e.Button.Equals(s_PrevClickedButton))
			{
				if (s_MouseDoubleClick != null)
				{
					//Fire double click
					s_MouseDoubleClick.Invoke(null, e);
				}
				//Stop timer
				s_DoubleClickTimer.Enabled = false;
				s_PrevClickedButton = MouseButtons.None;
			}
			else
			{
				//If it was the firts click start the timer
				s_DoubleClickTimer.Enabled = true;
				s_PrevClickedButton = e.Button;
			}
		}
		#endregion
		//################################################################
		#region System events

		//private static EventHandler s_MsgNoticed;

		//public static event EventHandler MsgNoticed
		//{
		//    add
		//    {
		//        EnsureSubscribedToGlobalSysMessageEvents();
		//        s_MsgNoticed += value;
		//    }
		//    remove
		//    {
		//        s_MsgNoticed -= value;
		//        TryUnsubscribeFromGlobalSysMessageEvents();
		//    }
		//}

		#endregion System events
		//################################################################
		#region Keyboard events

		private static event KeyPressEventHandler s_KeyPress;

		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		/// <remarks>
		/// Key events occur in the following order:
		/// <list type="number">
		/// <item>KeyDown</item>
		/// <item>KeyPress</item>
		/// <item>KeyUp</item>
		/// </list>
		///The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and KeyUp events.
		///Use the KeyChar property to sample keystrokes at run time and to consume or modify a subset of common keystrokes.
		///To handle keyboard events only in your application and not enable other applications to receive keyboard events,
		/// set the KeyPressEventArgs.Handled property in your form's KeyPress event-handling method to <b>true</b>.
		/// </remarks>
		public static event KeyPressEventHandler KeyPress
		{
			add
			{
				EnsureSubscribedToGlobalKeyboardEvents();
				s_KeyPress += value;
				GC.KeepAlive(s_KeyPress);
			}
			remove
			{
				s_KeyPress -= value;
				TryUnsubscribeFromGlobalKeyboardEvents();
			}
		}

		private static event KeyEventHandler s_KeyUp;

		/// <summary>
		/// Occurs when a key is released.
		/// </summary>
		public static event KeyEventHandler KeyUp
		{
			add
			{
				EnsureSubscribedToGlobalKeyboardEvents();
				s_KeyUp += value;
				GC.KeepAlive(s_KeyUp);
			}
			remove
			{
				s_KeyUp -= value;
				TryUnsubscribeFromGlobalKeyboardEvents();
			}
		}

		private static event KeyEventHandler s_KeyDown;

		/// <summary>
		/// Occurs when a key is preseed.
		/// </summary>
		public static event KeyEventHandler KeyDown
		{
			add
			{
				EnsureSubscribedToGlobalKeyboardEvents();
				s_KeyDown += value;
				GC.KeepAlive(s_KeyDown);
			}
			remove
			{
				s_KeyDown -= value;
				TryUnsubscribeFromGlobalKeyboardEvents();
			}
		}


		#endregion
		#endregion
	}

	#region MouseEventExtArgs
	/// <summary>
	/// Provides data for the MouseClickExt and MouseMoveExt events. It also provides a property Handled.
	/// Set this property to <b>true</b> to prevent further processing of the event in other applications.
	/// </summary>
	public class MouseEventExtArgs : MouseEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the MouseEventArgs class.
		/// </summary>
		/// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
		public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
			: base(buttons, clicks, x, y, delta)
		{ }

		/// <summary>
		/// Initializes a new instance of the MouseEventArgs class.
		/// </summary>
		/// <param name="e">An ordinary <see cref="MouseEventArgs"/> argument to be extended.</param>
		internal MouseEventExtArgs(MouseEventArgs e)
			: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
		{ }

		private bool m_Handled;

		/// <summary>
		/// Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other applications.
		/// </summary>
		public bool Handled
		{
			get { return m_Handled; }
			set { m_Handled = value; }
		}
	}
	#endregion
}