using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Controls;
using System.Windows.Forms;

namespace Utilities.Components.HA
{
	public class CommandCache
	{
		public static CommandCache Instance = new CommandCache();
		public List<CommandTreeViewNode> Nodes = new List<CommandTreeViewNode>();
		public void Add(TreeNode tvNode)
		{
			if (tvNode is CommandTreeViewNode)
			{
				Nodes.Add((CommandTreeViewNode)tvNode);
			}
		}
		public void ClearAndAdd(TreeNode tvNode)
		{
			Nodes.Clear();
			Add(tvNode);
		}
		public CommandTreeViewNode Read()
		{
			if (Nodes.Count > 0)
			{
				return Nodes[0].Clone();
			}
			return null;
		}
		public List<CommandTreeViewNode> ReadAll()
		{
			return Nodes;
		}
	}
}
