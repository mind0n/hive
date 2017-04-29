using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using Fs.Entities;
using System.IO;
using System.Data.Common;
using System.Xml;

namespace Fs.Data
{
	public class StrongTableInfo
	{
		public string PrimaryFieldName = "id";
		public int CurtPage = 1;
		public int PageSize = 5;
		public long TotalRecordCount;
	}
	public class StrongTable : DataTable
	{
		public StrongTableInfo Info
		{
			get
			{
				return vInfo;
			}
		}
		protected StrongTableInfo vInfo;
		public StrongTable()
			: base()
		{
		}
		public StrongTable(string tableName)
			: base(tableName)
		{
		}
		public static StrongTable ParseFromXElement(XElement table)
		{
			XName tname = table.Name;
			StrongTable rlt = new StrongTable();
			if (tname != null)
			{
				rlt.TableName = tname.ToString();
			}
			XElement el = table.FirstNode as XElement;
			if (el != null)
			{
				foreach (XAttribute col in el.Attributes())
				{
					rlt.Columns.Add(col.Name.ToString());
				}
				foreach (XElement row in table.Elements())
				{
					DataRow dr = rlt.NewRow();
					foreach (XAttribute col in row.Attributes())
					{
						dr[col.Name.ToString()] = col.Value;
					}
					rlt.Rows.Add(dr);
				}
			}
			return rlt;
		}
		public string ToXmlString(params string[] ignoreFields)
		{
			return ToXmlString(true, ignoreFields);
		}
		public string ToXmlString(bool rowAsAttribute, params string [] ignoreFields)
		{
			StringBuilder s = new StringBuilder();
			s.Append("<" + TableName + " TotalRecords=\"" + Info.TotalRecordCount + "\">");
			s.Append("<columns>");
			foreach (DataColumn col in Columns)
			{
				if (ignoreFields != null && ignoreFields.Contains<string>(col.ColumnName))
				{
					continue;
				}
				s.Append("<" + col.ColumnName + " Type=\"" + col.DataType + "\" />");
			}
			s.Append("</columns>");
			foreach (DataRow row in Rows)
			{
				if (rowAsAttribute)
				{
					s.Append("<row ");
				}
				else
				{
					s.Append("<row>");
				}
				foreach (DataColumn col in Columns)
				{
					if (ignoreFields != null && ignoreFields.Contains<string>(col.ColumnName))
					{
						continue;
					}
					if (rowAsAttribute)
					{
						s.Append(col.ColumnName + "=\"" + row[col].ToString() + "\" ");
					}
					else
					{
						s.Append("<" + col.ColumnName + ">" + row[col].ToString() + "</" + col.ColumnName + ">");
					}
				}
				if (rowAsAttribute)
				{
					s.Append(" />");
				}
				else
				{
					s.Append("</row>");
				}
			}
			s.Append("</" + TableName + ">");
			return s.ToString();
		}
		public virtual void PagedQuery(Db db, int curtPage, int pageSize, string whereClause, string orderClause, string groupClause)
		{
			if (vInfo == null)
			{
				vInfo = new StrongTableInfo();
			}
			if (pageSize > 0)
			{
				DataTable dt = db.PagedQuery(Info.PrimaryFieldName, curtPage.ToString(), pageSize.ToString(), "*", TableName, whereClause, orderClause, groupClause);
				Merge(dt);
				dt.Dispose();
				Info.CurtPage = curtPage;
				Info.PageSize = pageSize;
				Info.TotalRecordCount = (int)dt.ExtendedProperties["totalrecords"];
			}
		}
	}

}
