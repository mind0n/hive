using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.Admin.AdminDAL;
using DAL.Entities;
using System.Data.OleDb;
using Joy.Core;

namespace Site.Admin
{
    public partial class ArticleAddModify : System.Web.UI.Page
    {
        private static string articleId;

        private static string pageStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                //Response.Write("<script>alert('尚未登陆或超时,请重新登录!');window.location='Login.aspx'</script>");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["articleid"] != null && Request.QueryString["articlePageStatus"] != null)
                {
                    articleId = Request.QueryString["articleid"].ToString();
                    pageStatus = Request.QueryString["articlePageStatus"].ToString();
                    //labelTitle.Text = "文章修改";
                }
                else
                {
                    setArticleAddStatus();
                }
                if (Session["UserName"] as string != null)
                {
                    LabelAuthor.Text = Session["UserName"] as string;
                }
                DropDownListCategoriesBind();
                if (!string.IsNullOrEmpty(articleId) && pageStatus.Equals("modify"))
                {
                    LoadArticleData();
                    //labelTitle.Text = "文章修改";
                }
                else
                {
                    setArticleAddStatus();
                }
            }
        }

        private void setArticleAddStatus()
        {
            pageStatus = "add";
            articleId = "";
            //labelTitle.Text = "文章添加";
        }

        private void LoadArticleData()
        {
			HtmlStatusMac hm = new HtmlStatusMac();
			
            try
            {
                OleDbDataReader oleDr = DalHandler.GetArtilesDetailsByArticleId(Convert.ToInt32(articleId));

                while (oleDr.Read())
                {
					string content = GetColumnValue(oleDr, "content");
					if (!string.IsNullOrEmpty(content))
					{
						hm.Parse(content);
					}
                    //article.UserId = Convert.ToInt32(oleDr["userid"]);
					textBoxCaption.Text = GetColumnValue(oleDr, "tArticles.caption");
					dropdownlistCategories.SelectedValue = GetColumnValue(oleDr, "tArticles.categoryid");
					FCKeditor2.Value = content;
                    checkBoxVisible.Checked = GetColumnValue<bool>(oleDr,"tArticles.visible");
					txtLink.Text = GetColumnValue(oleDr,"link");
					txtTag.Text = GetColumnValue(oleDr,"tag");
					txtBrief.Text = hm.ToString();
					int? atype = GetColumnValue<int>(oleDr, "articletype");
					if (atype.HasValue)
					{
						string n = atype.Value.ToString();
						for (int i = 0; i < ddArticleType.Items.Count; i++)
						{
							ListItem li = ddArticleType.Items[i];
							if (string.Equals(li.Value, n))
							{
								ddArticleType.SelectedIndex = i;
								break;
							}
						}
					}
                }
            }
            catch (Exception ex)
            {
				ErrorHandler.Handle(ex);
            }
        }

		private static T GetColumnValue<T>(OleDbDataReader oleDr, string field)
		{
			if (oleDr[field] == null)
			{
				return default(T);
			}
			return (T)oleDr[field];
		}
		private static string GetColumnValue(OleDbDataReader oleDr, string field)
		{
			if (oleDr[field] != null)
			{
				return oleDr[field].ToString();
			}
			else
			{
				return null;
			}
		}
		class ArticleTypeItem
		{
			public string Text { get; set; }
			public string Value { get; set; }
		}
        private void DropDownListCategoriesBind()
        {
			ddArticleType.Items.Add(new ListItem { Text = "普通", Value = "0" });
			ddArticleType.Items.Add(new ListItem { Text = "图片新闻", Value = "1" });
			ddArticleType.Items.Add(new ListItem { Text = "视频信息", Value = "2" });
			try
            {
                DataTable dt = DalHandler.GetAllCategories2();
                dropdownlistCategories.DataValueField = "categoryid";
                dropdownlistCategories.DataTextField = "caption";
                dropdownlistCategories.DataSource = dt;
                dropdownlistCategories.DataBind();
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message ;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (pageStatus.Equals("modify"))
            {
                //update article
                try
                {
                    DAL.Entities.Article article = new DAL.Entities.Article();
                    if (!string.IsNullOrEmpty(articleId))
                    {
						string content = FCKeditor2.Value;
						HtmlStatusMac hm = new HtmlStatusMac();
                        article.ArticleId = Convert.ToInt32(articleId);
                        article.UserId = (Session["UserName"] as int?) == null ? 1 : (Session["UserName"] as int?);
                        article.Caption = textBoxCaption.Text.Trim();
                        article.CategoryId = Convert.ToInt32(dropdownlistCategories.SelectedValue);
                        article.Content = content;
                        article.IsVisible = checkBoxVisible.Checked;
                        article.ArticleUpdate = DateTime.Now;
						article.ArticleType = Convert.ToInt32(ddArticleType.SelectedValue);
						if (!string.IsNullOrEmpty(content))
						{
							hm.Parse(content);
							article.Brief = hm.ToString();
						}
                    }
                    int rlt = DalHandler.InsertOrUpdateArtile(article, 1);
                    if (rlt == 1)
                    {
                        pageStatus = string.Empty;
                        articleId = string.Empty;
                    }
                    else
                    {
                        //....
                    }
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }
            }
            else
            {
                //add article
                DAL.Entities.Article article = new DAL.Entities.Article();
                article.UserId = (Session["UserName"] as int?) == null ? 1 : (Session["UserName"] as int?);
                article.Caption = textBoxCaption.Text.Trim();
                article.CategoryId = Convert.ToInt32(dropdownlistCategories.SelectedValue);
                article.Content = FCKeditor2.Value;
                article.IsVisible = checkBoxVisible.Checked;
                article.ArticleUpdate = DateTime.Now;
                try
                {
                    int rlt = DalHandler.InsertOrUpdateArtile(article, 0);
                    if (rlt == 1)
                    {
                    }
                    else
                    {
                        //...
                    }
                    RetControlStatus();
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            RetControlStatus();
            btnAdd.Text = "添加";
            pageStatus = "add";
        }

        private void RetControlStatus()
        {
            textBoxCaption.Text = "";
            dropdownlistCategories.SelectedIndex = 0;
            FCKeditor2.Value = "";
            checkBoxVisible.Checked = false;
        }


    }
}
