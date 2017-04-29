using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DAL.Entities;

namespace DAL
{
	[Serializable]
	public partial class Article : Entity
	{
		//public void Initialize()
		//{
		//    if (this.ParentId.HasValue)
		//    {
		//        Parent = DAL.RootCategory.GetCategoryById(this.ParentId.Value);
		//    }
		//}
		public Category GetParentCategory()
		{
			return Db.Instance.RootCategory.GetCategoryById(CategoryId.Value);
		}
	}
}
