Fc.DataViewer = function (cfg) {
	var dv = new Object();
	dv.$ = cfg;
	dv.$.host = dv;
	dv.$$ = new Object();
	Fc.$[cfg.alias] = dv;
	dv.update = function (data) {
		var devt = dv.$.event;
		//var dispEl = Fc.g(dv.$.renderId);
		var dispEl = dv.$.renderEl;
		var updateEl;
		dispEl.innerHTML = "";
		devt.host = dv;
		dispEl.className = "dataviewer";
		dv.$$.dispEl = dispEl;
		dv.$$.updateEl = updateEl;
		dv.$$.gridRowEls = new Object();
		if (!dispEl.className || dispEl.className.indexOf(" " + dv.$.alias) < 0) {
			dispEl.className = dispEl.className + " " + dv.$.alias
		}
		if (devt.OnElUpdate) {
			updateEl = devt.OnElUpdate(dv, dispEl);
			if (!updateEl) {
				updateEl = dispEl;
			}
		}
		if (devt.OnHeaderUpdate) {
			devt.OnHeaderUpdate(dv, updateEl);
		}
		for (var i = 0; i < data.rows.length; i++) {
			if (devt.OnRowUpdate) {
				devt.OnRowUpdate(dv, data.rows[i], i, updateEl);
			}
		}
		if (dv.pager) {
			var pgparam = dv.$.request.param
			dv.pager.update(pgparam.page, pgparam.size, data.count);
		}
		if (dv.$.autoDisplayFormEditor && !dv.$.formeditor.allowmultiple) {
			if (dv.FormEditor) {
				dv.FormEditor.update(dv.$$.data.cols, dv.$.mask);
			}
		}
		if (dv.$.customEvent.OnUpdateCompleted) {
			dv.$.customEvent.OnUpdateCompleted(dv);
		}
	}
	dv.getNewRow = function () {
		var row = new Object();
		if (dv.$$.data) {
			var cols = dv.$$.data.cols;
			for (var p in cols) {
				row[p] = "";
			}
			//alert(row);
			return row;
		}
		//alert(dv.$$.data);
		return null;
	}
	dv.loadData = function (param) {
		var qrc = new Fc.QueuedRemoteCaller();
		if (!dv.$.request) {
			Fc.log("Error: Request missing in Fc.DataViewer configuration string. (" + dv.$.alias + ")");
		}
		if (param) {
			if (!dv.$.request.param) {
				dv.$.request.param = new Object();
			}
			dv.$.request.param = Fc.merge(dv.$.request.param, param);
		}

		qrc.add({
			CallBack: function (o, success) {
				if (!success) {
					alert(o.error);
				} else {
					if (dv.$.customEvent.OnLoadCompleted) {
						dv.$.customEvent.OnLoadCompleted(dv, o);
					}
					dv.$$.data = o;
					dv.update(o);
				}
			}
			, param: dv.$.request.param
			, method: dv.$.request.method
			, target: dv.$.request.target
		});
		qrc.send();
	}
	dv.init = function () {
		if (dv.$.pagerId) {
			//alert(dv.$.event.pager.OnPageSelected);
			dv.pager = new Fc.Pager({
				renderId: dv.$.pagerId
				, host: dv
				, OnPageSelected: dv.$.event.pager.OnPageSelected
			});
		}
		if (dv.$.formeditor) {
			dv.$.formeditor.parent = dv;
			if (!dv.$.formeditor.allowmultiple) {
				dv.FormEditor = new Fc.FormEditor(dv.$.formeditor);
				dv.FormEditor.parent = dv;
			}
		}
		dv.loadData();
	}
	dv.init();
	return dv;
}
Fc.Pager = function(cfg) {
	var pg = new Object();
	pg.$ = cfg;
	pg.$$ = new Object();
	pg.update = function(page, size, count) {
		var dispEl = Fc.g(pg.$.renderId);
		var range = 3;
		pg.$$.dispEl = dispEl;
		dispEl.innerHTML = "";
		var table = Fc.c("table");
		var tbody = Fc.c("tbody");
		var tr = Fc.c("tr");
		table.appendChild(tbody);
		tbody.appendChild(tr);
		table.className = "pager";
		var maxpage = parseInt(count / size);
		if (parseFloat(count / size) > maxpage) {
			maxpage++;
		}
		for (var i = page - range; i <= page + range; i++) {
			if (i > 0 && i <= maxpage) {
				var td = Fc.c("td");
				td.innerHTML = i;
				td.className = "page";
				td.pagenum = i;
				td.pagesize = size;
				td.pagecount = count;
				td.onclick = function() {
					if (pg.$.OnPageSelected) {
						pg.$.OnPageSelected(pg.$.host, td, this.pagenum);
					}
				}
				tr.appendChild(td);
			}
		}
		if (page > range + 1) {
			var td = Fc.c("td");
			td.className = "page";
			td.innerHTML = "...";
			tr.insertBefore(td, tr.firstChild);
			var td = Fc.c("td");
			td.className = "page";
			td.innerHTML = 1;
			td.onclick = function() {
				if (pg.$.OnPageSelected) {
					pg.$.OnPageSelected(pg.$.host, td, 1);
				}
			}
			//tr.appendChild(td);
			tr.insertBefore(td, tr.firstChild);
		}
		if (page + range < maxpage) {
			var td = Fc.c("td");
			td.className = "page";
			td.innerHTML = "...";
			tr.appendChild(td);
			var td = Fc.c("td");
			td.className = "page";
			td.innerHTML = maxpage;
			td.onclick = function() {
				if (pg.$.OnPageSelected) {
					pg.$.OnPageSelected(pg.$.host, td, maxpage);
				}
			}
			tr.appendChild(td);
		}
		//		if (parseFloat(count / size) > maxpage && i > maxpage) {
		//			var td = Fc.c("td");
		//			td.className = "page";
		//			td.innerHTML = maxpage + 1;
		//			td.onclick = function() {
		//				if (pg.$.OnPageSelected) {
		//					pg.$.OnPageSelected(pg.$.host, td, maxpage + 1);
		//				}
		//			}
		//			tr.appendChild(td);
		//		}
		dispEl.appendChild(table);
	}
	pg.init = function() {
	}
	return pg;
}