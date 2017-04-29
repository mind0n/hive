using System;
using System.Data;
using System.Web.UI.WebControls;
using Fs.Data;
using Fs.Entities;

namespace FsWeb.Controller
{
	public class GridControllerEventArgs
	{
		private DataControlRowType rowType;

		public DataControlRowType RowType
		{
			get { return rowType; }
			set { rowType = value; }
		}
		private TableCell cell;

		public TableCell Cell
		{
			get { return cell; }
			set { cell = value; }
		}
		private DataColumn column;

		public DataColumn Column
		{
			get { return column; }
			set { column = value; }
		}
	}
	public class GridController
	{
		protected GridView grid;
		protected Func<DataColumn, DataControlField> OnColumnDataBind;
		protected Func<GridControllerEventArgs, bool> OnCellDataBound;
		protected Func<GridView, TableCell, bool> OnPagerDataBound;
		public GridController(
			GridView gridView
			, Func<DataColumn, DataControlField>onColumnDataBind
			, Func<GridControllerEventArgs, bool>onCellDataBound
			, Func<GridView, TableCell, bool>onPagerDataBound
		)
		{
			grid = gridView;
			OnColumnDataBind = onColumnDataBind;
			OnCellDataBound = onCellDataBound;
			OnPagerDataBound = onPagerDataBound;
		}
		public void DataBind(DataTable table)
		{
			if (table != null)
			{
				bool autoGenerateColumns = grid.AutoGenerateColumns;
				grid.AutoGenerateColumns = false;
				grid.Columns.Clear();
				foreach (DataColumn column in table.Columns)
				{
					DataControlField field = null;
					if (OnColumnDataBind != null)
					{
						field = OnColumnDataBind(column);
					}
					if (field == null)
					{
						field = new BoundField();
						(field as BoundField).DataField = column.ColumnName;
						if (!string.IsNullOrEmpty(column.Caption))
							field.HeaderText = column.Caption;
						else
							field.HeaderText = column.ColumnName;
					}
					grid.Columns.Add(field);
				}
				if (OnPagerDataBound != null)
				{
					grid.ShowFooter = true;
				}
				grid.DataSource = table;
				grid.DataBind();
				grid.AutoGenerateColumns = autoGenerateColumns;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn column = table.Columns[i];
					TableCell cell = grid.HeaderRow.Cells[i];
					bool handled = false;
					if (OnCellDataBound != null)
					{
						handled = OnCellDataBound(new GridControllerEventArgs { Cell = cell, Column = column, RowType = DataControlRowType.Header });
					}
					if (!handled)
					{
						DataObjectAttribute attribute = column.ExtendedProperties[FsConstants.STR_DataObjectAttribute] as DataObjectAttribute;
						if (!attribute.Visible)
						{
							cell.Style.Add("display", "none");
						}
					}
				}
				foreach (GridViewRow row in grid.Rows)
				{
					for (int i = 0; i < table.Columns.Count; i++)
					{
						DataColumn column = table.Columns[i];
						TableCell cell = row.Cells[i];
						bool handled = false;
						if (OnCellDataBound != null)
						{
							handled = OnCellDataBound(new GridControllerEventArgs { Cell = cell, Column = column, RowType = row.RowType });
						}
						if (!handled)
						{
							DataObjectAttribute attribute = column.ExtendedProperties[FsConstants.STR_DataObjectAttribute] as DataObjectAttribute;
							if (!attribute.Visible)
							{
								cell.Style.Add("display", "none");
							}
						}
					}
				}
				if (OnPagerDataBound != null)
				{
					GridViewRow row = grid.FooterRow;
					int colspan = row.Cells.Count;
					row.Cells.Clear();
					TableCell cell = new TableCell();
					cell.Attributes.Add("colspan",colspan.ToString());
					cell.Attributes.Add("class", "pagerContainer");
					row.Cells.Add(cell);
					OnPagerDataBound(grid, cell);
				}
			}
		}
	}
}
