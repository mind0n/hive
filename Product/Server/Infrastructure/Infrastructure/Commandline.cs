using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
	public class Commandline
	{
		private static ServiceStartup startup = new ServiceStartup();

		public static void Handle(string[] args)
		{
			if (args != null && args.Length > 0)
			{
				if (string.Equals(args[0], "-i"))
				{
					var l = Assembly.GetCallingAssembly().Location;
					var v = args.Length > 1 ? args[1] : "a_" + l.PathLastName(false);
					Process.Start(new ProcessStartInfo("sc", $"create {v} binPath= {l}"));
					return;
				}
			}

			if (Debugger.IsAttached)
			{
				startup.Start();
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] { startup };
				ServiceBase.Run(ServicesToRun);
			}

		}
	}

	public class ServiceStartup : ServiceBase
	{
		protected Launcher starter { get; set; }
		protected override void OnStart(string[] args)
		{
			Start();
		}
		protected override void OnStop()
		{
			starter.Shutdown();
			log.i("Domain unloaded");
			log.Shutdown();
		}
		public void Start()
		{
			starter = new FolderLauncher();
			starter.Launch();
		}
	}

}
