using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dw.Module;
using Fs;
using System.Drawing;

namespace Dw.Window
{
	public class PluginForm : System.Windows.Forms.Form
	{
		public delegate void ParamDelegateCallback(params object[] pars);
		public delegate void DelegateCallback();
		protected delegate void DelegateDisposeCallback(bool disposing);

		public DelegateCallback CloseWindow;
		public DelegateCallback ShowWindow;
		public DelegateCallback HideWindow;
		public DelegateCallback ActivateWindow;
		public FunctionModule BelongModule;
		public int BorderWidth;
		public int TitlebarHeight;
		public bool IsFormLoaded = false;


		protected Point ClickedPoint;
		protected bool IsDragging = false;
		//protected bool IsReady = false;
		//private new bool IsDisposed = false;

		public PluginForm()
		{
			BorderWidth = (this.Width-this.ClientSize.Width)/2;
			TitlebarHeight = this.Height - this.ClientSize.Height - BorderWidth * 2;
			CloseWindow += CloseWindow_Delegate;
			ShowWindow += ShowWindow_Delegate;
			HideWindow += HideWindow_Delegate;
			ActivateWindow += ActivateWindow_Delegate;
			ShowWindow += delegate()
			{
				ActivateWindow_Delegate();
			};
			Disposed += new EventHandler(PluginForm_Disposed);
		}
		protected void MakeDelegate(DelegateCallback callback)
		{
			DelegateCallback delegateCallback = new DelegateCallback(callback);
			try
			{
				if (Handle != IntPtr.Zero)
				{
					if (InvokeRequired)
					{
						Invoke(delegateCallback);
					}
					else
					{
						delegateCallback.Invoke();
					}
				}
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}

		}
		protected void HideWindow_Delegate()
		{
			MakeDelegate(Hide);
		}
		protected void PluginForm_Disposed(object sender, EventArgs e)
		{
            IsFormLoaded = false;
		}
		protected void ShowWindow_Delegate()
		{
			MakeDelegate(Show);
		}
		protected void CloseWindow_Delegate()
		{
			MakeDelegate(Close);
		}
		protected void ActivateWindow_Delegate()
		{
			MakeDelegate(Activate);
		}
	}
}
