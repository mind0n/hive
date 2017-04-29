using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;

namespace DAL
{
	public class CategoryViewMappingCollection
	{
		public List<CategoryViewMapping> Mappings = new List<CategoryViewMapping>();
		public void Initialize()
		{
			foreach (CategoryViewMapping i in Mappings)
			{
				i.Category = Db.Instance.RootCategory.GetCategoryById(i.CategoryId.Value);
				i.View = Db.Instance.Views.GetViewById(i.ViewId.Value);
				if (i.View != null)
				{
					i.View.AddCategory(i.Category);
				}
			}
		}
	}
}
