joy.creators.ui.staticPopup = function (ext) {
	var json = {
		tag: 'div',
		className: 'popup',
		setval: function (val) {
			$(this).text(val);
		},
		built: function (json) {
			if (json.type == 'imgloading') {
				this.style.backgroundImage = 'url(img/icon/loading.gif)';
				this.style.backgroundPosition = 'center';
				this.style['background-size'] = '96px';
				this.style.backgroundRepeat = 'no-repeat';
				this.style.width = '64px';
				this.style.height = '64px';
			}
		}
	};
	delete ext.ui;
	joy.extend(json, ext);
	var el = joy.jbuilder(json);
	return el;
};

joy.creators.ui.formPopup = function (ext) {
	var json = {
		tag: 'div',
		className: 'popup form',
		setval: function (data) {
			$(this.$caption).text(data.title);
			if (data.content) {
				if (this.$content$) {
					destroy(this.$content$);
				}
				var el = data.content;
				if (!el.tagName) {
					el = joy.jbuilder(data.content);
				}
				this.$content$ = el;
				this.$body.appendChild(el);
				return el;
			}
			return null;
		},
		close: function () {
			this.style.display = 'none';
			if (this.onclose) {
				this.onclose();
			} else {
				this.dispose();
			}
		},
		dispose: function () {
			if (this.$content$) {
				destroy(this.$content$);
			}
		},
		popup: function (arg) {
			var parent = arg.target;
			parent.appendChild(this);
			var rc = this.getBoundingClientRect();
			el.style.left = arg.pos[2] - rc.width / 2 + 'px';
			el.style.top = arg.pos[3] - rc.height / 2 + 'px';
			el.style.width = el.astyle(['width']);
			el.style.height = el.astyle(['height']);
		},
		$: [
			{
				tag: 'div', alias: 'caption', className: 'caption', $target$: function () { return this.$root; }
			}, {
				tag: 'div', alias: 'bclose', className: 'btns', $: [

					{
						tag: 'div', className: 'btn bclose', onclick: function () {
							this.$root.close();
						}, $: '×'
					}, {
						tag: 'div', className: 'btn bmax', onclick: function () {
							if (!this.$state$) {
								this.$root.$evtrap$ = false;
								this.$state$ = {
									left: this.$root.style.left,
									top: this.$root.style.top,
									width: this.$root.style.width,
									height: this.$root.style.height
								};
								this.$root.style.width = '';
								this.$root.style.height = '';
								this.$root.style.left = '0px';
								this.$root.style.top = '0px';
								this.$root.style.right = '0px';
								this.$root.style.bottom = '0px';
							} else {
								this.$root.$evtrap$ = true;
								this.$root.style.width = this.$state$.width;
								this.$root.style.height = this.$state$.height;
								this.$root.style.left = this.$state$.left;
								this.$root.style.top = this.$state$.top;
								this.$root.style.right = '';
								this.$root.style.bottom = '';
								this.$state$ = null;
							}
						}, $: 'o'
					}
				]
			}, { tag: 'div', alias: 'body', className: 'body' }
		]
	};
	delete ext.ui;
	joy.extend(json, ext);
	var el = joy.jbuilder(json);
	return el;
};