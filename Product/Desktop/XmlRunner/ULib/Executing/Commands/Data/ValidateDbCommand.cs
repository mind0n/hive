using ULib.DataSchema;
using System.Data;
using System.Windows.Forms;
using System;
using ULib.Exceptions;

namespace ULib.Executing.Commands.Data
{
	[Command(IsPreviewRun = true, IsConfigurable = false, IsSkipAfterStart=true)]
	public class ValidateDbCommand : CommandNode
	{
		public bool IsPartitioned;
		public string ConnectionId;
		public string Database;
        public bool IsNoException
        {
            get
            {
                return Error == null;
            }
        }
		public string ErrorMsgFormat = "Database {1} {0} been partitioned.\r\nDo you want to run the tasks again?";
		bool isError;
		public string ErrorMsg
		{
			get
			{
				if (!isError)
				{
					return string.Format(ErrorMsgFormat, IsPartitioned ? "has not" : "has already", Database);
				}
				else
				{
					return "Exception occurred while validating the database.";
				}
			}
		}
		public override string Name
		{
			get
			{
				return string.Format("Check whether {0} {1} been partitioned", Database, IsPartitioned ? "has" : "has not");
			}
		}

		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			rlt.ShouldStop = false;
			try
			{
				isError = false;
				SqlConnectCommand cn = Executor.Instance.Get<SqlConnectCommand>(ConnectionId);
				DataSet ds = ExecuteSqlCommand.ExecuteSql(ConnectionId, Database,
					"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PartitionTables]') AND type in (N'U')) select * from PartitionTables GO", true);
				if (ds != null)
				{
					if (IsPartitioned)
					{
						if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
						{
							rlt.ShouldStop = true;
							rlt.ValidationFailed = true;
						}
					}
					else
					{
						if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
						{
							rlt.ShouldStop = true;
							rlt.ValidationFailed = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
                this.Error = ex;
				isError = true;
				rlt.LastError = ex;
				rlt.ShouldStop = true;
				rlt.ValidationFailed = true;
			}
		}
	}
}
