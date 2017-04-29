using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using WcfService;
using Wcf.Interface;
using System.ServiceModel.Description;
using Wcf.Service;
using Platform.Core;

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
			ServiceHost host = new ServiceHost(typeof(FavUrlService), httpUrl);

			//Add a service endpoint
			host.AddServiceEndpoint(typeof(IFavUrl), new WSHttpBinding(), "");

			//Enable metadata exchange
			ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
			smb.HttpGetEnabled = true;
			host.Description.Behaviors.Add(smb);

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
