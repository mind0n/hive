using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;

namespace ULib.Controls
{
    public partial class DockTabControl : UserControl
    {
        public event Action<TabControl> AddTabPage;
        public event TabControlCancelEventHandler Selecting
        {
            add
            {
                TabControl.Selecting += value;
            }
            remove
            {
                TabControl.Selecting -= value;
            }
        }
		
        protected override void OnPaint(PaintEventArgs e)
        {
            //if (TabControl.TabPages.Count > 0 && !bCloseTab.Visible)
            //{
            //    bCloseTab.Visible = true;
            //}
            //else if (TabControl.TabPages.Count < 1 && bCloseTab.Visible)
            //{
            //    bCloseTab.Visible = false;
            //}
            base.OnPaint(e);
        }
        public DockTabControl()
        {
            InitializeComponent();
            Load += new EventHandler(DockTabControl_Load);
            
        }

        void DockTabControl_Load(object sender, EventArgs e)
        {
			TabControl.SelectedIndexChanged += new EventHandler(TabControl_SelectedIndexChanged);
            UpdateUI();
        }

		void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			ActionTabPage page = TabControl.SelectedTab as ActionTabPage;
			if (page != null)
			{
				page.NotifyActivated(page);
			}
		}

        private void UpdateUI()
        {
            //if (TabControl.TabPages.Count < 1)
            //{
            //    bCloseTab.Visible = false;
            //}
        }


        private void bCloseTab_Click(object sender, EventArgs e)
        {
            CloseActiveTab();            
        }

        private void CloseActiveTab()
        {
            if (TabControl.TabPages.Count > 0)
            {
                TabPage tab = TabControl.SelectedTab;
                if (tab.Controls != null && tab.Controls.Count > 0)
                {
                    DockForm form = tab.Controls[0] as DockForm;
                    if (form != null)
                    {
                        form.Close();
                    }
                    else
                    {
                        tab.Controls[0].Dispose();
                    }
                }
                TabControl.TabPages.Remove(tab);
                UpdateUI();
            }
        }
    }
}
