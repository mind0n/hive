using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;
using System.ServiceModel;

namespace Utilities.Components.WCF
{
    public partial class WcfHoster : EmbededForm
    {
        ServiceHost host;
        public WcfHoster()
        {
            InitializeComponent();
            Load += new EventHandler(WcfHoster_Load);
            FormClosing += new FormClosingEventHandler(WcfHoster_FormClosing);
        }

        void WcfHoster_FormClosing(object sender, FormClosingEventArgs e)
        {
            host.Close();
        }

        void WcfHoster_Load(object sender, EventArgs e)
        {
            string url = "net.tcp://localhost:60000/communicate";
            host = new ServiceHost(typeof (Communicate));
            NetTcpBinding bind = new NetTcpBinding();
            bind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            bind.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            bind.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            bind.TransactionFlow = false;
            host.AddServiceEndpoint(typeof (ICommunicate), bind, url);
            host.Open();
        }
    }


    [ServiceContract]
    public interface ICommunicate
    {
        [OperationContract]
        string Echo(string msg);
    }

    public class Communicate : ICommunicate
    {
        public string Echo(string msg)
        {
            return msg;
        }
    }
}
