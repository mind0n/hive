Joy.controls.coverer = {
	tagName: 'div',
	className: 'cover',
	style: { position: 'absolute', display: 'none' },
	coverEl: function (el) {
		if (el) {
			var type = typeof (el);
			if (type == 'string') {
				el = document.getElementById(el);
			}
			this.coverRect(el.getBoundingClientRect());
		}
		return el;
	},
	coverRect: function (rect) {
		if (rect) {
			document.body.appendChild(this);
			this.style.display = '';
			this.style.position = 'absolute';
			this.style.width = rect.right - rect.left + 'px';
			this.style.height = rect.bottom - rect.top + 'px';
			this.style.top = rect.top + document.body.scrollTop + 'px';
			this.style.left = rect.left + document.body.scrollLeft + 'px';
			this.$content.style.top = this.rect().height / 2 - this.$content.rect().height + 'px';
			this.$content.style.left = this.rect().width / 2 - this.$content.rect().width / 2 + 'px';
		}
	},
	dispose: function () {
		Joy.destroy(this);
	},
	$:
	[
		{ tagName: 'div', alias: 'container', className: 'container', style: { padding: '0px auto', width: '100%', height: '100%', position: 'relative' }, $:
			[
				{ tagName: 'div', className: 'background' },
				{ tagName: 'div', alias: 'content', className: 'content', $: 'Loading ...', style: { position: 'absolute'} }
			]
		}
	]
};
Joy.cover = function (target) {
	var el, rect;
	if (target) {
		var type = typeof (el);
		if (type == 'string') {
			el = document.getElementById(target);
		} else if (!target.tagName) {
			rect = target;
		} else {
			el = target;
		}
		var cover = Joy.make('coverer');
		if (rect) {
			cover.coverRect(rect);
		} else if (el) {
			cover.coverEl(el);
		}
		return cover;
	}
	return null;
};