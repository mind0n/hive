using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Dw.Collections;
using Dw.Module;
using Fs.Xml;
using Fs;

namespace Dw.Plugins
{
	public class NotifyMenuItem
	{
		
	}
	public class Notifier : FunctionModule
	{
		private static new bool IsReady = false;
		private static new Notifier _instance = new Notifier();
		//private static List<MenuItem> MenuItems;
		private static System.Windows.Forms.Menu.MenuItemCollection MenuItems;
		protected static NotifierForm NtForm;
		public static bool IsIconVisible
		{
			get
			{
				return NtForm.NtIcon.Visible;
			}
		}

		public static Notifier GetInstance()
		{
			if (_instance != null)
			{
				return _instance;
			}
			else
			{
				_instance = new Notifier();
				return _instance;
			}
		}
		public static void ShowBalloon(string text, string title, int timeout, ToolTipIcon icon)
		{
			NtForm.NtIcon.ShowBalloonTip(timeout, title, text, icon);
		}
		public static bool AdjustMenuItem()
		{
			return AdjustMenuItem(0);
		}
		public static bool AdjustMenuItem(int index)
		{
			return AdjustMenuItem(new MenuItem("-"));
		}
		public static MenuItem AdjustMenuItem(MenuMerge mergeType, string text, EventHandler onClick)
		{
			if (!string.IsNullOrEmpty(text))
			{
				MenuItem mi = new MenuItem(mergeType, 100, Shortcut.None, text, onClick, null, null, null);
				if (AdjustMenuItem(mi))
				{
					return mi;
				}
				else
				{
					return null;
				}
			}
			return null;
		}
		public static bool AdjustMenuItem(MenuItem item)
		{
			return AdjustMenuItem(item, 0);
		}
		public static bool AdjustMenuItem(MenuItem item, int index)
		{
			if (item == null)
			{
				return false;
			}
			if (index < 0)
			{
				MenuItems.Add(item);
			}
			else
			{
				MenuItems.Add(index, item);
			}
			if (IsReady)
			{
				UpdateMenuItem();
			}
			return true;
		}
		public static void UpdateMenuItem()
		{
			//ContextMenu cm = new ContextMenu();
			////int max = 0;
			////System.Windows.Forms.Menu.MenuItemCollection mc = NtForm.NtIcon.ContextMenu.MenuItems;
			//foreach (MenuItem i in MenuItems)
			//{
			//    if (i != null)
			//    {
			//        cm.MenuItems.Add(i);
			//    }
			//}
			
			//NtForm.NtIcon.ContextMenu = cm;
			System.Windows.Forms.Menu.MenuItemCollection mc = NtForm.NtIcon.ContextMenu.MenuItems;
			mc.Clear();
			foreach (MenuItem mi in MenuItems)
			{
				mc.Add(0, mi);
			}

		}
		public override void Stop()
		{
			NtForm.CloseWindow();
			while (true) ;
		}
		public void OnPluginsLoad(DeskWall sender, PluginItem plugin)
		{
			XReader xr = new XReader(ConfigFilePath);
			bool display = (xr["Plugin"]["Style"]["$Display"].Value == "true");
			//NtForm.BgImgDir = BaseDir + "\\" + xr["Plugin"]["Style"]["$BackgroundDir"].Value;
			NtForm.NtIcon.Visible = display;
			NtForm.NtIcon.Icon = new Icon(BaseDir + "\\tray.ico");

			Application.Run(NtForm);
		}
		protected bool SwitchIconVisibility(bool? visible)
		{
			if (visible == null)
			{
				NtForm.NtIcon.Visible = NtForm.NtIcon.Visible == true ? false : true;
			}
			else
			{
				NtForm.NtIcon.Visible = visible == true;
			}
			return NtForm.NtIcon.Visible;
		}

		private Notifier()
		{
			IsSingleton = true;
			IsReady = false;
			Name = "Notifier";
			NtForm = new NotifierForm(this);
			NtForm.NtContainer = new System.ComponentModel.Container();
			NtForm.NtIcon = new System.Windows.Forms.NotifyIcon(NtForm.NtContainer);
			NtForm.NtIcon.ContextMenu = new ContextMenu();
			MenuItems = new Menu.MenuItemCollection(NtForm.NtIcon.ContextMenu); //new List<MenuItem>();
			NtForm.SwitchIconVisibilityDelegate += SwitchIconVisibility;
			DeskWall mi = DeskWall.GetInstance();
			mi.OnPluginsLoadComplete += OnPluginsLoad;
			//mi.OnPluginsLoadComplete += OnPluginsLoadComplete;
			//mi.OnAdjustNotifyIconMenuItem += AdjustMenuItem;
			MenuItem miHide = new MenuItem(MenuMerge.Add, 10000, Shortcut.None, "Hide Tray Icon", delegate(object sender, EventArgs ea)
			{
				SwitchIconVisibility(false);
			}, null, null, null);
			MenuItem miExit = new MenuItem(MenuMerge.Add, 10001, Shortcut.None, "Exit DeskWall", delegate(object sender, EventArgs ea)
			{
				SwitchIconVisibility(false);
				DeskWall m = DeskWall.GetInstance();
				m.TryExit();
				//if (BelongModule.IsRegisted)
				//{
				//    BelongModule.MainModule.TryExit(1000);
				//}
			}, null, null, null);
			AdjustMenuItem(-1);
			AdjustMenuItem(miHide, -1);
			AdjustMenuItem(miExit, -1);
		}
	}
}
