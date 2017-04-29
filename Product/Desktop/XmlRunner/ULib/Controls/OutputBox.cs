using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ULib.Controls
{
	public partial class OutputBox : UserControl, IOutput
	{
		static string Filename = "Output.log";
		public bool IsAutoSave = false;
		public string SaveFile = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Filename);
        private bool hideCmdBar;

        public bool HideCmdBar
        {
            get { return hideCmdBar; }
            set
            {
                hideCmdBar = value;
                pnCmd.Visible = !value;
            }
        }
		public OutputBox()
		{
            hideCmdBar = true;
            InitializeComponent();
            Load += new EventHandler(OutputBox_Load);
			web.Url = new Uri("about:blank");
			web.Document.Write("<html><body style='font:12px arial; background:white;'></body></html>");
			if (File.Exists(SaveFile))
			{
				File.Move(SaveFile, string.Concat(SaveFile, "_", Guid.NewGuid().ToString().Replace("-", ""), ".log"));
			}
		}
		public void Clear()
		{
			this.Invoke((MethodInvoker)delegate
			{
				web.Document.Body.InnerHtml = "";
			});
		}
		public void WriteErrorMsg(string msg, params string[] args)
		{
			this.Invoke((MethodInvoker)delegate
			{
				WriteMsg(msg, false, "darkred", true, args);
			});
		}
		public void WriteSuccessMsg(string msg, params string[] args)
		{
			this.Invoke((MethodInvoker)delegate
			{
				WriteMsg(msg, false, "green", true, args);
			});
		}
		public void WriteMsg(string msg, bool hideTimestamp = false, string color = "black", bool breakLine = true, params string[] args)
		{
			this.Invoke((MethodInvoker)delegate
			{
				string webmsg = msg.Replace("\r\n", "<br />");
                if (hideTimestamp)
                {
                    web.Document.Body.InnerHtml += string.Format("<font style='color:{0}'></font><font style='color:{0}'>{1}</font>", color, string.Format(webmsg, args));
                }
                else
                {
                    web.Document.Body.InnerHtml += string.Format("<font style='color:{0}'>&nbsp;{2}&nbsp;</font><font style='color:{0}'>{1}</font>", color, string.Format(webmsg, args), DateTime.Now.ToLongTimeString());
                }
				if (breakLine)
				{
					web.Document.Body.InnerHtml += "<br />";
				}
				web.Document.Body.ScrollTop = web.Document.Body.ScrollRectangle.Height;
				if (IsAutoSave)
				{
					string logmsg = string.Format(msg, args);
					logmsg = logmsg.Replace("&nbsp;", " ");
					logmsg = logmsg.Replace("<br />", "\r\n");
                    if (!hideTimestamp)
                    {
                        if (!File.Exists(SaveFile))
                        {
                            File.WriteAllText(SaveFile, string.Concat(DateTime.Now.ToString(), "\r\n"));
                        }
                        File.AppendAllText(SaveFile, string.Concat(DateTime.Now.ToString(), "\t", logmsg, "\r\n"));
                    }
                    else
                    {
                        File.AppendAllText(SaveFile, string.Concat(logmsg, "\r\n"));
                    }
				}
			});
		}
		private void OutputBox_Load(object sender, EventArgs e)
		{
            pnCmd.Visible = !hideCmdBar;
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
	}
}
