using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Core
{
	[ServiceContract]
	public interface ICoreService
	{
		[OperationContract]
		int Encode(object obj);
	}
}
