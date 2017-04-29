using System;
using ULib.Winform.Controls;
using System.Windows.Forms;

namespace Utility.Startup.Forms
{
	public partial class UrlToolForm : ScreenRegion
	{
		public UrlToolForm()
		{
			InitializeComponent();
			Load += new EventHandler(UrlToolForm_Load);
		}

		private void UrlToolForm_Load(object sender, EventArgs e)
		{
			
		}

		private void tPath_TextChanged(object sender, EventArgs e)
		{
			string s = tPath.Text.Replace('\\', '/');
			tUrl.Text = s;
			Clipboard.SetData(DataFormats.Text, s);
		}
	}
}
