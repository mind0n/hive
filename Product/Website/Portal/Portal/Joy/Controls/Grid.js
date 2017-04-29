var griddata = {
	rows:
	[
        { uname: 'admin', upwd: 'nothing', type: 0, note: 'System administrator' }
        , { uname: 'temp', upwd: 'nothing' }
        , { uname: 'guest', upwd: 'nothing' }
        , { uname: 'user' }
    ]
};

var gridconfig = {
	cols:
	[
        { field: 'uname', caption: 'Username', type: 'string' }
        , { field: 'upwd', caption: 'Password', type: 'string' }
        , { field: 'type', caption: 'User Type', type: 'int' }
        , { field: 'note', caption: 'Description', type: 'string' }
    ]
};
joy.controls.GridBody = {
	tagName: 'table',
	className: 'body_table',
	init: function (cfg) {
		if (!this.cfg) {
			this.cfg = {};
		}
		joy.merge(this.cfg, cfg);
	},
	setData: function (data) {
	    var list = data.rows;
	    this.innerHTML = '';
	    this.$tbody = document.createElement('tbody');
	    this.appendChild(this.$tbody);
		for (var i = 0; i < list.length; i++) {
			var rowdata = list[i];
			var roweditor = joy.make('RowEditor');
			roweditor.init(this.cfg);
			roweditor.setRowData(rowdata);
			this.$tbody.appendChild(roweditor);
		}
	},
	enumRow: function (sender, callback) {
		var roweditors = this.$tbody.childNodes;
		for (var i = 0; i < roweditors.length; i++) {
			if (!callback || !callback(sender, roweditors[i])) {
				break;
			}
		}
	},
	$:
	[
		{ tagName: 'tbody', alias: 'tbody' }
	]
};
joy.controls.GridHeaderCell = {
	tagName: 'td',
	className: 'header_cell',
	cfg: {},
	init: function (col) {
		this.$content.innerHTML = col.caption || col.field;
		joy.merge(this.cfg, col);
	},
	style: { overflow: 'visible' },
	$:
	[
		{ tagName: 'div', alias: 'contentarea', style: { width: '100%', height: '100%', position: 'relative' }, $:
		[
			{ tagName: 'div', className: 'header_cell_dragger', jid: joy.id(),
				style: { position: 'absolute', cursor: 'e-resize', top: '0px', right: '0px', height: '100%', width: '5px' },
				onmousedown: function (evt) {
					joy.mouse.drag(this, {
						isdragging: true,
						ondrag: function (el) {
							if (el.dragging.isdragging) {
								var startx = joy.mouse.prevX;
								var curtx = joy.mouse.x;
								var delta = curtx - startx;
								if (el.$root.rect().width + delta >= 12) {
									el.$root.style.width = parseInt(el.$root.style.width) + delta + 'px';
									el.$root.$grid.update();
									el.$root.$grid.scrollSync();
								}
							}
						},
						onup: function (el) {
							el.dragging.isdragging = false;
							joy.mouse.removeListener(el);
						}
					});
				}
			},
			{ tagName: 'div', className: 'header_cell_content', alias: 'content',
				style: { overflow: 'hidden', zIndex: 1 }
			}
		]
		}
	]
};

