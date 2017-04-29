using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fs.Authentication;
using System.Drawing.Imaging;
using System.Drawing;
using Fs.Web.Authentication;

namespace FSUI.Lib.Server.Authentication
{
	public class AuthImgPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = "image/png";

			Response.BinaryWrite(AuthCode.MakeAuthCode(5, Color.Maroon, 24));
		}
	}
}
