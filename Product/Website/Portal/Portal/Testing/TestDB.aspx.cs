using DAL;
using Joy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Portal.Testing
{
	public partial class TestDB : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				DbAccess db = DataCenter.Use<DbAccess>("dbportal");
				DataTable table = db.GetDataTable("select * from tusers");
				if (table != null)
				{
					box.InnerHtml = table.Rows.Count.ToString() + " records";
				}
			}
			catch (Exception ex)
			{
				box.InnerHtml = ex.ToString();
			}
		}
	}
}