using System;
using System.IO;

namespace Joy.Data
{
    public class EntryInfo
    {
        private uint _fileLength;
        private string _fileName;
        private Guid _id;
        private string _mimeType;

        internal EntryInfo(IndexNode node)
        {
            this._id = node.ID;
            this._fileName = node.FileName + "." + node.FileExtension;
            this._mimeType = MimeTypeConverter.Convert(node.FileExtension);
            this._fileLength = node.FileLength;
        }

        internal EntryInfo(string fileName)
        {
            this._id = Guid.NewGuid();
            this._fileName = Path.GetFileName(fileName);
            this._mimeType = MimeTypeConverter.Convert(Path.GetExtension(this._fileName));
            this._fileLength = 0;
        }

        public uint FileLength
        {
            get
            {
                return this._fileLength;
            }
            internal set
            {
                this._fileLength = value;
            }
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }

        public Guid ID
        {
            get
            {
                return this._id;
            }
        }

        public string MimeType
        {
            get
            {
                return this._mimeType;
            }
        }
    }
}

