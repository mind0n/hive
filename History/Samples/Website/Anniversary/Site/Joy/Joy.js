var Joy = function (id) {
	if (id) {
		return document.getElementById(id);
	}
	return null;
};

Joy.id = function () {
	if (!Joy._id) {
		Joy._id = 0;
	}
	return ++Joy._id;
};

Joy.controls = {};

Joy.readyTimeout = null;

Joy.mouse = {
	listeners: {
		onmove: {},
		onup: {},
		ondown: {},
		ondrag: {}
	},
	fireEvent: function (name) {
		if (name) {
			var list = this.listeners[name];
			for (var i in list) {
				if (list[i]) {
					//Joy.log(name);
					list[i].dragging[name](list[i]);
				}
			}
		}
	},
	removeListener: function (el) {
		if (el) {
			if (el.dragging.onupid) {
				//Joy.mouse.listeners.onup[el.dragging.onupid] = null;
				delete Joy.mouse.listeners.onup[el.dragging.onupid];
			}
			if (el.dragging.onmoveid) {
				//Joy.mouse.listeners.onmove[el.dragging.onmoveid] = null;
				delete Joy.mouse.listeners.onmove[el.dragging.onmoveid];
			}
			if (el.dragging.ondragid) {
				//Joy.mouse.listeners.ondrag[el.dragging.ondragid] = null;
				delete Joy.mouse.listeners.ondrag[el.dragging.ondragid];
			}
		}
	},
	drag: function (el, cfg) {
		el.dragging = cfg;
		if (cfg.onmove) {
			el.dragging.onmoveid = el.jid;
			Joy.mouse.listeners.onmove[el.dragging.onmoveid] = el;
		}
		if (cfg.onup) {
			el.dragging.onupid = el.jid;
			Joy.mouse.listeners.onup[el.dragging.onupid] = el;
		}
		if (cfg.ondrag) {
			el.dragging.ondragid = el.jid;
			Joy.mouse.listeners.ondrag[el.dragging.ondragid] = el;
		}
	}
};

Joy.c = function (tag) {
	return el = document.createElement(tag);
};
Joy.onready = function (callback) {
	if (!Joy.onready.$) {
		Joy.onready.$ = [];
	}
	Joy.onready.$.push(callback);
};
var isPageReady = false;
Joy.ready = function (callback) {
	if (document.body && isPageReady) {
		function getInternetExplorerVersion() {
			var rv = -1; // Return value assumes failure.
			if (navigator.appName == 'Microsoft Internet Explorer') {
				var ua = navigator.userAgent;
				var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
				if (re.exec(ua) != null)
					rv = parseFloat(RegExp.$1);
			}
			return rv;
		}

		if (!Joy.isInited) {
			Joy.isInited = true;
			var browser = {};
			var ua = navigator.userAgent.toLowerCase();
			if (navigator.userAgent.indexOf("MSIE") > 0) {
				browser.ie = true;
				var ver = getInternetExplorerVersion();
				if (ver > 0) {
					browser['ie' + ver] = true;
				}
			}
			if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
				browser.firefox = true;
			}
			if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {
				browser.safari = true;
			}
			if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
				browser.camino = true;
			}
			if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) {
				browser.gecko = true;
			}
			this.browser = browser;
			document.body.style.height = window.screen.height + 'px';
			document.body.onmousemove = function (evt) {
				var evt = evt || event;
				Joy.mouse.prevX = Joy.mouse.x;
				Joy.mouse.prevY = Joy.mouse.y;
				Joy.mouse.x = evt.clientX;
				Joy.mouse.y = evt.clientY;
				Joy.mouse.mousemove = true;
				if (Joy.mouse.mousedown) {
					Joy.mouse.dragging = true;
				}
				if (Joy.mouse.dragging) {
					Joy.mouse.fireEvent('ondrag');
				} else {
					Joy.mouse.fireEvent('onmove');
				}
			};
			document.body.onmousedown = function (evt) {
				if (!Joy.mouse.mousedown) {
					Joy.mouse.startpos = { x: Joy.mouse.x, y: Joy.mouse.y };
				}
				Joy.mouse.mousedown = true;
				Joy.mouse.fireEvent('ondown');
			};
			document.body.onmouseup = function (evt) {
				Joy.mouse.mousedown = false;
				Joy.mouse.endpos = { x: Joy.mouse.x, y: Joy.mouse.y };
				Joy.mouse.dragging = false;
				Joy.mouse.fireEvent('onup');
			};
		}
		callback(false);
		if (Joy.onready.$) {
			var list = Joy.onready.$;
			Joy.onready.$ = null;
			for (var i = 0; i < list.length; i++) {
				var func = list[i];
				func();
			}
		}
		callback(true);
	} else {
		window.setTimeout(function () {
			Joy.ready(callback);
		}, 200);
	}
};

