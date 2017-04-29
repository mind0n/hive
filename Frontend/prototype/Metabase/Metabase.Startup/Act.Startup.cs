using Act.Core;
using Act.Infrastructure;
using Foundation;
using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace Act
{
    class Startup
    {
        static void Main(string[] args)
		{
			log.Register(new ConfigLogManager());
			Cmd.Handle(args, ()=>
			{
				ServiceStartup ss = new ServiceStartup();
				if (!Cmd.Exists("s") || Debugger.IsAttached)
				{
					ss.Begin();
					log.w("Press ENTER to unload module(s) ...");
					Console.ReadLine();
					ss.End();
					log.w("Press any key to exit ...");
					Console.ReadKey();
				}
				else
				{
					ServiceBase[] svc = { ss };
					ServiceBase.Run(svc);
				}
			});

		}
    }
}
