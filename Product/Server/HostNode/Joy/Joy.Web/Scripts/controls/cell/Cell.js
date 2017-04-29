(function ($) {
    $.val = function(v, sender) {
        var t = typeof(v);
        if (t == 'object') {
            if (v.value) {
                sender.innerHTML = v.value;
                return v.value;
            } else if (v.img) {
                var img = document.createElement('img');
                img.src = v.img;
                sender.appendChild(img);
                return img;
            }
            return null;
        } else if (t == 'function') {
            return v(sender);
        }
        sender.innerHTML = v;
        return v;
    };
    $.userControls.Cell = {        
        tag: 'div',
        className: 'cell',
        val: function (data) {
            $.val(data, this.$content);
        },
        $: [{ tag: 'div', alias: 'content', className: 'body' }]
    };

    $.userControls.OnOffCell = {
        tag: 'div',
        className: 'onoffcell',
        isOn: true,
        init: function (c) {
            var s = c.layout;
            if (!s.bw) {
                s.bw = 1;
            }

            var bw = s.bw;
            var aw = s.width - s.hwidth - bw * 4;
            var ah = s.height - bw * 2;
            if (s.hwidth) {
                this.$handle.style.width = s.hwidth + 'px';
            }
            if (s.width) {
                this.style.width = s.width + 'px';
                this.$oncell.style.width = aw + 'px';
                this.$offcell.style.width = aw + 'px';
            }
            if (s.height) {
                this.style.height = s.height + 'px';
                this.$oncell.style.height = ah + 'px';
                this.$handle.style.height = ah + 'px';
                this.$offcell.style.height = ah + 'px';
            }
            if (!s.lh) {
                s.lh = 10;
            }
            this.$oncell.style.lineHeight = s.lh + 'px';
            this.$oncell.style.fontSize = s.lh + 'px';
            this.$offcell.style.lineHeight = s.lh + 'px';
            this.$offcell.style.fontSize = s.lh + 'px';
            this.$box.style.width = s.width + aw + s.bw * 6 + 'px';
        },
        areaWidth:function() {
            var l = this.layout;
            return l.width - l.hwidth - l.bw * 2;
        },
        val: function (isOn, data) {
            //var l = this.layout;
            if (!isOn) {
                $(this.$box).animate({ left: '-=' + this.areaWidth() }, 200);
            } else {
                $(this.$box).animate({ left: '+=' + this.areaWidth() }, 200);
            }
            this.isOn = isOn;
            if (data) {
                if (data.ontxt) {
                    this.$oncell.val(data.ontxt);
                }
                if (data.offtxt) {
                    this.$offcell.val(data.offtxt);
                }
            }
            if (this.onchange) {
                this.onchange(isOn);
            }
        },
        $: [
            {
                tag: 'div',
                alias: 'box',
                className: 'box',
                onmousedown: function(evt) {
                    var p = this.getparent('className', 'onoffcell');
                    p.val(!p.isOn);
                },
                $: [
                    {
                        cn: 'Cell',
                        className: 'on txt area',
                        alias: 'oncell',
                        init: function() { this.val('On'); }
                    },
                    {
                        cn: 'Cell',
                        alias: 'handle',
                        className: 'handle area'
                    },
                    {
                        cn: 'Cell',
                        alias: 'offcell',
                        className: 'off txt area',
                        init: function() { this.val('Off'); }
                    }
                ]
            }
        ]
    };
})(jQuery);