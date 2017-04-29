using Joy.Users;
using System;
using System.Web.Compilation;

namespace Joy.Server.Web
{
	public class RouteInfo
	{
		public string TypeName;
		public string Url;
	    public string[] Roles;
		public bool IsRestful;
		public bool IsNoSplit;
		public Type Type;

	    public bool Authenticate(UserPrincipal ppl)
	    {
	        if (ppl == null)
	        {
	            return false;
	        }
	        if (Roles == null)
	        {
	            return ppl.IsInRoles(URoles.AnonymousRole.Name);
	        }
	        var r = ppl.IsInRoles(Roles);

	        return r;
	    }

	    public void LoadType()
		{
			if (!string.IsNullOrEmpty(TypeName))
			{
				Type = Type.GetType(TypeName);
			}
			else if (!string.IsNullOrEmpty(Url))
			{
				Type = BuildManager.GetCompiledType(Url);
			}
		}
	}
}