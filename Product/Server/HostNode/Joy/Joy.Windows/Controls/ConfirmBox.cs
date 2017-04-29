using Joy.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Joy.Windows.Controls
{
    public partial class ConfirmBox : Form
    {
        public string Content
        {
            get
            {
                return tinput.Text;
            }
        }
        public ConfirmBox()
        {
            InitializeComponent();
        }

        public void Init(string hint, string caption = "Confirm")
        {
            lbhint.Text = hint;
            this.Text = caption;
        }
        public static T Show<T>(string hint, Control parent = null, string caption = "请输入参数")
        {
            if (parent != null)
            {
                T rlt = default(T);
                parent.Invoke((MethodInvoker)delegate
                {
                    rlt = ShowDialogInternal<T>(hint, caption);
                });
                return rlt;
            }
            else
            {
                return ShowDialogInternal<T>(hint, caption);
            }
        }

        private static T ShowDialogInternal<T>(string hint, string caption)
        {
            var box = new ConfirmBox();
            box.Init(hint, caption);
            var r = box.ShowDialog();
            if (r == DialogResult.OK)
            {
                var v = box.Content;
                try
                {
                    return (T)Convert.ChangeType(v, typeof(T));
                }
                catch (Exception ex)
                {
                    Error.Handle(ex);
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
