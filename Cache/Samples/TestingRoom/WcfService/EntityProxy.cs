using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Channels;
using System.Text;

namespace WcfService
{
	public class ProxyResult
	{
		public override string ToString()
		{
			return "ProxyResult";
		}
	}
	public class EntityProxy : RealProxy
	{
		protected object Entity;
		public EntityProxy(Type type):base(type)
		{

		}
		public EntityProxy(object target):base(target.GetType())
		{
			Entity = target;
		}
		public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
		{
			IMethodCallMessage mcMsg = msg as IMethodCallMessage;
			if (mcMsg != null)
			{
				ReturnMessage rlt = null;
				if (!string.Equals(mcMsg.MethodName, "Test", StringComparison.OrdinalIgnoreCase))
				{
					object instance = null;
					if (Entity == null)
					{
						Type type = Type.GetType(mcMsg.TypeName);
						instance = Activator.CreateInstance(type);
					}
					else
					{
						instance = Entity;
					}
					object returnValueObject = mcMsg.MethodBase.Invoke(instance, null);
					rlt = new ReturnMessage(returnValueObject, mcMsg.Args, mcMsg.ArgCount, mcMsg.LogicalCallContext, mcMsg);
				}
				else
				{
					rlt = new ReturnMessage(new ProxyResult(), mcMsg.Args, mcMsg.ArgCount, mcMsg.LogicalCallContext, mcMsg);
				}

				return rlt;
			}
			return null;
		}
	}
}
