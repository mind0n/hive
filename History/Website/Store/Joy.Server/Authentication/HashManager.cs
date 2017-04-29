using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace Joy.Server.Authentication
{
	public class HashManager
	{
		public virtual string GetLastTicket(string uname) { return null; }
		public virtual void UpdateTicket(string uname, string hash) { }
		public virtual void UpdateTicket(string uname) { }
		public virtual void ClearLastHash() { }
	}
	public class FileHashManager : HashManager
	{
		protected string BaseDir;
		protected string PostFix = ".hash";
		public FileHashManager()
		{
			Init(null);
		}
		public FileHashManager(string baseDir)
		{
			Init(baseDir);
		}
		protected void Init(string baseDir)
		{
			if (string.IsNullOrEmpty(baseDir) || !Directory.Exists(baseDir))
			{
				BaseDir = AppDomain.CurrentDomain.BaseDirectory + "UserHashes\\";
				if (!Directory.Exists(BaseDir))
				{
					Directory.CreateDirectory(BaseDir);
				}
			}
			else
			{
				BaseDir = baseDir;
			}
		}
		public override string GetLastTicket(string uname)
		{
			string fname = BaseDir + uname + PostFix;
			if (File.Exists(fname))
			{
				return File.ReadAllText(fname);
			}
			return null;
		}
		public override void UpdateTicket(string uname, string hash)
		{
			string fname = BaseDir + uname + PostFix;
			File.WriteAllText(fname, hash);
		}
		public override void ClearLastHash()
		{
			string fname = BaseDir + AuthManager.ActiveMember + PostFix;
			File.Delete(fname);
		}
	}
}
