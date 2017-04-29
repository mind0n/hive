using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Fs.Test
{
	public class SingletonBase<TSingletonClass> : System.Windows.Forms.Form where TSingletonClass : class
	{
		static private TSingletonClass instance;

		static SingletonBase()
		{
			//force hold before init flag
			//to lazy create this singleton...
		}

		static public TSingletonClass Instance
		{
			get
			{
				if (instance == null)
				{
					ConstructorInfo ci = typeof(TSingletonClass).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
					instance = (TSingletonClass)ci.Invoke(null);
				}
				return instance;
			}
		}
	}
}
