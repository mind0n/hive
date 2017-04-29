using System;
using System.Collections.Generic;
using System.Text;
using Dw.Module;
using Dw.Collections;
using Microsoft.Win32;

namespace Dw.Plugins
{
	public class WallRegistry : FunctionModule
	{
		public WallRegistry()
		{
			Name = "RegistryManager";
			IsSingleton = true;
			DeskWall mi = DeskWall.GetInstance();
			mi.OnPluginsLoadComplete += OnPluginsLoadComplete;
		}
		public void OnPluginsLoadComplete(DeskWall sender, PluginItem plugin)
		{
			RegistryReader rcu = new RegistryReader(Registry.CurrentUser);
			rcu = (RegistryReader)rcu.GetChildByPath(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", '\\');
			rcu.SetValue("ProxyOverride", "*.metlife*.com;<local>");
		}

	}
}
