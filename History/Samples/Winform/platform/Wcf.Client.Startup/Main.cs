using System;
using System.ServiceModel;
using System.Windows.Forms;
using Wcf.Interface;
using Wcf.Interface.DataSchema;
using Wcf.Interface.Faults;
using Wcf.Proxy;
using ULib.Core;


namespace Wcf.Client.Startup
{
	public partial class Main : Form
	{
		DispatchProxy proxy;
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
			bind.Security.Mode = SecurityMode.Message;
			//bind.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
			bind.MaxReceivedMessageSize = 10000;
			var addr = new EndpointAddress("http://localhost:10023/FavUrl");
			proxy = new DispatchProxy(bind, addr);
			//proxy.ClientCredentials.UserName.UserName = "uname";
			//proxy.ClientCredentials.UserName.Password = "upwd";
			

			//proxy.ClientCredentials.ClientCertificate.SetCertificate(
			//    StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "client.com"
			//);
		}
		void Output(string msg, params string[] args)
		{
			txt.Text += string.Format(msg, args) + "\r\n";
		}

		private void bAddUrl_Click(object sender, EventArgs e)
		{
			//File.WriteAllText("c:\\output.txt", WcfAction.BuildAction(ActionNames.AddUrlAction, turl.Text));
			proxy.Dispatch(WcfActions.CreateXml(WcfAction.BuildAction(ActionNames.AddUrlAction, turl.Text)));
			Output("Added: " + turl.Text);
		}

		private void bListUrls_Click(object sender, EventArgs e)
		{
			string xml = proxy.GetUrls();
			ExecuteResultSet resultSet = xml.FromXml<ExecuteResultSet>();
			Output(xml);
		}

		private void bError_Click(object sender, EventArgs e)
		{
			try
			{
				proxy.RaiseCustomError();
			}
			catch (FaultException<MyDetail> ee)
			{
				Output(ee.ToString());
			}
		}
	}
}
