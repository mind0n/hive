using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace WcfService
{
	[ServiceBehavior(
        IncludeExceptionDetailInFaults = true, 
        ConcurrencyMode = ConcurrencyMode.Multiple, 
        InstanceContextMode = InstanceContextMode.PerCall,
        AddressFilterMode = AddressFilterMode.Exact)]
    public class TestService : ITestService //MarshalByRefObject, ITestService
	{
	    [ThreadStatic] public static string SericeId;

        public string Temp = "Temp information";
        public string Name { get; set; }

        public static readonly string NetTcpAddress = "net.tcp://localhost:29999/testservice";
        public static readonly string BasicHttpAddress = "http://localhost:29900/testservice";
        public static readonly string SecureHttpAddress = "https://localhost:29901/testservice";
        public TestService()
		{
			Name = "TestServiceName";
		}

        private static void WriteEventLog()
        {
            string sSource;
            string sLog;
            string sEvent;

            sSource = "xxx";
            sLog = "Application";
            sEvent = "Sample Event";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            //EventLog.WriteEntry(sSource, sEvent);
            EventLog.WriteEntry("xxx", "Message Data",
                EventLogEntryType.Warning, 234);
            Thread.Sleep(1000);
        }

	    public string TestHttp()
	    {
	        return "<html><body>Success</body></html>";
	    }

        public void TestError()
		{
			throw new Exception("This is error");
		}

		public string Test()
		{
            StringBuilder b = new StringBuilder();
		    if (string.IsNullOrEmpty(SericeId))
		    {
		        SericeId = Guid.NewGuid().ToString();
		    }
		    b.Append(Thread.CurrentThread.ManagedThreadId).Append("\t").Append(SericeId).Append("\t");
		    if (ServiceSecurityContext.Current != null)
		    {
		        if (!ServiceSecurityContext.Current.IsAnonymous)
		        {
		            b.Append(ServiceSecurityContext.Current.PrimaryIdentity.Name).Append("\t").Append(ServiceSecurityContext.Current.PrimaryIdentity.AuthenticationType);
		        }
		        else
		        {
		            b.Append("Anonymous");
		        }
		    }
		    else
		    {
		        b.Append("NULL");
		    }
		    return b.ToString();
		}

	    public object TestObject()
		{
			return null;
		}
	}
}
