using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DAL.Entities
{
	public partial class CategoryViewMapping : Entity
	{
		[XmlIgnore]
		public Category Category;
		[XmlIgnore]
		public View View;

	}
}
