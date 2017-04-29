using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Winform.Interface;
using System.Windows.Forms;
using System.Reflection;

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
	public class ActionTreeNode : TreeNode, IActionControl
	{
		public object Parameter;
		public virtual void NotifyAction(IActionControl sender, ActionEventArgs argument)
		{
			if (ActionCallback != null)
			{
				ActionCallback(sender, argument);
			}
		}
		public event ActionCallback ActionCallback;
	}
	public class ScreenTreeNode : ActionTreeNode
	{
		public Form CachedScreen;

		//public ScreenTreeNode()
		//{
		//    ActionCallback += new Interface.ActionCallback(ScreenTreeNodeActionCallback);
		//}

		//void ScreenTreeNodeActionCallback(IActionControl sender, ActionEventArgs args)
		//{
		//    Type type = Type.GetType(Parameter as string);
		//    ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
		//    object screenObj = ci.Invoke(null);
		//    ScreenRegion region = screenObj as ScreenRegion;
		//    if (region != null)
		//    {
		//        region.Show();
		//    }
		//}
	}
}
