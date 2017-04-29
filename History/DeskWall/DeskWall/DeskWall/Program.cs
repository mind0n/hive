using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Fs.Core;
using Dw.Module;
using Fs.Xml;
using System.Threading;
using Fs;

namespace Dw
{
	static class Program
	{
		static DeskWall m;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        [STAThread]
		static void Main()
		{
            Logger.Log("<<< Program Started >>>\n");
            MainForm MainWindow;
            m = DeskWall.GetInstance();
			m.ConfigFilePath = AppDomain.CurrentDomain.BaseDirectory;
			m.ConfigFilename = "Wall.config";
			m.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new MainForm();
            MainWindow.BelongModule = m;
            m.MainWindow = MainWindow;
            if (MainWindow != null)
            {
                MainWindow.Visible = false;
                MainWindow.ShowInTaskbar = false;
                try
                {
                    m.SetAutoRun();
                    ((MainForm)MainWindow).OnMainFormLoaded += m.Init;
                    //m.RegistWindow("MainWindow", MainWindow, m);
                    {
						XReader xr = m.ConfigReader;
                        MainWindow.BgImgDir = xr["Config"]["Style"]["$BackgroundDir"].Value;
                    }
                    Application.Run(MainWindow);
                }
                catch (Exception err)
                {
                    Exceptions.LogOnly(err);
                }
            }
            Logger.Log("<<< Program Ended >>>\n");
        }
	}
}