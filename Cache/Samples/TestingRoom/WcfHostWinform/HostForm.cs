using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfService;

namespace WcfHostWinform
{
    public partial class HostForm : Form
    {
        List<ServiceHost> Hosts = new List<ServiceHost>();
        public HostForm()
        {
            InitializeComponent();
        }

        private void HostForm_Load(object sender, EventArgs e)
        {   
        }

        private void CreateWcfHost(Type contract, System.ServiceModel.Channels.Binding bind, string addr, params IEndpointBehavior[] ebh)
        {
            new Thread(delegate()
            {
                ServiceHost host = new ServiceHost(typeof (TestService));
                try
                {
                    var se = host.AddServiceEndpoint(contract, bind, addr);
                    if (ebh != null && ebh.Length > 0)
                    {
                        foreach (var i in ebh)
                        {
                            se.Behaviors.Add(i);
                        }
                    }
                    host.Open();
                    Hosts.Add(host);
                }
                catch
                {
                    host.Abort();
                }
            }).Start();
            WindowState = FormWindowState.Minimized;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            foreach (var i in Hosts)
            {
                if (i.State == CommunicationState.Faulted)
                {
                    i.Abort();
                }
                if (i.State != CommunicationState.Closing || i.State == CommunicationState.Closed)
                {
                    i.Close();
                }
            }
        }

        private void bNetTcpBasic_Click(object sender, EventArgs e)
        {
            var binding = new NetTcpBinding(SecurityMode.None);
            CreateWcfHost(typeof(ITestService), binding, TestService.NetTcpAddress);
        }

        private void bBasicHttp_Click(object sender, EventArgs e)
        {
            //var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var binding = new WebHttpBinding(WebHttpSecurityMode.None);
            CreateWcfHost(typeof(ITestService), binding, TestService.BasicHttpAddress, new WebHttpBehavior());
        }
    }
}
