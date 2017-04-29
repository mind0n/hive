function Tests() {
	
}
function tXmlConvert() {
	var o = {
		param: [
			{ name: 'content', $: 'value' }
			, { name: 'content2', $: 'value2' }
		]
		, target: {
			name: 'localhost'
			, value: '127.0.0.1'
		}
	}
	var obj = J.xml.convert2object(J.xml.convert2xml(o, 'invoke', true), 22);
	J.logobj(obj, 24);
}
function tObjInvoke() {
	var x = new JLib_v1.xmlObject('/TestRequest.xml');
	//J.logobj(x,22);
	var invoke = [
						{ Name: 'GetAnswer', param:
						[
							{ Name: 'content', $: 'This is the content :)' }
							, { Name: 'year', $: '2009' }
						]
						}
						 ,
						{ Name: 'GetAnswers', param:
						[
							{ Name: 'content', $: 'This is the content :)' }
							, { Name: 'year', $: '2009' }
						]
						}
					];
	var x = J.makeInvoke(invoke);
	//J.log(x);
	//return;
	J.groupInvoke("/Respond.aspx", x, [
		{
			name: 'GetAnswer'
			, callback: function (result) {
				J.logobj(result, 22);
			}
		}
		,
		{
			name: 'GetAnswers'
			, callback: function (result) {
				J.logobj(result, 22);
			}
		}

	]);
}
function tStoredProcedure() {
}
function tRequestInvoker() {
	var x = new JLib_v1.xmlObject('/TestDataHandler.xml');
	JLib_v1.logobj(x, 22);
	JLib_v1.log("===============================");
	JLib_v1.invoke("/Respond.aspx", x, function(result, successful, text) {
		JLib_v1.logobj(result, 22);
		JLib_v1.log('------------------------------');
		JLib_v1.log(successful);
		JLib_v1.log('------------------------------');
		JLib_v1.log(text);
	});
}
function tInvoker() {
	var x = new JLib_v1.xmlObject('/TestRequest.xml');
	//JLib_v1.log(s);
	var r = JLib_v1.parseJson('{o:"ok"}');
	alert(r.o);

	JLib_v1.invoke("/Respond.aspx", x, function(result, successful, text) {
		JLib_v1.logobj(result, 22);
		JLib_v1.log(successful);
		JLib_v1.log(text);
	});
}
function tRequest() {
	var x = new JLib_v1.xmlObject('/temp.xml');
	var s = JLib_v1.xmlSafeString(x, 'root');
	var r = new JLib_v1.request();
	//JLib_v1.log(s);
	r.send("/Respond.aspx", "xml=" + escape(s), function(content) {
		JLib_v1.log(content);
	});
}
function tXML() {
	var x = new JLib_v1.xmlObject('/temp.xml');
	JLib_v1.logobj(x.users[0].user[0]);
	JLib_v1.logobj(x.users[0]);
	JLib_v1.logobj(x);
	var s = JLib_v1.xml.obj2xmlstr(x, 'root');
	JLib_v1.log(s);
}
function aTests() {

	var arr = JLib_v1.GetElementsByStyle('#mx');
	for (var i = 0; i < arr.length; i++) {
		out(arr[i] + '\n');
	}
	out('\n===============\n');
	arr = JLib_v1.Select('DIV DIV SPAN.c#c');
	out(arr + '\n');
	JLib_v1.test = JLib_v1.Extend(origin, {
		b: 'extended'
	});
	var x = new JLib_v1.Xml('/temp.xml', function(nodes, doc) {
		JLib_v1.Log(JLib_v1.XmlValue(nodes));
		JLib_v1.Log('-----------------------------');
	});
	var xx = new JLib_v1.Xml('/temp.xml');
	//JLib_v1.Log(JLib_v1.XmlValue(xx));
	var ox = JLib_v1.Xml.xml2obj(xx);
	var xo = JLib_v1.Xml.obj2xml(xo);
	JLib_v1.logobj(ox.users[0].user[0]);
	//http://perfectionkills.com/how-ecmascript-5-still-does-not-allow-to-subclass-an-array/#function_objects_and_construct
	//    		var t = new MyArr();
	//    		t.push(1);
	//    		t.push(2);
	//    		t.push(3);
	//    		alert(t + ',' + t.ok);
}