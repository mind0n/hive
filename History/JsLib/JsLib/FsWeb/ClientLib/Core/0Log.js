J.Log = function (content, config, prefix, Count) {
	if (!J.Log.El) {
		var el = document.createElement('textarea');
		el.style.position = 'fixed';
		if (!config) {
			el.style.left = '0px';
			el.style.bottom = '0px';
		}
		el.style.width = '100%';
		el.style.height = '400px';
		el.style.overflow = 'scroll';
		document.body.appendChild(el);
		J.Log.El = el;
	}
	if (!J.debuging) {
		J.Log.El.style.display = 'none';
	}
	if (content) {
		if (prefix && Count) {
			for (var i = 1; i <= Count; i++) {
				content = prefix + content;
			}
		}
		J.Log.El.value = content + '\r\n' + J.Log.El.value;
	}
	if (config) {
		for (var i in config) {
			var tmp = config[i];
			J.Log.El.style[i] = tmp;
		}
	}
};
J.LogObj = function(obj, recusive, curtdepth) {
	if (!curtdepth) {
		curtdepth = 0;
	}
	for (var i in obj) {
		J.Log(i + '\t=\t' + obj[i], null, '-', curtdepth);
		if (recusive >= curtdepth && typeof (obj[i]) == 'object') {
			if (i == '$') {
				J.LogObj(obj[i], 0, curtdepth + 1);
			} else {
				J.LogObj(obj[i], recusive, curtdepth + 1);
			}
		}
	}
};
J.log = J.Log;
J.logobj = J.LogObj;
