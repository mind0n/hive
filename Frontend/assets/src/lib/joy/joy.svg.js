(function () {
	try {
		SVGElement.prototype.appendTo = joy.appendTo;
	} catch (e) {
		Object.prototype.appendTo = joy.appendTo;
	}
	joy.creators.svg = function () {
		function svgSetAttr(o, n, v) {
			if (n.indexOf('$') < 0) {
				if (o.setAttributeNS) {
					o.setAttributeNS(null, n, v);
				} else if (o.setAttribute) {
					o.setAttribute(n, v);
				} else {
					o[n] = v;
				}
			} else {
				o[n] = v;
			}
		}
		function svgCreateEl(n) {
			return document.createElementNS("http://www.w3.org/2000/svg", n);
		}
		var r = {
			createEl: svgCreateEl
            , create: function (n, j, c) {
            	var el = null;
            	if (this[n]) {
            		el = this[n](j, c);
            	} else {
            		el = this.createEl(n);
            		joy.extend(el, j, { setAttr: svgSetAttr });
            	}
            	return el;
            }
            , created: function (el) {
            	if (!el.style.zIndex) {
            		el.style.zIndex = 100;
            	}
            	el.$transform$ = {
            		translate: [0, 0]
                    , otranslate: [0, 0]
                    , location: [0, 0]
                    , rotate: 0
                    , scale: [1, 1]
                    , toString: function (sequence) {
                    	return 'translate(' + this.translate[0] + ',' + this.translate[1] + ')'
                            + ' rotate(' + this.rotate + ')'
                            + ' scale(' + this.scale[0] + ',' + this.scale[1] + ')'
                    	;
                    }
            	};
            	el.getContainer = function (layer) {
            		try {
            			var r = this.$root.getContainer(layer); //layer && this.$root['$' + layer] ? this.$root['$' + layer] : this.$root.$shapelayer;
            			return r;
            		} catch (e) {
            			debugger;
            			return null;
            		}
            	};
            	el.bbshow = function () {
            		var bb = this.getBBox();
            		this.$root.add({ svg: 'rect', x: bb.x, y: bb.y, width: bb.width, height: bb.height, style: { fill: 'none', strokeWidth: 1, stroke: 'green' } });
            	};
            	el.lineTo = function (tel) {
            		var that = this;
            		var st = that.$transform$;
            		var tt = tel.$transform$;
            		var p = this.$root.add({ svg: 'path', style: { zIndex: 1 }, stroke: 'black', d: 'M ' + st.translate[0] + ' ' + st.translate[1] + ' l ' + tt.translate[0] + ' ' + tt.translate[1] }, null, true, true);
            		p.bbshow();
            	};
            	el.translate2 = function (x, y, commit) {
            		if (x.indexOf && x.indexOf('%') > 0) {
            			x = this.$root.rect().w() * parseInt(x) / 100;
            		} else {
            			x = parseInt(x);
            		}
            		if (y.indexOf && y.indexOf('%') > 0) {
            			y = this.$root.rect().h() * parseInt(y) / 100;
            		} else {
            			y = parseInt(y);
            		}

            		if (!commit) {
            			this.$transform$.translate[0] = this.$transform$.otranslate[0] + x;
            			this.$transform$.translate[1] = this.$transform$.otranslate[1] + y;
            		} else {
            			this.$transform$.translate[0] = this.$transform$.otranslate[0] + x;
            			this.$transform$.translate[1] = this.$transform$.otranslate[1] + y;
            			this.$transform$.otranslate[0] = this.$transform$.otranslate[0] + x;
            			this.$transform$.otranslate[1] = this.$transform$.otranslate[1] + y;
            		}
            		$(this).attr('transform', this.$transform$.toString());
            		return this;
            	};
            	el.scale2 = function (sc, lm, orp, scene) {
            		function f(m, n, min, max) {
            			if (m == 0) {
            				return n;
            			}
            			if (n == 0) {
            				n = 1;
            			}
            			var r = n / (m * 2) + n;
            			if (min && r < min) {
            				r = min;
            			}
            			if (max && r > max) {
            				r = max;
            			}
            			return r;
            		}
            		if (!scene) {
            			scene = this;
            		}
            		var x = sc[0];
            		var y = sc[1];
            		if (lm) {
            			var xmin = lm.min[0];
            			var ymin = lm.min[1];
            			var xmax = lm.max[0];
            			var ymax = lm.max[1];
            			this.$transform$.scale[0] = f(x, this.$transform$.scale[0], xmin, xmax);
            			this.$transform$.scale[1] = f(y, this.$transform$.scale[1], ymin, ymax);
            		} else {
            			this.$transform$.scale[0] = f(x, this.$transform$.scale[0]);
            			this.$transform$.scale[1] = f(y, this.$transform$.scale[1]);
            		}
            		var s = this.$transform$.toString();
            		if (orp) {
            			var orx = orp[0];
            			var ory = orp[1];
            			this.style.transformOrigin = '0px 0px';
            		}
            		this.style.transform = s;
            		//$(this).attr('transform', s);
            		//this.setAttributeNS(null, 'transform', s);
            		return this;
            	};
            	el.rotate2 = function (degree) {
            		this.$transform$.rotate = degree;
            		var s = this.$transform$.toString();
            		debugger;
            		this.style.transform = s;
            		//$(this).attr('transform', s);
            		//this.setAttributeNS(null, 'transform', s);
            		return this;
            	};
            	el.append = function (target, switchToChild, isprepend) {
            		var r = !isprepend ? joy.append(this, target, switchToChild) : joy.prepend(this, target, switchToChild);
            		return r;
            	};
            	el.rect = function () {
            		var r = null;
            		if (this.getBBox) {
            			r = this.getBBox();
            			r.right = r.x + r.width;
            			r.bottom = r.y + r.height;
            			r.left = r.x;
            			r.top = r.y;
            			r.w = function () {
            				return this.width;
            			};
            			r.h = function () {
            				return this.height;
            			};
            		} else {
            			r = this.getBoundingClientRect();
            			r.w = function () {
            				return this.right - this.left;
            			}
            			r.h = function () {
            				return this.bottom - this.top;
            			}
            		}
            		return r;
            	};
            }
            , append: function (p, c) {
            	if (p && c && p.tagName && c.tagName) {
            		p.appendChild(c);
            	}
            }
            , val: function (p, v) {
            	var tn = document.createTextNode(v);
            	p.appendChild(tn);
            }
            , g: function (j, c) {
            	var el = document.createElementNS("http://www.w3.org/2000/svg", "g");
            	el.$isgroup$ = true;
            	el.$ispackage$ = false;
            	joy.extend(el, j, { setAttr: svgSetAttr });
            	return el;
            }
            , svg: function (j, c) {
            	var el = document.createElementNS("http://www.w3.org/2000/svg", "svg");
            	el.$isgroup$ = true;
            	joy.extend(el, j, { setAttr: svgSetAttr });
            	el.init = function (target) {
            		this.appendTo(target);
            		return this.$svgroup;
            	}
            	el.toSvgPoint = function (p) {
            		var svg = this;
            		//debugger;
            		var pt = svg.createSVGPoint();
            		pt.x = p[0];
            		pt.y = p[1];
            		var r = pt.matrixTransform(svg.getScreenCTM().inverse());
            		return r;
            	}
            	return el;
            }
            , p: function (j, c) {
            	var r = this.g(j, c);
            	r.$isgroup$ = false;
            	return r;
            }
            , svp: function (j, c) {
            	var r = this.svg(j, c);
            	r.$isgroup$ = false;
            	return r;
            }
		}
		return r;
	}();
})();