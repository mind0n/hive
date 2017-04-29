using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Joy.Storage
{
    public class Entity
    {
        public delegate void EnumFieldHandler(Entity entity, PropertyInfo info, FieldAttribute attr);
        protected bool isChanged = true;
        protected PropertyInfo[] propertyCache;
        protected Dictionary<string, FieldInfo> fieldCache = new Dictionary<string, FieldInfo>();

        [ScriptIgnore]
        public bool IsChanged
        {
            get { return isChanged; }
        }

        [ScriptIgnore]
        public bool IsNewObject
        {
            get
            {
                return string.Equals(ObjectKey, Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase);
            }
        }

        public string ObjectKey { get; set; }

        [ScriptIgnore]
        internal EntityGroup Parent { get; set; }

        public Entity()
        {
            ObjectKey = Guid.Empty.ToString();
        }

        public virtual Entities Join(EntityGroup g, string pname, string lname = null)
        {
            object key = ObjectKey;
            if (!string.IsNullOrEmpty(lname))
            {
                PropertyInfo l = GetType().GetProperty(lname);
                if (l != null)
                {
                    key = l.GetValue(this);
                }
            }
            if (g == null || string.IsNullOrEmpty(pname) || key == null)
            {
                return null;
            }
            object k = key;
            //if (g.HasHash(pname))
            //{
            //    Hashtable h = g.GetHashtable(pname);
            //    if (h.ContainsKey(k))
            //    {
            //        object entity = h[k];
            //        rlt.Add(entity as Entity);
            //    }
            //}
            //else
            //{
                Entities rlt = JoinInternal(g, pname, k);
            //}
            return rlt;
        }

        private static Entities JoinInternal(EntityGroup g, string pname, object k)
        {
            var rlt = new Entities();
            Entities list = g.GetEntities();
            PropertyInfo info = null;
            foreach (Entity i in list)
            {
                if (info == null)
                {
                    Type type = i.GetType();
                    info = type.GetProperty(pname);
                }
                if (k.Equals(info.GetValue(i)))
                {
                    rlt.Add(i);
                }
            }
            return rlt;
        }

        public virtual void Link()
        {
            CacheInfos();
            foreach (PropertyInfo i in propertyCache)
            {
                if (string.Equals(i.PropertyType.Name, typeof (Junction).Name))
                {
                    Junction j = i.GetValue(this) as Junction;
                    j.Link();
                }
                else
                {
                    FieldAttribute attr = i.GetCustomAttribute<FieldAttribute>();
                    if (attr != null)
                    {
                        FieldInfo info = fieldCache[attr.Target];
                        Link(i, info);
                    }
                }
            }
        }

        private void CacheInfos()
        {
            if (propertyCache == null)
            {
                Type type = GetType();
                propertyCache = type.GetProperties();
            }
            if (fieldCache == null)
            {
                Type type = GetType();
                FieldInfo[] infos = type.GetFields();
                foreach (FieldInfo i in infos)
                {
                    fieldCache[i.Name] = i;
                }
            }
        }

        protected virtual void Link(PropertyInfo p, FieldInfo f)
        {
            if (p == null || f == null)
            {
                return;
            }
            object key = p.GetValue(this);
            Entity e = ObjectPoolManager.Get(key);
            f.SetValue(this, e);
        }

        public void Clean()
        {
            isChanged = false;
        }

        public void Dirty()
        {
            isChanged = true;
        }

        public void Enum(EnumFieldHandler callback)
        {
            if (callback != null)
            {
                Type type = GetType();
                PropertyInfo[] list = type.GetProperties();
                foreach (PropertyInfo i in list)
                {
                    FieldAttribute attr = i.GetCustomAttribute<FieldAttribute>();
                    if (attr != null)
                    {
                        callback(this, i, attr);
                    }
                }
            }
        }
    }
}
