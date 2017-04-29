using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	public class Communicate : ICommunicate
	{
		public string Echo(string content)
		{
			return string.Concat("Echo: ", content);
		}
	}
}
