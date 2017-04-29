joy.controls.LabelControl = {
	tagName: 'div',
	className: 'labelctrl',
	init: function (cfg) {
		if (cfg.editor) {
			var editor = joy.make(cfg.editor);
			if (cfg.editortype) {
				editor.type = cfg.editortype;
			}
			if (!editor.setData) {
				if (editor.tagName.toLowerCase() == 'input' || editor.tagName.toLowerCase() == 'textarea') {
					editor.setData = function (data) {
						this.value = data;
					}
				} else {
					editor.setData = function (data) {
						this.innerHTML = data;
					}
				}
			}
			if (!editor.getData) {
				if (editor.tagName.toLowerCase() == 'input' || editor.tagName.toLowerCase() == 'textarea') {
					editor.getData = function (data) {
						return this.value;
					}
				} else {
					editor.getData = function (data) {
						return this.innerHTML;
					}
				}
			}
			this.$editor = editor;
			this.$ctrlarea.appendChild(editor);
		}
		if (cfg.icon) {
			var icon = joy.make('img');
			icon.src = cfg.icon;
			if (cfg.iconcfg) {
				joy.merge(icon, cfg.iconcfg);
			}
			if (cfg.autoRefreshIcon) {
				icon.src += '?' + new Date().getMilliseconds();
			}
			this.$icon.appendChild(icon);
			this.$icon.style.display = '';
		}
		if (cfg.label) {
			this.$label.innerHTML = cfg.label;
		}
	},
	getData: function () {
		return this.$editor.getData();
	},
	setData: function (data) {
	},
	$:
	[
		{ tagName: 'div', alias: 'label' },
		{ tagName: 'div', style: { display: 'none' }, alias: 'icon' },
		{ tagName: 'div', alias: 'ctrlarea' }
	]
};