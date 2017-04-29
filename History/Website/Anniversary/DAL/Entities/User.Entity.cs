using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Entities
{
	public partial class UserItem
	{
		public int? UserId;
		public string UName;
		public string UPwd;
		public int ULevel;
        public DateTime? UserUpdate;
        public string UText;
	}
}
