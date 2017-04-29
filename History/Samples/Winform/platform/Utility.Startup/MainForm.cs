using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptPlatform.Screens;
using ULib.Core;
using ULib.Winform;
using ULib.Core.DataSchema;
using Utility.Startup.Forms;

namespace Utility
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Load += new EventHandler(MainForm_Load);
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			TreeTabScreen tts = new TreeTabScreen();
			ActionNodeData nd = new ActionNodeData();
			nd.Add(typeof(PIDForm).AssemblyQualifiedName, "Pid", true);
			nd.Add(typeof(UrlToolForm).AssemblyQualifiedName, "Url");
            nd.Add(typeof(RemoteExecForm).AssemblyQualifiedName, "Remote");
            nd.Add(typeof(CommonExecution).AssemblyQualifiedName, "Common");
			this.BeginEmbed();
			this.Embed(tts, EmbedType.Fill);
			this.EndEmbed();
			tts.LoadNodeData(nd);
		}
	}
}
