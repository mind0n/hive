using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			TestQueue t = new TestQueue();
			t.Start();
			t.Run(10, false);
			t.LookRight();
			Console.ReadKey();
		}
	}
}
