using System;
using System.Collections.Generic;
using System.Text;
using Dw.Module;
using System.Windows.Forms;
using Native.Monitor;

namespace Dw.Plugins
{
	public class IdGen : FunctionModule
	{
		public IdGen()
		{
			Name = "GUIDGenerator";
			IsSingleton = true;
			HookManager.KeyDown += new KeyEventHandler(HookManager_KeyDown);
			HookManager.KeyUp += new KeyEventHandler(HookManager_KeyUp);
			MenuItem NotifierMenuItem = Notifier.AdjustMenuItem(System.Windows.Forms.MenuMerge.Add, "Generate New GUID", delegate(object sender, EventArgs e)
			{
				Start();
			});
		}

		void HookManager_KeyUp(object sender, KeyEventArgs e)
		{
			
		}

		void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.G && KeyboardStatus.Shift && KeyboardStatus.Alt)
			{
				Start();
			}
		}
		public override void Start()
		{
			string id = Guid.NewGuid().ToString();
			Clipboard.SetData(DataFormats.Text, id);
			if (Notifier.IsIconVisible)
			{
				Notifier.ShowBalloon(id + " has been copied to clipboard.", "Global Unique Identifier Generator", 1000, ToolTipIcon.Info);
			}
			else
			{
				MessageBox.Show(id + " has been copied to clipboard.", "Global Unique Identifier Generator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}
}
