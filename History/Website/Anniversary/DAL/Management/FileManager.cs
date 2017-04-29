using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Joy.Server;
using Joy.Server.Data;
using Joy.Server.DataSlots;

namespace DAL.Management
{
	public class FileManager
	{
		public static readonly FileManager Instance = new FileManager();
		public FileManager() { }
		public string ListFiles(int page, int size, string orderField = "utime", bool asc = true)
		{
			//select top 2 * from tfiles 
			//where id not in (select top 4 id from tfiles order by utime desc)
			//order by utime desc
			if (page < 1)
			{
				page = 1;
			}
			if (size < 1)
			{
				size = 10;
			}
			SqlObject sub = new SqlObject();
			SqlObject so = new SqlObject();
			if (page > 1)
			{
				so.SelectTop(size, "id", "fname", "dname", "utime").From("tfiles")
					.Where("id", "not in", string.Concat("(",
						sub.SelectTop(page * size - size, "id").From("tfiles").OrderBy(orderField, asc ? string.Empty : "desc").ToString(), ")")
						, false).OrderBy(orderField, asc ? string.Empty : "desc");
			}
			else
			{
				so.SelectTop(size, "id", "fname", "dname", "utime").From("tfiles").OrderBy(orderField, asc ? string.Empty : "desc");
			}
			string sql = so.ToString();
			//DataTable table = D.DB.GetDataTable(sql);
			ExecuteResult er = D.DB.ExecuteReader(sql, new Func<IDataReader, object>(delegate(IDataReader reader)
			{
				ViewerSlot vs = new ViewerSlot();
				vs.Retrieve(reader);
				return vs;
			}));
			if (er.IsNoException)
			{
				ViewerSlot rlt = er.ObjRlt as ViewerSlot;
				return rlt.ToJson();
			}
			else
			{
				return string.Concat("{ error:\"", er.Exception.Message.Replace("\"", "'"), "\"}");
			}
		}
	}
}
