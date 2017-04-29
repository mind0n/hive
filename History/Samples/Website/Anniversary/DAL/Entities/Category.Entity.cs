using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Joy.Server;

namespace DAL
{
	public partial class Category
	{
		public int? CategoryId;
		public string Caption;
		public int? ParentId;
		public bool? Visible;
		[Json(DateTimeFormat="yyyy/MM/dd")]
		public DateTime? CategoryUpdate;
	}
}
