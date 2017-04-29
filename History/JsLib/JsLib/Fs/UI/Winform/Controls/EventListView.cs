using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public partial class EventListView : ListView
	{
		public EventListView()
		{
			InitializeComponent();
			MouseDoubleClick += new MouseEventHandler(EventListView_MouseDoubleClick);
		}

		void EventListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (SelectedItems != null && SelectedItems.Count > 0)
			{
				EventListViewItem item = (EventListViewItem)SelectedItems[0];
				if (item.OnItemLaunch != null)
				{
					item.OnItemLaunch(item, e);
				}
			}
		}

	}
}
