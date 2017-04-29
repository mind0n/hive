(function ($) {
    $.userControls.Cell = function (cfg, callback) {
        var r = {
            tag: 'div',
            className: 'cell',
            val: function (data, append, iscallback) {
                if (iscallback && this.vc) {
                    this.vc(data, append);
                    return;
                }
                if (data.$$) {
                    data = data.$$;
                }
                if (data && (data.url || data.ins)) {
                    this.mask = $.mask({ el: this, data:'Loading ...' });
                }
                if (!data){
                    $.val(data, this.$content, append);
                }
                if (data.url) {
                    if (!cfg || !cfg.debug) {
                        $.r.send(data.url, null, function (sender, content, status) {
                            if (content) {
                                if (content.indexOf('{') == 0) {
                                    content = $.fromJSON(content);
                                }
                            }
                            if (sender.mask) {
                                $(sender.mask).remove();
                            }
                            if (sender.onunval) {
                                sender.onunval(sender);
                            }
                            sender.val(content, append, true);
                            var n = status.link.indexOf('#');
                            if (status.link && n > 0) {
                                var arg = null;
                                var act = status.link.substr(n + 1, status.link.length - n - 1);
                                var args = act.split(/,/ig);
                                act = args[0];
                                if (args.length > 1) {
                                    arg = args[1];
                                }
                                if (window[act]) {
                                    var tt = typeof (window[act]);
                                    //debugger;
                                    if (tt == 'function') {
                                        window[act](sender, status, arg);
                                    } else {
                                        debugger;
                                    }
                                }
                            }
                            //$.val(content, sender.$content, append);
                        }, this, false, data.method || 'GET');
                    }
                } else if (data.ins) {
                    if (!cfg || !cfg.debug) {
                        $.qr.send(data.ins, data.act, data.arg, this, function (sender, cnt, s) {
                            $(sender.mask).remove();
                            sender.val(cnt, append, true);
                        });
                    }
                } else if (this.vc) {
                    this.vc(data, append);
                } else {
                    $.val(data, this.$content, append);
                }
            },
            $: [{
                tag: 'div', alias: 'relative', className: 'relative',
                $: [{ tag: 'div', alias: 'content', className: 'body' }]
            }]
        };
        if (cfg) {
            //$.extend(r, cfg);
            joy.extend(r, cfg);
        }
        if (callback) {
            callback(r);
        }
        return r;
    }

    $.userControls.CenterCell = new $.userControls.Cell({
        className:'cover',
        onmade: function () {
            this.$relative.style.display = 'table-cell';
            this.$relative.style.verticalAlign = 'middle';
            this.val('Loading ...');
            this.$content.style.boxSizing = 'border-box';
        }
        , style:{
            position: 'absolute',
            display: 'table',
            left: 0, right: 0, top: 0, bottom: 0,
            textAlign:'center',
            background: '#F0F0F0',
        }
    });

})(jQuery);