using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;
using System.Data.SqlClient;

namespace ULib.Executing.Commands.Data
{
    public class BackupDBCommand : CommandNode
    {
		const string templateSql = @"ALTER DATABASE {0} SET RECOVERY FULL;
                            BACKUP DATABASE {0} TO DISK = N'{1}' WITH FORMAT";

		[ParameterAttribute(IsRequired = true)]
		public string Database;
		[ParameterAttribute(IsRequired = true)]
		public string ConnectionId;
		[ParameterAttribute(IsRequired = true)]
		public string File;
        public override string Name
        {
            get
            {
				ExecuteParameter connPar = Executor.Instance.GetVar(ConnectionId);
				string sqlConnectionString = null;
				if (connPar != null)
				{
					SqlConnectCommand cmd = connPar.Value as SqlConnectCommand;
					if (!string.IsNullOrEmpty(Database))
					{
						sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};{2}", cmd.Server, Database, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
					}
					else
					{
						sqlConnectionString = string.Format("Data Source={0};{1}", cmd.Server, cmd.IsTrusted ? "Integrated Security=True" : string.Format("user={0};pwd={1};", cmd.User, cmd.Pwd));
					}
				}

                return string.Format("Backup database {0} with connection {1} ({2})", Database, ConnectionId, sqlConnectionString);
            }
            set
            {
                base.Name = value;
            }
        }
		public override void Run(CommandResult rlt, bool isPreview = false)
		{
            ExecuteSqlCommand.ExecuteSql(ConnectionId, Database, string.Format(templateSql, Executor.Instance.ParseIdString(Database), Executor.Instance.ParseIdString(File)));
		}

        public override CommandNode Clone()
        {
			return new BackupDBCommand { Id = Id, Database = Database, ConnectionId = ConnectionId, File = File };
        }
    }
}
