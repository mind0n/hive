using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficePortal.MsExcel12;
using Fs.Xml;
using Fs;

namespace OAWidgets
{
	abstract class RangeItem
	{
		public delegate object GetValueDelegator(object content);
		public GetValueDelegator ValueOnGet;
		public virtual bool IsEmpty
		{
			get
			{
				return Values == null || Values.Count < 1;
			}
		}
		public virtual List<Excel.Range> Values
		{
			get
			{
				return vlist;
			}
			set
			{
				vlist = value;
			}
		}protected List<Excel.Range> vlist = new List<Excel.Range>();
		protected readonly int vReservedCols = 0;
		public RangeItem(Excel.Range pos, int reservedCols)
		{
			vReservedCols = reservedCols;
			Parse(pos);
		}
		public void Parse(Excel.Range startPos)
		{
			Excel.Range pos = startPos;
			while (pos.Value != null)
			{
				vlist.Add(pos);
				pos = OffsetNext(pos);
			}
		}
		public T GetValue<T>(int index, T failDefault)
		{
			return GetValue<T>(index, failDefault, true);
		}
		protected T GetValue<T>(int index, T failDefault, bool ignoreReservedCols)
		{
			try
			{
				T rlt;
				if (ignoreReservedCols)
				{
					index += vReservedCols;
				}
				if (vlist == null || index >= vlist.Count)
				{
					return failDefault;
				}
				if (ValueOnGet != null && !string.IsNullOrEmpty(vlist[index].Comment))
				{
					rlt = (T)ValueOnGet(vlist[index]);
				}
				else
				{
					rlt = (T)(vlist[index].Value);
				}
				return rlt;
			}
			catch (System.Exception err)
			{
				Fs.Exceptions.LogOnly(err);
				return failDefault;
			}
		}
		protected abstract Excel.Range OffsetNext(Excel.Range pos);
		protected abstract Excel.Range OffsetPrev(Excel.Range pos);
	}
	class ConfigRangeItem : RangeItem
	{
		protected Type vprocessor;
		public ConfigRangeItem(Excel.Range pos, Type processor)
			: base(pos, 2)
		{
			vprocessor = processor;
			ValueOnGet += new GetValueDelegator(ProcessValue);
		}
		public bool IsEnabled
		{
			get
			{
				return GetValue<bool>(1, false, false);
			}
		}
		public string Name
		{
			get
			{
				string rlt = GetValue<string>(0, null, false);
				return rlt;
			}
		}
		public override List<Excel.Range> Values
		{
			get
			{
				return base.Values.GetRange(2, base.Values.Count - 2);
			}
			set
			{
				base.Values = value;
			}
		}
		public object ProcessValue(object value, string methods)
		{
			if (string.IsNullOrEmpty(methods))
			{
				return value;
			}
			string[] ops = methods.Split('|');
			object rlt = value;
			foreach (string op in ops)
			{
				if (vprocessor != null)
				{
					rlt = vprocessor.InvokeMember(op, System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, null, new object[] { rlt });
				}
			}
			return rlt;
		}
		protected object ProcessValue(object value)
		{
			Excel.Range range = (Excel.Range)value;
			return ProcessValue(range.Value, range.Comment);
			//string[] ops = range.Comment.Split('|');
			//object rlt = range.Value;
			//foreach (string op in ops)
			//{
			//    if (vprocessor != null)
			//    {
			//        rlt = vprocessor.InvokeMember(op, System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, null, new object[] { rlt });
			//    }
			//}
			//return rlt;
		}

		protected override Excel.Range OffsetNext(Excel.Range pos)
		{
			return pos.Offset(0, 1);
		}
		protected override Excel.Range OffsetPrev(Excel.Range pos)
		{
			return pos.Offset(0, -1);
		}
	}
}
