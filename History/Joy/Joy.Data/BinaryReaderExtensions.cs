using System;
using System.IO;
using System.Text;

namespace Joy.Data
{
    internal static class BinaryReaderExtensions
    {
        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return new DateTime(reader.ReadInt64());
        }

        public static Guid ReadGuid(this BinaryReader reader)
        {
            return new Guid(reader.ReadBytes(0x10));
        }

        public static string ReadString(this BinaryReader reader, int size)
        {
            byte[] bytes = reader.ReadBytes(size);
            return Encoding.UTF8.GetString(bytes).Replace('\0', ' ').Trim();
        }

        public static long Seek(this BinaryReader reader, long position)
        {
            return reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
    }
}

