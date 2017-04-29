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
using ULib.Exceptions;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Threading;

namespace ULib.Executing.Commands.Data
{
	[Command(IsConfigurable = true, IsExecutable = true)]
	public class RunJobCommand : CommandNode
    {
		[ParameterAttribute(IsRequired = true)]
		public string ConnectionId;

		[ParameterAttribute(IsRequired = true)]
		public string JobName;

		public override string Name
        {
            get
            {
				return string.Format("Run Job {1} with connection ({0})", GetConnectionString(ConnectionId), Executor.Instance.ParseIdString(JobName));
            }
        }
		public int IdleTimeout = 30;

		public int ExecuteTimeout = 3600;


		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			string jobname = Executor.Instance.ParseIdString(JobName);
			string[] jobs = jobname.Split(';');
			foreach (string i in jobs)
			{
				CommandResult r = RunJob(Executor.Instance.ParseIdString(i));
				if (r.LastError != null && rlt.LastError == null)
				{
					rlt.LastError = r.LastError;
				    rlt.ConfirmExecution = rlt.ConfirmExecution || r.ConfirmExecution;
					Logger.Log(r.LastError.ToString());
				}
			}								
		}
		private CommandResult RunJob(string jobname)
		{
			CommandResult rlt = new CommandResult();
			try
			{
				SqlConnection conn = GetConnection(ConnectionId);
				if (conn != null)
				{
					jobname = Executor.Instance.ParseIdString(jobname);
					if (!string.IsNullOrEmpty(jobname))
					{
						ServerConnection sconn = new ServerConnection(conn);
						Server server = new Server(sconn);
						if (server.JobServer.Jobs.Contains(jobname))
						{
							Logger.Log2File("-----------------------------------------------------------------------------------");
							Logger.Log2File(string.Format("-- Preparing to run job: {0}", jobname));
							OutputHandler.Handle(string.Format("Preparing to run job: {0}", jobname));
							OutputHandler.Handle(server.JobServer.Name);
							Job job = server.JobServer.Jobs[jobname];
							if (job.State == SqlSmoState.Existing)
							{
								job.Start();
								job.Refresh();
								OutputHandler.Handle(string.Format("Job {0} status: {1}", jobname, job.CurrentRunStatus.ToString()));
								DateTime stime = DateTime.Now;
								while (job.CurrentRunStatus == JobExecutionStatus.Idle)
								{
									if (DateTime.Now - stime > TimeSpan.FromSeconds(IdleTimeout))
									{
										Exception ex = new TimeoutException(string.Format("Job {0} exceeded the idle monitoring timeout period. ({1} seconds)",
											jobname, IdleTimeout));
										ExceptionHandler.Handle(ex);
										rlt.LastError = ex;
									    rlt.ConfirmExecution = true;
										return rlt;
									}
									job.Refresh();
									Thread.Sleep(100);
								}
								OutputHandler.Handle(string.Format("Job {0} status: {1}", jobname, job.CurrentRunStatus.ToString()));
								stime = DateTime.Now;
								while (job.CurrentRunStatus == JobExecutionStatus.Executing || job.CurrentRunStatus == JobExecutionStatus.PerformingCompletionAction)
								{
									if (DateTime.Now - stime > TimeSpan.FromSeconds(ExecuteTimeout))
									{
										Exception ex = new TimeoutException(string.Format("Job {0} exceeded the execution monitoring timeout period. ({1} seconds)",
											jobname, ExecuteTimeout));
										ExceptionHandler.Handle(ex);
										rlt.LastError = ex;
									    rlt.ConfirmExecution = true;
										return rlt;
									}
									job.Refresh();
									Thread.Sleep(100);
								}
								OutputHandler.Handle(string.Format("Job {0} Completed", jobname));
							}
							else
							{
								rlt.LastError = new InvalidSmoOperationException(string.Format("Cannot start job {0} in state {1}", jobname, job.State.ToString()));
							}
						}
					}
					else
					{
						rlt.LastError = new ArgumentNullException("Job name missing");
					}
				}
			}
			catch (Exception e)
			{
				rlt.LastError = e;
			}
			return rlt;
		}

		public static string GetConnectionString(string connectionId, string database = null)
		{
			ExecuteParameter connPar = Executor.Instance.GetVar(connectionId);
			if (connPar != null)
			{
				SqlConnectCommand cmd = connPar.Value as SqlConnectCommand;
				string sqlConnectionString = string.IsNullOrEmpty(database)
					? string.Format("Data Source={0};{1}", cmd.Server, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd))
					: string.Format("Data Source={0};Initial Catalog={1};{2}", cmd.Server, database, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
				return sqlConnectionString;
			}
			return "[Unknown connection]";
		}
		public static SqlConnection GetConnection(string connectionId, string database = null)
		{
			ExecuteParameter connPar = Executor.Instance.GetVar(connectionId);
			if (connPar != null)
			{
				SqlConnectCommand cmd = connPar.Value as SqlConnectCommand;
				string sqlConnectionString = string.IsNullOrEmpty(database)
					? string.Format("Data Source={0};{1}", cmd.Server, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd))
					: string.Format("Data Source={0};Initial Catalog={1};{2}", cmd.Server, database, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
				SqlConnection conn = new SqlConnection(sqlConnectionString);
				return conn;
			}
			else
			{
				throw new ArgumentNullException(string.Format("Sql connection id '{0}' not found.", connectionId));
			}
		}

    }
}
