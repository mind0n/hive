using System;

namespace Joy.Data
{

    internal class IndexUnit : Unit
    {
        public const long HeaderSize = 0x2eL;
        public const int NodesPerUnit = 50;

        public IndexUnit(uint pageID)
        {
            base.UnitID = pageID;
            base.NextUnitID = uint.MaxValue;
            this.NodeIndex = 0;
            this.Nodes = new IndexNode[50];
            this.IsDirty = false;
            for (int i = 0; i < 50; i++)
            {
                this.Nodes[i] = new IndexNode(this);
            }
        }

        public bool IsDirty { get; set; }

        public byte NodeIndex { get; set; }

        public IndexNode[] Nodes { get; set; }

        public override UnitType Type
        {
            get
            {
                return UnitType.Index;
            }
        }
    }
}

