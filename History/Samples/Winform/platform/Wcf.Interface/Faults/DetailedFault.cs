using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Wcf.Interface.Faults
{
	public class MyDetail
	{
		public string Content = "This is exception details.";
	}
	public class MyException : FaultException<MyDetail>
	{
		public string Type = "My Exception";
		public MyException() : base(new MyDetail()) { }
	}
}
