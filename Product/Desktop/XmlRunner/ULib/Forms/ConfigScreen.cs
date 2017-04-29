using ULib.Forms;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.Integration;
using ULib.Controls;
using System;
using System.Collections.Generic;

namespace ULib.Forms
{
    public partial class ConfigScreen : DockForm
    {
        public event Action<object> OnConfigChanged;
		public ConfigMode Mode = ConfigMode.Object;
        protected object targetObj;
        protected Panel CurtPanel;

        public string Title
        {
            get
            {
                return lbCaption.Text;
            }
            set
            {
                lbCaption.Text = value;
            }
        }

        public ConfigScreen(object targetObject = null)
        {
            targetObj = targetObject;
            InitializeComponent();
            Load += new System.EventHandler(Screen_Load);
            pn.AutoScrollMinSize = new System.Drawing.Size(0, 12);
            if (CurtPanel == null)
            {
                CurtPanel = pn;
            }
        }

        public void Generate(object o = null, ConfigMode mode = ConfigMode.Object)
        {
            pn.Controls.Clear();
			if (o != null)
			{
				targetObj = o;
			}
			else
			{
				o = targetObj;
			}
			this.Mode = mode;
			if (mode == ConfigMode.Object)
			{
				FieldInfo[] list = o.GetType().GetFields();
				foreach (FieldInfo i in list)
				{
					bool isPwd = i.Name.IndexOf("password", System.StringComparison.OrdinalIgnoreCase) >= 0;
					AddConfigItemTo(CurtPanel, i.Name.Replace('_', ' '), i.Name, isPwd);
				}
			}
			else if (mode == ConfigMode.Dictionary)
			{
				IDictionary d = (IDictionary)o;
				foreach (object k in d.Keys)
				{
					string i = k.ToString();
					bool isPwd = i.IndexOf("password", System.StringComparison.OrdinalIgnoreCase) >= 0;
					isPwd = isPwd || i.IndexOf("pwd", System.StringComparison.OrdinalIgnoreCase) >= 0;
					if (d[k] != null)
					{
						AddConfigItemTo(CurtPanel, i, i, isPwd, d[k].GetType());
					}
				}
			}
        }

        public void AddTabPage(string tabPageName, TabControl target = null)
        {
            if (target == null)
            {
                if (CurtPanel is TabPage)
                {
                    target = ((TabPage)CurtPanel).Parent as TabControl;
                }
                else
                {
                    target = new TabControl();
                    target.Dock = DockStyle.Bottom;
                    CurtPanel.Controls.Add(target);
                }
            }
            TabPage page = new TabPage(tabPageName);
            target.TabPages.Add(page);
            CurtPanel = page;
        }

        public void AddConfigItem(string label, string targetPpt, int? index = null, bool isPassword = false, Panel parent = null)
        {
            AddConfigItemTo(parent, label, targetPpt, isPassword, null, index);
        }

        private void AddConfigItemTo(Panel parent, string label, string targetPpt, bool isPassword, Type type=null, int? index = null)
        {
            if (parent == null)
            {
                if (CurtPanel == null)
                {
                    CurtPanel = pn;
                }
                parent = CurtPanel;
            }
            object realObj = targetObj;
            string realName = targetPpt;
            string[] parentPpt = targetPpt.Split(',');
            if (index != null && parentPpt != null && parentPpt.Length > 0)
            {
                realName = parentPpt[parentPpt.Length - 1];
                for (int i = 0; i + 1 < parentPpt.Length; i++)
                {
                    string parentPptName = parentPpt[i];
                    FieldInfo fi = targetObj.GetType().GetField(parentPptName);
                    if (fi != null)
                    {
                        realObj = fi.GetValue(realObj);
                    }
                }
            }
            LabelTextControl ltc = new LabelTextControl(!index.HasValue ? realObj : ((IList)realObj)[index.Value], realName, type, Mode);
            ltc.Dock = DockStyle.Bottom;
            ltc.OnContentChanged += new LabelTextControl.ContentChangeDelegate(ltc_OnContentChanged);
            ltc.SetContent(label, null, isPassword);
            parent.Controls.Add(ltc);
            parent.AutoScroll = true;
        }

        private void ltc_OnContentChanged()
        {
            if (OnConfigChanged != null)
            {
                OnConfigChanged(targetObj);
            }
        }

        private void Screen_Load(object sender, System.EventArgs e)
        {
            bClose.Visible = false;
        }

        private void bClose_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            DockForm.Detach(this);
        }
    }
	public enum ConfigMode
	{
		Object,
		Dictionary
	}
}
