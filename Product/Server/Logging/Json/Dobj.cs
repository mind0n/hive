using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreJson
{
	[Serializable]
	public class Dobj : DynamicObject
	{
		private readonly IDictionary<string, object> _dictionary;

		internal DobjSettings settings = new DobjSettings();

		protected IDictionary<string, object> Data
		{
			get { return _dictionary; }
		}

		public Dobj(DobjSettings settings = null, IDictionary<string, object> dictionary = null)
		{
			if (dictionary == null)
			{
				_dictionary = new Dictionary<string, object>();
			}
			else
			{
				_dictionary = dictionary;
			}
			if (settings == null)
			{
				settings = new DobjSettings();
			}
			else
			{
				this.settings = settings;
			}
		}

		public string ToJson()
		{
			var json = new Json(this, DobjBuilder);
			return json.Text();
		}

		static string DobjBuilder(object val, ValType vtype)
		{
			if (val is Dobj)
			{
				var self = (Dobj)val;
				var dict = self.Data;
				var data = new Json(dict, DobjBuilder);
				return data.Text();
			}
			return null;
		}

		public static string ToJson(Dobj o)
		{
			return o.ToJson();
		}

		public static Dobj FromJson(string json)
		{
			var reader = new JsonParser(json);
			var rlt = reader.Parse();
			if (rlt.success)
			{
				return rlt.Get<Dobj>();
			}
			return new Dobj();
		}

		public static DobjSettings Settings(Dobj o)
		{
			return o.settings;
		}

		public static bool Exists(Dobj o, string field)
		{
			return o._dictionary.ContainsKey(field);
		}

		public static T Get<T>(Dobj o, string field)
		{
			if (o != null && o._dictionary != null && o._dictionary.ContainsKey(field))
			{
				return (T)o._dictionary[field];
			}
			return default(T);
		}

		public static bool Set(Dobj o, string field, object val)
		{
			if (o != null && o._dictionary != null && !string.IsNullOrEmpty(field))
			{
				o._dictionary[field] = val;
				return true;
			}
			return false;
		}

		public static bool Enum(dynamic o, Func<string, dynamic, bool> cb)
		{
			if (cb != null)
			{
				if (o is Dobj)
				{
					var d = (Dobj)o;
					var data = d._dictionary;
					foreach (var i in data.Keys)
					{
						dynamic v = data[i];
						if (v is IDictionary<string, object>)
						{
							v = new Dobj(d.settings, (IDictionary<string, object>)v);
						}
						if (cb(i, v))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public override string ToString()
		{
			var sb = new StringBuilder("{");
			ToString(sb);
			return sb.ToString();
		}

		private void ToString(StringBuilder sb)
		{
			var firstInDictionary = true;
			foreach (var pair in _dictionary)
			{
				if (!firstInDictionary)
					sb.Append(",");
				firstInDictionary = false;
				var value = pair.Value;
				var name = pair.Key;
				if (value is string)
				{
					sb.AppendFormat("{0}:\"{1}\"", name, value);
				}
				else if (value is IDictionary<string, object>)
				{
					new Dobj(settings, (IDictionary<string, object>)value).ToString(sb);
				}
				else if (value is ArrayList)
				{
					sb.Append(name + ":[");
					var firstInArray = true;
					foreach (var arrayValue in (ArrayList)value)
					{
						if (!firstInArray)
							sb.Append(",");
						firstInArray = false;
						if (arrayValue is IDictionary<string, object>)
							new Dobj(settings, (IDictionary<string, object>)arrayValue).ToString(sb);
						else if (arrayValue is string)
							sb.AppendFormat("\"{0}\"", arrayValue);
						else
							sb.AppendFormat("{0}", arrayValue);

					}
					sb.Append("]");
				}
				else
				{
					sb.AppendFormat("{0}:{1}", name, value);
				}
			}
			sb.Append("}");
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (!_dictionary.TryGetValue(binder.Name, out result))
			{
				if (!settings.AutoCreate)
				{
					result = null;
					return true;
				}
				else
				{
					result = new Dobj(settings);
					_dictionary[binder.Name] = result;
					return true;
				}
			}
			result = WrapResultObject(result);
			return true;
		}

		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			if (indexes.Length == 1 && indexes[0] != null)
			{
				if (!_dictionary.TryGetValue(indexes[0].ToString(), out result))
				{
					// return null to avoid exception.  caller can check for null this way...
					result = null;
					return true;
				}

				result = WrapResultObject(result);
				return true;
			}

			return base.TryGetIndex(binder, indexes, out result);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_dictionary[binder.Name] = value;
			return true;
		}

		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			if (indexes.Length == 1 && indexes[0] != null)
			{
				_dictionary[indexes[0].ToString()] = value;
				return true;
			}
			return false;
		}

		private object WrapResultObject(object result)
		{
			var dictionary = result as IDictionary<string, object>;
			if (dictionary != null)
				return new Dobj(settings, dictionary);

			var arrayList = result as ArrayList;
			if (arrayList != null && arrayList.Count > 0)
			{
				return arrayList[0] is IDictionary<string, object>
					? new List<object>(arrayList.Cast<IDictionary<string, object>>().Select(x => new Dobj(settings, x)))
					: new List<object>(arrayList.Cast<object>());
			}

			return result;
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return _dictionary.Keys;
		}
	}

	public class DobjSettings
	{
		public bool AutoCreate { get; set; }
	}
}
