using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Joy.Server.Widgets
{
	/*
	var config = {
		defaultEditor: 'editbox',
		styles: {
			uname: { width: '70px' },
			upwd: { width: '70px' },
			utype: { width: '70px' },
			unote: { width: '70px' }
		},
		cols: [
            { field: 'uname', caption: 'Username', type: 'string' }
            , { field: 'upwd', caption: 'Password', type: 'string' }
            , { field: 'utype', caption: 'User Type', type: 'int' }
            , { field: 'unote', caption: 'Description', type: 'string' }
        ]
	};
    var data = {
        rows: [
            { uname: 'admin', upwd: 'nothing', utype: 0, unote: 'System administrator' }
            , { uname: 'temp', upwd: 'nothing' }
            , { uname: 'guest', upwd: 'nothing' }
            , { uname: 'user' }
        ]
    };
	 */
	public class GridWidget : Widget
	{
		protected GridWidget()
		{
			this["defaultEditor"] = "editbox";
			this["defaultStyle"] = null;
			this["styles"] = null;
			this["cols"] = new WidgetCollection();
			this["rows"] = new WidgetCollection();
		}
		public GridWidget(IDataReader reader, Widget style = null, object defaultStyle = null)
			: this()
		{
			if (defaultStyle == null)
			{
				Widget dStyle = new Widget();
				dStyle["width"] = "70px";
				this["defaultStyle"] = dStyle;				
			}
			if (style == null)
			{
				style = new Widget();
			}
			this["styles"] = style;
			Retrieve(reader);
		}
		public void Split(string fieldName, string captionName, params string[] args)
		{
			Widget dataset = new Widget();
			WidgetCollection rows = this["rows"] as WidgetCollection;
			if (rows == null || rows.Count < 1)
			{
				return;
			}
			Widget prevRow = rows[0];
			object prevValue = prevRow[fieldName];
			WidgetCollection temp = new WidgetCollection();
			foreach (Widget i in rows)
			{
				object v = i[fieldName];
				if (!object.Equals(v, prevValue))
				{
					AddPreviousCategory(captionName, dataset, prevRow, temp, args);
					temp = new WidgetCollection();
					prevRow = i;
					prevValue = v;
				}
				temp.Add(i);
			}
			AddPreviousCategory(captionName, dataset, prevRow, temp, args);
			this["dataset"] = dataset;
			this.Remove("rows");
		}

		private static void AddPreviousCategory(string captionName, Widget dataset, Widget prevRow, WidgetCollection temp, params string [] args)
		{
			Widget data = dataset.Use<Widget>(Convert.ToString(prevRow[captionName]));
			foreach (string i in args)
			{
				string[] item = i.Split('=');
				if (item.Length == 1 && prevRow.ContainsKey(item[0]))
				{
					data[i] = prevRow[item[0]];
				}
				else if (item.Length > 1)
				{
					string key = item[0];
					item[0] = string.Empty;
					data[key] = string.Join("=", item).Substring(1);
				}
			}
			data["rows"] = temp;
		}
		public void Retrieve(IDataReader reader)
		{
			bool isColumnsEmpty = true;
			WidgetCollection rows = this["rows"] as WidgetCollection;

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
						WidgetCollection list = this["cols"] as WidgetCollection;
						if (list != null)
						{
							Widget col = new Widget();
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
					Widget row = new Widget();
					for (int i = 0; i < reader.FieldCount; i++)
					{
						string field = reader.GetName(i);
						row[field] = reader.GetValue(i);
					}
					rows.Add(row);
				}
				
			}
		}
		public Widget Columns(string fieldName)
		{
			WidgetCollection cols = GetColumns();
			if (cols != null)
			{
				return cols[fieldName, "field"];
			}
			return null;
		}
		protected WidgetCollection GetColumns()
		{
			return this["cols"] as WidgetCollection;
		}
	}
}
