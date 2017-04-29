using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public class EventTreeNode : TreeNode
	{
		public delegate void MouseActionHandler(EventTreeNode sender, MouseEventArgs e);
		public MouseActionHandler MouseOnClick;

	}
}
