using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Web.Services
{

	public enum ScriptMethodParamType
	{
		Raw = 0
		, Integrated = 1
		, Explicit = 2
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple= false)]
	public class ScriptMethodAttribute : Attribute
	{
		public ScriptMethodParamType ParamType;
		public ScriptMethodAttribute()
		{

		}
	}
}
