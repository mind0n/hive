using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Joy.Core;

namespace Joy.Server.Data
{
	public class SqlHelper
	{
		public static string ParseOrderStr(string OrderStr, bool FullFormat)
		{
			string rlt = "";
			int i;
			if (string.IsNullOrEmpty(OrderStr))
			{
				return null;
			}
			string[] orders = OrderStr.Split(',');
			for (i = 0; i < orders.Length; i++)
			{
				rlt += MakeOrderField(orders[i]) + ",";
			}
			if (i > 0)
			{
				if (FullFormat)
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
		public static string MakeOrderField(string OrderStr)
		{
			if (!string.IsNullOrEmpty(OrderStr))
			{
				if (OrderStr.IndexOf('^') < 0)
				{
					return MakeSafeFieldNameSql(OrderStr);
				}
				else
				{
					return string.Concat(MakeSafeFieldNameSql(OrderStr.Replace("^", "")), " desc ");
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
			return new string [] {rlt, MakeCountSql(tableClause, whereClause, pk)};
		}
		public static string[] MakePagedSelectSql(string pk, string page, string size, string fieldClause, string tableClause, string whereClause, string orderClause)
		{
			//select top {5} {*} from {a} where {p>0} and {id} not in (select top {10} {id} from {a} where {p>0} order by {f desc}) order by {f desc}
			string[] rlts;
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
				rlt = MakeSelectSql(" top " + ps.ToString() + " " + fieldClause, tableClause, whereClause, orderClause, pk);
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
					if (orderClause.IndexOf("[" + pk + "]") < 0)
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
					rlt = string.Format(rlt, ps, fieldClause, tableClause, whereClause, pk, ((pg - 1) * ps).ToString(), orderClause, " and ");
				}
				else
				{
					rlt = string.Format(rlt, ps, fieldClause, tableClause, whereClause, pk, ((pg - 1) * ps).ToString(), orderClause, "");
				}
			}
			rlts = new string [] {rlt, MakeCountSql(tableClause, whereClause, null)};
			return rlts;
		}
		public static string MakeCountSql(string tableClause, string whereClause, string pk)
		{
			StringBuilder rlt = new StringBuilder("select count(*) from ");
			rlt.Append(tableClause);
			if (!string.IsNullOrEmpty(whereClause))
			{
				if (whereClause.IndexOf(" where ") != 0)
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
				string s = string.Empty;
				if (string.Equals(i, "*"))
				{
					s = ",*";
				}
				else
				{
					if (i.IndexOf("^") < 0)
					{
						s = string.Concat(",", MakeSafeFieldNameSql(i));
					}
					else
					{
						s = string.Concat(",", MakeOrderField(i));
					}
				}
				rlt += s;
			}
			rlt = rlt.Substring(1);
			return rlt;
		}
		public static string MakeSafeFieldNameSql(string Name, char SplitChar)
		{
			string[] lst = Name.Split(SplitChar);
			return MakeSafeFieldNameSql(lst);
		}
		public static string MakeSafeFieldNameSql(string FieldName)
		{
			StringBuilder rlt = new StringBuilder(FieldName);
			rlt.Replace('[', ' ');
			rlt.Replace(']', ' ');
			rlt.Replace("'", "''");
			rlt.Insert(0, '[');
			rlt.Append(']');
			return rlt.ToString();
		}
		public static string MakeSafeFieldValue(string FieldVal, DataColumnCollection colinfo, DataColumn col)
		{
			if (colinfo != null)
			{
				DataColumn c = colinfo[col.ColumnName];
				if (c != null)
				{
					return MakeSafeFieldValue(FieldVal, c.DataType.ToString(), true);
				}
				throw Exceptions.LogOnly("Column missing error ({0})", col.ColumnName);
			}
			throw Exceptions.LogOnly("Missing column definition collection for {0}", col.ColumnName);
		}
		public static string MakeSafeFieldValue(string FieldVal, string FieldType = "str", bool IsFullFormat = false)
		{
			string rlt;
			int n;
			FieldType = FieldType.ToLower();
			if (FieldType.IndexOf("int", StringComparison.OrdinalIgnoreCase) >= 0 || FieldType.IndexOf("num", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				int.TryParse(FieldVal, out n);
				rlt = n.ToString();
			}
			else if (FieldType.IndexOf("bool", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				if (!string.IsNullOrEmpty(FieldVal))
				{
					rlt = FieldVal.Equals("true", StringComparison.OrdinalIgnoreCase).ToString();
				}
				else
				{
					rlt = "False";
				}
				if (IsFullFormat)
				{
					rlt = "'" + rlt + "'";
				}
			}
			else
			{
				rlt = FieldVal.Replace("'", "''");
				if (IsFullFormat)
				{
					rlt = "'" + rlt + "'";
				}
			}
			return rlt;
		}

	}
	public class SqlObject
	{
		protected StringBuilder builder = new StringBuilder();
		protected bool isWhereExist = false;
		public SqlObject Select(params string[] fields)
		{
			builder.Append(" select ");
			builder.Append(string.Join(",", fields));
			return this;
		}
		public SqlObject SelectTop(int number, params string[] fields)
		{
			builder.Append(" select top ");
			builder.Append(number);
			builder.Append(string.Join(",", fields));
			return this;
		}
		public SqlObject SelectFunc(string name, string par)
		{
			builder.Append(string.Concat(" select ", name, "(", par, ")"));
			return this;
		}
		public SqlObject InsertInto(string table, params string[] fields) 
		{
			builder.Append(" insert into ");
			builder.Append(table);
			builder.Append(string.Concat("(", string.Join(",", fields), ")"));
			return this;
		}
		public SqlObject Values(params string[] values)
		{
			const string template = " values({0})";
			builder.Append(string.Format(template, values));
			return this;
		}
		public SqlObject Update(string table)
		{
			builder.Append(" update ").Append(table);
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
			builder.Append(string.Join(",", rlt.ToArray()));
			return this;
		}
		public SqlObject From(params string[] tables)
		{
			builder.Append(" from ");
			builder.Append(string.Join(",", tables));
			return this;
		}
		public SqlObject Where(string field, string opcode, string value, bool isStringType = true)
		{
			builder.Append(" where ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject And(string field, string opcode, string value, bool isStringType = true)
		{
			builder.Append(" and ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject Or(string field, string opcode, string value, bool isStringType = true)
		{
			builder.Append(" or ");
			MakeWhereClause(field, opcode, value, isStringType);
			return this;
		}
		public SqlObject OrderBy(params string[] fields)
		{
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
			return builder.ToString();
		}
		private void MakeWhereClause(string field, string opcode, string value, bool isStringType)
		{
			if (!isStringType)
			{
				builder.Append(string.Join(" ", new string[] { field, opcode, value }));
			}
			else
			{
				builder.Append(string.Join(" ", new string[] { field, opcode, string.Concat("'", value, "'") }));
			}
		}
	}
}
