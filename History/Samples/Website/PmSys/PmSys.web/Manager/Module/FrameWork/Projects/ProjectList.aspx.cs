using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PmSys.Components;
using System.Collections;

namespace PmSys.web.Module.PmSys.Projects 
{
    public partial class ProjectList : System.Web.UI.Page
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
            ArrayList lst = BusinessFacade.PM_ProjectsList(qp, out RecordCount);
            GridView1.DataSource = lst;
            GridView1.DataBind();
            this.Pager.RecordCount = RecordCount;
        }

        protected void Pager_PageChanged(object sender, EventArgs e)
        {
            ListBind();
        }
    }
}