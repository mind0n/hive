using System;
using System.Data;

namespace Fs.Data
{
	public class ExecuteResult
	{
		public enum ResultType : int
		{
			Integer
			,
			Error
				,
			DataReader
				, Object
		}
		public Exception Exception;
		public int IntRlt = 0;
		public object ObjRlt = null;
		public IDataReader ReaderRlt = null;
		public ResultType Type;
		public ExecuteResult(object rlt, Exception err)
		{
			Exception = err;
			if (rlt is IDataReader)
			{
				ReaderRlt = rlt as IDataReader;
				Type = ResultType.DataReader;
			}
			else if (rlt is int)
			{
				IntRlt = (int)rlt;
				Type = ResultType.Integer;
			}
			else
			{
				if (err == null)
				{
					ObjRlt = rlt;
					Type = ResultType.Object;
				}
				else
				{
					Exception = err;
					Type = ResultType.Error;
				}
			}
		}
	}
}
