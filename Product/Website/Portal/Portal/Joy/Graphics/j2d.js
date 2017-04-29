(function () {
    function calcPos(c, rightPos, bottomPos) {
        if (!c) {
            return;
        }
        if (c.x !== nul) {
            rightPos[0] += c.x;
            bottomPos[0] += c.x;
        }
        if (c.y !== nul) {
            rightPos[1] += c.y;
            bottomPos[1] += c.y;
        }
    }

	function formatpos(origin, nul) {
		if (origin[0] === nul) {
			origin[0] = 0;
		}
		if (origin[1] === nul) {
			origin[1] = 0;
		}
	}
	function addPos(origin, offset) {
		formatpos(origin);
		formatpos(offset);
        origin[0] += offset[0];
        origin[1] += offset[1];
        return origin;
    }
    
    function Box(cfg, parent, nul) {
        var loc, cur;
        var host = {};
        host.updatePos = function (delta, t) {
            var layout = {};
        	if (!t) {
                t = this;
            }
            if (!delta) {
                delta = [0, 0];
            }
            var c = t.config();
            var theme = t.config().theme || {};
            if (c) {
		        layout = c.layout;
	        }
	        var p = t.$parent;
	        var borderOffset = layout.notCollapseBorder ? j2d.pixOffset : 0;
            loc = [layout.x || borderOffset, layout.y || borderOffset];
            addPos(loc, delta);
            if (p) {
            	var padding = 0;
	            if (p.config() && p.config().layout) {
		            padding = p.config().layout.padding || 0;
	            }
            	addPos(loc, p.location(true));
	            addPos(loc, [padding, padding]);
            }
            //alert(loc.x);
            cur = [loc.x ? loc.x : 0, loc.y ? loc.y : 0];
            var bw = j2d.edge(theme.borderWidth);
            cur[0] += bw.left;
            cur[1] += bw.top;
            if (t.$ && t.$.length > 0) {
                for (var i = 0; i < t.$.length; i++) {
                    var b = t.$[i];
                    b.updatePos(cur);
                	//console.log(b.display());
                    if (b.display == 'line') {
                    	addPos(cur, [0, b.height() + borderOffset]);
                        cur[0] = loc.x ? loc.x : 0;
                    } else {
                        addPos(cur, [b.width() + borderOffset, 0]);
                    }
                }
            }
        };
        host.render = function (context, isSkipUpdate) {
            if (!isSkipUpdate) {
                this.updatePos();
            }
            var l, d, oh = true;
            var c = this.config();
            var ctx = context || c.context;
            if (!ctx) {
                alert('ctx null');
                return;
            }
            if (c) {
                l = c.layout || {};
                d = c;
                c = c.theme || {};
            }
            var loc = this.location();
            var w = this.width();
            var h = this.height();
            ctx.fillStyle = c.backcolor || 'white';
            ctx.fillRect(loc[0], loc[1], w, h);

            ctx.strokeStyle = c.bordercolor || 'black';
            ctx.lineWidth = c.borderwidth || 1;
            ctx.strokeRect(loc[0], loc[1], w, h);

            ctx.textAlign = c.align || 'left';
            ctx.textBaseline = c.valign || 'middle';
            ctx.font = c.font || 'normal 12px arial';
            ctx.fillStyle = c.color || 'black';
            var padding = l.padding || 0;

            if (oh) {
                ctx.save();
                ctx.beginPath();
                ctx.rect(loc[0] + padding, loc[1] + padding, w - padding * 2 - j2d.hpixOffset, h - padding * 2 - j2d.hpixOffset);
                ctx.clip();
            }
            if (d.text) {
                var txt = d.text;
                var tw = ctx.measureText(txt).width;
                var offset = this.offset();
                var pw = padding * 2 + offset.left + offset.right;
                if (tw + pw > w) {
                    while (ctx.measureText(txt + '...').width + pw >= w) {
                        txt = txt.substr(0, txt.length - 2);
                    }
                    txt += '...';
                }
                //ctx.fillText(txt, loc[0] + parseInt(padding), loc[1] + parseInt(h / 2));
                if (this.config().layout.valign == 'middle') {
                    ctx.fillText(txt, loc[0] + padding + offset.left + j2d.hpixOffset, loc[1] + h / 2 + j2d.hpixOffset);
                }
            }
            if (this.$ && this.$.length > 0) {
                for (var k = 0; k < this.$.length; k++) {
                    var b = this.$[k];
                    b.render(ctx, true);
                }
            }
            if (oh) {
                ctx.restore();
            }
        };
        host.init = function(schema) {
            this.$schema = schema;
        };
        host.config = function () {
            return this;
        };
        host.offset = function (o) {
            if (!o && this.config().layout) {
                o = this.config().layout.offset;
            }
            if (o) {
                return { top: o[0], right: o[1], bottom: o[2], left: o[3] };
            }
            return { top: 0, right: 0, bottom: 0, left: 0 };
        };
        host.width = function (calcOffset) {
            var layout = this.config().layout || {};
            var theme = this.$parent ? this.$parent.config().theme : {};
            var bw = j2d.edge(theme.borderWidth);
            var rlt = 0;
            if (layout.w == 'extend') {
                if (this.$parent) {
                    var w = this.$parent.config().layout.w;
                    var p = this.$parent.config().layout.padding || 0;
                    w -= p * 2 + j2d.pixOffset * 2;
                    if (this.$parent.config().layout) {
                        w -= this.offset(this.$parent.config().layout.offset).left;
                        w -= this.offset(this.$parent.config().layout.offset).right;
                    }
                    rlt = w;
                    rlt -= bw.left + bw.right;
                } else {
                    debugger;
                }
            } else {
                rlt = layout.w;
            }
            return rlt;
        };
        host.height = function () {
            var layout = this.config().layout || {};
            var theme = this.$parent ? this.$parent.config().theme : {};
            var bw = j2d.edge(theme.borderWidth);
            var rlt = 0;
            if (layout.h == 'extend') {
                if (this.$parent) {
                    var h = this.$parent.config().layout.h;
                    var p = this.$parent.config().layout.padding || 0;
                    h -= p * 2;// + j2d.pixOffset * 2;
                    
                    if (this.$parent.config().layout) {
                        h -= this.offset(this.$parent.config().layout.offset).top;
                        h -= this.offset(this.$parent.config().layout.offset).bottom;
                    }
                    rlt = h;
                    rlt -= bw.top + bw.bottom;
                    //alert(bw.top + bw.bottom);
                } else {
                    debugger;
                }
            } else {
                rlt = layout.h;
            }
            return rlt;
        };
        host.location = function (isOrigin) {
            if (isOrigin) {
                return loc;
            }
            return [Math.round(parseInt(loc[0])) + j2d.hpixOffset, Math.round(parseInt(loc[1])) + j2d.hpixOffset];
        };
        host.cursor = function () {
            return cur;
        };

        host.$root = parent ? parent.$root : null;
        host.$ = j2d.ChildBox(host.$root);
        host.$parent = parent;
        for (var i in cfg) {
	        if (i != '$') {
		        host[i] = cfg[i];
	        }
        }

		if (!cfg) {
			debugger;
			return host;
		} else if (cfg.$) {
            for (var j = 0; j < cfg.$.length; j++) {
                var item = cfg.$[j];
                var b = Box(item, host);
                host.$.add(b);
            }
		}
        return host;
    }

    function j2d(canvas) {
        var cv = canvas?joy(canvas):null;
        if (cv) {
            cv.resize();
        }
        var ctx = cv?cv.getContext('2d'):null;
        return {
            createBox: function(cfg, p) {
                cfg.context = ctx;
                var b = Box(cfg, p);
                return b;
            }
        };
    }

    j2d.hpixOffset = 0.5;
    j2d.pixOffset = 1;
    j2d.dpixOffset = 2;
    j2d.edge = function (o) {
        var r = { left: 0, top: 0, right: 0, bottom: 0 };
        if (o) {
            if (o.length > 0) {
                r.top = o[0];
            }
            if (o.length > 1) {
                r.right = o[1];
            }
            if (o.length > 2) {
                r.bottom = o[2];
            }
            if (o.length > 3) {
                r.left = o[3];
            }
        }
        return r;
    };
    j2d.ChildBox = function(root) {
        var r = Joy.List();
        r.addBox = function (box) {
            if (box.alias && box.alias.trim().length > 0) {
                var alias = box.alias.trim();
                if (alias != 'root' && alias != 'parent') {
                    root['$' + alias] = box;
                }
            }
            this.add(box);
        };
        return r;
    };
    self.j2d = j2d;
    self.box = Box;
})();