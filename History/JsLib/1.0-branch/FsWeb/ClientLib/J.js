var pageConfig = {
	data: {}
};
var JLib_v1 = function (target) {
	if (target) {
		if (typeof (target) == 'string')
			return JLib_v1.Select(target);
		else
			return target;
	}
};
JLib_v1.ScriptReadyCallback = null;
JLib_v1.parseJson = function (json) {
	try {
		var rlt = eval('(' + json + ')');
		return rlt;
	} catch (e) {
		JLib_v1.log('Invalid JSON format:' + json);
		return null;
	}
};
JLib_v1.Extend = function (source, extended) {
	var o = new source();
	var e = extended;
	for (var i in e) {
		o[i] = e[i];
	}
	return function () {
		return o;
	}
};
JLib_v1.EnumChildElements = function (parent, recursive, callback) {
	var elems, rlts = new Array();
	if (!parent) {
		parent = document.body;
	}
	if (typeof (parent) == "string") {
		parent = document.getElementById(parent);
	}
	if (!recursive) {
		elems = parent.childNodes;
	} else {
		elems = parent.getElementsByTagName('*');
	}
	for (var i = 0; i < elems.length; i++) {
		var el = elems[i];
		if (el.tagName != '!' && el.tagName != 'STYLE' && el.tagName != 'SCRIPT') {
			rlts.push(el);
			if (callback) {
				callback(el);
			}
		}
	}
	return rlts;
};
JLib_v1.EnumParentElements = function (target, callback) {
	var elems = new Array();
	while (target) {
		elems.push(target);
		if (callback) {
			callback(target.parent);
			target = target.parent;
		}
	}
	return elems;
};
JLib_v1.GetEvent = function (evt, obj) {
	var evt = evt || event;
	var target = evt.target || event.srcElement;
	var rlt = {};
	if (evt.clientX) {
		rlt.x = evt.clientX;
		rlt.ax = rlt.x + document.documentElement.scrollLeft;
	} else {
		rlt.ax = obj.x + document.documentElement.scrollLeft;
	}
	if (evt.clientY) {
		rlt.y = evt.clientY;
		rlt.ay = rlt.y + document.documentElement.scrollTop;
	} else {
		rlt.ay = obj.y + document.documentElement.scrollTop;
	}
	if (target) {
		rlt.target = target;
	}
	//JLib_v1.Log(evt.clientY + ';' + rlt.y + '|' + obj.y);
	if (obj) {
		for (var i in rlt) {
			obj[i] = rlt[i];
		}
	}
	return rlt;
};
JLib_v1.GetElementById = function (id) {
	return document.getElementById(id);
};
JLib_v1.Split = function (content, splitters, includeSplitter) {
	var rlts = new Array();
	var unit = '';
	var pos = new Array();
	var curtpos = 0;
	while (true) {
		for (var i = 0; i < splitters.length; i++) {
			var splitter = splitters[i];
			var n = content.lastIndexOf(splitter);
			if (n >= 0) {
				pos.push(n);
			}
		}
		var max = -1;
		for (var i = 0; i < pos.length; i++) {
			max = Math.max(max, pos[i]);
		}
		if (max < 0 || pos.length <= 0) {
			if (content.length > 0) {
				rlts.push(content);
			}
			break;
		} else {
			if (includeSplitter) {
				rlts.push(content.substr(max, content.length - max));
			} else {
				rlts.push(content.substr(max + 1, content.length - max - 1));
			}
			content = content.substr(0, max);
			pos = new Array();
		}
	}
	return rlts.reverse();
};
JLib_v1.JudgeElement = function (condition, el) {
	var rlts = JLib_v1.Split(condition, new Array('#', '.'), true);
	if (!rlts || rlts.length == 0) {
		return null;
	}
	for (var i = 0; i < rlts.length; i++) {
		var unit = rlts[i];
		if (unit[0] == '#' && '#' + el.id != unit) {
			return null;
		} else if (unit[0] == '.') {
			var className = unit.substr(1, unit.length - 1);
			if (!el.className) { return null }
			else if (el.className.indexOf(className) < 0) { return null; }
		} else {
			if (unit[0] != '#' && el.tagName != unit) {
				return null;
			}
		}
	}
	return el;
};
JLib_v1.GetElementsByStyle = function (style, parent) {
	var sep;
	var recusive;
	var units = JLib_v1.Split(style, new Array('>', ' '), true); // style.split(sep);
	units[0] = ' ' + units[0];
	var els;
	if (!parent) {
		els = JLib_v1.EnumChildElements(parent, true);
	} else {
		els = JLib_v1.EnumChildElements(parent, false);
	}
	var rlts
	for (var i = 0; i < units.length; i++) {
		var unit = units[i];
		if (unit[0] == '>') {
			recusive = false;
		} else {
			recusive = true;
		}
		unit = unit.substr(1, unit.length - 1);
		if (unit && unit.length > 0) {
			rlts = new Array();
			for (var j = 0; j < els.length; j++) {
				var el = els[j];
				var selected = null;
				if (el) {
					if (unit[0] == '.' && '.' + el.className == unit) {
						selected = el;
					} else if (unit[0] == '#' && '#' + el.id == unit) {
						selected = el;
					} else if (el.tagName && unit.toLowerCase() == el.tagName.toLowerCase()) {
						selected = el;
					} else {
						selected = JLib_v1.JudgeElement(unit, el);
					}
				}
				if (selected) {
					if (i + 1 == units.length) {
						var unique = true;
						for (var d = 0; d < rlts.length; d++) {
							if (rlts[d] == selected) {
								unique = false;
								break;
							}
						}
						if (unique) {
							rlts.push(selected);
						}
					} else {
						rlts = rlts.concat(JLib_v1.EnumChildElements(selected, recusive));
					}
				}
			}
			els = rlts;
		}
	}
	return rlts;
};
JLib_v1.Invoke = function (methods, params) {
	if (methods && methods.length > 0) {
		for (var i = 0; i < methods.length; i++) {
			methods[i](params);
		}
	}
};
JLib_v1.EventQueue = function () {
	var rlt = {
		List: new Array(3, 2, 1)
	};
	return rlt;
};
JLib_v1.ScriptReady = function (callback) {
	if (document.body) {
		window.clearTimeout(JLib_v1.$DefaultTimer);
		document.onmousemove = JLib_v1.MouseController.OnMouseMove;
		document.onmousedown = JLib_v1.MouseController.OnMouseDown;
		document.onmouseup = JLib_v1.MouseController.OnMouseUp;
		window.onscroll = JLib_v1.MouseController.OnMouseMove;
		document.documentElement.on
		document.body.style.background = "silver";
		if (callback) {
			callback();
		}
		//		if (JLib_v1.ScriptReadyCallback == null) {
		//			this.ScriptReadyCallback = callback;
		//		}
	} else {
		JLib_v1.$DefaultTimer = window.setTimeout(function () {
			JLib_v1.ScriptReady(callback);
		}, 1000);
	}
};
var J = JLib_v1;

