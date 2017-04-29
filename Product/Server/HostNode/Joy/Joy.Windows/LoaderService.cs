using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Joy.Core;

namespace Joy.Windows
{
    public partial class LoaderService : ServiceBase
    {
        private static FileCfg<LoaderConfig> config = new FileCfg<LoaderConfig>();
        public LoaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        public static void StartService(bool isDebug = false)
        {
            //new Thread(new ThreadStart(delegate
            //{
                if (!isDebug)
                {
                    //var cmd = AppDomain.CurrentDomain.BaseDirectory + "TestVirtualDesktop.exe";
                    var cmd = config.Instance.ToString();
                    ApplicationLoader.PROCESS_INFORMATION procInfo;
                    ApplicationLoader.StartProcessAndBypassUAC(cmd, out procInfo);
                }
                else
                {
                    Process.Start(config.Instance.Fullname, config.Instance.Args);
                }
            //})).Start();
        }

        protected override void OnStop()
        {
            
        }
    }

    public class LoaderConfig
    {
        private string directory;
        public string Filename;

        public string Directory
        {
            get
            {
                if (string.IsNullOrEmpty(directory))
                {
                    directory = AppDomain.CurrentDomain.BaseDirectory;
                }
                return directory;
            }
            set
            {
                directory = value;
            }
        }

        public string Args;

        public string Fullname
        {
            get
            {
                return string.Concat(Directory, Filename);
            }
        }
        public override string ToString()
        {
            if (Filename.StartsWith("\\"))
            {
                Filename = Filename.Substring(1);
            }
            if (!Directory.EndsWith("\\"))
            {
                Directory += "\\";
            }

            return string.Concat("\"", Directory, Filename, "\" ", Args);
        }
    }
}
