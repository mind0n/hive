using Joy.Server.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Testing
{
	public partial class TestPage : JoyPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		    HttpCookie c = Request.Cookies["tkclient"];

		}
	}
}