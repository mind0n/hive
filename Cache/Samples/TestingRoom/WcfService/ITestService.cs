using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
	[ServiceContract]
	public interface ITestService
	{
		[OperationContract]
		[FaultContract(typeof(FaultException))]
		void TestError();

		[OperationContract]
		string Test();

		[OperationContract]
		object TestObject();

	    [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
	    string TestHttp();
	}
}
