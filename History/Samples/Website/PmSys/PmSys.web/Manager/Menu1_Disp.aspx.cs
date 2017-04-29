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
using System.Text;

using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web
{
    public partial class Menu1_Disp : System.Web.UI.Page
    {
        public string APCatID;
        public int NowCount = 0;
        public string OrderNo = (string)Common.sink("OrderNo", MethodType.Get, 255, 1, DataType.Str);
        public StringBuilder sb_HTMLSrc = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnStart();
            }
        }
        private void OnStart()
        {
            int ModuleID = (int)Common.sink("ModuleID", MethodType.Get, 255, 1, DataType.Int);
            ArrayList lst = BusinessFacade.GetPermissionModuleSub(ModuleID);
            foreach (sys_ModuleTable var in lst)
            {
                APCatID = var.M_PageCode;
                sb_HTMLSrc.AppendFormat("<table border={0}0{0} cellpadding={0}0{0} cellspacing={0}0{0} width={0}75{0} height={0}65{0} OnMouseOver={0}javascript:TableOnMouseOver('{1}');{0} OnMouseOut={0}javascript:TableOnMouseOut('{1}');{0} OnMouseDown={0}javascript:TableOnClick('{1}','{2}');{0} id={0}{1}{0} class={0}style_0_menu_ctable_off{0}>		<tr><td align={0}center{0}><img src={0}{4}{0} border={0}0{0} hspace={0}5{0} vspace={0}5{0}></td></tr>		<tr><td class={0}style_0_menu_ctext{0} align={0}center{0}>{3}</td></tr>	</table>", "\"", "x" + (NowCount + 1).ToString(), var.M_Directory.Replace("\\", "\\\\"), var.M_CName, ResolveClientUrl(var.M_Icon));
                NowCount++;
                
            }
        }
    }
}
