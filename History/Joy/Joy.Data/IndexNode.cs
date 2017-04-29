using System;
using System.IO;

namespace Joy.Data
{

    internal class IndexNode
    {
        public const int FILE_EXTENSION_SIZE = 5;
        public const int FILENAME_SIZE = 0x29;
        public const int INDEX_NODE_SIZE = 0x51;

        public IndexNode(Joy.Data.IndexUnit indexPage)
        {
            this.ID = Guid.Empty;
            this.IsDeleted = true;
            this.Right = new IndexLink();
            this.Left = new IndexLink();
            this.DataPageID = uint.MaxValue;
            this.IndexPage = indexPage;
        }

        public void UpdateFromEntry(EntryInfo entity)
        {
            this.ID = entity.ID;
            this.FileName = Path.GetFileNameWithoutExtension(entity.FileName);
            this.FileExtension = Path.GetExtension(entity.FileName).Replace(".", "");
            this.FileLength = entity.FileLength;
        }

        public uint DataPageID { get; set; }

        public string FileExtension { get; set; }

        public uint FileLength { get; set; }

        public string FileName { get; set; }

        public Guid ID { get; set; }

        public Joy.Data.IndexUnit IndexPage { get; set; }

        public bool IsDeleted { get; set; }

        public IndexLink Left { get; set; }

        public IndexLink Right { get; set; }
    }
}

