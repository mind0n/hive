JLib_v1.request = function () {
	var ajax, as, rlt;
	try {
		ajax = new ActiveXObject("Microsoft.XMLHTTP");
		as = 1;
	} catch (e) {
		try {
			ajax = new ActiveXObject("Msxml2.XMLHTTP");
			as = 1;
		} catch (e) {
			try {
				ajax = new XMLHttpRequest();
				as = 2;
			} catch (e) {
				ajax = null;
				as = 0;
			}
		}
	}
	if (ajax) {
		rlt = {};
		rlt.ajax = ajax;
		rlt.as = as;
		rlt.send = function (url, formparam, callback, isSync) {
			rlt.ajax.open("POST", url, isSync);
			rlt.ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			if (rlt.as == 1) {
				rlt.ajax.onreadystatechange = function () {
					if (rlt.ajax.readyState == 4) {
						content = rlt.ajax.responseText;
						callback(content, { status: rlt.ajax.status, link: url });
					}
				};
			} else {
				rlt.ajax.onload = function () {
					content = rlt.ajax.responseText;
					callback(content, { status: rlt.ajax.status, link: url });
				};
				rlt.ajax.onerror = function () {
					content = rlt.ajax.responseText;
					callback(content, { status: rlt.ajax.status, link: url });
				};
			}
			rlt.ajax.send(formparam);
		};
	}
	return rlt;
};
JLib_v1.invoke = function (url, param, callback, isSync, xmlInvokeKey) {
	var r = new JLib_v1.request();
	var content;
	if (!xmlInvokeKey) {
		xmlInvokeKey = "xml=";
	}
	if (typeof (param) != 'string') {
		content = JLib_v1.xmlSafeString(param, 'invoke');
	} else {
		content = JLib_v1.xmlSafeString(param);
	}
	r.send(url
	, xmlInvokeKey + escape(content)
	, function (content, param) {
		var status = param.status;
		var rlt = JLib_v1.xmlObject(content);
		var callbackParam = { content: rlt, invokeSuccessful: true, respondOk: status == 200, url: param.link };
		//J.logobj(rlt);
		if (!rlt) {
			callbackParam.invokeSuccessful = false;
		} else {
			callbackParam.invokeSuccessful = (rlt.$successful == 'true');
		}
		if (status != 200) {
			callbackParam.respondOk = false;
		}
		callback(callbackParam);
		if (!content) {
			JLib_v1.log("Response empty or page not found (" + param.link + ")");
		}
	}
	, isSync);
};
/*

url	=	/Respond.aspx
respondOk	=	true
invokeSuccessful	=	false
------parent	=	[object Object]
------values	=	Invalid method name(GetAnswers)
------name	=	error
-----$	=	[object Object]
----0	=	[object Object]
---error	=	[object Object]
------parent	=	[object Object]
------name	=	return
-----$	=	[object Object]
----0	=	[object Object]
---return	=	[object Object]
----parent	=	[object Object]
----name	=	method
---$	=	[object Object]
---$successful	=	false
---$name	=	GetAnswers
--1	=	[object Object]
------parent	=	[object Object]
------values	=	This is the content|2011
------name	=	return
-----$	=	[object Object]
----0	=	[object Object]
---return	=	[object Object]
----parent	=	[object Object]
----name	=	method
---$	=	[object Object]
---$successful	=	true
---$name	=	GetAnswer
--0	=	[object Object]
-method	=	[object Object],[object Object]
--name	=	result
-$	=	[object Object]
-$successful	=	true
content	=	[object Object]


*/
JLib_v1.groupInvoke = function (url, param, callbacks, isSync, xmlInvokeKey) {
	JLib_v1.invoke(url, param, function (result) {
		if (result.invokeSuccessful) {
			for (var i = 0; i < callbacks.length; i++) {
				var callback = callbacks[i];
				for (var j = 0; j < result.content.method.length; j++) {
					var detail = result.content.method[j];
					if (callback.name == detail.$name) {
						var callbackParam = {successful:detail.$successful=='true', details: detail.content[0].$.values, error: null, content: null };

						if (detail.$successful != "true") {
							callbackParam.error = detail.error[0].$.values;
							callbackParam.content = detail.error[0].$.values[0];
						}else{
							callbackParam.content = detail.content[0].$.values[0];
						}
						 
						callback.callback(callbackParam);
					}
				}
				//if (callbacks[i].name == result.content.method)
			}
		}
	}, isSync, xmlInvokeKey);
};
/*
[
{name:'GetAnswer', param:[{name:'content', $:'This is the content :)'}]}
,{name:'GetAnswers', param:[{name:'content', $:'This is the content :)'}]}
]
*/
JLib_v1.makeInvoke = function (obj) {
	var header = '<?xml version="1.0" encoding="utf-8" ?>';
	var methodStr = '';
	for (var i = 0; i < obj.length; i++) {
		var method = obj[i];
		var paramStr = '';
		for (var p = 0; p < method.param.length; p++) {
			var param = method.param[p];
			var attrStr = '';
			for (var attr in param) {
				if (attr != '$') {
					attrStr += ' ' + attr + '="' + param[attr] + '" ';
				}
			}
			paramStr += '<param ' + attrStr + '>' + param.$ + '</param>';
		}
		methodStr += '<method Name="'+method.Name+'">' + paramStr + '</method>';
	}
	return header + '<invoke>' + methodStr + '</invoke>';
};