(function (nul) {
    var joy = function (id) {
    	var t = typeof (id);
	    var el;
        if (id) {
            if (t == 'string') {
                el = document.getElementById(id);
            }
            else if (t == 'object') {
                el = id;
            }
            else if (t == 'function') {
                joy.ready(id);
                return null;
            }
            if (el) {
                for (var i in joy.fn) {
                    var tf = typeof (joy.fn[i]);
                    if (tf == 'function' && !el[i]) {
                        el[i] = joy.fn[i];
                    }
                }
                return el;
            }
        }
        return null;
    };

    var readyTimeout = null;
    joy.tk = 'tkclient';
    joy.tks = 'tks';
    joy.secPar = 'sp';
    joy.cs = 'cs';
    joy.ccs = 'ccs';
    joy.uid = 'uid';
    joy.delCookie = function(c_name) {
        joy.setCookie(c_name, '', -100);
    };
    
    joy.setCookie = function(c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        var entry = c_name + "=" + c_value;
        document.cookie = entry;
    };

    joy.getCookie = function(c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    };
    joy.List = function() {
        var list = new Array();
        list.selectedIndex = -1;
        list.add = function(o) {
            this[this.length] = o;
        };
        list.selection = function() {
            if (this.selectedIndex >= 0 && this.selectedIndex < this.length) {
                return this[this.selectedIndex];
            }
            return null;
        };
        list.select = function(v, f) {
            if (!f) {
                f = '_key';
            }
            if (!v) {
                return this.selection();
            }
            if (typeof(v) == 'number') {

            } else if (typeof(v) == 'string') {
                for (var i = 0; i < this.length; i++) {
                    var o = this[i];
                    if (o[f] == v) {
                        return o;
                    }
                }
            }
            return null;
        };
        list.enum = function(delegate) {
            if (delegate) {
                for (var i = 0; i < this.length; i++) {
                    if (this[i] !== null) {
                        delegate(this[i]);
                    }
                }
            }
        };
        return list;
    };
    joy.Dict = function() {
        var dict = List();
        dict.addItem = function(key, val) {
            val._key = key;
            this.add(val);
        };
        dict.retrieve = function(noi) {
            var kv = this.select(noi);
            return kv;
        };
        return dict;
    };
    joy.encrypt = function (text, pwd, alg) {
        if (!alg) {
            alg = 'AES';
        }
        var e = CryptoJS[alg];
        return e.encrypt(text, pwd);
    };
    joy.decrypt = function(secret, pwd, alg) {
        if (!alg) {
            alg = 'AES';
        }
        var e = CryptoJS[alg];
        return e.decrypt(secret, pwd).toString(CryptoJS.enc.Utf8);
    };
    joy.ready = function (callback) {
        var retry = function () {
            if (document.readyState === "complete") {
                window.clearTimeout(readyTimeout);

                document.onmousemove = function (evt) {
                    evt = evt || event;
	                var xPos = function(evt) {
		                if (evt.pageX) return evt.pageX;
		                else if (evt.clientX)
			                return evt.clientX + (document.documentElement.scrollLeft ?
				                document.documentElement.scrollLeft :
				                document.body.scrollLeft);
		                else return null;
	                };
	                var yPos = function(evt) {
		                if (evt.pageY) return evt.pageY;
		                else if (evt.clientY)
			                return evt.clientY + (document.documentElement.scrollTop ?
				                document.documentElement.scrollTop :
				                document.body.scrollTop);
		                else return null;
	                };
                    joy.mouse.x = xPos(evt);
                    joy.mouse.y = yPos(evt);
                    joy.mouse.fireEvent(evt, 'onmove');
                };
                callback();
                return;
            }
            window.clearTimeout(readyTimeout);
            joy.readyTimout = window.setTimeout(retry, 500);
        };
        joy.readyTimout = window.setTimeout(retry, 500);
    };

    joy.controls = {};

    joy.evt = function(e) {
        var evt = e || event;
        return { e: evt, el: evt.srcElement || evt.target };
    };
	joy.id = function() {
		if (!joy._id) {
			joy._id = 10000;
		}
		return joy._id++;
	};

    joy.get = function (target) {

    };

    joy.c = function (tag) {
        return el = document.createElement(tag);
    };

    joy.mouse = {
        x: 0, y: 0, onmove: {},
        fireEvent: function (evt, action, jid) {
            for (var i in this.onmove) {
                if (action && (!jid || i == jid)) {
                    this[action][i](evt, action);
                }
            }
        }
    };
    joy.handle = function (e, func) {
        if (joy.debug.activated) {
        	alert(func?func + ':':'' + e.message);
			if (func)
        		joy.debug.log(func);
            joy.debug.log(e);
        }
    };
    joy.fromJson = function (txt, reviver) {
    	try {
    		if (txt) {
    			var o = eval('(' + unescape(txt) + ')');
    			return o;
    		}
    		return null;
    	} catch (e) {
    		joy.handle(e, 'fromJson');
    	}
    }
    joy.toJson = function (o, ignoreEl, level) {
        try{
            if (JSON && JSON.stringify) {
                return JSON.stringify(o);
            }
        } catch (e) {
	        try {
		        return joy._toJson(o, ignoreEl, level);
	        } catch(ex) {
	        	joy.handle(ex, 'toJson');
	        }
        }
    };
    joy._toJson = function (o, ignoreEl, level) {
        if (!o) {
            return 'null';
        } else if (ignoreEl && o.tagName) {
            return null;
        } else if (level && level <= 1) {
            return null;
        }
        var t = typeof (o);
        if (o instanceof Array) {
            var s = '';
            for (var i = 0; i < o.length; i++) {
                var item = o[i];
                var r = joy._toJson(item, level - 1);
                if (r) {
                    s += r;
                    s += ',';
                }
            }
            if (s.length > 0) {
                s = s.substr(0, s.length - 1);
            }
            return "[" + s + "]";
        } else if (t == 'object') {
            var s = '';
            for (var i in o){
                var item = o[i];
                var r = joy._toJson(item, level - 1);
                if (r) {
                    s += '"' + i + '":' + r;
                    s += ',';
                }
            }
            if (s.length > 0) {
                s = s.substr(0, s.length - 1);
            }
            return "{" + s + "}";
        } else if (t == 'function') {
            return null;
        } else if (t == 'string') {
        	o = o.replace(/\"/, "\\\"");
            return "\"" + escape(o) + "\"";
        } else if (t == 'number') {
            return o;
        }
    };
    joy.debug = {
		activated:false,
    	log: function (o) {
    		if (!joy.debug.activated) {
    			joy.debug.acitvate();
    		}
    		if (joy.debug.el) {
    			var el = joy.debug.el;
    			var t = typeof (o);
    			if (t == 'function') {
    				return;
    			} else if (t == 'number' || t == 'string') {
    				el.innerHTML += o;
    				el.innerHTML += '<br />';
    				el.scrollTop = el.scrollHeight;
    				return;
    			}
    			var s = joy.toJson(o);
    			if (s) {
    				el.innerHTML += s.replace(/\r\n/ig, "<br />");
    				el.innerHTML += '<br />----------------------<br />';
    				el.scrollTop = el.scrollHeight;
    			}
    		}
    	},
    	acitvate: function () {
    		joy.debug.activated = true;
    		var el = joy.c('div');
    		el.style.width = '800px';
    		el.style.height = '400px';
    		el.style.position = 'absolute';
    		el.style.border = 'solid 1px silver';
    		el.style.bottom = '0px';
    		el.style.paddingTop = '32px';

    		var box = joy.c('div');
    		var mx = joy.c('input');
    		var my = joy.c('input');

    		el.appendChild(mx);
    		el.appendChild(my);
    		el.appendChild(box);
    		box.style.overflow = 'auto';
    		box.style.width = '100%';
    		box.style.height = '100%';

    		mx.style.position = 'absolute';
    		my.style.position = 'absolute';
    		mx.style.left = '0px';
    		mx.style.top = '0px';
    		my.style.right = '0px';
    		my.style.top = '0px';
    		joy.debug.el = box;
    		joy.mouse.onmove['debug'] = function (evt, action) {
    			mx.value = joy.mouse.x;
    			my.value = joy.mouse.y;
    		};
    		document.body.appendChild(el);
    	}
    };

    joy.fn = {
    	addClass: function (name) {
    		var el = this;
    		var classes = el.className.split(' ');
    		for (var i = 0; i < classes.length; i++) {
    			var cname = classes[i];
    			if (cname == name) {
    				return;
    			}
    		}
    		el.className += ' ' + name;
    		return el;
    	},
    	delClass: function (name) {
    		var el = this;
    		var classes = el.className.split(' ');
    		var rlt = '';
    		for (var i = 0; i < classes.length; i++) {
    			var cname = classes[i];
    			if (cname != ' ' && cname != '' && cname != name) {
    				rlt += cname + ' ';
    			}
    		}
    		el.className = rlt;
    		return el;
    	},
    	kill: function () {
    		var el = this;
    		if (!document.body.killer) {
    			var killer = document.createElement('div');
    			killer.style.display = 'none';
    			document.body.appendChild(killer);
    			document.body.killer = killer;
    		}
    		document.body.killer.appendChild(el);
    		document.body.killer.innerHTML = '';
    	},
    	rect: function (isInner) {
    	    var el = this;
    		if (el.getBoundingClientRect) {
    		    var r = el.getBoundingClientRect();
    		    if (!r.width) {
    		        r.width = r.right - r.left + 1;
    		    }
    		    if (!r.height) {
    		        r.height = r.bottom - r.top + 1;
    		    }
    		    if (isInner) {
    		        var s = el.realstyle();
    		        r.width -= s.borderLeftWidth + s.borderRightWidth;
    		        r.height -= s.borderTopWidth + s.borderBottomWidth;
    		    }
    		    return r;
    		}
    		return { left: 0, top: 0, right: 0, bottom: 0 };
    	},
    	resize: function() {
    	    var el = this;
    	    var p = el.parentNode || el.parent;
    	    if (el && p && el.tagName && el.tagName.toLowerCase() == 'canvas') {
    	    	var rect = joy(p).rect();
	    	    el.setAttribute('width', rect.width); //*1.5);
	    	    el.setAttribute('height', rect.height); //*1.5);
    	    }
    	},
    	realstyle: function() {
    	    var el = this;
    	    return el.currentStyle || getComputedStyle(el);
    	}
    };
    joy.id = function () {
    	if (!joy._id) {
    		joy._id = 0;
    	}
    	return ++joy._id;
    };

    joy.controls = {};
    joy.c = function (tag) {
    	return el = document.createElement(tag);
    };

    joy.destroy = function (el) {
		if (el && el.tagName)
    		joy(el).kill();
    };
    joy.make = function (obj, parentEl, rootEl) {
    	if (!obj) {
    		return null;
    	}
    	var curtEl;
    	if (typeof (obj) == 'string') {
    		if (joy.controls[obj]) {
    			obj = joy.controls[obj];
    			curtEl = document.createElement(obj.tag || obj.tagName);
    		} else {
    			curtEl = document.createElement(obj);
    		}
    	} else if (obj.controlName) {
    		curtEl = joy.make(joy.controls[obj.controlName]);
    	} else {
    		curtEl = document.createElement(obj.tag || obj.tagName);
    	}
    	if (!obj) {
    		throw ('Instance missing error.');
    	}
    	if (!rootEl) {
    	    rootEl = curtEl;
    	    if (rootEl != null) {
    	        rootEl.$aliasEls = [];
    	    }
    	} else {
    		curtEl.$root = rootEl;
    	}
    	for (var p in obj) {
    		var type = typeof (obj[p]);
    		if (p != 'tagName' && p != 'tag') {
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
    				rootEl.$aliasEls.push(curtEl);
    			} else if (p == 'className' || p == 'class') {
    				joy.addClass(curtEl, obj[p]);
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
        joy.initControl(curtEl);
    	//curtEl.rect = function () {
    	//	var rect = this.getBoundingClientRect();
    	//	return { top: rect.top, left: rect.left, bottom: rect.bottom, right: rect.right, width: rect.right - rect.left, height: rect.bottom - rect.top };
    	//};
    	//curtEl.append = function (target, skipNotify) {
    	//	if (!target) {
    	//		return;
    	//	}
    	//	var t = typeof (target);
    	//	var el = target;
    	//	if (t == 'string') {
    	//		el = joy(target);
    	//	}
    	//	if (el) {
    	//		el.appendChild(this);
    	//		if (this.onappend && !skipNotify) {
    	//			if (!this.onappend(el) && this.$aliasEls) {
    	//				for (var j = 0; j < this.$aliasEls.length; j++) {
    	//					var aliasEl = this.$aliasEls[j];
    	//					if (aliasEl.onappend) {
    	//						aliasEl.onappend();
    	//					}
    	//				}
    	//			}
    	//		}
    	//	}
    	//};
    	if (parentEl) {
    		parentEl.appendChild(curtEl);
    	}
    	if (curtEl.onmade) {
    		curtEl.onmade();
    	}
    	return curtEl;
    };
    joy.initControl = function(curtEl) {
        curtEl.rect = function () {
            var rect = this.getBoundingClientRect();
            return { top: rect.top, left: rect.left, bottom: rect.bottom, right: rect.right, width: rect.right - rect.left, height: rect.bottom - rect.top };
        };
        curtEl.append = function (target, skipNotify) {
            if (!target) {
                return;
            }
            var t = typeof (target);
            var el = target;
            if (t == 'string') {
                el = joy(target);
            }
            if (el) {
                el.appendChild(this);
                if (this.onappend && !skipNotify) {
                    if (!this.onappend(el) && this.$aliasEls) {
                        for (var j = 0; j < this.$aliasEls.length; j++) {
                            var aliasEl = this.$aliasEls[j];
                            if (aliasEl.onappend) {
                                aliasEl.onappend();
                            }
                        }
                    }
                }
            }
        };
    };
    joy.makeObject = function (style, parent, keepCss) {
    	for (var p in style) {
    		if (typeof (style[p]) == 'object') {
    			if (parent[p]) {
    				this.makeObject(style[p], parent[p]);
    			} else {
    				try {
    					parent[p] = {};
    				} catch (e) {
    					debugger;
    				}
    			}
    		} else {
    			if (!keepCss) {
    				parent[p] = style[p];
    			} else {
    				if (p == 'class' || p == 'className') {
    					joy.addClass(parent, style[p]);
    				} else if (p == 'style' && !style[p]) {
    					//do nothing
    				} else {
    					parent[p] = style[p];
    				}
    			}
    		}
    	}
    };
    joy.msg = function (m, el) {
    	if (!el) {
    		var o = {
    			tagName: 'div',
    			className: 'notification',
    			setData: function (msg) {
    				this.$msgarea.innerHTML = msg;
    			},
    			$: [
					{ tagName: 'div', alias: 'msgarea', className: 'msgarea' },
					{
						tagName: 'div',
						className: 'btnclose',
						$: 'X',
						onclick: function (evt) {
							joy(this.$root).kill();
						}
					}
    			]
    		};
    		el = joy.make(o);
    	}
    	if (typeof (m) == 'object' && m.length) {
    		var s = '';
    		for (var i = 0; i < m.length; i++) {
    			s += m[i] + '<br/>';
    		}
    		el.setData(s);
    	} else {
    		el.setData(m);
    	}
    	document.body.appendChild(el);
    	return el;
    };
    joy.List = function () {
    	var r = new Array();
        r.add = function(o) {
            r[r.length] = o;
        };
        r.contains = function(f, v) {
            if (f) {
                for (var i = 0; i < r.length; i++) {
                    var o = r[i];
                    if (o && o[f]) {
                        if (!v || o[f] == v)
                            return o;
                    }
                }
            }
            return null;
        };
    	return r;
    };
    joy.MethodCallbacks = {};

    window.joy = joy;
    window.Joy = joy;
})();


//joy.mouse = {
//	listeners: {
//		onmove: {},
//		onup: {},
//		ondown: {},
//		ondrag: {}
//	},
//	fireEvent: function (name) {
//		if (name) {
//			var list = this.listeners[name];
//			for (var i in list) {
//				if (list[i]) {
//					//joy.log(name);
//					list[i].dragging[name](list[i]);
//				}
//			}
//		}
//	},
//	removeListener: function (el) {
//		if (el) {
//			if (el.dragging.onupid) {
//				//joy.mouse.listeners.onup[el.dragging.onupid] = null;
//				delete joy.mouse.listeners.onup[el.dragging.onupid];
//			}
//			if (el.dragging.onmoveid) {
//				//joy.mouse.listeners.onmove[el.dragging.onmoveid] = null;
//				delete joy.mouse.listeners.onmove[el.dragging.onmoveid];
//			}
//			if (el.dragging.ondragid) {
//				//joy.mouse.listeners.ondrag[el.dragging.ondragid] = null;
//				delete joy.mouse.listeners.ondrag[el.dragging.ondragid];
//			}
//		}
//	},
//	drag: function (el, cfg) {
//		el.dragging = cfg;
//		if (cfg.onmove) {
//			el.dragging.onmoveid = el.jid;
//			joy.mouse.listeners.onmove[el.dragging.onmoveid] = el;
//		}
//		if (cfg.onup) {
//			el.dragging.onupid = el.jid;
//			joy.mouse.listeners.onup[el.dragging.onupid] = el;
//		}
//		if (cfg.ondrag) {
//			el.dragging.ondragid = el.jid;
//			joy.mouse.listeners.ondrag[el.dragging.ondragid] = el;
//		}
//	}
//};

//joy.onready = function (callback) {
//	if (!joy.onready.$) {
//		joy.onready.$ = [];
//	}
//	joy.onready.$.push(callback);
//};
//joy.ready = function (callback) {
//    if (document.body && document.readyState == 'complete') {
//		function getInternetExplorerVersion() {
//			var rv = -1; // Return value assumes failure.
//			if (navigator.appName == 'Microsoft Internet Explorer') {
//				var ua = navigator.userAgent;
//				var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
//				if (re.exec(ua) != null)
//					rv = parseFloat(RegExp.$1);
//			}
//			return rv;
//		}

//		if (!joy.isInited) {
//			joy.isInited = true;
//			var browser = {};
//			var ua = navigator.userAgent.toLowerCase();
//			if (navigator.userAgent.indexOf("MSIE") > 0) {
//				browser.ie = true;
//				var ver = getInternetExplorerVersion();
//				if (ver > 0) {
//					browser['ie' + ver] = true;
//				}
//			}
//			if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
//				browser.firefox = true;
//			}
//			if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {
//				browser.safari = true;
//			}
//			if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
//				browser.camino = true;
//			}
//			if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) {
//				browser.gecko = true;
//			}
//			this.browser = browser;
//			document.body.style.height = window.screen.height + 'px';
//			document.body.onmousemove = function (evt) {
//				var evt = evt || event;
//				joy.mouse.prevX = joy.mouse.x;
//				joy.mouse.prevY = joy.mouse.y;
//				joy.mouse.x = evt.clientX;
//				joy.mouse.y = evt.clientY;
//				joy.mouse.mousemove = true;
//				if (joy.mouse.mousedown) {
//					joy.mouse.dragging = true;
//				}
//				if (joy.mouse.dragging) {
//					joy.mouse.fireEvent('ondrag');
//				} else {
//					joy.mouse.fireEvent('onmove');
//				}
//			};
//			document.body.onmousedown = function (evt) {
//				if (!joy.mouse.mousedown) {
//					joy.mouse.startpos = { x: joy.mouse.x, y: joy.mouse.y };
//				}
//				joy.mouse.mousedown = true;
//				joy.mouse.fireEvent('ondown');
//			};
//			document.body.onmouseup = function (evt) {
//				joy.mouse.mousedown = false;
//				joy.mouse.endpos = { x: joy.mouse.x, y: joy.mouse.y };
//				joy.mouse.dragging = false;
//				joy.mouse.fireEvent('onup');
//			};
//		}
//		callback(false);
//		if (joy.onready.$) {
//			var list = joy.onready.$;
//			joy.onready.$ = null;
//			for (var i = 0; i < list.length; i++) {
//				var func = list[i];
//				func();
//			}
//		}
//		callback(true);
//	} else {
//		window.setTimeout(function () {
//			joy.ready(callback);
//		}, 300);
//	}
//};

joy.extend = function (base, ext, notConfig) {
	if (ext) {
		if (!notConfig) {
			base = joy.makeObject(ext, base, true);
		} else {
			if (ext && base) {
				ext.prototype = new base();
				return ext;
			}
		}
	}
	return base;
};

joy.merge = function (obj, ext, toplevelonly) {
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
						joy.merge(obj[i], ext[i]);
					} else if (etype == 'object') {
						if (ext[i] instanceof Array) {
							obj[i] = [];
						} else {
							obj[i] = {};
						}
						joy.merge(obj[i], ext[i]);
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
				//var otype = typeof (obj[i]);
				var etype = typeof (ext[i]);
				if (etype != 'object') {
					obj[i] = ext[i];
				}
			}
		}
	}
	return obj;
};

