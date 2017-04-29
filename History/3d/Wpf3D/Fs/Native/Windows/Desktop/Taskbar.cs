using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Fs.Native.Windows.Desktop
{
	public class Taskbar
	{
		[DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
		public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

		#region Struct RECT
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		#endregion

		#region Struct APPBARDATA
		[StructLayout(LayoutKind.Sequential)]
		public struct APPBARDATA
		{
			public int cbSize;
			public IntPtr hWnd;
			public int uCallbackMessage;
			public int uEdge;
			public RECT rc;
			public IntPtr lParam;
		}
		#endregion

		#region Struct ABMsg
		enum ABMsg : int
		{
			ABM_NEW = 0,
			ABM_REMOVE = 1,
			ABM_QUERYPOS = 2,
			ABM_SETPOS = 3,
			ABM_GETSTATE = 4,
			ABM_GETTASKBARPOS = 5,
			ABM_ACTIVATE = 6,
			ABM_GETAUTOHIDEBAR = 7,
			ABM_SETAUTOHIDEBAR = 8,
			ABM_WINDOWPOSCHANGED = 9,
			ABM_SETSTATE = 10
		}
		#endregion

		#region Struct ABEdge
		enum ABEdge : int
		{
			ABE_LEFT = 0,
			ABE_TOP,
			ABE_RIGHT,
			ABE_BOTTOM
		}
		#endregion

		#region Enum ABState
		enum ABState : int
		{
			ABS_MANUAL = 0,
			ABS_AUTOHIDE = 1,
			ABS_ALWAYSONTOP = 2,
			ABS_AUTOHIDEANDONTOP = 3,
		}
		#endregion

		#region Enum TaskBarEdge
		public enum TaskBarEdge : int
		{
			Bottom
			,
			Top
			,
			Left
			, Right
		}
		#endregion

		public double Height;
		public TaskBarEdge Position;
		public bool AutoHide;
		public static void AdjustSizeAgainstTaskBar(Control ctrl)
		{
			double w = Convert.ToDouble(ctrl.Width);
			double h = Convert.ToDouble(ctrl.Height);
			AdjustSizeAgainstTaskBar(ref w, ref h);
			ctrl.Width = Convert.ToInt32(w);
			ctrl.Height = Convert.ToInt32(h);
		}
		public static void AdjustSizeAgainstTaskBar(ref double width, ref double height)
		{
			Taskbar tb = new Taskbar();
			if (tb.Position == TaskBarEdge.Bottom || tb.Position == TaskBarEdge.Top)
			{
				height -= tb.Height;
			}
			else if (tb.Position == TaskBarEdge.Left || tb.Position == TaskBarEdge.Right)
			{
				width -= tb.Height;
			}
		}

		#region GetTaskBarInfo
		/// <summary>
		///  Method returns information about the Window's TaskBarEdge.
		/// </summary>
		/// Location of the TaskBar
		/// (Top,Bottom,Left,Right).
		/// Height of the TaskBarEdge.
		/// AutoHide property of the TaskBarEdge.
		public static Taskbar GetTaskBarInfo(Taskbar tb)
		{
			TaskBarEdge taskBarEdge;
			double height = 0;
			bool autoHide = false;
			APPBARDATA abd = new APPBARDATA();
			taskBarEdge = TaskBarEdge.Bottom;
			autoHide = false;

			#region TaskBar & Height
			uint ret = SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);
			switch (abd.uEdge)
			{
				case (int)ABEdge.ABE_BOTTOM:
					taskBarEdge = TaskBarEdge.Bottom;
					height = abd.rc.bottom - abd.rc.top;
					break;
				case (int)ABEdge.ABE_TOP:
					taskBarEdge = TaskBarEdge.Top;
					height = abd.rc.bottom;
					break;
				case (int)ABEdge.ABE_LEFT:
					taskBarEdge = TaskBarEdge.Left;
					height = abd.rc.right - abd.rc.left;
					break;
				case (int)ABEdge.ABE_RIGHT:
					taskBarEdge = TaskBarEdge.Right;
					height = abd.rc.right - abd.rc.left;
					break;

			}
			#endregion

			#region TaskBar AutoHide Property
			abd = new APPBARDATA();
			uint uState = SHAppBarMessage((int)ABMsg.ABM_GETSTATE, ref abd);
			switch (uState)
			{
				case (int)ABState.ABS_ALWAYSONTOP:
					autoHide = false;
					break;
				case (int)ABState.ABS_AUTOHIDE:
					autoHide = true;
					break;
				case (int)ABState.ABS_AUTOHIDEANDONTOP:
					autoHide = true;
					break;
				case (int)ABState.ABS_MANUAL:
					autoHide = false;
					break;
			}
			#endregion
			if (tb == null)
			{
				tb = new Taskbar(taskBarEdge, height, autoHide);
			}
			else
			{
				tb.Height = height;
				tb.AutoHide = autoHide;
				tb.Position = taskBarEdge;
			}
			return tb;
		}
		#endregion
		public Taskbar()
		{
			Taskbar.GetTaskBarInfo(this);
		}
		public Taskbar(TaskBarEdge pos, double height, bool autohide)
		{
			init(pos, height, autohide);
		}
		protected void init(TaskBarEdge pos, double height, bool autohide)
		{
			Height = height;
			Position = pos;
		}
	}
}