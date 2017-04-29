using System;
using System.Collections.Generic;
using System.Text;

namespace Joy.Server.Date
{
	public class DateHelper
	{
		public static string[] MonthShortNames = {"en-us", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
		public static string[] MonthNames = { "en-us", "January", "Febrary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
		public static string OfficialDateString(DateTime dt, string format, string[] MonthNames)
		{
			StringBuilder f = new StringBuilder(format);
			f.Replace("YYYY", dt.Year.ToString());
			f.Replace("MM", MonthNames[dt.Month]);
			f.Replace("DD", GetDayOfMonth(dt.Day));
			return f.ToString();
		}
		public static string GetDayOfMonth(int day)
		{
			if (day % 10 == 1 && day != 11)
			{
				return day + "st";
			}
			else if (day % 10 == 2 && day != 12)
			{
				return day + "nd";
			}
			else if (day % 10 == 3 && day != 13)
			{
				return day + "rd";
			}
			else
			{
				return day + "th";
			}
		}
	}
}
