using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Wcf.Interface
{
	[ServiceContract()]
	public interface ICalcService
	{
		[OperationContract()]
		int Add(int num1, int num2);
	}
}
