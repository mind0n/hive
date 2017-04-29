using System;
using System.Collections;
using System.Xml.Serialization;
using ULib.Core;

namespace Wcf.Interface.DataSchema
{
	[XmlInclude(typeof(ResultBase))]
	[Serializable]
	public class ExecuteResult : ResultBase
	{
		public string ActionName;
	}

	[XmlInclude(typeof(ExecuteResult))]
	[Serializable]
	public class ExecuteResultSet : ArrayList
	{
	}
}
