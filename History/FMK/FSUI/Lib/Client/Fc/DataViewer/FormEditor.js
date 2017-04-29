Fc.FormEditor = function (cfg) {
	var fe = new Fc.Control(cfg);
	fe.parent = cfg.parent;
	fe.pk = fe.parent.$.pk;
	//alert (fe.pk);
	fe.makeParam = function (cmdEl) {
		var rlt = new Object();
		var il = cmdEl.ItemList;
		rlt['_' + cmdEl.cmd] = true;
		for (var i = 0; i < il.length; i++) {
			var ctr = il[i];
			//alert (ctr.colname + "," + cmdEl.pk);
			if (ctr.colname == cmdEl.pk) {
				rlt['_' + cmdEl.cmd + '_pk'] = ctr.value;
			}
			rlt['_' + cmdEl.cmd + '_fld_' + ctr.colname] = ctr.value;
		}
		return rlt;
	}
	fe.submit = function (cmdEl) {
		var par = fe.makeParam(cmdEl);
		cmdEl.param = par;
		Fc.QueuedRemoteCall.add({
			CallBack: function (o, success) {
				if (!success) {
					alert(o.error);
				} else {
					fe.parent.loadData();
					if (fe.$.event.OnSubmitReturned) {
						fe.$.event.OnSubmitReturned(o, cmdEl);
					}
				}
			}
			, method: cmdEl.method
			, param: par
			, target: cmdEl.target
		});
		Fc.QueuedRemoteCall.send();
	}
	fe.update = function (cols, masks, row, dispEl) {
		if (!dispEl) {
			if (fe.$.renderEl) {
				dispEl = fe.$.renderEl;
			} else if (fe.$.renderId) {
				dispEl = Fc.g(fe.$.renderId);
			} else {
				Fc.log("Error: Missing display error(FormEditor) - " + fe.parent.alias);
				return;
			}
		}
		var event = fe.$.event;
		var updateEl;
		event.host = fe;
		fe.ItemList = new Array();
		dispEl.innerHTML = "";
		if (event.OnFormRendering) {
			updateEl = event.OnFormRendering(dispEl);
		}
		if (!row) {
			row = fe.$.parent.getNewRow();
		}
		fe.$$.row = row;
		fe.$$.cols = cols;
		for (var p in row) {
			if (event.OnItemRendering) {
				event.OnItemRendering(updateEl, row, p);
			}
		}
		if (event.OnFormRendered) {
			event.OnFormRendered(fe, dispEl);
		}
	}
	return fe;
}
