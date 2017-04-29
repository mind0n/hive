(function ($) {
	function rectof(el) {
		if (!el) {
			el = this;
		}
		if (!el) {
			return {};
		}
		var re = el.getBoundingClientRect();
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
	}
	window.rectofel = rectof;
	var prefixes = ["", "-webkit-"];
	function htmlSetAttr(o, n, v) {
		var t = typeof (v);
		if (t != 'function' && n && n.indexOf && n.indexOf('$') < 0) {
			if (o.setAttribute && n.toLowerCase() != 'classname') {
				o.setAttribute(n, v);
			} else {
				o[n] = v;
			}
		} else {
			o[n] = v;
		}
	}
	function appendTo(target, switchToParent) {
		if (typeof (target) == 'string') {
			target = document.getElementById(target);
		}
		if (target) {
			target.appendChild(this);
		}
		if (!this.$root && target) {
			this.$root = target.$root || target;
		}
		if (switchToParent) {
			return target;
		}
		return this;
	}
	try {
		HTMLElement.prototype.appendTo = appendTo;
		HTMLElement.prototype.style3d = function (n, v) {
			var that = this;
			for (var i = 0; i < prefixes.length; i++) {
				var p = prefixes[i] + n;
				if (that.style) {
					that.style[p] = v;
				}
			}
		};
	} catch (e) {
		Object.prototype.appendTo = appendTo;
		Object.prototype.style3d = function (n, v) {
			var that = this;
			for (var i = 0; i < prefixes.length; i++) {
				var p = prefixes[i] + n;
				if (that.style) {
					that.style[p] = v;
				}
			}
		};
	}
	joy.debug = function () {
		var a = arguments;
		var div = document.body.$debug$;
		if (!document.body.$debug$) {
			div = document.createElement('div');
			document.body.$debug$ = div;
			div.style.width = '400px';
			div.style.height = '200px';
			div.style.position = 'fixed';
			div.style.top = '0px';
			div.style.right = '0px';
			div.style.border = 'solid 1px blue';
			div.style.background = 'lightblue';
			div.style.overflow = 'auto';
			document.body.appendChild(div);
		}
		//div.innerHTML = '';
		for (var i = 0; i < a.length; i++) {
			div.innerHTML += a[i] + ';';
		}
		div.innerHTML += '<br />';
		div.scrollTop = div.scrollHeight;
	};
	joy.appendTo = appendTo;
	joy.prepend = function (el, target, switchToChild) {
		return joy.append(el, target, switchToChild, function (el, child) {
			if (el.childNodes.length > 0) {
				el.insertBefore(child, el.firstChild);
			} else {
				el.append(child);
			}
		});
	};
	joy.append = function (el, target, switchToChild, behavior) {
		if (el == null) {
			debugger;
		}
		var r = target.tagName ? target : joy.jbuilder(target, { root: el.$root, group: el.$group$, parent: el.$parent$ });
		if (!r) {
			debugger;
		}
		if (!behavior) {
			behavior = function (el, child) {
				el.appendChild(child);
			}
		}
		if (r) {
			if (r.length && r.$isdoms$) {
				for (var i = 0; i < r.length; i++) {
					r[i].$parent$ = el;
					r[i].$group$ = el.$group$;
					//el.appendChild(r[i]);
					behavior(el, r[i]);
				}
			} else {
				r.$parent$ = el;
				r.$group$ = el.$group$;
				//el.appendChild(r);
				behavior(el, r);
			}
			if (target.alias) {
				el['$' + target.alias] = r;
			}
			r.$root = el.$root || el;
		}
		if (switchToChild) {
			return !r.length || r.length < 0 ? r : r[0];
		}
		return el;
	};
	joy.prefix = function (s) {
		s = s.trim();
		var r = '';
		for (var i = 0; i < prefixes.length; i++) {
			r += ' ' + prefixes[i] + s;
		}
		return r;
	}
	joy.creators.tag = {
		createEl: function (n) {
			var el = document.createElement(n);
			if (window.depth) {
				depth(el);
			}
			return el;
		}
		, '#text': function (j, c) {
			var nd = document.createTextNode(j.$);
			j.$ = null;
			return nd;
		}
		, create: function (n, j, c) {
			var el = null;
			if (this[n]) {
				el = this[n](j, c);
			} else {
				el = this.createEl(n);
				joy.extend(el, j, { setAttr: htmlSetAttr, excludes: {} });
			}
			return el;
		}
		, created: function (el) {
			el.$transform$ = function () {
				var r = {
					translate: [0, 0, 0]
					, otranslate: [0, 0, 0]
					, rotate: [0, 0, 0]
					, scale: [1, 1, 1]
					, toString: function (sequence) {
						return 'translate3d(' + this.translate[0] + 'px,' + this.translate[1] + 'px,' + this.translate[2] + 'px)'
							+ ' rotateX(' + this.rotate[0] + 'deg)'
							+ ' rotateY(' + this.rotate[1] + 'deg)'
							+ ' rotateZ(' + this.rotate[2] + 'deg)'
							+ ' scale3d(' + this.scale[0] + ',' + this.scale[1] + ',' + this.scale[2] + ')'
						;
					}
				};
				return r;
			}();
			el.css = function (p) {
				var s = window.getComputedStyle ? window.getComputedStyle(this) : this.currentStyle;
				if (!p) {
					return s;
				} else {
					return s[p];
				}
			};
			el.matrix = function () {
				var computedStyle = window.getComputedStyle(this, null);
				var matrix = computedStyle.getPropertyValue('transform')
					|| computedStyle.getPropertyValue('-moz-transform')
					|| computedStyle.getPropertyValue('-webkit-transform')
					|| computedStyle.getPropertyValue('-ms-transform')
					|| computedStyle.getPropertyValue('-o-transform');
				var matrixPattern = /^\w*\((((\d+)|(\d*\.\d+)),\s*)*((\d+)|(\d*\.\d+))\)/i;
				var matrixValue = null;
				if (matrixPattern.test(matrix)) { // When it satisfy the pattern.
					var matrixCopy = matrix.replace(/^\w*\(/, '').replace(')', '');
					console.log(matrixCopy);
					matrixValue = matrixCopy.split(/\s*,\s*/);
				}
				return matrixValue;
			};
			el.scale3 = function (x, y, z, o, callback) {
				function axis(n, i, min, max) {
					if (!n) {
						return;
					}
					var on = this.$transform$.scale[i];
					var dn = on / n;
					var r = on + dn;
					if (r >= min && r <= max) {
						this.$transform$.scale[i] = r;
					}
					return r <= min || r >= max;
				}
				if (!o) {
					o = [0, 0];
				}
				var r = axis.call(this, x, 0, 0.5, 5)
				|| axis.call(this, y, 1, 0.5, 5)
				|| axis.call(this, z, 2, 0.5, 5);

				var s = this.$transform$.toString();
				this.style3d('transform', s);
				this.style.transformOrigin = o[0] + 'px ' + o[1] + 'px';
				if (callback) {
					callback(r);
				}
				return this;
			};
			el.translate3 = function (x, y, z, commit, offset) {
				function axis(n, i, m, commit) {
					var tt = this.$transform$;
					if (n.indexOf && n.indexOf('%') > 0) {
						n = parseInt(m) * parseInt(n) / 100;
					} else {
						n = parseInt(n);
					}
					tt.translate[i] = tt.otranslate[i] + n;
					if (commit) {
						tt.otranslate[i] += n;
					}
				}
				axis.call(this, x, 0, this.rect().w(), commit);
				axis.call(this, y, 1, this.rect().h(), commit);
				axis.call(this, z, 2, this.style.perspective, commit);
				var s = this.$transform$.toString();
				this.style3d('transform', s);
				return this;
			};
			el.rotate = function (x, y, z) {
				this.$transform$.rotateX = x;
				this.$transform$.rotateY = y;
				this.$transform$.rotateZ = z;
				var s = this.$transform$.toString();
				this.style3d('transform', s);
				return this;
			};
			el.append = function (target, switchToChild) {
				return joy.append(this, target, switchToChild);
			};
			el.rect = function () {
				var re = rectof(this);
				return re;
			};
		}
		, append: function (p, c) {
			if (p && c && p.tagName && (c.tagName || c.nodeName)) {
				p.appendChild(c);
			}
		}
		, val: function (p, v) {
			var tn = document.createTextNode(v);
			p.appendChild(tn);
		}
	};
})(jQuery);