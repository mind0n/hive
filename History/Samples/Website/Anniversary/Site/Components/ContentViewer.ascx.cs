using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Joy.Server;
using DAL;
using System.Web.UI.HtmlControls;
using System.Reflection;

namespace Site.Components
{
	public partial class ContentViewer : System.Web.UI.UserControl
	{
		const string templateSubTitle = "发布者：{0}&nbsp;&nbsp;&nbsp;&nbsp;来源：电子信息学院&nbsp;&nbsp;&nbsp;&nbsp;更新日期：{1}";
		public string VideoSrc;
		public string ImgSrc;
		public string TitleField;
		public string VideoSrcField;
		public string IconSrcField;
		public int MediaType;
		public string AuthorField;
		public string DateField;
		public string ContentField;
		public string UrlTemplate;
		public string KeyField;
		public string Title { get; set; }
		public string[] OutputFields;
		public List<string> Outputs = new List<string>();
		public DataTable data;
		private bool isHideFooterLabel;
		private bool isHideArticleTitle;

		public bool LoadData(string sql, params string[]args)
		{
			string s = string.Format(sql, args);
			DataTable table = D.DB.GetDataTable(s);
			data = table;
			if (data != null && data.Rows.Count > 0)
			{
				PrepareOutput(data.Rows[0]);
			}
			return data != null && data.Rows.Count > 0;
		}

		public void HideFooterLabel()
		{
			isHideFooterLabel = true;
		}
		public void HideArticleTitle()
		{
			isHideArticleTitle = true;
		}

		public void ShowToolBar(string id)
		{
			Control c = this.FindControl(id);
			if (c != null)
			{
				c.Visible = true;
			}
		}

		public void LoadPagedData(
			string primaryField, 
			string curtPage, 
			string pageSize, 
			string fieldClause, 
			string tableClause, 
			string whereClause, 
			string orderClause, 
			string groupClause)
		{
			data = D.DB.PagedQuery(primaryField, curtPage, pageSize, fieldClause, tableClause, whereClause, orderClause, groupClause);
			if (data != null && data.Rows.Count > 0)
			{
				PrepareOutput(data.Rows[0]);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (data == null || data.Rows.Count < 1)
			{
				return;
			}
			if (data.Rows.Count == 1)
			{
				DataRow row = data.Rows[0];
				title.InnerHtml = row.GetString(TitleField);
				subtitle.InnerHtml = string.Format(templateSubTitle, row.GetString(AuthorField), row.GetString(DateField));
				details.InnerHtml = row.GetString(ContentField);
				if (MediaType == 1)
				{
					itemicon.Visible = true;
					ImgSrc = row.GetString(IconSrcField);
					mediacontainer.Visible = true;
				}
				else if (MediaType == 2)
				{
					itemvideo.Visible = true;
					VideoSrc = row.GetString(VideoSrcField);
					mediacontainer.Visible = true;
				}
				PrepareOutput(row);
			}
			else
			{
				DataRow lastRow = null;
				details.InnerHtml = string.Empty;
				foreach (DataRow row in data.Rows)
				{
					HtmlGenericControl i = itemTemplate.Copy() as HtmlGenericControl;
					i.Visible = true;
					HtmlGenericControl title = i.Find("itemTitle") as HtmlGenericControl;
					HtmlGenericControl brief = i.Find("itemBrief") as HtmlGenericControl;
					HtmlGenericControl author = i.Find("itemAuthor") as HtmlGenericControl;
					HtmlGenericControl dtime = i.Find("itemDatetime") as HtmlGenericControl;
					HtmlGenericControl lAuthor = i.Find("labelAuthor") as HtmlGenericControl;
					HtmlGenericControl lDate = i.Find("labelDate") as HtmlGenericControl;
					//title.InnerHtml = row.GetString(TitleField);
					HtmlAnchor a = new HtmlAnchor();
					a.HRef = string.Format(UrlTemplate, row.GetString(KeyField));
					a.InnerHtml = row.GetString(TitleField);
					title.Controls.Add(a);
					title.Visible = !isHideArticleTitle;
					brief.InnerHtml = row.GetString(ContentField, null, 150);
					author.InnerHtml = row.GetString(AuthorField);
					dtime.InnerHtml = row.GetString(DateField, "yyyy-MM-dd");
					if (isHideFooterLabel)
					{
						lAuthor.Visible = false;
						lDate.Visible = false;
					}
					details.Controls.Add(i);
					lastRow = row;
				}
				//details.Controls.Add(table);
				if (lastRow != null)
				{
					title.InnerHtml = lastRow.GetString(Title);
				}
				PrepareOutput(lastRow);
			}

		}

		private void PrepareOutput(DataRow row)
		{
			if (OutputFields == null)
			{
				return;
			}
			foreach (string OutputField in OutputFields)
			{
				if (!string.IsNullOrEmpty(OutputField))
				{
					Outputs.Add(row.GetString(OutputField));
				}
			}
		}
	}
	public enum ContentViewerMode
	{
		Article,
		Category
	}
}