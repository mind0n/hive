using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OAWidgets.Widget;
using System.Reflection;

namespace OA
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//Main mfrm = BasicWidget.Create<Main>(AppDomain.CurrentDomain.BaseDirectory);
	
			Application.Run(new MainForm());
		}
	}
}
