using ULib.Winform.Controls;
using ULib.Winform;
using ULib.Core.DataSchema;
using ULib.Core;
using System.IO;
using System.Windows.Forms;
using System;
using System.Reflection;
namespace ScriptPlatform.Screens
{
	public partial class TreeTabScreen : ScreenRegion
	{
		public SplitterPanel ContentPanel;
		public TreeTabScreen()
		{
			InitializeComponent();
			ContentPanel = Splitter.Panel2;
			Load += new System.EventHandler(TreeTabScreen_Load);
		}

		public TreeTabScreen(TextNode node) : this()
		{
		}

		public void LoadNodeData(ActionNodeData nodeData)
		{
			ActionTreeView.OnActionTreeNodeMouseClick += new ActionTreeView<ScreenTreeNode>.NodeMouseClickHandler(ActionTreeView_OnActionTreeNodeMouseClick); 
			ActionTreeView.Load(nodeData);
		}

		void ActionTreeView_OnActionTreeNodeMouseClick(object s)
		{
			ScreenTreeNode sender = s as ScreenTreeNode;
			if (sender != null)
			{
				object screenObj = null;
				if (sender.CachedScreen == null)
				{
					ActionNodeData data = sender.Parameter as ActionNodeData;
					Type type = Type.GetType(data.TypeName);
					ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
					screenObj = ci.Invoke(null);
					sender.CachedScreen = (Form)screenObj;
				}
				else
				{
					screenObj = sender.CachedScreen;
				}
				ScreenRegion region = screenObj as ScreenRegion;
				ContentPanel.Embed(region, EmbedType.Fill);
			}
		}

		void TreeTabScreen_Load(object sender, System.EventArgs e)
		{
		}
	}
}
