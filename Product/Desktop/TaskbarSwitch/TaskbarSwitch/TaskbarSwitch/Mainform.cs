using Joy.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarSwitch
{
    public partial class Mainform : Form
    {
        Taskbar taskbar;
        public Mainform()
        {
            InitializeComponent();
            taskbar = new Taskbar();
            Load += Mainform_Load;
        }

        void Mainform_Load(object sender, EventArgs e)
        {
            var s = taskbar.GetTaskbarState();
            taskbar.SetTaskbarState(s == Taskbar.AppBarStates.AutoHide ? Taskbar.AppBarStates.AlwaysOnTop : Taskbar.AppBarStates.AutoHide);
            Close();
        }
    }
}
