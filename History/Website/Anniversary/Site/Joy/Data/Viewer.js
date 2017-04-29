/*
	var config = {
		enableOddEven: true,
		styles: {
			uname: { width: '70px' },
			upwd: { width: '70px' },
			utype: { width: '70px' },
			unote: { width: '70px' }
		},
		cols: [
			{ field: 'uname', caption: 'Username', type: 'string' }
			, { field: 'upwd', caption: 'Password', type: 'string' }
			, { field: 'utype', caption: 'User Type', type: 'int' }
			, { field: 'unote', caption: 'Description', type: 'string' }
		], 
		hierarchy:[
			{
				name:'Account Info', 
				$:[
					{col:'uname'},
					{col:'upwd'}
				]
			},
			{col:'utype'},
			{col:'unote'}
 		]
	};
	var data = {
		rows: [
			{ uname: 'admin', upwd: 'nothing', utype: 0, unote: 'System administrator' }
			, { uname: 'temp', upwd: 'nothing' }
			, { uname: 'guest', upwd: 'nothing' }
			, { uname: 'user' }
		]
	};
 */
Joy.controls.Viewer = {
	tag: 'div',
	className: 'viewer',
	onappend: function (parent) {
		this.$container = parent;
		var headerEl = this.headerEl;
		headerEl.resetWidth();
		//Joy.extend(this.$header.style, this.cfg.styles.hrow);
		this.$body.resetWidth(headerEl);
		return true;
	},
	init: function (cfg) {
		if (!cfg) {
			return;
		}
		this.cfg = cfg;
		var hr = Joy.make('ViewerHeadRow');
		hr.init(cfg);
		hr.setData();
		this.headerEl = hr;
		//Joy('headarea').appendChild(hr);
		this.$header.appendChild(hr);
	},
	setData: function (data) {
		var hr = this.headerEl;
		if (hr) {
			this.$body.$rows = [];
			for (var i = 0; i < data.rows.length; i++) {
				var row = Joy.make('ViewerBodyRow');
				row.init(hr);
				if (this.cfg.hovable) {
					Joy(row).addClass('hovable');
				} else {
					Joy(row).delClass('hovable');
				}
				// i starts from zero.
				if (i % 2 != 0 && !this.cfg.staticColor) {
					Joy(row).addClass('even');
				} else {
					Joy(row).addClass('odd');
				}
				row.setData(data.rows[i]);
				this.$body.$rows[this.$body.$rows.length] = row;
				this.$body.appendChild(row);
			}
		}
	},
	$: [
		{ tag: 'div', className: 'headarea', alias: 'header' },
		{ tag: 'div', className: 'bodyarea', alias: 'body', resetWidth:function(headerEl) {
			for (var i = 0; i < this.$rows.length; i++) {
				var row = this.$rows[i];
				row.style.width = headerEl.$lineActualWidth + 'px';
			}
		} },
		{ tag: 'div', className: 'footrow', alias: 'footer' }
	]
};
Joy.controls.ViewerDataCell = {
	tag: 'div',
	className: 'cell',
	defaultStyle: {},
	init: function (cfg) {
		var col = cfg.col;
		this.col = col;
		Joy.extend(this.style, this.defaultStyle);
		if (cfg) {
			Joy.extend(this, cfg);
		}
	},
	setData: function (content) {
		var t = typeof (content);
		this.$box.innerHTML = '';
		if (t == 'object') {
			this.$box.innerHTML = content.data;
		}else if (t == 'function') {
			content(this);
		} else {
			this.$box.innerHTML = content;
		}
	},
	$: [
		{ tag: 'div', alias: 'box', className:'box', style: { width: '100%', height: '100%' } }
	]
};
Joy.controls.ViewerAnchorCell = {
	tag: 'div',
	className: 'cell',
	defaultStyle: {},
	init: function (cfg) {
		var col = cfg.col;
		this.col = col;
		Joy.extend(this.style, this.defaultStyle);
		if (cfg) {
			Joy.extend(this, cfg);
		}
	},
	setData: function (content) {
		var col = this.col;
		this.$box.innerHTML = '';
		if (content && content.data != '&nbsp') {
		    var t = typeof (content);
		    if (t == 'object') {
		        this.$box.innerHTML = content.data;
		    } else if (t == 'function') {
		        content(this);
		    } else {
		        this.$box.innerHTML = content;
		    }
		} else if (col.$.text) {
		    this.$box.innerHTML = col.$.text;
		}
		if (col && col.$) {
			var url = col.$.url;
			//url = url.replace('@' + col.$.field, this.rowData[col.$.field]);
			for (var i in this.cols) {
			    url = url.replace('@' + i, this.rowData[i]);
			}
			this.$box.href = url;
		}

	},
	$: [
		{
			tag: 'div', className: 'box', style: { width: '100%', height: '100%' },
			$:[{ tag: 'a', alias: 'box' }]
		}
	]
};
Joy.controls.ViewerBodyRow = {
	tag: 'div',
	className: 'bodyrow',
	copyCol: function (col) {
		var rlt = {};
		for (var i in col) {
			if (i != 'el') {
				rlt[i] = col[i];
			}
		}
		return rlt;
	},
	init: function (headrow) {
		if (headrow) {
			var cols = headrow.cols;
			var cfg = headrow.cfg;
			this.rowEls = {};
			this.cols = cols;
			if (cols) {
				for (var i in cols) {
					var col = cols[i];
					var cell;
					if (!col.rowtype) {
						cell = Joy.make('ViewerDataCell');
					} else {
						cell = Joy.make(col.rowtype);
					}
					Joy(cell).addClass(i);
					if (cfg.styles) {
						if (cfg.styles.rows && cfg.styles.rows[i]) {
							cell.init({ col:this.copyCol(col), cols:cols, style: cfg.styles.rows[i] });
						} else {
							cell.init({ col:this.copyCol(col), style: { width: col.el.style.width, display: col.el.style.display } });
						}
					}
					cell.col = col;
					cell.cols = cols;
					this.rowEls[i] = cell;
					this.$recordarea.appendChild(cell);
				}
				//this.style.width = headrow.lineWidth + 24 + 'px';
			}
		}
	},
	setData:function(rowData, undefined) {
		if (rowData && this.rowEls) {
			for (var i in this.rowEls) {
				var cell = this.rowEls[i];
				cell.rowData = rowData;
				if (cell) {
					var cn = rowData[i];
					cell.setData({ data: cn === undefined ? '&nbsp' : cn });
				}
			}
		}
	},
	$: [
		{ tag: 'div', alias: 'recordarea' },
		{ tag: 'div', alias: 'previewarea', style: { display: 'none' } }
	]
};
Joy.controls.ViewerHeadRow = {
	tag: 'div',
	className: 'headrow',
	resetWidth:function() {
		var w = 0;
		for (var i in this.$colEls) {
			var cell = this.$colEls[i];
			w += Joy(cell).rect().width;
		}
		this.$lineActualWidth = w;
		this.style.width = w + 'px';
	},
	init:function(cfg) {
		if (!cfg || !cfg.cols) {
			return;
		}
		this.cfg = cfg;
		this.cols = {};
		this.$colEls = {};
		var w = 0;
		for (var i = 0; i < cfg.cols.length; i++) {
			var col = cfg.cols[i];
			var cell = Joy.make('ViewerDataCell');
			if (cfg.styles && cfg.styles.cols) {
			    Joy(cell).addClass(col.field);
				cell.init({ style: cfg.styles.cols[col.field] });
			} else {
				cell.init();
			}
			cell.col = col;
			col.el = cell;
			this.cols[col.field] = col;
			this.$colEls[col.field] = cell;
			this.appendChild(cell);
			//var rect = cell.getBoundingClientRect();
			w += parseInt(cell.style.width);
		}
		this.lineWidth = w;
		//alert(w);
		//this.style.width = w + 24 + 'px';
	},
	setData:function() {
		var cols = this.cols;
		if (cols) {
			for (var i in cols) {
				var col = cols[i];
				var el = col.el;
				if (el) {
					el.setData(col.caption);
				}
			}
		}
	}
}