Joy.extend = function (base, ext) {
    if (ext && base) {
        ext.prototype = new base();
        return ext;
    }
    return base;
};

Joy.merge = function (obj, ext, toplevelonly) {
	if (!obj) {
		return;
	}
	if (!toplevelonly) {
		if (ext) {
			if (ext instanceof Array && obj instanceof Array) {
				for (var i = 0; i < ext.length; i++) {
					obj[obj.length] = ext[i];
				}
			} else {
				for (var i in ext) {
					var etype = typeof (ext[i]);
					var otype = typeof (obj[i]);
					if (obj[i] && ext[i] && otype == 'object' && etype == 'object') {
						Joy.merge(obj[i], ext[i]);
					} else if (etype == 'object') {
						if (ext[i] instanceof Array) {
							obj[i] = [];
						} else {
							obj[i] = {};
						}
						Joy.merge(obj[i], ext[i]);
					} else {
						obj[i] = ext[i];
					}
				}
			}
		} else {
			for (var i in obj) {
				obj[i] = null;
			}
		}
	} else {
		if (ext) {
			for (var i in ext) {
				var otype = typeof (obj[i]);
				var etype = typeof (ext[i]);
				if (etype != 'object') {
					obj[i] = ext[i];
				}
			}
		}
	}
	return obj;
};

Joy.parse = function (elementJson) {
	return this.render(document.createElement('div'), elementJson);
};

Joy.actualSize = function (el) {
	return el.getBoundingClientRect();
};

Joy.resize = function(el, targetEl, offset){
	var bound = this.actualSize(targetEl);
	el.style.width = (bound.right - bound.left - offset) + 'px';
	el.style.height = (bound.bottom - bound.top - offset) + 'px';
};

Joy.make = function (obj, parentEl, rootEl) {
	if (!obj) {
		return null;
	}
	var curtEl;
	if (typeof (obj) == 'string') {
		if (Joy.controls[obj]) {
			obj = Joy.controls[obj];
			curtEl = document.createElement(obj.tagName);
		} else {
			curtEl = document.createElement(obj);
		}
	} else if (obj.controlName) {
		curtEl = Joy.make(Joy.controls[obj.controlName]);
	} else {
		curtEl = document.createElement(obj.tagName);
	}
	if (!obj) {
		throw ('Instance missing error.');
	}
	if (!rootEl) {
		rootEl = curtEl;
	} else {
		curtEl.$root = rootEl;
	}
	for (var p in obj) {
		var type = typeof (obj[p]);
		if (p != 'tagName') {
			if (p == '$') {
				if (type == 'object') {
					var list = obj[p];
					for (var i = 0; i < list.length; i++) {
						this.make(list[i], curtEl, rootEl);
					}
				} else {
					curtEl.innerHTML = obj[p];
				}
			} else if (p == 'alias') {
				rootEl['$' + obj[p]] = curtEl;
			} else {
				if (type == 'object') {
					if (!curtEl[p]) {
						curtEl[p] = {};
					}
					this.makeObject(obj[p], curtEl[p]);
				} else {
					curtEl[p] = obj[p];
				}
			}
		}
	}
	curtEl.rect = function () {
		var rect = this.getBoundingClientRect();
		return { top: rect.top, left: rect.left, bottom: rect.bottom, right: rect.right, width: rect.right - rect.left, height: rect.bottom - rect.top };
	}
	if (parentEl) {
		parentEl.appendChild(curtEl);
	}
	if (curtEl.onmade) {
		curtEl.onmade();
	}
	return curtEl;
};

Joy.makeObject = function (style, parent) {
    for (var p in style) {
        if (typeof (style[p]) == 'object') {
            this.makeObject(style[p], parent[p]);
        } else {
            parent[p] = style[p];
        }
    }
};

Joy.render = function (parentEl, obj) {
	var el = this.make(obj);
	if (parentEl) {
		parentEl.appendChild(el);
	}
};

Joy.xml2obj = function (xml) {
};

