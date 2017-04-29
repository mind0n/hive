using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Joy.Core
{
	
	public class ResultBase
	{
		private bool isResultSet;
		private Exception lastError;
		private object result;

		public bool IsResultSet
		{
			get { return isResultSet; }
		}

		[XmlIgnore]
		public Exception LastError
		{
			get { return lastError; }
			set
			{
				isResultSet = true;
				lastError = value;
			}
		}

		public string LastErrorMsg
		{
			get
			{
				return lastError == null ? null : lastError.Message;
			}
		}

		public bool IsNoException
		{
			get
			{
				return LastError == null;
			}
		}

		public bool IsSuccessful
		{
			get
			{
				return IsNoException && isResultSet;
			}
		}

		public object Result
		{
			get { return result; }
			set
			{
				isResultSet = true;
				result = value;
			}
		}

		public void Reset()
		{
			isResultSet = false;
			lastError = null;
		}
	}
}
