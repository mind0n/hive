using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Transactions;
using System.Xml.XPath;
using System.Timers;
using System.Threading;
using Joy.Core;

namespace Joy.Data
{

	public class DbAccess : Db
	{
		public const string KeyTotalRecords = "totalrecords";
		public override string ConnStr
		{
			get
			{
				return base.ConnStr;
			}
			set
			{
				base.ConnStr = value;
			}
		}
		public override DataAdapter GetAdapter(string Sql)
		{
			return new OdbcDataAdapter(Sql, GetConnection() as OdbcConnection);
		}
		public override DbCommand GetCommand(string Sql, DbConnection Connection)
		{
			return new OdbcCommand(Sql, Connection as OdbcConnection);
		}
		public override DbCommand GetCommand(string Sql, DbConnection Connection, DbTransaction Transaction)
		{
			OdbcCommand cmd = new OdbcCommand(Sql, Connection as OdbcConnection, Transaction as OdbcTransaction);
			return cmd;
		}
		public override DbConnection GetConnectionSingleTry()
		{
			Connection = new OdbcConnection(ConnStr);
			return Connection;
		}
		public override DataColumnCollection GetColumnsInfo(string tableName)
		{
			string tname = SqlHelper.MakeSafeFieldNameSql(tableName);
			DataTable dt = GetDataTable("select top(1) * from " + tname);
			DataColumnCollection rlt = dt.Columns;
			dt.Dispose();
			return rlt;
		}
		public override DataTable PagedQuery(string primaryField, string curtPage, string pageSize, string fieldClause, string tableClause, string whereClause, string orderClause, string groupClause)
		{
			string[] sql = SqlHelper.MakePagedSelectSql(primaryField, curtPage, pageSize, fieldClause, tableClause, whereClause, orderClause);
			DataTable dt = GetDataTable(sql[0]);
			int totalRecords = ExecScalar<int>(sql[1]);
			dt.ExtendedProperties[KeyTotalRecords] = totalRecords;
			return dt;
		}

		public override void GetReady(DbConfig cfg)
		{
			if (!string.IsNullOrEmpty(cfg.Url))
			{
				this.ConnStr = string.Format(cfg.Connector, cfg.Url.MapPath());
			}
		}
	}
}
