using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Storage
{
    public class FieldAttribute : Attribute
    {
        public string Target;
        public bool UseHash;
    }
}
