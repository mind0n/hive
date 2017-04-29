using System;

namespace Joy.Data
{
    internal abstract class Unit
    {
        public const long PAGE_SIZE = 0x1000L;

        protected Unit()
        {
        }

        public uint NextUnitID { get; set; }

        public uint UnitID { get; set; }

        public abstract UnitType Type { get; }
    }
}

