using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Reflection;

namespace Joy.Server
{
	public class Validator
	{
		public static void CheckNullReference(params object[] pars)
		{
			const string template = "Null reference detected ({0}.{1}:{2})";
			for (int i = 0; i < pars.Length; i++)
			{
				if (pars[i] == null)
				{
					StackTrace trace = new StackTrace();
					StackFrame frame = trace.GetFrame(1);
					MethodBase method = frame.GetMethod();
					throw new ArgumentNullException(string.Format(template, method.DeclaringType.FullName, method.Name, method.GetParameters()[i]));
				}
			}
		}
	}
}