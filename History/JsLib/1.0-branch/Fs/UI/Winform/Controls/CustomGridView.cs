using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Fs.UI.Winform.Controls
{
	public class CustomGridView : DataGridView
	{
		public bool autoColumnWidth = true;
		public CheckBox columnCheckBox;
		public bool [] bits;
		public CustomGridView():base()
		{
			DataBindingComplete += new DataGridViewBindingCompleteEventHandler(CustomGridView_DataBindingComplete);
			BackgroundColor = Color.White;
			EditMode = DataGridViewEditMode.EditProgrammatically;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			SelectionChanged += new EventHandler(CustomGridView_SelectionChanged);
			Click += new EventHandler(CustomGridView_Click);
		}

		void CustomGridView_Click(object sender, EventArgs e)
		{
			CustomGridView view = sender as CustomGridView;
			if (view != null)
			{
				if (columnCheckBox != null)
				{
					try
					{
						int index = Convert.ToInt32(columnCheckBox.Text);
						if (index >= 0 && index < Columns.Count)
						{
							DataGridViewRow row = view.CurrentRow;
							DataGridViewCell cell = row.Cells[index];
							bool value = ((bool)(cell.Value ?? false));
							cell.Value = !value;
							value = (bool)(cell.Value);
							bits[row.Index] = value;
							int i;
							bool start = bits[0], flag = true;

							for (i = 0; i < Rows.Count; i++)
							{
								if (bits[i] != start)
								{
									columnCheckBox.CheckState = CheckState.Indeterminate;
									flag = false;
									break;
								}
							}
							if (flag)
							{
								columnCheckBox.CheckState = start ? CheckState.Checked : CheckState.Unchecked;
							}
						}
					}
					catch (Exception)
					{
						throw;
					}
				}
			}
		}

		void CustomGridView_SelectionChanged(object sender, EventArgs e)
		{
		}
		public void AddCheckBoxColumn()
		{
			CheckBox ckBox = new CheckBox();
			ckBox.Name = "CheckAll";
			Controls.Add(ckBox);
			columnCheckBox = ckBox;
			Rectangle rect = this.GetCellDisplayRectangle(0, -1, true);
			ckBox.Size = new Size(14, 14);
			ckBox.Location = new Point(rect.Left + 4, rect.Top + 4);
			//ckBox.ThreeState = true;
			ckBox.CheckStateChanged += new EventHandler(ckBox_CheckStateChanged);
			DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
			chkCol.DataPropertyName = string.Empty;
			chkCol.DisplayIndex = 0;
			chkCol.HeaderText = null;
			chkCol.Width = 20;
			chkCol.Resizable = DataGridViewTriState.False;
			Columns.Add(chkCol);
			ckBox.Text = chkCol.Index.ToString();
		}

		void ckBox_CheckStateChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox != null)
			{
				try
				{
					int index = Convert.ToInt32(checkBox.Text);
					foreach (DataGridViewRow row in Rows)
					{
						if (index >= 0 && index < Columns.Count)
						{
							if (checkBox.CheckState == CheckState.Checked)
							{
								row.Cells[index].Value = true;
								bits[row.Index] = true;
							}
							else if (checkBox.CheckState == CheckState.Unchecked)
							{
								row.Cells[index].Value = false;
								bits[row.Index] = false;
							}
						}
					}
				}
				catch (Exception error)
				{
					throw error;
				}
			}
		}
		void CustomGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			bits = new bool[Rows.Count - 1];
			for (int i = 0; i < Rows.Count - 1; i++)
			{
				bits[i] = false;
			}
			AdjustColumnWidth();
			foreach (DataGridViewRow row in Rows)
			{
				row.Resizable = DataGridViewTriState.False;
			}
		}

		protected void AdjustColumnWidth()
		{
			RowHeadersVisible = false;
			foreach (DataGridViewColumn column in Columns)
			{
				if (column.DisplayIndex + 1 == Columns.Count)
				{
					column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
				//if ("RadioBox".Equals(column.DataPropertyName))
				//{
				//    column.HeaderText = null;
				//    column.Width = 20;
				//    column.Resizable = false;
				//    column.SortMode = DataGridViewColumnSortMode.NotSortable;
				//}else if ("CheckBox".Equals(column.DataPropertyName))
				//{
				//}
			}
		}
	}
}
