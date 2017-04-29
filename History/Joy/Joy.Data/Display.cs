using System;

namespace Joy.Data
{

    internal static class Display
    {
        public static string Fmt(this uint val)
        {
            if (val == uint.MaxValue)
            {
                return "----";
            }
            return val.ToString("0000");
        }
    }
}

