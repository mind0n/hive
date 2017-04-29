using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Codgen;
using Fs.Data;
using System.Data;

namespace TestWeb.Codgen
{
	class TableEntityGenerator : Generator
	{
		public static string Generate(string templates)
		{
			DbSqlServer db = Dbs.Use<DbSqlServer>("company");
			string str = templates;
			string rlt = "";

			db.EnumTables(delegate(DataRow row, string tableNameCol)
			{
				str = templates.Replace(">TableName<", row[tableNameCol].ToString());
				str = str.Replace(">NodeName<", "test");
				rlt += str;
			});
			return rlt;
		}
	}
	public partial class GenTableEntity : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Write("<pre>" + TableEntityGenerator.Run("Codgen\\CodgenTemplates\\CSharp\\TableEntityBuilder\\Main.xml") + "</pre>");
		}
	}
}
