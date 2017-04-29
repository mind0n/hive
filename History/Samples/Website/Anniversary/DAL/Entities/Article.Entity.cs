using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Entities
{
	public class Article
	{
		public int? ArticleId;
		public int? CategoryId;
		public int? UserId;
		public string Caption;
		public string Link;
		public DateTime? ArticleUpdate;
		public string Tag;
		public string Content;
		public bool? IsVisible;
		public string Brief;
		public int? ArticleType;
	}
}
