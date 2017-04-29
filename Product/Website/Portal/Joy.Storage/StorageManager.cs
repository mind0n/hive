using System.Collections.Generic;

namespace Joy.Storage
{
    public class StorageManager
    {
        public static readonly StorageManager Instance = new StorageManager();

        protected ISaveAdapter Adapter;
        protected EntityGroups Entry = new EntityGroups();

        public void Initialize(ISaveAdapter saveadapter)
        {
            Adapter = saveadapter;
        }  

        public void Save()
        {
            Adapter.Process(Entry);
        }

		public LoadResult Load(string input = null)
		{
			return Adapter.Load(input);
		}

        public void ResetAdapter()
        {
            Adapter.Clear();
            ObjectPoolManager.Clear();
        }

		public EntityGroup Create(string nameAndKey, char splitter = '_')
		{
			if (string.IsNullOrEmpty(nameAndKey))
			{
				return null;
			}
			string[] list = nameAndKey.Split(splitter);
			if (list.Length > 2)
			{
				return CreateOrGet(list[0], list[2]);
			}
			else if (list.Length > 0)
			{
				return CreateOrGet(list[0]);
			}
			return null;
		}

        public EntityGroup CreateOrGet(string name, string key = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            if (!Entry.ContainsKey(name))
            {
                var eg = new EntityGroup(name, key);
                Entry[name] = eg;
                ObjectPoolManager.Add(eg);
                return eg;
            }
            return Entry[name];
        }

        public void Drop(string name)
        {
            if (Entry.ContainsKey(name))
            {
                EntityGroup g = Entry[name];
                Adapter.DropEntityGroup(g);
                Entry.Remove(name);
            }
        }

        public EntityGroups GetEntry()
        {
            return Entry;
        }
    }

    public class EntityGroups : Dictionary<string, EntityGroup>
    {
        
    }
}
