using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public class EventTabPage : TabPage
	{
		public delegate bool PageEvtHandler();
		public PageEvtHandler TabOnClose;
		public bool AllowClose = true;
		protected TabControl vParent;
		public EventTabPage() {
			
		}
		public EventTabPage(string caption) : base(caption) { }
		public void AddTo(TabControl tabs)
		{
			if (tabs != null)
			{
				vParent = tabs;
				if (tabs.TabPages != null && tabs.TabPages.Count > 0)
				{
					AddTo(vParent.TabPages.Count, tabs);
				}
				else
				{
					AddTo(0, tabs);
				}
			}
		}
		public void AddTo(int index, TabControl tabs)
		{
			vParent = tabs;
			tabs.TabPages.Insert(index, this);
		}
		protected override void Dispose(bool disposing)
		{
			bool allowClose = AllowClose;
			if (TabOnClose != null)
			{
				allowClose &= TabOnClose();
			}
			if (vParent != null && allowClose)
			{
				vParent.TabPages.Remove(this);
				base.Dispose(disposing);
			}
		}
	}
}
