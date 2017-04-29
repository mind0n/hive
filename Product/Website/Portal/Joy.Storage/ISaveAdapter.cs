using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Storage
{
    public interface ISaveAdapter
    {
	    LoadResult Load(string input = null);
        void Process(EntityGroups pool);
        void Process(EntityGroup group);
        void Process(Entity entity);
        void DropEntityGroup(EntityGroup group);
        void Clear();
    }
}
