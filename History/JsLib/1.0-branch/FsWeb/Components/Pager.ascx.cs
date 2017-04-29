using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace FsWeb.Components
{
	public partial class Pager : System.Web.UI.UserControl
	{
		public delegate bool HandlePageIndexChanged(Pager sender, int pageIndex, int pageSize);
		public event HandlePageIndexChanged OnPageIndexChanging;
		public Dictionary<string, string> Style = new Dictionary<string,string>();
		private Control container;

		public Control Container
		{
			get { return container; }
			set { 
				container = value;
				//if (container != null)
				//{
				//    container.Controls.Add(this);
				//}
			}
		}
		//public string ContainerId
		//{
		//    get { return containerId; }
		//    set { 
		//        //if (!string.IsNullOrEmpty(containerId))
		//        //{
		//        //    Control origin = Page.FindControl(containerId);
		//        //    origin.Controls.Remove(this);
		//        //}
		//        containerId = value;
		//        if (!string.IsNullOrEmpty(containerId))
		//        {
		//            Control container = Page.FindControl(containerId);
		//            container.Controls.Add(this);
		//        }
		//    }
		//}
		public string TargetId
		{
			get { return targetId; }
			set { targetId = value; }
		}
		public int PageCount
		{
			get { return pageCount; }
			set { pageCount = value; }
		}
		public int PageSize
		{
			get { return pageSize; }
			set
			{
				int n = this.pageIndex * this.pageSize;
				pageSize = value;
				this.PageIndex = n / value;
			}
		}
		public int PageIndex
		{
			get { return pageIndex; }
			set
			{
				bool handled = false;
				if (OnPageIndexChanging != null)
				{
					handled = OnPageIndexChanging(this, value, pageSize);
				}
				pageIndex = value;
			}
		}
		public int TotalRecords
		{
			get { return totalRecords; }
			set { totalRecords = value; }
		}

		private int pageCount = 10;
		private int pageIndex = 0;
		private int pageSize = 10;
		private int totalRecords = 0;
		private string targetId;
		private string containerId;

		public Pager() : base() { }

		public void UpdateUI()
		{
			float floatPageCount = totalRecords / pageSize;
			int intPageCount = Convert.ToInt32(floatPageCount), startIndex = 0;

			if (intPageCount < floatPageCount)
			{
				intPageCount++;
			}
			startIndex = intPageCount - pageCount;
			if (pageIndex + pageCount / 2 < intPageCount)
			{
				startIndex = pageIndex - pageCount / 2;
			}
			if (startIndex < 0)
			{
				startIndex = 0;
			}
			if (numericCell != null)
			{
				numericCell.Controls.Clear();
				for (int i = startIndex; i < startIndex + pageCount; i++)
				{
					if (i < intPageCount)
					{
						//HtmlTableCell td = new HtmlTableCell();
						//td.Attributes.Add("class", "numeric");
						Button link = new Button();
						link.ID = "page" + i;
						link.Attributes.Add("index", i.ToString());
						link.Attributes.Add("class", "pagenumber");
						link.Text = (i + 1).ToString();
						link.Click += new EventHandler(DoPagerNumberClick);
						numericCell.Controls.Add(link);
					}
				}
			}
			foreach (string key in Attributes.Keys)
			{
				pagerTable.Attributes[key] = Attributes[key];
			}
			foreach (string key in Style.Keys)
			{
				pagerTable.Style[key] = Style[key];
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			UpdateUI();
		}
		protected void DoPagerNumberClick(object sender, EventArgs e)
		{
			Button number = sender as Button;
			PageIndex = Convert.ToInt32(number.Attributes["index"]);
		}

	}
}