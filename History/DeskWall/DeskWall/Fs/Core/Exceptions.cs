using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;
using Fs.Core;

namespace Fs
{
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
	}
}
