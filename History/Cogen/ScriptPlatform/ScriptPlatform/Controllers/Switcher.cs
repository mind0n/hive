using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform
{
	public class SwitchItem : TreeNode
	{
		public Control Target;
		public Screen Screen;
		public Func<SwitchItem, bool> OnActivate;
		public Func<SwitchItem, bool> OnLoad;
		public void Activate()
		{
			if (OnActivate == null || OnActivate(this))
			{
				Target.Controls.Clear();
				Screen.EmbedInto(Target);
			}
		}
	}

	public class Switcher : TreeView
	{
		public void AddSwitch(Control container, Type type)
		{
			Screen screen = type.GetConstructor(Type.EmptyTypes).Invoke(null) as Screen;
			AddSwitch(container, screen);
		}

		public void AddSwitch(Control container, Screen screen)
		{
			SwitchItem item = new SwitchItem { Target = container, Screen = screen };
			item.Text = screen.Text;
			Nodes.Add(item);

			if (item.OnLoad != null)
			{
				item.OnLoad(item);
			}
		}

		public void Activate()
		{
			if (Nodes.Count > 0)
			{
				(Nodes[0] as SwitchItem).Activate();
			}
		}

		public Switcher()
		{
			this.NodeMouseClick += new TreeNodeMouseClickEventHandler(Switcher_NodeMouseClick);
			this.BeforeExpand += new TreeViewCancelEventHandler(Switcher_BeforeExpand);
			this.BeforeCollapse += new TreeViewCancelEventHandler(Switcher_BeforeCollapse);
		}

		void Switcher_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			
		}

		void Switcher_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			SwitchItem item = e.Node as SwitchItem;
			if (item.OnActivate != null)
			{
				item.OnActivate(item);
			}
		}

		void Switcher_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			SwitchItem item = e.Node as SwitchItem;
			item.Activate();
		}
	}
}
