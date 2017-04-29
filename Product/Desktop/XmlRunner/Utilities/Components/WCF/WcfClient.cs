using ULib.Forms;
using System.ServiceModel;
using System;
using ULib.Exceptions;
using System.Net;

namespace Utilities.Components.WCF
{
    public partial class WcfClient : EmbededForm
    {
        ChannelFactory<ICommunicate> clientFactory;
        ServiceHost host;
        public WcfClient()
        {
            InitializeComponent();
            Load += new System.EventHandler(ConnectionScreen_Load);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(WcfClient_FormClosing);
        }

        void WcfClient_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            host.Close();
        }

        private void ConnectionScreen_Load(object sender, System.EventArgs e)
        {
            InitializeService();
            InitializeClient();
        }

        private void InitializeService()
        {
            string url = "net.tcp://localhost:60000/communicate";
            host = new ServiceHost(typeof(Communicate));
            NetTcpBinding bind = new NetTcpBinding();
            bind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            bind.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            bind.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            bind.TransactionFlow = false;
            host.AddServiceEndpoint(typeof(ICommunicate), bind, url);
            host.Open();
        }

        private void InitializeClient()
        {
            string url = "net.tcp://localhost:60000/communicate";
            NetTcpBinding bind = new NetTcpBinding();
            bind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            bind.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            bind.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            bind.TransactionFlow = false;

            NetworkCredential nc = new System.Net.NetworkCredential("jesse.shen", "abc123_");
            clientFactory = new ChannelFactory<ICommunicate>();
            clientFactory.Endpoint.Address = new EndpointAddress(url);
            clientFactory.Endpoint.Binding = bind;
            clientFactory.Credentials.Windows.ClientCredential = nc;
        }

        private void bsend_Click(object sender, System.EventArgs e)
        {
            try
            {
                clientFactory.Open();
                ICommunicate proxy = clientFactory.CreateChannel();
                proxy.Echo(tchat.Text);
                clientFactory.Close();
            }
            catch (Exception err)
            {
                ExceptionHandler.Handle(err);
                clientFactory.Abort();
            }
        }

        private void Output(string msg, params string[] args)
        {
            thistory.Text += string.Format(msg, args) + "\r\n";
        }
    }
}
