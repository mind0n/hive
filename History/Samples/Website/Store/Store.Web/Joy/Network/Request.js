Joy.makeRequest = function (cfg, r) {
	var JsRequest;
	var methodName = cfg.md;
	var args = cfg.ag;
	var nf = cfg.nf;
	var vf = cfg.vf;
	var method = { Name: methodName, Params: [] };
	if (!r) {
		JsRequest = {
			"xmlns:xsi": "http://www.w3.org/2001/XMLSchema-instance",
			"xmlns:xsd": "http://www.w3.org/2001/XMLSchema",
			Methods:
			[
				method
			]
		};
		if (args) {
			for (var i = 0; i < args.length; i++) {
				var list = method.Params;
				var arg = args[i];
				if (typeof (arg) == 'object') {
					list[list.length] = { Name: arg[nf], $: arg[vf] };
				} else {
					list[list.length] = { Name: 'arg' + i, $: arg };
				}
			}
		}
	} else {
		JsRequest = r;
		r.Methods[r.Methods.length] = method;
		if (args) {
			for (var i = 0; i < args.length; i++) {
				var list = method.Params;
				var arg = args[i];
				if (typeof (arg) == 'object') {
					list[list.length] = { Name: arg[nf], $: arg[vf] };
				} else {
					list[list.length] = { Name: 'arg' + i, $: arg };
				}
			}
		}
	}
	return JsRequest;
};
Joy.request = new function () {
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
		rlt.send = function (url, formparam, callback, sender, isSync) {
			rlt.ajax.open("POST", url, isSync);
			rlt.ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			if (rlt.as == 1) {
				rlt.ajax.onreadystatechange = function () {
					if (rlt.ajax.readyState == 4) {
						content = rlt.ajax.responseText;
						callback(sender, content, { status: rlt.ajax.status, link: url });
					}
				};
			} else {
				rlt.ajax.onload = function () {
					content = rlt.ajax.responseText;
					callback(sender, content, { status: rlt.ajax.status, link: url });
				};
				rlt.ajax.onerror = function () {
					content = rlt.ajax.responseText;
					callback(sender, content, { status: rlt.ajax.status, link: url });
				};
			}
			rlt.ajax.send(formparam);
		};
	}
	return rlt;
};