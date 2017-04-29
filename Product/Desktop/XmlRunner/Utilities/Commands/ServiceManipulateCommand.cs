using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.ServiceProcess;
using System.Threading;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Utilities.Commands
{
	public class ServiceManipulateCommand : CommandNode
	{

		public string Service_Name = "{0}";
		public bool Is_Running;
		public int Interval = 1000;
		public override string Name
		{
			get
			{
				return string.Format("Service {0} on {1} should be {2}", Service_Name, CommandExecutionContext.Instance.CurrentMachine.Address, Is_Running ? "running" : "stopped");
			}
		}

		public override CommandResult Execute(ULib.Controls.CommandTreeViewNode node = null)
		{
			CommandResult result = new CommandResult();
			MachineSettings machine = CommandExecutionContext.Instance.CurrentMachine;
			ImpersonateUser iu = new ImpersonateUser();
			try
			{
				iu.Impersonate(machine.Domain, machine.Username, machine.Password);
				result = ExecuteInternal();
				iu.Undo();
			}
			catch (Exception e)
			{
				result.LastError = e;
			}
			return result;
		}

		private CommandResult ExecuteInternal()
		{
			CommandResult result = new CommandResult();
			if (Is_Running)
			{
				while (!isCancelling)
				{
					using (ServiceController sc = new ServiceController(Service_Name, CommandExecutionContext.Instance.CurrentMachine.Address))
					{
						if (sc.Status == ServiceControllerStatus.Running)
						{
							break;
						}
					}
					Thread.Sleep(Interval);
				}
			}
			else
			{
				while (!isCancelling)
				{
					using (ServiceController sc = new ServiceController(Service_Name, CommandExecutionContext.Instance.CurrentMachine.Address))
					{
						if (sc.Status == ServiceControllerStatus.Stopped)
						{
							break;
						}
					}
					Thread.Sleep(Interval);
				}
			}
			base.AccomplishCancellation();
			return result;
		}

		public override CommandNode Clone()
		{
			ServiceManipulateCommand cmd = new ServiceManipulateCommand { Service_Name = this.Service_Name };
			base.CopyChildren(cmd);
			return cmd;
		}

	}
	public class ImpersonateUser
	{
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LogonUser(
		String lpszUsername,
		String lpszDomain,
		String lpszPassword,
		int dwLogonType,
		int dwLogonProvider,
		ref IntPtr phToken);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public extern static bool CloseHandle(IntPtr handle);
		private static IntPtr tokenHandle = new IntPtr(0);
		private static WindowsImpersonationContext impersonatedUser;
		// If you incorporate this code into a DLL, be sure to demand that it
		// runs with FullTrust.
		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		public void Impersonate(string domainName, string userName, string password)
		{
			//try
			{
				// Use the unmanaged LogonUser function to get the user token for
				// the specified user, domain, and password.
				const int LOGON32_PROVIDER_DEFAULT = 0;
				// Passing this parameter causes LogonUser to create a primary token.
				const int LOGON32_LOGON_INTERACTIVE = 2;
				tokenHandle = IntPtr.Zero;
				// ---- Step - 1
				// Call LogonUser to obtain a handle to an access token.
				bool returnValue = LogonUser(
				userName,
				domainName,
				password,
				LOGON32_LOGON_INTERACTIVE,
				LOGON32_PROVIDER_DEFAULT,
				ref tokenHandle); // tokenHandle - new security token
				if (false == returnValue)
				{
					int ret = Marshal.GetLastWin32Error();
					throw new System.ComponentModel.Win32Exception(ret);
				}
				// ---- Step - 2
				WindowsIdentity newId = new WindowsIdentity(tokenHandle);
				// ---- Step - 3
				{
					impersonatedUser = newId.Impersonate();
				}
			}
		}
		// Stops impersonation
		public void Undo()
		{
			impersonatedUser.Undo();
			// Free the tokens.
			if (tokenHandle != IntPtr.Zero)
			{
				CloseHandle(tokenHandle);
			}
		}
	}  
}
