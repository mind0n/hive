using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Core;


namespace Logger
{
	public class Entry : Dobj
	{
		public Entry()
		{
			dynamic self = this;
			self.UtcTime = DateTime.UtcNow;
			self.Time = DateTime.Now;
		}
	}

	public enum Level
	{
		Debug,
		Info,
		Error
	}
}

