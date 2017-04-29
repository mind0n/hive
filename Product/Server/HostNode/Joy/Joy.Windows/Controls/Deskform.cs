using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Joy.Core;
using Joy.Windows.Services;

namespace Joy.Windows.Controls
{
    public partial class Deskform : JoyForm
    {
        private FileCfg<DesktopConfig> cfg = new FileCfg<DesktopConfig>();
        private string name;
        private AgentService agtsvc;
        //private DesktopService svc;
        //private NamedPipeCfg<IAgentService, AgentService> agt;
        public Deskform() : this(null)
        {
            
        }

        public Deskform(string deskname)
        {
            InitializeComponent();
            name = deskname;
            ShowInTaskbar = false;
            Visible = false;
            Opacity = 0;
            Load += Deskform_Load;
            var svc = new NamedPipeCfg<IDesktopService, DesktopService>();
            svc.Setup(null, true, false);
            var h = svc.WaitSetup();
            agtsvc = new AgentService(h);
            var agt = new NamedPipeCfg<IAgentService, AgentService>(name);
            agt.Setup(agtsvc);
        }
        
        void Deskform_Load(object sender, EventArgs e)
        {
            //SwitchDesktop();
            var cfg = this.cfg.Instance;
            var ihandle = Desktops.DesktopOpenInput();
            cfg.DefaultHandle = ihandle;
            AddDesktop(new DesktopConfigItem {Handle = ihandle, Name = "Default"});
            foreach (var i in cfg.Desktops)
            {
                AddDesktop(i);
            }
        }

        void AddDesktop(DesktopConfigItem item)
        {
            var cfg = this.cfg.Instance;
            IntPtr handle = IntPtr.Zero;
            if (item.Handle == IntPtr.Zero)
            {
                handle = Desktops.DesktopOpenOrCreate(item.Name);
                item.Handle = handle;
            }
            AddPictureBox(cfg, item);
        }

        private void AddPictureBox(DesktopConfig cfg, DesktopConfigItem item)
        {
            var r = Screen.PrimaryScreen.Bounds;
            var b = new Bitmap(r.Width, r.Height);
            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(new SolidBrush(Color.Black), r);
            }

            var pb = new PictureBox();
            pb.Image = b;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Width = cfg.ScreenshotWidth;
            pb.Height = cfg.ScreenshotHeight;
            pb.Click += deskPicClick;
            pb.Tag = item;
            fpn.Controls.Add(pb);
        }

        void deskPicClick(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb != null)
            {
                var cfg = pb.Tag as DesktopConfigItem;
                if (cfg != null)
                {
                    if (!string.Equals(name, cfg.Name))
                    {
                        agtsvc.StopService();
                        cfg.Name.InvokeNamedPipe((IAgentService svc) => svc.StartService());
                    }
                }
            }
        }

        private void SwitchDesktop()
        {
            string name = "virtualdesk";
            var n = Desktops.DesktopName(Desktops.DesktopOpenInput());
            if (!Desktops.DesktopExists(name))
            {
                Desktops.DesktopCreate(name);
                Desktops.ProcessCreate(name, "explorer.exe");
            }
            Desktops.ProcessCreate(name, AppDomain.CurrentDomain.BaseDirectory + "DesktopService.exe");

            Desktops.DesktopSwitch(name);

            new Thread(new ThreadStart(delegate()
            {
                Thread.Sleep(int.Parse(txt.Text) * 1000);
                Desktops.DesktopSwitch(n);
            })).Start();
        }

        private void bsw_Click(object sender, EventArgs e)
        {
            SwitchDesktop();
        }

        protected override void OnActivated(EventArgs e)
        {
            Width = cfg.Instance.FormWidth;
            Height = cfg.Instance.FormHeight;
            Locate(0, 0, 3);
            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            Hide();
            base.OnDeactivate(e);
        }

        private void tray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Opacity = 100;
                Show();
                Activate();
            }
            else
            {
                Application.Exit();
            }
        }
    }

    [Serializable]
    public class DesktopConfig
    {
        [ScriptIgnore]
        public IntPtr DefaultHandle;
        public int ScreenshotWidth;
        public int ScreenshotHeight;
        public int FormWidth;
        public int FormHeight;
        public List<DesktopConfigItem> Desktops;
    }

    [Serializable]
    public class DesktopConfigItem
    {
        public string Name;
        public string Screenshot;

        [ScriptIgnore]
        public IntPtr Handle;

        [ScriptIgnore]
        public PictureBox Box;
    }
}
