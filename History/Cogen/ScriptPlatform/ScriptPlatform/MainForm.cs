using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Screens;

namespace Platform
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Load += new EventHandler(MainForm_Load);
			Shown += new EventHandler(MainForm_Shown);
		}

		void MainForm_Shown(object sender, EventArgs e)
		{
			treeMain.Activate();
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			treeMain.AddSwitch(pnMain.Panel2, new LexScreen());
		}

	}
}
