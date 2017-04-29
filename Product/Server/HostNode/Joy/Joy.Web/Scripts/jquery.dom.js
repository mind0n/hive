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

    $.log = function (t, maxdepth, n, depth) {
        if (!$.log.filter) {
            $.log.filter = function(name) {
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
            console.log(txt);
        } else if (depth < maxdepth) {
            this.log(n, maxdepth, null, depth);
            for (var i in t) {
                if ($.log.filter(i)) {
                    continue;
                }
                try {
                    $.log(t[i], maxdepth, i, depth + 1);
                } catch (e) {
                    $.log("Error: " + i + " cannot be logged.\t" + e);
                }
            }
        }
    };
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
    $.create = function(tag, cfg, autoDoc) {
        var el = document.createElement(tag);
        for (var i in cfg) {
            el[i] = cfg[i];
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
    
    $.fn.dom = function (index, undef) {
        var list = this;
        if (list.length > 1 && index) {
            return list.get(index);
        } else if (list.length == 1) {
            return list[0];
        }
        return [];
    };
    
    $.fn.domerge = function (target, noverride, excludes) {
        copy(this.dom(0), target, !noverride, excludes);
        return this;
    };
    $.userControls = {};
    $.fn.drawicons = function (cfg) {
        if (!cfg){
            cfg = {fillStyle:'gray'};
        }
        var h = 10;
        var w = 10;
        var cvs = this.dom(0);
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
    $.use = function (ctrl, cfg, pe) {
        function make(json, rootEl, parEl) {
            var rlt = null;
            if (json) {
                if (json.tag) {
                    var el = document.createElement(json.tag);
                    el.getparent = function(k, v) {
                        var p = this.$parent;
                        if (p[k] == v) {
                            return p;
                        }
                        while (p.$parent) {
                            p = p.$parent;
                            if (p[k] == v) {
                                return p;
                            }
                        }
                        return null;
                    };
                    if (!parEl) {
                        parEl = rootEl;
                    }
                    if (!rootEl) {
                        rootEl = el;
                    }
                    if (rootEl) {
                        el.$root = rootEl;
                        if (json.alias) {
                            rootEl['$' + json.alias] = el;
                        }
                    }
                    if (parEl) {
                        el.$parent = parEl;
                    }
                    $(el).domerge(json, false, ['$', 'tag']);
                    if (json.$) {
                        var t = typeof (json.$);
                        if (t == 'object') {
                            for (var i = 0; i < json.$.length; i++) {
                                var cjs = json.$[i];
                                var cel = make(cjs, rootEl, el);
                                el.appendChild(cel);
                            }
                        } else {
                            el.innerHTML = json.$;
                        }
                    }
                    rlt = el;
                } else if (json.cn) {
                    var el = $.use(json.cn, json, parEl);
                    if (rootEl && json.alias) {
                        rootEl['$' + json.alias] = el;
                    }
                    rlt = el;
                }
            }
            if (rlt && rlt.onmake) {
                rlt.onmake(cfg);
            }
            return rlt;
        }
        if (!ctrl) {
            return null;
        }
        var t = typeof (ctrl);
        var json = ctrl;
        if (t == 'string') {
            json = $.userControls[ctrl];
        }
        if (json) {
            var el = make(json, null, pe);
            if (cfg) {
                copy(el, cfg, true);
            }
            if (el.init) {
                el.init(cfg, pe);
            }
            el.appendTo = function(id) {
                var t = $(id);
                if (t) {
                    var d = t.dom();
                    if (d) {
                        d.appendChild(this);
                    }
                }
            };
            return el;
        }
        return null;
    };
    
})(jQuery);
