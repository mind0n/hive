using Joy.Core;
using Joy.Windows;
using System;
using System.Windows.Forms;
using Joy.Windows.Controls;
using TestRoom.Testing;
using TestRoom.VirtualDesktop;
using TestRoom.Wcf;

namespace TestRoom
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void bTestVirtualDesktop_Click(object sender, EventArgs e)
        {
            LaunchForm<Deskform>();
        }

        private void LaunchForm<T>() where T : Form, new()
        {
            WindowState = FormWindowState.Minimized;
            var f = new T();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
            f.Activate();
        }

        private void bTestLoaderService_Click(object sender, EventArgs e)
        {
            LaunchForm<ServiceForm>();
        }

        private void bWcf_Click(object sender, EventArgs e)
        {
            LaunchForm<WcfForm>();
        }

        private void bTestCfg_Click(object sender, EventArgs e)
        {
            LaunchForm<Testform>();
        }

        private void bShowTaskbar_Click(object sender, EventArgs e)
        {
            var tb = new Taskbar();
            var s = tb.GetTaskbarState();

            tb.SetTaskbarState(s == Taskbar.AppBarStates.AutoHide ? Taskbar.AppBarStates.AlwaysOnTop : Taskbar.AppBarStates.AutoHide);
        }
    }

}