Joy.obj2xml = function (obj, tagName, isChild) {
	var tag = tagName || obj.tagName;
	var attr = [];
	var attrstr = '';
	var inner = '';
	if (!tag) {
		tag = "x";
	}
	for (var i in obj) {
		var type = typeof (obj[i]);
		if (i == '$') {
			if (obj[i] instanceof Array) {
				var list = obj[i];
				for (var j = 0; j < list.length; j++) {
					inner += this.obj2xml(list[j], null, true);
				}
			} else if (type != 'object') {
				inner = obj[i];
			}
		} else if (type == 'object') {
			if (obj instanceof Array) {
				inner += this.obj2xml(obj[i], Joy.xml.makeSubItemName(tag), true);
			} else {
				inner += this.obj2xml(obj[i], i, true);
			}
		} else {
			if (obj instanceof Array) {
				inner += '<' + Joy.xml.makeSubItemName(tag) + '>' + obj[i] + '</' + Joy.xml.makeSubItemName(tag) + '>';
			} else {
				attr.push(' ' + i + '="' + obj[i] + '" ');
			}
		}
	}

	if (attr.length > 0) {
		for (var i = 0; i < attr.length; i++) {
			attrstr += attr[i];
		}
	} else {
		attrstr = '';
	}

	var rlt = '<' + tag + attrstr;
	if (inner.length < 1) {
		rlt += ' />';
	} else {
		rlt += '>' + inner + '</' + tag + '>';
	}
	if (isChild) {
		return rlt;
	} else {
		return Joy.xml.xmlAddTitle(rlt).content;
	}
};

Joy.xml = function (content, callback) {
    var doc, tokens, isXmlStr;
    function handleXML() {
        nodes = doc.documentElement;
        if (callback) {
            if (document.all) {
                if (doc.readyState == 4) {
                    callback(nodes, doc);
                }
            } else {
                callback(nodes, doc);
            }
        }
    }

    var rlt = xmlAddTitle(content);
    content = rlt.content;
    isXmlStr = rlt.isXmlStr;

    if (document.implementation && document.implementation.createDocument) {
        doc = document.implementation.createDocument("", "", null);
        if (content) {
            doc.onload = handleXML;
        }
        if (isXmlStr) {
            var parser = new DOMParser();
            doc = parser.parseFromString(content, 'text/xml');
        }
    }
    else if (window.ActiveXObject) {
        doc = new ActiveXObject('Microsoft.XMLDOM');
        if (content) {
            doc.onreadystatechange = handleXML;
        }
        if (isXmlStr) {
            doc.loadXML(content);
        }
    }
    if (!isXmlStr && content) {
        var isAsync = (callback != null) ? true : false;
        doc.async = isAsync;
        doc.load(content);
    }
    var rlt;
    if (doc && doc.childNodes && doc.childNodes.length > 1) {
        rlt = doc.childNodes[1];
    } else if (doc && doc.childNodes && doc.childNodes.length == 1) {
        rlt = doc.childNodes[0];
    } else {
        rlt = doc;
    }
    return rlt;
};
Joy.xml.makeSubItemName = function (name) {
	if (name){
		if (name.charAt(name.length - 1) == 's') {
			return name.substr(0, name.length - 1);
		} else if (name.indexOf('Collection') == (name.length - 'Collection'.length)) {
			return name.substr(0, name.length - 'Collection'.length);
		} else {
			return name + 'Item';
		}
	}
};
Joy.xml.xmlText = function (node) {
	if (node.text) {
   		return node.text;
	} else {
   		return node.textContent;
	}
};
Joy.xml.xmlValue = function (node) {
	if (node.xml) {
   		return node.xml;
	} else {
   		return new XMLSerializer().serializeToString(node);
	}
};

Joy.xml.xmlAddTitle = function (content) {
	var isXmlStr = false;
	var title = '<?xml version="1.0" encoding="utf-8" ?>\r\n';

	if (content) {
		tokens = content.substr(0, 5);
		if (tokens) {
			tokens = tokens.toLowerCase();
			if (tokens.indexOf('<') == 0) {
				if (tokens != '<?xml') {
					content = title + content;
				}
				isXmlStr = true;
			}
		}

	}
	return { content: content, isXmlStr: isXmlStr };
};

