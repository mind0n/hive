using System;
using System.Collections.Generic;
using System.IO;

namespace Joy.Data
{
    internal class CacheIndexPage
    {
        private Dictionary<uint, IndexUnit> _cache;
        private BinaryReader _reader;
        private uint _rootPageID;
        private BinaryWriter _writer;
        public const int CACHE_SIZE = 200;

        public CacheIndexPage(BinaryReader reader, BinaryWriter writer, uint rootPageID)
        {
            this._reader = reader;
            this._writer = writer;
            this._cache = new Dictionary<uint, IndexUnit>();
            this._rootPageID = rootPageID;
        }

        public void AddPage(IndexUnit indexPage)
        {
            this.AddPage(indexPage, false);
        }

        public void AddPage(IndexUnit indexPage, bool markAsDirty)
        {
            if (!this._cache.ContainsKey(indexPage.UnitID))
            {
                if (this._cache.Count >= 200)
                {
					KeyValuePair<uint, IndexUnit> pageToRemove = default(KeyValuePair<uint, IndexUnit>);
					foreach (KeyValuePair<uint, IndexUnit> x in _cache)
					{
						if (x.Key != this._rootPageID)
						{
							pageToRemove = x;
							break;
						}
					}
                    if (pageToRemove.Value.IsDirty)
                    {
                        UnitFactory.WriteToFile(pageToRemove.Value, this._writer);
                        pageToRemove.Value.IsDirty = false;
                    }
                    this._cache.Remove(pageToRemove.Key);
                }
                this._cache.Add(indexPage.UnitID, indexPage);
            }
            if (markAsDirty)
            {
                indexPage.IsDirty = true;
            }
        }

        public IndexUnit GetPage(uint pageID)
        {
            if (this._cache.ContainsKey(pageID))
            {
                return this._cache[pageID];
            }
            IndexUnit indexPage = UnitFactory.GetIndexPage(pageID, this._reader);
            this.AddPage(indexPage, false);
            return indexPage;
        }

        public void PersistPages()
        {
			List<IndexUnit> list = new List<IndexUnit>();
			foreach (IndexUnit x in _cache.Values)
			{
				if (x.IsDirty)
				{
					list.Add(x);
				}
			}
			IndexUnit[] pagesToPersist = list.ToArray();
            if (pagesToPersist.Length > 0)
            {
                foreach (IndexUnit indexPage in pagesToPersist)
                {
                    UnitFactory.WriteToFile(indexPage, this._writer);
                    indexPage.IsDirty = false;
                }
            }
        }
    }
}

