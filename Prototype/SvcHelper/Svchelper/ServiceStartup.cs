using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Joy.Startup
{
    public class ServiceStartup : ServiceBase
    {
        static Starter starter = new Starter();
        
        protected override void OnStart(string[] args)
        {
            Start();
        }
        protected override void OnStop()
        {
            starter.Stop();
        }
        public void Start()
        {
            starter.Start();
        }
    }
}
