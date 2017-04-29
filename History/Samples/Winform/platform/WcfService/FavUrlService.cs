using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface;
using System.Xml.Linq;
using Wcf.Interface.DataSchema;
using Wcf.Service.Executors;
using System.Collections.ObjectModel;
using System.ServiceModel;
using Wcf.Interface.Faults;

namespace Wcf.Service
{
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
}
