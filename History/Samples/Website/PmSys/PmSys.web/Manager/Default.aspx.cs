using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PmSys.web
{
    public partial class _Default : System.Web.UI.Page
    {
        public string FrameName = FrameSystemInfo.GetSystemInfoTable.S_Name;
        public string FrameNameVer = FrameSystemInfo.GetSystemInfoTable.S_Version;
        public int MenuStyle = Common.MenuStyle;//5~1-a-s-p-x
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
