using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using Joy.Server.Entities;

namespace Joy.Server.Web.Services
{
	class Opc
	{
		public static string e
		{
			get
			{
				return "=";
			}
		}
	}
	public class HttpDataQuery
	{
		//protected Dictionary<string, string> Queries;
		public Node Queries;
		public string TableName;
		public string Sql;
		protected NameValueCollection Form;
		protected delegate void ParseHandler(Node Root, string Name);
		protected delegate string MakeHandler(Node node, string key, string val);
		protected MakeHandler OnMake;
		protected ParseHandler OnParse;
		protected string Qstyle;
		protected string Cmd;
		public HttpDataQuery(HttpRequest request)
		{
			Cmd = request.Form["act"];
			Queries = new Node();
			Queries["cmd"] = new Node(Cmd, NodeType.String);
			Queries["curtstyle"] = new Node(NodeType.String);
			Queries["fields"] = new Node("name", NodeType.Object);
			//select [pagesize] [fields] from [tables] [wheres] [orders]
			//select [pagesize] [fields] from [tables] where [pk] not in select [excludes] from [tables] [wheres] [orders]
			Queries.AddChild("select", NodeType.Object);
			Queries["select"]["fields"] = new Node("#name", NodeType.Array);
			Queries["select"]["tables"] = new Node("#name", NodeType.Array);
			Queries["select"]["wheres"] = new Node("#list", NodeType.Object);
			Queries["select"]["orders"] = new Node("#list", NodeType.Object);
			Queries["select"]["pagesize"] = new Node("#num", NodeType.Empty);
			Queries["select"]["pageskip"] = new Node("#num", NodeType.Empty);
			Queries["select"]["curtpage"] = new Node("#num", NodeType.Empty);
			Queries["select"]["pk"] = new Node("#name", NodeType.Object);
			Form = request.Form;
			Qstyle = Form["qstyle"];
			Queries.SetValue(Qstyle, NodeType.String, "curtstyle");
			if (string.IsNullOrEmpty(Qstyle))
			{
				Qstyle = "select";
			}
			if (string.IsNullOrEmpty(Cmd))
			{
				Cmd = "normal";
			}
			Node Root = Queries["select"];
			OnParse = ParseTypes;
			EnumNames(null);
			OnParse = ParseNames;
			EnumNames(Root);
			Sql = MakeSql();
		}
		protected string MakeSql()
		{
			string Qstyle, rlt;
			if (Queries["curtstyle"] != null && Queries["curtstyle"].Value != null)
			{
				Qstyle = Queries["curtstyle"].Value.ToString();
			}
			else
			{
				return null;
			}
			if (string.IsNullOrEmpty(Cmd))
			{
				Cmd = "normal";
			}
			if (Cmd.Equals("normal"))
			{
				rlt = MakeNormalSql();
			}
			else if (Cmd.Equals("paged"))
			{
				rlt = MakePagedSql();
			}
			else
			{
				rlt = "";
			}
			return rlt;
		}
		protected string EnumKeys(Node n)
		{
			string rlt = "";
			foreach (string k in n.Keys)
			{
				if (OnMake != null)
				{
					rlt += OnMake(n, k, n.Value.ToString());
					if (n.Value.Equals("#list"))
					{
						break;
					}
					else if (n.Value.Equals("#name"))
					{
						rlt += ",";
					}
				}
			}
			if (!string.IsNullOrEmpty(rlt) && rlt.LastIndexOf(',') == rlt.Length - 1)
			{
				rlt = rlt.Substring(0, rlt.Length - 1);
			}
			return rlt;
		}
		protected string MakeExpValStr(Node n, string k)
		{
			string rlt = "";
			return rlt;
		}
		protected string MakeExpStr(Node n)
		{
			string rlt = "", sfo = "", sfdo = "";
			int num;
			//rlt += fld;
			foreach (string fld in n.Keys)
			{
				string fname = FormatFieldName(fld);
				num = 0;
				foreach (string op in n[fld].Keys)
				{
					num++;
					sfo = "";
					foreach (string val in n[fld][op].Keys)
					{
//{cmd:"sel",curtstyle:"mssql_normal",fields:{labname:"string",labnum:"int"},mssql_normal:{count:{Content:5},fields:[],tables:[],wheres:{labname:{like:{software:"string",network:"string"}},labnum:{=:{903:"int"}}},orders:{labname:{desc:{}},labnum:{}}},mssql_paged:{fields:[],tables:[],pageskip:{},wheres:{},orders:{}}}
						if (n[fld][op][val].Value.Equals("string") || n[fld][op][val].Value.Equals("date") || n[fld][op][val].Value.ToString().IndexOf("bool") >= 0)
						{
							sfo += fname + " " + op + " '" + val + "' or ";
						}
						else
						{
							sfo += fname + " " + op + " " + val + " or ";
						}
					}
					if (!string.IsNullOrEmpty(sfo))
					{
						sfdo = "(" + sfo.Substring(0, sfo.Length - 4) + ") and ";
					}
					else
					{
						sfdo = fname + " " + op + ",";
					}
				}
				if (!string.IsNullOrEmpty(sfdo) && num != 0)
				{
					rlt += sfdo;
				}
				else
				{
					rlt += fname + ",";
				}
			}
			if (!string.IsNullOrEmpty(rlt))
			{
				if (rlt.LastIndexOf(',') == rlt.Length - 1)
				{
					rlt = rlt.Substring(0, rlt.Length - 1);
				}
				else
				{
					rlt = rlt.Substring(0, rlt.Length - 5);
				}
			}
			return rlt;
		}
		protected string MakeFieldsStr(Node n, string k, string v)
		{
			string rlt;
			rlt = k;
			if (v.Equals("#name"))
			{
				rlt = FormatFieldName(n[k].Value);
				//rlt = rlt.Replace(']', ' ');
				//rlt = rlt.Replace(".", "].[");
				//if (n[k].Value.ToString().IndexOf("*") < 0)
				//{
				//    rlt = "[" + n[k].Value.ToString() + "]";
				//}
				//else
				//{
				//    rlt = "*";
				//}
			}
			else if (v.Equals("#list"))
			{
				//rlt = n[k].Value.ToString();
				rlt = MakeExpStr(n);
			}
			return rlt;
		}
		protected string FormatFieldName(object oname)
		{
			string name = oname.ToString();
			string rlt = name;
			if (oname == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(rlt))
			{
				rlt = rlt.Replace(']', ' ');
				rlt = rlt.Replace(".", "].[");
				if (name.IndexOf("*") < 0)
				{
					rlt = "[" + rlt + "]";
				}
				else
				{
					rlt = "*";
				}
			}
			return rlt;
		}
		//protected string OnMake
		protected string MakeNormalSql()
		{
			string rlt = "select [pagesize] [fields] from [tables] [wheres] [orders]";
			string count, fields, tables, wheres, orders;
			Node Root = Queries["select"];
			if (Root["pagesize"]["Content"] != null)
			{
				count = Root["pagesize"]["Content"].Value.ToString();
			}
			else
			{
				count = "";
			}
			OnMake = MakeFieldsStr;
			fields = EnumKeys(Root["fields"]);
			tables = EnumKeys(Root["tables"]);
			wheres = EnumKeys(Root["wheres"]);
			orders = EnumKeys(Root["orders"]);
			TableName = tables;
			if (!string.IsNullOrEmpty(count))
			{
				count = " top " + count;
			}
			if (string.IsNullOrEmpty(fields))
			{
				fields = "*";
			}
			if (!string.IsNullOrEmpty(wheres))
			{
				wheres = " where " + wheres;
			}
			if (!string.IsNullOrEmpty(orders))
			{
				orders = " order by " + orders;
			}
			rlt = rlt.Replace("[pagesize]", count);
			rlt = rlt.Replace("[fields]", fields);
			rlt = rlt.Replace("[tables]", tables);
			rlt = rlt.Replace("[wheres]", wheres);
			rlt = rlt.Replace("[orders]", orders);
			return rlt;
		}
		protected string MakePagedSql()
		{
			string rlt = "select [pagesize] [fields] from [tables] where [wheres] and [pk] not in (select [pageskip] [pk] from [tables] where [wheres]) [orders]";
			string pageskip = "", curtpage, pagesize, fields, tables, pk, wheres, orders;
			int nsize, ncurt;
			Node Root = Queries["select"];
			pagesize = Root["pagesize"]["Content"].Value.ToString();
			curtpage = Root["curtpage"]["Content"].Value.ToString();
			Int32.TryParse(pagesize, out nsize);
			Int32.TryParse(curtpage, out ncurt);
			if (nsize > 0 && ncurt > 0)
			{
				if (ncurt == 1)
				{
					return MakeNormalSql();
				}
				pagesize = " top " + nsize;
				pageskip = " top " + ((ncurt - 1) * nsize);
			}
			//pageskip = Root["pageskip"]["Content"].Value.ToString();
			
			OnMake = MakeFieldsStr;
			pk = EnumKeys(Root["pk"]);
			fields = EnumKeys(Root["fields"]);
			tables = EnumKeys(Root["tables"]);
			wheres = EnumKeys(Root["wheres"]);
			orders = EnumKeys(Root["orders"]);

			if (string.IsNullOrEmpty(fields))
			{
				fields = "*";
			}
			if (!string.IsNullOrEmpty(orders))
			{
				orders = " order by " + orders + "," + pk;
			}
			rlt = rlt.Replace("[pk]", pk);
			rlt = rlt.Replace("[pagesize]", pagesize);
			rlt = rlt.Replace("[pageskip]", pageskip);
			rlt = rlt.Replace("[fields]", fields);
			rlt = rlt.Replace("[tables]", tables);
			rlt = rlt.Replace("[wheres]", wheres);
			rlt = rlt.Replace("[orders]", orders);
			return rlt;
		}
		protected void ParseTypes(Node Root, string Name)
		{
			string val;
			if (string.IsNullOrEmpty(Name))
			{
				return;
			}
			if (Name.IndexOf("t_") == 0)
			{
				val = Form[Name];
				Name = Name.Substring(2, Name.Length - 2);
				Queries["fields"].SetValue(val, NodeType.String, Name);
			}
		}
		protected void ParseNames(Node Root, string Name)
		{
			string[] Splitted;
			string val;
			if (Root == null)
			{
				return;
			}
			if (Name[0] == '_')
			{
				Name = Name.Substring(1, Name.Length - 1);
				if (Root.ContainsKey(Name))
				{
					val = Form["_" + Name];
					if (Root[Name].Value == null)
					{
						return;
					}
					if (Root[Name].Value.Equals("#name") || Root[Name].Value.Equals("#list"))
					{
						Splitted = val.Split(',');
						foreach (string s in Splitted)
						{
							//Node exp = new Node(s, NodeType.String);
							//Root[Name][""] = exp;
							if (Root[Name].Value.Equals("#list"))
							{
								//exp.Type = NodeType.Object;
								//ParseExp(exp, s);
								ParseExp(Root[Name], s);
							}
							else
							{
								Root[Name].AddChild((object)s, NodeType.String);
							}
						}
					}
					else if (Root[Name].Value.Equals("#num"))
					{
						Root[Name]["Content"] = new Node(val, NodeType.Number);
					}
				}
			}
		}
		protected void ParseExp(Node ExpRoot, string Name)
		{
			string[] Splitter;
			string fl, op, vl;
			if (string.IsNullOrEmpty(Name))
			{
				return;
			}
			Splitter = Name.Split('$');

			fl = Splitter[0];
			if (!ExpRoot.ContainsKey(fl))
			{
				ExpRoot[fl] = new Node();
			}
			if (Splitter.Length > 1)
			{
				op = Splitter[1];
				if (!ExpRoot[fl].ContainsKey(op))
				{
					ExpRoot[fl][op] = new Node();
				}
				if (Splitter.Length > 2)
				{
					vl = Splitter[2];
					ExpRoot[fl][op].AddChild(Queries["fields"][fl].Value.ToString(), NodeType.String, vl);
				}
			}
		}
		protected void EnumNames(Node Root)
		{
			foreach (string s in Form.AllKeys)
			{
				OnParse(Root, s);
			}
		}
	}
}
