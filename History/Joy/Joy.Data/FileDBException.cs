using System;

namespace Joy.Data
{
    public class FileDBException : ApplicationException
    {
        public FileDBException(string message) : base(message)
        {
        }

        public FileDBException(string message, params object[] args) : base(string.Format(message, args))
        {
        }
    }
}

