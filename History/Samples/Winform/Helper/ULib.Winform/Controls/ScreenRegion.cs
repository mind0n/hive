using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib.Winform.Controls
{
	public class ScreenRegion : Form
	{
		public ScreenRegion()
		{
			Load += new EventHandler(Screen_Load);
			TopLevel = false;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		}

		void Screen_Load(object sender, EventArgs e)
		{
		}
	}
}
