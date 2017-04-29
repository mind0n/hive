using Joy.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Entities
{
	[Table(Name = "tFiles")]
	public class FileItem
	{
		[Column(Name = "uid", Type = "num")]
		public int? UserId;

		[Column(Name = "cid", Type = "num")]
		public int? CategoryId;

		[Column(Name = "fname", Type = "str")]
		public string Filename;

		[Column(Name = "dname", Type = "str")]
		public string DisplayName;
	}
}
