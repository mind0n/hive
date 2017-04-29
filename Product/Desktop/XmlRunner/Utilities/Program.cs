using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ULib.Forms;

namespace Utilities
{
    /*
        job runned status
        db bak add time
        add db name into connection
        run jobs separatly
     */
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
			Application.Run(new StartupForm());
		}
	}
}
