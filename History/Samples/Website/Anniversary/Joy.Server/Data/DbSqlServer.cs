using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using System.Transactions;
using Joy.Core;

namespace Joy.Server.Data
{
	//public class SqlSp
	//{
	//    public static SqlParameter Create(string name, object value)
	//    {
	//        return new SqlParameter(name, value);
	//    }
	//    public static SqlParameter Create(string name, object value, ParameterDirection dir)
	//    {
	//        SqlParameter rlt = new SqlParameter(name, value);
	//        rlt.Direction = dir;
	//        return rlt;
	//    }
	//    public static SqlParameter Create(string name, SqlDbType type, object value, ParameterDirection dir)
	//    {
	//        SqlParameter rlt = new SqlParameter(name, value);
	//        rlt.Direction = dir;
	//        return rlt;
	//    }
	//}
	public class DbSqlServer : Db
	{
		public DbSqlServer()
		{
			
		}
		public DbSqlServer(string ConnectionString)
			: base(ConnectionString)
		{
			
		}
		public int Execute(string Sql, SqlTransaction Transaction)
		{
			int RowCount;
			DbConnection Connection = GetConnection();
			DbCommand cm = GetCommand(Sql, Connection);
			cm.Transaction = Transaction;
			Connection.Open();
			RowCount = cm.ExecuteNonQuery();
			Connection.Close();
			return RowCount;
		}
		public override DataAdapter GetAdapter(string Sql)
		{
			SqlDataAdapter sda = new SqlDataAdapter(Sql, GetConnection() as SqlConnection);
			sda.UpdateBatchSize = 0;
			return sda;
		}
		public override DbCommand GetCommand(string Sql, DbConnection Connection)
		{
			return new SqlCommand(Sql, Connection as SqlConnection);
		}
		public override DbCommand GetCommand(string Sql, DbConnection Connection, DbTransaction Transaction)
		{
			SqlCommand cmd = new SqlCommand(Sql, Connection as SqlConnection, Transaction as SqlTransaction);
			return cmd;
		}
		public override DbConnection GetConnectionSingleTry()
		{
			Connection = new SqlConnection(ConnStr); ;
			return Connection as SqlConnection;
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
			string[] sql = SqlHelper.MakeSqlPagedSelectSql(primaryField, curtPage, pageSize, fieldClause, tableClause, whereClause, orderClause, groupClause);
			DataTable dt = GetDataTable(sql[0]);
			int totalRecords = ExecScalar<int>(sql[1]);
			dt.ExtendedProperties["totalrecords"] = totalRecords;
			return dt;
		}
		public SqlParameter CreateSqlParameter(string name, SqlDbType type, ParameterDirection dir)
		{
			SqlParameter rlt = new SqlParameter(name, type);
			rlt.Direction = dir;
			return rlt;
		}
		public SqlParameter CreateSqlParameter(string name, ParameterDirection dir)
		{
			SqlParameter rlt = new SqlParameter();
			rlt.ParameterName = name;
			rlt.Direction = dir;
			return rlt;
		}
		public SqlParameter CreateSqlParameter(string name, object value, SqlDbType type, int size)
		{
			SqlParameter rlt = new SqlParameter(name, value);
			rlt.SqlDbType = type;
			rlt.Size = size;
			return rlt;
		}
		public SqlParameter CreateSqlParameter(string name, object value, SqlDbType type, int size, ParameterDirection dir)
		{
			SqlParameter rlt = new SqlParameter(name, type, size);
			rlt.Direction = dir;
			rlt.Value = value;
			return rlt;
		}
		public SqlCommand GetSpCommand(string Sql, params SqlParameter[] pars)
		{
			SqlCommand cmd = GetCommand(Sql) as SqlCommand;
			if (cmd != null)
			{
				cmd.CommandType = CommandType.StoredProcedure;
				foreach (SqlParameter par in pars)
				{
					cmd.Parameters.Add(par);
				}
			}
			return cmd;
		}
		public XmlDocument ExecuteXml(SqlCommand cmd, Transaction ts)
		{
			XmlDocument xd = null;
			XmlReader xr = null;
			try
			{
				if (cmd.Connection.State != ConnectionState.Open)
				{
					cmd.Connection.Open();
				}
				if (ts != null)
				{
					cmd.Connection.EnlistTransaction(ts);
				}
				xr = cmd.ExecuteXmlReader();
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
				if (ts != null)
				{
					ts.Rollback();
				}
			}
			finally
			{
				cmd.Connection.Close();
			}
			xd = new XmlDocument();
			xd.AppendChild(xd.CreateElement("root"));
			//xr.ReadStartElement("row");
			xd.Load(xr);
			xr.Close();
			return xd;
		}
	}
}
