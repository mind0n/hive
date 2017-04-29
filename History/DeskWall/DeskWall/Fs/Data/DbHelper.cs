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
			XReader xr = n["configuration"]["connections"];

			foreach (XReader item in xr.Children)
			{
				try
				{
					string decoder = item["$decoder"].Value;
					string decodeMethod = item["$method"].Value;
					Type typ = Type.GetType(item["$type"].Value);
					Db db = (Db)Activator.CreateInstance(typ); //new Db(item.Value, item["$type"].Value, item["$url"].Value);
					if (string.IsNullOrEmpty(decoder))
					{
						db.ConnStr = item.Value;
					}
					else
					{
						string decoded;
						if (string.IsNullOrEmpty(decodeMethod))
						{
							decodeMethod = "Decode";
						}

						decoded = (string)ClassHelper.Invoke(Type.GetType(decoder), decodeMethod, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, new object[] { item.Value });
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
	public abstract class Db
	{
		public delegate void TableEnumerationHandler(DataRow row, string tableNameCol);
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
		public int Execute(string Sql)
		{
			int RowCount;
			DbConnection Connection = GetConnection();
			DbCommand cm = GetCommand(Sql, Connection);
			Connection.Open();
			RowCount = cm.ExecuteNonQuery();
			Connection.Close();
			return RowCount;
		}
		public IDataReader ExecuteReader(string Sql)
		{
			IDataReader rlt;
			DbConnection Connection = GetConnection();
			DbCommand cm = GetCommand(Sql, Connection);
			Connection.Open();
			rlt = cm.ExecuteReader(CommandBehavior.CloseConnection);
			return rlt;
		}
		public object ExecScalar(string Sql)
		{
			object Rlt;
			DbConnection Connection = GetConnection();
			DbCommand cm = GetCommand(Sql, Connection);
			Connection.Open();
			Rlt = cm.ExecuteScalar();
			Connection.Close();
			return Rlt;
		}
		public void EnumTables(TableEnumerationHandler callback)
		{
			DataTable dt = GetSchema("tables");
			foreach (DataRow row in dt.Rows)
			{
				callback(row, "TABLE_NAME");
			}
		}
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
		public abstract DbCommand GetCommand(string Sql, DbConnection Connection);
		public abstract DbConnection GetConnection();
	}
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
		public override DbCommand GetCommand(string Sql, DbConnection connection)
		{
			return new SqlCommand(Sql, connection as SqlConnection);
		}
		public override DbConnection GetConnection()
		{
			Connection = new SqlConnection(ConnStr); ;
			return Connection;
		}
	}
	public class DbAccess : Db
	{
		public override DataAdapter GetAdapter(string Sql)
		{
			return new OdbcDataAdapter(Sql, GetConnection() as OdbcConnection);
		}
		public override DbCommand GetCommand(string Sql, DbConnection connection)
		{
			return new OdbcCommand(Sql, connection as OdbcConnection);
		}
		public override DbConnection GetConnection()
		{
			Connection = new OdbcConnection(ConnStr);
			return Connection;
		}
	}
}
