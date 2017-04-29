Fc.XmlHttp = function(CallBackFunc, Cmd) {
	var xmlhttp = null;

	if (window.XMLHttpRequest) {
		// code for all new browsers
		xmlhttp = new XMLHttpRequest();
	} else if (window.ActiveXObject) {
		// code for IE5 and IE6
		xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
	}
	if (xmlhttp != null) {
		//=============================================
		this.send = function(url, form) {
			xmlhttp.open("post", url, true);
			xmlhttp.setRequestHeader("content-type", "application/x-www-form-urlencoded");
			xmlhttp.setRequestHeader("charset", "utf-8");
			xmlhttp.send(form);
		};
		//=============================================
		xmlhttp.onreadystatechange = function() {
			var rlt;
			// 4 = "loaded"
			if (xmlhttp.readyState == 4) {
				// 200 = OK
				if (xmlhttp.status == 200) {
					//CallBackFunc (escape (xmlhttp.responseText), escape (Cmd));
					//alert(xmlhttp.responseXML.innerHTML);
					rlt = xmlhttp.responseText;
					if (rlt.indexOf("Error") == 0) {
						//alert(xmlhttp.responseText);
						var txt;
						txt = document.createElement("pre");
						txt.innerHTML = rlt;
						txt.style.display = "none";
						txt.style.width = "800px";
						txt.style.height = "500px";
						document.body.appendChild(txt);
						var o = Fc.parseJsonStr(rlt, true);
						o.error = o.content;
						CallBackFunc(o, false, Cmd);
					} else {
						var o = Fc.parseJsonStr(xmlhttp.responseText, true);
						CallBackFunc(o, true, xmlhttp.responseText);
					}
					return true;
				} else {
					//var t = window.open("about:blank", "_blank");
					document.write("Problem retrieving XML data\n" + xmlhttp.responseText);
					return null;
				}
			}
		};
	} else {
		alert("Your browser does not support XMLHTTP.");
		return null;
	}
	this.xmlhttp = xmlhttp;
	return this;
}
Fc.Request = function(CallBack) {
	var request = new Object();
	request.Query = new Fc.Dict();
	request.Form = new Fc.Dict();
	request.CallBack = CallBack;
	request.send = function(target, CallBack) {
		if (!CallBack) {
			CallBack = request.CallBack;
		}
		var xmlhttp = new Fc.XmlHttp(function(rlt, success, text) {
			if (success) {
				if (rlt.error) {
					if (rlt.execute) {
						if (Fc.$$[rlt.execute]) {
							Fc.$$[rlt.execute](rlt.params);
						} else {
							if (rlt.alterscript) {
								rlt.alterscript();
							}
						}
					}
					CallBack(rlt.error, false);
				} else {
					CallBack(text, true);
				}
			}
		});
		var url = target + "?" + request.Query.toUrlString(true);
		var frm = request.Form.toUrlString(true);
		xmlhttp.send(url, frm);
	}
	return request;
}

Fc.RemoteCall = function(cfg) {
	var r = new Fc.Request(function(text, success) {
		var o;
		if (success) {
			o = Fc.parseJsonStr(text);
			if (cfg.CallBack) {
				cfg.CallBack(o, true)
			}
		} else {
			if (cfg.CallBack) {
				cfg.CallBack(text, false);
			}
		}
		if (cmd) {
			cmd(text, success);
		}
		//document.write(text);
	});
	r.Form.add('methodname', cfg.method);
	if (cfg.param) {
		var i = 0;
		for (var p in cfg.param) {
			r.Form.add('par_' + i, cfg.param[p]);
			i++;
		}
	}
	else if (cfg.params) {
		for (var i = 0; i < cfg.params.length; i++) {
			r.Form.add('par_' + i, cfg.params[i]);
		}
	}

	if (cfg.target) {
		r.send(cfg.target);
	}
}
Fc.AsynRequest = function(cfg) {
	var request = new Object();
	request.$ = cfg;
	request.Query = new Fc.Dict();
	request.Form = new Fc.Dict();
	request.CallBack = cfg.CallBack;
	request.send = function(target, CallBack) {
		if (!CallBack) {
			CallBack = request.CallBack;
		}
		var xmlhttp = new Fc.XmlHttp(function(rlt, success, text) {
			if (success) {
				if (rlt.error) {
					if (rlt.execute) {
						if (Fc.$$[rlt.execute]) {
							Fc.$$[rlt.execute](rlt.params);
						} else {
							if (rlt.alterscript) {
								rlt.alterscript();
							}
						}
					}
					CallBack(rlt.error, false);
				} else {
					CallBack(rlt, true);
				}
			} else {
				CallBack(rlt, false);
			}
			if (request.$.OnCallBackComplete) {
				request.$.OnCallBackComplete(rlt, success, text);
			}
		});
		var url = target + "?" + request.Query.toUrlString(true);
		var frm = request.Form.toUrlString(true);
		xmlhttp.send(url, frm);
	}
	return request;
}
Fc.QueuedRemoteCaller = function() {
	var qrc = new Object();
	var rclist = new Array();
	var pos = 0;
	qrc._rclist = rclist;
	qrc._pos = 0;
	qrc.add = function(cfg) {
		qrc._rclist.push(cfg);
	}
	qrc.send = function() {
		var curt, r;
		if (qrc._pos < qrc._rclist.length) {
			curt = qrc._rclist[qrc._pos];
			curt.OnCallBackComplete = qrc.send;
			r = new Fc.AsynRequest(curt);
			qrc._pos++;
			r.Form.add('methodname', curt.method);
			if (curt.param) {
				var i = 0;
				for (var p in curt.param) {
					r.Form.add('par_' + p, curt.param[p]);
					i++;
				}
			}

			if (curt.target) {
				r.send(curt.target);
			}
		}
	}
	return qrc;
}
Fc.QueuedRemoteCall = new Fc.QueuedRemoteCaller();

/*
		Fc.RemoteCall({
			CallBack: function(o, success) {
				if (success) {
					Fc.$.MsgContext.FromMember = null;
					Fc.$$.login();
				}
			}
			, method: "Logout"
			, target: "DataProvider/Authenticate.aspx"
		})
		Fc.QueuedRemoteCall.add({
    		CallBack: function(o, success) {
    			if (!success) {
    				alert(o.error);
    			} else {
    				alert(o.content);
    			}
    		}
			, method: "Test"
			, param: {
				str: "Success - 1"
			}
			, target: "DataProvider/RemoteCallHandler.aspx"
    	});
		Fc.QueuedRemoteCall.send();
*/