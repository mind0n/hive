(function () {
	var watchlist = [];
	function findhandler(d) {
		for (var i = 0; i < watchlist.length; i++) {
			var kv = watchlist[i];
			if (kv.model == d) {
				return kv;
			}
		}
		return null;
	}
	function createhandler(d, c) {
		var h = {
			model: d,
			changes: {},
			addchange: function (c) {
				var chlist = this.changes[c.name];
				if (!chlist) {
					chlist = [c.change];
					this.changes[c.name] = chlist;
				} else {
					for (var i = 0; i < chlist.length; i++) {
						var fn = chlist[i];
						if (fn == c.change) {
							return;
						}
					}
					chlist[chlist.length] = c.change;
				}
			}
		};
		h.addchange(c);
		watchlist[watchlist.length] = h;
		return h;
	}

	function getforward(c) {
		var el = null;
		if (c && c.forward) {
			var t = typeof (c.forward);
			if (t == 'string') {
				el = document.getElementById(c.forward);
			} else if (t == 'function') {
				el = c.forward();
			} else {
				el = c.forward;
			}
		}
		return el;
	}

	var dhandler = {
		typechange: function (p, c) {
			function evtchange(event) {
				if (!event.keyCode) {
					p[c.name] = this.value;
				} else if (!event.altKey && !event.ctrlKey) {
					if (event.keyCode >= 33 && event.keyCode <= 40 || event.keyCode == 16) {
						// do nothing
					} else if (event.keyCode == 46 || event.keyCode == 8) {

					} else {
						p[c.name] = this.value;
					}
				}
			}
			var el = getforward(c);
			if (el) {
				el.addEventListener('onkeyup', evtchange);
				el.addEventListener('onchange', evtchange);
				el.addEventListener('onblur', evtchange);
			} else {
				console.log('Forward target missing');
			}
		}, attrchange: function (p, c) {
			// select the target node
			var args = (c && c.args) ? c.args.init : {};
			var target = getforward(args);
			if (target) {
				if (args.forward) {
					target.addEventListener(args.evt, function (event) {
						this.setAttribute(args.prop, this[args.prop]);
					});
					// create an observer instance
					var observer = new MutationObserver(function (mutations) {
						mutations.forEach(function (mutation) {
							if (mutation.type == 'attributes') {
								var target = mutation.target;
								p[c.name] = target.getAttribute(mutation.attributeName);
							}
						});
					});

					// configuration of the observer:
					var config = { attributes: true, childList: true, characterData: true };

					target.setAttribute(args.prop, p[c.name]);
					// pass in the target node, as well as the observer options
					observer.observe(target, config);

					// later, you can stop observing
					//observer.disconnect();
				}
			} else {
				console.log('Forward target missing');
			}
		}, propchange: function (p, c) {
			// select the target node
			var target = getforward(c);
			if (target) {
				watchp(target, {
					name: 'value'
				}, [function (model, property, value, origin) {
					console.log(model);
					console.log(property);
					console.log(value);
				}]);
			} else {
				console.log('Forward target missing');
			}
		}
	}
	function gethandler(key) {
		if (!key) {
			return null;
		}
		var t = typeof (key);
		if (t == 'string') {
			return dhandler[key];
		} else {
			return key;
		}
	}
	function watchp(d, c, chlist) {
		var ov = d[c.name], nv = ov;
		if (delete d[c.name]) {
			if (Object.defineProperty) {
				Object.defineProperty(d, c.name, {
					get: function () {
						return nv;
					},
					set: function (val) {
						if (nv == val) {
							console.log('No change:' + val);
						} else {
							if (chlist && chlist.length > 0) {
								for (var i = 0; i < chlist.length; i++) {
									var fn = chlist[i];
									fn(d, c.name, val, nv);
								}
							}
							nv = val;
						}
					}
				});
				var initer = gethandler(c.init);
				if (initer) {
					initer(d, c);
				}
				d[c.name] = nv;
				return true;
			} else {
				console.log('Define property not supported');
				debugger;
			}
		} else {
			console.log('Cannot watch ' + c.name);
			debugger;
		}
		return false;
	}
	function watch(d, c, undef) {
		var ov = d[c.name], nv = ov;
		var h = findhandler(d);
		var initer = gethandler(c.init);
		if (!h) {
			h = createhandler(d, c);
			watchp(d, c, h.changes[c.name]);
		} else {
			h.addchange(c);
			if (initer) {
				initer(d, c);
			}
			return true;
		}

		return false;
	}
	window.watch = watch;
})();