using System;
using System.IO;

namespace Joy.Data
{
    internal class HeaderFactory
    {
        public static void ReadFromFile(Header header, BinaryReader reader)
        {
            reader.BaseStream.Seek(0L, SeekOrigin.Begin);
            if (reader.ReadString("FileDB".Length) != "FileDB")
            {
                throw new FileDBException("The file is not a valid storage archive");
            }
            if (reader.ReadInt16() != 1)
            {
                throw new FileDBException("The archive version is not valid");
            }
            header.IndexRootPageID = reader.ReadUInt32();
            header.FreeIndexPageID = reader.ReadUInt32();
            header.FreeDataPageID = reader.ReadUInt32();
            header.LastFreeDataPageID = reader.ReadUInt32();
            header.LastPageID = reader.ReadUInt32();
            header.IsDirty = false;
        }

        public static void WriteToFile(Header header, BinaryWriter writer)
        {
            writer.BaseStream.Seek(0L, SeekOrigin.Begin);
            writer.Write("FileDB".ToBytes("FileDB".Length));
            writer.Write((short) 1);
            writer.Write(header.IndexRootPageID);
            writer.Write(header.FreeIndexPageID);
            writer.Write(header.FreeDataPageID);
            writer.Write(header.LastFreeDataPageID);
            writer.Write(header.LastPageID);
        }
    }
}

