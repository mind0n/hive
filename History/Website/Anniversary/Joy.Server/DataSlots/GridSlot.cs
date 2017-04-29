using System;
using System.Data;

namespace Joy.Server.DataSlots
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
	public class GridSlot : TableSlot
	{
		protected GridSlot()
		{
			this["defaultEditor"] = "editbox";
			this["defaultStyle"] = null;
			this["styles"] = null;
		}
		public GridSlot(IDataReader reader, DataSlot style = null, object defaultStyle = null)
			: this()
		{
			if (defaultStyle == null)
			{
				DataSlot dStyle = new DataSlot();
				dStyle["width"] = "70px";
				this["defaultStyle"] = dStyle;				
			}
			if (style == null)
			{
				style = new DataSlot();
			}
			this["styles"] = style;
			Retrieve(reader);
		}
		public void Split(string fieldName, string captionName, params string[] args)
		{
			DataSlot dataset = new DataSlot();
			DataSlotCollection rows = this["rows"] as DataSlotCollection;
			if (rows == null || rows.Count < 1)
			{
				return;
			}
			DataSlot prevRow = rows[0];
			object prevValue = prevRow[fieldName];
			DataSlotCollection temp = new DataSlotCollection();
			foreach (DataSlot i in rows)
			{
				object v = i[fieldName];
				if (!Equals(v, prevValue))
				{
					AddPreviousCategory(captionName, dataset, prevRow, temp, args);
					temp = new DataSlotCollection();
					prevRow = i;
					prevValue = v;
				}
				temp.Add(i);
			}
			AddPreviousCategory(captionName, dataset, prevRow, temp, args);
			this["dataset"] = dataset;
			Remove("rows");
		}

		private static void AddPreviousCategory(string captionName, DataSlot dataset, DataSlot prevRow, DataSlotCollection temp, params string [] args)
		{
			DataSlot data = dataset.Set<DataSlot>(Convert.ToString(prevRow[captionName]));
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
	}
}
