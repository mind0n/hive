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
using ULib.Executing.Commands.Data;
using ULib.Executing.Commands.Common;

namespace ULib.Executing.Commands.OS
{
	public class ServiceCommand : CommandNode
	{
		[ParameterAttribute(IsRequired = true)]
		public string ServiceId;
		[ParameterAttribute(IsRequired = true)]
		public string ConnectionId;
		public bool Start;
		public int TimeoutSecond;

		public override string Name
		{
			get
			{
				string connect = string.Empty;
				ConnectCommand cn = Executor.Instance.Get<ConnectCommand>(ConnectionId);
				if (cn != null)
				{
					connect = cn.Server;
				}
				return string.Format("{0} service {1} on {2}", Start ? "Start" : "Stop", Executor.Instance.GetString(ServiceId), connect);
			}
		}

		public ServiceCommand()
		{
			TimeoutSecond = 90;
		}

		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			ConnectCommand cn = Executor.Instance.Get<ConnectCommand>(ConnectionId);
			
			ImpersonateUser iu = new ImpersonateUser();
				iu.Impersonate(cn.Domain, cn.User, cn.Pwd);
				ExecuteInternal(cn);
				iu.Undo();
		}

		private void ExecuteInternal(ConnectCommand cn)
		{
			using (ServiceController sc = new ServiceController(Executor.Instance.GetString(ServiceId), cn.Server))
			{
				if (Start)
				{
					sc.Start();
					sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(TimeoutSecond));
				}
				else
				{
					sc.Stop();
					sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(TimeoutSecond));
				}
			}
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



		/// <summary>
		/// If you incorporate this code into a DLL, be sure to demand that it
		/// runs with FullTrust.
		/// </summary>
		/// <param name="domainName">Domain name</param>
		/// <param name="userName">Username</param>
		/// <param name="password">Password</param>
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
					throw new global::System.ComponentModel.Win32Exception(ret);
				}
				// ---- Step - 2
				WindowsIdentity newId = new WindowsIdentity(tokenHandle);
				// ---- Step - 3
				{
					impersonatedUser = newId.Impersonate();
				}
			}
		}

		/// <summary>
		/// Stops impersonation
		/// </summary>
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
