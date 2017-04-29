using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dw.Collections;
using Dw.Module;
using Dw.Window;
using Fs.Xml;
using Native.Monitor;
using Fs.IO;
using System.IO;
using Fs;

namespace Dw.Plugins
{
	public partial class NotifierForm : PluginForm
	{
		
		public delegate bool SwitchHandler(bool? switch0n);
		public SwitchHandler SwitchIconVisibilityDelegate;
		public new Notifier BelongModule;
		public StrongArray MenuItems = new StrongArray();
		public System.Windows.Forms.NotifyIcon NtIcon;
		public System.ComponentModel.IContainer NtContainer;

		protected bool? SwitchIconVisibility(bool? visible)
		{
			if (SwitchIconVisibilityDelegate != null)
			{
				return SwitchIconVisibilityDelegate(visible);
			}
			return null;
		}
		protected void HookManager_KeyUp(object sender, KeyEventArgs e) { }
		protected void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Home && KeyboardStatus.Shift && KeyboardStatus.Alt)
			{
				SwitchIconVisibility(null);
			}
		}
		protected void NtIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SwitchIconVisibility(false);
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			NtIcon.MouseClick += new MouseEventHandler(NtIcon_MouseClick);
			HookManager.KeyDown += new KeyEventHandler(HookManager_KeyDown);
			HookManager.KeyUp += new KeyEventHandler(HookManager_KeyUp);
			base.OnLoad(e);
		}
		protected override void OnShown(EventArgs e)
		{
			BelongModule.IsReady = true;
			base.OnShown(e);
		}
		public NotifierForm(Notifier belongModule)
			: base()
		{
			InitializeComponent();
			Visible = false;
			Opacity = 0;
			ShowInTaskbar = false;
			WindowState = FormWindowState.Minimized;
			BelongModule = belongModule;
		}
	}
}