using System;
using System.Diagnostics;
using System.Reflection;
using Logger;
using Infrastructure;
using Settings;

namespace PluginB
{
    public class Entry
    {
	    public static string Name { get; } = "Addin B";

	    private static OwinAdapter adapter;

	    public static void Start()
	    {
			var cfg = new FileSetting(new {name="owin.log.json"});
			adapter = new OwinAdapter(cfg.Instance);
			adapter.Start();
			log.i("Plugin B Started");
		}

		public static void Stop()
		{
			adapter.Stop();
			log.Shutdown();
		}
	}
}