Joy.xml.xmlSafeString = function (content, isAttribute) {
	var rlt = content;
	rlt = rlt.replace(/\</g, '&lt;');
	rlt = rlt.replace(/\"/g, '&quot;');
	return rlt;
};
Joy.destroyer = document.createElement('div');
Joy.destroy = function (el) {
	if (el && el.tagName) {
		this.destroyer.appendChild(el);
		this.destroyer.innerHTML = '';
	}
};
Joy.log = function (info, hideLog) {
	var el = document.body.logEl;
	if (!el) {
		document.body.logEl = Joy.c('textarea');
		el = document.body.logEl;
		el.style.position = 'fixed';
		el.style.width = '800px';
		el.style.height = '400px';
		el.style.bottom = '100px';
		el.style.left = '100px';
		el.style.zIndex = 1000;
		el.style.border = 'solid 2px green';
		el.isbig = true;
		el.ondblclick = function (evt) {
			if (this.isbig) {
				this.isbig = false;
				this.style.width = '2px';
				this.style.height = '2px';
			} else {
				this.isbig = true;
				this.style.width = '800px';
				this.style.height = '400px';
			}
		}
		document.body.appendChild(el);
	}

	if (!hideLog) {
		el.style.display = '';
	} else {
		el.style.display = 'none';
	}

	if (info && !hideLog) {
		var type = typeof (info);
		if (type instanceof Array) {

		} else if (type == 'object') {
			for (var i in info) {
				el.value = i + '=' + info[i] + '\r\n' + el.value;
			}
		} else if (type == 'function') {
			//do nothing
		} else {
			el.value = info + "\r\n" + el.value;
		}
	}
};
Joy.sleep = function (nMillis)
{
	var dt1 = new Date();
	for (; ; )
	{
		var dt2 = new Date();
		if ((dt2.getTime() - dt1.getTime()) >= nMillis)
			break;
	}
};
Joy.json = function (p) {
	var type = typeof (p);
	function serializeArray(a) {
		var list = new Array();
		for (var i = 0; i < a.length; i++) {
			var o = a[i];
			var rlt = toJson(o);
			list.push(rlt);
		}
		return "[" + list.toString() + "]";
	}
	function serializeObject(o) {
		var list = new Array();
		for (var i in o) {
			var p = o[i];
			var rlt = toJson(p);
			list.push(i + ':' + rlt);
		}
		return '{' + list.toString() + '}';
	}
	function serializeValue(v) {
		var type = typeof (v);
		if (type == 'string') {
			return '"' + v + '"';
		} else {
			return v;
		}
	}
	function toJson(p) {
		var type = typeof (p);
		if (p instanceof Array) {
			return serializeArray(p);
		} else if (type == 'object') {
			return serializeObject(p);
		} else if (type == 'function') {
			return p.toString();
		} else {
			return serializeValue(p);
		}
		return '';
	}
	if (type == 'string') {

		var rlt = null;
		try {
			rlt = eval('(' + p + ')');
		} catch (e) {
			rlt = null;
		}
		return rlt;
	} else {
		var rlt = toJson(p);
		return rlt;
	}
};
Joy.cookie = function (id, value, forceReload)
{
	var s = '';
	var rlt;
	if (forceReload || !document.body.cookie)
	{
		var list = document.cookie.split(';');
		rlt = {};
		for (i = 0; i < list.length; i++)
		{
			x = list[i].substr(0, list[i].indexOf("="));
			y = list[i].substr(list[i].indexOf("=") + 1);
			x = x.replace(/^\s+|\s+$/g, "");
			rlt[x] = y;
		}
		document.body.cookie = rlt;
	} else
	{
		rlt = document.body.cookie;
	}
	if (value && !id)
	{
		delete rlt[value];
	} else if (id && value)
	{
		rlt[id] = value;
	} else if (id && !value)
	{
		return unescape(rlt[id].replace(/\+/g, ' '));
	}
	for (var i in rlt)
	{
		s += i + '=' + escape(rlt[i]) + ';';
	}
	document.cookie = s;
	return s;
};
Joy.dateDiff = function(dt, df){
	if (!df){
		df = new Date();
	}
	var millisecondsPerDay = 1000 * 60 * 60 * 24;
	return Math.floor((dt - df)/millisecondsPerDay);
};
Joy.dateString = function (d, format) {
	if (!format) {
		return d.toLocaleString();
	}
	var r = format;
	r = r.replace(/yyyy/, d.getFullYear());
	r = r.replace(/MM/, d.getMonth() + 1);
	r = r.replace(/dd/, d.getDay());
	r = r.replace(/HH/, d.getHours());
	r = r.replace(/mm/, d.getMinutes());
	r = r.replace(/ss/, d.getSeconds());
	return r;
};
Joy.addClass = function (el, name) {
	var classes = el.className.split(' ');
	for (var i = 0; i < classes.length; i++) {
		var cname = classes[i];
		if (cname == name) {
			return;
		}
	}
	el.className += ' ' + name;
}
Joy.delClass = function (el, name) {
	var classes = el.className.split(' ');
	var rlt = '';
	for (var i = 0; i < classes.length; i++) {
		var cname = classes[i];
		if (cname != ' ' && cname != '' && cname != name) {
			rlt += cname + ' ';
		}
	}
	el.className = rlt;
}