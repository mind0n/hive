using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using XL = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace OfficePortal.MsExcel12
{
	public class Excel
	{
		public class Range
		{
			public delegate void CopyHandler(object content);
			public bool IsEmpty
			{
				get
				{
					return Value == null;
				}
			}
			public object Value
			{
				get
				{
					return xRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
				}
				set
				{
					xRange.set_Value(XlRangeValueDataType.xlRangeValueDefault, value);
				}
			}
			public string Comment
			{
				get
				{
					return xRange.NoteText(Type.Missing, Type.Missing, Type.Missing);
				}
			}
			protected XL.Range xRange;
			internal Range(XL.Range range)
			{
				xRange = range;
			}
			public object Copy(CopyHandler RangeOnCopy)
			{
				object origin = xRange.get_Value(Missing.Value), rlt;
				if (RangeOnCopy != null)
				{
					//RangeOnCopy(this);
				}
				rlt = xRange.Copy(Missing.Value);
				if (RangeOnCopy != null)
				{
					RangeOnCopy(this);
					//xRange.set_Value(Missing.Value, origin);
				}
				return rlt;
			}
			public Range Offset(int row, int col)
			{
				return new Range(xRange.get_Offset(row, col));
			}
		}
		public class Sheet
		{
			public string SheetName
			{
				get
				{
					return xSheet.Name;
				}
				set
				{
					xSheet.Name = value;
				}
			}
			protected XL.Worksheet xSheet;
			public Sheet(XL.Worksheet sheet)
			{
				xSheet = sheet;
			}
			public Range GetRange(object cell1, object cell2)
			{
				return new Range(xSheet.get_Range(cell1, cell2));
			}
		}
		public class Book
		{
			public delegate bool EnumSheetsHandler(XL.Worksheet sheet);
			public delegate bool ExternEnumSheetsHandler(Sheet sheet);
			protected XL.Workbook xBook;

			protected Book(XL.Workbook book)
			{
				xBook = book;
			}
			public static Excel.Book Open(string fullPath, bool IsReadOnly)
			{
				//Dim oldCI As System.Globalization.CultureInfo = _
				//    System.Threading.Thread.CurrentThread.CurrentCulture
				//System.Threading.Thread.CurrentThread.CurrentCulture = _
				//    New System.Globalization.CultureInfo("en-US")

				Excel.Book rlt = new Excel.Book(Application.Workbooks.Open
					(fullPath, 0, IsReadOnly, Type.Missing, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", !IsReadOnly, false, 0, false, 1, 0));
				return rlt;
			}
			public Sheet GetSheetByName(string name)
			{
				Sheet s = EnumSheets(delegate(XL.Worksheet sheet)
				{
					if (sheet.Name.Equals(name))
					{
						return false;
					}
					return true;
				});
				return s;
			}
			public void Close()
			{
				xBook.Close(false, null, null);
			}
			public Sheet EnumSheets(ExternEnumSheetsHandler callback)
			{
				return EnumSheets(delegate(Worksheet ws)
				{
					if (callback != null)
					{
						return callback(new Sheet(ws));
					}
					return false;
				});
			}
			protected Sheet EnumSheets(EnumSheetsHandler callback)
			{
				XL.Worksheet w = null;
				for (int i = 1; i <= xBook.Worksheets.Count; i++)
				{
					w = (XL.Worksheet)xBook.Worksheets[i];
					if (!callback(w))
					{
						return new Sheet(w);
					}
				}
				return null;
			}

		}
		public static XL.ApplicationClass Application
		{
			get
			{
				if (xApp == null)
				{
					xApp = new XL.ApplicationClass();
				}
				return xApp;
			}
		}protected static XL.ApplicationClass xApp;
		public static Excel.Book OpenWorkBook(string fullPath, bool IsReadOnly)
		{
			try
			{
				return Excel.Book.Open(fullPath, IsReadOnly);
			}
			catch (Exception err)
			{
				return null;
			}
		}
		protected Excel(){}
		private void releaseObject(object obj)
		{
			try
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
				obj = null;
			}
			catch (Exception ex)
			{
				obj = null;
				throw new Exception("Unable to release the Object " + ex.ToString());
			}
			finally
			{
				GC.Collect();
			}
		}
	}
}
