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

using PmSys;
using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.FieldManager
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }


        private void ListBind()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_FieldList(qp, out RecordCount);
            GridView1.DataSource = lst;
            GridView1.DataBind();
            this.AspNetPager1.RecordCount = RecordCount;
        }

        public string GetSubCount(string F_Key)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where V_F_Key='{0}'", Common.inSQL(F_Key));
            int RecordCount = 0;
            BusinessFacade.sys_FieldValueList(qp, out RecordCount);
            return RecordCount.ToString();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            ListBind();
        }
    }
}
