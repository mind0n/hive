using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ServiceModel;
using Wcf.Interface.Faults;

namespace Wcf.Interface
{
	[ServiceContract]
	public interface IDispatchService
	{
		[OperationContract]
		void Dispatch(string data);

		[OperationContract]
		string GetUrls();

		[OperationContract]
		[FaultContract(typeof(MyDetail))]
		void RaiseCustomError();
	}
}
