using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using WcfService;

namespace WcfHostConsole
{
	class Program
	{
        [ThreadStatic]
	    private static int n;
		static void Main(string[] args)
		{
            //for (int i = 0; i < 10; i++)
            //{
            //    new Thread(delegate()
            //    {
            //        n++;
            //        Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ";" + n);

            //    }).Start();
            //}

            ServiceHost host = new ServiceHost(typeof(TestService));
            try
            {
                ServiceThrottlingBehavior stb = new ServiceThrottlingBehavior
                {
                    MaxConcurrentCalls = 1,
                    MaxConcurrentInstances = 1
                };
                host.AddServiceEndpoint(typeof(ITestService), new NetTcpBinding(), TestService.NetTcpAddress);
                host.Description.Behaviors.Add(stb);
                host.Open();
            }
            catch
            {
                host.Abort();
            }

			Console.WriteLine("Press to exit ...");
			Console.ReadKey();
		}
	}
}
