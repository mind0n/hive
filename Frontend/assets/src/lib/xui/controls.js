joy.creators.ui = {
	create: function (n, j, c) {
		var el = null;
		if (this[n]) {
			el = this[n](j, c);
		}
		return el;
	}
  , created: function (el) {
  }
  , append: function (p, c) {
  	if (p && c && p.tagName && (c.tagName || c.nodeName)) {
  		if (p.append) {
  			p.append(c);
  		} else {
  			p.appendChild(c);
  		}
  	}
  }
  , val: function (p, v) {
  	if (p.setval) {
  		p.setval(v);
  	} else {
  		var tn = document.createTextNode(v);
  		p.appendChild(tn);
  	}
  }
};
joy.creators.ui.simpleMenu = function (ext) {
	var json = {
		tag: 'ul',
		alias: 'menu',
		className: 'simple-menu',
		setval: function (data) {
			if (data instanceof Array) {
				for (var i = 0; i < data.length; i++) {
					var iel = joy.jbuilder({ ui: 'simpleMenuItem' });
					iel.setval(data[i]);
					iel.$root = this.$root;
					this.appendChild(iel);
				}
			}
		},
		getval: function () {
			var rlt = [];
			for (var i = 0; i < this.childNodes.length; i++) {
				var item = this.childNodes[i]
				rlt[rlt.length] = item.getval();
			}
			return rlt;
		},
		show: function (target) {
			if (!target) {
				return;
			}
			var rc = target.getBoundingClientRect();
			var p = [rc.left, rc.bottom, rc.width];
			this.style.position = 'fixed';
			this.style.left = p[0] + 'px';
			this.style.top = p[1] + 'px';
			this.style.width = p[2] + 'px';
			$(this).show();
		},
		hide: function () {
			$(this).hide();
		}
	};
	delete ext.ui;
	joy.extend(json, ext);
	var el = joy.jbuilder(json);
	return el;
};
joy.creators.ui.simpleMenuItem = function (ext) {
	var item = {
		tag: 'li',
		className: 'simple-menu-item',
		getval: function () {
			return this.$link.innerHTML;
		},
		setval: function (val) {
			$(this.$link).text(val);
		},
		onclick: function () {
			this.$root.select(this);
		},
		$: { tag: 'a', alias: 'link' }
	};
	delete ext.ui;
	joy.extend(item, ext);
	var el = joy.jbuilder(item);
	return el;
};

joy.creators.ui.selector = function (ext) {
	function toggle(e) {
		var root = this.$root;
		if ($(root.$menu).is(':visible')) {
			console.log('visible');
			this.$root.hideMenu();
		} else {
			console.log('hidden');
			this.$root.showMenu();
		}
	}
	var json = {
		tag: 'div',
		className: 'dropdown',
		select: function (item) {
			this.$txt.nodeValue = item.getval();
			this.hideMenu();
		},
		selectedIndex: function () {
			var txt = this.$txt.nodeValue;
			var items = $(this.$menu).find('li');
			for (var i = 0; i < items.length; i++) {
				var item = items[i];
				if (item.getval() == txt) {
					return i;
				}
			}
			return -1;
		},
		getval: function () {
			return this.$txt.nodeValue;
		},
		setval: function (data) {
			if (!data || (data instanceof Array && data.length < 1)) {
				return;
			}
			var menu = this.$menu;
			if (menu) {
				destroy(menu);
			}
			menu = joy.jbuilder({ ui: 'simpleMenu' });
			menu.$root = this;
			this.$menu = menu;
			//this.appendChild(menu);
			document.body.appendChild(menu);
			menu.setval(data);
		},
		showMenu: function () {
			if (this.$menu) {
				this.$menu.show(this);
			}
		},
		hideMenu: function () {
			if (this.$menu) {
				this.$menu.hide();
			}
		},
		$: [
				{
					tag: 'div', alias: 'btn', className: 'toggle'
					, onclick: toggle
					, $: [
					  { tag: '#text', alias: 'txt', $: 'Select an option' },
					  { tag: 'span', className: 'caret right' }
					]
				}
		]
	};
	if (!ext) {
		ext = {};
	}
	if (!ext.type || ext.type == 'dropdownlist') {

	} else if (ext.type == 'dropdowneditor') {
		delete json.$[0].onclick;
		json.select = function (item) {
			this.$txt.value = item.getval();
			this.hideMenu();
		};
		json.$[0].$ = [
			{ tag: 'input', type: 'text', value: 'Select an option', alias: 'txt' },
			{
				tag: 'div', alias: 'caret', className: 'caret-container'
				, onclick: toggle
				, $: { tag: 'span', className: 'caret' }
			}
		];
	}
	delete ext.ui;
	joy.extend(json, ext);
	var el = joy.jbuilder(json);
	return el;
}