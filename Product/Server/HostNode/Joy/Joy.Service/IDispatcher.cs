using Joy.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Joy.Service
{
    [ServiceContract]
    public interface IDispatcher
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "{cname}/{aname}/{*pars}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream Dispatch(string cname, string aname, string pars);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream Invoke(Stream formdata);
    }
}
