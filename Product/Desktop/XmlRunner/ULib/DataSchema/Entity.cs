using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ULib.DataSchema;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ULib.Results;
using System.ComponentModel;

namespace ULib.DataSchema
{
    public class Entity
    {
        const string insTmp = "insert into {0}({1}) values({2})\r\n";
        protected EntityMappings mappings;
        private string _tablename;
        public EntitySet EntitySet;
        public string _MiscInfo { get; set; }

        protected string tablename
        {
            get {
                if (string.IsNullOrEmpty(_tablename))
                {
                    TableAttribute at = this.GetType().GetAttribute<TableAttribute>();
                    if (at != null)
                    {
                        _tablename = at.Name;
                    }
                }
                return _tablename; 
            }
        }
        public Values OriginalValues = new Values();
        public bool HasPField
        {
            get
            {
                foreach (KeyValuePair<string, EntityMappingsItem> i in mappings)
                {
                    if (i.Value.Attr.IsPrimary)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsPFieldEmpty
        {
            get
            {
                foreach (KeyValuePair<string, EntityMappingsItem> i in mappings)
                {
                    if (i.Value.Attr.IsPrimary)
                    {
                        object o = this.OriginalValues[i.Key];
                        if (o == null || string.IsNullOrEmpty(o.ToString().Trim()))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public string OriginalFile;
        public Entity()
        {
            mappings = this.GetType().GetMappedFields();
            TableAttribute attr = this.GetType().GetAttribute<TableAttribute>();
            _tablename = attr == null ? null : attr.Name;
        }
        public MakeSqlResult MakeInsertSQL(SqlConnection connection = null)
        {
            MakeSqlResult rlt = new MakeSqlResult();
            List<string> fs = new List<string>();
            List<string> vs = new List<string>();

            foreach (KeyValuePair<string, EntityMappingsItem> i in mappings)
            {
                fs.Add(i.Value.FieldInfo.Name);
                FieldInfo info = i.Value.FieldInfo;
                object vo = this.OriginalValues.ContainsKey(i.Key) ? this.OriginalValues[i.Key] : null;
                string v = vo == null ? (string.IsNullOrEmpty(i.Value.Attr.Default) ? "NULL" : i.Value.Attr.Default) : vo.ToString();
                //if (i.Value.Attr.IsCached)
                //{
                //    if (EntitySet.Cache.Exists(i.Key, v))
                //    {
                //        object val = GetValueByAttribute(connection, i, v, info, rlt);
                //        EntitySet.Cache.Set(i.Key, v, val);
                //        v = val.ToString();
                //    }
                //    else
                //    {
                //        v = GetValueByAttribute(connection, i, v, info, rlt);
                //    }
                //}
                //else
                //{
                    v = GetValueByAttribute(connection, i, v, info, rlt);
                //}
                TypeConverter conv = TypeDescriptor.GetConverter(i.Value.FieldInfo.FieldType);
                if (!string.Equals(v, "NULL", StringComparison.OrdinalIgnoreCase))
                {
                    i.Value.FieldInfo.SetValue(this, conv.ConvertFrom(v));
                }
                if (!string.IsNullOrEmpty(i.Value.Attr.Trigger))
                {
                    MethodInfo mtrigger = this.GetMethod(i.Value.Attr.Trigger);
                    mtrigger.Invoke(this, new object[]{connection});
                }
                v = SecureFieldValue(i.Value.Attr, v);
                //Convert.ChangeType(v, i.Value.FieldInfo.FieldType));
                vs.Add(v);
            }
            string fields = string.Join(",\t", fs.ToArray());
            string values = string.Join(",\t", vs.ToArray());
            rlt.Sql = string.Format(insTmp, tablename, fields, values);
            return rlt;
        }

        private string GetValueByAttribute(SqlConnection connection, KeyValuePair<string, EntityMappingsItem> i, string v, FieldInfo info, MakeSqlResult rlt)
        {
            if (i.Value.Attr.HasFilter)
            {
                MethodInfo m = this.GetMethod(i.Value.Attr.Filter);
                v = m.Invoke(this, new object[] {info.Name}).ToString();
            }
            if (i.Value.Attr.HasCondition && connection != null)
            {
                string sql = string.Empty;
                if (string.IsNullOrEmpty(i.Value.Attr.ConditionField))
                {
                    sql = string.Format("select {0} from {1} where {2}", i.Value.Attr.Column,
                                  i.Value.Attr.ForeignTable, string.Format(i.Value.Attr.Condition, v));
                }
                else
                {
                    FieldInfo inf = this.GetType().GetField(i.Value.Attr.ConditionField);
                    sql = string.Format("select {0} from {1} where {2}", i.Value.Attr.Column,
                                  i.Value.Attr.ForeignTable, string.Format(i.Value.Attr.Condition, v, inf.GetValue(this) ));
                }
                object r = null; //SqlHelper.ExecuteScalar(connection, System.Data.CommandType.Text, sql);
                if (r != null)
                {
                    v = r.ToString();
                }
                else
                {
                    v = "NULL";
                    if (!string.IsNullOrEmpty(i.Value.Attr.Verify))
                    {
                        MethodInfo mverifier = this.GetMethod(i.Value.Attr.Verify);
                        if (mverifier == null || !((bool) mverifier.Invoke(this, new object[] {info.Name, v})))
                        {
                            rlt.LastError =
                                new Exception(string.Format("Cannot get {0} according to {1}.  Sql:{2}",
                                                            i.Value.Attr.Column, i.Key, sql));
                            rlt.MiscInfo = _MiscInfo;
                            rlt.OriginalData = MakeOriginalOutput();
                        }
                    }
                }
            }
            return v;
        }

        public string MakeOriginalOutput()
        {
            List<string> r = new List<string>();
            foreach (KeyValuePair<string, object> i in OriginalValues)
            {
                object o = i.Value;
                if (o != null)
                {
                    r.Add(o.ToString());
                }
                else
                {
                    r.Add(string.Empty);
                }
            }
            return string.Join("\t", r.ToArray());
        }

        private static string SecureFieldValue(ColumnAttribute attr,string v)
        {
            if (!string.Equals(v, "NULL"))
            {
                if (attr.Type == ValueType.String)
                {
                    if (string.IsNullOrEmpty(v))
                    {
                        return "NULL";
                    }
                    return "'" + v.Replace("'", "''") + "'";
                }
            }
            return v;
        }
    }
    public class EntitySet : List<Entity>
    {
        public EntityCache Cache
        {                       
            get { return cache; }
        }
        protected EntityCache cache = new EntityCache();
        protected Dictionary<string, int> ItemCounter = new Dictionary<string, int>();
        public void AddEntity(Entity e)
        {
            e.EntitySet = this;
            Add(e);
        }

        public int CountItem(string key)
        {
            if (ItemCounter.ContainsKey(key))
            {
                ItemCounter[key]++;
            }
            else
            {
                ItemCounter[key] = 1;
            }
            return ItemCounter[key];
        }
    }
    public class Values : Dictionary<string, object>
    {
        public bool IsEmpty
        {
            get
            {
                foreach (KeyValuePair<string, object> i in this)
                {
                    if (i.Value != null)
                    {
                        if (i.Value is string)
                        {
                            if (!string.IsNullOrEmpty(i.Value.ToString().Trim()))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public string Get(string name)
        {
            if (this.ContainsKey(name))
            {
                object o = this[name];
                if (o != null)
                {
                    return o.ToString();
                }
            }
            return string.Empty;
        }
    }
    public class EntityMappings : Dictionary<string, EntityMappingsItem>
    {

    }
    public class EntityMappingsItem
    {
        public FieldInfo FieldInfo;
        public string Alias;
        public ColumnAttribute Attr;
    }
    public class TableAttribute : Attribute
    {
        public string Name;
    }
    public class ColumnAttribute : Attribute
    {
        public string Filter;
        public string Verify;
        public bool Nullable;
        public bool HasCondition
        {
            get
            {
                return !string.IsNullOrEmpty(Condition);
            }
        }
        public bool HasAlias
        {
            get
            {
                return !string.IsNullOrEmpty(Alias);
            }
        }
        public bool HasFilter
        {
            get
            {
                return Filter != null;
            }
        }
        public ValueType Type = ValueType.String;
        public string Alias;
        public string ForeignTable;
        public string Column;
        public string Condition;
        public string ConditionField;
        public string Default;
        public string Trigger;
        public bool IsPrimary;
        public bool IsCached;
    }
    public class EntityCache : Dict<string, FieldCache>
    {
        public delegate Result CacheEnumCallback(string k, object o);
        public Result Enum(string field, CacheEnumCallback callback)
        {
            if (ContainsKey(field))
            {
                foreach (KeyValuePair<string, object> i in this[field])
                {
                    Result r = callback(i.Key, i.Value);
                    if (r.IsCanceled)
                    {
                        return r;
                    }
                }
            }
            return new Result();
        }
        public void CountItem(string field, string key)
        {
            if (Exists(field, key))
            {
                int n = (int) this[field][key];
                this[field][key] = n + 1;
            }
            else
            {
                Set(field, key, 1);
            }
        }
        public void Set(string field, string key, object v)
        {
            if (!this.ContainsKey(field))
            {
                this[field] = new FieldCache();
            }
            this[field][key] = v;
        }
        public bool Exists(string field, string key)
        {
            if (ContainsKey(field) && this[field].ContainsKey(key))
            {
                return true;
            }
            return false;
        }
        public T Get<T>(string field, string key)
        {
            if (ContainsKey(field) && this[field].ContainsKey(key))
            {
                return (T)this[field][key];
            }
            return default(T);
        }
    }
    public class FieldCache : Dict<string, object>
    {
        
    }
    public enum ValueType
    {
        String,
        Number
    }
}
