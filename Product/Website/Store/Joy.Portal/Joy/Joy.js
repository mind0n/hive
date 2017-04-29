(function (nul) {
	var joy = function (selector) {
		if (!selector) {
			return;
		}
		var type = typeof (selector);
		if (type == 'object') {
			if (!selector.jid) {
				selector.jid = joy.id();
			}
			return selector;
		} else if (type == 'string') {
			if (selector.charAt(0) == '#') {
				var el = document.getElementById(selector.substr(1, selector.length - 1));
				return el;
			}
		}
	};

	joy.ready = function (callback) {
		var retry = function () {
			if (document.readyState === "complete") {
				window.clearTimeout(joy.readyTimeout);

				document.onmousemove = function (evt) {
					evt = evt || event;
					var xPos = function (evt) {
						if (evt.pageX) return evt.pageX;
						else if (evt.clientX)
							return evt.clientX + (document.documentElement.scrollLeft ?
							document.documentElement.scrollLeft :
							document.body.scrollLeft);
						else return null;
					}
					var yPos = function (evt) {
						if (evt.pageY) return evt.pageY;
						else if (evt.clientY)
							return evt.clientY + (document.documentElement.scrollTop ?
							document.documentElement.scrollTop :
							document.body.scrollTop);
						else return null;
					}
					joy.mouse.x = xPos(evt);
					joy.mouse.y = yPos(evt);
					joy.mouse.fireEvent(evt, 'onmove');
				};
				callback();
				return;
			}
			window.clearTimeout(joy.readyTimeout);
			joy.readyTimout = window.setTimeout(retry, 500);
		};
		joy.readyTimout = window.setTimeout(retry, 500);
	};

	joy.controls = {};

	joy.id = function () {
		if (!joy._id) {
			joy._id = 10000;
		}
		return joy._id++;
	}

	joy.get = function (target) {

	};

	joy.c = function (tag) {
		return el = document.createElement(tag);
	};

	joy.mouse = {
		x: 0, y: 0, onmove: {},
		fireEvent: function (evt, action, jid) {
			for (var i in this.onmove) {
				if (action && (!jid || i == jid)) {
					this[action][i](evt, action);
				}
			}
		}
	};

	joy.debug = {
		activated: false,
		acitvate: function () {
			var el = joy.c('div');
			el.style.width = '800px';
			el.style.height = '400px';
			el.style.position = 'absolute';
			el.style.border = 'solid 1px silver';
			el.style.bottom = '0px';
			el.style.paddingTop = '32px';

			var mx = joy.c('input');
			var my = joy.c('input');

			el.appendChild(mx);
			el.appendChild(my);

			mx.style.position = 'absolute';
			my.style.position = 'absolute';
			mx.style.left = '0px';
			mx.style.top = '0px';
			my.style.right = '0px';
			my.style.top = '0px';
			this.el = el;
			this.acitvated = true;
			joy.mouse.onmove['debug'] = function (evt, action) {
				mx.value = joy.mouse.x;
				my.value = joy.mouse.y;
			};
			document.body.appendChild(el);
		}
	};

	window.joy = joy;
})();
