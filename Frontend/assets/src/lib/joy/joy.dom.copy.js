(function ($) {
	function copy(des, src, override, excludes) {
		if (!excludes) {
			excludes = [];
		}
		for (var i in src) {
			var t = typeof (src[i]);
			var skip = false;
			for (var j = 0; j < excludes.length; j++) {
				if (excludes[j] == i) {
					skip = true;
					break;
				}
			}
			if (skip) {
				continue;
			}
			if (override || !des[i]) {
				if (t == 'object') {
					if (typeof (des[i]) != 'object') {
						des[i] = {};
					}
					copy(des[i], src[i], override);
				} else {
					try {
						des[i] = src[i];
					} catch (e) {
						debugger;
						//this.log("Error: " + i + " cannot be merged.\t" + e);
					}
				}
			}
		}
	}
	function mask(cfg) {
		var json = {
			tag: 'div'
            , className: 'mask'
            , show: function (c) {
            	var w = window.innerWidth;
            	var h = window.innerHeight;
            	if (!c) {
            		c = { el: document.body, mode: 'fixed', full: true };
            	} else if (!c.el) {
            		c.el = document.body;
            		c.mode = 'fixed';
            		c.full = true;
            	} else if (!c.mode) {
            		c.mode = 'inner';
            	}
            	var el = $(c.el).dom();
            	var r = el.rect();

            	if (c.mode == 'fixed') {
            		this.style.position = 'fixed';
            		if (c.full) {
            			this.style.left = 0;
            			this.style.top = 0;
            			this.style.right = 0;
            			this.style.bottom = 0;
            		} else {
            			this.style.left = r.left + 'px';
            			this.style.top = r.top + 'px';
            			this.style.right = w - r.right + 'px';
            			this.style.bottom = h - r.bottom + 'px';
            		}
            		this.style.zIndex = 99999;
            		this.style.display = 'none';
            		$(document.body).append(this);
            		$(this).fadeIn(200);
            	} else if (c.mode == 'inner') {
            		this.style.position = 'absolute';
            		this.style.left = 0;
            		this.style.top = 0;
            		this.style.right = 0;
            		this.style.bottom = 0;
            		this.style.zIndex = 99999;
            		this.style.display = 'none';
            		$(c.el).append(this);
            		$(this).fadeIn(200);
            	}
            	if (c && c.data) {
            		this.$content.val(c.data);
            	}
            }
            , $: [
                {
                	tag: 'div', alias: 'relative', className: 'inner relative container center'
                    , $: [{ cn: 'Cell', alias: 'content', className: 'maskcontent centercell' }]
                }
            ]
		};
		var mel = $.u(json);
		//mel.appendTo(document.body);
		mel.show(cfg);
		return mel;
	}
	function use(ctrl, cfg, controlset) {
		if (!controlset) {
			if ($ && $.userControls) {
				controlset = $.userControls;
			} else if (joy && joy.userControls) {
				controlset = joy.userControls;
			} else {
				debugger;
				return null;
			}
		}
		function parse(ctrl) {
			var t = typeof (ctrl);
			if (t == 'string') {
				if (t[0] == '{') {
					ctrl = joy.fromJson(ctrl);
				} else {
					ctrl = $.userControls[ctrl];
					if (typeof (ctrl) == 'function') {
						ctrl = ctrl();
					}
				}
			} else if (t == 'function') {
				ctrl = ctrl();
			}
			if (!ctrl) {
				return null;
			}
			return ctrl;
		}
		function make(json, ec, cc, rootEl, parEl) {
			var el;
			if (json.tag) {
				el = ec(json, json.tag, rootEl, parEl);
			} else if (json.cn) {
				el = cc(json, json.cn, null, parEl);
				if (rootEl && el.alias) {
					rootEl['$' + el.alias] = el;
				}
			}
			el.appendTo = function (s) {
				if (!s) {
					return this;
				}
				if (s.appendChild && s.tagName) {
					s.appendChild(this);
				} else if ($ && $.dom && typeof ($.dom) == 'function') {
					$(s).dom().appendChild(this);
				}
				return this;
			};
			if (el.onmade) {
				el.onmade();
			}
			return el;
		}
		function soncreator(parEl, json, rootEl, cc) {
			if (json) {
				var t = typeof (json);
				if (t == 'string') {
					if (parEl.val) {
						parEl.val(json);
					} else {
						parEl.innerHTML = json;
					}
				} else if (json instanceof Array) {
					for (var i = 0; i < json.length; i++) {
						var cjson = json[i];
						var cel = make(cjson, elcreator, cncreator, rootEl, parEl);
						if (!cc) {
							parEl.appendChild(cel);
						} else {
							cc(parEl, cel);
						}
						if (cel.alias) {
							rootEl['$' + cel.alias] = cel;
						}
					}
				} else if (t == 'object') {
					var cel = make(json, elcreator, cncreator, rootEl, parEl);
					if (!cc) {
						parEl.appendChild(cel);
					} else {
						cc(parEl, cel);
					}
					if (cel.alias) {
						rootEl['$' + cel.alias] = cel;
					}
				} else {
					debugger;
				}
			}
		}
		function hiddenEl() {
			var div = document.createElement("div");
			div.style.display = "none";
			return div;
		}
		var excludes = {
			$: true
            , dataset: true
            , external: true
            , ownerElement: true
            , nodeValue: true
            , textContent: true
            , inputEncoding: true
            , mozApps: true
            , currentStyle: true
            , outerText: true
            , msContentZoomFactor: true
		};
		function elcreator(json, tag, rootEl, parEl) {
			var el = document.createElement(tag);
			joy.extend(el, json, { excludes: excludes });
			el.$root = rootEl;
			if (!rootEl) {
				rootEl = el;
			}
			soncreator(el, json.$, rootEl);
			return el;
		}
		function cncreator(c, name, rootEl, parEl) {
			var json = controlset[name];
			if (!json) {
				return hiddenEl();
			} else if (typeof (json) == 'function') {
				json = json();
			}
			joy.extend(json, c, { excludes: excludes });
			var el = elcreator(json, json.tag, null, parEl);
			if (!rootEl) {
				rootEl = el;
			}
			soncreator(el, c.$, rootEl, function (pel, cel) {
				pel.val(cel, true);
			});
			return el;
		}
		var json = parse(ctrl);
		if (!json) {
			return hiddenEl();
		}
		joy.extend(json, cfg);
		var el = make(json, elcreator, cncreator, null, null);
		if (el.onmade) {
			el.onmade();
		}
		return el;
	}
	$.joy = joy;

	$.userControls = {};

	$.u = use;
	$.use = use;

	$.mask = mask;

	$.val = function (v, sender, append) {
		var t = typeof (v);
		if (!sender) {
			debugger;
			return;
		}
		if (!append) {
			sender.innerHTML = '';
		}
		if (t == 'object') {
			if (v.tagName) {
				if (!sender) {
					debugger;
				}
				sender.appendChild(v);
				return v;
			} else if (v.value) {
				sender.innerHTML = v.value;
				return v.value;
			} else if (v.img) {
				var img = document.createElement('img');
				joy.extend(img, v.img);
				sender.appendChild(img);
				return img;
			} else if (v.Result) {
				sender.innerHTML = v.Result;
				return v.Result;
			} else {
				//debugger;
			}
			return null;
		} else if (t == 'function') {
			return v(sender);
		} else {
			sender.innerHTML = v;
			if (v && v.indexOf('<script') >= 0) {
				var els = sender.getElementsByTagName('script');
				for (var i = 0; i < els.length; i++) {
					var s = els[i].innerHTML;
					eval(s);
				}
			}
		}
		return v;
	};

	$.create = function (tag, c, autoDoc) {
		var el = document.createElement(tag);
		for (var i in c) {
			el[i] = c[i];
		}
		if (autoDoc) {
			//$(document.body).append(qel);
			document.body.appendChild(el);
		}
		return el;
	};

	$.uid = function (prefix) {
		$.uid.curtval = $.uid.curtval > 0 ? $.uid.curtval + 1 : 1;
		return prefix + '_' + $.uid.curtval;
	};

	$.test = function (o) {
		var el = document.body.testbox;
		if (!el) {
			el = $.u({ cn: 'Form', alias: 'box', id: 'box', style: { zIndex: 9999999 }, className: 'testbox form fixed' });
			document.body.testbox = el;
			document.body.appendChild(el);
		}
		var txt = $.log(o, 3);
		if (el && el.val) {
			if (!txt) {
				debugger;
				return;
			}
			try {
				el.val(txt + '');
			} catch (e) {
				debugger;
			}
		}
	}
	$.dispose = function (el) {
		if (!document.body.disposerEl) {
			document.body.disposerEl = document.createElement('div');
			document.body.disposerEl.style.display = 'none';
			document.body.appendChild(document.body.disposerEl);
		}
		var d = document.body.disposerEl;
		d.appendChild(el);
		d.innerHTML = '';
	};
	$.log = function (t, maxdepth, n, depth) {
		if (!$.log.filter) {
			$.log.filter = function (name) {
				if (name.indexOf('Element') >= 0) {
					return true;
				}
				return false;
			};
		}
		if (!t) {
			t = '""';
		}
		if (!depth) {
			depth = 0;
		}
		var tp = typeof (t);
		if (tp != 'object') {
			var txt = t;
			if (n) {
				txt = n + "=" + t;
			}
			for (var i = 0; i < depth; i++) {
				txt = "--" + txt;
			}
			//console.log(txt);
			return txt;
		} else if (depth < maxdepth) {
			this.log(n, maxdepth, null, depth);
			var tt = '';
			for (var i in t) {
				if ($.log.filter(i)) {
					continue;
				}
				try {
					var v = t[i];
					tt += '<br />' + $.log(v, maxdepth, i, depth + 1);
				} catch (e) {
					tt += '<br />' + $.log("Error: " + i + " cannot be logged.\t" + e);
				}
			}
			return tt;
		}
	};

	// Submit form in background by specified method
	$.fn.kick = function (url, method, callback) {
		var frm = this.dom();
		if (!frm.tagName || frm.tagName.toLowerCase() != 'form') {
			return;
		}
		if (url) {
			frm.action = url;
		}
		if (method) {
			frm.method = method;
		}
		if (!frm.target) {
			var id = $.uid('ifm');
			var ifm = $.create('iframe', {
				src: 'about:blank',
				id: id,
				name: id,
				onload: function (e) {
					if (e.srcElement.contentDocument.body.innerHTML != '' && callback) {
						callback(this);
					}
				}
			}, true);
			ifm.name = id;
			frm.target = id;
		}
		frm.submit();
	};

	$.userControls.Flex = function () {
		var n = 6;
		function loc(a, p, v) {
			a[p] = parseInt(v) + 'px';
		}
		function hider(flex, name) {
			for (var i in flex) {
				if (i.length > 1 && i.indexOf('$') == 0 && i.lastIndexOf('$') != i.length - 1) {
					if (i != name) {
						$(flex[name]).hide();
					}
				}
			}
		}
		function shower(flex) {
			flex.$mover.style.display = '';
			flex.$resizer.style.display = '';
			flex.$wer.style.display = '';
			flex.$her.style.display = '';
		}
		var r = {
			tag: 'div'
            , className: 'flex fixed deselect'
            , $: [
                {
                	tag: 'div', className: 'container relative deselect', $: [
                    { tag: 'div', alias: 'mover', className: 'mover float box deselect' }
                    , { tag: 'div', alias: 'wer', className: 'wer float box deselect' }
                    , { tag: 'div', alias: 'her', className: 'her float box deselect' }
                    , { tag: 'div', alias: 'resizer', className: 'resizer float box deselect' }
                	]
                }
            ]
            , behaviors: {
            	fixed: {
            		regist: function (s) {
            			var host = s.$el;
            			host.style.left = host.rect().left + 'px';
            			host.style.top = host.rect().top + 'px';
            			$.ms.regist({
            				host: host, el: s, h: s.$mover, behavior: $.ms.behaviors.move, ac: this.mc, mc: this.movecallback
            			});
            			$.ms.regist({
            				host: host, el: s, h: s.$resizer, override: true, behavior: $.ms.behaviors.resize_bf, horizantal: true, vertical: true, ac: this.rc, mc: this.movecallback
            			});
            			$.ms.regist({
            				host: host, el: s, h: s.$wer, override: true, behavior: $.ms.behaviors.resize_bf, horizantal: true, ac: this.rc, mc: this.movecallback
            			});
            			$.ms.regist({
            				host: host, el: s, h: s.$her, override: true, behavior: $.ms.behaviors.resize_bf, vertical: true, ac: this.rc, mc: this.movecallback
            			});
            		}
                    , movecallback: function (c) {
                    	c.el.relocate($(c.el).dom().rect());
                    }
                    , mc: function (c, el) {
                    	var host = c.host;
                    	//var ox = c.mouse.x2 - c.mouse.x1;
                    	//var oy = c.mouse.y2 - c.mouse.y1;
                    	host.style.left = el.style.left;
                    	host.style.top = el.style.top;
                    	host.style.width = el.style.width;
                    	host.style.height = el.style.height;
                    	//host.style.left = host.rect().left + ox + 'px';
                    	//host.style.top = host.rect().top + oy + 'px';
                    	//host.style.width = host.$bak$.rect.w() + 'px';
                    	//host.style.height = host.$bak$.rect.h() + 'px';
                    }
                    , rc: function (c, el) {
                    	var host = c.host;
                    	//var ox = c.mouse.x2 - c.mouse.x1;
                    	//var oy = c.mouse.y2 - c.mouse.y1;
                    	host.style.left = el.style.left;
                    	host.style.top = el.style.top;
                    	host.style.width = el.style.width;
                    	host.style.height = el.style.height;
                    	el.relocate(el.rect());
                    }
            	}
            }
            , relocate: function (rect) {
            	var n = $(this.$resizer).dom().rect().w() / 2;
            	loc(this.$mover.style, 'left', 0);
            	loc(this.$mover.style, 'top', 0);
            	loc(this.$resizer.style, 'right', 0);
            	loc(this.$resizer.style, 'bottom', 0);
            	loc(this.$wer.style, 'right', 0);
            	loc(this.$wer.style, 'top', rect.h() / 2 - n);
            	loc(this.$her.style, 'bottom', 0);
            	loc(this.$her.style, 'left', rect.w() / 2 - n);
            }
            , attach: function (el, target) {
            	if (!el) {
            		debugger; // Element cannot be null
            		return;
            	}
            	this.$el = $(el).dom();
            	var rect = el.rect();
            	var prm = rect.prm();
            	var pbm = rect.pbm();
            	var p = { x: rect.left, y: rect.top };
            	el.$flex$ = this;
            	loc(this.style, 'left', rect.left);
            	loc(this.style, 'top', rect.top);
            	loc(this.style, 'width', rect.w());
            	loc(this.style, 'height', rect.w());
            	this.relocate(rect);
            	//loc(this.$mover.style, 'left', p.x - 6);
            	//loc(this.$mover.style, 'top', p.y - 6);
            	//loc(this.$resizer.style, 'left', rect.right);
            	//loc(this.$resizer.style, 'top', rect.bottom);
            	//loc(this.$wer.style, 'left', prm.x);
            	//loc(this.$wer.style, 'top', prm.y);
            	//loc(this.$her.style, 'left', pbm.x);
            	//loc(this.$her.style, 'top', pbm.y);
            	if (!target) {
            		target = document.body;
            	}
            	$(target).append(this);
            	$(this).hide().fadeIn(200);

            	var pos = el.css().position;
            	if (pos && this.behaviors[pos]) {
            		this.behaviors[pos].regist(this);
            	}
            }
            , detach: function (el) {
            	//$(this).fadeOut(200);
            	var div = document.createElement('div');
            	div.appendChild(this);
            	div.innerHTML = null;
            }
		};

		return r;
	};
	$.fn.flex = function () {
		var el = this.dom();
		if (el.$flex$) {
			el.$flex$ = false;
			$(el).draggable('disable').resizable('disable').children().remove('.float.ui-resizable-m');
		} else {
			el.$flex$ = true;
			$(el).addClass('flex');
			$(el).append('<div class="float ui-resizable-m"></div>');
			$(el).resizable({ containment: "parent" }).draggable({ handle: '.float.ui-resizable-m', containment: "parent" });
			$(el).draggable('enable').resizable('enable');
		}
	};
	$.fn.dom = function (index, undef) {
		var list = this;
		var r = [];
		if (list.length > 1 && index) {
			r = list.get(index);
		} else if (list.length == 1) {
			r = list[0];
		}
		r.dispose = function () {
			$.dispose(this);
		};
		r.rect = function () {
			var re = this.getBoundingClientRect();
			re.w = function () {
				return this.right - this.left;
			};
			re.h = function () {
				return this.bottom - this.top;
			};
			// Center
			re.pm = function () {
				return { y: this.h() / 2 + this.top, x: this.w() / 2 + this.left };
			};
			// Right middle
			re.prm = function () {
				return { y: this.h() / 2 + this.top, x: this.right };
			};
			// Bottom center
			re.pbm = function () {
				return { y: this.bottom, x: this.w() / 2 + this.left };
			};
			re.pointin = function (x, y) {
				if (x < this.left || y < this.top || x > this.right || y > this.bottom) {
					return false;
				}
				return true;
			}
			return re;
		};
		r.deflex = function () {
			var flex = this.$flex$;
			if (flex) {
				flex.detach(this);
			}
		};
		r.css = function (p) {
			var s = window.getComputedStyle ? window.getComputedStyle(this) : this.currentStyle;
			if (!p) {
				return s;
			} else {
				return s[p];
			}
		}
		return r;
	};

	$.fn.domerge = function (target, noverride, excludes) {
		copy(this.dom(0), target, !noverride, excludes);
		return this;
	};

	$.fn.drawicons = function (cfg) {
		if (!cfg) {
			cfg = { fillStyle: 'gray' };
		}
		var h = 10;
		var w = 10;
		var cvs = this.dom(0);
		if (!cvs.getContext) {
			debugger; // Maybe this is not Canvas element.
			return;
		}
		var ctx = cvs.getContext('2d');
		copy(ctx, cfg, true);
		ctx.beginPath();
		ctx.moveTo(5, 2);
		ctx.lineTo(2, 8);
		ctx.lineTo(8, 8);
		ctx.closePath();
		ctx.fill();

		ctx.beginPath();
		ctx.moveTo(5, 8 + h);
		ctx.lineTo(2, 2 + h);
		ctx.lineTo(8, 2 + h);
		ctx.closePath();
		ctx.fill();
		return this;
	};

})(jQuery);
