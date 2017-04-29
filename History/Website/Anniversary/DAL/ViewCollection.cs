using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;

namespace DAL
{
	public class ViewCollection : EntityCollection<View>
	{
		public void Initialize()
		{
			foreach (View i in Items)
			{
				i.Initialize();	
			}
		}
		public View GetViewById(Guid id)
		{
			foreach (View i in Items)
			{
				if (Guid.Equals(i.Id, id))
				{
					return i;
				}
			}
			return null;
		}
		
	}
}
