using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using Joy.Core;

namespace Joy.Storage
{
    public static class StorageExtensions
    {
        public static string ToJson(this object o)
        {
            if (o == null)
            {
                return string.Empty;
            }
            var jss = new JavaScriptSerializer();
            return jss.Serialize(o);
            
            //DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            //settings.UseSimpleDictionaryFormat = true;
            //DataContractJsonSerializer dcs = new DataContractJsonSerializer(o.GetType(), settings);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (XmlWriter w = JsonReaderWriterFactory.CreateJsonWriter(ms))
            //    {
            //        dcs.WriteObject(w, o);
            //    }
            //    return Encoding.Default.GetString(ms.GetBuffer());
            //}
        }

        public static object FromJson(this string s, Type type)
        {
	        try
	        {
		        if (string.IsNullOrEmpty(s))
		        {
			        return null;
		        }
		        var jss = new JavaScriptSerializer();
		        object o = jss.Deserialize(s, type);
		        if (o != null)
		        {
			        return o;
		        }
	        }
	        catch (Exception ex)
	        {
		        ErrorHandler.Handle(ex);
	        }
			return null;
		}

        public static T FromJson<T>(this string s) where T : class
        {
            //using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(s)))
            //{
            //DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            //settings.UseSimpleDictionaryFormat = true;
            //DataContractJsonSerializer dcs = new DataContractJsonSerializer(typeof(T), settings);
            //DataContractJsonSerializer dcs = new DataContractJsonSerializer(typeof(T));
            //object o = dcs.ReadObject(ms);
            //}
            return s.FromJson(typeof (T)) as T;
        }

        public static void RmDir(this string dir, string parentDir = null)
        {
            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                if (parentDir == null)
                {
                    parentDir = AppDomain.CurrentDomain.BaseDirectory;
                }
                string fullpath = parentDir + dir;
                string[] files = Directory.GetFiles(fullpath, "*.*", SearchOption.AllDirectories);
                foreach (string i in files)
                {
                    File.Delete(i);
                }
                Directory.Delete(fullpath, true);
            }
        }
        public static void Save2File(this Entity entity, string fullpath)
        {
            if (entity == null || string.IsNullOrEmpty(fullpath))
            {
                return;
            }
            string content = entity.ToJson();
            File.WriteAllText(fullpath, content);
            entity.Clean();
        }

        public static Entity LoadEntity(this string fullpath)
        {
            if (File.Exists(fullpath))
            {
                string content = File.ReadAllText(fullpath);
                string[] ttmp = fullpath.PathLastName(false).Split('_');
                if (ttmp.Length < 2)
                {
                    return null;
                }
                string tname = ttmp[0];
                string ekey = ttmp[1];
                Type type = Type.GetType(tname);
                if (type != null)
                {
                    Entity e = content.FromJson(type) as Entity;
                    return e;
                }
            }
            return null;
        }
        public static T LoadEntity<T>(this string fullpath) where T:Entity
        {
            if (File.Exists(fullpath))
            {
                string content = File.ReadAllText(fullpath);
                return content.FromJson<T>();
            }
            return default(T);
        }

        public static EntityGroup LoadGroup(this string dir)
        {
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
                string d = dir.PathLastName();
                if (!string.IsNullOrEmpty(d))
                {
                    string[] tmp = d.Split('_');
                    if (tmp.Length < 3)
                    {
                        return null;
                    }
                    string name = tmp[0];
                    string key = tmp[2];
                    EntityGroup g = StorageManager.Instance.CreateOrGet(name, key);
                    foreach (string i in files)
                    {
                        Entity e = i.LoadEntity();
                        if (e != null)
                        {
                            g.AddEntity(e);
                        }
                    }
                }
            }
            return null;
        }

        public static T Clone<T>(this object obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static List<T> To<T>(this Junction j) where T : Entity
        {
            if (j != null && j.Items.Count > 0)
            {
                List<T> r = new List<T>();
                foreach (Joint o in j.Items)
                {
                    r.Add(o.Target as T);
                }
                return r;
            }
            return null;
        }
        public static List<T> To<T>(this Entities list) where T:Entity
        {
            if (list != null && list.Count > 0)
            {
                List<T> r = new List<T>();
                foreach (object o in list)
                {
                    r.Add(o as T);
                }
                return r;
            }
            return null;
        } 
        public static List<T> To<T>(this List<object> list) where T : class
        {
            if (list != null && list.Count > 0)
            {
                List<T> r = new List<T>();
                foreach (object o in list)
                {
                    r.Add(o as T);
                }
                return r;
            }
            return null;
        }
    }
}
