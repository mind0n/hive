using System;
using System.Web.Caching;

namespace Joy.Server
{
	public class Credential
	{
		private Cache cache;
		private const string STR_Dash = "-";
		private const char CH_Underline = '_';
		private const char CH_Dash = '-';
		private static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(20);
		public string ServerPassword;
		public string Pid;
		public string DPid;
		public string Uid;
		public string ClientUid
		{
			get
			{
				return Uid.Split(CH_Underline)[0];
			}
		}
		public bool IsValidRequest
		{
			get
			{
				return DPid == Pid;
			}
		}
		protected Credential(string uid, Cache cache)
		{
			this.Uid = uid;
			this.cache = cache;
		}
		public static Credential CreateInstance(string uid, Cache cache)
		{
			if (cache != null && !cache.Exists(uid))
			{
				Credential c = new Credential(uid, cache);
				cache.Insert(uid, c, null, DateTime.MaxValue, DefaultDuration);
				return c;
			}
			else
			{
				return cache[uid] as Credential;
			}
		}
		public void GeneratePassword(bool forceNewPassword = false)
		{
			if (forceNewPassword || string.IsNullOrEmpty(this.ServerPassword))
			{
				this.ServerPassword = Guid.NewGuid().ToString().Replace(STR_Dash, string.Empty);
			}
		}
		public void GeneratePid(ClientCredential client = null, bool forceNewPid = true)
		{
			if (string.IsNullOrEmpty(Pid))
			{
				Pid = Guid.NewGuid().ToString().Replace(STR_Dash, string.Empty);
			}
			if (client != null)
			{
				if (string.IsNullOrEmpty(client.Pid) || forceNewPid)
				{
					client.Pid = Pid.EncryptStringAES(ServerPassword);
				}
			}
		}
		public bool Validate(ClientCredential client, bool forceDecrypt = true)
		{
			if (client != null)
			{
				if (!string.IsNullOrEmpty(client.Pid))
				{
					try
					{
						if (forceDecrypt || string.IsNullOrEmpty(DPid))
						{
							DPid = client.Pid.DecryptStringAES(ServerPassword);
						}
					}
					catch
					{
						return false;
					}
				}
				client.IsValid = IsValidRequest;
			}
			return IsValidRequest;
		}
	}
	public class ClientCredential
	{
		public string ClientPassword;
		public string Pid;
		public string Uid;
		public bool IsValid;
		public string Page;
		public ClientCredential()
		{
			Uid = Guid.NewGuid().ToString();
		}
	}
}
