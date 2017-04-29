using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Joy.Server.Entities;

namespace Joy.Server.Native.Windows
{
	public class ParamTimer : Timer
	{
		public DictParams DictParams = new DictParams();
		public object Param;
		//public List<object> Params = new List<object>();
		public ParamTimer(double interval) : base(interval) { }
		public ParamTimer() : base(32) { }
	}
}
