using System;

namespace Joy.Data
{
    internal class IndexLink
    {
        public IndexLink()
        {
            this.Index = 0;
            this.PageID = uint.MaxValue;
        }

        public byte Index { get; set; }

        public bool IsEmpty
        {
            get
            {
                return (this.PageID == uint.MaxValue);
            }
        }

        public uint PageID { get; set; }
    }
}

