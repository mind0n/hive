using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Joy.Storage;
using DAL.DataEntity;

namespace Portal.Testing
{
    public partial class TestStorage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitializeStorage();
            //FileAdapter fa = new FileAdapter();
            //StorageManager.Instance.Initialize(fa);
            //EntityGroup users = StorageManager.Instance.CreateOrGet("Users");
            //EntityGroup roles = StorageManager.Instance.CreateOrGet("UserRoles");
            //UserRole admin = roles.AddEntity<UserRole>(new UserRole { RoleName = "Administrator" });
            //UserRole power = roles.AddEntity<UserRole>(new UserRole { RoleName = "PowerUser" });
            //for (int i = 0; i < 10; i++)
            //{
            //    users.AddEntity<User>(new User
            //    {
            //        Username = "user_" + i,
            //        RoleId = i%2 == 0 ? admin.ObjectKey : power.ObjectKey
            //    });
            //}
            //StorageManager.Instance.Clear();
            //StorageManager.Instance.Save();
        }

        //private static void InitializeStorage()
        //{
        //    FileAdapter fa = new FileAdapter();
        //    StorageManager.Instance.Initialize(fa);

        //    EntityGroup users = StorageManager.Instance.CreateOrGet("Users");
        //    EntityGroup roles = StorageManager.Instance.CreateOrGet("UserRoles");
        //    UserRole admin = roles.AddEntity<UserRole>(new UserRole {RoleName = "Administrator"});
        //    UserRole power = roles.AddEntity<UserRole>(new UserRole {RoleName = "PowerUser"});
        //    users.AddEntity<User>(new User {Username = "admin", RoleId = admin.ObjectKey});
        //    users.AddEntity<User>(new User {Username = "jerry", RoleId = power.ObjectKey});
        //    StorageManager.Instance.Clear();
        //    StorageManager.Instance.Save();
        //}
    }
}