using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace ULib.Core.NativeSystem
{
	/// <summary>
	/// A class that manages a global low level keyboard hook
	/// </summary>
    public class Native
    {
        private static Native instance = new Native();

        public static Native Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Native();
                }
                return instance;
            }
        }

        #region Constant, Structure and Delegate Definitions
        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

        public struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        #endregion

        #region Instance Variables
        /// <summary>
        /// The collections of keys to watch for
        /// </summary>
        public List<Keys> HookedKeys = new List<Keys>();
        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        IntPtr hhook = IntPtr.Zero;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when one of the hooked keys is pressed
        /// </summary>
        public event KeyEventHandler KeyDown;
        /// <summary>
        /// Occurs when one of the hooked keys is released
        /// </summary>
        public event KeyEventHandler KeyUp;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Native"/> class and installs the keyboard hook.
        /// </summary>
        public Native()
        {
            Hook();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Native"/> is reclaimed by garbage collection and uninstalls the keyboard hook.
        /// </summary>
        ~Native()
        {
            Unhook();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Installs the global hook
        /// </summary>
        public void Hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void Unhook()
        {
            UnhookWindowsHookEx(hhook);
        }

        /// <summary>
        /// The callback for the keyboard hook
        /// </summary>
        /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The keyhook event information</param>
        /// <returns></returns>
        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                KeyEventArgs keyArgs = new KeyEventArgs(key);
                if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                {
                    if (!SetKeyStatus(lParam.vkCode, true))
                    {
                        KeyDown(this, keyArgs);
                    }
                }
                else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                {
                    if (!SetKeyStatus(lParam.vkCode, false))
                    {
                        KeyUp(this, keyArgs);
                    }
                }
                if (keyArgs.Handled)
                {
                    return 1;
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }

        private static bool SetKeyStatus(int key, bool status)
        {
            Debug.WriteLine(key);
            if (key == 164)
            {
                KeyboardStatus.Alt = status;
            }
            else if (key == 160)
            {
                KeyboardStatus.Shift = status;
            }
            else if (key == 162)
            {
                KeyboardStatus.Ctrl = status;
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region DLL imports
        /// <summary>
        /// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
        /// </summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        #endregion
    }
    public class KeyboardStatus
    {
        public static bool Shift = false;
        public static bool Ctrl = false;
        public static bool Alt = false;
        //public static bool A = false;
        //public static bool B = false;
        //public static bool C = false;
        //public static bool D = false;
        //public static bool E = false;
        //public static bool F = false;
        //public static bool G = false;
        //public static bool H = false;
        //public static bool I = false;
        //public static bool J = false;
        //public static bool K = false;
        //public static bool L = false;
        //public static bool M = false;
        //public static bool N = false;
        //public static bool O = false;
        //public static bool P = false;
        //public static bool Q = false;
        //public static bool R = false;
        //public static bool S = false;
        //public static bool T = false;
        //public static bool U = false;
        //public static bool V = false;
        //public static bool W = false;
        //public static bool X = false;
        //public static bool Y = false;
        //public static bool Z = false;
        //public static bool Slash = false;
        //public static bool Comma = false;
        //public static bool Dot = false;
        //public static bool Quotes = false;
        //public static bool BackSlash = false;
        //public static bool OBrace = false;
        //public static bool CBrace = false;
        //public static bool One = false;
        //public static bool Two = false;
        //public static bool Three = false;
        //public static bool Four = false;
        //public static bool Five = false;
        //public static bool Six = false;
        //public static bool Seven = false;
        //public static bool Eight = false;
        //public static bool Nine = false;
        //public static bool Zero = false;
        //public static bool Wave = false;
        //public static bool Plus = false;
        //public static bool Minus = false;
        //public static bool Backspace = false;
    }
}
