using System;
using System.IO;
using System.Windows.Forms;
using ScriptPlatform.Screens;
using ULib.Core;
using ULib.Core.DataSchema;
using ULib.Winform;
using ULib.Winform.Controls;

namespace ULib.Startup
{
	public partial class MasterForm : Form
	{
		public MasterForm()
		{
			InitializeComponent();
			Load += MasterForm_Load;
		}

		void MasterForm_Load(object sender, EventArgs e)
		{
			//ScannerScreen scanner = new ScannerScreen();
			TreeTabScreen treetab = new TreeTabScreen();
			Text = string.Concat(Text, " - ", Application.ProductVersion);
			this.BeginEmbed();
			this.Embed(treetab, EmbedType.Fill);
			this.EndEmbed();

			ActionNodeData tn = new ActionNodeData();
			tn.Add(typeof(ScannerScreen).FullName + ",Platform.Startup", "Basic");
			treetab.LoadNodeData(tn);
		}
	}
}
