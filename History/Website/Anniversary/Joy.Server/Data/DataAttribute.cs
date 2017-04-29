using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Server.Data
{
	public class ColumnAttribute : Attribute
	{
		public string Name;
		public string Type = "str";

	}
	public  class TableAttribute : Attribute
	{
		public string Name;
	}
}
