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
using System.Web;
using System.Web.UI;
using System.Xml;
//using MySql.Data.MySqlClient;
using Fs.Xml;
using Fs.Reflection;
using Fs.Text;
using System.Transactions;
using System.Xml.XPath;
using System.Timers;
using Fs.Native.Windows;
using System.Threading;

namespace Fs.Data
{
	public class Dbs : IConfigurable
	{
		public static int Count
		{
			get
			{
				return list.Count;
			}
		}
		protected static Dictionary<string, Db> list;
		static Dbs()
		{
			ReadConfigFile();
		}
		public static string Decode(string encoded)
		{
			try
			{
				encoded = encoded.Replace("\r", "");
				encoded = encoded.Replace("\n", "");
				encoded = encoded.Replace("\t", "");
				encoded = encoded.Replace(" ", "");
				return Base64.Decode(encoded);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
				return null;
			}
		}
		protected static void ReadConfigFile()
		{
			//FileStream fs;
			string path = string.Empty, file = "DB.Config";
			Page p = new Page();
			list = new Dictionary<string, Db>();
			XmlDocument x = new XmlDocument();
			try
			{
				//path = p.Server.MapPath(file);
				path = AppDomain.CurrentDomain.BaseDirectory + file;
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
				path = Directory.GetCurrentDirectory() + "\\" + file;
			}
			if (!System.IO.File.Exists(path))
			{
				throw Exceptions.Log("Config file not found: " + path);
			}
			x.Load(path);
			XReader n = new XReader(x);
			
			XReader xr = n.Reset()["configuration"]["connections"];

			foreach (XReader item in xr)
			{
				try
				{
					
					string decoder = item.Restore()["$decoder"].Value;
					string decodeMethod = item.Restore()["$method"].Value;
					Type typ = Type.GetType(item.Restore()["$type"].Value);
					Db db = (Db)Activator.CreateInstance(typ); //new Db(item.Value, item["$type"].Value, item["$url"].Value);
					if (string.IsNullOrEmpty(decoder))
					{
						db.ConnStr = item.Restore().Value;
					}
					else
					{
						string decoded;
						if (string.IsNullOrEmpty(decodeMethod))
						{
							decodeMethod = "Decode";
						}

						decoded = (string)ClassHelper.Invoke(Type.GetType(decoder), decodeMethod, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, new object[] { item.Restore().Value });
						db.ConnStr = decoded;
					}
					list[item.Name] = db;
				}
				catch (Exception err)
				{
					Exceptions.LogOnly(err);
					throw err;
				}
			}
		}
		public static T Use<T>(string DbName) where T : Db
		{
			if (list.Count > 0)
			{
				return list[DbName] as T;
			}
			else
			{
				throw new Exception("Error: Database connection configuration conntent not found.");
			}
		}

		#region IConfigurable Members

		public object RereadConfigFile()
		{
			ReadConfigFile();
			return null;
		}

		#endregion

	}
	public class ExecuteResult
	{
		public enum ResultType : int
		{
			Integer
			, Error
			, DataReader
			, Object
		}
		public Exception Exception;
		public int IntRlt = 0;
		public object ObjRlt = null;
		public IDataReader ReaderRlt = null;
		public ResultType Type;
		public ExecuteResult(object rlt, Exception err)
		{
			Exception = err;
			if (rlt is IDataReader)
			{
				ReaderRlt = rlt as IDataReader;
				Type = ResultType.DataReader;
			}
			else if (rlt is int)
			{
				IntRlt = (int)rlt;
				Type = ResultType.Integer;
			}
			else
			{
				if (err == null)
				{
					ObjRlt = rlt;
					Type = ResultType.Object;
				}
				else
				{
					Exception = err;
					Type = ResultType.Error;
				}
			}
		}
	}
	public abstract class Db
	{
		public delegate void TableEnumerationHandler(DataRow row, string tableNameCol);
		public delegate void ColumnEnumerationHandler(DataRow row, string colNameCol);
		public DbConnection Connection
		{
			get
			{
				if (_connection != null)
				{
					return _connection;
				}
				else
				{
					return GetConnection();
				}
			}
			set
			{
				_connection = value;
			}
		}protected DbConnection _connection;
		
