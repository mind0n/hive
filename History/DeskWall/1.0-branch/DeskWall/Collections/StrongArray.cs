using System;
using System.Collections;
using Fs;

namespace Dw.Collections
{
	public class StrongArray : ArrayList
	{
		public delegate void EnumItemCallback<T>(T item);
		public T GetItemByIndex<T>(int index)
		{
			if (this.Count <= index || index < 0)
			{
				return default(T);
			}
			return (T)this[index];
		}
		public void EnumItem<T>(EnumItemCallback<T> Callback)
		{
			for (int i = 0; i < Count; i++)
			{
				try
				{
					T item = GetItemByIndex<T>(i);
					Callback(item);
				}
				catch (Exception err)
				{
					Exceptions.LogOnly(err, Exceptions.ExceptionType.Debug);
					continue;
				}
			}
		}
		//public bool Exists(string key, string val)
		//{
			
		//}
	}
}
