using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace ULib.Executing
{
	public class ExecuteParameters : Dict<string, ExecuteParameter>
	{
		public static string XmlTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<begin>\r\n{0}\r\n</begin>\r\n";
		public void Cache(string file = null)
		{
			if (!string.IsNullOrEmpty(file))
			{
				List<string> lines = new List<string>();
				foreach (KeyValuePair<string, ExecuteParameter> i in this)
				{
					string key = i.Key;
					ExecuteParameter val = i.Value;
					if (val.IsCommand)
					{
						CommandNode cmd = (CommandNode)val.Value;
						//cmd.Id = key;
						lines.Add(cmd.ToXml());
					}
				}
				string content = string.Format(XmlTemplate, string.Join("\r\n", lines.ToArray()));
				File.WriteAllText(file, content);
			}
		}

		public void Load(string file = null)
		{
			if (!string.IsNullOrEmpty(file) && File.Exists(file))
			{
				string xml = File.ReadAllText(file);
				if (!string.IsNullOrEmpty(xml))
				{
					XDocument doc = XDocument.Parse(xml);
					if (doc != null)
					{
						XElement root = doc.Root;
						foreach (XElement i in root.Elements())
						{
							CommandNode cmd = Executor.CreateCmd(i);
							if (cmd != null)
							{
								//ExecuteParameter par = new ExecuteParameter { Name = cmd.Id, Value = cmd, IsCondition = false };
								ExecuteParameter par = Executor.Instance.GetVar(cmd.Id);
								if (par == null || !par.IsCommand)
								{
									par = new ExecuteParameter { Name = cmd.Id, Value = cmd, IsCondition = false };
									Executor.Instance.SetVar(cmd.Id, par);
								}
								else
								{
									CommandNode extCmd = (CommandNode)par.Value;
									extCmd.Parse(cmd);
								}
								//Executor.Instance.SetVar(cmd.Id, par);
							}
						}
					}
				}
			}

		}

		//private static string GetValidFile(string file)
		//{
		//    if (string.IsNullOrEmpty(file))
		//    {
		//        file = AppDomain.CurrentDomain.BaseDirectory + "ParameterBackup.xml";
		//    }
		//    return file;
		//}
	}

}
