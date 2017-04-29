using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DAL.Entities;

namespace DAL
{
	[Serializable]
	public partial class Category
	{
		public string ParentName;
	}
}
