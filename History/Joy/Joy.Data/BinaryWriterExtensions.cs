using System;
using System.IO;
using System.Threading;

namespace Joy.Data
{
    internal static class BinaryWriterExtensions
    {
        private const int DELAY_TRY_LOCK_FILE = 50;
        private const int MAX_TRY_LOCK_FILE = 50;

        public static void Lock(this BinaryWriter writer, long position, long length)
        {
            FileStream fileStream = writer.BaseStream as FileStream;
            TryLockFile(fileStream, position, length, 0);
        }

        public static long Seek(this BinaryWriter writer, long position)
        {
            return writer.BaseStream.Seek(position, SeekOrigin.Begin);
        }

        private static void TryLockFile(FileStream fileStream, long position, long length, int tryCount)
        {
            try
            {
                fileStream.Lock(position, length);
            }
            catch (IOException ex)
            {
                if (!ex.IsLockException())
                {
                    throw ex;
                }
                if (tryCount >= 50)
                {
                    throw new FileDBException("Database file is in lock for a long time");
                }
                Thread.Sleep((int) (tryCount * 50));
                TryLockFile(fileStream, position, length, ++tryCount);
            }
        }

        public static void Unlock(this BinaryWriter writer, long position, long length)
        {
            (writer.BaseStream as FileStream).Unlock(position, length);
        }

        public static void Write(this BinaryWriter writer, DateTime dateTime)
        {
            writer.Write(dateTime.Ticks);
        }

        public static void Write(this BinaryWriter writer, Guid guid)
        {
            writer.Write(guid.ToByteArray());
        }
    }
}

