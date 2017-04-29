using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib.Forms
{
    public partial class MsgForm : Form
    {
        protected string Message { get; set; }
        public MsgForm(string msg)
        {
            InitializeComponent();
            lbMsg.Text = msg;
        }

        public static DialogResult Show(string caption, string message)
        {
            MsgForm f = new MsgForm(message);
            f.Text = caption;
            return f.ShowDialog();
        }
    }
}
