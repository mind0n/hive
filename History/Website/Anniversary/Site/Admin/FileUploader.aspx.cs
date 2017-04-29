using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Management;
using Joy.Server.Data;

namespace Site.Admin
{
	public partial class FileUploader : System.Web.UI.Page
	{
		public string JsonData;
		protected void Page_Load(object sender, EventArgs e)
		{
			string act = Request.QueryString["act"];
			int id = GetQueryString<int>("id");
			if (string.Equals(act, "del", StringComparison.OrdinalIgnoreCase))
			{
				SqlObject so = new SqlObject();
				if (id > 0)
				{
					so.DeleteFrom("tfiles").Where("id", "=", id.ToString(), false);
				}
				else
				{
					so.DeleteFrom("tfiles");
				}
				string sql = so.ToString();
				D.DB.ExecuteNonQuery(sql);
			}
			int pageSize = GetQueryString<int>("ps");
			int pageNum = GetQueryString<int>("pn");
			JsonData = FileManager.Instance.ListFiles(pageNum, pageSize, "utime", false);
		}
		protected T GetQueryString<T>(string key)
		{
			try
			{
				T val = (T)Convert.ChangeType(Request.QueryString[key], typeof(T));
				return val;
			}
			catch
			{
				return default(T);
			}
		}
	}
}