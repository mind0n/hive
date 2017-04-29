using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joy.Server.Errors
{
	public class ServerErrorCode
	{
		public const string MethodNull = "1";
		public const string InvalidHttpMethod = "2";
		public const string InvalidNormalRequestMethod = "3";
	}
}