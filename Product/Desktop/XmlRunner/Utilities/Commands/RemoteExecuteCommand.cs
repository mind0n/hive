using System;
using System.Diagnostics;
using System.Management;
using ULib.Controls;
using ULib.DataSchema;

namespace Utilities.Commands
{
	[Serializable]
	public class RemoteExecuteCommand : CommandNode
    {
        public override string Name
        {
            get
            {
                return string.Format(base.Name, Command_Line);
            }
        }
        public string Command_Line = "{0}";
		public int Timeout_Second = 3;
        public RemoteExecuteCommand()
        {
            base.Name = "Execute command: {0}";
        }
        public override CommandResult Execute(CommandTreeViewNode node = null)
        {
			CommandResult rlt = new CommandResult();
			try
			{
				object resultValue = new CommandResult();
				if (string.Equals(CommandExecutionContext.Instance.CurrentMachine.Address, ".") || string.Equals(CommandExecutionContext.Instance.CurrentMachine.Address, "127.0.0.1"))
				{
					ProcessStartInfo info = new ProcessStartInfo();
					info.FileName = "cmd.exe";
					info.Arguments = string.Format("/c {0}", this.Command_Line);
					Process.Start(info);
				}
				else
				{
					resultValue = RemoteExecute(this.Command_Line);
				}
				rlt.ResultValue = resultValue;
			}
			catch (Exception e)
			{
				rlt.LastError = e;
			}
			return rlt;
        }

		public override CommandNode Clone()
		{
			RemoteExecuteCommand rlt = new RemoteExecuteCommand { Command_Line = this.Command_Line };
			base.CopyChildren(rlt);
			return rlt;
		}

		public object RemoteExecute(string command)
		{
			object[] theProcessToRun = { command };
			ConnectionOptions theConnection = new ConnectionOptions();
			theConnection.Timeout = TimeSpan.FromSeconds(Timeout_Second);
			theConnection.Username = CommandExecutionContext.Instance.CurrentMachine.DomainUsername;
			theConnection.Password = CommandExecutionContext.Instance.CurrentMachine.Password;
			ManagementScope theScope = new ManagementScope("\\\\" + CommandExecutionContext.Instance.CurrentMachine.Address + "\\root\\cimv2", theConnection);
			ManagementClass theClass = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
			object rlt = theClass.InvokeMethod("Create", theProcessToRun);
			return rlt;
		}

		public override void Reset()
		{
			base.Reset();
		}
	}
}
