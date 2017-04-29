using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ULib.Native
{
	public partial class Native
	{
		public const int MF_BYPOSITION = 0x400;

		[DllImport("User32")]
		public static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

		[DllImport("User32")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("User32")]
		public static extern int GetMenuItemCount(IntPtr hWnd);

        public static void DisableCloseButton(IntPtr handle)
        {
            IntPtr hMenu = Native.GetSystemMenu(handle, false);
            int menuItemCount = Native.GetMenuItemCount(hMenu);
            Native.RemoveMenu(hMenu, menuItemCount - 1, Native.MF_BYPOSITION);
        }
	}
}
