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

using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.ModuleManager
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {

            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            qp.PageIndex = Pager.CurrentPageIndex;
            qp.PageSize = Pager.PageSize;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ApplicationsList(qp, out RecordCount);
            GridView1.DataSource = lst;
            GridView1.DataBind();
            this.Pager.RecordCount = RecordCount;
        }

        protected void Pager_PageChanged(object sender, EventArgs e)
        {
            ListBind();
        }

        public int GetCatCount(int ApplicationID)
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_ApplicationID ={0} and M_ParentID=0 ", ApplicationID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
            return RecordCount;
        }
    }
}