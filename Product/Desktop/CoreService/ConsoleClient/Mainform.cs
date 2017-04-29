using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace ServiceClient
{
	public partial class Mainform : Form
	{
		public Mainform()
		{
			InitializeComponent();
		}

		private void bCall_Click(object sender, EventArgs e)
		{
			string addr = "http://localhost:50000/core";
			ChannelFactory<ICoreService> channel = new ChannelFactory<ICoreService>(new BasicHttpBinding(), new EndpointAddress(addr));
			ICoreService proxy = channel.CreateChannel();
			output(proxy.Encode("ok").ToString());
		}
		void output(string msg, params string[] args)
		{
			box.Text += string.Format(msg, args) + "\r\n";
		}
	}
}
