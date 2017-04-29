using Joy.Portal.Layouts;
using Joy.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joy.Portal
{
    public partial class Portal : JPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			LoadLayout("/Layouts/HomeLayout.ascx");
        }
    }
}