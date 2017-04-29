using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.DataSchema
{
    public class Result
    {
		public bool IsError
		{
			get
			{
				return LastError != null;
			}
		}
        public bool IsNoException
        {
            get { return LastError == null; }
        }
        public virtual bool IsSuccessful
        {
            get { return LastError == null && !isCanceled; }
        }

		public bool IsCanceled
		{
			get { return isCanceled; }
		}

        public bool ConfirmExecution;
        public Exception LastError;

		private bool isCanceled;

		public void Cancel()
		{
			isCanceled = true;
		}
        public void LogError(string msg, params string[] args)
        {
            LastError = new Exception(string.Format(msg, args));
        }

		public override string ToString()
		{
			if (IsError)
			{
				return string.Concat("Error: ", LastError.ToString());
			}
			return string.Empty;
		}
    }
}
