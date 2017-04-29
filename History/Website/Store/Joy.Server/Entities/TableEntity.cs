using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Joy.Server.Data;
using Joy.Server.Web.Services;
using Joy.Common;
using Joy.Core.Logging;

namespace Joy.Server.Entities
{
	public class TableEntity
	{
		public EnumRowHandler OnRowEnumed;
		public delegate DataRow EnumRowHandler(DataRow row);
		public string TableName;
		public string PrimaryField;
		protected Db Database;
		protected string ConnStr;
		//protected DbsType BelongDbType;
		protected IDataAdapter da;
		protected DataSet ds;
		protected DataTable dt;
		public bool EnumRow(DataRowState RowState)
		{
			if (OnRowEnumed != null)
			{
				foreach (DataRow row in dt.Rows)
				{
					OnRowEnumed(row);
				}
				if (dt != null && dt.Rows.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
		public DataRow GetNewRow()
		{
			return dt.NewRow();
		}
		public DataRow GetValuedRow(IntegratedParams InParams)
		{
			DataRow row = GetNewRow();
			foreach (string fld in InParams.Keys)
			{
				if (dt.Columns.Contains(fld))
				{
					row[fld] = InParams[fld];
				}
			}
			return row;
		}
		public DataColumnCollection GetColumns()
		{
			return dt.Columns;
		}
		public void AddToDb(IntegratedParams InParams, bool ignorePk, bool forced)
		{
			AddToDb(GetValuedRow(InParams), ignorePk, forced);
		}
		public void AddToDb(DataRow row, bool ignorePk, bool forced)
		{
			//DataSet d;
			//dt.Rows.Add(row);
			//d = ds.GetChanges(DataRowState.Added);
			//da.Update(d);
			//DbHelper.Execute(MakeInsertSql(row, ignorePk, forced), ConnStr, BelongDbType);
			Database.Execute(MakeInsertSql(row, ignorePk, forced));
		}
		public void DelFromDb(string whereClause)
		{
			Database.Execute(MakeDeleteSql(whereClause));
		}
		public void Update(IntegratedParams InParams, string whereClause, bool ignorePk, bool forced)
		{
			Update(GetValuedRow(InParams), whereClause, ignorePk, forced);
		}
		public void Update(DataRow row, string whereClause, bool ignorePk, bool forced)
		{
			Database.Execute(MakeUpdateSql(row, whereClause, ignorePk, forced));
		}
		public string MakeDeleteSql(string whereClause)
		{
			string rlt = "delete from " + TableName;
			if (!string.IsNullOrEmpty(whereClause))
			{
				rlt += " where " + whereClause;
			}
			return rlt;
		}
		public string MakeUpdateSql(IntegratedParams InParams, string whereClause, bool ignorePk, bool forced)
		{
			DataRow row = GetValuedRow(InParams);
			return MakeUpdateSql(row, whereClause, ignorePk, forced);
		}
		public string MakeUpdateSql(DataRow row, string whereClause, bool ignorePk, bool forced)
		{
			string rlt;
			string upd = "update " + TableName + " set [content] ";
			string list = "";
			if (row != null)
			{
				foreach (DataColumn col in dt.Columns)
				{
					string val;
					if (ignorePk && col.ColumnName.Equals(PrimaryField))
					{
						continue;
					}
					if (MakeFieldSql(col.ColumnName, row, out val, forced, true))
					{
						list += " " + col.ColumnName + "=" + val + ",";
					}
				}
				if (!string.IsNullOrEmpty(list))
				{
					list = list.Substring(0, list.Length - 1);
				}
				rlt = upd.Replace("[content]", list);
				if (!string.IsNullOrEmpty(whereClause))
				{
					rlt += " where " + whereClause;
				}
				return rlt;
			}
			else
			{
				return "";
			}
		}
		public string MakeInsertSql(IntegratedParams InParams, bool ignorePk, bool forced)
		{
			DataRow row = GetValuedRow(InParams);
			return MakeInsertSql(row, ignorePk, forced);
		}
		public string MakeInsertSql(DataRow row, bool ignorePk, bool forced)
		{
			string rlt;
			string ins = "insert into " + TableName + "([cols]) values([vals])";
			string cols = "", vals = "";
			if (row != null)
			{
				foreach (DataColumn col in dt.Columns)
				{
					string val;
					if (ignorePk && col.ColumnName.Equals(PrimaryField))
					{
						continue;
					}
					if (MakeFieldSql(col.ColumnName, row, out val, forced, true))
					{
						vals += val + ",";
						cols += col.ColumnName + ",";
					}
				}
				if (!string.IsNullOrEmpty(cols))
				{
					cols = cols.Substring(0, cols.Length - 1);
					vals = vals.Substring(0, vals.Length - 1);
				}
				rlt = ins.Replace("[cols]", cols);
				rlt = rlt.Replace("[vals]", vals);
				return rlt;
			}
			else
			{
				return null;
			}
		}
		public string MakeOpListSql(string colNames, string values, string connector)
		{
			StringBuilder rlt = new StringBuilder();
			string col = "";
			object val;
			string[] cols = colNames.Split(',');
			string[] vals = values.Split(',');
			int lc = cols.Length;
			int lv = vals.Length;
			int i;
			for (i = 0; i < lv; i++)
			{
				val = vals[i];
				if (i < lc)
				{
					col = cols[i];
					if (!dt.Columns.Contains(col))
					{
						continue;
					}
				}
				if (string.IsNullOrEmpty(col))
				{
					continue;
				}
				rlt.Append(connector);
				rlt.Append(MakeOpSql(col, val));
			}
			if (i > 0)
			{
				rlt.Remove(0, connector.Length);
			}
			return rlt.ToString();
		}
		public string MakeOpSql(string colName, object val)
		{
			string rlt;
			MakeFieldSql(colName, val, out rlt, false, true);
			if (rlt.IndexOf("'") == 0)
			{
				return colName + " like " + rlt;
			}
			return colName + "=" + rlt;
		}
		public bool MakeFieldSql(string colName, object val, out string rlt, bool forced, bool fullformat)
		{
			string typ;
			if (!string.IsNullOrEmpty(colName) && !dt.Columns.Contains(colName))
			{
				rlt = null;
				return false;
			}
			DataColumn col = dt.Columns[colName];
			typ = col.DataType.ToString().ToLower();
			if (typ.IndexOf("date") >= 0 || typ.IndexOf("time") >= 0 || typ.IndexOf("string") >= 0 || typ.IndexOf("bool") >= 0)
			{
				rlt = val + "";
				if (fullformat)
				{
					rlt = "'" + rlt.Replace("'", "''") + "'";
				}
				if (!forced)
				{
					if (val == null)
					{
						return false;
					}
					else if (string.IsNullOrEmpty(val.ToString()))
					{
						return false;
					}
				}
			}
			else
			{
				if (val != null)
				{
					int n;
					Int32.TryParse(val.ToString(), out n);
					rlt = n.ToString();
				}
				else
				{
					rlt = "0";
				}
				if (!forced)
				{
					if (val == null || string.IsNullOrEmpty(val.ToString()))
					{
						rlt = null;
						return false;
					}
				}
			}
			return true;
		}
		public bool MakeFieldSql(string colName, DataRow row, out string rlt, bool forced, bool fullformat)
		{
			return MakeFieldSql(colName, row[colName], out rlt, forced, fullformat);
		}
		public int Load(string Sql)
		{
			int n;
			dt = Database.GetDataTable(Sql);
			if (dt != null && dt.Rows != null)
			{
				n = dt.Rows.Count;
			}
			else
			{
				n = 0;
			}
			return n;
		}
		public TableEntity(string TableName, Db Database)
		{
			init(TableName, Database);
		}
		protected void init(string TableName, Db Database)
		{
			//this.ConnStr = ConnStr;
			//BelongDbType = Type;
			this.Database = Database;
			ds = new DataSet();
			try
			{
				da = Database.GetAdapter("select top 1 * from " + TableName);
				//da = DbHelper.GetAdapter(`"select top 1 * from " + TableName, ConnStr, Type);
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
				throw e;
			}
			dt = da.FillSchema(ds, SchemaType.Source)[0];
			this.TableName = TableName;
			this.PrimaryField = dt.Columns[0].ColumnName;
		}
	}
}
