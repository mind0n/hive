using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestRoom.VirtualDesktop
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            Process.Start("cmdCreateService.bat", AppDomain.CurrentDomain.BaseDirectory);
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Process.Start("cmdStartService.bat", "a");
        }
    }
}
