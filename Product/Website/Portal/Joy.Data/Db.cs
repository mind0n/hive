using System;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Timers;
using System.Threading;
using Joy.Core;
using System.Collections.Generic;

namespace Joy.Data
{
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



		public virtual string DbInfo
		{
			get { return dbinfo; }
			set { dbinfo = value; }
		}protected string dbinfo;

		public virtual string ConnStr
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
		public DataTable PagedQuery(
			string primaryFieldName,
			int curtPage,
			int pageSize,
			string[] fieldClauses,
			string[] tableClauses,
			string whereClause,
			string[] orderClauses,
			string[] groupClauses
		)
		{
			string primaryField = SqlHelper.MakeSafeFieldNameSql(primaryFieldName);
			string fieldClause = SqlHelper.MakeSafeFieldNameSql(fieldClauses);
			string tableClause = SqlHelper.MakeSafeFieldNameSql(tableClauses);
			string orderClause = SqlHelper.MakeSafeFieldNameSql(orderClauses);
			string groupClause = SqlHelper.MakeSafeFieldNameSql(groupClauses);
			return PagedQuery(primaryField, curtPage.ToString(), pageSize.ToString(), fieldClause, tableClause, whereClause, orderClause, groupClause);
		}
		public abstract void GetReady(DbConfig cfg);
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
		public ExecuteResult Execute(string Sql, DbConnection Connection, Transaction Tst, string scopeIdentityTable = null)
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
				if (!string.IsNullOrEmpty(scopeIdentityTable))
				{
					int id = ExecScalar<int>(string.Concat("select @@IDENTITY from ", scopeIdentityTable));
					return new ExecuteResult(id);
				}
				else
				{
					return new ExecuteResult(RowCount);
				}
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
		public T ExecuteResult<T>(string Sql)
		{
			DataTable r = GetDataTable(Sql);
			if (r != null && r.Rows.Count > 0)
			{
				try
				{
					return (T)Convert.ChangeType(r.Rows[0][0], typeof(T));
				}
				catch (Exception e)
				{
					ErrorHandler.Handle(e);
					return default(T);
				}
			}
			else
			{
				return default(T);
			}
		}
		public int ExecuteInsert(string table, string textField, string idField)
		{
			string gid = Guid.NewGuid().ToString();
			string sqlInsert = string.Concat("insert into ", table, "(", textField, ") values('", gid, "')");
			string sqlGetInserted = string.Concat("select ", idField, " from ", table, " where ", textField, " like '", gid, "'");
			Execute(sqlInsert);
			return ExecScalar<int>(sqlGetInserted);
		}
		public int ExecuteNonQuery(string sql, params string[] args)
		{
			return Execute(string.Format(sql, args));
		}
		public int Execute(string Sql, string scopeIdentityTable = null)
		{
			ExecuteResult r = Execute(Sql, null, null, scopeIdentityTable);
			return r.IntRlt;
		}
		public ExecuteResult ExecuteReader(string Sql, Func<IDataReader, object> callback, DbConnection Connection, Transaction Tst)
		{
			IDataReader reader = null;
			bool isGetRltDone = false;
			if (callback == null)
			{
				return new ExecuteResult { Exception = new ArgumentNullException("callback") };
			}
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
				ExecuteResult r = null;
				using (reader = cm.ExecuteReader(CommandBehavior.CloseConnection))
				{
					isGetRltDone = true;
					r = new ExecuteResult { ObjRlt = callback(reader) };
				};
				return r;
			}
			catch (TransactionException err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				Exceptions.LogOnly(err);
				return new ExecuteResult { Exception = err };
			}
			catch (Exception err)
			{
				if (isGetRltDone)
				{
					throw;
				}
				else
				{
					if (Tst != null)
					{
						Tst.Rollback(err);
					}
					Exceptions.LogOnly(err);
					return new ExecuteResult { Exception = err };
				}
			}
			finally
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
					{
						reader.Close();
					}
					reader.Dispose();
				}
				if (Connection != null)
				{
					Connection.Close();
				}
			}
		}
		public object ExecuteValue(string sql)
		{
			var table = GetDataTable(sql);
			if (table != null && table.Rows.Count > 0)
			{
				return table.Rows[0][0];
			}
			return null;
		}
		public List<T> ExecuteList<T>(string Sql) where T : new()
		{
			ExecuteResult rlt = ExecuteReader(Sql, new Func<System.Data.IDataReader, object>(delegate(IDataReader reader)
			{
				return reader.Fill<T>();
			}));
			if (rlt.IsNoException)
			{
				return rlt.ObjRlt as List<T>;
			}
			else
			{
				return null;
			}
		}
		public ExecuteResult ExecuteReader(string Sql, Func<IDataReader, object> callback)
		{
			return ExecuteReader(Sql, callback, null, null);
		}
		public T ExecScalar<T>(string Sql, DbConnection Connection, Transaction Tst)
		{
			T rlt;
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
				object returnValue = cm.ExecuteScalar();
				rlt = (T)Convert.ChangeType(returnValue, typeof(T));
				return rlt;
			}
			catch (TransactionException err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				ErrorHandler.Handle(err);
				return default(T);
			}
			catch (Exception err)
			{
				if (Tst != null)
				{
					Tst.Rollback(err);
				}
				ErrorHandler.Handle(err);
				return default(T);
			}
			finally
			{
				Connection.Close();
			}
		}
		public T ExecScalar<T>(string Sql)
		{
			return ExecScalar<T>(Sql, null, null);
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
			return GetConnection(5, 1000);
		}

		public virtual DbConnection GetConnection(int retryTimes, int waitInterval)
		{
			for (int i = 0; i < retryTimes; i++)
			{
				DbConnection rlt = GetConnectionSingleTry();
				if (rlt != null)
				{
					return rlt;
				}
				else
				{
					Thread.Sleep(waitInterval);
				}
			}
			return null;
		}

		public abstract DataColumnCollection GetColumnsInfo(string tableName);
	}
}
