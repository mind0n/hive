using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joy.Storage;

namespace DAL.DataEntity
{
    public class Article : Entity
    {
        public string Caption { get; set; }
        public string Content { get; set; }
        public Junction Tags { get; set; }

        public Article()
        {
            Tags = new Junction();
        }
    }
}
