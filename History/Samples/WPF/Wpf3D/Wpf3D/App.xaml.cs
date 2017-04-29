using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using FsDelta.UI.WPF;
namespace Wpf3D
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
		}
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Test(0);
		}
		private void Test(int choice)
		{
			if (choice == 1)
			{
				MainWindow MainWindow = new MainWindow(true);
				MainWindow.Show();
			}
			else if (choice == 2)
			{
				Test t = new Test();
				t.Show();
			}
			else
			{
				MotionWindow mw = new MotionWindow();
				mw.Show();
			}
		}
	}
}
