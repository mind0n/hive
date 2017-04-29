using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreJson;


namespace Logging.Tests
{
	[TestClass]
	public class JsonParserTests
	{
		string singleLineJson = "{ config : { boolval: true, numval: 20.1, oval : { 'result':'pass' }, aval:[ 1, true, 'text', { 'key': 'val' }, [ 1.0, 2.0 , 3.0 ]] } }";

		[TestMethod]
		public void SimpleObjectDeserialize()
		{
			var parser = new JsonParser(singleLineJson);
			var rlt = parser.Parse();
			if (rlt.success)
			{
				var d = rlt.Get<Dobj>();
				var j = Dobj.ToJson(d);
				var s = "{ \"config\":{ \"boolval\":True,\"numval\":20.1,\"oval\":{ \"result\":\"pass\" },\"aval\":[1,True,\"text\",{ \"key\":\"val\" },[1.0,2.0,3.0]] } }";
				Assert.AreEqual(s, j);
			}
			Assert.AreEqual(true, rlt.success);
		}
	}
}
