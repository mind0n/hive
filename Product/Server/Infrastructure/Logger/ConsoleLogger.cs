using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
	public class ConsoleLogger : Logger
	{
		public ConsoleLogger() : base()
		{
			queue.OnDequeue += write2console;
		}
		public ConsoleLogger(object settings = null) : base(settings)
		{
			queue.OnDequeue += write2console;
		}

		private void write2console(Entry entry)
		{
			var content = FormatContent(entry);
			Console.WriteLine(content);
		}

		protected virtual string FormatContent(dynamic entry)
		{
			var lines = entry.Message.Split('\n');
			var list = new List<string>();
			for (int i = 0; i < lines.Length; i++)
			{
				var ln = lines[i];
				if (i > 0)
				{
					list.Add($"\t\t{ln.Replace("\r", "").Trim()}");
				}
				else
				{
					list.Add(ln);
				}
			}
			var content = string.Join("\r\n", list.ToArray());
			return content;
		}
	}
}
