using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreJson;
using Logger;

namespace Middlewares
{
	public class ProcessorFactory
	{
		protected static Dobj cache { get; } = new Dobj();

		static ProcessorFactory()
		{
			Dobj.Set(cache, "log", "Middlewares.LogProcessor,Middlewares");
		}

		public T Create<T>(string name, object [] args) where T : MiddleProcessor
		{
			try
			{
				if (Dobj.Exists(cache, name))
				{
					var typename = Dobj.Get<string>(cache, name);
					T processor = (T) typename.CreateInstance(null, args);
					return processor;
				}
			}
			catch (Exception ex)
			{
				log.e(ex);
			}
			return default(T);
		}
	}
}
