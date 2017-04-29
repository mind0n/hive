using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public class EventListViewItem : ListViewItem
	{
		public delegate void MouseActionHandler(EventListViewItem sender, MouseEventArgs e);
		public MouseActionHandler OnItemLaunch;
		public EventListViewItem() : base() { }
		public EventListViewItem(string text) : base(text) { }
	}
}
