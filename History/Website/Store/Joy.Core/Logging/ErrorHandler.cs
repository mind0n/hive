using Joy.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Joy.Core.Logging
{
	public class ErrorHandler
	{
		private static Exception lastError;

		public Exception LastError
		{
			get { return lastError; }
		}
		public readonly static Handlers<Exception> ExceptionHandlers = new Handlers<Exception>();

		public static void Handle(string msg, string source, params string[] args)
		{
			Handle(CreateException(msg, source, args));
		}

		public static Exception Handle(Exception e)
		{
			while (e.InnerException != null)
			{
				e = e.InnerException;
			}
			
			if (e != null)
			{
				lastError = e;
				Debug.WriteLine(e.ToString());
				Exceptions.LogOnly(e);
				if (ExceptionHandlers.Count > 0)
				{
					foreach (Action<Exception> item in ExceptionHandlers)
					{
						item(e);
					}
				}
			}
			return e;
		}

		public static void Raise(string msg, string source, params string[] args)
		{
			Exception e = CreateException(msg, source, args);
			throw e;
		}

		private static Exception CreateException(string msg, string source, string[] args)
		{
			string content = string.Concat(source, "\r\n", string.Format(msg, args));
			Exception e = new Exception(content);
			return e;
		}
	}
	public class IgnorableException : Exception
	{
		public IgnorableException(string errinf) : base(errinf) { }
		public IgnorableException() : base() { }
		public IgnorableException(string errinf, params object[] format) : base(string.Format(errinf, format)) { }
	}
	public class CustomException : Exception
	{
		public CustomException(string errinf) : base(errinf) { }
		public CustomException() : base() { }
		public CustomException(string errinf, params object[] format) : base(string.Format(errinf, format)) { }
	}
	public class Exceptions
	{
		public delegate void OutputHandler(Exception error, ExceptionType type);
		public enum ExceptionType : int
		{
			Runtime,
			Debug,
			UserDefined
		}
		class ExceptionItem
		{
			public Exception Detail;
			public ExceptionType Type;
			public ExceptionItem(Exception err, ExceptionType typ)
			{
				Detail = err;
				Type = typ;
			}
		}

		static Object Lock = new Object();
		static List<ExceptionItem> Errors = new List<ExceptionItem>();
		public static OutputHandler Output;

		#region Log Methods
		public static Exception LogOnly(string errinf, params string[] format)
		{
			return LogOnly(string.Format(errinf, format));
		}
		public static Exception LogOnly(string errinf)
		{
			return LogOnly(errinf, ExceptionType.UserDefined);
		}
		public static Exception LogOnly(Exception error)
		{
			return LogOnly(error, ExceptionType.Debug);
		}
		public static Exception LogOnly(string errinf, ExceptionType type)
		{
			return LogOnly(new Exception(errinf), type);
		}
		public static Exception LogOnly(Exception error, ExceptionType type)
		{
			return Log(error, type, delegate(Exception e, ExceptionType t) { });
		}
		public static Exception Log(Exception error)
		{
			return Log(error, ExceptionType.Runtime, null);
		}
		public static Exception Log(Exception error, ExceptionType type)
		{
			return Log(error, type, null);
		}
		public static Exception Log(string errinf)
		{
			return Log(new Exception(errinf), ExceptionType.UserDefined, null);
		}
		public static Exception Log(string errinf, ExceptionType type)
		{
			return Log(new Exception(errinf), type, null);
		}
		public static Exception Log(Exception error, ExceptionType type, OutputHandler OutputCallback)
		{
			lock (Lock)
			{
				OutputHandler callback = new OutputHandler(delegate(Exception e, ExceptionType t) { });
				Errors.Add(new ExceptionItem(error, type));
				if (Output != null)
				{
					callback = Output;
				}
				if (OutputCallback != null)
				{
					callback = OutputCallback;
				}
				if (callback != null)
				{
					callback(error, type);
				}
				Logger.Log(">> Exception Detected >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
				//System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
				//for (int i = 0; i < st.FrameCount; i++)
				//{
				//    System.Diagnostics.StackFrame sf = st.GetFrame(i);
				//    Logger.Log(String.Format(">> File name:{0}\tLine:{1}\tColumn:{2}\tMethodName:{3} ", sf.GetFileName(), sf.GetFileLineNumber(), sf.GetFileColumnNumber(), sf.GetMethod().Name));
				//}
				Logger.Log(error.ToString());
			}
			return error;
		}
		#endregion
		public static string GetExceptionMessage(Exception e)
		{
			string rlt = e.Message;
			while (e.InnerException != null)
			{
				e = e.InnerException;
				rlt += " | " + e.Message;
			}
			return rlt;
		}
	}
	public interface IConfigurable
	{
		object RereadConfigFile();
	}
	public class AttributeInfo
	{
		public Attribute Attribute
		{
			get
			{
				return vattr;
			}
		}
		public MemberInfo MemberInfo
		{
			get
			{
				return vmi;
			}
		}
		protected Attribute vattr;
		protected MemberInfo vmi;
		public AttributeInfo(Attribute attr, MemberInfo memberInfo)
		{
			vmi = memberInfo;
			vattr = attr;
		}
		public T GetAttribute<T>() where T : Attribute
		{
			return (T)vattr;
		}
	}
	public class TypeHelper
	{
		public static AttributeInfo GetMethodCustomAttribute(object vtarget, string method)
		{
			MethodInfo mi = vtarget.GetType().GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (mi != null)
			{
				return new AttributeInfo(GetCustomAttribute<Attribute>(mi), mi);
			}
			return null;
		}
		public static T GetCustomAttribute<T>(MemberInfo mif) where T : class
		{
			object[] attrs = mif.GetCustomAttributes(typeof(T), true);
			if (attrs != null && attrs.Length > 0)
			{
				return attrs[0] as T;
			}
			return default(T);
		}
		public static bool IsAttributeExist<T>(object target) where T : class
		{
			if (GetCustomAttribute<T>(target.GetType()) != null)
			{
				return true;
			}
			return false;
		}
	}

}
