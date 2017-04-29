using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joy.Storage;
using System.Runtime.Serialization;

namespace DAL.DataEntity
{
    
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [Field(Target = "Role", UseHash = true)]
        public string RoleId { get; set; }

        [IgnoreDataMember]
        public UserRole Role;
    }
}
