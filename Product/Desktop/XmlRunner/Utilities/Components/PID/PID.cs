using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ULib.Forms;
using ULib;

namespace Utilities.Components
{
    public partial class PID : EmbededForm
    {
        HtmlDocument d;
        public PID()
        {
            InitializeComponent();
            d = history.Document;
            Load += new EventHandler(PID_Load);
        }

        void PID_Load(object sender, EventArgs e)
        {
            tCmd.Text = Process.GetCurrentProcess().Id.ToString();
        }

        private void Output(int paddingLeft, string message, Color color, params string[] args)
        {
            string msg = message;
            if (args != null && args.Length > 0)
            {
                msg = string.Format(message, args);
            }
            d.Write("<table><tr>");
            for (int i = 0; i < paddingLeft; i++)
            {
                d.Write("<td style='width:32px;'></td>");
            }
            d.Write("<td><div style='font:16px arial; color:" + color.Name + ";'>" + msg + "</div></td>");
            d.Write("</tr></table>");
            txt.Text = history.DocumentText;
        }

        private void bAnalyze_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tCmd.Text))
            {
                int padding = 0;
                try
                {
                    Process p = Process.GetProcessById(Convert.ToInt32(tCmd.Text));
                    Output(padding, DateTime.Now.ToLongTimeString(), Color.Blue);
                    while (p != null)
                    {
                        Output(padding, p.ProcessName, Color.Black);
                        Output(padding, p.Modules[0].FileName, Color.Brown);
                        padding++;
                        p = p.Parent();
                    }
                }
                catch (Exception error)
                {
                    Output(padding - 1, error.Message, Color.Red);
                }
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            d.Body.InnerHtml = "";
        }

    }
}
