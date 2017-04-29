using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fs.Date;

namespace OAWidgets
{
	public class Processor
	{
		public static string SunGLMonitorResetDate(string content)
		{
			return content.Replace("$Today$", DateHelper.OfficialDateString(DateTime.Now, "MM DD", DateHelper.MonthNames));
		}
		public static string FactQCResetDate(string content)
		{
			return content.Replace("$Today$", DateHelper.OfficialDateString(DateTime.Now, "MM DD YYYY", DateHelper.MonthShortNames));
		}
		public static string ResetID(string content)
		{
			return content.Replace("$ID$", "xxxxx");
		}
	}
}
