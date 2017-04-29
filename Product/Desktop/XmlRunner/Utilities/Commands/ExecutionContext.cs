using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;

namespace Utilities.Commands
{
    public class MachineSettings
    {
        public string DomainUsername;
		public string Username
		{
			get
			{
				if (!string.IsNullOrEmpty(DomainUsername))
				{
					string[] list = DomainUsername.Split('\\');
					if (list.Length > 1)
					{
						return list[1];
					}
					else if (list.Length > 0)
					{
						return list[0];
					}
				}
				return string.Empty;
			}
		}
		public string Domain
		{
			get
			{
				if (!string.IsNullOrEmpty(DomainUsername))
				{
					string[] list = DomainUsername.Split('\\');
					if (list.Length > 0)
					{
						return list[0];
					}
				}
				return string.Empty;
			}
		}
        public string Password;
        public string Address;
    }
    public class CommandExecutionContext
    {
        public CommandExecutionContext()
        {
        }
        public static CommandExecutionContext Instance = new CommandExecutionContext();
        public string AgentPath = @"\\192.168.17.52\__Svnlocal\P1Utility\P1UtilityAgent\bin\Debug\P1UtilityAgent.exe";
        public MachineSettings CurrentMachine = new MachineSettings();
        //public List<MachineSettings> Machines = new List<MachineSettings>();
    }

}
