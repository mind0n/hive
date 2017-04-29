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
            rlt.send = function(url, formparam, callback, sender, isSync) {
                rlt.ajax.open("POST", url, isSync);
                rlt.ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                if (rlt.as == 1) {
                    rlt.ajax.onreadystatechange = function() {
                        if (rlt.ajax.readyState == 4) {
                            content = rlt.ajax.responseText;
                            callback(sender, content, { status: rlt.ajax.status, link: url });
                        }
                    };
                } else {
                    rlt.ajax.onload = function() {
                        content = rlt.ajax.responseText;
                        callback(sender, content, { status: rlt.ajax.status, link: url });
                    };
                    rlt.ajax.onerror = function() {
                        content = rlt.ajax.responseText;
                        callback(sender, content, { status: rlt.ajax.status, link: url });
                    };
                }
                rlt.ajax.send(formparam);
            };
            rlt.invoke = function(url, r, callback) {
                this.send(url, 'json=' + joy.toJson(r), function(sender, content, s) {
                    s.request = sender;
                    if (s.status == 200) {
                        var res = joy.fromJson(content);
                        if (res) {
                            res.IsAllSuccessful = res.IsAllSuccessful == 'True';
                            res.get = function(name) {
                                for (var i = 0; i < res.Methods.length; i++) {
                                    if (res.Methods[i].Name == name) {
                                        return res.Methods[i];
                                    }
                                }
                                return null;
                            };

                            for (var i = 0; i < res.Methods.length; i++) {
                                var m = res.Methods[i];
                                if (m && m.Name && joy.MethodCallbacks && joy.MethodCallbacks[m.Name]) {
                                    joy.MethodCallbacks[m.Name](m, s);
                                }
                            }
                        }
                    } else {
                        callback(null, s);
                    }
                }, r);
            };
            rlt.call = function(url, r, callback) {
                this.send(url, 'xml=' + joy.obj2xml(r, 'JsRequest'), function(sender, content, s) {
                    if (s.status == 200) {
                        s.sender = sender;
                        var o = joy.json(content);
                        var t = typeof(o);
                        s.raw = content;
                        s.isObj = (t == 'object');
                        s.isSuccess = false;
                        if (s.isObj) {
                            s.isAllSuccess = o.IsAllSuccessful == 'True';
                            o.get = function(name) {
                                for (var i = 0; i < o.Methods.length; i++) {
                                    if (o.Methods[i].Name == name) {
                                        return o.Methods[i];
                                    }
                                }
                                return null;
                            };
                            for (var i = 0; i < o.Methods.length; i++) {
                                var m = o.Methods[i];
                                if (joy.MethodCallbacks[m.Name]) {
                                    s.isSuccess = m.IsNoException == 'True';
                                    joy.MethodCallbacks[m.Name](o.get(m.Name), s);
                                }
                            }
                            if (callback) {
                                callback(o, s);
                            }
                        } else {
                            if (callback) {
                                callback(o, s);
                            }
                        }
                    }
                });
            };
        }
        return rlt;
    };
})(jQuery);
         