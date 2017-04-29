using System;
using System.Windows.Forms;
using System.ServiceModel;
using Common;

namespace Server.Startup
{
	public partial class ServerForm : Form
	{
		private ServiceHost host;
		public ServerForm()
		{
			InitializeComponent();
			Load += new EventHandler(ServerForm_Load);
			FormClosing += new FormClosingEventHandler(ServerForm_FormClosing);
		}

		void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			host.Close();
		}

		void ServerForm_Load(object sender, EventArgs e)
		{
			string serviceUrl = "net.tcp://localhost:65500/Communicate";
			host = new ServiceHost(typeof(Communicate));
			host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
			NetTcpBinding ntb = new NetTcpBinding();
			ntb.TransactionFlow = false;
			ntb.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			ntb.Security.Mode = SecurityMode.None;
			host.AddServiceEndpoint(typeof(ICommunicate), ntb, serviceUrl);
			host.Open();
			Output("Service Started");
		}

		void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
		{
			Output("Unknown message received: {0}", e.Message.ToString());
		}
		void Output(string msg, params string[] args)
		{
			thistory.Text += string.Format(msg, args);
			thistory.Text += "\r\n";
		}
	}
}
