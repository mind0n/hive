using System;

namespace Joy.Data
{
    internal class IndexFactory
    {
        public static IndexNode BinaryInsert(EntryInfo target, IndexNode baseNode, Engine engine)
        {
            int dif = baseNode.ID.CompareTo(target.ID);
            if (dif == 1)
            {
                if (baseNode.Right.IsEmpty)
                {
                    return BinaryInsertNode(baseNode.Right, baseNode, target, engine);
                }
                return BinaryInsert(target, GetChildIndexNode(baseNode.Right, engine), engine);
            }
            if (dif != -1)
            {
                throw new FileDBException("Same GUID?!?");
            }
            if (baseNode.Left.IsEmpty)
            {
                return BinaryInsertNode(baseNode.Left, baseNode, target, engine);
            }
            return BinaryInsert(target, GetChildIndexNode(baseNode.Left, engine), engine);
        }

        private static IndexNode BinaryInsertNode(IndexLink baseLink, IndexNode baseNode, EntryInfo entry, Engine engine)
        {
            IndexUnit pageIndex = engine.GetFreeIndexPage();
            IndexNode newNode = pageIndex.Nodes[pageIndex.NodeIndex];
            baseLink.PageID = pageIndex.UnitID;
            baseLink.Index = pageIndex.NodeIndex;
            newNode.UpdateFromEntry(entry);
            newNode.DataPageID = DataFactory.GetStartDataPageID(engine);
            if (pageIndex.UnitID != baseNode.IndexPage.UnitID)
            {
                engine.CacheIndexPage.AddPage(baseNode.IndexPage, true);
            }
            engine.CacheIndexPage.AddPage(pageIndex, true);
            return newNode;
        }

        public static IndexNode BinarySearch(Guid target, IndexNode baseNode, Engine engine)
        {
            int dif = baseNode.ID.CompareTo(target);
            if (dif == 1)
            {
                if (baseNode.Right.IsEmpty)
                {
                    return null;
                }
                return BinarySearch(target, GetChildIndexNode(baseNode.Right, engine), engine);
            }
            if (dif != -1)
            {
                return baseNode;
            }
            if (baseNode.Left.IsEmpty)
            {
                return null;
            }
            return BinarySearch(target, GetChildIndexNode(baseNode.Left, engine), engine);
        }

        private static IndexNode GetChildIndexNode(IndexLink link, Engine engine)
        {
            return engine.CacheIndexPage.GetPage(link.PageID).Nodes[link.Index];
        }

        public static IndexNode GetRootIndexNode(Engine engine)
        {
            return engine.CacheIndexPage.GetPage(engine.Header.IndexRootPageID).Nodes[0];
        }
    }
}

