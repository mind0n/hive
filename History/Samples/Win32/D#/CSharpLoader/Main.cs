using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpLoader
{
	unsafe public partial class Main : Form
	{
		Timer tmr = new Timer();
		public Main()
		{
			InitializeComponent();
			tmr.Interval = 11;
			tmr.Tick += new EventHandler(tmr_Tick);
			tmr.Enabled = true;
			tmr.Start();
			FormClosing += new FormClosingEventHandler(Main_FormClosing);
			Load += new EventHandler(Main_Load);
		}

		void Main_Load(object sender, EventArgs e)
		{
			DllNative.InitDirect3D(pnx.Handle.ToPointer());
		}

		void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			tmr.Stop();
			DllNative.CleanUpDirect3D();
		}

		void tmr_Tick(object sender, EventArgs e)
		{
			DllNative.Render();
		}
	}
}
