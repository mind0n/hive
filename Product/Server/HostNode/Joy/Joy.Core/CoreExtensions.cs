using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;


namespace Joy.Core
{
	public static class CoreExtensions
	{
        public static SecureString Secure(this string pwd)
        {
            var ss = new SecureString();

            foreach (var i in pwd)
            {
                ss.AppendChar(i);
            }
            return ss;
        }
        public static Thread Launch(this Thread th, Action worker, bool isback = true, bool start = true)
        {
            if (worker == null)
            {
                return null;
            }
            th = new Thread(new ThreadStart(worker));
            th.IsBackground = isback;
            if (start)
            {
                th.Start();
            }
            return th;
        }
        public static void Shutdown(this ServiceHost host)
	    {
	        if (host != null)
	        {
	            if (host.State == CommunicationState.Faulted || host.State == CommunicationState.Opening)
	            {
	                host.Abort();
	            }
	            else
	            {
	                host.Close();
	            }
	        }
	    }

	    public static object DefaultVal(this Type t)
	    {
            if (t != null && t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }
	        return null;
	    }

        public static Stream Stream(this string cnt, bool dispose = false)
        {
            if (string.IsNullOrEmpty(cnt))
            {
                if (dispose)
                {
                    using (var ms = new MemoryStream())
                    {
                        return ms;
                    }
                }
                return new MemoryStream();
            }
            var en = new UTF8Encoding();
            var bs = en.GetBytes(cnt);
            if (dispose)
            {
                using (var mms = new MemoryStream(bs))
                {
                    return mms;
                }
            }
            return new MemoryStream(bs);
        }

        public static object Call(this string method, object instance, params object[] args)
        {
            var ms = instance.GetType().GetMembers();
            foreach (var i in ms)
            {
                if (string.Equals(i.Name, method, StringComparison.OrdinalIgnoreCase))
                {
                    return i.Call(instance, args);
                }
            }
            return null;
        }

	    public static object Call(this MemberInfo info, object instance, params object[] args)
	    {
	        object r = null;
            if (info is MethodInfo)
            {
                var mi = (MethodInfo)info;
                try
                {
                    var pars = mi.GetParameters();
                    MethodAttribute attr = null;
                    object[] opars = null;
                    if (!mi.HasAttr<MethodAttribute>(out attr))
                    {
                        attr = new MethodAttribute();
                    }
                    opars = attr.OnInvoke(instance, pars, args);
                    if (mi.ReturnType != typeof(void))
                    {
                        r = mi.Invoke(instance, opars);
                        if (r == null)
                        {
                            return mi.ReturnType.DefaultVal();
                        }
                        return r;
                    }
                    else
                    {
                        mi.Invoke(instance, opars);
                        return mi.ReturnType.DefaultVal();
                    }
                }
                catch (Exception ex)
                {
                    Error.Handle(ex);
                    return mi.ReturnType.DefaultVal();
                }
            }
            else if (info is PropertyInfo)
            {
                var pi = (PropertyInfo)info;
                try
                {
                    if (pi.CanRead && (args == null || args.Length < 1))
                    {
                        r = pi.GetValue(instance, null);
                        if (r == null)
                        {
                            return pi.PropertyType.DefaultVal();
                        }
                        return r;
                    }
                    if (pi.CanWrite)
                    {
                        pi.SetValue(instance, args[0], null);
                    }
                }
                catch
                {
                    return pi.PropertyType.DefaultVal();
                }
            }
            else if (info is FieldInfo)
            {
                var fi = (FieldInfo)info;
                try
                {
                    if (args == null || args.Length < 1)
                    {
                        r = fi.GetValue(instance);
                        if (r == null)
                        {
                            return fi.FieldType.DefaultVal();
                        }
                        return r;
                    }
                    fi.SetValue(instance, args[0]);
                    return fi.FieldType.DefaultVal();
                }
                catch
                {
                    return fi.FieldType.DefaultVal();
                }
            }
	        return null;
	    }

	    public static object InvokeAttributes<T>(this MethodCallMessageWrapper msg, T Instance, JoyAttribute<T> att, bool isproperty, bool isget, string name)
        {
            object rlt = null;
            if (att != null)
            {
                att.Instance = Instance;
                if (isproperty)
                {
                    if (isget)
                    {
                        rlt = att.Retrieve(name);
                    }
                    else if (msg != null && msg.ArgCount > 0)
                    {
                        att.Save(name, msg.Args[0]);
                    }
                }
            }
            return rlt;
        }   

