using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Fs.Data;
using System.Threading;
using System.Collections.ObjectModel;
using Fs.Reflection;
using Fs.Entities;
using FsWeb.Controller;
using FsWeb.Components;

namespace TestWebApp
{
	public partial class TestGridView : System.Web.UI.Page
	{
		public string BeatNameDropDownData = "[{display:'Beat 0', value:'0'},{display:'Beat 1', value:'1'},{display:'Beat 2', value:'2'}]";
		protected void Page_Load(object sender, EventArgs e)
		{
			gvMain.RowDataBound += new GridViewRowEventHandler(gvMain_RowDataBound);
			pager.OnPageIndexChanging += new FsWeb.Components.Pager.HandlePageIndexChanged(DoOnPageIndexChanging);
			pager.TotalRecords = GetBeats().Count;
			if (!IsPostBack)
			{
				DoOnPageIndexChanging(pager, 1, 10);
			}
		}

		bool DoOnPageIndexChanging(FsWeb.Components.Pager sender, int pageIndex, int pageSize)
		{
			int index = pageSize * pageIndex;
			DataTable dt = DataTableFactory.Create<DataTable>(typeof(Beat));
			DataTableFactory.AddRows<Beat>(dt, GetBeats(), true);
			GridController gc = new GridController(gvMain, null, null, DoPagerDataBound);
			DataTable dtPage = dt.Clone();
			for (int i = index; i < index + pageSize; i++)
			{
				DataRow row = dtPage.NewRow();
				for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
				{
					row[j] = dt.Rows[i][j];
				}
				dtPage.Rows.Add(row);
			}
			gc.DataBind(dtPage);
			return true;
		}
		bool DoPagerDataBound(GridView grid, TableCell cell)
		{
			//Pager pager = Page.LoadControl(typeof(Pager), null) as Pager;
			
			//pager.Container = cell;
			//pager.UpdateUI();
			return true;
		}
		void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			GridView grid = sender as GridView;
			GridViewRow row = e.Row;
			GridViewRow header = grid.HeaderRow;
			if (row.RowType == DataControlRowType.Header)
			{
				foreach (TableCell cell in row.Cells)
				{
					DataObjectAttribute attribute = ClassHelper.GetPropertyAttribute<DataObjectAttribute>(typeof(Beat), cell.Text, null);
					if (attribute == null || !attribute.Visible)
					{
						cell.Style.Add("display", "none");
					}
				}
			}
			if (row.RowType == DataControlRowType.DataRow)
			{
				row.Attributes.Add("rowid", "row" + e.Row.RowIndex);
				row.Attributes.Add("editable", "true");
				row.Attributes.Add("onclick", "J.EditRow(this, externConfig);");
				for (int i = 0; i < header.Cells.Count; i++)
				{
					TableCell headerCell = header.Cells[i];
					TableCell rowCell = row.Cells[i];
					rowCell.Attributes.Add("field", headerCell.Text);
					rowCell.Style.Add("display", headerCell.Style["display"]);
					switch (headerCell.Text)
					{
						case "BeatKey":
							rowCell.Attributes.Add("isprimaryfield", "true");
							break;
						case "BeatName":
							rowCell.Attributes.Add("editable", "true");
							rowCell.Attributes.Add("editor", "select");
							break;
						case "SectorKey":
							//rowCell.Attributes.Add("editable", "true");
							rowCell.Attributes.Add("editor", "select");
							break;
						default:
							break;
					}
				}
			}
		}
		Collection<Beat> GetBeats()
		{
			if (Cache["data"] != null)
			{
				return Cache["data"] as Collection<Beat>;
			}
			Collection<Beat>rlt = new Collection<Beat>();
			for (int i = 0; i < 30; i++)
			{
				rlt.Add(new Beat { BeatKey = i, BeatName = "Beat " + i, SectorKey = i });
			}
			Cache["data"] = rlt;
			return rlt;
		}
	}
	public class Beat
	{
		[DataObject(Visible = true)]
		public int BeatKey
		{
			get { return beatKey; }
			set { beatKey = value; }
		}
		[DataObject(Visible = true)]
		public string BeatName
		{
			get { return beatName; }
			set { beatName = value; }
		}
		[DataObject(Visible = true)]
		public int SectorKey
		{
			get { return sectorKey; }
			set { sectorKey = value; }
		}
		private int beatKey;
		private string beatName;
		private int sectorKey;
	}
}
