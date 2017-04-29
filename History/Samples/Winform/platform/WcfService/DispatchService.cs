using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using ULib.Core;
using Wcf.Interface;
using Wcf.Interface.DataSchema;
using Wcf.Interface.Faults;
using Wcf.Service.Executors;

namespace Wcf.Service
{
	// Service class which implements the service contract.
	// Added code to write output to the console window
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class DispatchService : IDispatchService
	{
		public void Dispatch(string data)
		{
			WcfActions actions = WcfAction.Parse(data);
			ExecuteResultSet result = ActionExecutor.ExecuteActions(actions);
		}
		public string GetUrls()
		{
			WcfActions actions = WcfAction.ParseEmpty(ActionNames.GetUrlsAction);
			ExecuteResultSet result = ActionExecutor.ExecuteActions(actions);
			return result.ToXml();
		}
		public void RaiseCustomError()
		{
			FaultException e = new MyException();
			e.Source = "ok";
			throw e;
		}
	}
	public class CustomUserNameValidator : UserNamePasswordValidator
	{
		// This method validates users. It allows in two users, test1 and test2 
		// with passwords 1tset and 2tset respectively.
		// This code is for illustration purposes only and 
		// MUST NOT be used in a production environment because it is NOT secure.	
		public override void Validate(string userName, string password)
		{
			if (null == userName || null == password)
			{
				throw new ArgumentNullException();
			}

			if (!(userName == "test1" && password == "1tset") && !(userName == "test2" && password == "2tset"))
			{
				throw new FaultException("Unknown Username or Incorrect Password");
			}
		}
	}
}
