joy.cover = function (c) {
	var hide = c.hide;
	var target = c.target;
	var fixed = false;
	if (!target) {
		target = document.body;
		fixed = true;
	}
	var ovl = target.$ovl$;
	var spos = false;
	if (!ovl) {
		ovl = document.createElement('div');
		ovl.className = 'cover ';// + (fixed ? 'fixed' : '');
		ovl.$evtrap$ = true;
		target.$ovl$ = ovl;

		var initpos = target.astyle(['position']);
		console.log('Cover: ' + initpos);
		target.$position$ = initpos;

		if (!fixed && initpos == 'static') {
			target.style.position = 'relative';
			spos = true;
		}
		target.appendChild(ovl);
	}
	if (hide) {
		ovl.$displayed$ = false;
		ovl.style.display = 'none';
		if (ovl.$popup$) {
			destroy(ovl.$popup$);
		}
		if (spos) {
			target.style.position = target.$position$;
		}
	} else if (!ovl.$displayed$) {
		ovl.$displayed$ = true;
		ovl.style.display = '';
		if (c.popup) {
			var rect = ovl.getBoundingClientRect();
			var el = joy.jbuilder(c.popup);
			el.onclose = function () {
				joy.cover({ hide: true, target: target });
			}
			if (ovl.$popup$) {
				destroy(ovl.$popup$);
			}
			ovl.$popup$ = el;
			var arg = { target: target, pos: [rect.left, rect.top, rect.width / 2, rect.height / 2, rect.right, rect.bottom], details: c.details };
			if (fixed) {
				el.style.position = 'fixed';
			}
			if (el.popup) {
				el.popup(arg);
			} else {
				target.appendChild(el);
				var rc = el.getBoundingClientRect();
				el.style.left = arg.pos[2] - rc.width / 2 + 'px';
				el.style.top = arg.pos[3] - rc.height / 2 + 'px';
			}
			touchable(el, { mode: 'zsizing' });
			return el;
		}
	}
};