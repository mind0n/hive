using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

namespace Fs.Entities
{
	//NodeType.Object
	public enum NodeType
	{
		Empty = 0,
		Boolean = 1,
		Number = 2,
		String = 4,
		Array = 8,
		Object = 16
	}
	public class Node : Dictionary<string, Node>
	{
		public string Key;
		public NodeType Type;
		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}
		public new Node this[string key]
		{
			get
			{
				if (ContainsKey(key))
				{
					Node rlt;
					this.TryGetValue(key, out rlt);
					return rlt;
				}
				return null;
			}
			set
			{
				if (string.IsNullOrEmpty(key))
				{
					_id++;
					key = "_" + _id.ToString();
				}
				value.Key = key;
				//if (this.ContainsKey(key))
				//{
				//    foreach (string k in this.Keys)
				//    {
				//        Node nd = new Node();
				//        nd[""] = this[k];
				//    }
				//    this.Remove(key);
				//}
				Add(key, value);
			}
		}
		public Node Parent;
		protected object _value;
		protected int _id;
		public void SetValue(object val, string typ, string key)
		{
			if (!string.IsNullOrEmpty(typ))
			{
				typ = typ.ToLower().Replace("system.", "");
				if (typ.IndexOf("int") == 0)
				{
					SetValue(val, NodeType.Number, key);
				}
				else if (typ == "string" || typ.IndexOf("date") >= 0 || typ.IndexOf("time") >= 0)
				{
					SetValue(val, NodeType.String, key);
				}
				else if (typ.IndexOf("bool") >= 0)
				{
					SetValue(val, NodeType.Boolean, key);
				}
				else if (typ.IndexOf("array") >= 0 || typ.IndexOf("list") >= 0)
				{
					SetValue(val, NodeType.Array, key);
				}
				else
				{
					SetValue(val, NodeType.Object, key);
				}
			}
		}
		public void SetValue(object val, NodeType typ, string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				Value = val;
				Type = typ;
			}
			else
			{
				if (!this.ContainsKey(key))
				{
					this[key] = new Node();
				}
				this[key].Value = val;
				this[key].Type = typ;
				this[key].Parent = this;
			}
		}
		public bool AddChild(object val, NodeType typ)
		{
			string key;
			_id++;
			key = "_" + _id;
			SetValue(val, typ, key);
			return true;
		}
		public bool AddChild(string key, NodeType typ)
		{
			return AddChild(null, typ, key);
		}
		public bool AddChild(object val, NodeType typ, string key)
		{
			if (!this.ContainsKey(key))
			{
				SetValue(val, typ, key);
				return true;
			}
			return false;
		}
		public string ToJsonString()
		{
			string fmt, subrlt = "";
			if (Type == NodeType.Array)
			{
				fmt = "[*]";
			}
			else if (Type == NodeType.Number)
			{
				fmt = "*";
			}
			else if (Type == NodeType.Boolean)
			{
				fmt = "'*'";
			}
			else if (Type == NodeType.String)
			{
				fmt = "\"*\"";
			}
			else if (Type == NodeType.Empty)
			{
				fmt = "*";
			}
			else
			{
				fmt = "{*}";
			}
			foreach (string k in this.Keys)
			{
				Node jnd = this[k];
				string sub = jnd.ToJsonString();
				if (k.IndexOf('_') == 0)
				{
					if (Type == NodeType.Object && Value != null)
					{
						subrlt += Value.ToString() + ":" + sub + ",";
					}
					else
					{
						subrlt += sub + ",";
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(sub))
					{
						subrlt += k + ":" + sub + ",";
					}
				}
			}
			if (!string.IsNullOrEmpty(subrlt))
			{
				subrlt = subrlt.Substring(0, subrlt.Length - 1);
			}
			else
			{
				Page p = new Page();
				if (Type == NodeType.Boolean || Type == NodeType.Number || Type == NodeType.String)
				{
					if (Value != null)
					{
						subrlt = p.Server.HtmlEncode(Value.ToString());
						subrlt = subrlt.Replace("\r\n", "%0A%0D");
					}
				}
				//else
				//{
				//    if (Value != null)
				//    {
				//        subrlt = p.Server.HtmlEncode(Value.ToString());
				//        subrlt = subrlt.Replace("\r\n", "%0A%0D");
				//    }
				//}
			}
			return fmt.Replace("*", subrlt);
		}
		public static Node ParseDataTable(DataTable dt)
		{
			return ParseDataTable(dt, true);
		}
		public static Node ParseDataTable(DataTable dt, bool LowFieldCase)
		{
			int i = 0;
			string fld;
			if (dt == null)
			{
				return null;
			}
			Node root = new Node(NodeType.Object);
			Node cols = new Node(NodeType.Object);
			Node rows = new Node(NodeType.Array);
			cols.Parent = root;
			rows.Parent = root;
			foreach (DataRow row in dt.Rows)
			{
				i++;
				Node n = new Node(NodeType.Object);
				n.Parent = rows;
				foreach (DataColumn col in dt.Columns)
				{
					Node c = new Node(row[col], col.DataType);
					c.Parent = n;
					if (LowFieldCase)
					{
						fld = col.ColumnName.ToLower();
					}
					else
					{
						fld = col.ColumnName;
					}
					if (i == 1)
					{
						cols[fld] = new Node(col.DataType, NodeType.String);
					}
					n[fld] = c;
				}
				rows[""] = n;
			}
			if (!string.IsNullOrEmpty(dt.TableName))
			{
				root["table"] = new Node(dt.TableName.Replace("[", "").Replace("]", ""), NodeType.String);
			}
			root["count"] = new Node(dt.ExtendedProperties["recordcount"], NodeType.Number);
			root["cols"] = cols;
			root["rows"] = rows;
			return root;
		}
		public Node()
		{
			this.Type = NodeType.Object;
		}
		public Node(object obj)
		{
			this.Value = obj;
			this.Type = NodeType.Object;
		}
		public Node(NodeType typ)
		{
			this.Type = typ;
		}
		public Node(object val, NodeType typ)
		{
			this.Type = typ;
			this.Value = val;
		}
		public Node(object val, Type typ)
		{
			SetValue(val, typ.ToString(), null);
		}
	}
}
