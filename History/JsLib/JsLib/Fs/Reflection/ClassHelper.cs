using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Collections;

namespace Fs.Reflection
{
	public class ThreadResults : List<Object>
	{
		public delegate void CallBackHandler(object sender, ThreadResults results, params object [] pars);
		public bool IsEmpty = true;
		public CallBackHandler PostInvokeCallBack;
		public CallBackHandler PreInvokeCallBack;
		public object Target;
		public Exception Err;
		public Thread Thread;
	}
	public class ClassHelper
	{
		public delegate object ObjectInvoker(params object[] paramlist);
		public delegate object DelegateInvoker(Delegate d);
		public static object GetFirstSpecifiedAttribute(object host, Type attrType) 
		{
			object rlt;
			object [] attrs = host.GetType().GetCustomAttributes(attrType, true);
			if (attrs != null)
			{
				rlt = attrs[0];
			}
			else
			{
				rlt = null;
			}
			return rlt;
		}
		public static ArrayList EnumDelegatesFrom(Delegate method, DelegateInvoker callback)
		{
			Delegate[] dgs = method.GetInvocationList();
			ArrayList rlts = new ArrayList();
			foreach (Delegate dg in dgs)
			{
				rlts.Add(callback(dg));
			}
			return rlts;
		}
		public static object Invoke(Type typ, string method, BindingFlags flags, params object [] pars)
		{
			return typ.InvokeMember(method, flags, null, typ, pars);
		}
		public static ThreadResults[] AsyncInvokeDelegates(Delegate method, params object[] pars)
		{
			return AsyncInvokeDelegates(method, null, pars);
		}
		public static ThreadResults [] AsyncInvokeDelegates(Delegate method, ThreadResults.CallBackHandler preInvokeCallback, ThreadResults.CallBackHandler postInvokeCallback, params object[] pars)
		{
			//List<ThreadResults> rlt = new List<ThreadResults>();
			Delegate[] dgs = method.GetInvocationList();
			ThreadResults[] rlt = new ThreadResults[dgs.Length];
			int i = 0;
			foreach (Delegate dg in dgs)
			{
				rlt[i] = AsyncInvokeSingleDelegate(dg, preInvokeCallback, postInvokeCallback, pars);
				i++;
			}
			return rlt;
		}
		public static ThreadResults AsyncInvokeSingleDelegate(Delegate method, ThreadResults.CallBackHandler preInvokeCallBack, ThreadResults.CallBackHandler postInvokeCallback, params object[] pars)
		{
			ThreadResults returnVal;
			AsyncInvokeSingleDelegate(method, preInvokeCallBack, postInvokeCallback, out returnVal, pars);
			return returnVal;
		}
		public static void AsyncInvokeSingleDelegate(Delegate method, ThreadResults.CallBackHandler preInvokeCallBack, ThreadResults.CallBackHandler postInvokeCallback, out ThreadResults returnVal, params object[] pars)
		{
			ThreadResults rlts = new ThreadResults();
			if (postInvokeCallback != null)
			{
				rlts.PostInvokeCallBack = postInvokeCallback;
			}
			if (preInvokeCallBack != null)
			{
				rlts.PreInvokeCallBack = preInvokeCallBack;
			}
			ParameterizedThreadStart pts = new ParameterizedThreadStart(delegate(object par)
			{
				ThreadResults tr = (ThreadResults)par;
				try
				{
					tr.Add(method.DynamicInvoke(pars));
					tr.IsEmpty = false;
				}
				catch (Exception err)
				{
					Exceptions.LogOnly(err.InnerException);
					tr.Err = err.InnerException;
					throw (tr.Err);
				}
				finally
				{
					tr.Target = method.Target;
					if (tr.PostInvokeCallBack != null)
					{
						//tr.CallBack.DynamicInvoke(null);
						tr.PostInvokeCallBack(method.Target, tr, pars);
					}
				}
			});
			Thread th = new Thread(pts);
			th.SetApartmentState(ApartmentState.STA);
			rlts.Thread = th;
			if (rlts.PreInvokeCallBack != null)
			{
				rlts.PreInvokeCallBack(method.Target, rlts, pars);
			}
			th.Start(rlts);
			returnVal = rlts;
			//return returnVal;
		}
		public static Thread CreateThread(ParameterizedThreadStart worker)
		{
			Thread th = new Thread(worker);
			th.SetApartmentState(ApartmentState.STA);
			th.Start();
			return th;
		}
	}
}
