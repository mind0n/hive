JLib_v1.Log = function (content, config, prefix, Count) {
	if (!JLib_v1.Log.El) {
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
		JLib_v1.Log.El = el;
	}
	if (!JLib_v1.debuging) {
		JLib_v1.Log.El.style.display = 'none';
	}
	if (content) {
		if (prefix && Count) {
			for (var i = 1; i <= Count; i++) {
				content = prefix + content;
			}
		}
		JLib_v1.Log.El.value = content + '\r\n' + JLib_v1.Log.El.value;
	}
	if (config) {
		for (var i in config) {
			var tmp = config[i];
			JLib_v1.Log.El.style[i] = tmp;
		}
	}
};
JLib_v1.LogObj = function(obj, recusive, curtdepth) {
	if (!curtdepth) {
		curtdepth = 0;
	}
	for (var i in obj) {
		JLib_v1.Log(i + '\t=\t' + obj[i], null, '-', curtdepth);
		if (recusive >= curtdepth && typeof (obj[i]) == 'object') {
			if (i == '$') {
				JLib_v1.LogObj(obj[i], 0, curtdepth + 1);
			} else {
				JLib_v1.LogObj(obj[i], recusive, curtdepth + 1);
			}
		}
	}
};
JLib_v1.log = JLib_v1.Log;
JLib_v1.logobj = JLib_v1.LogObj;
