
Element.prototype.astyle = function actualStyle(props) {
	var el = this;
	var compStyle = window.getComputedStyle(el, null);
	for (var i = 0; i < props.length; i++) {
		var style = compStyle[props[i]];
		if (style != null) {
			return style;
		}
	}
	return null;
};

function destroy(element) {
	if (element.dispose && !element.$disposing$) {
		element.$disposing$ = true;
		element.dispose();
	}
	var destroyer = document.body.$destroyer$;
	if (!destroyer) {
		destroyer = document.createElement('div');
		document.body.$destroyer$ = destroyer;
	}
	destroyer.appendChild(element);
	element.destroying = true;
	for (var i in element) {
		if (i.startsWith('$', 0) && i.indexOf('$root') < 0 && i.indexOf('$parent') < 0 && i.indexOf('$group') < 0) {
			if (i == '$scene$' || i == '$assist$') {
				debugger;
			}
			var item = element[i];
			if (item && item.tagName) {
				var ch = element[i];
				if (!ch.destroying) {
					destroy(ch);
				}
			}
			delete item;
		}
	}
	element.destroying = false;
	destroyer.innerHTML = '';
}
// Opera 8.0+
var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
// Firefox 1.0+
var isFirefox = typeof InstallTrigger !== 'undefined';
// At least Safari 3+: "[object HTMLElementConstructor]"
var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
// Internet Explorer 6-11
var isIE = /*@cc_on!@*/false || !!document.documentMode;
// Edge 20+
var isEdge = !isIE && !!window.StyleMedia;
// Chrome 1+
var isChrome = !!window.chrome && !!window.chrome.webstore;
// Blink engine detection
var isBlink = (isChrome || isOpera) && !!window.CSS;
var isMobile = {
	Android: function () {
		var r = navigator.userAgent.match(/Android/i);
		if (r) {
			console.log('match Android');
		}
		return r;
	},
	BlackBerry: function () {
		var r = navigator.userAgent.match(/BlackBerry/i);
		if (r) {
			console.log('match Android');
		}
		return r;
	},
	iOS: function () {
		var r = navigator.userAgent.match(/iPhone|iPad|iPod/i);
		if (r) {
			console.log('match Android');
		}
		return r;
	},
	Opera: function () {
		var r = navigator.userAgent.match(/Opera Mini/i);
		if (r) {
			console.log('match Android');
		}
		return r;
	},
	Windows: function () {
		var r = navigator.userAgent.match(/IEMobile/i);
		if (r) {
			console.log('match Android');
		}
		return r;
	},
	any: function () {
		return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
	}
};
String.prototype.format = function () {
	var args = arguments;
	var s = this;
	if (!args || args.length < 1) {
		return s;
	}
	var r = s;
	for (var i = 0; i < args.length; i++) {
		var reg = new RegExp('\\{' + i + '\\}');
		r = r.replace(reg, args[i]);
	}
	return r;
};
window.simplecopy = function (from, target) {
	var r = target || {};
	var self = from;
	for (var i in self) {
		try {
			r[i] = self[i];
		} catch (e) {
			console.log(e);
		}
	}
	return r;
}
window.spawn = function (o) {
	var r = {};
	if (o instanceof Array || o.length) {
		r = [];
		for (var i = 0; i < o.length; i++) {
			r[r.length] = o[i];
		}
	}
	for (var i in o) {
		if (!r[i] && o[i]) {
			r[i] = o[i];
		}
	}
	return r;
};
Date.prototype.pattern = function (fmt) {
	var o = {
		"M+": this.getMonth() + 1, //Month         
		"d+": this.getDate(), //Day of month         
		"h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //Hour 12
		"H+": this.getHours(), // Hour 24
		"m+": this.getMinutes(), // Minute
		"s+": this.getSeconds(), // Second         
		"q+": Math.floor((this.getMonth() + 3) / 3), // Season         
		"S": this.getMilliseconds() // Millisecond         
	};
	var week = {
		"0": "/u65e5",
		"1": "/u4e00",
		"2": "/u4e8c",
		"3": "/u4e09",
		"4": "/u56db",
		"5": "/u4e94",
		"6": "/u516d"
	};
	if (/(y+)/.test(fmt)) {
		fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	}
	if (/(E+)/.test(fmt)) {
		fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
	}
	for (var k in o) {
		if (new RegExp("(" + k + ")").test(fmt)) {
			fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
		}
	}
	return fmt;
};

