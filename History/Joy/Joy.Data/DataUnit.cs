using System;

namespace Joy.Data
{
    internal class DataUnit : Unit
    {
        public const long DataPerUnit = 0xff8L;
        public const long HeaderSize = 8L;

        public DataUnit(uint unitID)
        {
            base.UnitID = unitID;
            this.IsEmpty = true;
            this.DataBlockLength = 0;
            base.NextUnitID = uint.MaxValue;
            this.DataBlock = new byte[0xff8L];
        }

        public byte[] DataBlock { get; set; }

        public short DataBlockLength { get; set; }

        public bool IsEmpty { get; set; }

        public override UnitType Type
        {
            get
            {
                return UnitType.Data;
            }
        }
    }
}

