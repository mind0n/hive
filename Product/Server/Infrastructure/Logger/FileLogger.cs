using Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
	public class FileLogger : Logger
	{
		protected string file { get; private set; }

		public FileLogger() : base()
		{
			Init();
		}

		public FileLogger(object config) : base(config)
		{
			Init();
		}

		private void Init()
		{
			var basedir = Dobj.Exists(settings, "folder") ? Dobj.Get<string>(settings, "folder") : string.Empty;
			file = LogExtensions.ShiftFile("log", basedir);
			queue.OnDequeue += write2file;
		}

		private void write2file(Entry entry)
		{
			var content = FormatContent(entry);
			var msg = FormatMessage(entry, content);
			File.AppendAllText(file, msg);
		}

		protected virtual string FormatMessage(dynamic entry, dynamic content)
		{
			return $"[{entry.Time.ToString("yyyy/MM/dd HH:mm:ss fff")}]{entry.Session}({entry.Duration}){entry.Category}:\t{entry.Source}>\t{content}\r\n";
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
