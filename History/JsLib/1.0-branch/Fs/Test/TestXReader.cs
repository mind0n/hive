using System;
using System.Collections.Generic;
using System.Text;
using Fs.Xml;

namespace Fs.Test
{
	public class TestXReader
	{
		public static void Test()
		{
			XReader xr = new XReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Temp.xml");
			Console.WriteLine(xr["root"]["add"]["$name"].Value);
			XReader du = xr.Duplicate<XReader>();
			xr.Reset()["root"]["add"].SetValue("$name", "Modified");
			xr.Reset()["root"].SetValue("new", "Created");
			xr.Reset()["root"]["new"].SetValue("$name", "Newly created");
			xr.Reset()["root"].SetValue("", "Modified Root Content");
			xr.Reset()["root"]["Temp"].SetValue("$test", "ok");
			xr.Reset()["root"]["temp"].SetValue(null, "ok");
			xr.Reset()["root"]["temp"].SetValue(null, "success");
			xr.Save();
			Console.WriteLine(xr.Reset()["root"].Value);
			for (int i = 1; i <= 1; i++)
			{
				Console.WriteLine("============================");
				foreach (XReader child in xr.Reset()["root"])
				{
					Console.WriteLine(child.Name);
				}
				Console.WriteLine("----------------------------");
				xr.Reset()["root"].EnumChilds(delegate(object ii)
				{
					XReader item = (XReader)ii;
					Console.WriteLine(item.Name);
					return true;
				});
				Console.WriteLine("****************************");
			}
			Console.WriteLine(du.NodeContent<object>());
			Console.ReadKey();
		}
	}
}
