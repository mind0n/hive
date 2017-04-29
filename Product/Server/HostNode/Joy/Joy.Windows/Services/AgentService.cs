using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Joy.Core;

namespace Joy.Windows.Services
{
    public class AgentService : IAgentService
    {
        private ServiceHost h;
        public AgentService(ServiceHost host)
        {
            h = host;
        }

        public bool StartService()
        {
            try
            {
                h.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StopService()
        {
            try
            {
                h.Shutdown();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    [ServiceContract]
    public interface IAgentService
    {
        [OperationContract]
        bool StartService();

        [OperationContract]
        bool StopService();
    }
}
