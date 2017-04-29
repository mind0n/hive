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
        public List<object> ResultSet = new List<object>(); 
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

        public string StringResult
        {
            get
            {
                return Result == null ? string.Empty : Result.ToString();
            }
        }

		public object Result
		{
		    get
		    {
		        if (ResultSet != null && ResultSet.Count > 0)
		        {
		            return ResultSet[0];
		        }
		        return null;
		    }
			set
			{
				isResultSetted = true;
			    if (ResultSet == null)
			    {
			        ResultSet = new List<object>();
			    }
			    if (value != null)
			    {
			        ResultSet.Add(value);
			    }
			    else
			    {
			        ResultSet.Clear();
			    }
			}
		}

		public void Reset()
		{
			isResultSetted = false;
			lastError = null;
		}

	    public override string ToString()
	    {
	        return "Result {0} returned {1} exception{2}.".Fmt((Result == null ? "NULL" : Result.ToString()),
	            (IsNoException ? "without" : "contains"), (IsNoException ? string.Empty : LastError.ToString()));
	    }
	}
}
