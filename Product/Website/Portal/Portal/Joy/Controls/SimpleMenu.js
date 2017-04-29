joy.controls.SimpleMenuItem = {
	tagName: 'div',
	parentMenu: null,
	className: 'simple_menu_item',
	setData: function (data) {
		this.innerHTML = data.text;
		this.value = data.value;
		this.parentMenu = data.menu;
	},
	getData: function () {
		return this.value;
	},
	style: {
		cursor: 'default'
	},
	onmouseup: function (evt) {
		if (this.parentMenu) {
			this.parentMenu.value = this.value;
			this.parentMenu.text = this.innerHTML;
			this.parentMenu.$root.select({ text: this.innerHTML, value: this.value });
			this.parentMenu.$root.showMenu(false);
		}
	},
	onmouseover: function (evt) {
		this.$backcolor = this.style.background;
		this.$color = this.style.color;
		this.style.background = 'gray';
		this.style.color = 'white';
	},
	onmouseout: function (evt) {
		this.style.background = this.$backcolor;
		this.style.color = this.$color;
	}
};
joy.controls.SimpleMenu = {
    tagName: 'div',
    className: 'simple_menu',
	alias: 'menu',
	style: { position: 'absolute', display: 'none', minWidth: '50px', minHeight: '12px', border: 'solid 1px gray', padding: '4px' },
	onmouseover: function (evt) {
	},
	setData: function (data) {
		var list = data.items;
		var selectedValue = data.selectedValue;
		var selectedText = data.selectedText;
		this.innerHTML = '';
		for (var i = 0; i < list.length; i++) {
			var d = list[i];
			var el = joy.make('SimpleMenuItem');
			d.menu = this;
			el.setData(d);
			this.appendChild(el);
			if (data.value && d.value == data.value) {
				this.text = d.text;
				this.value = d.value;
				this.$root.select(d);
			}
		}
	},
	show: function (el, visible) {
		var rect = el.getBoundingClientRect();
		var rectDoc = document.body.getBoundingClientRect();
		this.style.top = rect.bottom - parseInt(el.style.borderWidth||'0px') - rectDoc.top + 'px';
		this.style.left = rect.left + document.body.scrollLeft - rectDoc.left + 'px';
		this.style.minWidth = (rect.right - rect.left - parseInt(this.style.paddingLeft) - parseInt(this.style.paddingRight) - parseInt(this.style.borderWidth) * 2) + 'px';
		if (visible) {
			this.style.display = '';
		} else {
			this.style.display = 'none';
		}
		//debugger;
		rect = this.getBoundingClientRect();
		rectRoot = el.getBoundingClientRect();
		el = this.$root;
		if (!el.intervalHandle) {
			el.intervalHandle = window.setInterval(function () {
				//var b = document.getElementById('box');
				//b.value = joy.mouse.x + ',' + joy.mouse.y;
				if (!(joy.mouse.x >= rectRoot.left && joy.mouse.x <= rectRoot.right && joy.mouse.y <= rectRoot.bottom && joy.mouse.y >= rectRoot.top) && (joy.mouse.x < rect.left || joy.mouse.x > rect.right || joy.mouse.y < rect.top || joy.mouse.y > rect.bottom)) {
					el.showMenu(false);
					window.clearInterval(el.intervalHandle);
					el.intervalHandle = null;
				}
			}, 100);
		}

	}

};