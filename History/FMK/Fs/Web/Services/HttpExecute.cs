using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.UI;
using Fs.Data;
using Fs.Entities;

namespace Fs.Web.Services
{
	public class HttpMethodHandler
	{
		public string Result;
		public HttpRequest Request;
		public object ObjInstance;
		public List<string> MethodsFilter;
		public HttpMethodHandler(HttpRequest request, object obj)
		{
			Request = request;
			ObjInstance = obj;
			MethodsFilter = new List<string>();
		}
		public void Invoke()
		{
			string methodName;
			NameValueCollection Form = Request.Form;
			methodName = Form["methodname"];
			if (!MethodsFilter.Contains(methodName))
			{
				Result = "Error: Method not exist. " + methodName;
				return;
			}
			if (string.IsNullOrEmpty(methodName))
			{
				Result = "Error: Method name missing.";
				return;
			}
			Result = (string)Call(ObjInstance, methodName, ParseParams(Form));
		}
		public object[] ParseParams(NameValueCollection Form)
		{
			object[] rlt;
			List<object> pars = new List<object>();
			//NameValueCollection Form = request.Form;
			RequestHelper.EnumKeys(Form, "par_", delegate (string key, string val) {
				pars.Add(val);
			});
			rlt = new object[pars.Count];
			for (int i = 0; i < pars.Count; i++)
			{
				rlt[i] = pars[i];
			}
			return rlt;
		}
		protected object Call(object Obj, string MethodName, object [] Params)
		{
			object rlt;
			try
			{
				MethodInfo dynMethod = Obj.GetType().GetMethod(MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				rlt = dynMethod.Invoke(Obj, Params);
			}
			catch (Exception err)
			{
				rlt = "Error: Method not exist or parameter error." + err.ToString();
			}
			return rlt;
		}
	}
	public class HttpDataHandler
	{
		public string Sql;
		public string TableName;
		protected Node Nodes;
		protected Db Database;
		protected NameValueCollection Form;
		protected string MergeRlt;
		protected string[] prevList;
		public HttpDataHandler(HttpRequest request, Db db)
		{
			string cmd = request.Form["ex"];
			Form = request.Form;
			Database = db;
			Nodes = new Node();
			if (!string.IsNullOrEmpty(cmd))
			{
				if (cmd.Equals("insert"))
				{
					ParseInsertQuery();
				}
				else if (cmd.Equals("update"))
				{
					ParseUpdateQuery();
				}
				else if (cmd.Equals("delete"))
				{
					ParseDeleteQuery();
				}
				else if (cmd.Equals("select"))
				{
					ParseSelectQuery();
				}
			}
		}
		protected void ParseSelectQuery()
		{
			string w = "", o = "", f = "", tb, s;
			tb = FormatFieldName(Form["tb"]);
			TableName = tb;
			f = ParseFields();
			ClauseBuilder cw = new ClauseBuilder(new string[] { " and ", " and ", " or " }, new string[] { "", "", "" });
			Node nw = ParseClause("wheres");
			cw.BuildClause(nw, new string[] { "", "", "" }, 0);
			if (!string.IsNullOrEmpty(cw.MergeRlt))
			{
				w += " where " + cw.MergeRlt;
			}
			ClauseBuilder co = new ClauseBuilder(new string[] { " , ", " " }, new string[] { "", "" });
			Node nd = ParseClause("orders");
			co.BuildClause(nd, new string[] { "", "" }, 0);
			if (!string.IsNullOrEmpty(co.MergeRlt))
			{
				o += " order by " + co.MergeRlt;
			}
			s = " select " + f + " from " + tb + w + o;
			Sql = s;
		}
		protected string ParseFields()
		{
			string rlt = "";
			string fieldstr = Form["fields"];
			string [] fields;
			if (!string.IsNullOrEmpty(fieldstr))
			{
				fields = fieldstr.Split(',');
			}
			else
			{
				return "*";
			}
			foreach (string fld in fields)
			{
				rlt += FormatFieldName(fld) + ",";
			}
			rlt = rlt.Substring(0, rlt.Length - 1);
			return rlt;
		}
		protected Node ParseClauseItem(Node parent, string [] ops, int depth)
		{
			Node rlt;
			if (ops.Length == depth)
			{
				rlt = new Node("end", NodeType.Object);
				return rlt;
			}
			if (parent.ContainsKey(ops[depth]))
			{
				rlt = ParseClauseItem(parent[ops[depth]], ops, depth + 1);
			}
			else
			{
				parent[ops[depth]] = new Node(NodeType.Object);
				rlt = ParseClauseItem(parent[ops[depth]], ops, depth + 1);
			}
			return rlt;
		}
		protected Node ParseClause(string key)
		{
			Node rlt;
			string[] list;
			string[] ops;
			string clause = Form[key];
			if (string.IsNullOrEmpty(clause))
			{
				return null;
			}
			list = clause.Split(',');
			rlt = Nodes[key] = new Node(NodeType.Array);
			foreach (string item in list)
			{
				ops = item.Split('$');
				Nodes[key][""] = ParseClauseItem(Nodes[key], ops, 0);
				//rlt += ParseClauseItem(
				//Node nb = Nodes[key];
				//for (int i=0; i<ops.Length; i++)
				//{
				//    Node nc = new Node(ops[i], NodeType.Object);
				//    nc[ops[i]] = new Node();
				//    //nc.Parent = nb;
				//    //foreach (string k in nb.Keys)
				//    //{
				//    //    if (nb[k].Value != null && nb[k].Value.Equals(op))
				//    //    {
				//    //        nb[k][""] = nc;
				//    //    }
				//    //    else
				//    //    {
				//    //        nb[""] = new Node(op, NodeType.Object);
				//    //        nb[""][""] = nc;
				//    //    }
				//    //}
				//    nb[""] = nc;
				//    nb = nc;
				//}
			}
			//rlt = Nodes.ToJsonString();
			//rlt = Nodes[key];
			return rlt;
		}
		#region modify data
		protected void ParseInsertQuery()
		{
			DataColumnCollection cols;
			TableEntity te;
			string sql;
			string tb = FormatFieldName(Form["tb"]);
			te = new TableEntity(tb, Database);
			DataRow dr = te.GetNewRow();
			cols = te.GetColumns();
			foreach (DataColumn col in cols)
			{
				string v = Form["fld_" + col.ColumnName];
				if (!string.IsNullOrEmpty(v))
				{
					string rlt;
					te.MakeFieldSql(col.ColumnName, v, out rlt, false, false);
					
					dr[col.ColumnName] = rlt;
				}
			}
			sql = te.MakeInsertSql(dr, true, false);
			Sql = sql;
		}
		protected void ParseUpdateQuery()
		{
			DataColumnCollection cols;
			TableEntity te;
			string pv, sql;
			string tb = FormatFieldName(Form["tb"]);
			string pk = Form["pk"];
			te = new TableEntity(tb, Database);
			DataRow dr = te.GetNewRow();
			cols = te.GetColumns();
			pv = Form["fld_" + pk];
			foreach (DataColumn col in cols)
			{
				string v = Form["fld_" + col.ColumnName];
				if (!string.IsNullOrEmpty(v))
				{
					string rlt;
					te.MakeFieldSql(col.ColumnName, v, out rlt, false, false);
					dr[col.ColumnName] = rlt;
				}
			}
			sql = te.MakeUpdateSql(dr, te.MakeOpSql(pk, pv), true, false);
			Sql = sql;
		}
		protected void ParseDeleteQuery()
		{
			TableEntity te;
			string[] pvs;
			string pv;
			string sql = "";
			string pk = Form["pk"];
			string tb = FormatFieldName(Form["tb"]);
			te = new TableEntity(tb, Database);
			foreach (string key in Form.Keys)
			{
				string val;
				string fld = key;
				if (key.IndexOf("fld_") == 0 && key.Length > 4)
				{
					fld = key.Substring(4, key.Length - 4);
				}
				else
				{
					continue;
				}
				val = Form[key];
				if (!string.IsNullOrEmpty(val))
				{
					pvs = val.Split(',');
					foreach (string pval in pvs)
					{
						sql += te.MakeOpSql(fld, pval) + " or ";
					}
					if (pvs.Length > 0)
					{
						sql = sql.Substring(0, sql.Length - 4);
					}
					sql += " and ";
				}
			}
			if (!string.IsNullOrEmpty(sql))
			{
				sql = sql.Substring(0, sql.Length - 5);
			}
			//pv = Form["fld_" + pk];
			////pk = FormatFieldName(pk);
			//if (!string.IsNullOrEmpty(pv))
			//{
			//    pvs = pv.Split(',');
			//    foreach (string pval in pvs)
			//    {
			//        sql += te.MakeOpSql(pk, pval) + " or ";
			//    }
			//    if (pvs.Length > 1)
			//    {
			//        sql = sql.Substring(0, sql.Length - 4);
			//    }
			//}
			sql = te.MakeDeleteSql(sql);
			Sql = sql;
		}
		#endregion
		protected string FormatFieldName(object oname)
		{
			string name;
			string rlt;
			if (oname == null)
			{
				return null;
			}
			name = oname.ToString();
			rlt = name;
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

	}
}
