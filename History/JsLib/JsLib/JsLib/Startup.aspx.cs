using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Fs.Data;

namespace TestWebApp
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//DbSqlServer db = Dbs.Use<DbSqlServer>("company");
			//DataTable tb = db.GetSchema("Columns");
			//Response.Write("<table>");
			//foreach (DataColumn col in tb.Columns)
			//{
			//    Response.Write("<th>" + col.ColumnName + "</th>");
			//}
			//foreach (DataRow row in tb.Rows)
			//{
			//    Response.Write("<tr>");
			//    foreach (DataColumn col in tb.Columns)
			//    {
			//        Response.Write("<td>" + row[col] + "</td>");
			//    }
			//    Response.Write("<tr>");
			//}
			//Response.Write("</table>");
			//Response.End();
		}
	}
}
