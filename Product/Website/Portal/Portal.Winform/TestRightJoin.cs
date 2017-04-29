using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.DataEntity;
using Joy.Storage;

namespace Portal.Winform
{
    public partial class TestRightJoin : UserControl
    {
        public TestRightJoin()
        {
            InitializeComponent();
            udg.OnGridLoad += UdgOnGridLoad;
            Load += TestRightJoin_Load;
        }

        void TestRightJoin_Load(object sender, EventArgs e)
        {
        }
        private EntityGroup users;
        private EntityGroup roles;
        void UdgOnGridLoad()
        {
            users = StorageManager.Instance.CreateOrGet("Users");
            roles = StorageManager.Instance.CreateOrGet("UserRoles");
            UserRole admin = roles.AddEntity<UserRole>(new UserRole { RoleName = "Administrator" });
            UserRole power = roles.AddEntity<UserRole>(new UserRole { RoleName = "PowerUser" });
            for (int i = 0; i < 10; i++)
            {
                users.AddEntity<User>(new User
                {
                    Username = "user_" + i,
                    Password = "pwd_" + i,
                    RoleId = i % 2 == 0 ? admin.ObjectKey : power.ObjectKey
                });
            }

            udg.Gv.DataSource = roles.GetEntities<UserRole>(); //users.GetEntities<User>();
            udg.Gv.CellClick += Gv_CellClick;
        }

        void Gv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            udg.GvDetails.DataSource = roles[e.RowIndex].Join(users, "RoleId").To<User>();
        }
    }
}