Date.prototype.diff = function (d) {
	var t = d || new Date();
	var s = this;
	var r = { hour: t.getHours() - s.getHours(), minute: t.getMinutes() - s.getMinutes(), second: t.getSeconds() - s.getSeconds(), msecond: t.getMilliseconds() - s.getMilliseconds() };
	return r.msecond + r.second * 1000 + r.minute * 60 * 1000 + r.hour * 60 * 60 * 1000;
};
Array.prototype.add = function (o) {
	this[this.length] = o;
};
Array.prototype.last = function (n, fn) {
	var r = this[this.length - n];
	if (fn && r) {
		return fn(r);
	} return r;
};
Array.prototype.clear = function () {
	var q = this;
	while (q.length > 0) {
		var i = q.pop();
		delete i;
	}
};

function List(destroyer) {
	var a = new Array();
	a.add = function (o) {
		a[a.length] = o;
	};
	a.del = function (o) {
		var t = typeof (o);
		var index = -1;
		if (t == 'object') {
			for (var i = 0; i < this.length; i++) {
				if (this[i] == o) {
					index = i;
					break;
				}
			}
		} else {
			index = parseInt(o);
			if (isNaN(index)) {
				index = -1;
			}
		}
		if (index >= 0 && index < this.length) {
			for (var j = i; j > 0; j--) {
				this[j] = this[j - 1];
			}
			this.pop();
		}
	};
	a.clear = function () {
		for (var i = 0; i < this.length; i++) {
			var item = this.pop();
			if (destroyer) {
				destroyer(item);
			}
			if (item) {
				delete item;
				item = null;
			}
		}
	};
	return a;
}

function Dict(destroyer) {
	var o = {};
	o.exist = function (key) {
		if (this[key]) {
			return true;
		}
		return false;
	}
	return o;
}

function agent() {
	var s = navigator.userAgent.toLowerCase();
	return {
		isAndroid: function () {
			return s.indexOf('android') >= 0;
		}, isIos: function () {
			return s.indexOf('ipad') >= 0 || s.indexOf('iphone') >= 0;
		}, isMobile: function () {
			return this.isAndroid() || this.isIos();
		}, text: function () {
			return s;
		}
	}
}

function attr(el, name, val) {
	if (el && el.tagName && name) {
		if (val) {
			el.setAttributeNS(null, name, val);
		} else {
			return el.getAttribute(name) || el.getAttributeNS(null, name) || el[name];
		}
	}
}

function fromJson(s) {
	if (!s) {
		return {};
	}
	try {
		var r = JSON.parse(s);
		if (!r) {
			r = eval('(' + s + ')');
		}
		return r;
	} catch (e) {
		return eval('(' + s + ')');
	}
}
function p2e(pos, el) {
	var rlt = [];
	el.onmouseover = function (event) {
		rlt[0] = event.offsetX;
		rlt[1] = event.offsetY;
	}
	simulate(el, 'mouseover', pos);
	return rlt;
}
function p2c(el) {
	var rc = el.getBoundingClientRect();
	var rlt = [rc.left, rc.top];
	return rlt;
}
function ebp(pos, c) {
	var list = [];
	while (true) {
		var el = document.elementFromPoint(pos[0], pos[1]);
		if (!c) {
			break;
		}
		if (c.hasp && el[c.hasp]) {
			break;
		}
		if (c.hasa && el.getAttribute(c.hasa)) {
			break;
		}
		if (c.nop && !el[c.nop]) {
			break;
		}
		if (c.noa && !el.getAttribute(c.noa)) {
			break;
		}
		if (el == document.body) {
			el = null;
			break;
		}
		list.add(el);
		el.style.display = 'none';
	}
	for (var i = 0; i < list.length; i++) {
		var it = list[i];
		it.style.display = '';
	}
	list = null;
	return el;
}
function simulate(element, eventName, pos) {
	function extend(destination, source) {
		for (var property in source)
			destination[property] = source[property];
		return destination;
	}

	var eventMatchers = {
		'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
		'MouseEvents': /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
	}

	var defaultOptions = {
		pointerX: 100,
		pointerY: 100,
		button: 0,
		ctrlKey: false,
		altKey: false,
		shiftKey: false,
		metaKey: false,
		bubbles: true,
		cancelable: true
	}
	if (pos) {
		defaultOptions.pointerX = pos[0];
		defaultOptions.pointerY = pos[1];
	}
	var options = extend(defaultOptions, arguments[3] || {});
	var oEvent, eventType = null;

	for (var name in eventMatchers) {
		if (eventMatchers[name].test(eventName)) { eventType = name; break; }
	}

	if (!eventType)
		throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');

	if (document.createEvent) {
		oEvent = document.createEvent(eventType);
		if (eventType == 'HTMLEvents') {
			oEvent.initEvent(eventName, options.bubbles, options.cancelable);
		}
		else {
			oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView,
            options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY,
            options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element);
		}
		element.dispatchEvent(oEvent);
	}
	else {
		options.clientX = options.pointerX;
		options.clientY = options.pointerY;
		var evt = document.createEventObject();
		oEvent = extend(evt, options);
		element.fireEvent('on' + eventName, oEvent);
	}
	return element;
}
