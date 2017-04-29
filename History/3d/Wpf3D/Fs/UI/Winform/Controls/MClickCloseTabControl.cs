using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public partial class MClickCloseTabControl : TabControl
	{
		public MClickCloseTabControl()
		{
			InitializeComponent();
			//Version v = System.Environment.Version;

			//if (v.Major < 2)
			//{
			//    this.SetStyle(ControlStyles.DoubleBuffer, true);
			//}
			//else
			//{
			//    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			//}

			//this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			//this.SetStyle(ControlStyles.UserPaint, true);
			//this.SetStyle(ControlStyles.ResizeRedraw, true);
			//this.DoubleBuffered = true;
			
			MouseClick += new MouseEventHandler(MClickCloseTabControl_MouseClick);
		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			//base.OnPaintBackground(pevent);
		}
		public void AddTab(TabPage page)
		{
			if (TabPages.Count <= 0)
			{
				AddTab(0, page);
			}
			else
			{
				AddTab(TabPages.Count - 1, page);
			}
		}
		public void AddTab(int index, TabPage page)
		{
			TabPages.Insert(index, page);
		}
		public TabPage GetPreviousTabPage(TabPage curt)
		{
			for (int i = 0; i < TabCount; i++)
			{
				if (TabPages[i] == curt)
				{
					if (i == 0)
					{
						if (TabCount == 1)
						{
							return null;
						}
						return TabPages[1];
					}
					else
					{
						return TabPages[i - 1];
					}
				}
			}
			return null;
		}
		protected virtual void MClickCloseTabControl_MouseClick(object sender, MouseEventArgs e)
		{
			TabPage sel = (EventTabPage)TabPages[SelectedIndex];
			EventTabPage pg = null;
			for (int i = 0; i < TabCount; i++)
			{
				Rectangle r = GetTabRect(i);
				if (r.Contains(e.Location))
				{
					pg = (EventTabPage)TabPages[i];
				}
			}
			if (pg == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Middle)
			{
				if (pg.AllowClose)
				{
					if (pg == sel)
					{
						sel = GetPreviousTabPage(pg);
					}
					pg.Dispose();
					if (sel != null)
					{
						this.SelectTab(sel);
					}
				}
			}
		}
	}
}
