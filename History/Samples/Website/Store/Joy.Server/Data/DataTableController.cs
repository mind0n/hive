using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Joy.Server.Reflection;
using Joy.Server.Entities;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Joy.Server.Data
{
	public class FsConstants
	{
		public const string STR_DataObjectAttribute = "DataObjectAttribute";
	}
	public class DataTableFactory
	{
		public static DataTable Create<T>(Type defaultType, params string [] columns) where T:DataTable
		{
			if (defaultType != null)
			{
				//DataTable table = defaultType.Assembly.CreateInstance(defaultType.ToString());
				DataTable table = ClassHelper.CreateInstance<T>();
				if (table != null)
				{
					foreach (string column in columns)
					{
						table.Columns.Add(column, defaultType);
					}
					return table;
				}
			}
			return null;
		}
		public static DataTable Create<T>(Type dataObjectType) where T : DataTable
		{
			DataTable table = ClassHelper.CreateInstance<T>();
			PropertyInfo[] infos = dataObjectType.GetProperties();
			foreach (PropertyInfo info in infos)
			{
				DataObjectAttribute attribute = ClassHelper.GetPropertyAttribute<DataObjectAttribute>(info);
				DataColumn column = new DataColumn(info.Name, info.PropertyType);
				column.ExtendedProperties[FsConstants.STR_DataObjectAttribute] = attribute;
				if (attribute != null && attribute.Order.HasValue)
				{
					column.SetOrdinal(attribute.Order.Value);
				}
				table.Columns.Add(column);
			}
			return table;
		}
		public static DataTable AddRow(DataTable table, params object[] data)
		{
			if (table != null)
			{
				DataRow row = table.NewRow();
				row.ItemArray = data;
				table.Rows.Add(row);
				return table;
			}
			return null;
		}
		public static DataTable AddRow(DataTable table, object item, bool ignoreNullValue)
		{
			if (table != null && item != null)
			{
				DataRow row = table.NewRow();
				PropertyInfo[] list = item.GetType().GetProperties();
				foreach (PropertyInfo info in list)
				{
					object value = info.GetValue(item, null);
					if (ignoreNullValue && value == null)
					{
						//do nothing
					}
					else
					{
						row[info.Name] = value;
					}
				}
				table.Rows.Add(row);
				return table;
			}
			return null;
		}
		public static DataTable AddRows<T>(DataTable table, Collection<T> list, bool ignoreNullValue)
		{
			foreach (T item in list)
			{
				AddRow(table, item, ignoreNullValue);
			}
			return table;
		}
	}
}
