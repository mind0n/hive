using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.ServiceModel.Dispatcher;

namespace Common
{
	public class TraceBehavior:Attribute, IOperationInvoker, IOperationBehavior, IServiceBehavior
	{
		IOperationInvoker innerOperationInvoker;
		public TraceBehavior()
		{
		}
		public TraceBehavior(IOperationInvoker i)
		{
			innerOperationInvoker = i;
		}
		public object[] AllocateInputs()
		{
			return innerOperationInvoker.AllocateInputs();
		}

		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			return (string)this.innerOperationInvoker.Invoke(
							instance, inputs, out outputs);
		}

		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			return innerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
		}

		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			return innerOperationInvoker.InvokeEnd(instance, out outputs, result);
		}

		public bool IsSynchronous
		{
			get
			{
				return innerOperationInvoker.IsSynchronous;
			}
		}

		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new TraceBehavior(dispatchOperation.Invoker);
		}

		public void Validate(OperationDescription operationDescription)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
