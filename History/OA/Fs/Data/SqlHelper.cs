using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Data
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
				if (OrderStr.IndexOf('^') > 0)
				{
					return MakeSafeFieldNameSql(OrderStr.Substring(0, OrderStr.Length - 1));
				}
				else
				{
					return MakeSafeFieldNameSql(OrderStr) + " desc";
				}
			}
			else
			{
				return null;
			}
		}
		public static string[] MakeSqlServerPagedSelectSql(string pk, string page, string size, string fieldClause, string tableClause, string whereClause, string orderClause, string groupClause)
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
		public static string [] MakePagedSelectSql(string pk, string page, string size, string fieldClause, string tableClause, string whereClause, string orderClause)
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
				rlt = MakeSelectSql(" top " + ps.ToString() + " " + fieldClause, tableClause, whereClause, orderClause);
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
			if (!string.IsNullOrEmpty(pk))
			{
				rlt.Append(" group by ");
				rlt.Append(pk);
			}
			return rlt.ToString();
		}
		public static string MakeSelectSql(string fieldClause, string tableClause, string whereClause, string orderClause)
		{
			string rlt = "select {0} from {1} {2} {3}";
			if (!string.IsNullOrEmpty(whereClause))
			{
				whereClause = " where " + whereClause;
			}
			if (!string.IsNullOrEmpty(orderClause))
			{
				orderClause = " order by " + orderClause;
			}
			rlt = string.Format(rlt, fieldClause, tableClause, whereClause, orderClause);
			return rlt;
		}
		public static string MakeSafeNameSql(string Name, char SplitChar)
		{
			string[] lst = Name.Split(SplitChar);
			string rlt = "";
			foreach (string i in lst)
			{
				string s = i.Replace(']', ' ');
				s = s.Replace("'", "");
				if (i.Equals("*"))
				{
					rlt += "[" + s + "],";
				}
				else
				{
					rlt += "*,";
				}
			}
			rlt = rlt.Substring(0, rlt.Length - 1);
			return rlt;
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
		public static string MakeSafeFieldSql(string FieldVal, string FieldType, bool IsFullFormat)
		{
			string rlt;
			int n;
			FieldType = FieldType.ToLower();
			if (FieldType.IndexOf("int") >= 0 || FieldType.IndexOf("num") >= 0)
			{
				int.TryParse(FieldVal, out n);
				rlt = n.ToString();
			}
			else if (FieldType.IndexOf("bool") >= 0)
			{
				if (!string.IsNullOrEmpty(FieldVal))
				{
					rlt = FieldVal.ToLower().Equals("true").ToString();
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
}
