using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Winform.Interface;
using System.Windows.Forms;

namespace ULib.Winform.Controls
{
	public class ActionNodeEventArgs : ActionEventArgs
	{
		public enum ActionNodeEventType
		{
			MouseClick,
			MouseDblClick
		}
		public ActionNodeEventType ActionType;
	}
	public class ActionNode : TreeNode, IActionControl
	{
		public string TypeName;
		public void NotifyAction(IActionControl sender, ActionEventArgs argument)
		{
			if (ActionCallback != null)
			{
				ActionCallback(sender, argument);
			}
		}
		public event ActionCallback ActionCallback;
	}
	
}
