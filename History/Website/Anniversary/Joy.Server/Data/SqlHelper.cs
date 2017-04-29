using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using Joy.Core;
using System.Reflection;

namespace Joy.Server.Data
{
	public class SqlHelper
	{
		public static string ParseOrderStr(string orderStr, bool fullFormat)
		{
			string rlt = "";
			int i;
			if (string.IsNullOrEmpty(orderStr))
			{
				return null;
			}
			string[] orders = orderStr.Split(',');
			for (i = 0; i < orders.Length; i++)
			{
				rlt += MakeOrderField(orders[i]) + ",";
			}
			if (i > 0)
			{
				if (fullFormat)
				{
					return " order by " + rlt.Substring(0, rlt.Length - 1);
				}
				else
				{
					return rlt.Substring(0, rlt.Length - 1);
				}
			}
			else
			{
				return null;
			}
		}
		public static string MakeOrderField(string orderStr)
		{
			if (!string.IsNullOrEmpty(orderStr))
			{
				if (orderStr.IndexOf('^') < 0)
				{
					return MakeSafeFieldNameSql(orderStr);
				}
				else
				{
					return string.Concat(MakeSafeFieldNameSql(orderStr.Replace("^", "")), " desc ");
				}
			}
			else
			{
				return null;
			}
		}
		public static string[] MakeSqlPagedSelectSql(string pk, string page, string size, string fieldClause, string tableClause, string whereClause, string orderClause, string groupClause)
		{
			//SELECT * FROM (
			//	SELECT ROW_NUMBER() OVER(ORDER BY ReferenceOrderID) AS RowNumber, *
			//	FROM Production.TransactionHistoryArchive) AS T
			//	WHERE T.RowNumber<=@PageNumber*@Count AND T.RowNumber>(@PageNumber-1)*@Count;
			// {0}:pk; {1}:table; {2}:page; {3}:size; {4}:where; {5}:order; {6}:group; {7}:fields;
			int pg, ps;
			string rlt = "select {7} from (select ROW_NUMBER() over(order by {0}) as RowNumber, * from {1}) as T where T.RowNumber<={2} * {3} and T.RowNumber>({2} - 1) * {3} {4} {5} {6}";
			Int32.TryParse(page, out pg);
			Int32.TryParse(size, out ps);
			if (pg == 0 || ps == 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(orderClause))
			{
				orderClause = " order by " + orderClause;
			}
			if (!string.IsNullOrEmpty(whereClause))
			{
				whereClause = " and " + whereClause;
			}
			if (!string.IsNullOrEmpty(groupClause))
			{
				groupClause = " group by " + groupClause;
			}
			rlt = string.Format(rlt, pk, tableClause, pg, ps, whereClause, orderClause, groupClause, fieldClause);
			return new[] {rlt, MakeCountSql(tableClause, whereClause, pk)};
		}
		public static string[] MakePagedSelectSql(string pk, string page, string size, string fieldClause, string tableClause, string whereClause, string orderClause)
		{
			//select top {5} {*} from {a} where {p>0} and {id} not in (select top {10} {id} from {a} where {p>0} order by {f desc}) order by {f desc}
			int pg, ps;
			//string strpk;

			string rlt = "select top {0} {1} from {2} {3} {7} {4} not in (select top {5} {4} from {2} {3} {6}) {6}";
			Int32.TryParse(page, out pg);
			Int32.TryParse(size, out ps);
			if (pg == 0 || ps == 0)
			{
				return null;
			}
			if (pg == 1)
			{
				rlt = MakeSelectSql(" top " + ps.ToString(CultureInfo.InvariantCulture) + " " + fieldClause, tableClause, whereClause, orderClause, pk);
			}
			else
			{
				if (!string.IsNullOrEmpty(whereClause))
				{
					whereClause = " where (" + whereClause + ") ";
				}
				else
				{
					whereClause = " where 1=1 ";
				}
				if (!string.IsNullOrEmpty(orderClause))
				{
					if (orderClause.IndexOf("[" + pk + "]", StringComparison.OrdinalIgnoreCase) < 0)
					{
						orderClause = " order by " + orderClause + ", " + pk;
					}
					else
					{
						orderClause = " order by " + orderClause;
					}
				}
				else
				{
					orderClause = " order by " + pk;
				}
				if (!string.IsNullOrEmpty(whereClause))
				{
					rlt = string.Format(rlt, ps, fieldClause, tableClause, whereClause, pk, ((pg - 1) * ps).ToString(CultureInfo.InvariantCulture), orderClause, " and ");
				}
				else
				{
					rlt = string.Format(rlt, ps, fieldClause, tableClause, whereClause, pk, ((pg - 1) * ps).ToString(CultureInfo.InvariantCulture), orderClause, "");
				}
			}
			string[] rlts = new[] {rlt, MakeCountSql(tableClause, whereClause, null)};
			return rlts;
		}
		public static string MakeCountSql(string tableClause, string whereClause, string pk)
		{
			StringBuilder rlt = new StringBuilder("select count(*) from ");
			rlt.Append(tableClause);
			if (!string.IsNullOrEmpty(whereClause))
			{
				if (whereClause.IndexOf(" where ", StringComparison.OrdinalIgnoreCase) != 0)
				{
					rlt.Append(" where ");
				}
				rlt.Append(whereClause);
			}
			//if (!string.IsNullOrEmpty(pk))
			//{
			//    rlt.Append(" group by ");
			//    rlt.Append(pk);
			//}
			return rlt.ToString();
		}
		public static string MakeSelectSql(string fieldClause, string tableClause, string whereClause, string orderClause, string pk = null)
		{
			string rlt = "select {0} from {1} {2} {3}";
			if (!string.IsNullOrEmpty(whereClause))
			{
				whereClause = " where " + whereClause;
			}
			if (!string.IsNullOrEmpty(orderClause))
			{
				orderClause = " order by " + orderClause;
				if (!string.IsNullOrEmpty(pk))
				{
					orderClause += ", " + pk;
				}
			}
			rlt = string.Format(rlt, fieldClause, tableClause, whereClause, orderClause);
			return rlt;
		}
		public static string MakeSafeFieldNameSql(string[] lst)
		{
			string rlt = string.Empty;
			if (lst == null)
			{
				return rlt;
			}
			foreach (string i in lst)
			{
				string s;
				if (string.Equals(i, "*"))
				{
					s = ",*";
				}
				else
				{
					s = string.Concat(",", i.IndexOf("^", StringComparison.Ordinal) < 0 
						? MakeSafeFieldNameSql(i) : MakeOrderField(i));
				}
				rlt += s;
			}
			rlt = rlt.Substring(1);
			return rlt;
		}
		public static string MakeSafeFieldNameSql(string name, char splitChar)
		{
			string[] lst = name.Split(splitChar);
			return MakeSafeFieldNameSql(lst);
		}
		public static string MakeSafeFieldNameSql(string fieldName)
		{
			StringBuilder rlt = new StringBuilder(fieldName);
			rlt.Replace('[', ' ');
			rlt.Replace(']', ' ');
			rlt.Replace("'", "''");
			rlt.Insert(0, '[');
			rlt.Append(']');
			return rlt.ToString();
		}
		public static string MakeSafeFieldValue(string fieldVal, DataColumnCollection colinfo, DataColumn col)
		{
			if (colinfo != null)
			{
				DataColumn c = colinfo[col.ColumnName];
				if (c != null)
				{
					return MakeSafeFieldValue(fieldVal, c.DataType.ToString(), true);
				}
				throw Exceptions.LogOnly("Column missing error ({0})", col.ColumnName);
			}
			throw Exceptions.LogOnly("Missing column definition collection for {0}", col.ColumnName);
		}
		public static string MakeSafeFieldValue(string fieldVal, string fieldType = "str", bool isFullFormat = false)
		{
			string rlt;
			fieldType = fieldType.ToLower();
			if (fieldType.IndexOf("int", StringComparison.OrdinalIgnoreCase) >= 0 || fieldType.IndexOf("num", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				int n;
				int.TryParse(fieldVal, out n);
				rlt = n.ToString(CultureInfo.InvariantCulture);
			}
			else if (fieldType.IndexOf("bool", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				if (!string.IsNullOrEmpty(fieldVal))
				{
					rlt = fieldVal.Equals("true", StringComparison.OrdinalIgnoreCase).ToString();
				}
				else
				{
					rlt = "False";
				}
				if (isFullFormat)
				{
					rlt = "'" + rlt + "'";
				}
			}
			else
			{
				rlt = fieldVal.Replace("'", "''");
				if (isFullFormat)
				{
					rlt = "'" + rlt + "'";
				}
			}
			return rlt;
		}

	}
	public class SqlObject
	{
		protected StringBuilder Builder = new StringBuilder();
		protected bool IsWhereExist = false;
		public SqlObject DeleteFrom(string table)
		{
			Builder.Append(" delete from " + SqlHelper.MakeSafeFieldNameSql(table) + " ");
			return this;
		}
		public SqlObject Select(params string[] fields)
		{
			Builder.Append(" select ");
			Builder.Append(string.Join(",", fields));
			return this;
		}
		public SqlObject SelectTop(int number, params string[] fields)
		{
			Builder.Append(" select top ");
			Builder.Append(number);
			Builder.Append(" ");
			if (fields != null && fields.Length > 0)
			{
				Builder.Append(string.Join(",", fields));
			}
			else
			{
				Builder.Append(" * ");
			}
			return this;
		}
		public SqlObject SelectFunc(string name, string par)
		{
			Builder.Append(string.Concat(" select ", name, "(", par, ")"));
			return this;
		}

		public SqlObject InsertInto(object entity)
		{
			if (entity == null)
			{
				return this;
			}
			FieldInfo[] flist = entity.GetType().GetFields();
			List<string> fields = new List<string>();
			List<string> values = new List<string>();
			string table;
			object[] olist = entity.GetType().GetCustomAttributes(typeof(TableAttribute), true);
			if (olist.Length > 0 && olist[0] is TableAttribute)
			{
				var tattr = (TableAttribute) olist[0];
				table = tattr.Name;
			}
			else
			{
				table = entity.GetType().Name;
			}
			foreach (FieldInfo i in flist)
			{
				olist = i.GetCustomAttributes(typeof (ColumnAttribute), true);
				string fieldname;
				string typename;
				if (olist.Length > 0 && olist[0] is ColumnAttribute)
				{
					var attr = (ColumnAttribute) olist[0];
					fieldname = attr.Name;
					typename = attr.Type;
				}
				else
				{
					fieldname = i.Name;
					typename = i.FieldType.Name;
				}
				object value = i.GetValue(entity);
				if (value != null)
				{
					fields.Add(SqlHelper.MakeSafeFieldNameSql(fieldname));
					values.Add(SqlHelper.MakeSafeFieldValue(value.ToString(), typename, true));
				}
			}
			SqlObject so = InsertInto(table, fields.ToArray()).Values(string.Join(",", values.ToArray()));
			return so;
		}

		public SqlObject InsertInto(string table, params string[] fields) 
		{
			Builder.Append(" insert into ");
			Builder.Append(SqlHelper.MakeSafeFieldNameSql(table));
			Builder.Append(string.Concat("(", string.Join(",", fields), ")"));
			return this;
		}
		public SqlObject Values(params object[] values)
		{
			const string template = " values({0})";
			Builder.Append(string.Format(template, values));
			return this;
		}
		public SqlObject Update(string table)
		{
			Builder.Append(" update ").Append(table);
			return this;
		}
		public SqlObject Set(bool isStringType = true, params string[] list)
		{
			List<string> rlt = new List<string>();
			for (int i = 0; i + 1 < list.Length; i += 2)
			{
				string f = list[i];
				string v = list[i + 1];
				if (!isStringType)
				{
					rlt.Add(string.Concat(f, "=", v));
				}
				else
				{
					rlt.Add(string.Concat(f, "='", v, "'"));
				}
			}
			Builder.Append(string.Join(",", rlt.ToArray()));
			return this;
		}
		public SqlObject From(params string[] tables)
		{
			Builder.Append(" from ");
			Builder.Append(string.Join(",", tables));
			return this;
		}
		public SqlObject Where(string field, string opcode, string value, bool isStringType = true)
		{
			Builder.Append(" where ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject And(string field, string opcode, string value, bool isStringType = true)
		{
			Builder.Append(" and ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject Or(string field, string opcode, string value, bool isStringType = true)
		{
			Builder.Append(" or ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject OrderBy(params string[] fields)
		{
			if (fields == null || fields.Length < 1)
			{
				return this;
			}
			Builder.Append(" order by ");
			string[] filtered = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				string f = fields[i];
				if (string.Equals(f, "desc", StringComparison.OrdinalIgnoreCase) || string.Equals(f, "asc", StringComparison.OrdinalIgnoreCase))
				{
					filtered[i] = f;
				}
				else if (!string.IsNullOrEmpty(f))
				{
					filtered[i] = SqlHelper.MakeSafeFieldNameSql(f);
				}
			}
			Builder.Append(string.Join(" ", filtered));
			return this;
		}
		public SqlObject GroupBy(params string[] fields)
		{
			return this;
		}
		public T ExecuteScalar<T>(Db db)
		{
			try
			{
				return db.ExecScalar<T>(ToString());
			}
			catch (Exception e)
			{
				ErrorHandler.Handle(e);
				return default(T);
			}
		}
		public override string ToString()
		{
			return Builder.ToString();
		}
		private void MakeWhereClause(string field, string opcode, string value, bool isStringType)
		{
			if (!isStringType)
			{
				Builder.Append(string.Join(" ", new[] { field, opcode, value }));
			}
			else
			{
				Builder.Append(string.Join(" ", new[] { field, opcode, string.Concat("'", value, "'") }));
			}
		}
	}
}
