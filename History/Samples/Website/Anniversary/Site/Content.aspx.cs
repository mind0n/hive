using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Joy.Server;
using Site.Data;
using Joy.Server.Data;
using System.Data;
using DAL;
using DAL.Entities;
using Joy.Server;
using System.Web.UI.HtmlControls;

namespace Site
{
	public partial class Content : PageBase
	{
		private string id;
		private string pn;
		private string ps;
		private string total;
		protected void Page_Load(object sender, EventArgs e)
		{
			const string sqlArticle = "select top 1 tarticles.articleid, tarticles.caption, tarticles.articleupdate, tarticles.content, tcategories.caption as categoryname, tarticles.categoryid, tusers.utext as author, tarticles.articletype, tarticles.link, tarticles.tag from vArticleCategory where tarticles.visible=True and tcategories.visible=True and articleid={0}";
			const string sqlUpdateCounter = "update tArticles set counter=counter+1 where articleid=";
			id = Request.QueryString["aid"];
			
			if (!string.IsNullOrEmpty(id))
			{
				id = SqlHelper.MakeSafeFieldValue(id, "num");
				ContentViewer.TitleField = "caption";
				ContentViewer.DateField = "articleupdate";
				ContentViewer.ContentField = "content";
				ContentViewer.AuthorField = "author";
				ContentViewer.VideoSrcField = "link";
				ContentViewer.IconSrcField = "tag";
				ContentViewer.OutputFields = new string[] { "categoryid", "categoryname", "articletype" };

				if (ContentViewer.LoadData(sqlArticle, id))
				{
					ContentViewer.MediaType = Convert.ToInt32(ContentViewer.Outputs[2]);
					D.DB.Execute(string.Concat(sqlUpdateCounter, id));
				}
			}
			else
			{
				id = Request.QueryString["cid"];
				pn = Request.QueryString["pn"];
				ps = Request.QueryString["ps"];

				id = SqlHelper.MakeSafeFieldValue(id, "num");
				pn = SqlHelper.MakeSafeFieldValue(pn, "num");
				ps = SqlHelper.MakeSafeFieldValue(ps, "num");

				if (string.IsNullOrEmpty(pn) || "0".Equals(pn))
				{
					pn = "1";
				}
				if (string.IsNullOrEmpty(ps) || "0".Equals(ps))
				{
					ps = "12";
				}
				if (!string.IsNullOrEmpty(id) && !"0".Equals(id))
				{
					ContentViewer.KeyField = "articleid";
					ContentViewer.UrlTemplate = "../content.aspx?aid={0}";
					ContentViewer.AuthorField = "author";
					ContentViewer.ContentField = "content";
					ContentViewer.Title = "categoryname";
					ContentViewer.TitleField = "caption";
					ContentViewer.DateField = "articleupdate";
					ContentViewer.OutputFields = new string[] { "categoryid", "categoryname" };
					ContentViewer.LoadPagedData("articleid", pn, ps, "tarticles.articleid, tarticles.caption, tarticles.articleupdate, tcategories.caption as categoryname, utext as author, tarticles.categoryid, tarticles.content", "vArticleCategory", string.Format(" tarticles.visible=True and tarticles.categoryid={0}", id), " articleupdate desc", "");
					total = ContentViewer.data.ExtendedProperties[DbAccess.KeyTotalRecords].ToString();
					if (ContentViewer.Outputs != null && ContentViewer.Outputs.Count > 0)
					{
						string categoryId = ContentViewer.Outputs[0];
						string categoryname = ContentViewer.Outputs[1];
						if (!string.IsNullOrEmpty(categoryId))
						{
							string r = D.DB.ExecuteResult<string>(string.Format("select widgetsettings from tPcMappings where pid like '{0}' and categoryid={1}", this.PageId, categoryId));
							if (!string.IsNullOrEmpty(r))
							{
								if (string.Equals("message", r, StringComparison.OrdinalIgnoreCase))
								{
									ContentViewerContainer.Attributes["class"] = "hcate";
									ContentViewer.HideFooterLabel();
									ContentViewer.ShowToolBar("MessageToolBar");
								}
							}
						}
						if (string.Equals(categoryname, "祝福寄语", StringComparison.OrdinalIgnoreCase))
						{
							ContentViewer.HideArticleTitle();
						}
					}

				}
			}
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
		}

		private void SetFooter()
		{
			if (string.IsNullOrEmpty(ps))
			{
				return;
			}
			pager.Visible = true;
			pagenum.InnerHtml = pn;
			int pagenumber, pagesize, totalrecords, totalpages;
			int.TryParse(ps, out pagesize);
			int.TryParse(total, out totalrecords);
			int.TryParse(pn, out pagenumber);
			totalpages = (int)(totalrecords / pagesize) + 1;
			if (totalrecords % pagesize == 0)
			{
				totalpages--;
			}
			pagetotal.InnerHtml = totalpages + string.Empty;
			for (int i = pagenumber - 5; i < pagenumber + 5; i++)
			{
				if (i > 0 && i <= totalpages)
				{
					HtmlAnchor a = new HtmlAnchor();
					a.HRef = string.Concat("content.aspx?cid=", id, "&pn=", i.ToString(), "&ps=", ps);
					a.InnerHtml = i.ToString();
					pages.Controls.Add(a);
				}
			}
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			AddHistory(id);
			SetFooter();
		}

		private void AddHistory(string id)
		{
			if (ContentViewer.Outputs.Count > 0 && !string.IsNullOrEmpty(ContentViewer.Outputs[0]))
			{
				historyCategory.InnerHtml = ContentViewer.Outputs[1];
				historyCategory.HRef += ContentViewer.Outputs[0];
				historyCategory.Visible = true;
			}
		}

	}
}