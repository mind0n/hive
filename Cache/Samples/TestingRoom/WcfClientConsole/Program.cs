
using System;
using System.ServiceModel;
using WcfService;

namespace WcfClientConsole
{
    class Program
    {
        static void Log(string msg)
        {
            Console.WriteLine(msg);
        }
        static void Main(string[] args)
        {
            using (ChannelFactory<ITestService> cf = new ChannelFactory<ITestService>(new NetTcpBinding(), TestService.NetTcpAddress))
            {
                ITestService ts = cf.CreateChannel();
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        var rlt = ts.Test();
                        Log(rlt);
                    }
                    catch (Exception ex)
                    {
                        Log(ex.Message);
                        cf.Abort();
                    }
                }
            } 

            //using (ChannelFactory<ITestService> cf = new ChannelFactory<ITestService>(new NetTcpBinding(), TestService.Address))
            //{
            //    ITestService ts = cf.CreateChannel();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        try
            //        {
            //            var rlt = ts.Test();
            //            Log(rlt);
            //        }
            //        catch (Exception ex)
            //        {
            //            Log(ex.Message);
            //            cf.Abort();
            //        }
            //    }
            //}
            Console.ReadKey();
        }
    }
}
