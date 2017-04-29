if (!Fc.DataViewer) {
	Fc.DataViewer = new Object();
}
Fc.DataViewer.GridViewerEvents = {
	CheckCellContentSize: function(el, content) {
		el.innerHTML = content;
		var sw = el.scrollWidth;
		var ew = parseInt(el.style.width);
		var rect = el.getBoundingClientRect();
		var len = 0;
		if (!ew) {
			ew = rect.right - rect.left;
		}

		if (sw > ew) {
			len = parseInt(ew / sw * (content.length - 9));
			el.innerHTML = content.substr(0, len) + ' ...';
		}
		len = el.innerHTML.length;
		while (sw > ew) {
			el.innerHTML = content.substr(0, len--) + ' ...';
			sw = el.scrollWidth;
		}
		content += '';
		if (len != content.length) {
			el.innerHTML = content.substr(0, len--) + ' ...';
		}

	}
	, OnHeaderColResize: function(DataViewer, target) {
		var colEl = target.headerEl;
		var cells = DataViewer.$$.gridRowEls[colEl.dataIndex];
		var cfg = this;
		//alert();
		//tb.scrollLeft = target
		for (var i = 0; i < cells.length; i++) {
			var cell = cells[i];

			cell.style.width = colEl.dragger.parentCell.style.width;
			//alert(cell.scrollWidth + "," + cell.style.width);
			cfg.CheckCellContentSize(cell, cell.row[colEl.dataIndex]);
			//cell.style.background = "blue";
		}
	}
	, OnHeaderColClick: function(evt) {
		var colEl = Fc.v(evt);
		var colname = colEl.dataIndex;
		var dv = colEl.dv;
		if (!dv.orderlist) {
			dv.orderlist = Fc.clone(dv.$$.data.cols);
		}
		var orders = dv.orderlist;
		if (orders[colname] == "^") {
			orders[colname] = "";
		} else {
			orders[colname] = "^";
		}
		//alert(dv.orderlist[colname].asc);
		this.dv.loadData({ order: colname + orders[colname] });
	}
	, OnElUpdate: function(DataViewer, dispEl) {
		var table = Fc.c("table");
		var tbody = Fc.c("tbody");
		table.appendChild(tbody);
		dispEl.appendChild(table);
		tbody.parentEl = table;
		return tbody;
	}
	, OnCellContentUpdate: function(el, content) {
		var divl = "<div class='leftspacing' style='float:left;'></div>";
		var divr = "<div class='rightspacing' style='float:right'></div>";
		el.innerHTML = divl + content + divr;
	}
	, OnHeaderUpdate: function(DataViewer, updateEl) {
		var dv = DataViewer;
		var masks = DataViewer.$.mask;
		var cols = DataViewer.$$.data.cols;
		var mask = null;
		var tr = Fc.c("tr");
		var cfg = this;
		tr.className = "headerow";
		tr.style.cursor = "default";
		updateEl.appendChild(tr);

		for (var p in cols) {
			var realth = Fc.c("th");
			var div = Fc.c("div");

			var th = Fc.c("div");
			var dragger = Fc.c("div");
			realth.appendChild(div);
			div.appendChild(th);
			th.className = "headercell " + p;
			th.style.cursor = "pointer";
			div.style.position = "relative";
			th.innerHTML = p;
			dv.$$.gridRowEls[p] = new Array();
			th.cols = cols;
			if (masks && masks[p]) {
				mask = masks[p];
				realth.style.display = mask.gridisplay;
				th.style.textAlign = mask.gridalign;
				//th.style.width = "80px";
				//th.style.overflow = "hidden";
				if (mask.headertext) {
					th.innerHTML = mask.headertext;
				}
				if (mask.OnRendered) {
					mask.OnRendered(th, cols, p, true);
				}
			}
			th.cols = cols;
			th.dataIndex = p;
			th.rowEl = tr;
			th.dataType = cols[p];
			th.dv = DataViewer;
			if (DataViewer.$.maskshow) {
				if (masks[p]) {
					tr.appendChild(realth);
				}
			} else {
				tr.appendChild(realth);
			}
			th.onclick = this.OnHeaderColClick;
			DataViewer.$$.headerow = tr;
			div.appendChild(dragger);
			dragger.className = "dragger";
			dragger.style.position = "absolute";
			dragger.style.width = "12px";
			dragger.style.height = "100%";
			dragger.style.right = "0px";
			dragger.style.top = "0px";
			dragger.style.cursor = "e-resize";
			dragger.style.margin = "0px 0px 0px 0px";
			dragger.style.padding = "0px 0px 0px 0px";
			dragger.style.zIndex = 1000;
			dragger.parentCell = th;
			dragger.scrollHeader = tr;
			dragger.scrollBody = updateEl.parentEl;
			dragger.headerEl = th;
			dragger.innerHTML = "&nbsp;";
			dragger.dv = DataViewer;
			th.dragger = dragger;
			th.dv = DataViewer;
			div.dv = DataViewer;
			realth.dv = DataViewer;

			$(dragger).draggable({
				axis: 'x'
				, helper: new Object()
				, start: function(evt, ui) {
					var evt = window.event || evt;
					var target = evt.srcElement || evt.target;
					ui.helper.target = target;
				}
				, drag: function(evt, ui) {
					var target = ui.helper.target;
					var rtar = target.getBoundingClientRect();
					var rcel = target.parentCell.getBoundingClientRect();
					if (rtar.right - rcel.left > 32) {
						target.parentCell.style.width = (rtar.right - rcel.left) + 'px';
					}

					cfg.OnHeaderColResize(dv, target);
				}
				, stop: function(evt, ui) {
					var target = ui.helper.target;
					target.style.left = "";
					target.style.right = "0px";
				}
			});
			if (this.OnCellContentUpdate) {
				this.OnCellContentUpdate(th, th.innerHTML);
			}
		}
	}
	, OnRowUpdate: function(DataViewer, row, pos, updateEl) {
		var dv = DataViewer;
		var masks = DataViewer.$.mask;
		var mask = null;
		var tr = Fc.c("tr");
		var cssname = "datarow";
		if (pos % 2 == 0) {
			cssname += " odd";
		} else {
			cssname += " even";
		}
		tr.className = cssname;
		tr.style.cursor = "default";
		tr.rowdata = row;
		tr.cols = DataViewer.$$.data.cols;
		tr.DataViewer = DataViewer;
		tr.onclick = function() {
			var dv = this.DataViewer;
			var FormEditor;
			if (dv.$.formeditor) {
				if (dv.$.formeditor.allowmultiple) {
					FormEditor = new Fc.FormEditor(dv.$.formeditor);
				} else {
					if (!dv.FormEditor) {
						dv.FormEditor = new Fc.FormEditor(dv.$.formeditor);
					}
					FormEditor = dv.FormEditor;
				}
				FormEditor.parent = dv;
				FormEditor.update(tr.cols, DataViewer.mask, tr.rowdata);
			}
		}
		updateEl.appendChild(tr);
		for (var p in row) {
			var realtd = Fc.c("td");
			var td = Fc.c("div");
			realtd.appendChild(td);
			td.className = "datacell " + p;
			td.dv = DataViewer;
			td.row = row;
			td.innerHTML = row[p];
			dv.$$.gridRowEls[p].push(td);
			if (masks && masks[p]) {
				mask = masks[p];
				td.style.display = mask.gridisplay;
				td.style.textAlign = mask.gridalign;
				//td.style.width = "80px";
				//td.style.overflow = "hidden";
				if (mask.OnRendered) {
					mask.OnRendered(td, row, p);
				}
			}
			if (DataViewer.$.maskshow) {
				if (masks[p]) {
					tr.appendChild(realtd);
				}
			} else {
				tr.appendChild(realtd);
			}
			this.CheckCellContentSize(td, row[p]);
			if (this.OnCellContentUpdate) {
				this.OnCellContentUpdate(td, td.innerHTML);
			}
		}
	}
	, pager: {
		OnPageSelected: function(dv, el, page) {

			dv.loadData({
				page: page
			});
		}
	}
}
EmptyViewerEvents = {
	OnElUpdate: function(DataViewer, dispEl) {
		var table = Fc.c("table");
		var tbody = Fc.c("tbody");
		table.appendChild(tbody);
		dispEl.appendChild(table);
		return tbody;
	}
	, OnRowUpdate: function(DataViewer, row, updateEl) {
		var tr = Fc.c("tr");
		updateEl.appendChild(tr);
		for (var p in row) {
			var td = Fc.c("td");
			td.innerHTML = row[p];
			tr.appendChild(td);
		}
	}
}