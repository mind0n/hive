using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL;

namespace Site
{
	public class DbInitializer
	{
		public static bool IsInitialized = false;
		static DbInitializer()
		{
			Initialize();
		}
		public static void Initialize()
		{
			if (Db.IsDbNew)
			{
				Db db = Db.Instance;
				Category p = new Category { Name = "Index" };
				Category c;
				c = new Category { Name = "校庆新闻" };
				p.AddChild(c);
				c = new Category { Name = "通知公告" };
				p.AddChild(c);
				c = new Category { Name = "院情院史" };
				p.AddChild(c);
				c = new Category { Name = "办学成果" };
				p.AddChild(c);

				Category d;
				d = new Category { Name = "教授风采", ParentId = c.Id };
				db.RootCategory.AddChild(d);
				d = new Category { Name = "教学成果", ParentId = c.Id };
				db.RootCategory.AddChild(d);
				d = new Category { Name = "科研成果", ParentId = c.Id };
				db.RootCategory.AddChild(d);
				d = new Category { Name = "育人成果", ParentId = c.Id };
				db.RootCategory.AddChild(d);
				d = new Category { Name = "大学生科创", ParentId = c.Id };
				db.RootCategory.AddChild(d);

				c = new Category { Name = "校友风采" };
				p.AddChild(c);
				c = new Category { Name = "祝福寄语" };
				p.AddChild(c);
				c = new Category { Name = "光影电机" };
				p.AddChild(c);
				c = new Category { Name = "联系我们" };
				p.AddChild(c);
				db.RootCategory.AddChild(p);
				View v;
				v = new View { Name = typeof(Index).FullName };
				db.Views.AddItem(v);

				CategoryViewMapping m;
				m = new CategoryViewMapping { ViewId = v.Id, CategoryId = p.Id };

				db.Initialize();
				Db.SaveDb();
			}
			IsInitialized = true;
		}
	}
}
