using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace Joy.Startup
{
    public static class Program
    {
        private static ServiceStartup startup = new ServiceStartup();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (string.Equals(args[0], "-i"))
                {
                    var s = AppDomain.CurrentDomain.BaseDirectory;
                    Process.Start(new ProcessStartInfo("sc", string.Format("create SvcHelper binPath= {0}startup.exe", s)));
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
}
