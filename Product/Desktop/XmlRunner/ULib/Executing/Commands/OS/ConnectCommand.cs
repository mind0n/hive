using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;

namespace ULib.Executing.Commands.OS
{
	[Command(IsConfigurable = true)]
	public class ConnectCommand : CommandNode
    {
		[ParameterAttribute(IsRequired = true)]
		public string Server;
		[ParameterAttribute(IsRequired = true)]
		public string User;
		[ParameterAttribute(IsRequired = true)]
		public string Pwd;
		[ParameterAttribute(IsRequired = true)]
		public string Domain;
		public override string Name
		{
			get
			{
				return string.Format("Connection to {0} with {1}", Server, User);
			}
			set
			{
				base.Name = value;
			}
		}
        public override CommandNode Clone()
        {
			return new ConnectCommand { Id = Id, Server = Server, User = User, Pwd = Pwd, Domain = Domain };
        }
    }
}
