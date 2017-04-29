using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Joy.Server.Data;

namespace DAL
{
	public class D
	{
		public static Db DB = Dbs.Use<DbAccess>("dbportal");
		public static SiteCache Cache = new SiteCache { Name = "Root" };
	}
	public class SiteCache
	{
		public string Name;
		public string Value;
		public readonly List<SiteCache> Children = new List<SiteCache>();
		public SiteCache Retrieve(string name)
		{
			foreach (SiteCache i in Children)
			{
				if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			SiteCache r = new SiteCache();
			r.Name = name;
			Children.Add(r);
			return r;
		}
	}
}
