using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wcf.Proxy;
using System.ServiceModel;
using Wcf.Interface.DataSchema;
using Wcf.Interface;

namespace Wcf.Client.Startup
{
	public partial class Main : Form
	{
		FavUrlProxy proxy;
		public Main()
		{
			InitializeComponent();
			Load += new EventHandler(Main_Load);
		}

		void Main_Load(object sender, EventArgs e)
		{
			var bind = new WSHttpBinding();
			bind.SendTimeout = new TimeSpan(0, 0, 0, 0, 100000);
			bind.OpenTimeout = new TimeSpan(0, 0, 0, 0, 100000);
			bind.MaxReceivedMessageSize = 10000;
			var addr = new EndpointAddress("http://localhost:10023/FavUrl");
			proxy = new FavUrlProxy(bind, addr);
		}
		void Output(string msg, params string[] args)
		{
			txt.Text += string.Format(msg, args) + "\r\n";
		}

		private void bAddUrl_Click(object sender, EventArgs e)
		{
			proxy.SetUrl(WcfAction.Build(ActionNames.AddUrlAction, turl.Text));
			Output("Added: " + turl.Text);
		}

		private void bListUrls_Click(object sender, EventArgs e)
		{
			string xml = proxy.GetUrls();
			Output(xml);
		}
	}
}
