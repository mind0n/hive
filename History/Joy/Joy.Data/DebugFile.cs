using System;
using System.Text;

namespace Joy.Data
{
    public class DebugFile
    {
        private Engine _engine;

        internal DebugFile(Engine engine)
        {
            this._engine = engine;
        }

        public string DisplayPages()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Constants:");
            sb.AppendLine("=============");
            sb.AppendLine("BasePage.PAGE_SIZE       : " + 0x1000L);
            sb.AppendLine("IndexPage.HEADER_SIZE    : " + 0x2eL);
            sb.AppendLine("IndexPage.NODES_PER_PAGE : " + 50);
            sb.AppendLine("DataPage.HEADER_SIZE     : " + 8L);
            sb.AppendLine("DataPage.DATA_PER_PAGE   : " + 0xff8L);
            sb.AppendLine();
            sb.AppendLine("Header:");
            sb.AppendLine("=============");
            sb.AppendLine("IndexRootPageID    : " + this._engine.Header.IndexRootPageID.Fmt());
            sb.AppendLine("FreeIndexPageID    : " + this._engine.Header.FreeIndexPageID.Fmt());
            sb.AppendLine("FreeDataPageID     : " + this._engine.Header.FreeDataPageID.Fmt());
            sb.AppendLine("LastFreeDataPageID : " + this._engine.Header.LastFreeDataPageID.Fmt());
            sb.AppendLine("LastPageID         : " + this._engine.Header.LastPageID.Fmt());
            sb.AppendLine();
            sb.AppendLine("Pages:");
            sb.AppendLine("=============");
            for (uint i = 0; i <= this._engine.Header.LastPageID; i++)
            {
                Unit page = UnitFactory.GetBasePage(i, this._engine.Reader);
                sb.AppendFormat("[{0}] >> [{1}] ({2}) ", page.UnitID.Fmt(), page.NextUnitID.Fmt(), (page.Type == UnitType.Data) ? "D" : "I");
                if (page.Type == UnitType.Data)
                {
                    DataUnit dataPage = (DataUnit) page;
                    if (dataPage.IsEmpty)
                    {
                        sb.Append("Empty");
                    }
                    else
                    {
                        sb.AppendFormat("Bytes: {0}", dataPage.DataBlockLength);
                    }
                }
                else
                {
                    IndexUnit indexPage = (IndexUnit) page;
                    sb.AppendFormat("Keys: {0}", indexPage.NodeIndex + 1);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

