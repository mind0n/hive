using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.Admin.AdminDAL;
using System.Web.UI.HtmlControls;
using Joy.Core;

namespace Site.Admin
{
    public partial class ArticlesManage : System.Web.UI.Page
    {
        private static string categoryid;
        private static bool isNeedBindAllData;
		private DataTable gridViewData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                //Response.Write("<script>alert('尚未登陆或超时,请重新登录!');window.location='Login.aspx'</script>");
            }
            if (!IsPostBack)
            {
                ViewState["SortOrder"] = "articleupdate";
                ViewState["OrderDire"] = "desc";
                if (Request.QueryString["categoryid"] != null)
                {
                    categoryid = Request.QueryString["categoryid"].ToString();
                    BindArticlesData(false);
                    isNeedBindAllData = false;
                }
                else
                {
                    BindArticlesData(true);
                    isNeedBindAllData = true;
                }
            }
        }

        private void BindArticlesData(bool isAllData)
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAllData)
                {
                    dt = DalHandler.GetArtilesDetails();
                }
                else
                {
                    dt = DalHandler.GetArtilesDetails(Convert.ToInt32(categoryid));
                }

                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                dt.DefaultView.Sort = sort;
                GridViewArticles.DataSource = dt;
				gridViewData = dt;
                GridViewArticles.DataBind();

            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        protected void GridViewArticles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewArticles.PageIndex = e.NewPageIndex;
            if (isNeedBindAllData)
            {
                BindArticlesData(true);
            }
            else
            {
                BindArticlesData(false);
            }
        }

        protected void GridViewArticles_DataBound(object sentdr, GridViewRowEventArgs e)
        {
			GridViewRow row = e.Row;
			HtmlGenericControl b = row.FindControl("divToolTip") as HtmlGenericControl;
			HtmlGenericControl c = row.FindControl("divContent") as HtmlGenericControl;
			if (b != null)
			{
				string brief = b.InnerHtml;
				if (!string.IsNullOrEmpty(brief))
				{
					string content = c.InnerHtml;
					if (!string.IsNullOrEmpty(content))
					{
						HtmlStatusMac m = new HtmlStatusMac();
						m.Parse(content);
						b.InnerHtml = m.ToString();
					}
				}
			}
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!DelCheck(GridViewArticles))
            {
                return;
            }
            foreach (GridViewRow row in GridViewArticles.Rows)
            {
                if ((row.FindControl("checkBoxSelect") as CheckBox).Checked)
                {
                    int articleId = Convert.ToInt32((row.FindControl("lblArticleID") as Label).Text);
                    try
                    {
                        int rlt = DalHandler.DeleteArticleById(articleId);
                        if (rlt == 1)
                        {
                            if (isNeedBindAllData)
                            {
                                BindArticlesData(true);
                            }
                            else
                            {
                                BindArticlesData(false);
                            }
                        }
                        else
                        {
                            //do sth                        
                        }
                    }
                    catch (Exception ex)
                    {
                        errorPlace.InnerText = ex.Message;
                    }
                }
            }
        }
        private bool DelCheck(GridView gridView)
        {
            if (gridView == null)
            {
                return false;
            }
            int checkNum = 0;
            foreach (GridViewRow row in gridView.Rows)
            {
                if ((row.FindControl("checkBoxSelect") as CheckBox).Checked)
                {
                    checkNum++;
                }
            }
            if (checkNum == 0)
            {
                return false;
            }
            return true;
        }

        protected void GridViewArticles_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "Desc")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "Desc";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            if (isNeedBindAllData)
            {
                BindArticlesData(true);
            }
            else
            {
                BindArticlesData(false);
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            string CheckAllSelectedChange = @"<script type='text/javascript'>
    function doCheckAllSelectedChange(checkBox) {
        var checkAll = checkBox;
        if (checkAll != null) {
var gridView=" + GridViewArticles.ClientID + @";
            if (gridView) {
                var inputs = gridView.getElementsByTagName('input');
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == 'checkbox' && inputs[i].name.indexOf('checkBoxSelect')>0) {
                        inputs[i].checked = checkBox.checked;
                    }
                }
            }
        }
    }
</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "CheckAllSelectedChange", CheckAllSelectedChange);
            base.OnPreRender(e);
        }
		public string GetBrief(string articleIdStr, string brief)
		{
			int articleId;
			if (int.TryParse(articleIdStr, out articleId))
			{
				if (gridViewData != null && articleId > 0 && string.IsNullOrEmpty(brief))
				{
					foreach (DataRow r in gridViewData.Rows)
					{
						int id = (int)r["articleid"];
						if (id == articleId)
						{
							return id.ToString();
						}
					}
				}
			}
			return brief;
		}
    }
}