using System;

namespace Joy.Data
{

    internal class Header
    {
        public const string FileID = "FileDB";
        public const short FileVersion = 1;
        public const long HEADER_SIZE = 100L;
        public const long LOCKER_POS = 0x62L;

        public Header()
        {
            this.IndexRootPageID = uint.MaxValue;
            this.FreeIndexPageID = uint.MaxValue;
            this.FreeDataPageID = uint.MaxValue;
            this.LastFreeDataPageID = uint.MaxValue;
            this.LastPageID = uint.MaxValue;
            this.IsDirty = false;
        }

        public uint FreeDataPageID { get; set; }

        public uint FreeIndexPageID { get; set; }

        public uint IndexRootPageID { get; set; }

        public bool IsDirty { get; set; }

        public uint LastFreeDataPageID { get; set; }

        public uint LastPageID { get; set; }
    }
}

