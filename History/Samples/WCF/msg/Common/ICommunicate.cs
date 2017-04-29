using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common
{
	[ServiceContract]
	public interface ICommunicate
	{
		[OperationContract]
		string Echo(string content);
	}
}
