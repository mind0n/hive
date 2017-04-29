using System;
using System.IO;
using System.IO.Compression;
using System.Text;


namespace Fs.IO.Compression
{
	public class GZip
	{
		public static Exception SaveToFile(string filename, byte[] content)
		{
			Exception rlt = null;
			FileStream fs = null;
			try
			{
				FileInfo fi = new FileInfo(filename);
				
				fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
				fs.Write(content, 0, content.Length);
			}
			catch (Exception err)
			{
				rlt = err;
				Exceptions.LogOnly(err);
			}
			finally
			{
				fs.Close();
			}
			return rlt;
		}
		public static byte[] ReadFromFile(string filename)
		{
			try
			{
				byte[] rlt;
				FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				rlt = new byte[fs.Length];
				fs.Read(rlt, 0, (int)fs.Length);
				fs.Close();
				return rlt;
			}
			catch (Exception err)
			{
				throw err;
			}
		}
		public static byte[] Zip(string originData)
		{
			MemoryStream streamCompressed = new MemoryStream();
			GZipStream gzipStream = new GZipStream(streamCompressed, CompressionMode.Compress);
			StreamWriter sw = new StreamWriter(gzipStream);
			sw.Write(originData);
			sw.Close();
			return streamCompressed.ToArray();
		}
		public static string UnZip(byte[] compressed)
		{
			string decompressed = string.Empty;
			MemoryStream streamCompressed = new MemoryStream(compressed);
			GZipStream gzipStream = new GZipStream(streamCompressed, CompressionMode.Decompress);
			StreamReader reader = new StreamReader(gzipStream);
			decompressed = reader.ReadToEnd();
			reader.Close();
			return decompressed;
		}
	}
}