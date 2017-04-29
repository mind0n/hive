joy.hideMenu = function (el) {
	var prect = null;
	var rect = el.getBoundingClientRect();
	if (el.$parent) {
		prect = el.$parent.getBoundingClientRect();
	}
	el.hideMenuHandle = window.setInterval(function () {
		if (!joy.mouseInRect(rect) && (prect && !joy.mouseInRect(prect))) {
			el.hide();
			window.clearInterval(el.hideMenuHandle);
		}
	}, 500);
};
joy.mouseInRect = function (rect) {
	if (joy.mouse.x >= rect.left && joy.mouse.x <= rect.right && joy.mouse.y <= rect.bottom && joy.mouse.y >= rect.top) {
		return true;
	}
	return false;
};
joy.data = {};
joy.data.treenode = function (data, cfg, cate) {
	var rlt = {};
	var level = 0;
	var pm = cfg.parentMember;
	var vm = cfg.valueMember;
	var itemcfg, menucfg;
	function setCfg(list, cfg, level) {
		menucfg = joy.merge({}, cfg, true);
		menucfg.level = level;
		if (cfg.menucfg.length > level) {
			joy.merge(menucfg, cfg.menucfg[level]);
		} else {
			joy.merge(menucfg, cfg.menucfg[cfg.menucfg.length]);
		}
		itemcfg = {};
		if (cfg.itemcfg.length > level) {
			joy.merge(itemcfg, cfg.itemcfg[level]);
		} else {
			joy.merge(itemcfg, cfg.itemcfg[cfg.itemcfg.length]);
		}
		list.$mcfg = menucfg;
		list.$icfg = itemcfg;
	}

	if (!data || data.length < 1) {
		return null;
	}
	if (!cate) {
		cate = data[0][pm];
	}
	var list = rlt
	list.data = {};
	setCfg(list, cfg, level);
	for (var i = 0; i < data.length; i++) {
		var item = data[i];
		if (item[pm] != cate) {
			var tcate = item[pm];
			if (!list.data[tcate]) {
				continue;
			}
			list.data[tcate].$ = {};
			list = list.data[tcate].$;
			list.data = {};
			setCfg(list, cfg, ++level);
			cate = item[pm];
		}
		list.data[item[vm]] = item;
	}
	return rlt;
};
/*************************************************************************
{
	data:{
		1:{
			CategoryId:1,
			Caption:"校庆新闻",
			ParentId:0,
			Visible:"True",
			CategoryUpdate:"2012-06-24"
		},
		4:{
			CategoryId:4,
			Caption:"办学成果",
			ParentId:0,
			Visible:"True",
			CategoryUpdate:"2012-06-24",
			$:{
				data:{
					9:{
						CategoryId:9,
						Caption:"教授风采",
						ParentId:4,
						Visible:"True",
						CategoryUpdate:"2012-06-24"
					},
					10:{
						CategoryId:10,
						Caption:"教学成果",
						ParentId:4,
						Visible:"True",
						CategoryUpdate:"2012-06-24"
					}
				},
				$mcfg:{
					displayMember:"Caption",
					valueMember:"CategoryId",
					parentMember:"ParentId",
					url:"content.aspx?cid=",
					level:0,
					autoHide:false,
					className:"toplevelmenu"
				},
				$icfg:{
					style:{
						float:"left"
					}
				}
			}
		},
		5:{
			CategoryId:5,
			Caption:"校友风采",
			ParentId:0,Visible:"True",
			CategoryUpdate:"2012-06-24"
		},
	},
	$mcfg:{
		displayMember:"Caption",valueMember:"CategoryId",parentMember:"ParentId",
		url:"content.aspx?cid=",
		level:0,
		autoHide:false,
		className:"toplevelmenu"
	},
	$icfg:{style:{float:"left"}}
}
*************************************************************************/
joy.controls.MenuItem = {
	tagName: 'div',
	className: 'menu',
	init: function (icfg) {
		if (!icfg) {
			return;
		}
		joy.merge(this, icfg);
	},
	setData: function (data) {
		var menu = this.$parent;
		var dm = menu.displayMember;
		var vm = menu.valueMember;
		this.$link.href = menu.url + data[vm];
		this.$link.innerHTML = data[dm];
		if (data.$) {
			this.makeSubMenu(data.$);
		}
	},
	makeSubMenu: function (node) {
		var menu = joy.make('Menu');
		menu.$parent = this;
		menu.init(node);
		menu.setData(node);
		this.$submenu = menu;
		document.body.appendChild(menu);
	},
	activate: function () {
		if (this.$submenu) {
			this.$submenu.showAt({ el: this });
		}
	},
	$:
	[
		{ tagName: 'a', alias: 'link', href: '#' }
	]
};
joy.controls.Menu = {
	tagName: 'div'
	, style: { display: 'none' }
	, children: {}
	, init: function (node) {
		if (node.$mcfg) {
			joy.merge(this, node.$mcfg);
		}
		this.itemcfg = node.$icfg;
	}
	, setData: function (node) {
		if (!node.data) {
			return;
		}
		var data = node.data;
		var vm = this.valueMember;
		for (var item in data) {
			var mi = joy.make('MenuItem');
			mi.$parent = this;
			mi.init(this.itemcfg);
			mi.setData(data[item]);
			this.children[item[vm]] = mi;
			this.appendChild(mi);
		}
	}
	, showIn: function (container) {
		if (container) {
			container.appendChild(this);
			this.style.display = '';
			this.showCompleted();
		}
	}
	, showAt: function (region) {
		if (!region) {
			return;
		}
		var el = region.el;
		var rect = el.rect();
		var tx = region.tx;
		var ty = region.ty;
		var ex = region.ex;
		var ey = region.ey;
		var pos = region.pos;
		var ox = region.ox;
		var oy = region.oy;
		var drect = document.body.getBoundingClientRect();
		if (!tx) {
			tx = 'left';
		}
		if (!ty) {
			ty = 'top';
		}
		if (!ex) {
			ex = 'left';
		}
		if (!ey) {
			ey = 'bottom';
		}
		if (!pos) {
			pos = 'absolute';
		}
		if (!ox) {
			ox = -12 - drect.left;
		}
		if (!oy) {
			oy = -2 - drect.top;
		}
		this.style.position = pos;
		this.style[tx] = rect[ex] + ox + 'px';
		this.style[ty] = rect[ey] + oy + 'px';
		this.style.display = '';
		this.showCompleted();
	}
	, showCompleted: function () {
		if (this.autoHide) {
			joy.hideMenu(this);
		}
	}
	, hide: function () {
		this.style.display = 'none';
	}
	, select: function (item) {
	}
};