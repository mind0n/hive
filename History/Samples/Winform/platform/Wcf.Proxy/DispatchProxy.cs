using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface;
using System.ServiceModel;
using System.Xml.Linq;
using Wcf.Interface.Faults;

namespace Wcf.Proxy
{
	public class DispatchProxy : ClientBase<IDispatchService>, IDispatchService
	{
		public DispatchProxy(WSHttpBinding bind, EndpointAddress addr) : base(bind, addr) { }

		public void Dispatch(string data)
		{
			base.Channel.Dispatch(data);
		}

		public string GetUrls()
		{
			string result = base.Channel.GetUrls();
			return result;
		}

		public void RaiseCustomError()
		{
			base.Channel.RaiseCustomError();
		}
	}
}
