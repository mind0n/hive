using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ServiceModel;

namespace Wcf.Interface
{
	[ServiceContract]
	public interface IFavUrl
	{
		[OperationContract]
		void SetUrl(string favurl);

		[OperationContract]
		string GetUrls();
	}
}
