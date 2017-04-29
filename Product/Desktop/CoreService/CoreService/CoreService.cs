using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Core
{
    public class CoreService : ICoreService
    {
		public int Encode(object obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}
    }
}
