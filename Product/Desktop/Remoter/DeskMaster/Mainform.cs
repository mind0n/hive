using Joy.Core;
using Joy.Core.Encode;
using Joy.Windows;
using Joy.Windows.Events;
using Joy.Windows.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskMaster
{
    public partial class Mainform : Form
    {
        FileCfg<MasterConfig> c = new FileCfg<MasterConfig>();
        EvtFactory fac = new EvtFactory();

        private ServiceHost h;

        public Mainform()
        {
            InitializeComponent();
            Load += Mainform_Load;
            FormClosing += Mainform_FormClosing;
        }

        void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            isExiting = true;
        }
        bool isExiting;
        long count = 0;
        Stopwatch wh = new Stopwatch();

        void Mainform_Load(object sender, EventArgs e)
        {
            if (c.Instance.IsPassive)
            {
                var th = new Thread(new ThreadStart(delegate
                {
                    count = 0;
                    while (!isExiting && !IsDisposed)
                    {
                        try
                        {
                            wh.Start();
                            var encoding = new ASCIIEncoding();
                            var postData = Wall.CaptureScreenSec("EfRGL8mt");
                            //var rlt = new ServiceResult { Result = postData }.ToJson();
                            //byte[] data = encoding.GetBytes(rlt);
                            byte[] data = encoding.GetBytes(postData);
                            var urlstr = c.Instance.Url.Fmt("dl");
                            var req = WebRequest.Create(new Uri(urlstr, UriKind.Absolute));
                            req.Method = "POST";
                            req.ContentLength = data.Length;
                            var sreq = req.GetRequestStream();
                            sreq.Write(data, 0, data.Length);
                            using (var res = req.GetResponse())
                            {
                                var s = new StreamReader(res.GetResponseStream()).ReadToEnd();
                                if (!string.IsNullOrEmpty(s))
                                {
                                    try
                                    {
                                        var json = s;
                                        //var j = HttpUtility.UrlDecode(json);
                                        json = OpenSSL.OpenSSLDecrypt(json, DesktopService.Pwd);
                                        fac.ParseEvents(json);
                                    }
                                    catch (Exception ex)
                                    {
                                        Error.Handle(ex);
                                    }

                                    //Invoke((MethodInvoker)delegate
                                    //{
                                    //    box.Text = s + "\r\n" + box.Text;
                                    //});
                                }
                                //Debugger.Break();
                            }
                            wh.Stop();
                            Sleep();
                            wh.Reset();
                        }
                        catch (WebException wex)
                        {
                            var sx = wex.Response.GetResponseStream();
                            string ss = "";
                            int lastNum = 0;
                            do
                            {
                                lastNum = sx.ReadByte();
                                ss += (char)lastNum;
                            } while (lastNum != -1);
                            sx.Close();
                            Sleep();
                        }
                    }
                }));
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                var svc = new DesktopService();
                svc.OnConnect += svc_OnConnect;
                var cfg = new WebHttpCfg<IDesktopService, DesktopService>(8000, "ds");
                cfg.Setup(svc, false);
                h = cfg.WaitSetup();
            }
        }

        private void Sleep()
        {
            if (!isExiting && !IsDisposed && IsHandleCreated)
            {
                Invoke((MethodInvoker)delegate
                {
                    if (wh.ElapsedMilliseconds > 0)
                    {
                        Text = "{0} - {1} - {2}".Fmt(++count, wh.ElapsedMilliseconds, (int)(1000 / wh.ElapsedMilliseconds));
                    }
                });
            }
            if (c.Instance.Interval > 0)
            {
                Thread.Sleep(c.Instance.Interval);
            }
        }

        void svc_OnConnect(System.ServiceModel.Channels.RemoteEndpointMessageProperty epp, string action, params string[] args)
        {
            Invoke((MethodInvoker)delegate
            {
                box.Text += "{0}-{3}->{1}:{2}\r\n".Fmt(action, epp.Address, epp.Port, args == null ? string.Empty : args[0]);
            });
        }
    }
    public class MasterConfig
    {
        public bool IsPassive;
        public string ExUrl;
        public string InUrl;
        public bool UseEx;
        public int Interval;
        public string Url
        {
            get
            {
                return UseEx ? ExUrl : InUrl;
            }
        }
    }
}
