using Joy.Server.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Testing
{
	public partial class TestRequestServicePage : ServicePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		public string testa(string a, string b)
		{
			return "testa_Success";
		}
		public string testb(string a, string b)
		{
			return "testb_Success";
		}
	}
}