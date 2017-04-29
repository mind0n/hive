using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreJson
{
	public class Json
	{
		protected object val;
		protected ValType vtype;
		protected Type type;
		protected Func<object, ValType, string>[] cvs;

		public Json(object value, params Func<object, ValType, string>[] customvalidators)
		{
			val = value;
			if (val == null)
			{
				vtype = ValType.Null;
			}
			else
			{
				type = val.GetType();
				var name = type.Name;
				if (name.Has("int", "long", "float", "decimal", "short", "bool"))
				{
					vtype = ValType.Value;
				}
				else if (name.Has("list", "collection", "array"))
				{
					vtype = ValType.Array;
				}
				else if (name.Has("string", "datetime", "guid"))
				{
					vtype = ValType.String;
				}
				else
				{
					vtype = ValType.Object;
				}
			}
			cvs = customvalidators;
		}

		public string CustomValidate()
		{
			foreach (var i in cvs)
			{
				var rlt = i(val, vtype);
				if (rlt != null)
				{
					return rlt;
				}
			}
			return null;
		}

		public string Text()
		{
			try
			{
				if (val == null || vtype == ValType.Null)
				{
					return "null";
				}
				if (cvs != null)
				{
					string result = CustomValidate();
                    if (result != null)
					{
						return result;
					}
				}
				if (vtype == ValType.Value)
				{
					return val.ToString();
				}
				else if (vtype == ValType.String)
				{
					return $"\"{val.ToString().Jsonf()}\"";
				}
				else if (vtype == ValType.Array || vtype == ValType.Object)
				{
					List<string> list = new List<string>();
					if (vtype == ValType.Array)
					{
						return ArrayBuilder(list);
					}
					else
					{
						return ObjBuilder(list);
					}
				}
				else
				{
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
					return "null";
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
			}
			return "null";
		}

		private string ObjBuilder(List<string> list)
		{
			if (val is IDictionary)
			{
				return DictBuilder(list);
			}
			else
			{
				var ps = type.GetProperties(BindingFlags.Public);
				foreach (var i in ps)
				{
					if (!i.CanRead)
					{
						continue;
					}
					var atts = i.GetCustomAttributes(typeof (JsonIgnoreAttribute));
					if (atts == null && atts.Count() < 1)
					{
						continue;
					}
					var v = i.GetValue(val);
					var json = new Json(v, cvs);
					list.Add($"\"{i.Name.Jsonf()}\":{json.Text()}");
				}
				return $"{{ {string.Join(",", list.ToArray())} }}";
			}
		}

		private string DictBuilder(List<string> list)
		{
			var dc = (IDictionary) val;
			foreach (var k in dc.Keys)
			{
				var v = dc[k];
				var json = new Json(v, cvs);
				list.Add($"\"{k.ToString().Jsonf()}\":{json.Text()}");
			}
			return $"{{ {string.Join(",", list.ToArray())} }}";
		}

		private string ArrayBuilder(List<string> list)
		{
			var cn = (ICollection) val;
			foreach (var i in cn)
			{
				var json = new Json(i, cvs);
				list.Add(json.Text());
			}
			return $"[{string.Join(",", list.ToArray())}]";
		}
	}

	public enum ValType
	{
		Null,
		Value,
		String,
		Object,
		Array
	}

	public class Result
	{
		protected bool hasresult { get; set; }

		protected Exception error { get; set; }

		public bool noerror { get { return error == null; } }

		public bool success { get { return noerror && hasresult; } }

		public void Error(Exception ex = null)
		{
			error = ex;
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		public void Error(string msg)
		{
			Error(new Exception(msg));
		}

		public virtual void Set(object result = null)
		{
			hasresult = true;
		}
	}

	public class ObjResult : Result
	{
		protected object result { get; set; }

		public override void Set(object result = null)
		{
			this.result = result;
			base.Set();
		}

		public T Get<T>()
		{
			return (T) result;
		}
	}
}
