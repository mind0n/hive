using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Joy.Server.DataSlots
{
	public class DataSlot : Dictionary<string, object>
	{
		public T Set<T>(string key, object value = null, bool autoCreate = true) where T : class, new()
		{
			var rlt = default(T);
			if (ContainsKey(key))
			{
				rlt = this[key] as T;
			}
			else if (autoCreate || value != null)
			{
				if (value == null)
				{
					rlt = new T();
					this[key] = rlt;
				}
				else
				{
					rlt = this as T;
					this[key] = value;
				}
			}
			return rlt;
		}
	}
	public class TableSlot : DataSlot
	{
		public TableSlot()
		{
			this["rows"] = new DataSlotCollection();
			this["cols"] = new DataSlotCollection();
		}
		public void Retrieve(IDataReader reader)
		{
			var isColumnsEmpty = true;
			var rows = this["rows"] as DataSlotCollection;

			while (reader.Read())
			{
				if (isColumnsEmpty)
				{
					isColumnsEmpty = false;
					for (int i = 0; i < reader.FieldCount; i++)
					{
						string field = reader.GetName(i);
						string type = reader.GetFieldType(i).Name;
						string dtype = reader.GetDataTypeName(i);
						var list = this["cols"] as DataSlotCollection;
						if (list != null)
						{
							var col = new DataSlot();
							col["field"] = field;
							col["caption"] = field;
							col["type"] = type;
							col["datatype"] = dtype;
							list.Add(col);
						}
					}

				}
				if (rows != null)
				{
					var row = new DataSlot();
					for (int i = 0; i < reader.FieldCount; i++)
					{
						string field = reader.GetName(i);
						row[field] = reader.GetValue(i);
					}
					rows.Add(row);
				}

			}
		}
		public DataSlot Columns(string fieldName)
		{
			var cols = GetColumns();
			return cols != null ? cols[fieldName, "field"] : null;
		}
		protected DataSlotCollection GetColumns()
		{
			return this["cols"] as DataSlotCollection;
		}

	}
	public class DataSlotCollection : List<DataSlot>
	{
		public DataSlot this[string key, string property]
		{
			get
			{
				return (
					from col in this 
					let field = col[property] as string 
					where !string.IsNullOrEmpty(field) && String.Equals(key, field, StringComparison.OrdinalIgnoreCase) 
					select col
				).FirstOrDefault();
			}
		}
	}
}
