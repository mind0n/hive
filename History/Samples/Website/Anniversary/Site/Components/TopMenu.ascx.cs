using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DAL;
using System.Data;
using Joy.Core;
using Joy.Server;

namespace Site.Components
{
	public partial class TopMenu : System.Web.UI.UserControl
	{
        public string TopMenuJsUrl;
		protected void Page_Load(object sender, EventArgs e)
		{
            TopMenuJsUrl = Page.ResolveUrl("~/Scripts/TopMenu.js");
			List<Category> list = D.DB.ExecuteList<Category>("select tcategories.categoryid, caption, parentid, visible from vPageCategories where visible=True and pid like 'topmenu' order by tcategories.parentid, morder, caption, tcategories.categoryid");
			WebPage.RegistVariable(this.Page as WebPage, "catemenugroup", list.ToJson());
		}
	}
}