using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Joy.Server.Web
{
	public class JsRequest
	{
		public bool IsAllSuccessful
		{
			get { return lastError == null && Methods.All(i => i.IsNoException); }
		}
		protected bool IsDetailedMessage;
		private Exception lastError;
		public string Message
		{
			get
			{
				if (lastError != null)
				{
					return IsDetailedMessage ? lastError.ToString() : lastError.Message;
				}
				else
				{
					return null;
				}
			}
		}


		public List<Method> Methods = new List<Method>();
		public Method AddMethod(string name)
		{
			Method r = new Method { Name = name };
			Methods.Add(r);
			return r;
		}
		public Exception GetRawException()
		{
			return lastError;
		}
		public void SetException(Exception ex, string name = null)
		{
			if (name == null)
			{
				lastError = ex;
			}
			else
			{
				foreach (Method i in Methods)
				{
					if (string.Equals(i.Name, name))
					{
						i.Error = IsDetailedMessage ? ex.ToString() : ex.Message;
					}
				}
			}
		}
		public void ResetDetailedMessageStatus(bool on)
		{
			IsDetailedMessage = on;
		}
	}

	public class Method
	{
		public string Name;
		public List<Param> Params = new List<Param>();

		protected object returnValue;
		public object MethodReturnValue
		{
			set
			{
				returnValue = value;
			}
		}
		public string ReturnValue
		{
			get
			{
				if (returnValue != null)
				{
					return returnValue.ToString();
				}
				return null;
			}
		}
		public bool IsNoException { get { return string.IsNullOrEmpty(Error); } }
		public string Error;
		public object[] ToArray()
		{
			return Params.ToArray<object>();
		}
		public Param AddParam(string value, string name = null)
		{
			Param r = new Param { Value = value };
			if (!string.IsNullOrEmpty(name))
			{
				r.Name = name;
			}
			Params.Add(r);
			return r;
		}

	}
	public class Param
	{
		public string Name;
		public string Value;
	}
}