(function($) {
    $.r = new function() {
        var ajax, as, rlt;
        try {
            ajax = new ActiveXObject("Microsoft.XMLHTTP");
            as = 1;
        } catch(e) {
            try {
                ajax = new ActiveXObject("Msxml2.XMLHTTP");
                as = 1;
            } catch(e) {
                try {
                    ajax = new XMLHttpRequest();
                    as = 2;
                } catch(e) {
                    ajax = null;
                    as = 0;
                }
            }
        }
        if (ajax) {
            rlt = {};
            rlt.ajax = ajax;
            rlt.as = as;
            rlt.send = function(url, formparam, callback, sender, isSync, method) {
                rlt.ajax.open(method || "POST", url, !isSync);
                rlt.ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                if (rlt.as == 1) {
                    rlt.ajax.onreadystatechange = function() {
                        if (rlt.ajax.readyState == 4) {
                            content = rlt.ajax.responseText;
                            callback(sender, content, { status: rlt.ajax.status, link: url, o: content.indexOf('{') == 0 ? $.fromJSON(content) : null });
                        }
                    };
                } else {
                    rlt.ajax.onload = function() {
                        content = rlt.ajax.responseText;
                        callback(sender, content, { status: rlt.ajax.status, link: url, o: content.indexOf('{') == 0 ? $.fromJSON(content) : null });
                    };
                    rlt.ajax.onerror = function() {
                        content = rlt.ajax.responseText;
                        callback(sender, content, { status: rlt.ajax.status, link: url, o: content.indexOf('{') == 0 ? $.fromJSON(content) : null });
                    };
                }
                rlt.ajax.send(formparam);
            };
        }
        return rlt;
    };
    $.qr = new function () {
        var h;
        function q() {
            var gid = 0;
            var queue = [];
            queue.add = function (i, qid) {
                var t = typeof (i);
                if (t == 'object') {
                    i.queueid = qid ? qid : ++gid;
                }
                this[this.length] = i;
            };
            queue.clear = function () {
                for (var i = 0; i < this.length; i++) {
                    var u = this.pop();
                    delete u;
                }
            };
            return queue;
        }
        var rlt = {
            icache: q(),
            //qcache: q(),
            send: function (ins, action, args, sender, callback) {
                var ic = this.icache;
                ic.add({ ins: ins, act: action, arg: args, cb: callback, sd: sender });
            },
            flush: function (name, url) {
                window.clearTimeout(h);
                if (!name) {
                    name = 'i';
                }
                var cache = rlt[name + 'cache'];
                if (!cache) {
                    debugger;
                    return;
                } else if (cache.length < 1) {
                    return;
                }
                rlt[name + 'cache'] = q();
                if (cache) {
                    var req = {
                        actions:q()
                    };
                    for (var i = 0; i < cache.length; i++) {
                        var t = cache[i];
                        var ad = { ins: t.ins, act: t.act, arg: t.arg };
                        req.actions.add(ad, t.queueid);
                    }
                    var s = $.toJSON(req);
                    $.r.send(url || '/', 'reqjson=' + escape(s), function (cache, b, c) {
                        var o = c.o;
                        for (var i = 0; i < cache.length; i++) {

                            var item = cache[i];
                            for (var j = 0; j < o.actions.length; j++) {
                                var ritem = o.actions[j];
                                if (item.queueid == ritem.queueid) {
                                    var rlt = ritem.rlt;
                                    if (rlt.indexOf('{') == 0) {
                                        rlt = $.fromJSON(rlt);
                                    }
                                    var content = rlt;
                                    //var t = typeof (rlt);
                                    
                                    if (rlt){
                                        if (rlt.Result) {
                                            content = rlt.Result;
                                        } else if (rlt.ResultSet) {
                                            content = rlt.ResultSet;
                                            if (content.length == 1) {
                                                content = content[0];
                                            }
                                        }        
                                    }
                                    item.cb(item.sd, content, { status: c.status, o: rlt });
                                }
                            }

                        }
                    }, cache);
                    req.actions = null;
                    req = null;
                }
                h = window.setTimeout(rlt.flush, 1000);
            }
        };
        h = window.setTimeout(rlt.flush, 1000);
        return rlt;
    };
})(jQuery);
         