(function ($) {
    $.userControls.Form = function () {
        var json = {
            tag: 'div'
            , className: 'form fixed deselect'
            , zIndex: 10
            , onmade: function () {
                var el = this;
                $.ms.regist({ el: el, h: el.$caption, behavior: $.ms.behaviors.move });
                $.ms.regist({ el: el, h: el.$resizer, override: true, behavior: $.ms.behaviors.resize_bf, horizantal: true, vertical: true });
                //$.ms.regist({ el: el, h: el.$resizer, behavior: $.ms.behaviors.resize_rb });
            }
            , onmousedown: function (e) {
                $.ms.activate(this);
            }
            , ondrag: function () {
                this.style.zIndex = $.ms.hindex;
            }
            , ondrop: function () {
                this.style.zIndex = 20;
            }
            , val: function (data, append) {
                if (data.caption) {
                    $.val(data.caption, this.$caption);
                    data.caption = null;
                }
                this.$content.val(data, append);
            }
            , $: [
                {
                    tag: 'div', className: 'container relative', $: [
                        { tag: 'div', alias: 'back', className: 'background float container deselect', zIndex: 1 }
                        , { tag: 'div', alias: 'caption', className: 'caption float deselect', zIndex: 2, $: 'caption' }
                        , { cn: 'Cell', alias: 'content', className: 'content float', zIndex: 2 }
                        , { tag: 'div', alias: 'resizer', className: 'resizer float deselect', zIndex: 3 }
                    ]
                }
            ]
        };
        return json;
    };

})(jQuery);