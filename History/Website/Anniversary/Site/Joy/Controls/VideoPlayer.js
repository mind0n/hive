Joy.controls.VideoPlayer = {
	tagName: 'div',
	className: 'videoplayer',
	init: function (cfg) {
	},
	setData: function (row) {
		this.$embed.src = row.link;
		this.$title.innerHTML = row.caption;
	},
	update: function () {
	},
	$:
	[
		{ tagName: 'div', alias: 'title', className: 'title' },
		{ tagName: 'div', alias: 'player', className: 'player',
			$: [
				{ tagName: 'embed'
					, alias: 'embed'
					, src: ''
					, allowFullScreen: 'true'
					, width: '100%'
					, height: '100%'
					, align: 'middle'
					, type: "application/x-shockwave-flash"
				}

			]
		}
	]
};