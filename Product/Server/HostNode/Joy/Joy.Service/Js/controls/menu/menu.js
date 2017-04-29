(function ($) {
    $.userControls.Menu = function () {
        var r = $.userControls.Cell();
        var menuitems = joy.list();
        joy.extend(r, {
            className: 'menu cell line'
            , init: function (c) {
                joy.extend(this, c);
            }
            , vc: function (data, append) {
                this.$content.innerHTML = '';
                if (data && data.items && data.items.length > 0) {
                    var el = null;
                    for (var i = 0; i < data.items.length; i++) {
                        var json = data.items[i];
                        json.tar = this.tar;
                        el = $.u('MenuItem');
                        menuitems.add(el);
                        this.$content.appendChild(el);
                        el.$menu = this;
                        if (json.$) {
                            el.init(json.$);
                        }
                        el.val(json);
                    }
                    if (el) {
                        $(el).addClass('lastone');
                    }
                }
            }
            , size: function () {
                for (var i = 0; i < menuitems.length; i++) {
                    var mi = menuitems[i];
                    if (mi.$submenu) {
                        mi.$submenu.size();
                    }
                }
                var a = menuitems[0];
                var b = menuitems[menuitems.length - 1];
                var ra = a.getBoundingClientRect();
                var rb = b.getBoundingClientRect();
                this.style.height = rb.bottom - ra.top + 'px';
            }
            , deselect: function (force) {
                for (var i = 0; i < menuitems.length; i++) {
                    var item = menuitems[i];
                    item.deselect(true);
                }
            }
        });
        return r;
    };
    $.userControls.MenuItem = function () {
        var json = {
            tag: 'div'
            , className: 'menuitem line'
            , tar: null
            , behaviors: {
                expmi: {
                    setval: function (el) {
                        var dat = el.data;
                        if (!dat && el.$root) {
                            dat = el.$root.data;
                        }
                        el.select();
                        var tel = null;
                        if (dat.tar) {
                            tel = $(dat.tar).dom();
                        }
                        if (dat.url) {
                            if (tel && tel.val) {
                                tel.val(dat);
                            }
                        } else if (dat.func) {
                            try {
                                window[dat.func](this, tel);
                            } catch (e) {
                                debugger; // Maybe func not exist.
                            }
                        }
                    },
                    exp: function (el) {
                        el = $(el).dom();
                        if (el.$submenu) {
                            var sel = $(el.$subs);
                            if (sel.hasClass('hidden')) {
                                $(el.$subs).removeClass('hidden');
                                $(el).addClass('expanded');
                            } else {
                                $(el.$subs).addClass('hidden');
                                $(el).removeClass('expanded');
                            }
                            var root = el.rootMenu();
                            root.size();
                        }
                    }
                }
            }
            , select: function () {
                var el = this;
                $(el.$itemarea).addClass('selected');
                //$(el.$itemarea).addClass('selected');
            }
            , deselect: function (force) {
                var el = this;
                $(el.$itemarea).removeClass('selected');
                //$(el.$itemarea).removeClass('selected');
                if (el.$submenu) {
                    el.$submenu.deselect();
                }
            }
            , val: function (dat) {
                this.data = dat;
                $.val(dat.text, this.$link);
                var el = this;
                el.$submenu = null;
                if (dat.items && dat.items.length > 0) {
                    $(el.$subs).removeClass('hidden');
                    var sub = $.u('Menu');
                    sub.appendTo(el.$subs);
                    if (this.$menu && this.$menu.tar) {
                        sub.tar = this.$menu.tar;
                    }
                    sub.val(dat, true, true);
                    el.$submenu = sub;
                    sub.$paritem = el;
                    sub.size();
                    $(el.$subs).addClass('hidden');
                    $(el.$lexp).removeClass('hidden');
                    $(el.$rexp).removeClass('hidden');
                } else {
                    $(el.$lexp).addClass('hidden');
                    $(el.$rexp).addClass('hidden');
                }
            }
            , exp: function () {
                var el = this;

            }
            , rootMenu: function () {
                var m = this.$menu;
                if (m.$paritem) {
                    return m.$paritem.rootMenu();
                }
                return m;
            }
            , init: function (c) {
                if (c) {
                    joy.extend(this, c);
                }
                if (!this.behavior || typeof(this.behavior) != 'function') {
                    this.behavior = this.behaviors.expmi;
                }
                $.ms.regist({ el: this, behavior: $.ms.behaviors.monitor });
            }
            , mouseover: function (e) {
                if (!this.disabled) {
                    $(this).addClass('mouseover');
                    //$(this.$itemarea).addClass('mouseover');
                    if (this.moc) {
                        this.moc($(this).dom(), e);
                    }
                }
            }
            , mouseout: function (e) {
                $(this).removeClass('mouseover');
                //$(this.$itemarea).removeClass('mouseover');
                if (this.muc) {
                    this.muc($(this).dom(), e);
                }
            }
            , onmade: function () {
                this.init();
            }
            , $: [
                {
                    tag: 'div', alias: 'itemarea', className: 'itemarea line relative'
                    , onmouseover: function (e) {
                        var el = this;
                        $(el).addClass('mouseover');
                    }
                    , onmouseout: function (e) {
                        var el = this;
                        $(el).removeClass('mouseover');
                    }, $: [
                    {
                        tag: 'div', alias: 'lexp', className: 'lexp', $: '&nbsp;', onclick: function (e) {
                            if (this.$root.behavior) {
                                this.$root.behavior.exp(this.$root);
                            }
                        }}
                    , {
                        tag: 'div', alias: 'link', className: 'link', onclick: function (e) {
                            //debugger;
                            if (this.$root.disabled) {
                                return;
                            }
                            if (this.$root.behavior) {
                                if (this.$root.$submenu) {
                                    this.$root.behavior.exp(this.$root);
                                } else {
                                    var m = this.$root.rootMenu();
                                    m.deselect();
                                    if (this.$root.behavior) {
                                        this.$root.behavior.setval(this.$root);
                                    }
                                }
                            } else {
                                debugger; //No behavior
                            }
                        }
                    }
                    , {
                        tag: 'div', alias: 'rexp', className: 'rexp', onclick: function (e) {
                            if (this.$root.behavior) {
                                this.$root.behavior.exp(this.$root);
                            }
                        }
                    }]
                }
                , { tag: 'div', alias: 'subs', className: 'subs hidden' }
                , { tag: 'div', alias: 'clear', className: 'clear' }
            ]
        }
        return json;
    };
})(jQuery);