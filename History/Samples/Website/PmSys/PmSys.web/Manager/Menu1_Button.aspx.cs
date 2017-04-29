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
    public partial class Menu1_Button : System.Web.UI.Page
    {
        public string APCatalogueID = "S"+(string)Common.sink("APCatalogueID", MethodType.Get, 255, 1, DataType.Str);
        public string APCatCName = (string)Common.sink("APCatCName", MethodType.Get, 255, 1, DataType.Str);
        public string OrderNo = (string)Common.sink("OrderNo", MethodType.Get, 255, 1, DataType.Str);
        public string ButtonClass = "style_0_menu_button";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (OrderNo == "1")
            {
                ButtonClass = "style_0_nowmenu_button";
                
            }
        }
    }
}
