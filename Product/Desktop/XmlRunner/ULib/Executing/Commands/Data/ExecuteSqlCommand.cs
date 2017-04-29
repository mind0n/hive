using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data;

namespace ULib.Executing.Commands.Data
{
	[Command( IsPreviewRun=false)]
    public class ExecuteSqlCommand : CommandNode
    {
		public string SqlFile;
		[ParameterAttribute(IsRequired = true)]
		public string ConnectionId;
		[ParameterAttribute(IsRequired = true)]
		public string Database;
		public string Sql;
		public string ResultId;
		public string SqlId;

        public override string Name
        {
            get
            {
				if (!string.IsNullOrEmpty(SqlId))
				{
					return string.Format("Execute SqlId {0}: {1} (Database: {2})", SqlId, Executor.Instance.GetString(SqlId), Database);
				}
				else
				{
					return string.Format("Execute Sql {1}: {0} (Database: {2})", !string.IsNullOrEmpty(SqlFile) ? SqlFile : Sql, string.IsNullOrEmpty(Sql) && string.IsNullOrEmpty(SqlId) ? "File " : string.Empty, Database);
				}
            }
        }

		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			ExecuteSql(rlt);
		}
		public static DataSet ExecuteSql(string connName, string db, string sql, bool isResult = false)
		{
			using (SqlConnection connection = ExecuteSqlCommand.GetConnection(connName, db))
			{
				Logger.Log2File("-----------------------------------------------------------------------------------");
				Logger.Log2File(string.Format("-- Preparing to execute sql:\r\n{0}", sql));
				if (!isResult)
				{
					connection.Open();
					SqlCommand cmd = connection.CreateCommand();
					cmd.CommandTimeout = int.MaxValue;
					cmd.CommandType = global::System.Data.CommandType.Text;
					cmd.CommandText = sql;
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					return null;
				}
				else
				{
					DataSet ds = new DataSet();
					using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
					{
						adapter.Fill(ds);
					}
					return ds;
				}
			}
		}

		private void ExecuteSql(CommandResult rlt)
		{
			try
			{
				SqlConnection conn = GetConnection(ConnectionId, Database);
				if (conn != null)
				{
					if (string.IsNullOrEmpty(Sql) && string.IsNullOrEmpty(SqlId))
					{
						if (File.Exists(SqlFile))
						{
							FileInfo file = new FileInfo(SqlFile);
							string script = file.OpenText().ReadToEnd();
							ServerConnection sconn = new ServerConnection(conn);
							Server server = new Server(sconn);
							Logger.Log2File("-----------------------------------------------------------------------------------");
							Logger.Log2File(string.Format("-- Preparing to execute file sql:\r\n{0}", script));
							if (string.IsNullOrEmpty(ResultId) && string.IsNullOrEmpty(OutputId))
							{
								server.ConnectionContext.ExecuteNonQuery(script);
							}
							else
							{
							    DataSet ds = server.ConnectionContext.ExecuteWithResults(script);
							    SaveResult(rlt, ds);
							}
						}
						else
						{
							rlt.LastError = new FileNotFoundException();
						}
					}
					else
					{
						string sql = Executor.Instance.ParseIdString(Sql);
						if (!string.IsNullOrEmpty(SqlId))
						{
							 sql = Executor.Instance.GetString(SqlId);
						}
						DataSet ds = ExecuteSql(ConnectionId, Database, sql, !string.IsNullOrEmpty(ResultId) || !string.IsNullOrEmpty(OutputId));
						SaveResult(rlt, ds);
					}
				}
			}
			catch (Exception e)
			{
				rlt.LastError = e;
			}
		}

	    private void SaveResult(CommandResult rlt, DataSet ds)
	    {
	        if (!string.IsNullOrEmpty(ResultId))
	        {
	            Executor.Instance.SetVar(ResultId, false, ds);
	        }
	        else if (!string.IsNullOrEmpty(OutputId) && rlt != null)
	        {
	            rlt.ResultValue = ds;
	            rlt.IsCondition = false;
	        }
	    }

	    public static SqlConnection GetConnection(string connectionId, string database = null)
		{
			ExecuteParameter connPar = Executor.Instance.GetVar(connectionId);
			if (connPar != null)
			{
				SqlConnectCommand cmd = connPar.Value as SqlConnectCommand;
				if (!string.IsNullOrEmpty(database))
				{
					string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};{2}", cmd.Server, database, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
					SqlConnection conn = new SqlConnection(sqlConnectionString);
					return conn;
				}
				else
				{
					string sqlConnectionString = string.Format("Data Source={0};{1}", cmd.Server, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
					SqlConnection conn = new SqlConnection(sqlConnectionString);
					return conn;
				}
			}
			else
			{
				throw new ArgumentNullException(string.Format("Sql connection id '{0}' not found.", connectionId));
			}
		}

        public override CommandNode Clone()
        {
			return new ExecuteSqlCommand { Id = Id, SqlFile = SqlFile, ConnectionId = ConnectionId, Database = Database, Sql = Sql, ResultId = ResultId, SqlId = SqlId };
        }
    }
}
