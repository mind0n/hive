using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DAL.Entities
{
	public partial class View : Entity
	{
		[XmlIgnore]
		public List<Category> Categories = new List<Category>();
		public void Initialize(Category c = null)
		{
			Categories.Clear();
			if (c == null)
			{
				foreach (CategoryViewMapping i in Db.Instance.CVMappings.Mappings)
				{
					if (Guid.Equals(i.ViewId.Value, this.Id))
					{
						AddCategory(i.Category);
					}
				}
			}
			else
			{
				AddCategory(c);
			}
		}

		public void AddCategory(Category c)
		{
			if (!Categories.Contains(c))
			{
				Categories.Add(c);
			}
		}
	}
}