JLib_v1.Dict = function () {
	var o = {
		vhost: {}
		, Add: function (methodName, method) {
			this.vhost[methodName] = {
				name: methodName
				, callback: method
			}
			return methodName;
		}
		, Remove: function (key) {
			for (var name in this.vhost) {
				if (key == name) {
					delete this.vhost[name];
					//this.vhost[name] = null;
				}
			}
		}
	};
	return o;
};
JLib_v1.EventHandler = function () {
	var o = {
		vcount: 0
		, vhost: {}
		, Add: function (method) {
			var methodName = "method" + this.vcount;
			this.vhost[methodName] = {
				name: methodName
				, callback: method
			}
			this.vcount++;
			return methodName;
		}
		, Remove: function (method) {
			for (var name in this.vhost) {
				if (this.vhost[name] != null && this.vhost[name].callback == method) {
					this.vhost[name] = null;
				}
			}
		}
		, Invoke: function (param) {
			for (var name in this.vhost) {
				if (this.vhost[name] != null) {
					var unit = this.vhost[name];
					unit.callback(param);
				}
			}
		}
	};
	return o;
};
JLib_v1.Select = function (query) {
	var rlt = cssQuery(query);
	if (rlt.length > 0) {
		return rlt[0];
	}
	return rlt;
};

