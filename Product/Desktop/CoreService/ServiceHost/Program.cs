using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Core;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace ServiceHoster
{
	class Program
	{
		static void Main(string[] args)
		{
			using (ServiceHost host = new ServiceHost(typeof (CoreService), new Uri("http://localhost:50000/")))
			{
				Binding binding = new BasicHttpBinding();
				host.AddServiceEndpoint(typeof (ICoreService), binding, "core");
				if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
				{
					ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
					behavior.HttpGetEnabled = true;
					behavior.HttpGetUrl = new Uri("http://localhost:50000/coreservice");
					host.Description.Behaviors.Add(behavior);
					host.Opened += host_Opened;
				}
				host.Open();
				Console.ReadKey();
				host.Close();
			}
		}

		static void host_Opened(object sender, EventArgs e)
		{
			Console.WriteLine("Host Started...");
		}
	}
}
