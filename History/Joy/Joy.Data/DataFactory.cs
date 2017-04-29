using System;
using System.IO;

namespace Joy.Data
{
    internal class DataFactory
    {
        public static DataUnit GetNewDataPage(DataUnit basePage, Engine engine)
        {
            if (basePage.NextUnitID != uint.MaxValue)
            {
                UnitFactory.WriteToFile(basePage, engine.Writer);
                DataUnit dataPage = UnitFactory.GetDataPage(basePage.NextUnitID, engine.Reader, false);
                engine.Header.FreeDataPageID = dataPage.NextUnitID;
                if (engine.Header.FreeDataPageID == uint.MaxValue)
                {
                    engine.Header.LastFreeDataPageID = uint.MaxValue;
                }
                return dataPage;
            }
            Header header = engine.Header;
            uint pageID = ++header.LastPageID;
            DataUnit newPage = new DataUnit(pageID);
            basePage.NextUnitID = newPage.UnitID;
            UnitFactory.WriteToFile(basePage, engine.Writer);
            return newPage;
        }

        public static uint GetStartDataPageID(Engine engine)
        {
            if (engine.Header.FreeDataPageID != uint.MaxValue)
            {
                DataUnit startPage = UnitFactory.GetDataPage(engine.Header.FreeDataPageID, engine.Reader, true);
                engine.Header.FreeDataPageID = startPage.NextUnitID;
                if (engine.Header.FreeDataPageID == uint.MaxValue)
                {
                    engine.Header.LastFreeDataPageID = uint.MaxValue;
                }
                return startPage.UnitID;
            }
            Header header = engine.Header;
            header.LastPageID++;
            return engine.Header.LastPageID;
        }

        public static void InsertFile(IndexNode node, Stream stream, Engine engine)
        {
            DataUnit dataPage = null;
            byte[] buffer = new byte[0xff8L];
            uint totalBytes = 0;
            int read = 0;
            int dataPerPage = 0xff8;
            while ((read = stream.Read(buffer, 0, dataPerPage)) > 0)
            {
                totalBytes += (uint) read;
                if (dataPage == null)
                {
                    dataPage = engine.GetPageData(node.DataPageID);
                }
                else
                {
                    dataPage = GetNewDataPage(dataPage, engine);
                }
                if (!dataPage.IsEmpty)
                {
                    throw new FileDBException("Page {0} is not empty", new object[] { dataPage.UnitID });
                }
                Array.Copy(buffer, dataPage.DataBlock, read);
                dataPage.IsEmpty = false;
                dataPage.DataBlockLength = (short) read;
            }
            if (dataPage.NextUnitID != uint.MaxValue)
            {
                engine.Header.FreeDataPageID = dataPage.NextUnitID;
                dataPage.NextUnitID = uint.MaxValue;
            }
            UnitFactory.WriteToFile(dataPage, engine.Writer);
            node.FileLength = totalBytes;
        }

        public static void MarkAsEmpty(uint firstPageID, Engine engine)
        {
            DataUnit dataPage = UnitFactory.GetDataPage(firstPageID, engine.Reader, true);
            uint lastPageID = uint.MaxValue;
            bool cont = true;
            while (cont)
            {
                dataPage.IsEmpty = true;
                UnitFactory.WriteToFile(dataPage, engine.Writer);
                if (dataPage.NextUnitID != uint.MaxValue)
                {
                    lastPageID = dataPage.NextUnitID;
                    dataPage = UnitFactory.GetDataPage(lastPageID, engine.Reader, true);
                }
                else
                {
                    cont = false;
                }
            }
            if (engine.Header.FreeDataPageID == uint.MaxValue)
            {
                engine.Header.FreeDataPageID = firstPageID;
                engine.Header.LastFreeDataPageID = (lastPageID == uint.MaxValue) ? firstPageID : lastPageID;
            }
            else
            {
                DataUnit lastPage = UnitFactory.GetDataPage(engine.Header.LastFreeDataPageID, engine.Reader, true);
                if ((lastPage.NextUnitID != uint.MaxValue) || !lastPage.IsEmpty)
                {
                    throw new FileDBException("The page is not empty");
                }
                lastPage.NextUnitID = firstPageID;
                UnitFactory.WriteToFile(lastPage, engine.Writer);
                engine.Header.LastFreeDataPageID = (lastPageID == uint.MaxValue) ? firstPageID : lastPageID;
            }
        }

        public static void ReadFile(IndexNode node, Stream stream, Engine engine)
        {
            DataUnit dataPage = UnitFactory.GetDataPage(node.DataPageID, engine.Reader, false);
            while (dataPage != null)
            {
                stream.Write(dataPage.DataBlock, 0, dataPage.DataBlockLength);
                if (dataPage.NextUnitID == uint.MaxValue)
                {
                    dataPage = null;
                }
                else
                {
                    dataPage = UnitFactory.GetDataPage(dataPage.NextUnitID, engine.Reader, false);
                }
            }
        }
    }
}

