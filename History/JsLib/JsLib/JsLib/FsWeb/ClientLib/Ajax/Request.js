J.request = function () {
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
		rlt.send = function (url, formparam, callback) {
			rlt.ajax.open("POST", url, false);
			rlt.ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			if (rlt.as == 1) {
				rlt.ajax.onreadystatechange = function () {
					if (rlt.ajax.readyState == 4) {
						content = rlt.ajax.responseText;
						callback(content);
					}
				};
			} else {
				rlt.ajax.onload = function () {
					content = rlt.ajax.responseText;
					callback(content);
				};
				rlt.ajax.onerror = function () {
					content = rlt.ajax.responseText;
					callback(content);
				};
			}
			rlt.ajax.send(formparam);
		};
	}
	return rlt;
};
J.invoke = function (url, param, callback) {
	var r = new J.request();
	r.send(url, 'xml=' + escape(J.xmlSafeString(param, 'invoke')), function (content) {
		var rlt = J.xmlObject(content);
		if (!rlt.$successful) {
			rlt.$successful = "false";
			rlt.content = content;
		}
		if (content) {
			try {
				var v = rlt.$successful.toLowerCase();
				var isOk = (v == "true") ? true : false;
				callback(rlt, isOk, content);
			} catch (e) {
				callback(rlt, false, content);
			}
		}
	});
}
