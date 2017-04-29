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
Joy.controls.GridBody = {
	tagName: 'table',
	className: 'body_table',
	init: function (cfg)
	{
		if (!this.cfg)
		{
			this.cfg = {};
		}
		Joy.merge(this.cfg, cfg);
	},
	setData: function (data)
	{
		var list = data.rows;
		for (var i = 0; i < list.length; i++)
		{
			var rowdata = list[i];
			var roweditor = Joy.make('RowEditor');
			roweditor.init(this.cfg);
			roweditor.setRowData(rowdata);
			this.$tbody.appendChild(roweditor);
		}
	},
	enumRow: function (sender, callback)
	{
		var roweditors = this.$tbody.childNodes;
		for (var i = 0; i < roweditors.length; i++)
		{
			if (!callback || !callback(sender, roweditors[i]))
			{
				break;
			}
		}
	},
	$:
	[
		{ tagName: 'tbody', alias: 'tbody' }
	]
};
Joy.controls.GridHeaderCell = {
	tagName: 'td',
	className: 'header_cell',
	cfg: {},
	init: function (col)
	{
		this.$content.innerHTML = col.caption || col.field;
		Joy.merge(this.cfg, col);
	},
	style: { overflow: 'visible' },
	$:
	[
		{ tagName: 'div', alias: 'contentarea', style: { width: '100%', height: '100%', position: 'relative' }, $:
		[
			{ tagName: 'div', className: 'header_cell_dragger', jid: Joy.id(),
				style: { position: 'absolute', cursor: 'e-resize', top: '0px', right: '0px', height: '100%', width: '5px' },
				onmousedown: function (evt)
				{
					Joy.mouse.drag(this, {
						isdragging: true,
						ondrag: function (el)
						{
							if (el.dragging.isdragging)
							{
								var startx = Joy.mouse.prevX;
								var curtx = Joy.mouse.x;
								var delta = curtx - startx;
								if (el.$root.rect().width + delta >= 12)
								{
									el.$root.style.width = parseInt(el.$root.style.width) + delta + 'px';
									el.$root.$grid.update();
									el.$root.$grid.scrollSync();
								}
							}
						},
						onup: function (el)
						{
							el.dragging.isdragging = false;
							Joy.mouse.removeListener(el);
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

Joy.controls.GridHeader = {
	tagName: 'table',
	className: 'header_table',
	fields: {},
	lastCell: null,
	init: function (cfg)
	{
		var cols = cfg.cols;
		for (var i = 0; i < cols.length; i++)
		{
			var cell = Joy.make('GridHeaderCell');
			cell.$header = this;
			cell.$grid = this.$grid;
			cell.init(cols[i]);
			Joy.merge(cell.style, cfg.styles[cols[i].field]);
			if (i + 1 == cols.length)
			{
				this.lastCell = cell;
			}
			this.$headerRow.appendChild(cell);
			var field = cols[i].field;
			this.fields[field] = cell;
		}
	},
	updateBody: function ()
	{
		var body = this.$grid.$body;
		body.enumRow(this, function (sender, el)
		{
			var tds = el.tds;
			for (var i in tds)
			{
				var td = tds[i];

				if (sender.fields[i])
				{
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
Joy.controls.Grid = {
	autoWidthLast: true,
	init: function (cfg)
	{
		var header = Joy.make('GridHeader');
		var body = Joy.make('GridBody');

		header.$grid = this;
		body.$grid = this;
		this.$header = header;
		this.$body = body;

		header.init(cfg);
		body.init(cfg);

		this.$headerarea.innerHTML = '';
		this.$headerarea.appendChild(header);
		this.$bodyarea.innerHTML = '';
		this.$bodyarea.appendChild(body);
	},
	setData: function (data)
	{
		this.$body.setData(data);
	},
	update: function ()
	{
		if (this.autoWidthLast)
		{
			var gw = this.rect().width;
			var hw = parseInt(this.$header.rect().width);
			var cw = parseInt(this.$header.lastCell.style.width);
			var w = gw - hw + cw - 2;
			if (w >= 12)
			{
				this.$header.lastCell.style.width = w + 'px';
			}
		}
		this.$header.updateBody();
	},
	scrollSync: function ()
	{
		this.$header.style.marginLeft = -this.$bodyarea.scrollLeft + 'px';
	},
	tagName: 'div',
	className: 'grid',
	$:
	[
        { tagName: 'div', alias: 'headerarea', className: 'caption' },
        { tagName: 'div', alias: 'bodyarea', className: 'body',
        	onscroll: function (evt)
        	{
        		this.$root.scrollSync();
        	}
        },
        { tagName: 'div', alias: 'footarea', className: 'foot', $: 'footer' }
    ]
};
