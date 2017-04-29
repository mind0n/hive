using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface;
using System.ServiceModel;
using System.Xml.Linq;

namespace Wcf.Proxy
{
	public class FavUrlProxy : ClientBase<IFavUrl>, IFavUrl
	{
		public FavUrlProxy(WSHttpBinding bind, EndpointAddress addr) : base(bind, addr) { }

		public void SetUrl(string favurl)
		{
			base.Channel.SetUrl(favurl);
		}

		public string GetUrls()
		{
			string result = base.Channel.GetUrls();
			return result;
		}
	}
}
