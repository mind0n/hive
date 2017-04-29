using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DAL.DataEntity;
using Joy.Storage;

namespace Portal.Winform.Components
{
    [ComVisible(true)]
    public class StorageManageService : IScriptProcessor
    {
        public string Name = "Storage Management Service";

        public void AddGroup(string name)
        {
            StorageManager.Instance.CreateOrGet(name);
        }

        public string GetEntry()
        {
            EntityGroups cache = StorageManager.Instance.GetEntry();
            string rlt = cache.ToJson();
            return rlt;
        }

        public void DropGroup(string name)
        {
            StorageManager.Instance.Drop(name);
        }

        public void OnClose()
        {
            StorageManager.Instance.Save();
        }

        public void OnLoad()
        {
            StorageManager.Instance.Initialize(new FileAdapter());
	        LoadResult rlt = StorageManager.Instance.Load();
	        if (!rlt.IsSuccessful)
	        {
		        EntityGroup users = StorageManager.Instance.CreateOrGet("Users");
		        EntityGroup roles = StorageManager.Instance.CreateOrGet("Roles");
		        EntityGroup articles = StorageManager.Instance.CreateOrGet("Articles");
		        EntityGroup tags = StorageManager.Instance.CreateOrGet("Tags");

		        UserRole admin = roles.AddEntity<UserRole>(new UserRole {RoleName = "Administrator"});
		        UserRole power = roles.AddEntity<UserRole>(new UserRole {RoleName = "PowerUser"});
		        for (int i = 0; i < 10; i++)
		        {
			        User u = users.AddEntity<User>(new User {Username = "U" + i, Password = "empty"});
			        if (i%2 == 0)
			        {
				        u.RoleId = admin.ObjectKey;
			        }
			        else
			        {
				        u.RoleId = power.ObjectKey;
			        }
		        }

		        var ts = new Tag[3];
		        ts[0] = tags.AddEntity<Tag>(new Tag {Name = "News"});
		        ts[1] = tags.AddEntity<Tag>(new Tag {Name = "SourceCode"});
		        ts[2] = tags.AddEntity<Tag>(new Tag {Name = "Advertisement"});

		        for (int i = 0; i < 10; i++)
		        {
			        Article a = articles.AddEntity<Article>(new Article {Caption = "title_" + i, Content = "content_" + i});
			        for (int j = 0; j <= i%3; j++)
			        {
				        a.Tags.AddJoint(ts[j]);
			        }
		        }
	        }
        }
    }
}
