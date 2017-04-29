using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform
{
	public partial class Screen : Form
	{
		public Screen()
		{
			InitializeComponent();
			TopLevel = false;
			Load += new EventHandler(Screen_Load);
		}

		public virtual void EmbedInto(Control target)
		{
			target.Controls.Add(this);
			this.Show();
			Dock = DockStyle.Fill;
		}

		void Screen_Load(object sender, EventArgs e)
		{
			
		}

	}
}
