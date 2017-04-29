using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Winform.Interface;

namespace ULib.Winform.Controls
{
	public class ActionTreeView : TreeView
	{
		public ActionNode AddNode(string text, ActionNode parent = null, string type = null, ActionCallback callback = null)
		{
			TreeNodeCollection nodes;
			if (parent == null)
			{
				nodes = this.Nodes;
			}
			else
			{
				nodes = parent.Nodes;
			}
			ActionNode node = CreateNode();
			node.Text = text;
			node.TypeName = type;
			if (callback != null)
			{
				node.ActionCallback += callback;
			}
			nodes.Add(node);
			return node;
		}
		protected virtual ActionNode CreateNode()
		{
			return new ActionNode();
		}
		protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			ActionNode node = e.Node as ActionNode;
			if (node != null)
			{
				node.NotifyAction(node, new ActionNodeEventArgs { ActionType = ActionNodeEventArgs.ActionNodeEventType.MouseClick });
			}
		}
		protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			ActionNode node = e.Node as ActionNode;
			if (node != null)
			{
				node.NotifyAction(node, new ActionNodeEventArgs { ActionType = ActionNodeEventArgs.ActionNodeEventType.MouseDblClick });
			}
		}
	}
}
