using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Joy.Storage
{
	[Serializable]
    public class Junction
    {
		public List<Joint> Items = new List<Joint>();
        public void AddJoint(string id)
        {
            Items.Add(new Joint {Id = id});
        }

        public void AddJoint(Entity e)
        {
            if (e != null)
            {
                Items.Add(new Joint {Id = e.ObjectKey, Target = e});
            }
        }

        internal void Link()
        {
            foreach (Joint i in Items)
            {
                Entity e = ObjectPoolManager.Get(i.Id);
                if (e != null)
                {
                    i.Target = e;
                }
            }
        }
    }

	[Serializable]
    public class Joint
    {
        public string Id { get; set; }

        [ScriptIgnore]
        public Entity Target;
    }
}
