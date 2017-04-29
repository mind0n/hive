
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class JsonParser
	{
		protected string input;
		protected Stack<object> stack;
		protected StringBuilder cache;
		protected char begin;
		protected string field;
		protected string rawValue;
		protected object value;

		protected State state;
		protected object result;
		protected string[] purevals;
		public JsonParser(string json)
		{
			input = json.Trim();
			stack = new Stack<object>();
			cache = null;
			field = null;
			rawValue = null;
			result = null;
			state = State.BeforeObj;
			purevals = new string[] { "true", "false", "null" };
		}

		public ObjResult Parse()
		{
			var rlt = new ObjResult();
			if (string.IsNullOrWhiteSpace(input))
			{
				rlt.Set(new Dobj());
			}
			for (int i = 0; i < input.Length; i++)
			{
				if (!ProcessChar(i, rlt))
				{
					break;
				}
			}
			if (stack.Count > 0)
			{
				rlt.Error($"Unexpected end of input {stack.Count} object(s) still in stack");
			}
			rlt.Set(result);
			return rlt;
		}

		private bool ProcessChar(int i, ObjResult rlt)
		{
			char ch = input[i];
			if (char.IsWhiteSpace(ch) && state != State.Key && state != State.StrValue && state != State.NumValue)
			{
				return true;
			}
			if (state == State.BeforeObj)
			{
				if (ch == '{')
				{
					state = State.Obj;
				}
				else if (ch == '[')
				{
					state = State.Array;
				}
				else if (ch == ']')
				{
					state = State.ArrayEnd;
				}
				else if (ch == '"' || ch == '\'')
				{
					state = State.BeginStrValue;
				}
				else if (char.IsNumber(ch) || ch == '.')
				{
					cache = new StringBuilder();
					cache.Append(ch);
					state = State.NumValue;
					return true;
				}
				else if (char.IsLetter(ch) && ch.Belong(purevals))
				{
					cache = new StringBuilder();
					cache.Append(ch);
					state = State.PureValue;
					return true;
				}
				else
				{
					rlt.Error($"Expecting object / array but got {ch} at position {i}");
					return false;
				}
			}
			if (state == State.Array)
			{
				var a = new ArrayList();
				Append(a, rlt);
				stack.Push(a);
				field = null;
				state = State.BeforeObj;
				return true;
			}
			if (state == State.Obj)
			{
				var o = new Dobj();
				Append(o, rlt);
				stack.Push(o);
				state = State.BeforeKey;
				return true;
			}
			if (state == State.BeforeKey)
			{
				if (ch == '}')
				{
					state = State.ObjEnd;
				}
				else
				{
					// Not whitespace : key begin
					cache = new StringBuilder();
					state = State.Key;
				}
			}
			if (state == State.Key)
			{
				if (ch == ':')
				{
					state = State.Colon;
				}
				else
				{
					cache.Append(ch);
					return true;
				}
			}
			if (state == State.Colon)
			{
				var key = cache.ToString().Trim();
				cache = new StringBuilder();
				if (key.StartsWith("\"") || key.EndsWith("\""))
				{
					field = key.Trim('"');
				}
				else if (key.StartsWith("'") || key.EndsWith("'"))
				{
					field = key.Trim('\'');
				}
				else
				{
					field = key;
				}
				state = State.BeforeObj;
				return true;
			}
			if (state == State.BeginStrValue)
			{
				begin = ch;
				cache = new StringBuilder();
				state = State.StrValue;
				return true;
			}
			if (state == State.StrValue)
			{
				if ((ch == '"' && begin == '"') || (ch == '\'' && begin == '\''))
				{
					rawValue = cache.ToString();
					value = rawValue;
					state = State.StrValueEnd;
				}
				else
				{
					cache.Append(ch);
					return true;
				}
			}
			if (state == State.NumValue || state == State.PureValue)
			{
				if (state == State.NumValue)
				{
					if (!char.IsNumber(ch) && ch != '.')
					{
						rawValue = cache.ToString();
						if (rawValue.Contains('.'))
						{
							value = decimal.Parse(rawValue);
						}
						else
						{
							value = int.Parse(rawValue);
						}
						state = State.NumValueEnd;
					}
					else
					{
						cache.Append(ch);
						return true;
					}
				}
				else
				{
					if (char.IsLetter(ch))
					{
						cache.Append(ch);
						return true;
					}
					else
					{
						rawValue = cache.ToString().Trim();
						if (string.Equals("true", rawValue, StringComparison.OrdinalIgnoreCase))
						{
							value = true;
						}
						else if (string.Equals("false", rawValue, StringComparison.OrdinalIgnoreCase))
						{
							value = false;
						}
						else
						{
							value = null;
						}
						state = State.PureValueEnd;
					}
				}
			}
			if (state == State.StrValueEnd || state == State.NumValueEnd || state == State.PureValueEnd)
			{
				if (!IsArray())
				{
					var target = Target<Dobj>();
					Dobj.Set(target, field, value);
					if (state == State.NumValueEnd || state == State.PureValueEnd)
					{
						if (char.IsWhiteSpace(ch))
						{
							ClearValueStatus();
							state = State.ObjPostValueEnd;
							return true;
						}
						else if (ch == ',')
						{
							ClearValueStatus();
							state = State.BeforeKey;
							return true;
						}
						else if (ch == '}')
						{
							state = State.ObjEnd;
						}
						else
						{
							rlt.Error($"Unexpected char {ch} after field {field} value {rawValue} at position {i}");
							ClearValueStatus();
							return false;
						}
						ClearValueStatus();
					}
					else
					{
						state = State.ObjPostValueEnd;
						ClearValueStatus();
						return true;
					}
				}
				else
				{
					var target = Target<ArrayList>();
					target.Add(value);
					if (state == State.NumValueEnd || state == State.PureValueEnd)
					{
						if (char.IsWhiteSpace(ch))
						{
							ClearValueStatus();
							state = State.ArrayPostValueEnd;
							return true;
						}
						else if (ch == ',')
						{
							ClearValueStatus();
							state = State.BeforeObj;
							return true;
						}
						else if (ch == ']')
						{
							state = State.ArrayEnd;
						}
						else
						{
							rlt.Error($"Unexpected char {ch} after array value {rawValue} at position {i}");
							ClearValueStatus();
							return false;
						}
						ClearValueStatus();
					}
					else
					{
						state = State.ObjPostValueEnd;
						ClearValueStatus();
						return true;
					}
				}
			}
			if (state == State.ArrayPostValueEnd)
			{
				if (ch == ',')
				{
					state = State.BeforeObj;
					return true;
				}
				else if (ch == ']')
				{
					state = State.ArrayEnd;
				}
				else
				{
					rlt.Error($"Unexpected char {ch} for object at position {i}");
					return false;
				}
			}
			if (state == State.ObjPostValueEnd)
			{
				if (ch == ',')
				{
					if (IsArray())
					{
						state = State.BeforeObj;
					}
					else
					{
						state = State.BeforeKey;
					}
					return true;
				}
				else if (ch == '}')
				{
					state = State.ObjEnd;
				}
				else
				{
					rlt.Error($"Unexpected char {ch} for object at position {i}");
					return false;
				}
			}
			if (state == State.PostObjEnd)
			{
				if (ch == '}')
				{
					state = State.ObjEnd;
				}
				else if (ch == ',')
				{
					if (IsArray())
					{
						state = State.BeforeObj;
					}
					else
					{
						state = State.BeforeKey;
					}
					return true;
				}
				else if (ch == ']')
				{
					state = State.ArrayEnd;
				}
				else
				{
					rlt.Error($"Unexpected post object symbol {ch} at position {i}");
					return false;
				}
			}
			if (state == State.ObjEnd)
			{
				var target = Pop<Dobj>();
				result = target;
				ClearValueStatus();
				if (target != null)
				{
					state = State.PostObjEnd;
					return true;
				}
				else
				{
					rlt.Error($"Unexpected end of object at position {i}");
					return false;
				}
			}
			if (state == State.ArrayEnd)
			{
				var target = Pop<ArrayList>();
				result = target;
				ClearValueStatus();
				if (target != null)
				{
					state = State.PostObjEnd;
					return true;
				}
				else
				{
					rlt.Error($"Unexpected end of array at position {i}");
					return false;
				}
			}
			rlt.Error($"Unexpected state {state} at position {i}");
			return false;
		}

		private void ClearValueStatus()
		{
			field = null;
			rawValue = null;
			begin = Char.MinValue;
		}

		protected void Append(object o, ObjResult rlt)
		{
			if (stack.Count > 0)
			{
				var p = Target<object>();
				if (p is ArrayList)
				{
					var arr = (ArrayList)p;
					arr.Add(o);
				}
				else if (p is Dobj)
				{
					if (!string.IsNullOrEmpty(field))
					{
						var d = (Dobj)p;
						Dobj.Set(d, field, o);
					}
					else
					{
						rlt.Error($"Empty field unexpected");
						if (Debugger.IsAttached)
						{
							Debugger.Break();
						}
					}
				}
				else
				{
					rlt.Error($"Unexpected parent object");
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
				}
			}
		}

		protected bool IsArray()
		{
			var p = stack.Peek();
			return p is ArrayList;
		}

		protected T Target<T>()
		{
			try
			{
				if (stack == null || stack.Count < 1)
				{
					return default(T);
				}
				return (T)stack.Peek();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				return default(T);
			}
		}

		protected T Pop<T>()
		{
			try
			{
				if (stack == null || stack.Count < 1)
				{
					return default(T);
				}
				return (T)stack.Pop();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				return default(T);
			}
		}
	}

}
