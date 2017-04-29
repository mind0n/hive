using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Core
{
	public class StatusMac<Ti, Ts>
	{
		public delegate void CacheFullHandler(Ti[] Cache, Ts[] Status);
		public delegate bool RoleProcessHandler(Ti[] Cache, Ts[] Status);

		protected event CacheFullHandler OnCacheFull;
		protected List<RoleProcessHandler> RoleProcessors = new List<RoleProcessHandler>();
		const int cachesize = 3;
		protected Ti[] Cache = new Ti[3];
		protected Ts[] Status = new Ts[3];
		protected char Regist;
		protected void Read(Ti i)
		{
			ShiftLeft();
			Cache[2] = i;
		}

		protected void ApplyRoles()
		{
			foreach (RoleProcessHandler p in RoleProcessors)
			{
				if (p(Cache, Status))
				{
					break;
				}
			}
		}
		protected void ShiftLeft()
		{
			if (!object.Equals(default(Ti), Cache[0]))
			{
				if (OnCacheFull != null)
				{
					OnCacheFull(Cache, Status);
				}
			}
			for (int i = 0; i < Cache.Length - 1; i++)
			{
				Cache[i] = Cache[i + 1];
				Status[i] = Status[i + 1];
			}
		}
	}
	public class HtmlStatusMac : StatusMac<char, StatusCode>
	{
		int limit = 100;
		protected StringBuilder b = new StringBuilder();
		public HtmlStatusMac()
		{
			OnCacheFull += new CacheFullHandler(HtmlStatusMac_OnCacheFull);
			RoleProcessors.Add(new RoleProcessHandler(delegate(char[] cache, StatusCode[] status)
			{
				if (object.Equals(default(char), cache[1]))
				{
					return false;
				}
				if (cache[1] == '<')
				{
					if (status[0] == StatusCode.EndTag || status[0] == StatusCode.Content || status[0] == StatusCode.Unknown)
					{
						status[1] = StatusCode.BeginTag;
					}
					else
					{
						status[1] = status[0];
					}
				}
				else if (cache[1] == '>')
				{
					if (status[0] == StatusCode.Unknown || status[0] == StatusCode.Content)
					{
						status[1] = StatusCode.Content;
					}
					else if (status[0] == StatusCode.BeginStr || status[0] == StatusCode.Str)
					{
						status[1] = StatusCode.Str;
					}
				}
				else if (cache[1] == '"')
				{
					if (status[0] == StatusCode.Content || (Regist == '\'' && status[0] == StatusCode.Str))
					{
						status[1] = status[0];
					}
					else if (status[0] == StatusCode.BeginStr)
					{
						if (Regist != '"')
						{
							status[1] = StatusCode.Str;
						}
						else
						{
							status[1] = StatusCode.EndStr;
						}
					}
					else if (status[0] == StatusCode.EndStr)
					{
						status[1] = StatusCode.BeginStr;
						Regist = cache[1];
					}
				}
				else if (cache[1] == '\'')
				{
					if (status[0] == StatusCode.Content || (Regist == '"' && status[0] == StatusCode.Str))
					{
						status[1] = status[0];
					}
					else if (status[0] == StatusCode.BeginStr)
					{
						if (Regist != '\'')
						{
							status[1] = StatusCode.Str;
						}
						else
						{
							status[1] = StatusCode.EndStr;
						}
					}
					else if (status[0] == StatusCode.EndStr)
					{
						status[1] = StatusCode.BeginStr;
						Regist = cache[1];
					}
				}
				else
				{
					if (status[0] == StatusCode.EndStr || status[0] == StatusCode.Tag || status[0] == StatusCode.BeginTag)
					{
						status[1] = StatusCode.Tag;
					}
					else if (status[0] == StatusCode.BeginStr || status[0] == StatusCode.Str)
					{
						status[1] = StatusCode.Str;
					}
					else if (status[0] == StatusCode.EndTag || status[0] == StatusCode.Unknown || status[0] == StatusCode.Content)
					{
						status[1] = StatusCode.Content;
					}
				}
				return true;
			}));
		}

		void HtmlStatusMac_OnCacheFull(char[] Cache, StatusCode[] Status)
		{
			char c = Cache[0];
			StatusCode s = Status[0];
			if (s == StatusCode.Content && b.Length < limit)
			{
				b.Append(c);
			}
		}
		public void Parse(string html)
		{
			for (int i = 0; i < html.Length; i++)
			{
				char c = html[i];
				Read(c);
				ApplyRoles();
			}
			Flush();
		}
		public override string ToString()
		{
			return b.ToString();
		}
		protected void Flush()
		{
			for (int i = 1; i <= 3; i++)
			{
				ShiftLeft();
				ApplyRoles();
			}
		}
	}
	public enum StatusCode : int
	{
		Unknown,
		BeginStr,
		Str,
		EndStr,
		BeginTag,
		Tag,
		EndTag,
		Content
	}
}
