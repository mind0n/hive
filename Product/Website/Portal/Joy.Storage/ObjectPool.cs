using System;
using System.Collections;

namespace Joy.Storage
{
    public class ObjectPool
    {
        internal Hashtable slots = new Hashtable();
        public ObjectPool()
        {
            
        }

        public void Add(Entity o)
        {
            if (o == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(o.ObjectKey) || string.Equals(o.ObjectKey, Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                o.ObjectKey = Guid.NewGuid().ToString();
            }
            slots[o.ObjectKey] = o;
        }

        public void Remove(Entity o)
        {
            if (o == null)
            {
                return;
            }
            if (slots.ContainsKey(o.ObjectKey))
            {
                slots.Remove(o.ObjectKey);
            }
        }
    }

    public class ObjectPoolManager
    {
        private static ObjectPool pool = new ObjectPool();

        public static void Add(Entity o)
        {
            pool.Add(o);
        }

        public static void Remove(Entity e)
        {
            pool.Remove(e);
        }

        public static bool Contains(object key)
        {
            return pool.slots.ContainsKey(key);
        }

        public static Entity Get(object key)
        {
            if (pool.slots.ContainsKey(key))
            {
                return pool.slots[key] as Entity;
            }
            return null;
        }

        internal static void Clear()
        {
            pool.slots.Clear();
        }
    }

    //public class Hash
    //{
    //    public string Code = Guid.Empty.ToString();
    //    public static Hash Empty = new Hash();
    //    public virtual bool IsEmpty
    //    {
    //        get
    //        {
    //            return string.Equals(Code, Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase);
    //        }
    //    }
    //}
}
