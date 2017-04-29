using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common
{
	public class WcfHost : ServiceHost
	{
		public WcfHost()
			: base()
		{
		}
		public WcfHost(Type type)
			: base(type)
		{
		}
		public WcfHost(object obj)
			: base(obj.GetType())
		{
		}
	}
}
