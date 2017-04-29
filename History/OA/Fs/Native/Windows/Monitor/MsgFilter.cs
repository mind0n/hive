using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Fs.Reflection;
using System.Collections;

namespace Fs.Native.Windows.Monitor
{
	public class MsgFilter : IMessageFilter
	{
		public delegate bool MsgProcessDelegate(ref Message m);
		public static MsgProcessDelegate OnMsgProcess;
		public static void Start()
		{
			Application.AddMessageFilter(new MsgFilter());
		}
		public bool PreFilterMessage(ref Message m)
		{
			bool rlt = false;
			Message msg = m;
			if (OnMsgProcess != null)
			{
				ArrayList rlts = ClassHelper.EnumDelegatesFrom(OnMsgProcess, delegate(Delegate omp) {
					return omp.DynamicInvoke(new object[]{msg});
				});
				foreach(object r in rlts)
				{
					if (((bool)r))
					{
						rlt = true;
					}
				}
				return rlt;
			}
			return false;
		}

	}
	/*
 			switch (m.Msg)
			{
				//WM_QUERYENDSESSION
				case (0x0011):
					m.Result = IntPtr.Zero;
					return true;
				//WM_SYSCOMAND SC_SCREENSAVE SC_MONITORPOWER
				case (0x0112):
					if (m.LParam == (IntPtr)0xF140 || m.LParam == (IntPtr)0XF170)
					{
						m.Result = IntPtr.Zero;
						return true;
					}
					else
					{
						return false;
					}
				default:
					return false;
			}
 
	 */
}
