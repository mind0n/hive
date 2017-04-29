using System.IO;
using System.Runtime.InteropServices;

namespace Joy.Data
{
    internal static class IOExceptionExtensions
    {
        public static bool IsLockException(this IOException exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & 0xffff;
            if (errorCode != 0x20)
            {
                return (errorCode == 0x21);
            }
            return true;
        }
    }
}

