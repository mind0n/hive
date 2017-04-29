using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Portal.Winform.Components;

namespace Portal.Winform.Controls
{
    public partial class WebForm : Form
    {
        protected Dictionary<string, IScriptProcessor> processors = new Dictionary<string, IScriptProcessor>();
        protected string Uri;
        public WebForm(string uri, string name, IScriptProcessor service)
        {
            Uri = uri;
            AddProcessor(name, service);
            InitializeComponent();
            Load += WebForm_Load;
            FormClosing += WebForm_FormClosing;
        }

        void WebForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (KeyValuePair<string, IScriptProcessor> i in processors)
            {
                i.Value.OnClose();
            }
        }

        public void AddProcessor(string name, IScriptProcessor processor)
        {
            processors[name] = processor;
        }

        public object GetProcessor(string name)
        {
            return processors[name];
        }

        void WebForm_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, IScriptProcessor> i in processors)
            {
                i.Value.OnLoad();
            }
            this.MinimumSize = new Size(300, 150);
            web.ObjectForScripting = new WebExternal(this);
            web.Anchor = AnchorStyles.None;
            web.Left = 0;
            web.Top = 0;
            web.Width = Width;
            web.Height = Height;
            web.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Padding = new Padding(1);
            web.Navigate(new Uri("about:blank"));
            web.Document.Write("Loading ...");
            web.Navigate(new Uri(Uri));
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bTop_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }

        public void Wp(ref Message m)
        {
            WndProc(ref m);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y >= 0)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= 0 && pos.Y >= 0)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
