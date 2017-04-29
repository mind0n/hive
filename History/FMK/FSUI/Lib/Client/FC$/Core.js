var FC = {
	_cloneCount: 0
	, $: {}
	, setCookie: function (name, value, expires, path, domain, secure) {
		var today = new Date();
		today.setTime(today.getTime());

		if (expires) {
			expires = expires * 1000;
		}
		var expires_date = new Date(today.getTime() + (expires));
		document.cookie = name + "=" + escape(value) + ((expires) ? ";expires=" + expires_date.toGMTString() : "") + ((path) ? ";path=" + path : "") + ((domain) ? ";domain=" + domain : "") + ((secure) ? ";secure" : "");
	}
	, getCookie: function (name) {
		if (document.cookie.length > 0) {
			c_start = document.cookie.indexOf(name + "=");
			if (c_start != -1) {
				c_start = c_start + name.length + 1;
				c_end = document.cookie.indexOf(";", c_start);
				if (c_end == -1) {
					c_end = document.cookie.length;
				}
				return unescape(document.cookie.substring(c_start, c_end));
			}
		}
		return "";
	}
	, cleanCookie: function (name) {
		if (document.cookie.length > 0) {
			var dict = new FC.Dict();
			var cookies = document.cookie.split("; ");
			for (var i = 0; i < cookies.length; i++) {
				var item = cookies[i];
				item = item.split("=");
				var k = item[0];
				var v = item[1];
				dict.replace(k, v);
			}
			//  			, toSpecifiedString: function(kvSplitter, vl, vr, itemSplitter, groupSplitter, forceEncode) {
			document.cookie = dict.toSpecifiedString('=', '', '', '', '; ', false);
			//alert(dict.toSpecifiedString('=', '', '', '', '; ', false));
		}
	}
	, delCookie: function (name) {
		var d = new Date();
		this.setCookie(name, "", -1);
		//var c = this.getCookie(name);
		//c.expires = new Date(d - 1000);
	}
	, parseJsonStr: function (jsonstr, forceObjRlt) {
		if (jsonstr && jsonstr.length > 1) {
			try {
				return eval("(" + jsonstr + ")");
			} catch (e) {
				if (forceObjRlt) {
					var rlt = new Object();
					rlt.content = jsonstr;
					return rlt;
				} else {
					return jsonstr;
				}
			}
		} else {
			return new Object();
		}
	}
	, G: function (id) {
		return document.getElementById(id);
	}
	, C: function (tag) {
		return document.createElement(tag);
	}
	, V: function (evt, withEvt) {
		var evt = evt || window.event;
		var target = evt.target || evt.srcElement;
		if (withEvt) {
			return {
				evt: evt
				, target: target
			}
		} else {
			return target;
		}
	}
	, Log: function (content, title) {
		//alert(content);
		if (!document.logmsg) {
			document.logmsg = new Array();
		}
		document.logmsg.push({ title: title, content: content + "<br />" });
	}
	, CloneNode: function (origin, target) {
		if (!origin) {
			return null;
		}
		if (!target) {
			//target = document.createElement(origin.nodeName);
			target = origin.cloneNode(false);
		}
		for (var p in origin) {
			try {
				var typ = typeof (origin[p]);
				//				if (typ == "string" && origin[p].indexOf("1") >= 0) {
				//					alert(p + "=" + origin[p]);
				//				}
				if (p != "childNodes" && p != "innerText" && p != "textContent" && p != "innerHTML") {
					target[p] = origin[p];
				}
			} catch (ex) {

			}
		}
		for (var i = 0; i < origin.childNodes.length; i++) {
			var node = origin.childNodes[i];
			if (node.nodeType == 1) {
				var child = this.CloneNode(node);
				target.appendChild(child);
			} else {
				target.appendChild(node.cloneNode(false));
			}
		}
		return target;
	}
  	, Clone: function (origin, target, skipOverwrite) {
  		var deepCopy = true;
  		if (!origin) {
  			origin = this;
  		}
  		if (!target) {
  			target = new Object();
  		}
  		for (var p in origin) {
  			if (target[p] && skipOverwrite) {
  				continue;
  			}
  			var typ = typeof (origin[p]);
  			if (deepCopy && (typ == "object")) {
  				target[p] = this.Clone(origin[p]);
  			} else {
  				target[p] = origin[p];
  			}
  		}
  		return target;
  	}
  	, Extend: function (extension, base) {
  		if (!base) { base = this; }
  		var rlt = this.Clone(base, extension, true);
  		if (!rlt.$) { rlt.$ = {}; }
  		rlt.$.base = base;
  		return rlt;
  	}
	, RebindAttr: function (el) {
		for (var i = 0; i < el.attributes.length; i++) {
			var item = el.attributes[i];
			//alert(item.name + "=" + item.value);
			if (item.name.indexOf('_') == 0) {
				var name = item.name.substr(1, item.name.length - 1);
				el[name] = item.value;
				alert(name + "=" + item.value);
			}
		}
	}
  	, Dict: function () {
  		var dict = {
  			_list: {}
  			, add: function (key, val) {
  				if (!dict._list[key]) {
  					dict._list[key] = new Array();
  				}
  				dict._list[key].push(val);
  			}
  			, replace: function (key, val) {
  				dict._list[key] = null;
  				dict._list[key] = new Array();
  				dict._list[key].push(val);
  			}
  			, toUrlString: function (forceEncode) {
  				var lst = dict._list;
  				var rlt = "";
  				forceEncode = true;
  				for (var p in lst) {
  					for (var i = 0; i < lst[p].length; i++) {
  						if (forceEncode) {
  							rlt += p + "=" + escape(lst[p][i]) + "&";
  						} else {
  							rlt += p + "=" + lst[p][i] + "&";
  						}
  					}
  				}
  				rlt = rlt.substr(0, rlt.length - 1);
  				return rlt;
  			}
  			, toSpecifiedString: function (kvSplitter, vl, vr, itemSplitter, groupSplitter, forceEncode) {
  				var lst = dict._list;
  				var rlt = "";
  				forceEncode = true;
  				for (var p in lst) {
  					for (var i = 0; i < lst[p].length; i++) {
  						if (forceEncode) {
  							rlt += p + kvSplitter + vl + escape(lst[p][i]) + vr + itemSplitter;
  						} else {
  							rlt += p + kvSplitter + vl + lst[p][i] + vr + itemSplitter;
  						}
  					}
  					rlt = rlt.substr(0, rlt.length - itemSplitter.length);
  					rlt += groupSplitter;
  				}
  				rlt = rlt.substr(0, rlt.length - groupSplitter.length);
  				return rlt;
  			}
  		}
  		return dict;
  	}
  	, Base64: {

  		// private property
  		_keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

  		// public method for encoding
  		encode: function (input) {
  			var output = "";
  			var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
  			var i = 0;

  			input = Base64._utf8_encode(input);

  			while (i < input.length) {

  				chr1 = input.charCodeAt(i++);
  				chr2 = input.charCodeAt(i++);
  				chr3 = input.charCodeAt(i++);

  				enc1 = chr1 >> 2;
  				enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
  				enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
  				enc4 = chr3 & 63;

  				if (isNaN(chr2)) {
  					enc3 = enc4 = 64;
  				} else if (isNaN(chr3)) {
  					enc4 = 64;
  				}

  				output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

  			}

  			return output;
  		},

  		// public method for decoding
  		decode: function (input) {
  			var output = "";
  			var chr1, chr2, chr3;
  			var enc1, enc2, enc3, enc4;
  			var i = 0;

  			input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

  			while (i < input.length) {

  				enc1 = this._keyStr.indexOf(input.charAt(i++));
  				enc2 = this._keyStr.indexOf(input.charAt(i++));
  				enc3 = this._keyStr.indexOf(input.charAt(i++));
  				enc4 = this._keyStr.indexOf(input.charAt(i++));

  				chr1 = (enc1 << 2) | (enc2 >> 4);
  				chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
  				chr3 = ((enc3 & 3) << 6) | enc4;

  				output = output + String.fromCharCode(chr1);

  				if (enc3 != 64) {
  					output = output + String.fromCharCode(chr2);
  				}
  				if (enc4 != 64) {
  					output = output + String.fromCharCode(chr3);
  				}

  			}

  			output = Base64._utf8_decode(output);

  			return output;

  		},

  		// private method for UTF-8 encoding
  		_utf8_encode: function (string) {
  			string = string.replace(/\r\n/g, "\n");
  			var utftext = "";

  			for (var n = 0; n < string.length; n++) {

  				var c = string.charCodeAt(n);

  				if (c < 128) {
  					utftext += String.fromCharCode(c);
  				}
  				else if ((c > 127) && (c < 2048)) {
  					utftext += String.fromCharCode((c >> 6) | 192);
  					utftext += String.fromCharCode((c & 63) | 128);
  				}
  				else {
  					utftext += String.fromCharCode((c >> 12) | 224);
  					utftext += String.fromCharCode(((c >> 6) & 63) | 128);
  					utftext += String.fromCharCode((c & 63) | 128);
  				}

  			}

  			return utftext;
  		},

  		// private method for UTF-8 decoding
  		_utf8_decode: function (utftext) {
  			var string = "";
  			var i = 0;
  			var c = c1 = c2 = 0;

  			while (i < utftext.length) {

  				c = utftext.charCodeAt(i);

  				if (c < 128) {
  					string += String.fromCharCode(c);
  					i++;
  				}
  				else if ((c > 191) && (c < 224)) {
  					c2 = utftext.charCodeAt(i + 1);
  					string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
  					i += 2;
  				}
  				else {
  					c2 = utftext.charCodeAt(i + 1);
  					c3 = utftext.charCodeAt(i + 2);
  					string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
  					i += 3;
  				}

  			}

  			return string;
  		}

  	}
	, LoginModule: function ($) {
		var rlt = {
			$: $
			, HashPwd: function (upwd) {
				return "--" + upwd + "--";
			}
			, Encode: function (uname, upwd) {
				var ticket = FC.getCookie("_ak_ticket_");
				//alert(ticket);
				return uname + '_' + this.HashPwd(upwd) + '_' + ticket;
			}
			, signIn: function () {
				//FC.RebindAttr(par);
				var par = this.$;
				var target = par.processor;
				var uname = FC.G(par.elementIds.usernameBox).value;
				var upwd = FC.G(par.elementIds.passwordBox).value;
				var code = FC.G(par.elementIds.authCodeBox).value;
				FC.setCookie("Cooked", this.Encode(uname, upwd));
				FC.QueuedRemoteCall.add({
					CallBack: function (o, success) {
						if (!success) {
							alert(o);
						} else {
							if (par.sredirect == '*') {
								par.sredirect = par.previousurl;
							}
							window.open(par.sredirect, '_top');
						}
					}
				, method: "SignIn"
				, param: {
					user: uname
					, hash: this.Encode(uname, upwd)
					, remember: FC.G(par.elementIds.rememberCheckBox).checked
					, code: code
				}
				, target: target
				});
				FC.QueuedRemoteCall.send();
			}
		};
		//alert($.elementIds.authImg);
		var authImg = FC.G($.elementIds.authImg);
		//alert(authImg);
		authImg.src = $.authimgurl;
		return rlt;
	}
};
  if (FC$) {
  	FC.$ = FC$;
  }
  document.logmsg = new Array();
