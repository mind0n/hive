using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;
using Joy.Native.Windows.API;

namespace Joy.Native.Windows.Desktop
{
	public static class Screensaver
	{
		// Signatures for unmanaged calls


		// Callbacks


		// Constants

		private const int SPI_GETSCREENSAVERACTIVE = 16;
		private const int SPI_SETSCREENSAVERACTIVE = 17;
		private const int SPI_GETSCREENSAVERTIMEOUT = 14;
		private const int SPI_SETSCREENSAVERTIMEOUT = 15;
		private const int SPI_GETSCREENSAVERRUNNING = 114;
		private const int SPIF_SENDWININICHANGE = 2;

		private const uint DESKTOP_WRITEOBJECTS = 0x0080;
		private const uint DESKTOP_READOBJECTS = 0x0001;
		private const int WM_CLOSE = 16;


		// Returns TRUE if the screen saver is active

		// (enabled, but not necessarily running).

		public static bool GetScreenSaverActive()
		{
			bool isActive = false;

			WinAPI.SystemParametersInfo(SPI_GETSCREENSAVERACTIVE, 0,
			ref isActive, 0);
			return isActive;
		}

		// Pass in TRUE(1) to activate or FALSE(0) to deactivate

		// the screen saver.

		public static void SetScreenSaverActive(int Active)
		{
			int nullVar = 0;

			WinAPI.SystemParametersInfo(SPI_SETSCREENSAVERACTIVE,
			Active, ref nullVar, SPIF_SENDWININICHANGE);
		}

		// Returns the screen saver timeout setting, in seconds

		public static Int32 GetScreenSaverTimeout()
		{
			Int32 value = 0;

			WinAPI.SystemParametersInfo(SPI_GETSCREENSAVERTIMEOUT, 0,
			ref value, 0);
			return value;
		}

		// Pass in the number of seconds to set the screen saver

		// timeout value.

		public static void SetScreenSaverTimeout(Int32 Value)
		{
			int nullVar = 0;

			WinAPI.SystemParametersInfo(SPI_SETSCREENSAVERTIMEOUT,
			Value, ref nullVar, SPIF_SENDWININICHANGE);
		}

		// Returns TRUE if the screen saver is actually running

		public static bool GetScreenSaverRunning()
		{
			bool isRunning = false;

			WinAPI.SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0,
			ref isRunning, 0);
			return isRunning;
		}

		// From Microsoft's Knowledge Base article #140723:

		// http://support.microsoft.com/kb/140723

		// "How to force a screen saver to close once started

		// in Windows NT, Windows 2000, and Windows Server 2003"


		public static void KillScreenSaver(bool canCloseForegroundWindow)
		{
			IntPtr hDesktop = WinAPI.OpenDesktop("Screen-saver", 0,
			false, DESKTOP_READOBJECTS | DESKTOP_WRITEOBJECTS);
			if (hDesktop != IntPtr.Zero)
			{
				WinAPI.EnumDesktopWindows(hDesktop, new
				WinAPI.EnumDesktopWindowsProc(KillScreenSaverFunc),
				IntPtr.Zero);
				WinAPI.CloseDesktop(hDesktop);
			}
			else
			{
				if (canCloseForegroundWindow)
				{
					WinAPI.PostMessage(WinAPI.GetForegroundWindow(), WM_CLOSE,
					0, 0);
				}
			}
		}

		private static bool KillScreenSaverFunc(IntPtr hWnd,
		IntPtr lParam)
		{
			if (WinAPI.IsWindowVisible(hWnd))
				WinAPI.PostMessage(hWnd, WM_CLOSE, 0, 0);
			return true;
		}
	}
}