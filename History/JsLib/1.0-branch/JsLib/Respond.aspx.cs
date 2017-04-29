using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using FsWeb.Service;
using Fs.Entities;
using System.Text;
using System.Collections;
using Fs.Reflection;
using Fs.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Transactions;

namespace TestWebApp
{
	public partial class Respond : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RequestXmlInvoker rxi = new RequestXmlInvoker();
			rxi.Invoke(this, Request, Response);
		}
		[InvokableMethod(InvokableMethodAttribute.ParamTypes.ObjectArray)]
		public string GetAnswer(string content, string year)
		{
			//string rlt = "";
			//foreach (XElement param in pars)
			//{
			//    rlt += param.Value + "\n";
			//}
			string rlt = content + "|" + year;
			return rlt;
		}
		[InvokableMethod(InvokableMethodAttribute.ParamTypes.DictParams)]
		public string GetDataTable(DictParams parlist)
		{
			StrongTable st = new StrongTable("Managers");
			st.PagedQuery(Dbs.Use<DbSqlServer>("company"),1, 5, "", "", "");
			return st.ToXmlString("RowNumber");
		}
		[InvokableMethod(InvokableMethodAttribute.ParamTypes.XElement)]
		public string TestDataHandler(IEnumerable<XElement> pars)
		{
			string rlt = "";
			DbSqlServer db = Dbs.Use<DbSqlServer>("company");
			SqlConnection conn = (SqlConnection)db.GetConnection();
			foreach (XElement param in pars)
			{
				if ("add".Equals(param.Attribute("Name").Value))
				{
					foreach (XElement table in param.Elements())
					{
						DataColumnCollection dtInfo = db.GetColumnsInfo(table.Name.ToString());
						StrongTable dt = StrongTable.ParseFromXElement(table);
						SqlBulkOperate bh = new SqlBulkOperate(db);
						bh.Insert(dt, dtInfo, "id");
						dt.Dispose();
					}
				}
				else if ("amend".Equals(param.Attribute("Name").Value))
				{
					foreach (XElement table in param.Elements())
					{
						DataColumnCollection dtInfo = db.GetColumnsInfo(table.Name.ToString());
						StrongTable dt = StrongTable.ParseFromXElement(table);
						SqlBulkOperate bh = new SqlBulkOperate(db);
						bh.Update(dt, "id", dtInfo, "id");
						dt.Dispose();
					}
				}
				else if ("del".Equals(param.Attribute("Name").Value))
				{
					foreach (XElement table in param.Elements())
					{
						DataColumnCollection dtInfo = db.GetColumnsInfo(table.Name.ToString());
						StrongTable dt = StrongTable.ParseFromXElement(table);
						SqlBulkOperate bh = new SqlBulkOperate(db);
						bh.Delete(dt, "Age", dtInfo);
						dt.Dispose();
					}
				}
			}
			return rlt;
		}
		[InvokableMethod(InvokableMethodAttribute.ParamTypes.ObjectArray)]
		public string TestSp(string TableName)
		{
			DbSqlServer db = Dbs.Use<DbSqlServer>("family");
			SqlCommand cmd = db.GetSpCommand("GetRecordsFromTable"
				, new SqlParameter("@TableName", TableName)
				, db.CreateSqlParameter("@Result", SqlDbType.Int, ParameterDirection.Output)
				);
			
			XmlDocument doc = db.ExecuteXml(cmd, null);
			return doc.InnerXml;
			//return doc.t
			//DataSet ds = new DataSet();
			//SqlDataAdapter sda = new SqlDataAdapter(cmd as SqlCommand);
			//sda.Fill(ds);

			//StringBuilder rlt = new StringBuilder();
			//StringWriter sw = new StringWriter(rlt);
			//ds.WriteXml(sw);
			//sw.Close();
			//ds.Dispose();
			//sda.Dispose();
			//return rlt.ToString();
		}
	}
}