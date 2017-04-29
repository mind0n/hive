using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrayUpdator
{
	public partial class Mainform : Form
	{
		bool isDone;
		string ipurl = "http://ddns.oray.com/checkip";
		string updateurl = "http://markyuanmj:332211@ddns.oray.com/ph/update?hostname=ready0racle.imzone.in&myip=";
		public Mainform()
		{
			InitializeComponent();
			Load += Mainform_Load;
			web.DocumentCompleted += web_DocumentCompleted;
		}

		void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			this.ControlBox = false;
			if (!isDone)
			{
				string content = web.Document.Body.InnerText;
				if (!string.IsNullOrEmpty(content))
				{
					string[] list = content.Split(' ');
					if (list != null && list.Length > 0)
					{
						string ip = list[list.Length - 1];
						web.Navigate(updateurl + ip);
						isDone = true;
						tUrl.Text = updateurl + ip;
						Clipboard.SetData(System.Windows.Forms.DataFormats.Text, tUrl.Text);
					}
				}
			}
			else
			{
				File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "result.txt", string.Concat(DateTime.Now.ToString(), "\t", web.Document.Body.InnerText, "\r\n"));
				Close();
			}
		}

		void Mainform_Load(object sender, EventArgs e)
		{
			web.Navigate(ipurl);
		}
	}
}
