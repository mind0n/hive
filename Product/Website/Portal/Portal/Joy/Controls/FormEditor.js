//var formCfg = {
//	rows:
//	[
//		{
//			utext: { label: '姓名：', controlName: 'LabelControl', editor:'input', className: 'utext' },
//			authcode: { label: '验证码：', controlName: 'LabelControl', editor:'input', className: 'authcode', icon: 'authcode.aspx#' }
//		},
//		{ content: { label: '祝福：', controlName: 'LabelControl', editor:'textarea', className: 'content'} }
//	]
//};
//var formdata = {
//	rows:
//	[
//		{ utext: '学生', authcode: '2341', content: '祝福寄语'},
//		{ utext: '学生2', authcode: '2342', content: '祝福寄语2'}
//	]
//};

joy.controls.FormEditorRow = {
	className: 'form_editor_row',
	tagName: 'div',
	init: function (cfg) {
		var form = this.$form;
		if (!form.editors) {
			form.editors = {};
		}
		for (var i in cfg) {
			if (cfg[i]) {
				var ctrl = joy.make(cfg[i].controlName);
				ctrl.init(cfg[i]);
				form.editors[i] = ctrl;
				this.appendChild(ctrl);
			}
		}
	}
};

joy.controls.FormEditor = {
	className: 'formeditor',
	tagName: 'div',
	rows: null,
	init: function (cfg) {
		if (cfg.rows) {
			this.$container.innerHTML = '';
			if (cfg.title) {
				this.$title.style.display = '';
				this.$title.innerHTML = cfg.title;
			} else {
				this.$title.style.display = 'none';
			}
			if (this.rows) {
				this.rows = null;
			}
			this.rows = [];
			for (var i = 0; i < cfg.rows.length; i++) {
				var rowCfg = cfg.rows[i];
				var row = joy.make('FormEditorRow');
				row.$form = this;
				row.init(rowCfg);
				this.rows[this.rows.length] = row;
				this.$container.appendChild(row);
			}
		}
	},
	setData: function (data) {
	},
	getData: function () {
		var r = [];
		if (this.editors) {
			for (var i in this.editors) {
				var editor = this.editors[i];
				r.push({ field: i, value: editor.getData() });
			}
		}
		return r;
	},
	show: function () {
		var c = joy.cover(
			{
				left: 0,
				top: 0,
				right: document.body.clientWidth,
				bottom: document.body.clientHeight
			},
			{ style: joy.browser.ie6 ? { position: 'absolute'} : { position: 'fixed'} }
		);
		this.cover = c;
		c.$content.innerHTML = '';
		c.$content.appendChild(this);
	},
	showMsg: function (msg) {
		this.$box.innerHTML = msg;
	},
	hide: function () {
		if (this.cover) {
			this.cover.dispose();
		} else {
			joy.destroy(this);
		}
	},
	$:
	[
		{ tagName: 'div', className: 'closebtn', $: 'X',
			onclick: function (evt) {
				this.$root.hide();
			}
		},
		{ tagName: 'div', className: 'form_title', alias: 'title' },
		{ tagName: 'div', className: 'form_container', alias: 'container' },
		{ tagName: 'div', className: 'form_message_area', alias: 'box' },
		{ tagName: 'div', className: 'btnbar', alias: 'btnarea',
			$:
			[
				{ tagName: 'input', type: 'button', value: '　提交　',
					onmouseup: function (evt) {
						if (this.$root.onsubmit) {
							this.$root.onsubmit(this.$root);
						}
					}
				}
			]
		}
	]
};

//joy.controls.Forms = {
//	className: 'forms',
//	init: function (cfg) {
//	},
//	setData: function (data) {
//	},
//	save: function () {
//	}
//};