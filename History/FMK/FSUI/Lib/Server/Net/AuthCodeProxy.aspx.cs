using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fs.Net;
using System.IO;
using Fs.Web.Authentication;
using System.Net;

namespace FSUI.Lib.Server.Net
{
	public class AuthCodeProxy : System.Web.UI.Page
	{
		public static CookieContainer Cookies = new CookieContainer();
		protected void Page_Load(object sender, EventArgs e)
		{
			string authImgUrl = Request.QueryString.Get("imgurl");
			string authCodeUrl = Request.QueryString.Get("codeurl");
			string type = Request.QueryString.Get("type");
			if (!string.IsNullOrEmpty(authImgUrl))
			{
				Stream s = NetHelper.BinaryTransfer(authImgUrl, Cookies);
				System.Drawing.Image img = System.Drawing.Image.FromStream(s);
				MemoryStream ms = new MemoryStream();
				img.Save(ms, img.RawFormat);
				Response.BinaryWrite(ms.ToArray());
				TransferResult tr = NetHelper.Transfer(authCodeUrl, Cookies);
				Authenticator.AuthCode = tr.Result;
			}
		}
	}
}
