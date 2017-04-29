using System.IO;

namespace Joy.Data
{

    internal class UnitFactory
    {
        public static Unit GetBasePage(uint pageID, BinaryReader reader)
        {
            reader.Seek(100L + (pageID * 0x1000L));
            if (reader.ReadByte() == 2)
            {
                return GetIndexPage(pageID, reader);
            }
            return GetDataPage(pageID, reader, true);
        }

        public static DataUnit GetDataPage(uint pageID, BinaryReader reader, bool onlyHeader)
        {
            DataUnit dataPage = new DataUnit(pageID);
            ReadFromFile(dataPage, reader, onlyHeader);
            return dataPage;
        }

        public static IndexUnit GetIndexPage(uint pageID, BinaryReader reader)
        {
            IndexUnit indexPage = new IndexUnit(pageID);
            ReadFromFile(indexPage, reader);
            return indexPage;
        }

        public static void ReadFromFile(IndexUnit indexPage, BinaryReader reader)
        {
            long initPos = reader.Seek(100L + (indexPage.UnitID * 0x1000L));
            if (reader.ReadByte() != 2)
            {
                throw new FileDBException("PageID {0} is not a Index Page", new object[] { indexPage.UnitID });
            }
            indexPage.NextUnitID = reader.ReadUInt32();
            indexPage.NodeIndex = reader.ReadByte();
            reader.Seek(initPos + 0x2eL);
            for (int i = 0; i <= indexPage.NodeIndex; i++)
            {
                IndexNode node = indexPage.Nodes[i];
                node.ID = reader.ReadGuid();
                node.IsDeleted = reader.ReadBoolean();
                node.Right.Index = reader.ReadByte();
                node.Right.PageID = reader.ReadUInt32();
                node.Left.Index = reader.ReadByte();
                node.Left.PageID = reader.ReadUInt32();
                node.DataPageID = reader.ReadUInt32();
                node.FileName = reader.ReadString(0x29);
                node.FileExtension = reader.ReadString(5);
                node.FileLength = reader.ReadUInt32();
            }
        }

        public static void ReadFromFile(DataUnit dataPage, BinaryReader reader, bool onlyHeader)
        {
            long initPos = reader.Seek(100L + (dataPage.UnitID * 0x1000L));
            if (reader.ReadByte() != 1)
            {
                throw new FileDBException("PageID {0} is not a Data Page", new object[] { dataPage.UnitID });
            }
            dataPage.NextUnitID = reader.ReadUInt32();
            dataPage.IsEmpty = reader.ReadBoolean();
            dataPage.DataBlockLength = reader.ReadInt16();
            if (!dataPage.IsEmpty && !onlyHeader)
            {
                reader.Seek(initPos + 8L);
                dataPage.DataBlock = reader.ReadBytes(dataPage.DataBlockLength);
            }
        }

        public static void WriteToFile(DataUnit dataPage, BinaryWriter writer)
        {
            long initPos = writer.Seek(100L + (dataPage.UnitID * 0x1000L));
            writer.Write((byte) dataPage.Type);
            writer.Write(dataPage.NextUnitID);
            writer.Write(dataPage.IsEmpty);
            writer.Write(dataPage.DataBlockLength);
            if (!dataPage.IsEmpty)
            {
                writer.Seek(initPos + 8L);
                writer.Write(dataPage.DataBlock, 0, dataPage.DataBlockLength);
            }
        }

        public static void WriteToFile(IndexUnit indexPage, BinaryWriter writer)
        {
            long initPos = writer.Seek(100L + (indexPage.UnitID * 0x1000L));
            writer.Write((byte) indexPage.Type);
            writer.Write(indexPage.NextUnitID);
            writer.Write(indexPage.NodeIndex);
            writer.Seek(initPos + 0x2eL);
            for (int i = 0; i <= indexPage.NodeIndex; i++)
            {
                IndexNode node = indexPage.Nodes[i];
                writer.Write(node.ID);
                writer.Write(node.IsDeleted);
                writer.Write(node.Right.Index);
                writer.Write(node.Right.PageID);
                writer.Write(node.Left.Index);
                writer.Write(node.Left.PageID);
                writer.Write(node.DataPageID);
                writer.Write(node.FileName.ToBytes(0x29));
                writer.Write(node.FileExtension.ToBytes(5));
                writer.Write(node.FileLength);
            }
        }
    }
}

