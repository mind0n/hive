var joy = function () {
    var curtid = 0;
    var mergeprop = '$merge$';
    function invoke(evts, arg, ignoreNull) {
        if (!evts) {
            return true;
        }
        var t = typeof (evts);
        if (t == 'function') {
            return evts(arg);
        }
        else if (evts instanceof Array) {
            for (var i = 0; i < evts.length; i++) {
                var e = evts[i];
                if (e && typeof (e) == 'function') {
                    if (!e(arg) && !ignoreNull) {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }
    function merge(o, c, ec, eid, cfg) {
        if (!o) {
            //debugger;
            return;
        }
        if (!eid) {
            eid = joy.uid(mergeprop);
        }
        if (!c) {
            return o;
        }
        if (!c[mergeprop] || c[mergeprop] != eid) {
            c[mergeprop] = eid;
        } else {
            return;  // Cycle merge detected.
        }
        if (!(o.length > 0 && c instanceof Array)) {
            for (var i in c) {
                try {
                    if (cfg && cfg.excludes && cfg.excludes[i]) {
                        continue;
                    }
                    var t = typeof (c[i]);
                    if (t == 'object' && ec) {
                        ec(o, c, i, eid, cfg);
                    } else {
                        o[i] = c[i];
                    }
                } catch (e) {
                    console.log(i);
                    debugger;
                }
            }
        } else {
            for (var i = 0; i < c.length; i++) {
                try {
                    if (i >= o.length) {
                        break;
                    }
                    var t = typeof (c[i]);
                    if (t == 'object' && ec) {
                        ec(o[i], c[i], null, eid, cfg);
                    } else {
                        o[i] = c[i];
                    }

                } catch (e) {
                    debugger;
                }
            }
        }
    }
    function objmerge(o, c, i, eid, cfg) {
        if (i) {
            if (!o[i]) {
                o[i] = {};
            }
            merge(o[i], c[i], objmerge, eid, cfg);
        } else {
            merge(o, c, objmerge, eid, cfg);
        }
    }
    function list() {
        var r = new Array();
        r.add = function (o) {
            this[this.length] = o;
        };
        r.clear = function () {
            for (var i = this.length - 1; i >=0; i--) {
                var it = this[i];
                if (it.dispose) {
                    it.dispose();
                }
                delete this.pop();
            }
        };
        return r;
    }
    var joyrlt = {
        uid: function (prefix) {
            curtid++;
            return prefix + '_' + curtid;
        },
        removeprop: function (o, p) {
            try {
                if (o && p) {
                    if (o[p]) {
                        o[p] = null;
                        delete o[p];
                    }
                    return true;
                }
            } catch (e) {
                debugger;
            }
            return false;
        }
        , backup: function (o, ps) {
            var r = {};
            for (var i in o) {
                if (ps[i]) {
                    r[i] = o[i];
                }
            }
            return r;
        }, restore: function (o, r, ex) {
            for (var i in r) {
                if (ex && ex[i]) {
                    continue;
                }
                o[i] = r[i];
            }
            return o;
        }
        , fromJson: function (s) {
            return eval('(' + s + ')');
        }
        , invoke: invoke
        , merge: merge
        , objmerge: objmerge
        , extend: function (o, c, settings) {
            merge(o, c, objmerge, null, settings);
        }
        , list: list
    };
    return joyrlt;
}();
