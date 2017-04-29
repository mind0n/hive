using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Common
{
	public class Communicate : ICommunicate
	{
		[TraceBehavior]
		public string Echo(string content)
		{
			return string.Concat("Echo: ", content);
		}
	}
}
