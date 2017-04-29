using System;
using System.Collections.Generic;
using System.IO;

namespace Joy.Data
{
    internal class Engine : IDisposable
    {
        public Engine(FileStream stream)
        {
            this.Reader = new BinaryReader(stream);
            if (stream.CanWrite)
            {
                this.Writer = new BinaryWriter(stream);
                this.Writer.Lock(0x62L, 1L);
            }
            this.Header = new Joy.Data.Header();
            HeaderFactory.ReadFromFile(this.Header, this.Reader);
            this.CacheIndexPage = new Joy.Data.CacheIndexPage(this.Reader, this.Writer, this.Header.IndexRootPageID);
        }

        public bool Delete(Guid id)
        {
            IndexNode indexNode = this.Search(id);
            if (indexNode == null)
            {
                return false;
            }
            indexNode.IsDeleted = true;
            this.CacheIndexPage.AddPage(indexNode.IndexPage, true);
            DataFactory.MarkAsEmpty(indexNode.DataPageID, this);
            this.Header.IsDirty = true;
            return true;
        }

        public void Dispose()
        {
            if (this.Writer != null)
            {
                this.Writer.Unlock(0x62L, 1L);
                this.Writer.Close();
            }
            this.Reader.Close();
        }

        public IndexUnit GetFreeIndexPage()
        {
            IndexUnit freeIndexPage = this.CacheIndexPage.GetPage(this.Header.FreeIndexPageID);
            if (freeIndexPage.NodeIndex >= 0x31)
            {
                Joy.Data.Header header = this.Header;
                header.LastPageID++;
                this.Header.IsDirty = true;
                IndexUnit newIndexPage = new IndexUnit(this.Header.LastPageID);
                freeIndexPage.NextUnitID = newIndexPage.UnitID;
                this.Header.FreeIndexPageID = this.Header.LastPageID;
                this.CacheIndexPage.AddPage(freeIndexPage, true);
                return newIndexPage;
            }
            freeIndexPage.NodeIndex = (byte) (freeIndexPage.NodeIndex + 1);
            return freeIndexPage;
        }

        public DataUnit GetPageData(uint pageID)
        {
            if (pageID == this.Header.LastPageID)
            {
                return new DataUnit(pageID);
            }
            return UnitFactory.GetDataPage(pageID, this.Reader, false);
        }

        public EntryInfo[] ListAllFiles()
        {
            IndexUnit pageIndex = this.CacheIndexPage.GetPage(this.Header.IndexRootPageID);
            bool cont = true;
            List<EntryInfo> list = new List<EntryInfo>();
            while (cont)
            {
                for (int i = 0; i <= pageIndex.NodeIndex; i++)
                {
                    IndexNode node = pageIndex.Nodes[i];
                    if (!node.IsDeleted)
                    {
                        list.Add(new EntryInfo(node));
                    }
                }
                if (pageIndex.NextUnitID != uint.MaxValue)
                {
                    pageIndex = this.CacheIndexPage.GetPage(pageIndex.NextUnitID);
                }
                else
                {
                    cont = false;
                }
            }
            return list.ToArray();
        }

        public void PersistPages()
        {
            if (this.Header.IsDirty)
            {
                HeaderFactory.WriteToFile(this.Header, this.Writer);
                this.Header.IsDirty = false;
            }
            this.CacheIndexPage.PersistPages();
        }

        public EntryInfo Read(Guid id, Stream stream)
        {
            IndexNode indexNode = this.Search(id);
            if (indexNode == null)
            {
                return null;
            }
            EntryInfo entry = new EntryInfo(indexNode);
            DataFactory.ReadFile(indexNode, stream, this);
            return entry;
        }

        public IndexNode Search(Guid id)
        {
            IndexNode rootIndexNode = IndexFactory.GetRootIndexNode(this);
            IndexNode indexNode = IndexFactory.BinarySearch(id, rootIndexNode, this);
            if ((indexNode != null) && !indexNode.IsDeleted)
            {
                return indexNode;
            }
            return null;
        }

        public void Write(EntryInfo entry, Stream stream)
        {
            IndexNode rootIndexNode = IndexFactory.GetRootIndexNode(this);
            IndexNode indexNode = IndexFactory.BinaryInsert(entry, rootIndexNode, this);
            DataFactory.InsertFile(indexNode, stream, this);
            entry.FileLength = indexNode.FileLength;
            indexNode.IsDeleted = false;
            this.Header.IsDirty = true;
        }

        public Joy.Data.CacheIndexPage CacheIndexPage { get; private set; }

        public Joy.Data.Header Header { get; private set; }

        public BinaryReader Reader { get; private set; }

        public BinaryWriter Writer { get; private set; }
    }
}

