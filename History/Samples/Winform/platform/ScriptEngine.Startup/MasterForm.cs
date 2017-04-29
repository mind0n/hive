using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Winform;
using ULib.Winform.Controls;
using ScriptPlatform.Screens;

namespace Platform.Startup
{
	public partial class MasterForm: Form
	{
		public MasterForm()
		{
			InitializeComponent();
			Load += MasterForm_Load;
		}

		void MasterForm_Load(object sender, EventArgs e)
		{
			ScannerScreen scanner = new ScannerScreen();
			Text = string.Concat(Text, " - ", Application.ProductVersion);
			ActionTreeView<ActionTreeNode> treeView = new ActionTreeView<ActionTreeNode>();
			ActionTreeNode node = treeView.AddNode("Basic Script");
			treeView.AddNode("Scan", node);
			TabControl tabs = new TabControl();
			this.BeginEmbed();
			this.Embed(treeView, EmbedType.Left);
			//this.Embed(tabs, EmbedType.Fill);
			this.Embed(scanner, EmbedType.Fill);
			this.EndEmbed();
		}
		
	}
}
