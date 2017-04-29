using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Joy.Storage
{
    public class EntityGroup : Entity
    {
        private const string OBJKEY = "ObjectKey";
        protected HashGroup hgroup = new HashGroup();

        protected string name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = GetType().Name;
                }
                return name;
            }
        }
        protected Entities list = new Entities();

        public Entity this[int i]
        {
            get
            {
                return list[i];
            }
        }
        public EntityGroup(string n, string key = null, Entities collection = null)
        {
            name = n;
            if (collection != null)
            {
                list = collection;
            }
            if (key != null)
            {
                ObjectKey = key;
            }
        }

        public Entities GetEntities()
        {
            return list;
        }

        public T GetEntity<T>(int index) where T:Entity
        {
            return list[index] as T;
        }

        public List<T> GetEntities<T>() where T:Entity
        {
            List<T> rlt = new List<T>();
            foreach (Entity i in list)
            {
                rlt.Add(i as T);
            }
            return rlt;
        }

        public T AddEntity<T>(Entity entity) where T:Entity
        {
            AddEntity(entity);
            return entity as T;
        }
        public void AddEntity(Entity entity, bool dup = false)
        {
            Entity rlt = dup ? entity.Clone<Entity>() : entity;
            if (rlt.IsNewObject)
            {
                ObjectPoolManager.Add(rlt);
            }
            rlt.Parent = this;
            hgroup.AddHashItem(OBJKEY, rlt.ObjectKey, rlt);
            //rlt.Enum(delegate(Entity e, PropertyInfo p, FieldAttribute a)
            //{
            //    if (a.UseHash)
            //    {
            //        hgroup.AddHashItem(p.Name, p.GetValue(e), rlt);
            //    }
            //});
            list.Add(rlt);
        }
        public override void Link()
        {
            foreach (Entity i in list)
            {
                i.Link();
            }
        }


        internal bool HasHash(string pname)
        {
            return hgroup.ContainsKey(pname);
        }

        internal Hashtable GetHashtable(string pname)
        {
            return hgroup[pname];
        }
    }

    public class Entities : List<Entity>
    {
    }

}
