using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Winform.Interface;
using ULib.Core.DataSchema;

namespace ULib.Winform.Controls
{
	public class ActionTreeView<T> : TreeView where T:ActionTreeNode,new()
	{
		public delegate void NodeMouseClickHandler(object sender);
		public event NodeMouseClickHandler OnActionTreeNodeMouseClick;
		public T AddNode(string text, ActionTreeNode parent = null, object parameter = null, ActionCallback callback = null)
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
			T node = CreateNode();
			node.Text = text;
			node.Parameter = parameter;
			if (callback != null)
			{
				node.ActionCallback += callback;
			}
			nodes.Add(node);
			return node;
		}
		public void Load(ActionNodeData root)
		{
			Load(root, Nodes);
		}
		public void Load(List<ActionNodeData> nodes)
		{
			ActionNodeData root = new ActionNodeData();
			root.AddRange(nodes);
			Load(root, Nodes);
		}
		public void Load(ActionNodeData node, TreeNodeCollection tnc)
		{
			if (node != null && tnc != null)
			{
				List<Node> nc = node.Items;
				for (int i = 0; i < nc.Count; i++)
				{
					ActionNodeData ActionNodeData = nc[i] as ActionNodeData;
					if (ActionNodeData != null)
					{
						T treeNode = new T();
						treeNode.Text = ActionNodeData.Text;
						treeNode.Parameter = ActionNodeData;
						treeNode.ActionCallback += new ActionCallback(delegate(IActionControl sender, ActionEventArgs e)
						{
							if (OnActionTreeNodeMouseClick != null)
							{
								OnActionTreeNodeMouseClick(treeNode);
							}
						});
						if (ActionNodeData.IsDefault)
						{
							treeNode.NotifyAction(treeNode, null);
						}
						tnc.Add(treeNode);
						if (ActionNodeData.Items.Count > 0)
						{
							Load(ActionNodeData, treeNode.Nodes);
						}
					}
				}
			}
		}
		protected virtual T CreateNode()
		{
			return new T();
		}
		protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			ActionTreeNode node = e.Node as ActionTreeNode;
			if (node != null)
			{
				node.NotifyAction(node, new ActionNodeEventArgs { ActionType = ActionNodeEventArgs.ActionNodeEventType.MouseClick });
			}
		}
		protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			ActionTreeNode node = e.Node as ActionTreeNode;
			if (node != null)
			{
				node.NotifyAction(node, new ActionNodeEventArgs { ActionType = ActionNodeEventArgs.ActionNodeEventType.MouseDblClick });
			}
		}
	}
}
