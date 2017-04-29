using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace WebTestRoom
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string ext = "wav";
			string filename = @"c:\test." + ext;
			string outname = @"c:\output." + ext;
			byte[] bs = File.ReadAllBytes(filename);
			ASCIIEncoding en = new ASCIIEncoding();
			tMy.Value = bs.Length + "\r\n";
			//tMy.Text += output.Length.ToString() + "\r\n";
			string output = Convert.ToBase64String(bs);
			tMy.Value += output.Length.ToString();
			tSys.Value = output;
			byte[] decode = Convert.FromBase64String(output);
			File.WriteAllBytes(outname, decode);
		}
	}
}