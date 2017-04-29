using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptManager
{
	public abstract class Reader<TCache>
	{
		public Func<TCache, bool> CacheAddComplete;
		public bool IsCacheEmpty
		{
			get
			{
				foreach (TCache item in cache)
				{
					if (!object.Equals(item, default(TCache)))
					{
						return false;
					}
				}
				return true;
			}
		}
		protected TCache[] cache;

		/// <summary>
		/// Read next item.
		/// </summary>
		/// <param name="content">Content to be read</param>
		public virtual void ReadNext(object content)
		{
			TCache item = CreateCacheItem(content);
			AddCacheItem(item);
		}

		/// <summary>
		/// Flush all the cached items.
		/// </summary>
		public virtual void Flush()
		{
			while (!IsCacheEmpty)
			{
				AddCacheItem(default(TCache));
			}
		}

		/// <summary>
		/// Initiate cache space.
		/// </summary>
		protected abstract void InitiateReader();

		protected abstract TCache CreateCacheItem(object content);

		/// <summary>
		/// Shift cache left and cache new item.
		/// </summary>
		/// <param name="item">Item to be cached at the right side of the cache</param>
		protected virtual void AddCacheItem(TCache item)
		{
			if (cache != null)
			{
				if (!object.Equals(cache[0], default(TCache)) && CacheAddComplete != null)
				{
					if (CacheAddComplete(cache[0]))
					{
						ShiftCacheLeft(item);
					}
				}
				else if (object.Equals(item, default(TCache)) || IsCacheEmpty)
				{
					ShiftCacheLeft(item);
				}
				else if (object.Equals(cache[0], default(TCache)) && (!IsCacheEmpty || !object.Equals(item, default(TCache))))
				{
					ShiftCacheLeft(item);
				}
			}
		}

		/// <summary>
		/// Shift cache left and cache the item.
		/// </summary>
		/// <param name="item">Item to be cached</param>
		private void ShiftCacheLeft(TCache item)
		{
			for (int i = 0; i < cache.Length - 1; i++)
			{
				cache[i] = cache[i + 1];
			}
			cache[cache.Length - 1] = item;
		}
	}
}
