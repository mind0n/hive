using System;
using System.Collections.Generic;
using System.Text;

namespace Codgen
{
	public class TestXml
	{
		public static void Run()
		{
			CodgenXml cx = new CodgenXml();
			string path = "CodgenTemplates/CSharp/ClassBuilder/ClassBuilder.xml";
			Console.WriteLine(cx.ReadFile(path));
		}
		public static string GenerateMethods(string template)
		{
			StringBuilder rlt = new StringBuilder();
			List<string> list = new List<string>();
			list.Add("Add");
			list.Add("Update");
			list.Add("Delete");
			list.Add("View");
			foreach (string item in list)
			{
				StringBuilder sf = new StringBuilder(template);
				sf.Replace(">Opcode<", item);
				rlt.Append(sf.ToString());
			}
			return rlt.ToString();
		}
	}
}