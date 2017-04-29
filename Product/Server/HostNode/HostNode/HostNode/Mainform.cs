using Joy.Core;
using Joy.Service;
using Joy.Windows;
using Joy.Windows.Controls;
using Joy.Windows.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostNode
{
    public partial class Mainform : Form
    {
        private FileCfg<NodeSettings> cfg = new FileCfg<NodeSettings>();
        public Mainform()
        {
            InitializeComponent();
            Error.ExceptionHandlers.Add(ex =>
            {
                Debug.WriteLine(ex);
            });
            Load += Mainform_Load;
            Shown += Mainform_Shown;
        }

        void Mainform_Shown(object sender, EventArgs e)
        {
            LaunchWebServer(cfg.Instance.Port, (port) =>
            {
                this.Msg(() =>
                {
                    lbmsg.Text = "服务器已启动，端口：{0}".Fmt(port);
                });
                var p = Process.Start("http://localhost:{0}/ts/test/arg1/arg2".Fmt(port));
                p.WaitForExit();
                if (cfg.Instance.IsAutoExit)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        Close();
                    });
                }
                //Process.Start("http://localhost:{0}/joy/js/jquery.dom".Fmt(port));
            }, (port) =>
            {
                this.Msg(() =>
                {
                    lbmsg.Text = "服务器启动失败";
                });
            });

        }

        private void LaunchWebServer(int port, Action<int> successCallback = null, Action<int> failCallback = null)
        {
            Thread th = null;
            th.Launch(() =>
            {
                var svc = new WebDispatcher(port, new Func<string>(delegate()
                {
                    var r = ConfirmBox.Show<string>("管理员密码:", this);
                    return r;
                }));
                var cfg = new WebHttpCfg<IDispatcher, WebDispatcher>(port, string.Empty);
                cfg.Setup(svc, false);
                var h = cfg.WaitSetup();
                if (h.State == System.ServiceModel.CommunicationState.Opened)
                {
                    if (successCallback != null)
                    {
                        successCallback(port);
                    }
                }
                else
                {
                    if (failCallback != null)
                    {
                        failCallback(port);
                    }
                }
            });
        }

        void Mainform_Load(object sender, EventArgs e)
        {
            lbmsg.Text = "正在启动 Web 服务器，请稍候 ...";
        }
    }
}
