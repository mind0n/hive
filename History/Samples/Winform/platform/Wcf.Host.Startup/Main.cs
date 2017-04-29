using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using ULib.Core;
using Wcf.Interface;
using Wcf.Service;
using System.Security.Cryptography.X509Certificates;

namespace Wcf.Startup
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			Load += new EventHandler(Main_Load);
		}

		void Main_Load(object sender, EventArgs e)
		{
			Logger.LogHandlers.Add(Output);
			//Create a URI to serve as the base address
			Uri httpUrl = new Uri("http://localhost:10023/FavUrl");

			//Create ServiceHost
			ServiceHost host = new ServiceHost(typeof(DispatchService), httpUrl);
			host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
			host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNameValidator();
			//Add a service endpoint
			WSHttpBinding bind = new WSHttpBinding();
			//bind.Security.Mode = SecurityMode.Message;
			//bind.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
			host.AddServiceEndpoint(typeof(IDispatchService), bind, "");

			//Enable metadata exchange
			ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
			smb.HttpGetEnabled = true;
			host.Description.Behaviors.Add(smb);

			//host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
			//Start the Service
			host.Open();

			Logger.Log("Service is host at " + DateTime.Now.ToString());
		}
		void Output(string msg)
		{
			txt.Text += msg + "\r\n";
		}
	}

}
