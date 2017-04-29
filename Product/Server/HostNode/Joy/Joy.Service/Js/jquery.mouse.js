(function ($) {
    var joy = $.joy;
    var regs = {};
    var is = {};

    function getEl(cfg) {
        return $(cfg.p || cfg.el).dom();
    }

    function getResizeTarget(cfg) {
        return $(cfg.rel).dom();
    }

    function def(c) {
        var el = getEl(c);
        if (el) {
            var r = el.rect();
            c.vx = r.left;
            c.vy = r.top;
            c.vw = r.right - r.left;
            c.vh = r.bottom - r.top;
        }
    }

    function validateInWindow(c) {
        var w = window.innerWidth;
        var h = window.innerHeight;
        var r = { x: 0, y: 0, w: w - 1, h: h - 1};
        if (c.vw > r.w) {
            c.vw = r.w;
        }
        if (c.vh > r.h) {
            c.vh = r.h;
        }
        if (c.vx < r.x) {
            c.vx = r.x+1;
        }
        if (c.vx + c.vw > r.w || $.ms.cx > r.w) {
            if (c.isresize) {
                c.vx = 0;
            } else {
                c.vx = r.w - c.vw;
            }
            c.vw = 0;
        }

        if (c.vy < r.y) {
            c.vy = r.y+1;
        }
        if (c.vy + c.vh > r.h || $.ms.cy > r.h) {
            if (c.isresize) {
                c.vy = 0;
            } else {
                c.vy = r.h - c.vh;
            }
            c.vh = 0;
        }
        //if (c.el && c.el.val) {
        //    c.el.val(c.vx + ',' + c.vy + '-' + c.vw + ',' + c.vh + '[' + w + ',' + h + ']');
        //}
    }

    $.ms = {
        move: new Array(),
        down: new Array(),
        up: new Array(),
        curt: null,
        hindex: 99999,
        lindex: 20,
        behaviors: {
            move: {
                sc: function (c) {
                    var el = getEl(c);
                    el.$bak$ = { rect: el.rect() };
                    return true;
                }, mc: function (c) {
                    var ox = c.mouse.x2 - c.mouse.x1;
                    var oy = c.mouse.y2 - c.mouse.y1;
                    var el = c.el;
                    c.vx = el.$bak$.rect.left + ox;
                    c.vy = el.$bak$.rect.top + oy;
                    c.vh = el.$bak$.rect.h();
                    c.vw = el.$bak$.rect.w();
                    return true;
                }, pc: function (c) {
                    if (!c.novalidate) {
                        validateInWindow(c);
                    }
                    return true;
                }
            }
            , resize_bf: {
                sc: function(c){
                    var el = getEl(c);

                    el.$bak$ = { rect: el.rect() };
                    return true;
                }
                , mc: function (c) {
                    c.isresize = true;
                    var ox = c.horizantal ? c.mouse.x2 - c.mouse.x1 : 0;
                    var oy = c.vertical ? c.mouse.y2 - c.mouse.y1 : 0;
                    var ela = c.el;
                    var elb = c.elb;
                    if (ela) {
                        var ow = ela.$bak$ ? ela.$bak$.rect.w() : 0;
                        var oh = ela.$bak$ ? ela.$bak$.rect.h() : 0;
                        if (ox != 0) {
                            //debugger;
                            var vw = parseInt(ow) + ox;
                            //ela.style.width = vw + 'px';
                            c.vw = vw;
                        } else {
                            c.vw = 0;
                        }
                        if (oy != 0) {
                            var vh = parseInt(oh) + oy;
                            //ela.style.height = vh + 'px';
                            c.vh = vh;
                        } else {
                            c.vh = 0;
                        }
                    }
                    if (elb) {
                        var ow = ela.$bak$ ? ela.$bak$.rect.w() : 0;
                        var oh = ela.$bak$ ? ela.$bak$.rect.h() : 0;
                        if (ox != 0) {
                            var vw = parseInt(ow) - ox;
                            //elb.style.width = vw + 'px';
                            c.vwb = vw;
                        }
                        if (oy != 0) {
                            var vh = parseInt(oh) - oy;
                            //elb.style.height = vh + 'px';
                            c.vhb = vh;
                        }
                    }
                    if (c.movehandle) {
                        c.movehandle(ox, oy);
                    }
                }
                , pc: function (c) {
                    if (!c.novalidate) {
                        validateInWindow(c);
                    }
                    return true;
                }
            }
            , monitor: {

            }
            , amove: {
                sc: function (c) {
                    var el = getEl(c);
                    el.$bak$ = { rect: el.rect() };
                    return true;
                }, mc: function (c) {
                    var ox = c.mouse.px2 - c.mouse.px1;
                    var oy = c.mouse.py2 - c.mouse.py1;
                    var el = c.el;
                    c.vx = el.$bak$.rect.left + ox;
                    c.vy = el.$bak$.rect.top + oy;
                    c.vh = el.$bak$.rect.h();
                    c.vw = el.$bak$.rect.w();
                    return true;
                }, pc: function (c) {
                    if (!c.novalidate) {
                        validateInWindow(c);
                    }
                    return true;
                }
            }
            , aresize_bf: {
                sc: function (c) {
                    var el = getEl(c);

                    el.$bak$ = { rect: el.rect() };
                    return true;
                }
                , mc: function (c) {
                    c.isresize = true;
                    var ox = c.horizantal ? c.mouse.x2 - c.mouse.x1 : 0;
                    var oy = c.vertical ? c.mouse.y2 - c.mouse.y1 : 0;
                    var ela = c.el;
                    var elb = c.elb;
                    if (ela) {
                        var ow = ela.$bak$ ? ela.$bak$.rect.w() : 0;
                        var oh = ela.$bak$ ? ela.$bak$.rect.h() : 0;
                        if (ox != 0) {
                            //debugger;
                            var vw = parseInt(ow) + ox;
                            //ela.style.width = vw + 'px';
                            c.vw = vw;
                        } else {
                            c.vw = 0;
                        }
                        if (oy != 0) {
                            var vh = parseInt(oh) + oy;
                            //ela.style.height = vh + 'px';
                            c.vh = vh;
                        } else {
                            c.vh = 0;
                        }
                    }
                    if (elb) {
                        var ow = ela.$bak$ ? ela.$bak$.rect.w() : 0;
                        var oh = ela.$bak$ ? ela.$bak$.rect.h() : 0;
                        if (ox != 0) {
                            var vw = parseInt(ow) - ox;
                            //elb.style.width = vw + 'px';
                            c.vwb = vw;
                        }
                        if (oy != 0) {
                            var vh = parseInt(oh) - oy;
                            //elb.style.height = vh + 'px';
                            c.vhb = vh;
                        }
                    }
                    if (c.movehandle) {
                        c.movehandle(ox, oy);
                    }
                }
                , pc: function (c) {
                    if (!c.novalidate) {
                        validateInWindow(c);
                    }
                    return true;
                }
            }

        },
        activate: function(el){
            if ($.ms.curt) {
                if ($.ms.curt.ondrop) {
                    $.ms.curt.ondrop();
                }
            }
            $.ms.curt = el;
            if (el.ondrag) {
                el.ondrag();
            }
        },
        updatePos: function(e){
            var evt = e || event;
            var m = $.ms;
            m.px = evt.pageX;
            m.py = evt.pageY;
            m.cx = evt.clientX;
            m.cy = evt.clientY;
        },
        regist: function (cfg) {
            if (!cfg) {
                debugger;
                return $.ms;
            }
            if (!cfg.ox) {
                cfg.ox = 0;
            }
            if (!cfg.oy) {
                cfg.oy = 0;
            }
            var el = cfg.el;
            var key = cfg.k;
            var handle = cfg.h
            if (!el) {
                debugger;
                return $.ms;
            }
            if (!key) {
                key = joy.uid('ms_' + el.id);
            }
            el.$ms$ = {};
            var i = cfg;
            regs[key] = i;

            if (handle) {
                handle.$ms$ = key;
                $(handle).mousedown(function (e) {
                    var element = e.srcElement || e.target;
                    if (!element) {
                        debugger;
                    }
                    var key = element.$ms$;
                    if (key) {
                        var r = regs[key];
                        if (r) {
                            r.mouse = { x1: $.ms.cx, y1: $.ms.cy, px1: $.ms.px, py1: $.ms.py, evt: e || event };
                            if (!r.behavior || !r.behavior.sc || r.behavior.sc(r)) {
                                var mel = getEl(r);
                                $.ms.activate(mel);
                                $.ms.curt = mel;
                                is[key] = r;
                            }
                            if (r.sc) {
                                r.sc(r);
                            }
                        }
                    } else {
                        debugger;
                    }
                });
            }
            return $.ms;
        },
        unreg: function (key) {
            joy.removeprop(regs, key);
        },
        fire: function (en, e) {
            if (!en) {
                return;
            }
            var list = this[en];
            if (en == 'move') {
                this.mouseover(e);
                this.drag(e);
            }
            if (en == 'up') {
                this.drop();
            }
            if (!list || !list.length || list.length < 1) {
                return;
            }
            for (var i = 0; i < list.length; i++) {
                var f = list[i];
                if (f && typeof (f) == 'function') {
                    f(this, e);
                } else {
                    debugger;
                }
            }
        },
        drop: function () {
            var tis = is;
            is = {};
            for (var i in tis) {
                var item = tis[i];
                var t = getEl(item);
                //t.style.position = item.bak.position;
                //if (t.style.position == 'relative') {
                //    joy.restore(t.style, item.bak, { zIndex: true });
                //}
                //t.style.left = parseInt(t.style.left) + 'px';
                //t.style.top = parseInt(t.style.top) + 'px';
                item.mouse.x2 = $.ms.cx;
                item.mouse.y2 = $.ms.cy;
                item.mouse.px2 = $.ms.px;
                item.mouse.py2 = $.ms.py;
                if (item.behavior && item.behavior.dc) {
                    item.behavior.dc(item, t);
                }
                $.joy.removeprop(tis, i);
                if (item.ac) {
                    item.ac(item, t);
                }
                t.$bak$ = {};
                item.mouse.evt = null;
            }
            tis = null;
            delete tis;
        }
        ,mouseover: function(e){
            for (var i in regs) {
                var r = regs[i];
                var el = getEl(r);
                var rect = el.rect();
                if (el.mouseover || el.mouseout) {
                    if (rect.pointin($.ms.cx, $.ms.cy)) {
                        el.$ms$.mouseover = true;
                        if (el.mouseover) {
                            el.mouseover(r);
                        }
                    } else {
                        if (el.$ms$.mouseover) {
                            el.$ms$.mouseover = false;
                            if (el.mouseout) {
                                el.mouseout(r);
                            }
                        }
                    }
                }
            }
        }
        , drag: function (e) {
            for (var i in is) {
                var item = is[i];
                var mel = getEl(item);
                if (mel) {
                    def(item);
                    item.mouse.x2 = $.ms.cx;
                    item.mouse.y2 = $.ms.cy;
                    item.mouse.px2 = $.ms.px;
                    item.mouse.py2 = $.ms.py;
                    if (item.behavior && item.behavior.mc) {
                        joy.invoke(item.behavior.mc, item);
                    } else {
                        item.vx = $.ms.cx;
                        item.vy = $.ms.cy;

                    }
                    if (item.mc) {
                        item.mc(item);
                    }
                    if (item.behavior.pc && !joy.invoke(item.behavior.pc, item)) {
                        continue;
                    }
                    if (item.vx) {
                        mel.style.left = item.vx + 'px';
                    }
                    if (item.vy) {
                        mel.style.top = item.vy + 'px';
                    }
                    if (item.vw) {
                        mel.style.width = item.vw + 'px';
                    }
                    if (item.vh) {
                        mel.style.height = item.vh + 'px';
                    }
                    item.vx = 0;
                    item.vy = 0;
                    item.vw = 0;
                    item.vh = 0;
                } else {
                    debugger;
                }
            }
        }
    };
    $(function () {
        var m = $.ms;
        $(document).mousemove(function (e) {
            m.updatePos(e);
            m.fire('move', e);
        });
        $(document).mouseup(function (e) {
            m.updatePos(e);
            m.fire('up', e);
        });
    });
})(jQuery);