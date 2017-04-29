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
		ChannelFactory<ICommunicate> f;
		public ClientForm()
		{
			InitializeComponent();
			Load += new EventHandler(ClientForm_Load);
			FormClosing += new FormClosingEventHandler(ClientForm_FormClosing);
		}

		void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			f.Close();
		}

		void ClientForm_Load(object sender, EventArgs e)
		{
			string serviceUrl = "net.tcp://localhost:65500/Communicate";
			NetTcpBinding ntb = new NetTcpBinding();
			ntb.TransactionFlow = false;
			ntb.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
			ntb.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			ntb.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
			ntb.Security.Mode = SecurityMode.Transport;
			f = new ChannelFactory<ICommunicate>(ntb, serviceUrl);
			f.Credentials.Windows.ClientCredential.UserName = "michael";
			f.Credentials.Windows.ClientCredential.Password = "k:;1n";
			f.Open();
		}

		private void bSend_Click(object sender, EventArgs e)
		{
			ICommunicate serviceProxy = f.CreateChannel();
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
