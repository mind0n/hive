using System;
using System.IO;
using System.Text;

namespace Joy.Data
{
    public class FileDB : IDisposable
    {
        private DebugFile _debug;
        private Engine _engine;
        private FileStream _fileStream;

        public FileDB(string fileName, FileAccess fileAccess)
        {
            this.Connect(fileName, fileAccess);
        }

        private void Connect(string fileName, FileAccess fileAccess)
        {
            if (!File.Exists(fileName))
            {
                CreateEmptyFile(fileName);
            }
            FileAccess fa = ((fileAccess == FileAccess.Write) || (fileAccess == FileAccess.ReadWrite)) ? FileAccess.ReadWrite : FileAccess.Read;
            this._fileStream = new FileStream(fileName, FileMode.Open, fa, FileShare.ReadWrite, 0x1000, FileOptions.None);
            this._engine = new Engine(this._fileStream);
        }

        public static void CreateEmptyFile(string dbFileName)
        {
            CreateEmptyFile(dbFileName, true);
        }

        public static void CreateEmptyFile(string dbFileName, bool ignoreIfExists)
        {
            if (File.Exists(dbFileName))
            {
                if (!ignoreIfExists)
                {
                    throw new FileDBException("Database file {0} already exists", new object[] { dbFileName });
                }
            }
            else
            {
                using (FileStream fileStream = new FileStream(dbFileName, FileMode.CreateNew, FileAccess.Write))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        FileFactory.CreateEmptyFile(writer);
                    }
                }
            }
        }

        public bool Delete(Guid id)
        {
            return this._engine.Delete(id);
        }

        public static bool Delete(string dbFileName, Guid id)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.ReadWrite))
            {
                return db.Delete(id);
            }
        }

        public void Dispose()
        {
            if (this._engine != null)
            {
                this._engine.PersistPages();
                if (this._fileStream.CanWrite)
                {
                    this._fileStream.Flush();
                }
                this._engine.Dispose();
                this._fileStream.Dispose();
            }
        }

        public void Export(string directory)
        {
            this.Export(directory, "{filename}.{id}.{extension}");
        }

        public void Export(string directory, string filePattern)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            foreach (EntryInfo file in this.ListFiles())
            {
                string fileName = filePattern.Replace("{id}", file.ID.ToString()).Replace("{filename}", Path.GetFileNameWithoutExtension(file.FileName)).Replace("{extension}", Path.GetExtension(file.FileName).Replace(".", ""));
                this.Read(file.ID, Path.Combine(directory, fileName));
            }
        }

        public static void Export(string dbFileName, string directory, string filePattern)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.Read))
            {
                db.Export(directory, filePattern);
            }
        }

        public EntryInfo[] ListFiles()
        {
            return this._engine.ListAllFiles();
        }

        public static EntryInfo[] ListFiles(string dbFileName)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.Read))
            {
                return db.ListFiles();
            }
        }

        public EntryInfo Read(Guid id, Stream output)
        {
            return this._engine.Read(id, output);
        }

        public EntryInfo Read(Guid id, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                return this.Read(id, stream);
            }
        }

		public string Load(EntryInfo entry)
		{
			MemoryStream stream = new MemoryStream();
			EntryInfo info = Read(entry.ID, stream);
			string rlt = Encoding.Unicode.GetString(stream.ToArray());
			return rlt;
		}

        public static EntryInfo Read(string dbFileName, Guid id, Stream output)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.Read))
            {
                return db.Read(id, output);
            }
        }

        public static EntryInfo Read(string dbFileName, Guid id, string fileName)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.Read))
            {
                return db.Read(id, fileName);
            }
        }

        public EntryInfo Search(Guid id)
        {
            IndexNode indexNode = this._engine.Search(id);
            if (indexNode == null)
            {
                return null;
            }
            return new EntryInfo(indexNode);
        }

        public void Shrink()
        {
            string dbFileName = this._fileStream.Name;
            FileAccess fileAccess = this._fileStream.CanWrite ? FileAccess.ReadWrite : FileAccess.Read;
            string tempFile = string.Concat(new object[] { Path.GetDirectoryName(dbFileName), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(dbFileName), ".temp", Path.GetExtension(dbFileName) });
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            EntryInfo[] entries = this.ListFiles();
            CreateEmptyFile(tempFile, false);
            using (FileDB tempDb = new FileDB(tempFile, FileAccess.ReadWrite))
            {
                foreach (EntryInfo entry in entries)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        this.Read(entry.ID, stream);
                        stream.Seek(0L, SeekOrigin.Begin);
                        tempDb.Store(entry, stream);
                    }
                }
            }
            this.Dispose();
            File.Delete(dbFileName);
            File.Move(tempFile, dbFileName);
            this.Connect(dbFileName, fileAccess);
        }

        public static void Shrink(string dbFileName)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.Read))
            {
                db.Shrink();
            }
        }
		public EntryInfo Save(EntryInfo i, string content)
		{
			EntryInfo rlt = null;
			EntryInfo info = Search(i.ID);
			if (info != null)
			{
				rlt = Save(i.FileName, content);
				Delete(i.ID);
			}
			return rlt;
		}
		public EntryInfo Save(string filename, string content)
		{
			MemoryStream s = new MemoryStream(Encoding.Unicode.GetBytes(content));
			return Store(filename, s);
		}
        public EntryInfo Store(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return this.Store(fileName, stream);
            }
        }

        internal void Store(EntryInfo entry, Stream input)
        {
            this._engine.Write(entry, input);
        }

        public EntryInfo Store(string fileName, Stream input)
        {
            EntryInfo entry = new EntryInfo(fileName);
            this._engine.Write(entry, input);
            return entry;
        }

        public static EntryInfo Store(string dbFileName, string fileName)
        {
            using (FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Store(dbFileName, fileName, input);
            }
        }

        public static EntryInfo Store(string dbFileName, string fileName, Stream input)
        {
            using (FileDB db = new FileDB(dbFileName, FileAccess.ReadWrite))
            {
                return db.Store(fileName, input);
            }
        }

        public DebugFile Debug
        {
            get
            {
                if (this._debug == null)
                {
                    this._debug = new DebugFile(this._engine);
                }
                return this._debug;
            }
        }
    }
}

