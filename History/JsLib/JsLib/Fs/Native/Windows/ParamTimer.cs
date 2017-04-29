using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Fs.Entities;

namespace Fs.Native.Windows
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