	    private static Mutex m;

        private const Int32 SRCCOPY = 0xCC0020;
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, // handle to destination DC
            int nXDest, // x-coord of destination upper-left corner
            int nYDest, // y-coord of destination upper-left corner
            int nWidth, // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc, // handle to source DC
            int nXSrc, // x-coordinate of source upper-left corner
            int nYSrc, // y-coordinate of source upper-left corner
            System.Int32 dwRop // raster operation code
        );

	    public static Bitmap SaveImage(this Graphics g, int w, int h)
	    {
	        return g.SaveImage(new Size(w, h));
	    }

	    public static Bitmap SaveImage(this Graphics g, Size s)
	    {
            var memImage = new Bitmap(s.Width, s.Height, g);
	        using (Graphics memGraphic = Graphics.FromImage(memImage))
	        {
	            IntPtr dc1 = g.GetHdc();
	            IntPtr dc2 = memGraphic.GetHdc();
	            BitBlt(dc2, 0, 0, s.Width, s.Height, dc1, 0, 0, SRCCOPY);
	            g.ReleaseHdc(dc1);
	            memGraphic.ReleaseHdc(dc2);
	            return memImage;
	        }
	    }

        public static Bitmap AdjustQuality(this Image img, int quality, bool autoDispose = true)
        {
            if (quality < 0 || quality > 100)
            {
                return img as Bitmap;
            }


            // Encoder parameter for image quality 
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            var s = new MemoryStream();
            img.Save(s, jpegCodec, encoderParams);
            if (autoDispose)
            {
                img.Dispose();
            }
            return new Bitmap(s);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

	    public static bool IsNewInstance(this string name)
	    {
	        bool cn;
            m = new Mutex(true, name, out cn);
	        if (cn)
	        {
	            GC.KeepAlive(m);
	        }
	        return cn;
	    }

        public static void DisposeService(this CommunicationObject co)
        {
            if (co == null)
            {
                return;
            }
            try
            {
                if (co.State == CommunicationState.Faulted)
                {
                    co.Abort();
                }
                else
                {
                    co.Close();
                }
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                co.Abort();
            }
        }

	    public static object InvokeNamedPipe<T>(this string name, Func<T, object> invoker)
	    {
	        string url = "net.pipe://localhost/{0}".Fmt(name);
	        var b = new NetNamedPipeBinding();
	        b.MaxReceivedMessageSize = int.MaxValue;
	        var r = InvokeService(url, invoker, b);
	        return r;
	    }

	    public static object InvokeService<T>(this string url, Func<T, object> invoker, Binding b = null)
	    {
            ChannelFactory<T> factory = null;
            try
            {
                if (b == null)
                {
                    b = new NetNamedPipeBinding {MaxReceivedMessageSize = int.MaxValue, MaxBufferSize = int.MaxValue};
                }
                factory =
                    new ChannelFactory<T>(b,
                        new EndpointAddress(url));

                T proxy =
                    factory.CreateChannel();
                var d = invoker.Invoke(proxy);
                return d;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                if (factory != null)
                {
                    factory.Abort();
                }
                return null;
            }
            finally
            {
                if (factory != null)
                {
                    if (factory.State == CommunicationState.Faulted)
                    {
                        factory.Abort();
                    }
                    else
                    {
                        factory.Close();
                    }
                }
            }
	    }

        public static Type RetrieveType(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var t = Type.GetType(name);
            return t;
        }

        public static Stream NewStream()
        {
            return new MemoryStream();
        }

	    public static bool HasAttr<T>(this MemberInfo info, out T rlt) where T : class
	    {
	        if (info == null)
	        {
	            rlt = default (T);
	            return false;
	        }
	        T[] o;
	        var r = info.HasAttr(out o);
	        if (o != null)
	        {
	            rlt = o[0];
	        }
	        else
	        {
	            rlt = default (T);
	        }
	        return r;
	    }

	    public static bool HasAttr<T>(this MemberInfo info, out T[] rlt) where T : class
	    {
	        var atts = info.GetCustomAttributes(typeof (T), true);
	        if (atts != null && atts.Length > 0)
	        {
	            rlt = new T[atts.Length];
	            for (int i = 0; i < rlt.Length; i++)
	            {
	                rlt[i] = atts[i] as T;
	            }
	            return true;
	        }
	        rlt = null;
	        return false;
	    }

	    public static string UrlDecode(this string s)
	    {
	        return HttpUtility.UrlDecode(s);
	    }

	    public static string UrlEncode(this string s)
	    {
	        return HttpUtility.UrlEncode(s);
	    }

	    public static string HtmlDecode(this string s)
	    {
	        return HttpUtility.HtmlDecode(s);
	    }

	    public static string HtmlEncode(this string s)
	    {
	        return HttpUtility.HtmlEncode(s);
	    }

        public static IOrderedQueryable<T> Reorder<T>(this IQueryable<T> query, string propertyName)
        {
            var pe = Expression.Parameter(typeof(T));
            var ip = Expression.Property(pe, propertyName);
            var lmd = Expression.Lambda(ip, pe);
            var exp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(T), ip.Type }, query.Expression, lmd);
            var rlt = query.Provider.CreateQuery(exp);
            return (IOrderedQueryable<T>)rlt;
        } 

        public static MethodInfo GetMethodByName(this Type type, string name)
        {
            MethodInfo[] list = type.GetMethods();
            foreach (var i in list)
            {
                if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return null;
        }

		public static string AsString(this object o, bool isLowerCase = false, string defaultValue = null)
		{
			if (o != null)
			{
				if (isLowerCase)
				{
					return o.ToString().ToLowerInvariant();
				}
				return o.ToString();
			}
			return defaultValue;
		}

		public static string[] SubList(this string[] o, int start, int end = -1)
		{
			if (o != null)
			{
				if (start > -1 && start < o.Length)
				{
					if (end < 0)
					{
						end = o.Length - 1;
					}
					else if (end >= o.Length)
					{
						return null;
					}
					string[] rlt = new string[o.Length - start];
					for (int i = start; i <= end; i++)
					{
						rlt[i - start] = o[i];
					}
					return rlt;
				}
			}
			return null;
		}

	    public static T ChangeType<T>(this object value)
	    {
	        return (T)ChangeType(value, typeof (T));
	    }

        public static object ChangeType(this object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != null)
                {
                    NullableConverter nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                }
                else
                {
                    return null;
                }
            }

            return Convert.ChangeType(value, conversionType);
        }

		public static object[] ConvertType(this object[] o, int start, ParameterInfo[] types)
		{
			if (o != null && types != null && o.Length == types.Length + start && o.Length > 0)
			{
				if (start > -1 && start < o.Length)
				{
					int end = o.Length - 1;
					object[] rlt = new object[o.Length - start];
					for (int i = start; i <= end; i++)
					{
						rlt[i - start] = Convert.ChangeType(o.GetValue(i), types[i - start].ParameterType);
					}
					return rlt;
				}
			}
			return null;
		}

        public static T CreateInstance<T>(this string name, string assembly = null)
        {
            try
            {
                return (T)name.CreateInstance(assembly);
            }
            catch
            {
                return default(T);
            }
        }

		public static object CreateInstance(this string name, string assembly = null)
		{
			try
			{
				if (!string.IsNullOrEmpty(name))
				{
					Type type = Type.GetType(name, true);
					if (type != null)
					{
						var o = Activator.CreateInstance(type, true);
                        return o;
					}
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

        public static string PathMap(this string url, string bas = null, char splitter = '/', char joiner = '\\')
        {
            string rlt = url;
            if (bas == null)
            {
                bas = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                bas = bas.PathNormalize();
            }
            if (rlt.StartsWith(splitter.ToString()))
            {
                rlt = rlt.Substring(1);
            }
            if (rlt.EndsWith(splitter.ToString()))
            {
                rlt = rlt.Substring(0, rlt.Length - 1);
            }
            if (rlt.IndexOf("{0}") >= 0)
            {
                rlt = rlt.Fmt(bas);
            }
            else
            {
                rlt = bas + rlt;
            }
            rlt = rlt.PathNormalize(splitter, joiner, false);
            return rlt.Replace("\\\\", "\\");
        }
        public static string PathNormalize(this string path, char splitter = '\\', char joiner = '\\', bool appendTailJoiner = true)
        {
            var list = path.Split(splitter);
            var rlt = new List<string>();
            foreach (var i in list)
            {
                if ("..".Equals(i) && rlt.Count > 0)
                {
                    rlt.RemoveAt(rlt.Count - 1);
                    continue;
                }
                else if (".".Equals(i) || string.IsNullOrEmpty(i.Trim()))
                {
                    continue;
                }
                rlt.Add(i);
            }
            return string.Join(joiner.ToString(), rlt.ToArray()) + (appendTailJoiner ? joiner.ToString() : string.Empty);
        }
		public static string MapPath(this string url, string bas = null)
		{
            if (bas == null)
            {
                bas = AppDomain.CurrentDomain.BaseDirectory;
            }
			if (!string.IsNullOrEmpty(url))
			{
				if (url.IndexOf('/') < 0)
				{
					return bas + url;
				}
				if (string.Equals("/", url))
				{
					return bas;
				}
				if (url.StartsWith("/"))
				{
					url = url.Substring(1);
				}
				string[] ulist = url.Split('/');
				string path = bas;
				path = path.Substring(0, path.Length - 1);
				string[] plist = path.Split('\\');
				//int n = plist.Length - 1;
				List<string> rlt = new List<string>();
				rlt.AddRange(plist);
				foreach (string i in ulist)
				{
					if ("..".Equals(i))
					{
						if (rlt.Count > 1)
						{
							rlt.RemoveAt(rlt.Count - 1);
						}
						else
						{
							return null;
						}
					}
					else
					{
						rlt.Add(i);
					}
				}
				return string.Join("\\", rlt.ToArray());
			}
			return null;
		}

		public static string FileExt(this string target, bool withDot = false)
		{
			string rlt;
			string filename = target.PathLastName();
			string[] list = filename.Split('.');
			if (list.Length == 1)
			{
				rlt = list[0];
			}
			else
			{
				rlt = list[list.Length - 1];
			}
			if (withDot)
			{
				rlt = '.' + rlt;
			}
			return rlt;
		}

		public static string PathLastName(this string target, bool includeExt = true, params char [] splitter)
		{
			if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(target.Trim()))
			{
				if (splitter == null || splitter.Length < 1)
				{
					splitter = new [] {'\\'};
				}
				string[] list = target.Split(splitter);
				string result = list[list.Length - 1];
				if (!includeExt)
				{
					int pos = result.LastIndexOf('.');
					if (pos <= 0)
					{
						result = string.Empty;
					}
					else if (pos > 0)
					{
						result = result.Substring(0, pos - 1);
					}
					//result = result.Split('.')[0];
				}
				return result;
			}
			return string.Empty;
		}

        public static string PathWithoutFilename(this string file)
        {
            var list = new List<string>(file.Split('\\'));
            list.RemoveAt(list.Count - 1);
            return string.Join("\\", list.ToArray());
        }

        public static string PathAppendFilename(this string path, string filename)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }
                path += filename;
            }
            return path;
        }

	    public static string MakeAbsolutePath(this string path)
	    {
	        if (!path.IsAbsolutePath())
	        {
	            return AppDomain.CurrentDomain.BaseDirectory + path;
	        }
	        return path;
	    }
	    public static bool IsAbsolutePath(this string path)
	    {
	        if (string.IsNullOrEmpty(path))
	        {
	            return false;
	        }
	        if (path.IndexOf(':') >= 0 || path.StartsWith("\\\\"))
	        {
	            return true;
	        }
	        return false;
	    }

		public static List<T> Fill<T>(this IDataReader reader) where T : new()
		{
			if (reader == null)
			{
				return null;
			}
			List<T> rlt = new List<T>();
			Type type = typeof(T);
			PropertyInfo[] plist = type.GetProperties();
			FieldInfo[] flist = type.GetFields();
			while (reader.Read())
			{
				T item = new T();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					bool isContinue = false;
					foreach (PropertyInfo pi in plist)
					{
						if (pi.CanWrite && string.Equals(pi.Name, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
						{
							pi.SetValue(item, reader.GetValue(i), null);
							isContinue = true;
							break;
						}
					}
					if (isContinue)
					{
						continue;
					}
					foreach (FieldInfo fi in flist)
					{
						if (string.Equals(fi.Name, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
						{
							fi.SetValue(item, reader.GetValue(i));
							break;
						}
					}
				}
				rlt.Add(item);
			}
			return rlt;
		}

		public static string Left(this string content, int count)
		{
			if (count < 0)
			{
				count = content.Length + count;
			}
			if (count <= 0)
			{
				return string.Empty;
			}
			if (count > 0 && count < content.Length)
			{
				return content.Substring(0, count);
			}
			if (count >= content.Length)
			{
				return content;
			}
			return content;
		}

		public static string ToJson(this object o)
		{
			if (o != null)
			{
				try
				{
					JavaScriptSerializer jss = new JavaScriptSerializer();
					return jss.Serialize(o);
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

        public static T FromJsonFile<T>(this string file, T def = null)  where T : class
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (file.IndexOf("/") >= 0)
                {
                    file = file.MapPath();
                }
                if (File.Exists(file))
                {
                    var c = File.ReadAllText(file);
                    var r = c.FromJson(typeof(T));
                    if (r != null)
                    {
                        return r as T;
                    }
                }
            }
            if (def != null)
            {
                return def;
            }
            return default(T);
        }

        public static object FromJson(this string json, Type t)
        {
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    jss.MaxJsonLength = int.MaxValue;
                    return jss.Deserialize(json, t);
                }
                catch (Exception ex)
                {
                    Error.Handle(ex);
                    return null;
                }
            }
            return null;
        }

	    public static string To(this string s, params string[] args)
	    {
	        if (!string.IsNullOrEmpty(s))
	        {
	            if (args == null)
	            {
	                return s;
	            }
	            return string.Format(s, args);
	        }
	        return s;
	    }

		public static T FromJson<T>(this string json)
		{
            try
            {
                return (T)json.FromJson(typeof(T));
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return default(T);
            }
		}

		public static T FromXml<T>(this string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				try
				{
					StringReader sr = new StringReader(xml);
					XmlSerializer xs = new XmlSerializer(typeof(T));
					T result = (T)xs.Deserialize(sr);
					sr.Close();
					return result;
				}
				catch
				{
					return default(T);
				}
			}
			return default(T);
		}

	    public static string Fmt(this string s, params object[] args)
	    {
	        if (!string.IsNullOrEmpty(s))
	        {
	            return string.Format(s, args);
	        }
	        return s;
	    }
	    public static List<T> EnumFieldValue<T>(this Type type, Func<FieldInfo, bool> callback = null, object target = null, BindingFlags flags = BindingFlags.Public | BindingFlags.GetField | BindingFlags.Static | BindingFlags.FlattenHierarchy)
	    {
	        if (type == null)
	        {
	            return null;
	        }
            List<T> rlt = new List<T>();
	        FieldInfo [] fi = type.GetFields(flags);
	        foreach (FieldInfo i in fi)
	        {
	            if (callback == null || callback(i))
	            {
                    object o = i.GetValue(target);
                    rlt.Add(o.ChangeType<T>());
	            }
	        }
	        return rlt;
	    }

		public static string ToXml(this object o, string file = null)
		{
			if (o != null)
			{
				StringBuilder b = new StringBuilder();
				StringWriter sw = new StringWriter(b);
				XmlSerializer xs = new XmlSerializer(o.GetType());
				xs.Serialize(sw, o);
				sw.Close();
				if (file != null)
				{
					try
					{
						File.WriteAllText(file, b.ToString());
					}
					catch (Exception e)
					{
						Error.Handle(e);
					}
				}
				return b.ToString();
			}
			return null;
		}

		public static T[] ToArray<T>(this IList list, string fieldName) where T : class
		{
			if (list == null || list.Count < 1)
			{
				return null;
			}
			List<T> rlt = new List<T>();
			Type type = list[0].GetType();
			PropertyInfo pi = type.GetProperty(fieldName);
			FieldInfo fi = type.GetField(fieldName);
			foreach (object i in list)
			{
				try
				{
					if (pi != null && pi.CanRead)
					{
						object v = pi.GetValue(i, null);
						rlt.Add(Convert.ChangeType(v, typeof(T)) as T);
					}
					else if (fi != null)
					{
						object v = fi.GetValue(i);
						rlt.Add(Convert.ChangeType(v, typeof(T)) as T);
					}
				}
				catch (Exception e)
				{
					Error.Handle(e);
				}
			}
			return rlt.ToArray();
		}

	    public static object Clone(this object o)
	    {
            if (o != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, o);
                    stream.Position = 0;
                    object r = formatter.Deserialize(stream);
                    return r;
                }
            }
            return null;
        }
		public static T Clone<T>(this object o) where T:class
		{
            if (o != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, o);
                    stream.Position = 0;
                    object r = formatter.Deserialize(stream);
                    return r as T;
                }
            }
            return default(T);
        }
	}
}
