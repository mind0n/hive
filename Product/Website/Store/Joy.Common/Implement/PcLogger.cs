using Joy.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Joy.Common.Implement
{
	public class PcLogger : JLogger
	{
		public PcLogger()
		{
			base.LogMethods += WriteFile;
			if (File.Exists(LogFile))
			{
				DateTime n = DateTime.Now;
				string desFile =   string.Concat(LogFile.PathWithoutName(), string.Format("Log_{0}_{1}_{2}_{3}_{4}_{5}_{6}.txt", n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second, n.Millisecond));
				File.Move(LogFile, desFile);
			}
		}

		public void WriteFile(string msg)
		{
			File.AppendAllText(LogFile, msg);
		}
	}
}
