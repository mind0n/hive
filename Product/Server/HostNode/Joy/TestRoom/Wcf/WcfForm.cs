using System.IO;
using System.Net;
using Joy.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using Joy.Windows.Services;

namespace TestRoom.Wcf
{
    public partial class WcfForm : Form
    {
        Stopwatch sw = new Stopwatch();
        private ServiceHost h;
        public WcfForm()
        {
            InitializeComponent();

        }

        private void bStartWcfHost_Click(object sender, EventArgs e)
        {
            //Process.Start(AppDomain.CurrentDomain.BaseDirectory + "DeskSvc.exe");
            var svc = new DesktopService();
            var cfg = new WebHttpCfg<IDesktopService, DesktopService>();
            cfg.Setup(svc, false);
            h = cfg.WaitSetup();
        }

        private void bPipe_Click(object sender, EventArgs e)
        {
            //ChannelFactory<IDesktopService> httpFactory = new ChannelFactory<IDesktopService>(new WebHttpBinding(),
            //    new EndpointAddress("http://localhost:8000/Desk"));

            //IDesktopService httpProxy =
            //  httpFactory.CreateChannel();
            ChannelFactory<IDesktopService> pipeFactory = null;
            try
            {
                var b = new NetNamedPipeBinding();
                b.MaxReceivedMessageSize = int.MaxValue;
                b.MaxBufferSize = int.MaxValue;
                pipeFactory =
                    new ChannelFactory<IDesktopService>(b,
                        new EndpointAddress("net.pipe://localhost/DeskPipe"));

                IDesktopService pipeProxy =
                    pipeFactory.CreateChannel();
                sw.Start();
                var d = pipeProxy.C("par");
                sw.Stop();
                box.Text += "GetData returned {0} bytes and costed {1} ms\r\n".Fmt(d.Result.ToString().Length, sw.ElapsedMilliseconds);
                var s = d.Result.ToString();
                if (!string.IsNullOrEmpty(s))
                {
                    var bytes = Convert.FromBase64String(s);
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(bytes, 0, bytes.Length);
                        var bmp = Image.FromStream(ms);
                        pic.Image = bmp;
                    }
                }
                //var img = d.Result as Bitmap;
                //pic.Image = img;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
            }
            finally
            {
                sw.Reset();
                if (pipeFactory != null && pipeFactory.State != CommunicationState.Closed)
                {
                    if (pipeFactory.State == CommunicationState.Faulted)
                    {
                        pipeFactory.Abort();
                    }
                    else
                    {
                        pipeFactory.Close();
                    }
                }
            }
        }

        private void bWebHttp_Click(object sender, EventArgs e)
        {
            try
            {

                sw.Reset();
                sw.Start();
                
                WebRequest request = WebRequest.Create("http://localhost:8000/DesktopService/c/20");
                WebResponse ws = request.GetResponse();
                var d = new StreamReader(ws.GetResponseStream(), true).ReadToEnd().UrlDecode();
                //var s = ws.GetResponseStream().Read()
                //var s = d.Substring(1, s.Length - 2);
                var r = d.FromJson<ServiceResult>();
                var s = r.Result.ToString();
                //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "c.txt", s);
                sw.Stop();
                box.Text += "GetData returned {0} bytes and costed {1} ms\r\n".Fmt(s.Length,
                    sw.ElapsedMilliseconds);
                byte[] bs = Convert.FromBase64String(s);
                using (MemoryStream ms = new MemoryStream(bs))
                {
                    var bmp = Image.FromStream(ms);
                    pic.Image = bmp;
                }
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
            }
        }

        private void bViewer_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "deskviewer.exe");
        }

        private void bStopSvc_Click(object sender, EventArgs e)
        {
            h.Abort();
        }
    }
}
