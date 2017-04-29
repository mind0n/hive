using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Wcf.Interface;

namespace Wcf.Proxy
{
	public class CalcProxy : ClientBase<ICalcService>, ICalcService
	{
		public CalcProxy(WSHttpBinding bind, EndpointAddress addr) : base(bind, addr) { }
		public int Add(int num1, int num2)
		{
			return base.Channel.Add(num1, num2);
		}
	}
}