		public string ConnStr
		{
			get
			{
				if (string.IsNullOrEmpty(_connstr))
				{
					throw new Exception("Error: Connection string missing");
				}
				return _connstr;
			}
			set
			{
				_connstr = value;
			}
		}protected string _connstr;
		public Db()
		{
			ConnStr = "";
		}
		public Db(string ConnectionString)
		{
			ConnStr = ConnectionString;
		}
		public DataSet GetDataSet(DataAdapter adapter)
		{
			int RowCount = 0;
			DataSet ds = new DataSet();
			try
			{
				RowCount = adapter.Fill(ds);
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
				RowCount = 0;
				ds.ExtendedProperties["error"] = e;
			}
			return ds;
		}
		public abstract DataTable PagedQuery(string primaryField, string curtPage, string pageSize, string fieldClause, string tableClause, string whereClause, string orderClause, string groupClause);
		public DataTable GetDataTable(string Sql)
		{
			DataSet ds = GetDataSet(GetAdapter(Sql));
			return GetDataTable(ds);
		}
		public DataTable GetDataTable(DataSet dataset)
		{
			if (dataset.Tables != null && dataset.Tables.Count > 0)
			{
				return dataset.Tables[0];
			}
			return null;
		}
		public ExecuteResult Execute(string Sql, DbConnection Connection, Transaction Tst)
		{
			int RowCount;
			if (Connection == null)
			{
				Connection = GetConnection();
			}
			try
			{
				if (Connection.State == ConnectionState.Closed)
				{
					Connection.Open();
				}
				if (Tst != null)
				{
					Connection.EnlistTransaction(Tst);
				}
				DbCommand cm = GetCommand(Sql, Connection);
				RowCount = cm.ExecuteNonQuery();
				return new ExecuteResult(RowCount, null);
			}
			catch (TransactionException err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			catch (Exception err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			finally
			{
				Connection.Close();
			}
		}
		public int Execute(string Sql)
		{
			return Execute(Sql, null, null).IntRlt;
		}
		public ExecuteResult ExecuteReader(string Sql, DbConnection Connection, Transaction Tst)
		{
			IDataReader rlt;
			if (Connection == null)
			{
				Connection = GetConnection();
			}
			try
			{
				if (Connection.State == ConnectionState.Closed)
				{
					Connection.Open();
				}
				if (Tst != null)
				{
					Connection.EnlistTransaction(Tst);
				}
				DbCommand cm = GetCommand(Sql, Connection);
				rlt = cm.ExecuteReader(CommandBehavior.CloseConnection);
				return new ExecuteResult(rlt, null);
			}
			catch (TransactionException err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			catch (Exception err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			finally
			{
				Connection.Close();
			}
		}
		public IDataReader ExecuteReader(string Sql)
		{
			return ExecuteReader(Sql, null, null).ReaderRlt;
		}
		public ExecuteResult ExecScalar(string Sql, DbConnection Connection, Transaction Tst)
		{
			object rlt;
			if (Connection == null)
			{
				Connection = GetConnection();
			}
			try
			{
				if (Connection.State == ConnectionState.Closed)
				{
					Connection.Open();
				}
				if (Tst != null)
				{
					Connection.EnlistTransaction(Tst);
				}
				DbCommand cm = GetCommand(Sql, Connection);
				rlt = cm.ExecuteScalar();
				return new ExecuteResult(rlt, null);
			}
			catch (TransactionException err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			catch (Exception err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult(null, err);
			}
			finally
			{
				Connection.Close();
			}
		}
		public object ExecScalar(string Sql)
		{
			return ExecScalar(Sql, null, null).IntRlt as object;
		}
		public void EnumTables(TableEnumerationHandler callback)
		{
			DataTable dt = GetSchema("tables");
			foreach (DataRow row in dt.Rows)
			{
				callback(row, "TABLE_NAME");
			}
		}
		//public void EnumColumns(string tableName, ColumnEnumerationHandler callback)
		//{
		//}
		public DataTable GetSchema()
		{
			return GetSchema(null);
		}
		public DataTable GetSchema(string collectionName)
		{
			DataTable dt;
			DbConnection cn = GetConnection();
			cn.Open();
			if (string.IsNullOrEmpty(collectionName))
			{
				dt = cn.GetSchema();
			}
			else
			{
				dt = cn.GetSchema(collectionName);
			}
			cn.Close();
			return dt;
		}
		public abstract DataAdapter GetAdapter(string Sql);
		public virtual DbCommand GetCommand(string sql)
		{
			return GetCommand(sql, GetConnection());
		}
		public abstract DbCommand GetCommand(string Sql, DbConnection Connection);
		public abstract DbCommand GetCommand(string Sql, DbConnection Connection, DbTransaction Transaction);
		public abstract DbConnection GetConnectionSingleTry();
		public virtual DbConnection GetConnection()
		{
			return GetConnection(5, 1000); //GetConnectionSingleTry();
		}

		public virtual DbConnection GetConnection(int retryTimes, double waitInterval)
		{
			ParamTimer pt = new ParamTimer();
			pt.Interval = waitInterval;
			pt.Param = null;
			pt.DictParams["retrytimes"] = retryTimes;
			pt.Elapsed += new ElapsedEventHandler(pt_Elapsed);
			pt.Enabled = true;
			pt.Start();
			while (pt.Param == null){
				Thread.Sleep(1000);
			}
			if (pt.Param is DbConnection)
			{
				return pt.Param as DbConnection;
			}
			else
			{
				return null;
			}
		}

		protected void pt_Elapsed(object sender, ElapsedEventArgs e)
		{
			ParamTimer pt = sender as ParamTimer;
			pt.Stop();
			int retryTimes = (int)pt.DictParams["retrytimes"];
			try
			{
				pt.Param = GetConnectionSingleTry();
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err); 
				pt.DictParams["retrytimes"] = retryTimes - 1;
				if (retryTimes > 0)
				{
					pt.Start();
				}
				else
				{
					pt.Param = err;
				}
			}

		}
		public abstract DataColumnCollection GetColumnsInfo(string tableName);
	}
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
			int totalRecords = (int)ExecScalar(sql[1]);
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

	public class DbAccess : Db
	{
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
			int totalRecords = (int)ExecScalar(sql[1]);
			dt.ExtendedProperties["totalrecords"] = totalRecords;
			return dt;
		}
	}
}