joy.resize = function (el, targetEl, offset) {
	var bound = this.actualSize(targetEl);
	el.style.width = (bound.right - bound.left - offset) + 'px';
	el.style.height = (bound.bottom - bound.top - offset) + 'px';
};


joy.obj2xml = function (obj, tagName, isChild) {
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
				inner += this.obj2xml(obj[i], joy.xml.makeSubItemName(tag), true);
			} else {
				inner += this.obj2xml(obj[i], i, true);
			}
		} else {
			if (obj instanceof Array) {
				inner += '<' + joy.xml.makeSubItemName(tag) + '>' + obj[i] + '</' + joy.xml.makeSubItemName(tag) + '>';
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
		return joy.xml.xmlAddTitle(rlt).content;
	}
};
joy.xml = function (content, callback) {
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
	if (doc && !isXmlStr && content) {
		var isAsync = (callback != null) ? true : false;
		doc.async = isAsync;
		doc.load(content);
	}
	if (doc && doc.childNodes && doc.childNodes.length > 1) {
		rlt = doc.childNodes[1];
	} else if (doc && doc.childNodes && doc.childNodes.length == 1) {
		rlt = doc.childNodes[0];
	} else {
		rlt = doc;
	}
	return rlt;
};
joy.xml.makeSubItemName = function (name) {
	if (name) {
		if (name.charAt(name.length - 1) == 's') {
			return name.substr(0, name.length - 1);
		} else if (name.indexOf('Collection') == (name.length - 'Collection'.length)) {
			return name.substr(0, name.length - 'Collection'.length);
		} else {
			return name + 'Item';
		}
	}
};
joy.xml.xmlText = function (node) {
	if (node.text) {
		return node.text;
	} else {
		return node.textContent;
	}
};
joy.xml.xmlValue = function (node) {
	if (node.xml) {
		return node.xml;
	} else {
		return new XMLSerializer().serializeToString(node);
	}
};
joy.xml.xmlAddTitle = function (content) {
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
joy.xml.xmlSafeString = function (content, isAttribute) {
	var rlt = content;
	rlt = rlt.replace(/\</g, '&lt;');
	rlt = rlt.replace(/\"/g, '&quot;');
	return rlt;
};

joy.log = function (info, hideLog) {
	var el = document.body.logEl;
	if (!el) {
		document.body.logEl = joy.c('textarea');
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

joy.sleep = function (nMillis) {
	var dt1 = new Date();
	for (; ;) {
		var dt2 = new Date();
		if ((dt2.getTime() - dt1.getTime()) >= nMillis)
			break;
	}
};

joy.json = function (p) {
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
	}
	var rlt = null;
	if (type == 'string') {
		try {
			var pre = p.substr(0, 5);
			if (pre) {
				pre = pre.toLowerCase();
				if (pre == '<pre>') {
					p = p.substr(5, p.length - 11);
				}
			}
		} catch(e) {
			
		}
		try {
			rlt = eval('(' + p + ')');
		} catch (e) {
			rlt = null;
		}
		return rlt;
	} else {
		rlt = toJson(p);
		return rlt;
	}
};

joy.cookie = function (id, value, forceReload) {
	var s = '';
	var rlt;
	if (forceReload || !document.body.cookie) {
		var list = document.cookie.split(';');
		rlt = {};
		for (i = 0; i < list.length; i++) {
			x = list[i].substr(0, list[i].indexOf("="));
			y = list[i].substr(list[i].indexOf("=") + 1);
			x = x.replace(/^\s+|\s+$/g, "");
			rlt[x] = y;
		}
		document.body.cookie = rlt;
	} else {
		rlt = document.body.cookie;
	}
	if (value && !id) {
		delete rlt[value];
	} else if (id && value) {
		rlt[id] = value;
	} else if (id && !value) {
		return unescape(rlt[id].replace(/\+/g, ' '));
	}
	for (var i in rlt) {
		s += i + '=' + escape(rlt[i]) + ';';
	}
	document.cookie = s;
	return s;
};

joy.dateDiff = function (dt, df) {
	if (!df) {
		df = new Date();
	}
	var millisecondsPerDay = 1000 * 60 * 60 * 24;
	return Math.floor((dt - df) / millisecondsPerDay);
};

joy.dateString = function (d, format) {
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

joy.addClass = function (el, name) {
	var classes = el.className.split(' ');
	for (var i = 0; i < classes.length; i++) {
		var cname = classes[i];
		if (cname == name) {
			return;
		}
	}
	el.className += ' ' + name;
};
joy.delClass = function (el, name) {
	var classes = el.className.split(' ');
	var rlt = '';
	for (var i = 0; i < classes.length; i++) {
		var cname = classes[i];
		if (cname != ' ' && cname != '' && cname != name) {
			rlt += cname + ' ';
		}
	}
	el.className = rlt;
};

joy.model = function (cfg) {
	if (cfg) {
		if (!document.body.modelEl) {
			var modelEl = joy.make({
				tagName: 'div',
				style: { position: 'fixed', zIndex: 1000, left: 0, top: 0, right: 0, bottom: 0, display: '' },
				$: [
					{
						tagName: 'div',
						style: {
							position: 'absolute',
							padding: '10px',
							zIndex: 1002,
							left: '50%',
							top: '50%',
							backgroundColor: 'white',
							border:'solid 1px silver',
							width: '25px',
							height: '25px',
							marginTop: '-50px',
							marginLeft: '-50px',
							textAlign: 'center',
							verticalAlign: 'middle'
						},
						$: [
							{
								tagName: 'img',
								src: joy.rootUrl + 'joy/theme/default/images/loading.gif',
								alias: 'modelImg',
								style: { width: '100%', height: '100%' }
							}]
					},
					{
						tagName: 'div',
						className: 'opacity',
						style: {
							position: 'absolute',
							zIndex: 1001,
							backgroundColor: 'silver',
							opacity: 0.5,
							left: 0,
							right: 0,
							top: 0,
							bottom: 0
						}
					}
				]
			});
			if (!joy.modelTemplates) {
				joy.modelTemplates = {};
			}
			joy.modelTemplates.$ = modelEl;
			document.body.modelEl = modelEl;
			document.body.appendChild(modelEl);
		}

		var t = typeof (cfg);
		if (t == 'object') {

		} else if (t == 'string' || t == 'number' || t == 'boolean') {

		}
		document.body.modelEl.style.display = '';
	} else {
		if (document.body.modelEl) {
			document.body.modelEl.style.display = 'none';
		}
	}
};


