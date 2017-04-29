using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Joy.Core
{
	
	[Serializable]
	public class ResultBase
	{
        protected List<object> resultSet = new List<object>(); 
		private bool isResultSetted;
		private Exception lastError;

		public bool IsResultSetted
		{
			get { return isResultSetted; }
		}

		[XmlIgnore]
		public Exception LastError
		{
			get { return lastError; }
			set
			{
				isResultSetted = true;
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
				return IsNoException && isResultSetted;
			}
		}

		public object Result
		{
		    get
		    {
		        if (resultSet != null && resultSet.Count > 0)
		        {
		            return resultSet[0];
		        }
		        return null;
		    }
			set
			{
				isResultSetted = true;
			    if (resultSet == null)
			    {
			        resultSet = new List<object>();
			    }
			    if (value != null)
			    {
			        resultSet.Add(value);
			    }
			    else
			    {
			        resultSet.Clear();
			    }
			}
		}

		public void Reset()
		{
			isResultSetted = false;
			lastError = null;
		}
	}
}
