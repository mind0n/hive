using System;
using System.IO;

namespace Joy.Data
{
    internal class FileFactory
    {
        public static void CreateEmptyFile(BinaryWriter writer)
        {
            Header header = new Header {
                IndexRootPageID = 0,
                FreeIndexPageID = 0,
                FreeDataPageID = uint.MaxValue,
                LastFreeDataPageID = uint.MaxValue,
                LastPageID = 0
            };
            HeaderFactory.WriteToFile(header, writer);
            IndexUnit pageIndex = new IndexUnit(0) {
                NodeIndex = 0,
                NextUnitID = uint.MaxValue
            };
            IndexNode indexNode = pageIndex.Nodes[0];
            indexNode.ID = new Guid(new byte[] { 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f });
            indexNode.IsDeleted = true;
            indexNode.Right = new IndexLink();
            indexNode.Left = new IndexLink();
            indexNode.DataPageID = uint.MaxValue;
            indexNode.FileName = string.Empty;
            indexNode.FileExtension = string.Empty;
            UnitFactory.WriteToFile(pageIndex, writer);
        }
    }
}

