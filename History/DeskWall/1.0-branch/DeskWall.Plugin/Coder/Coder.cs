using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dw.Module;
using Native.Monitor;

namespace Dw.Plugins
{
	public class Coder : FunctionModule
	{
		CoderForm fcf;
		//public override void Start()
		//{
		//    fcf = new CoderForm();
		//}
		public override void Stop()
		{
			fcf.AllowClose = true;
			fcf.CloseWindow();
		}
		protected void OnNotifyMenuItemClick(object sender, EventArgs e)
		{
			fcf = new CoderForm();
			fcf.ShowWindow();
		}
		public void OnPluginsLoadComplete(DeskWall sender, PluginItem plugin)
		{
			DeskWall mi = DeskWall.GetInstance();
			mi.OnKeyDown += new KeyEventHandler(HookManager_KeyDown);
			mi.OnKeyUp += new KeyEventHandler(HookManager_KeyUp);
			fcf = new CoderForm();
		}

		void HookManager_KeyUp(object sender, KeyEventArgs e)
		{
			
		}

		void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			if (KeyboardStatus.Alt && KeyboardStatus.Shift && e.KeyCode == Keys.C)
			{
				fcf.ShowWindow();
			}
		}

		public Coder()
		{
			IsSingleton = false;
			Name = "Code Generator";
			DeskWall mi = DeskWall.GetInstance();
			mi.OnPluginsLoadComplete += OnPluginsLoadComplete;
		}
	}
}
