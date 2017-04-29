using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Exceptions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using ULib.DataSchema;

namespace ULib
{
    public static class Extensions
    {
        public static MethodInfo GetMethod(this object o, string name)
        {
            if (o != null)
            {
                Type type = o.GetType();
                MethodInfo info = type.GetMethod(name);
                return info;
            }
            return null;
        }
        public static T GetAttribute<T>(this Type type) where T:class
        {
            object[] o = type.GetCustomAttributes(typeof(T), true);
            if (o != null && o.Length > 0)
            {
                return o[0] as T;
            }
            return default(T);
        }
        public static EntityMappings GetMappedFields(this Type type)
        {
            if (type == null)
            {
                return null;
            }
            FieldInfo[] infos = type.GetFields();
            EntityMappings mapping = new EntityMappings();
            foreach (FieldInfo i in infos)
            {
                if (i.IsPublic)
                {
                    GetCustomFieldAttr(mapping, i);
                }
            }
            return mapping;
        }

        private static void GetCustomFieldAttr(EntityMappings mapping, FieldInfo i)
        {
            object[] attr = i.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (attr != null && attr.Length > 0)
            {
                ColumnAttribute m = attr[0] as ColumnAttribute;
                if (m != null)
                {
                    mapping[string.IsNullOrEmpty(m.Alias)?i.Name:m.Alias] = new EntityMappingsItem { FieldInfo = i, Alias = m.Alias, Attr = m };
                }
            }
        }
        public static void Handle(this Exception error)
        {
            ExceptionHandler.Handle(error);
        }

		public static T GetAttribute<T>(this object target, int index = 0)
		{
			if (target != null)
			{
				Type type = target.GetType();
				object[] list = type.GetCustomAttributes(typeof(T), true);
				if (list != null && list.Length > 0)
				{
					if (index < 0 || index >= list.Length)
					{
						return (T)list[0];
					}
					return (T)list[index];
				}
			}
			return default(T);
		}
		public static string ValidPath(this string path, string splitter = "\\")
		{
			if (string.IsNullOrEmpty(path))
			{
				return splitter;
			}
			else if (!path.EndsWith(splitter))
			{
				return path + splitter;
			}
			return path;
		}
		public static string SubPath(this string path, params string[] subpaths)
		{
			if (string.IsNullOrEmpty(path) || subpaths == null || subpaths.Length < 1)
			{
				return path;
			}
			string subpath = string.Concat(subpaths);
			subpath = TrimPath(subpath);
			path = TrimPath(path);
			string[] src = path.Split('\\');
			string[] sub = subpath.Split('\\');
			List<string> rlt = new List<string>();
			bool flag = src[0] == sub[0];
			int i = 0;
			if (flag)
			{
				for (i = 0; i < src.Length; i++)
				{
					if (i >= sub.Length)
					{
						break;
					}
					else if (!string.Equals(src[i], sub[i], StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
				}
				for (int k = i; k < src.Length; k++)
				{
					rlt.Add(src[k]);
				}
				return string.Join("\\", rlt.ToArray());
			}
			else
			{
				for (i = 0; i < src.Length; i++)
				{
					if (src[i] == sub[0])
					{
						for (int j = 0; j < sub.Length; j++)
						{
							if (i + j >= 0 && i + j < src.Length && src[i + j] != sub[j])
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							flag = true;
							break;
						}
						else
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					for (int k = 0; k < i; k++)
					{
						rlt.Add(src[k]);
					}
					return string.Join("\\", rlt.ToArray());
				}
			}
			return path;
		}

		public static string TrimPath(this string path)
		{
			if (path.StartsWith("\\"))
			{
				path = path.Substring(1);
			}
			if (path.EndsWith("\\"))
			{
				path = path.Substring(0, path.Length - 1);
			}
			return path;
		}
        public static Process Parent(this Process process)
        {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
        }
		public static void Open(this string path)
		{
			if (File.Exists(path))
			{
				Process.Start(path);
			}
		}
        private static Process FindPidFromIndexedProcessName(string indexedProcessName)
        {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int)parentId.NextValue());
        }
        private static string FindIndexedProcessName(int pid)
        {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string processIndexdName = null;
            for (var index = 0; index < processesByName.Length; index++)
            {
                processIndexdName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                if ((int)processId.NextValue() == pid)
                {
                    return processIndexdName;
                }
            }
            return processIndexdName;
        }
        public static string PathMakeAbsolute(this string path, string baseDirectory = null)
        {
            if (string.IsNullOrEmpty(baseDirectory))
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            if (!baseDirectory.EndsWith("\\"))
            {
                baseDirectory += "\\";
            }
            return string.Concat(baseDirectory, path);
        }
        public static bool IsAbsolute(this string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Length > 2)
                {
                    return path[1] == ':';
                }
            }
            return false;
        }
        public static string PathLastName(this string path, bool withExt = true)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string[] list = path.Split('\\');
                if (list.Length > 0)
                {
					string filename = list[list.Length - 1];
					if (withExt)
					{
						return filename;
					}
					else
					{
						return filename.Split('.')[0];
					}
                }
            }
            return null;
        }
		public static T FromXml<T>(this string xml, params Type[] extTypes)
		{
			try
			{
				XmlSerializer xs = new XmlSerializer(typeof(T), extTypes);
				using (StringReader reader = new StringReader(xml))
				{
					object rlt = xs.Deserialize(reader);
					if (rlt != null)
					{
						return (T)rlt;
					}
				}
			}
			catch(Exception e)
			{
				ExceptionHandler.Handle(e);
			}
			return default(T);
		}
		public static string ToXml(this object target, params Type[] extraTypes)
		{
			try
			{
				StringBuilder b = new StringBuilder();
				XmlSerializer xs = new XmlSerializer(target.GetType(), extraTypes); //, new Type[] { typeof(ChangeServerCommand), typeof(RemoteExecuteCommand) });
				using (StringWriter w = new StringWriter(b))
				{
					xs.Serialize(w, target);
				}
				return b.ToString();
			}
			catch(Exception e)
			{
				ExceptionHandler.Handle(e);
				return string.Empty;
			}
		}
        public static T CreateInstance<T>(this string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return default(T);
            }
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                try
                {
                    T obj = (T)type.GetConstructor(Type.EmptyTypes).Invoke(null);
                    return obj;
                }
                catch (Exception e)
                {
                    ExceptionHandler.Handle(e);
                }
            }
            return default(T);
        }

        public static bool ContainsPath(this System.Windows.Forms.ComboBox.ObjectCollection list, string content)
        {
            if (content is string)
            {
                foreach (string item in list)
                {
                    if (string.Equals(item, content, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return list.Contains((object)content);
            }
        }

        public static bool ContainsIgnoreCase(this List<string> list, string content)
        {
            if (list != null)
            {
                foreach (string  i in list)
                {
                    if (string.Equals(i, content, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
		public static string GetDragString(this DragEventArgs e)
		{
			if (e != null)
			{
				object o = e.Data.GetData(DataFormats.FileDrop, true);
				if (o == null)
				{
					o = e.Data.GetData(DataFormats.Text, true);
				}
				if (o != null)
				{
					if (o is string[])
					{
						o = ((string[])o)[0];
					}
					return o.ToString();
				}
			}
			return null;
		}

    }
}
