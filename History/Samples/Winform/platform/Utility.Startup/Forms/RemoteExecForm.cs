using System;
using ULib.Winform.Controls;
using System.Management;

namespace Utility.Startup.Forms
{
    public partial class RemoteExecForm : ScreenRegion
    {
        public RemoteExecForm()
        {
            InitializeComponent();
            Load += new EventHandler(UrlToolForm_Load);
        }

        private void UrlToolForm_Load(object sender, EventArgs e)
        {
        }
        private void RemoteExec()
        {
            var processToRun = new[] { "notepad.exe" };
            var connection = new ConnectionOptions();
            connection.Username = "celine";
            connection.Password = "nothing";
            connection.Authentication = AuthenticationLevel.Connect;
            var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", "192.168.9.101"), connection);
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            wmiProcess.InvokeMethod("Create", processToRun);
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            RemoteExec();
        }
    }
}