joy.controls.GridHeader = {
	tagName: 'table',
	className: 'header_table',
	fields: {},
	lastCell: null,
	init: function (cfg) {
		var cols = cfg.cols;
		for (var i = 0; i < cols.length; i++) {
			var cell = joy.make('GridHeaderCell');
			cell.$header = this;
			cell.$grid = this.$grid;
			cell.init(cols[i]);
		    if (cfg.styles) {
		        var curtstyle = cfg.styles[cols[i].field];
		        if (!curtstyle) {
		            curtstyle = cfg.defaultStyle;
		        }
		        if (curtstyle) {
		            joy.merge(cell.style, curtstyle);
		        }
		    }
		    if (i + 1 == cols.length) {
				this.lastCell = cell;
			}
			this.$headerRow.appendChild(cell);
			var field = cols[i].field;
			this.fields[field] = cell;
		}
	},
	updateBody: function () {
		var body = this.$grid.$body;
		body.enumRow(this, function (sender, el) {
			var tds = el.tds;
			for (var i in tds) {
				var td = tds[i];

				if (sender.fields[i]) {
					var width = sender.fields[i].style.width;
					td.style.width = width;
				}
			}
			return true;
		});
	},
	$:
	[
		{ tagName: 'tbody', $: [{ tagName: 'tr', alias: 'headerRow'}] }
	]
};
joy.controls.Grid = {
	autoWidthLast: false,
	init: function (cfg) {
		var header = joy.make('GridHeader');
		var body = joy.make('GridBody');
		var title = joy.make('GridTitle');
		this.autoWidthLast = cfg.autoWidthLast;
		header.$grid = this;
		body.$grid = this;
		title.$grid = this;
		this.$title = title;
		this.$header = header;
		this.$body = body;

		header.init(cfg);
		body.init(cfg);
		if (cfg.hideCaptions) {
			this.$headerarea.style.display = "none";
		} else {
			this.$headerarea.style.display = "";
		}
		if (cfg.hideFooter) {
			this.$footarea.style.display = "none";
		} else {
			this.$footarea.style.display = "";
		}
		if (cfg.hideTitle) {
			this.$titlearea.style.display = "none";
		} else {
			this.$titlearea.style.display = "";
		}
		this.$titlearea.appendChild(title);
		this.$headerarea.innerHTML = '';
		this.$headerarea.appendChild(header);
		this.$bodyarea.innerHTML = '';
		this.$bodyarea.appendChild(body);
	},
	setData: function (data) {
	    if (data.rows) {
			this.$body.setData(data);
			this.$title.setData(data);
		}
	},
	update: function () {
		if (this.autoWidthLast) {
			var gw = this.rect().width;
			var hw = parseInt(this.$header.rect().width);
			var cw = parseInt(this.$header.lastCell.style.width);
			var w = gw - hw + cw - 2;
			if (w >= 12) {
				this.$header.lastCell.style.width = w + 'px';
			}
		}
		this.$header.updateBody();
	},
	scrollSync: function () {
		this.$header.style.marginLeft = -this.$bodyarea.scrollLeft + 'px';
	},
	tagName: 'div',
	className: 'grid',
	$:
	[
		{ tagName: 'div', alias: 'titlearea', className: 'title' },
        { tagName: 'div', alias: 'headerarea', className: 'caption' },
        { tagName: 'div', alias: 'bodyarea', className: 'body',
        	onscroll: function (evt) {
        		this.$root.scrollSync();
        	}
        },
        { tagName: 'div', alias: 'footarea', className: 'foot', $: 'footer' }
    ]
};
joy.controls.GridTitle = {
	tagName: 'table',
	setData: function (data) {
		this.$contentbox.innerHTML = data.categoryname;
		if (data.url) {
			this.$morebox.href = data.url + data[data.field];
		}
	},
	$:
	[
		{ tagName: 'tbody', $:
		[
			{ tagName: 'tr', $:
				[
					{ tagName: 'td', alias: 'contentbox', className: 'contentbox' },
					{ tagName: 'td', className: 'more', $:
						[
							{ tagName: 'a', style: { textDecoration: 'none' }, alias: 'morebox', href: '#', $: 'MORE>' }
						]
					}
				]
			}
		]
		}
	]
};
joy.makeWidgets = function (cfg) {
	function makeWidget(c, d) {
		if (d.rows) {
			var wname = d.widgetname;
			if (!wname) {
				wname = 'Grid';
			}
			var rlt = joy.make(wname);
			if (rlt.init) {
				rlt.init(c);
			}
			if (d.widgetsettings && d.widgetsettings != '{}') {
				var settings = joy.json(d.widgetsettings);
				if (settings) {
					joy.merge(d, settings);
				}
			}
			if (rlt.setData) {
				rlt.setData(d);
			}
			if (rlt.update) {
				rlt.update();
			}
			if (joy(d.container)) {
				var p = joy(d.container);
				p.appendChild(rlt);
			}
		}
	}
	if (cfg.rows) {
		return makeWidget(cfg, cfg);
	} else if (cfg.dataset) {
		var rlt = [];
		for (var i in cfg.dataset) {
			var d = cfg.dataset[i];
			rlt.push(makeWidget(cfg, d));
		}
		return rlt;
	}
	return null;
};
