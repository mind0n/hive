using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using Joy.Server.Entities;

using System.Collections;
using Joy.Server;
using Joy.Core;

namespace Joy.Server.Data
{
	public abstract class BulkOperate
	{
		public Exception LastException;
		protected Db db;
		protected int vSubmitCount;

		public BulkOperate(Db database)
		{
			Init(database, 1000);
		}
		protected void Init(Db database, int submitCount)
		{
			db = database;
			vSubmitCount = submitCount;
		}
		public abstract int Insert(DataTable dt, DataColumnCollection colinfo, params string [] ignoreFields);
		public abstract int Update(DataTable dt, string fname, DataColumnCollection colinfo, params string [] ignoreFields);
		public abstract int Delete(DataTable dt, string fname, DataColumnCollection colinfo);
	}
	public class SqlBulkOperate : BulkOperate
	{
		public SqlBulkOperate(Db database) : base(database) { }
		public override int Insert(DataTable dt, DataColumnCollection colinfo, params string[] ignoreFields)
		{
			/*
				insert into Managers(ManagerName)
				select 'a' union all
				select 'b' union all
				select 'c'
			 */
			int rlt = 0;
			int vNextSubmitValue = vSubmitCount;
			string sqlmain = "insert into {0} ({1}) {2}";
			string sqlsel = " union all select {0} ";
			string cols = "", sels = "", sql;
			string tname = SqlHelper.MakeSafeFieldNameSql(dt.TableName);
			int i = 0;
			LastException = null;

			foreach (DataColumn col in dt.Columns)
			{
				if (!ignoreFields.Contains<string>(col.ColumnName))
				{
					cols += "," + SqlHelper.MakeSafeFieldNameSql(col.ColumnName);
				}
			}
			cols = cols.Substring(1);
			CommittableTransaction ts = new CommittableTransaction();
			DbConnection conn = db.GetConnection();
			conn.Open();
			conn.EnlistTransaction(ts);

			while (i < dt.Rows.Count)
			{
				for (; i < dt.Rows.Count; i++)
				{
					DataRow row = dt.Rows[i];
					string vals = "";
					foreach (DataColumn col in dt.Columns)
					{
						if (!ignoreFields.Contains<string>(col.ColumnName))
						{
							string val = row[col].ToString();
							vals += "," + SqlHelper.MakeSafeFieldValue(val, colinfo, col) + " ";
						}
					}
					sels += string.Format(sqlsel, vals.Substring(1));
					if (i + 1 >= vNextSubmitValue)
					{
						vNextSubmitValue += vSubmitCount;
						break;
					}
				}

				sels = sels.Substring(11);
				sql = string.Format(sqlmain, tname, cols, sels);
				Logger.Log(sql);
				ExecuteResult er = db.Execute(sql, conn, ts);
				rlt += er.IntRlt;
				if (er.Exception != null)
				{
					rlt = 0;
					LastException = er.Exception;
					break;
				}
				sels = "";
				i++;
			}
			if (rlt != 0 && LastException == null)
			{
				ts.Commit();
			}
			if (conn.State != ConnectionState.Closed)
			{
				conn.Close();
			}
			ts.Dispose();
			return rlt;
		}
		public override int Update(DataTable dt, string fname, DataColumnCollection colinfo, params string[] ignoreFields)
		{
			/*
				UPDATE Managers
				SET ManagerName = CASE id
					WHEN 6 THEN 'value'
					WHEN 7 THEN 'value'
					WHEN 8 THEN 'value'
				END
				WHERE id IN (6,7,8)"
			 */
			int rlt = 0;
			int vNextSubmitValue = vSubmitCount;
			string sqlmain = "update {0} set {1} ";
			string sqlset = " , {0} = case {1} {2} end ";
			string sqlwhen = " when {0} then {1} ";
			string sqlwhere = " where {0} in ({1}) ";
			string sets = "", ids = "", sql, where, whens;
			string tname = SqlHelper.MakeSafeFieldNameSql(dt.TableName);
			int i = 0, n = 0;
			LastException = null;

			CommittableTransaction ts = new CommittableTransaction();
			DbConnection conn = db.GetConnection();
			conn.Open();
			conn.EnlistTransaction(ts);

			while (i < dt.Rows.Count)
			{
				foreach (DataColumn col in dt.Columns)
				{
					whens = "";
					//foreach (DataRow row in dt.Rows)
					for (i = vNextSubmitValue - vSubmitCount; i < vNextSubmitValue; i++)
					{
						DataRow row = dt.Rows[i];
						if (!col.ColumnName.Equals(fname))
						{
							whens += string.Format(sqlwhen
								, SqlHelper.MakeSafeFieldValue(row[fname].ToString(), colinfo, dt.Columns[fname])
								, SqlHelper.MakeSafeFieldValue(row[col].ToString(), colinfo, col));
						}
						else
						{
							ids += ","
								+ SqlHelper.MakeSafeFieldValue(row[col].ToString(), colinfo, col);
						}
						if (i + 1 >= vNextSubmitValue)
						{
							break;
						}
						else if (i + 1 >= dt.Rows.Count)
						{
							break;
						}
					}
					if (!col.ColumnName.Equals(fname))
					{
						if (ignoreFields.Contains<string>(col.ColumnName))
						{
							continue;
						}
						sets += string.Format(sqlset, SqlHelper.MakeSafeFieldNameSql(col.ColumnName), SqlHelper.MakeSafeFieldNameSql(fname), whens);
					}
				}
				sets = sets.Substring(2);
				ids = ids.Substring(1);
				where = string.Format(sqlwhere, fname, ids);
				sql = string.Format(sqlmain, tname, sets) + where;
				Logger.Log(sql);
				ExecuteResult er = db.Execute(sql, conn, ts);
				rlt += er.IntRlt;
				if (er.Exception != null)
				{
					rlt = 0;
					LastException = er.Exception;
					break;
				}
				//rlt += Execute(sql, conn, ts);
				//if (LastException != null)
				//{
				//    rlt = 0;
				//    break;
				//}
				sets = "";
				where = "";
				ids = "";
				vNextSubmitValue += vSubmitCount;
				if (i + 1 >= dt.Rows.Count)
				{
					break;
				}
			}
			if (rlt != 0 && LastException == null)
			{
				ts.Commit();
			}
			if (conn.State != ConnectionState.Closed)
			{
				conn.Close();
			}
			ts.Dispose();
			return rlt;
		}
		public override int Delete(DataTable dt, string fname, DataColumnCollection colinfo)
		{
			int rlt = 0;
			string sqlmain = "delete from {0} ";
			string sqlwhere = " where {0} in ({1}) ";
			string sql, where, ids = "";
			LastException = null;
			foreach (DataRow row in dt.Rows)
			{
				if (dt.Columns.Contains(fname))
				{
					ids += "," + SqlHelper.MakeSafeFieldValue(row[fname].ToString(), colinfo[fname].ToString(), true);
				}
			}
			ids = ids.Substring(1);
			where = string.Format(sqlwhere, SqlHelper.MakeSafeFieldNameSql(fname), ids);
			sql = string.Format(sqlmain, SqlHelper.MakeSafeFieldNameSql(dt.TableName)) + where;
			Logger.Log(sql);
			CommittableTransaction ts = new CommittableTransaction();
			DbConnection conn = db.GetConnection();
			conn.Open();
			conn.EnlistTransaction(ts);
			ExecuteResult er = db.Execute(sql, conn, ts);
			rlt = er.IntRlt;
			if (er.Exception != null)
			{
				LastException = er.Exception;
				rlt = 0;
			}
			else
			{
				ts.Commit();
			}
			if (conn.State != ConnectionState.Closed)
			{
				conn.Close();
			}
			ts.Dispose();
			return rlt;
		}
	}
}
