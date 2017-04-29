using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
	public class EntityCollection<T>
	{
		public List<T> Items = new List<T>();
		public void AddItem(T item)
		{
			if (!Items.Contains(item))
			{
				Items.Add(item);
			}
		}
		public void RemoveItem(T item)
		{
			if (Items.Contains(item))
			{
				Items.Remove(item);
			}
		}
		public void RemoveItemAt(int index)
		{
			if (index >= 0 && index < Items.Count)
			{
				Items.RemoveAt(index);
			}
		}
		protected List<T> GetItems(Func<SearchCriteria<T>, bool> callback, int pageSize = int.MaxValue, int pageNum = 0)
		{
			List<T> rlt = new List<T>();
			int startIndex = pageSize * pageNum;
			int endIndex = pageSize * (pageNum + 1);
			for (int i = 0; i < Items.Count; i++)
			{
				if (pageSize == int.MaxValue)
				{
					if (callback(new SearchCriteria<T> { Item = Items[i], PageNum = pageNum, PageSize = pageSize, Index = i }))
					{
						rlt.Add(Items[i]);
					}
				}
				else
				{
					if (i >= startIndex && i < endIndex)
					{
						if (callback(new SearchCriteria<T> { Item = Items[i], PageNum = pageNum, PageSize = pageSize, Index = i }))
						{
							rlt.Add(Items[i]);
						}
					}
					if (i >= endIndex)
					{
						break;
					}
				}
			}
			return rlt;
		}
	}

	public class SearchCriteria<T>
	{
		public T Item;
		public int Index;
		public int PageSize;
		public int PageNum;
	}
}
