using Portal.Winform.Components;
using Portal.Winform.Controls;
using System;
using System.Windows.Forms;

namespace Portal.Winform
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
			//Application.Run(new WebForm("http://localhost:1105/Testing/3js/Test3JS.aspx", "storage", new StorageManageService()));
			Application.Run(new WebForm("http://localhost:1105/sm/LoadManageView", "storage", new StorageManageService()));
            //Application.Run(new ManageForm());
        }
    }
}
