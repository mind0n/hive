Joy.controls.ImgList = {
	tagName: 'div',
	className: 'imglist',
	init: function (cfg) {
	},
	setData: function (data) {
		this.$title.innerHTML = data.categoryname;
		if (data.rows) {
			for (var i = 0; i < data.rows.length; i++) {
				var row = data.rows[i];
				var a = document.createElement('a');
				var img = document.createElement('img');
				a.appendChild(img);
				a.href = row.link;
				img.src = row.tag;
				this.$list.appendChild(a);
			}
		}
	},
	update: function () {
	},
	$:
	[
		{ tagName: 'div', className:'title', alias: 'title' },
		{ tagName: 'div', className:'list', alias: 'list' }
	]
};