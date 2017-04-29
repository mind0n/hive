function j(clues) {
	return J.Select(clues);
}
var J = {
	ScriptReadyCallback: null
	, parseJson: function (json) {
		//json = json.replace(/\n/g, '');
		try {
			var rlt = eval('(' + json + ')');
			return rlt;
		} catch (e) {
			J.log('Invalid JSON format:' + json);
			return null;
		}
	}
	, Extend: function (source, extended) {
		var o = new source();
		var e = extended;
		for (var i in e) {
			o[i] = e[i];
		}
		return function () {
			return o;
		}
	}
	, EnumChildElements: function (parent, recursive, callback) {
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
	}
	, EnumParentElements: function (target, callback) {
		var elems = new Array();
		while (target) {
			elems.push(target);
			if (callback) {
				callback(target.parent);
				target = target.parent;
			}
		}
		return elems;
	}
	, GetEvent: function (evt, obj) {
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
		//J.Log(evt.clientY + ';' + rlt.y + '|' + obj.y);
		if (obj) {
			for (var i in rlt) {
				obj[i] = rlt[i];
			}
		}
		return rlt;
	}
	, GetElementById: function (id) {
		return document.getElementById(id);
	}
	, Split: function (content, splitters, includeSplitter) {
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
	}
	, JudgeElement: function (condition, el) {
		var rlts = J.Split(condition, new Array('#', '.'), true);
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
	}
	, GetElementsByStyle: function (style, parent) {
		var sep;
		var recusive;
		//		if (style.indexOf('>') > 0) {
		//			sep = '>';
		//			recusive = false;
		//		} else {
		//			sep = ' ';
		//			recusive = true;
		//		}
		var units = J.Split(style, new Array('>', ' '), true); // style.split(sep);
		units[0] = ' ' + units[0];
		var els;
		if (!parent) {
			els = J.EnumChildElements(parent, true);
		} else {
			els = J.EnumChildElements(parent, false);
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
							selected = J.JudgeElement(unit, el);
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
							rlts = rlts.concat(J.EnumChildElements(selected, recusive));
						}
					}
				}
				els = rlts;
			}
		}
		return rlts;
	}
	, Invoke: function (methods, params) {
		if (methods && methods.length > 0) {
			for (var i = 0; i < methods.length; i++) {
				methods[i](params);
			}
		}
	}
	, EventQueue: function () {
		var rlt = {
			List: new Array(3, 2, 1)
		};
		return rlt;
	}
	, ScriptReady: function (callback) {
		if (document.body == null) {
			if (J.ScriptReadyCallback == null) {
				this.ScriptReadyCallback = callback;
			}
			J.$DefaultTimer = window.setTimeout("J.ScriptReady();", 1000);
		} else {
			window.clearTimeout(J.$DefaultTimer);
			if (this.ScriptReadyCallback != null) {
				document.onmousemove = J.MouseController.OnMouseMove;
				document.onmousedown = J.MouseController.OnMouseDown;
				document.onmouseup = J.MouseController.OnMouseUp;
				window.onscroll = J.MouseController.OnMouseMove;
				document.documentElement.on
				document.body.style.background = "silver";
				this.ScriptReadyCallback();
			}
		}
	}
};
J.Dict = function() {
	var o = {
		vhost: {}
		, Add: function(methodName, method) {
			this.vhost[methodName] = {
				name: methodName
				, callback: method
			}
			return methodName;
		}
		, Remove: function(key) {
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
J.EventHandler = function() {
	var o = {
		vcount: 0
		, vhost: {}
		, Add: function(method) {
			var methodName = "method" + this.vcount;
			this.vhost[methodName] = {
				name: methodName
				, callback: method
			}
			this.vcount++;
			return methodName;
		}
		, Remove: function(method) {
			for (var name in this.vhost) {
				if (this.vhost[name] != null && this.vhost[name].callback == method) {
					this.vhost[name] = null;
				}
			}
		}
		, Invoke: function(param) {
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
J.Select = function(query) {
	var rlt = cssQuery(query);
	if (rlt.length > 0) {
		return rlt[0];
	}
	return rlt;
};

