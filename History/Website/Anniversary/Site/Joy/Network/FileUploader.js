Joy.controls.FuItem = {
	tagName: 'div',
	className: 'item',
	defaultImg: 'http://www.sdmyyz.com/Images/Nopic.jpg',
	init: function (cfg) {
		var input = this.reset();
	},
	reset: function () {
		this.$upimg.src = Joy.controls.FuItem.defaultImg;
		this.$inputarea.innerHTML = "<input name='fuitem' type='file' />";
		return this.$inputarea.childNodes[0];
	},
	$: [
		{ tagName: 'div', className: 'fleft', $: [{ tagName: 'img', alias: 'upimg', src: '', style: { display: 'none' } }] },
		{ tagName: 'div', className: 'fleft', alias: 'inputarea' }
	]
};
Joy.controls.FuList = {
	tagName: 'div',
	className: 'filelist',
	init: function (cfg) {
		var n = cfg.startnum;
		this.itemlist = [];
		for (var i = 0; i < n; i++) {
			var fi = Joy.make('FuItem');
			fi.init({ index: i });
			this.itemlist[this.itemlist.length] = fi;
		}
		this.cfg = cfg;
	},
	update: function () {
		var n = this.itemlist.length;
		this.innerHTML = '';
		for (var i = 0; i < n; i++) {
			this.appendChild(this.itemlist[i]);
		}
	},
	save: function () {
	},
	reset: function () {
		var n = this.itemlist.length;
		for (var i = 0; i < n; i++) {
			this.itemlist[i].reset();
		}
	}
}