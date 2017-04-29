using Joy.Common;
using Joy.Core;
using Joy.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joy.Portal.Testing
{
    public partial class TestLogger : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
        {
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			for (int i = 0; i < 100; i++)
			{
				Logger.Log("Log information {0}", i.ToString());
			}
        }
    }
}