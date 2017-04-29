using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
	public class Entity
	{
		public Guid Id;
		public Entity()
		{
			Id = Guid.NewGuid();
		}
	}
}
