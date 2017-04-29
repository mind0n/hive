using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface;
using System.Xml.Linq;
using Wcf.Interface.DataSchema;
using Wcf.Service.Executors;
using System.Collections.ObjectModel;

namespace Wcf.Service
{
	public class FavUrlService : IFavUrl
	{
		public void SetUrl(string favurl)
		{
			Collection<WcfAction> actions = WcfAction.Parse(favurl);
			ExecuteResultSet result = ActionExecutor.ExecuteActions(actions);
		}
		public string GetUrls()
		{
			Collection<WcfAction> actions = WcfAction.ParseEmpty(ActionNames.GetUrlsAction);
			ExecuteResultSet result = ActionExecutor.ExecuteActions(actions);
			return WcfExtensions.Object2Xml(result, typeof(ExecuteResultSet));
		}
	}
}
