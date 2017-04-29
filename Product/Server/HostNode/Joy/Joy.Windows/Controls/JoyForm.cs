using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Joy.Windows.Controls
{
    public partial class JoyForm : Form
    {
        public JoyForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
        }

        protected void Locate(int x, int y, int mo = 0)
        {
            var a = Screen.PrimaryScreen.WorkingArea;
            int l = x, t = y;
            if ((mo & 2) == 2)
            {
                l = a.Width - x - Width;
            }
            if ((mo & 1) == 1)
            {
                t = a.Height - y - Height;
            }
            Left = l;
            Top = t;
        }
    }
}
