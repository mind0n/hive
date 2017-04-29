Joy.controls.adswitcher = {
	tagName: 'div',
	className: 'adswitcher',
	style: { position: 'relative' },
	duration: 1000,
	interval: 10,
	setData: function (cfg) {
		var list = cfg.srcs;
		if (cfg.duration) {
			this.duration = cfg.duration;
		}
		var p = 0;
		var active = true;
		for (var i = 0; i < list.length; i++) {
			var src = list[i];
			var img = Joy.make('img');
			img.src = src;
			img.style.position = 'absolute';
			img.style.left = '0px';
			img.style.top = '0px';
			img.style.width = '100%';
			img.style.height = '100%';
			img.zIndex = 1;
			this.$imgarea.appendChild(img);
			var div = Joy.make('div');
			div.innerHTML = i + 1;
			div.$img = img;
			div.$switcher = this;
			div.$i = i;
			div.onmouseover = function (evt) {
				p = this.$i;
				active = false;
			};
			div.onmouseout = function (evt) {
				active = true;
			}
			img.$div = div;
			img.$i = i;
			img.$switcher = this;
			this.$btnarea.appendChild(div);
		}
		var imgarea = this.$imgarea;
		var l = imgarea.childNodes.length;
		window.setInterval(function () {
			for (var i = 0; i < imgarea.childNodes.length; i++) {
				var img = imgarea.childNodes[i];
				if (i == p) {
					img.style.zIndex = 9;
					img.style.display = '';
				} else {
					img.style.zIndex = 8;
					img.style.display = 'none';
				}
			}
			if (active) {
				p++;
				if (p >= imgarea.childNodes.length) {
					p = 0;
				}
			}
		}, 1000);
	},
	$:
	[
		{ tagName: 'div', className: 'imgarea', alias: 'imgarea' },
		{ tagName: 'div', className: 'btnarea', alias: 'btnarea' }
	]
};