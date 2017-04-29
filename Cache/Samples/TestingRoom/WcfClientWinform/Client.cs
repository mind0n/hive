using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows.Forms;
using WcfService;

namespace WcfClientWinform
{
	public partial class Client : Form
	{
		public Client()
		{
			InitializeComponent();
		}

        public void Log(string msg, params string[] args)
        {
            box.Text += string.Format(msg, args);
            box.Text += "\r\n";
            box.Select(box.TextLength, 1);
            box.ScrollToCaret();
        }

        private void bTest_Click(object sender, EventArgs e)
	    {
	        var binding = new NetTcpBinding(SecurityMode.Transport);
	        using (ChannelFactory<ITestService> cf = new ChannelFactory<ITestService>(binding, TestService.NetTcpAddress))
	        {
                ITestService ts = cf.CreateChannel();
                for (int i = 0; i < 10; i++)
	            {
	                try
	                {
                        var rlt = ts.Test();
                        Log(rlt);
	                }
	                catch (Exception ex)
	                {
	                    Log(ex.Message);
	                    cf.Abort();
	                }
	            }
	        }
	    }

	    private void bError_Click(object sender, EventArgs e)
		{
			using (ChannelFactory<ITestService> cf = new ChannelFactory<ITestService>(new NetTcpBinding(), TestService.NetTcpAddress))
			{
				try
				{
					ITestService ts = cf.CreateChannel();
					ts.TestError();
				}
				catch(FaultException ex)
				{
					MessageBox.Show(ex.ToString());
					cf.Abort();
				}
			}
		}

		private void bTp_Click(object sender, EventArgs e)
		{
			//RemotingServices.IsTransparentProxy(this);
			TestService t = new TestService();
			t.Name = "xxx";
			EntityProxy p = new EntityProxy(t);
			object o = p.GetTransparentProxy();
			TestService s = o as TestService;
			//var r = s.Test();
			var r = s.Name;
			MessageBox.Show(r.ToString());
		}

        private void bBasicHttp_Click(object sender, EventArgs e)
        {
            var binding = new WebHttpBinding(WebHttpSecurityMode.None);
            using (ChannelFactory<ITestService> cf = new ChannelFactory<ITestService>(binding, TestService.BasicHttpAddress))
            {
                try
                {
                    cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                    ITestService ts = cf.CreateChannel();
                    string rlt = ts.TestHttp();
                    Log(rlt);
                }
                catch (FaultException ex)
                {
                    MessageBox.Show(ex.ToString());
                    cf.Abort();
                }
            }
        }
	}
}
