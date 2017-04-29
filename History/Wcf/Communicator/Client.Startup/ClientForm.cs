using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Common;

namespace Client.Startup
{
	public partial class ClientForm : Form
	{
		public ClientForm()
		{
			InitializeComponent();
		}

		private void bSend_Click(object sender, EventArgs e)
		{
			string serviceUrl = "net.tcp://localhost:65500/Communicate";
			NetTcpBinding ntb = new NetTcpBinding();
			ntb.TransactionFlow = false;
			ntb.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
			ntb.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			ntb.Security.Mode = SecurityMode.None;
			EndpointAddress addr = new EndpointAddress(serviceUrl);
			ICommunicate serviceProxy = ChannelFactory<ICommunicate>.CreateChannel(ntb, addr);
			using (serviceProxy as IDisposable)
			{
				Output(serviceProxy.Echo(tchat.Text));
			}
		}
		void Output(string msg, params string[] args)
		{
			thistory.Text += string.Format(msg, args);
			thistory.Text += "\r\n";
		}
	}
}
