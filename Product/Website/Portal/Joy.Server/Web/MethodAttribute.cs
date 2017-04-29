using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joy.Server.Web
{
	public class MethodAttribute : Attribute
	{
		public bool IsPage;
	}
}