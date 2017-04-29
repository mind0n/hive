using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Joy.Server.Core;

namespace Site.Admin.incs
{
    public partial class admin_checkPurview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                // Server.Execute("../incs/checkPurview.aspx");  // 此处用户权限验证
                Response.Write("对不起，您没有权限！");
                Response.Redirect(JoyConfig.UrlFromRoot("Admin/Login.aspx"));
            }


        }
    }
}
