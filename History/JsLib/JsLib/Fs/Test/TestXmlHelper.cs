using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Fs.Core;
using Fs.Xml;

namespace Fs.Test
{
	public class TestFs
	{
		public static void TestXmlHellper()
		{
			XmlDocument xd = new XmlDocument();
			XReader xr = new XReader(xd);
			xr["abc"]["test"]["$attr"].Value = "ok";
			xr["abc"]["test"].Value = "success";
			xr["abc"]["test"]["success"].Value = "done!";
			xr.Save("d:\\temp.xml");
			xr = new XReader("d:\\temp.xml");
			Logger.Log(xr["abc"]["test"]["$attr"].Value);
			Logger.Log(xr["abc"]["test"]["success"].Value);
			Logger.Log(xr["abc"]["test"].Value);
		}
	}
}
