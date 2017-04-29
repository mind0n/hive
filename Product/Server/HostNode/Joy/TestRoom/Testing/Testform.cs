using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using Joy.Core;

namespace TestRoom.Testing
{
    public partial class Testform : Form
    {
        private ServiceCfg<ITestCfg, TestCfg> c = new ServiceCfg<ITestCfg, TestCfg>(
            "net.pipe://localhost/{0}", typeof (TestCfg).Name,
            () => new NetNamedPipeBinding {MaxReceivedMessageSize = int.MaxValue, MaxBufferSize = int.MaxValue}
            );
        public Testform()
        {
            InitializeComponent();
            FormClosing += Testform_FormClosing;
        }

        void Testform_FormClosing(object sender, FormClosingEventArgs e)
        {
            c.Dispose();
        }

        private void bCfg_Click(object sender, EventArgs e)
        {
            var p = c.Proxy;
            var s = p.IsSetup;
            var t = p.GetContent("Success");
            if (s)
            {
                MessageBox.Show(t);
            }
        }

        private void bStartProxy_Click(object sender, EventArgs e)
        {
            c.Setup();
            Text = "Testform - Connected";
        }

        
    }
    public class TestCfg : MarshalByRefObject, ITestCfg
    {
        public bool IsSetup
        {
            get { return true; }
        }

        public string GetContent(string arg)
        {
            return arg;
        }
    }

    [ServiceContract]
    public interface ITestCfg
    {

        bool IsSetup { [OperationContract]get; }

        [OperationContract]
        string GetContent(string arg);
    }
}
