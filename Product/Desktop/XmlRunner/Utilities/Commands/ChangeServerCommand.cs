using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Controls;

namespace Utilities.Commands
{
	[Serializable]
    public class ChangeServerCommand : CommandNode
    {
        public override string Name
        {
            get
            {
				return string.Format("Server Ip:{0}  Account:{1}", Address, Username);
            }
        }
        public string Address = "{0}";
        public string Username = "{1}";
        public string Password;
        public ChangeServerCommand() : base()
        {
            base.Name = "Server Ip: {0} | Account: {1}";
            Type = CommandType.Command;
        }

        public override CommandResult Execute(CommandTreeViewNode node = null)
        {
            CommandResult result = new CommandResult();
            CommandExecutionContext.Instance.CurrentMachine.DomainUsername = Username;
            CommandExecutionContext.Instance.CurrentMachine.Password = Password;
            CommandExecutionContext.Instance.CurrentMachine.Address = Address;
            return result;
        }

		public override CommandNode Clone()
		{
			CommandNode rlt = new ChangeServerCommand { Address = this.Address, Cmd = this.Cmd, Value = this.Value, Password = this.Password, Type = this.Type, Username = this.Username };
			base.CopyChildren(rlt);
			return rlt;
		}

		public override void Reset()
		{
			base.Reset();
		}
	}
}
