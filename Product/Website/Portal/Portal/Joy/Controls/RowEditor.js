joy.controls.RowEditor = {
	tagName: 'tr',
	init: function (cfg) {
		var list = cfg.cols;
		this.rects = {};
		this.tds = new Object();
		this.pk = null;
	    if (cfg.rowcfg) {
	        joy.merge(this, cfg.rowcfg);
	    }
		for (var i = 0; i < list.length; i++) {
			var col = list[i];
			var fname = col.field;
		    var curtStyle = cfg.styles ? (cfg.styles[fname] || cfg.defaultStyle) : null;
			var editor = joy.make('EditableRect');
			editor.$ = col;
			editor.editor = col.editor || cfg.defaultEditor || 'editbox';
			editor.$row = this;
			var td = joy.make('td');
		    td.style.display = curtStyle ? curtStyle.display : '';
			td.editor = editor;
			td.appendChild(editor);
			td.className = col.field;
			this.appendChild(td);
			this.rects[col.field] = editor;
			this.tds[col.field] = td;
			if (cfg.cols[i].pk) {
				this.pk = cfg.cols[i].pk;
			}
		}
	},
	getEditors: function (callback) {
		for (var i in this.rects) {
			var editor = this.rects[i];
			if (callback) {
				callback(editor);
			}
		}
	},
	accept: function () {
		this.getEditors(function (editor) {
			editor.accept();
		});
	},
	reject: function () {
		this.getEditors(function (editor) {
			editor.reject();
		});
	},
	edit: function () {
		this.getEditors(function (editor) {
			editor.edit();
		});
	},
	getRowData: function () {
		this.$data = {};
		var dat = this.$data;
		for (var i in this.rects) {
			var editor = this.rects[i];
			if (editor) {
				dat[i] = editor.getData();
			}
		}
		return dat;
	},
	setRowData: function (rowData) {
		this.$data = rowData;
		if (rowData) {
			for (var i in this.rects) {
				var editor = this.rects[i];
				if (i == this.pk) {
					this.pkv = rowData[i];
				}
				if (editor.$ && editor.$.setData) {
					editor.$.setData(editor, rowData[i]);
				} else {
					editor.setData({ value: rowData[i] });
				}
			}
		} else {
			for (var i in this.rects) {
				var editor = this.rects[i];
				if (editor.$ && editor.$.setData) {
					editor.$.setData(editor, {});
				} else {
					editor.setData({ value: {} });
				}
			}
		}
	}
};