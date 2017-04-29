joy.controls.DropDown = {
    tagName: 'div'
        , className: 'dropdown'
        , style: { width: '100%', height: '100%', overflow: 'hidden', position: 'relative' }
        , showMenu: function (visible) {
            var menu = this.$menu;
            menu.show(this, visible);
        }
        , mode: 'combo'
        , init: function(cfg) {
            joy.merge(this, cfg);
            if (cfg.mode != 'combo') {
                this.$textarea.innerHTML = '';
                
                var input = document.createElement('input');
                input.$dropbtn = this.$dropbtn;
                input.type = 'text';
                input.className = 'dropdown_input';
                input.style.position = 'absolute';
                input.style.left = '4px';
                input.style.top = '4px';
                input.style.right = '4px';
                input.style.bottom = '6px';
                input.onkeyup = function (e) {
                    var evt = event || e;
                    if (evt.keyCode == 13) {
                        this.$dropbtn.onmouseup(evt);
                    } else {
                        //alert(evt.keyCode);
                    }
                };
                this.$textarea.appendChild(input);
                this.$input = input;
                this.$textarea.onmouseup = null;
            }
            if (cfg.btnText) {
                this.$dropbtn.innerHTML = cfg.btnText;
            }
        }
        , setData: function (data) {
            if (this.mode == 'combo') {
                this.$input.value = data.value;
            } else {
                this.$menu.setData(data);
            }
        }
        , select: function (item) {
            alert('select');
            this.$textarea.innerHTML = item.text;
            this.value = item.value;
        }
        , getData: function () {
            if (this.mode == 'combo') {
                return this.$input.value;
            } else {
                this.$textarea.innerHTML = this.$menu.text;
                return this.$menu.value;
            }
        }
        , clearData: function() {
            if (this.mode == 'combo') {
                this.$input.value = '';
            } else {
                //this.$menu.setData(data);
                this.$textarea.innerHTML = '';
            }
        }
        , onappend: function() {
            this.$dropbtn.style.lineHeight = this.$dropbtn.rect().height + 'px';
            //this.$input.style.height = this.$dropbtn.rect().height + 'px';
            this.$input.style.width = this.rect().width - this.$dropbtn.rect().width - 12 + 'px';
            this.$input.style.fontSize = this.rect().height - 12 + 'px';
        }
        , onmade: function () {
            var m = {
                controlName: 'SimpleMenu'
            };
            var mel = joy.make(m);
            mel.$root = this;
            this.$menu = mel;
            document.body.appendChild(mel);
        }
        , $:
        [
            {
                tagName: 'div', className: 'dropdown_container', style: { width: '100%', height: '100%', overflow: 'hidden', position: 'relative' },
                $:
                [
                    {
                        tagName: 'div', className: 'dropdown_box', alias: 'textarea', style: { cursor: 'default', overflow: 'hidden', width: '100%', height: '100%', position:'relative' },
                        onmouseup: function (evt) {
                            this.$root.showMenu(true);
                        }
                    },
                    {
                        tagName: 'div', alias:'dropbtn', className: 'dropdown_button', style: { cursor: 'default', font: '11px verdana', width: '12px', height: '100%', position: 'absolute', right: '0px', top: '0px', background: 'white', borderLeft: 'solid 1px gray', textAlign: 'center' }, innerHTML: 'v',
                        onmouseup: function (evt) {
                            this.$root.showMenu(true);
                        }
                    }
                ]
            }
        ]
};
