using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface;

namespace WcfService
{
	public class CalcService : ICalcService
	{
		public int Add(int num1, int num2)
		{
			return num1 + num2;
		}
	}
}
