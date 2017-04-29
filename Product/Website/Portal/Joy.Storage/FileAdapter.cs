using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Joy.Core;

namespace Joy.Storage
{
    public class FileAdapter : ISaveAdapter
    {
        public static readonly string DefaultRootDir = AppDomain.CurrentDomain.BaseDirectory + "$Storage\\";
	    private static string _rootDir;
	    public static string RootDir
	    {
		    get { return string.IsNullOrEmpty(_rootDir) ? DefaultRootDir : _rootDir; }
			set { _rootDir = value; }
	    }
		public LoadResult Load(string rootDir = null)
		{
			LoadResult rlt = new LoadResult();
			if (rootDir == null)
			{
				rootDir = RootDir;
			}
			else
			{
				RootDir = rootDir;
			}
			if (!Directory.Exists(RootDir))
			{
				rlt.LastError = new DirectoryNotFoundException(RootDir);
			}
			else
			{
				string[] dirs = Directory.GetDirectories(rootDir);
				foreach (string i in dirs)
				{
					//DirectoryInfo di = new DirectoryInfo(i);
					string name = i.PathLastName();
					EntityGroup g = StorageManager.Instance.Create(name);
					if (g != null)
					{
						foreach (string j in Directory.EnumerateFiles(i))
						{
							Entity e = LoadEntity(j, false);
							if (e != null)
							{
								g.AddEntity(e);
							}
						}
					}
				}
				rlt.Result = StorageManager.Instance;
			}
			return rlt;
		}
		public Entity LoadEntity(string path, bool checkExist = true)
		{
			if (checkExist && !File.Exists(path))
			{
				return null;
			}
			string tmp = path.PathLastName(false);
			string[] lst = tmp.Split('_');
			if (lst.Length < 2)
			{
				return null;
			}
			Type type = Type.GetType(lst[0]);
			string key = lst[1];
			if (type == null || string.IsNullOrEmpty(key))
			{
				return null;
			}
			string content = File.ReadAllText(path);
			if (string.IsNullOrEmpty(content))
			{
				return null;
			}
			return content.FromJson(type) as Entity;
		}
        public void Process(EntityGroups cache)
        {
            foreach (KeyValuePair<string, EntityGroup> i in cache)
            {
                SaveEntities(i.Value);
            }
        }

        public void DropEntityGroup(EntityGroup group)
        {
            if (group == null)
            {
                return;
            }
            string dir = EntityGroupName(group);
            dir.RmDir();
            ObjectPoolManager.Remove(group);
        }
        
        protected virtual void SaveEntity(Entity entity)
        {
            string fullpath = StoragePath(entity);
            if (entity.IsChanged)
            {
                entity.Save2File(fullpath);
            }
        }

        protected virtual void SaveEntities(EntityGroup entities)
        {
            string name = EntityGroupName(entities);
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(RootDir + name);
            }
            Entities list = entities.GetEntities();
            foreach (Entity i in list)
            {
                SaveEntity(i);
            }
        }
        private string StoragePath(Entity entity)
        {
            string par = string.Empty;
            if (entity.Parent != null)
            {
                par = EntityGroupName(entity.Parent);
            }
            return string.Join("\\", RootDir, par, EntityName(entity) + ".json");
        }
		
        private string EntityGroupName(EntityGroup eg)
        {
            return string.Concat(eg.Name, "_", eg.GetType().Name, "_", eg.ObjectKey);
        }
        private string EntityName(Entity entity)
        {
            return string.Concat(entity.GetType().AssemblyQualifiedName, "_", entity.ObjectKey);
        }


        public void Clear()
        {
            if (Directory.Exists(RootDir))
            {
                string[] files = Directory.GetFiles(RootDir, "*.*", SearchOption.AllDirectories);
                foreach (string i in files)
                {
                    File.Delete(i);
                }
                Directory.Delete(RootDir, true);
            }
        }


        public void Process(EntityGroup group)
        {
            SaveEntities(group);
        }

        public void Process(Entity entity)
        {
            SaveEntity(entity);
        }
    }
}
