function Tests() {
	var x = new J.xmlObject('/TestSP.xml');
	J.invoke("/Respond.aspx", x, function(result, successful, text) {
		J.logobj(result, 22);
		J.log('------------------------------');
		J.log(successful);
		J.log('------------------------------');
		J.log(text);
	});
}
function tStoredProcedure() {
}
function tRequestInvoker() {
	var x = new J.xmlObject('/TestDataHandler.xml');
	J.logobj(x, 22);
	J.log("===============================");
	J.invoke("/Respond.aspx", x, function(result, successful, text) {
		J.logobj(result, 22);
		J.log('------------------------------');
		J.log(successful);
		J.log('------------------------------');
		J.log(text);
	});
}
function tInvoker() {
	var x = new J.xmlObject('/TestRequest.xml');
	//J.log(s);
	var r = J.parseJson('{o:"ok"}');
	alert(r.o);

	J.invoke("/Respond.aspx", x, function(result, successful, text) {
		J.logobj(result, 22);
		J.log(successful);
		J.log(text);
	});
}
function tRequest() {
	var x = new J.xmlObject('/temp.xml');
	var s = J.xmlSafeString(x, 'root');
	var r = new J.request();
	//J.log(s);
	r.send("/Respond.aspx", "xml=" + escape(s), function(content) {
		J.log(content);
	});
}
function tXML() {
	var x = new J.xmlObject('/temp.xml');
	J.logobj(x.users[0].user[0]);
	J.logobj(x.users[0]);
	J.logobj(x);
	var s = J.xml.obj2xmlstr(x, 'root');
	J.log(s);
}
function aTests() {

	var arr = J.GetElementsByStyle('#mx');
	for (var i = 0; i < arr.length; i++) {
		out(arr[i] + '\n');
	}
	out('\n===============\n');
	arr = J.Select('DIV DIV SPAN.c#c');
	out(arr + '\n');
	J.test = J.Extend(origin, {
		b: 'extended'
	});
	var x = new J.Xml('/temp.xml', function(nodes, doc) {
		J.Log(J.XmlValue(nodes));
		J.Log('-----------------------------');
	});
	var xx = new J.Xml('/temp.xml');
	//J.Log(J.XmlValue(xx));
	var ox = J.Xml.xml2obj(xx);
	var xo = J.Xml.obj2xml(xo);
	J.logobj(ox.users[0].user[0]);
	//http://perfectionkills.com/how-ecmascript-5-still-does-not-allow-to-subclass-an-array/#function_objects_and_construct
	//    		var t = new MyArr();
	//    		t.push(1);
	//    		t.push(2);
	//    		t.push(3);
	//    		alert(t + ',' + t.ok);
}