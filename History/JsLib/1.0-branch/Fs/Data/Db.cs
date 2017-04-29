using System;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Timers;
using Fs.Native.Windows;
using System.Threading;

namespace Fs.Data
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
