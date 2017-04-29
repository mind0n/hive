using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Storage
{
    public class HashGroup : Dictionary<string, Hashtable>
    {
        public void AddHashItem(string field, object key, object value)
        {
            if (string.IsNullOrEmpty(field) || key == null)
            {
                return;
            }
            if (!ContainsKey(field))
            {
                this[field] = new Hashtable();
            }
            this[field][key] = value;
        }
    }
}
