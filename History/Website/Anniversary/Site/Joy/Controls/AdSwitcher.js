/*
	图片新闻:{ 
		container:"topimg",
		categoryname:"图片新闻",
		categoryid:17,
		field:"categoryid",
		url:"content.aspx?cid=",
		widgetname:"AdSwitcher",
		widgetsettings:"{}",
		rows:[ 
			{ 
				articleid:38,
				categoryid:17,
				userid:1,
				caption:"图片新闻1",
				link:{  },
				articleupdate:"2012-07-14",
				tag:"Themes/Default/Images/newsimg1.jpg",
				categoryname:"图片新闻",
				container:"topimg",
				morder:1,
				pid:"index_aspx",
				widgetname:"AdSwitcher",
				widgetsettings:"{}",
				clientvisible:"True" 
			},
			{ articleid:39,categoryid:17,userid:1,caption:"图片新闻2",link:{  },articleupdate:"2012-07-14",tag:"Themes/Default/Images/newsimg2.jpg",categoryname:"图片新闻",container:"topimg",morder:1,pid:"index_aspx",widgetname:"AdSwitcher",widgetsettings:"{}",clientvisible:"True" } 
		] }
*/
Joy.controls.AdSwitcherButton = {
	tagName: 'div',
	className: 'button',
	style: { position: 'relative' },
	init: function (cfg) {
		Joy.merge(this, cfg);
	},
	setData: function (data) {
		this.$data = data;
		this.$txt.innerHTML = this.index + 1;
		this.$src = data.tag;
		if (this.$switcher.player) {
			this.player = Joy.make(this.$switcher.player);
			this.player.setData(data);
		}
		if (!this.isImgBtn || !data.tag) {
			this.$txt.style.display = '';
			this.$img.style.display = 'none';
		} else {
			this.$img.src = data.tag;
			this.$img.style.display = '';
			this.$txt.style.display = 'none';
		}
	},
	getSrc: function () {
		return this.$src || this.$img.src;
	},
	onmouseover: function (evt) {
		if (this.$switcher.player) {
			this.$switcher.setPlayer(this);
		} else {
			this.$switcher.isPause = true;
			this.$switcher.setImg(this);
		}
	},
	$:
	[
		{ tagName: 'div', alias: 'txt' },
		{ tagName: 'img', alias: 'img', style: { display: 'none'} }
	]
};
Joy.controls.AdSwitcher = {
	tagName: 'div',
	className: 'adswitcher',
	style: { position: 'relative' },
	duration: 3000,
	interval: 10,
	btns: [],
	isPause: false,
	setData: function (data) {
		var btns = [];
		Joy.merge(this, data, true);
		if (!data.rows) {
			return;
		}
		if (!data.hidetitle && data.categoryname) {
			this.$title.innerHTML = data.categoryname
		}
		for (var i = 0; i < data.rows.length; i++) {
			var row = data.rows[i];
			var b = Joy.make('AdSwitcherButton');
			b.$switcher = this;
			b.init({ url: this.url, isImgBtn: this.isImgBtn, index: i });
			b.setData(row);
			btns[btns.length] = b;
			this.$btnarea.appendChild(b);
		}
		if (data.player) {
			this.setPlayer(btns[0]);
		} else {
			this.setImg(btns[0]);
		}
		this.btns = btns;
		var switcher = this;
		var p = 0;
		var l = btns.length;
		window.setInterval(function () {
			var btn = btns[p % l];
			if (!switcher.isPause && !switcher.disableAutoPlay) {
				switcher.setImg(btn);
			}
			p++;
		}, this.duration);
	},
	setImg: function (btn) {
		this.$img.src = btn.getSrc();
		for (var i = 0; i < this.btns.length; i++) {
			var b = this.btns[i];
			Joy.delClass(b, 'activated');
		}
		Joy.addClass(btn, 'activated');
	},
	setPlayer: function (btn) {
		//this.$img.style.display = 'none';
		for (var i = 0; i < this.$imgarea.childNodes.length; i++) {
			var child = this.$imgarea.childNodes[i];
			child.style.display = 'none';
		}
		for (var i = 0; i < this.btns.length; i++) {
			var b = this.btns[i];
			Joy.delClass(b, 'activated');
		}
		Joy.addClass(btn, 'activated');
		var player = btn.player;
		player.style.display = '';
		this.$imgarea.appendChild(player);
	},
	$:
	[
		{ tagName: 'div', className: 'title', alias: 'title' },
		{ tagName: 'div', className: 'container', alias: 'container',
			$:
			[
				{ tagName: 'div', className: 'btnarea', alias: 'btnarea' },
				{ tagName: 'div', className: 'imgarea', alias: 'imgarea',
					$:
					[
						{ tagName: 'img', className: 'activeimg', alias: 'img', src: '', onmouseover: function (evt) { this.$root.isPause = true; }, onmouseout: function (evt) { this.$root.isPause = false; } }
					]
				}
			]
		}
	]
};
