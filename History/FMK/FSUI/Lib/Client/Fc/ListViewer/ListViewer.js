Fc.GridViewer = function(cfg) {

}
Fc.SimpleListViewer = function(cfg) {
	cfg.OnElUpdate = function(renderEl) {
		var table = document.createElement('table');
		var tbody = document.createElement('tbody');
		table.className = 'content';
		table.appendChild(tbody);
		renderEl.innerHTML = "";
		renderEl.appendChild(table);
		return tbody;
	}
	cfg.OnRowUpdate = function(el, row, pos) {
		var tr = document.createElement('tr');
		var ctitle = document.createElement('td');
		var cdate = document.createElement('td');
		var item = document.createElement('a');
		var d = new Date(row.updatetime);
		item.setAttribute('target', '_blank');
		item.setAttribute('href', 'front.aspx?cid=' + row.bigclassid + '&nid=' + row.newsid);
		item.innerHTML = row.title;
		ctitle.className = 'newstitle';
		cdate.className = 'newsdate';
		ctitle.appendChild(item);

		cdate.innerHTML = row.updatetime.substr(0, row.updatetime.indexOf(' '));
		tr.appendChild(ctitle);
		tr.appendChild(cdate);
		el.appendChild(tr);
		return tr;
	}
	var slv = new Fc.ListViewer(cfg);
	return slv;
}

Fc.ListViewer = function(cfg) {
	// OnElUpdate(renderEl);
	// OnRowUpdate(activeEl, rows[i], i);
	var lv = new Object();
	cfg.host = lv;
	lv.$ = cfg;
	lv.data = null;
	lv.rows = null;
	lv.status = "wild";
	lv.addRow = function(row, compareFld) {
		if (compareFld) {
			for (var i = 0; i < lv.data.rows.length; i++) {
				var r = lv.data.rows[i];
				if (r[compareFld] == row[compareFld]) {
					lv.data.rows[i] = row;
					lv.update();
					return;
				}
			}
			lv.data.rows.push(row);
		} else {
			lv.data.rows.push(row);
		}
		lv.update();
	}
	lv.removeRow = function(row, compareFld) {
		var rlt = new Array();
		if (compareFld) {
			for (var i = 0; i < lv.data.rows.length; i++) {
				var r = lv.data.rows[i];
				if (r[compareFld] == row[compareFld]) {
					continue;
				}
				rlt.push(r);
			}
		} else {
			for (var i = 0; i < lv.data.rows.length; i++) {
				var found = true;
				var r = lv.data.rows[i];
				for (var p in r) {
					if (r[p] != row[p]) {
						found = false;
						break;
					}
				}
				if (found) {
					continue;
				}
				rlt.push(r);
			}
		}
		lv.data.rows = rlt;
		lv.update();
	}
	lv.exist = function(fld, val) {
		if (!lv.data || !lv.data.rows) {
			return false;
		}
		if (lv.data.rows.length && lv.data.rows.length > 0) {
			for (var i = 0; i < lv.data.rows.length; i++) {
				var row = lv.data.rows[i];
				if (row[fld] == val) {
					return row;
				}
			}
			return false;
		}
	}
	lv.update = function(data, text) {
		lv.status = "updating";
		if (lv.$.OnBeforeUpdate) {
			if (data) {
				data = lv.$.OnBeforeUpdate(lv, data, text);
			}
		}
		if (data) {
			lv.data = data;
		} else {
			data = lv.data;
		}
		var rows = data.rows;
		var cols = data.cols;
		var table = data.table;
		var renderEl = document.getElementById(lv.$.renderId);
		var activeEl;
		lv.rows = rows;
		lv.$.renderEl = renderEl;
		renderEl.innerHTML = "";
		if (lv.toolbar) {
			var toolbarEl = document.createElement("");
			renderEl.appendChild(toolbarEl);
			lv.toolbar.$.renderEl = toolbarEl;
			lv.toolbar.$.data = data;
			lv.toolbar.update(toolbarEl);
		}
		if (renderEl && lv.$.OnElUpdate) {
			activeEl = lv.$.OnElUpdate(renderEl);
		}

		for (var i = 0; i < rows.length; i++) {
			if (lv.$.OnRowUpdate) {
				var curtrow = rows[i];
				var rowEl = lv.$.OnRowUpdate(activeEl, rows[i], i, lv);
				if (rowEl && lv.$.liststyle && lv.$.liststyle.pointer) {
					rowEl.style.cursor = "pointer";
				}
				if (lv.$.dispRowModifier) {

					if (rowEl.tagName.toLowerCase() == "tr") {
						var elRowEdt = document.createElement("td");
						var elRowDel = document.createElement("td");
						elRowEdt.rowdata = curtrow;
						elRowDel.rowdata = curtrow;
						elRowDel.innerHTML = "X";
						elRowEdt.innerHTML = "#";
					} else {
					}
					rowEl.appendChild(elRowEdt);
					rowEl.appendChild(elRowDel);
					elRowDel.onclick = function() {
						if (lv.$.OnRowDelete) {
							lv.$.OnRowDelete(this.rowdata, lv);
						} else {
							lv.OnRowDelete(this.rowdata);
						}
					}
					elRowEdt.onclick = function() {
						if (lv.$.OnRowEdit) {
							lv.$.OnRowEdit(this.rowdata, lv);
						} else {
							lv.OnRowEdit(this.rowdata);
						}
					}
				}
			}
		}
		if (lv.pager) {
			lv.pager.update();
		}
		if (lv.$.OnLoadComplete) {
			lv.$.OnLoadComplete(this);
		}
		lv.status = "updated";
	}
	lv.load = function(target) {
		lv.status = "loading";
		if (lv.$.method) {
			if (lv.$.OnMethodCalling) {
				lv.$.OnMethodCalling(lv);
			}
			var r = new Fc.Request(function(text, success) {
				if (success) {
					o = Fc.parseJsonStr(text);
					lv.update(o, text);
				} else {
					document.body.innerHTML += "<div style='display:none;'>" + text + "</div>";
				}
			});
			r.Form.add('methodname', lv.$.method);
			var i = 0;
			for (var p in lv.$.param) {
				r.Form.add('par_' + i, lv.$.param[p]);
				i++;
			}
			if (!target) {
				r.send(lv.$.provider);
			} else {
				r.send(target);
			}
		} else {
			var r = new Fc.Request(function(text, success) {
				var o;
				if (success) {
					o = Fc.parseJsonStr(text);
					lv.update(o, text);
					return;
				}

			});
			r.Form.add('a', 'select');
			r.Form.add('act', 'paged');
			r.Form.add('qstyle', 'mssql_paged');
			r.Form.add('_pagesize', lv.$.pagesize);
			r.Form.add('_curtpage', lv.$.curtpage);
			r.Form.add('_tables', lv.$.target);
			for (var i = 0; i < lv.$.fields.length; i++) {
				var item = lv.$.fields[i];
				r.Form.add('_fields', item.field);
				r.Form.add('t_' + item.field, item.type);
			}
			for (var i = 0; i < lv.$.conditions.length; i++) {
				var item = lv.$.conditions[i];
				r.Form.add('_wheres', item);
			}
			for (var i = 0; i < lv.$.sequence.length; i++) {
				var item = lv.$.sequence[i];
				r.Form.add('_orders', item);
			}
			if (!target) {
				r.send(lv.$.provider);
			} else {
				r.send(target);
			}
		}
		lv.status = "loaded";
	}
	lv.init = function() {
		lv.status = "initing";
		if (lv.$.pagerId) {
			lv.pager = new Fc.ListViewer.Pager({
				renderId: lv.$.pagerId
				, range: 5
				, OnPageSelect: function(param) {
					if (lv.$.OnPageSelect) {
						lv.$.OnPageSelect(lv, param);
					}
				}
			});
		}
		if (lv.$.disptoolbar) {
			lv.toolbar = new Fc.ListViewer.Toolbar({
				ListViewer: lv
				, OnAdd: lv.OnAddItem
			});
		}
		lv.status = "inited";
	}
	//-------------------------------------------------------------------
	lv.OnRowEdit = function(row) {
		var div = document.createElement("div");
		var dataForm = new Fc.DataForm(lv.$.DataForm);
		var dialogOpts = {
			autoOpen: true,
			width: 300,
			height: "auto",
			minWidth: 150,
			minHeight: 150,
			title: lv.$.DataForm.title,
			resize: function() {
				dataForm.resize();
			}
		};
		if (lv.data) {
			$(div).dialog(dialogOpts);
			dataForm.$.mode = "update";
			dataForm.update(div, lv.data.cols, row);
		}
	}
	lv.OnRowDelete = function(row) {
		var dataForm = new Fc.DataForm(lv.$.DataForm);
		dataForm.$.mode = "remove";
		dataForm.remove(row);
	}
	lv.OnAddItem = function() {
		var div = document.createElement("div");
		var dataForm = new Fc.DataForm(lv.$.DataForm);
		var dialogOpts = {
			autoOpen: true,
			width: 300,
			height: "auto",
			minWidth: 150,
			minHeight: 150,
			title: lv.$.DataForm.title,
			resize: function() {
				dataForm.resize();
			}
		};
		if (lv.data) {
			$(div).dialog(dialogOpts);
			dataForm.$.mode = "insert";
			dataForm.update(div, lv.data.cols);
		}
	}
	lv.init();
	Fc.$[cfg.alias] = lv;
	if (!lv.$.skipload) {
		lv.load();
	}
	return lv;
}
Fc.ListViewer.Toolbar = function(cfg) {
	var tb = new Object();
	tb.$ = cfg;
	tb.makeCmdBtn = function(el, text, onclick) {
		var td = document.createElement("td");
		var input = document.createElement("input");
		input.value = text;
		input.type = "button";
		input.onclick = onclick;
		td.appendChild(input);
		el.appendChild(td);
	}
	tb.update = function(renderEl) {
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");
		var tr = document.createElement("tr");
		table.appendChild(tbody);
		tbody.appendChild(tr);
		tb.makeCmdBtn(tr, tb.$.btnTextAdd || "添加", function() {
			if (tb.$.OnAdd) {
				tb.$.OnAdd();
			}
		});
		renderEl.appendChild(table);
	}
	return tb;
}
Fc.ListViewer.Pager = function(cfg) {
	var pg = new Object();
	pg.$ = cfg;
	pg.$$ = new Object();
	pg.update = function(curt, psize, totalrecords, totalpages) {
		var pagecount;
		var renderEl = document.getElementById(pg.$.renderId);
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");
		var tr = document.createElement("tr");

		if (!curt) {
			curt = pg.$$.curtpage;
		}
		if (!psize) {
			psize = pg.$$.pagesize;
		}
		if (!totalrecords) {
			totalrecords = pg.$$.totalrecords;
		}
		if (!totalpages) {
			totalpages = pg.$$.totalpages;
		}
	
		table.appendChild(tbody);
		tbody.appendChild(tr);
		
		table.className = "pager";
		tr.className = "row";
		
		if (psize == 0) {
			pagecount = 0;
		} else {
			pagecount = totalpages;
		}
		curt = parseInt(curt);
		
		for (var i = curt - pg.$.range; i <= curt + pg.$.range; i++) {
			if (i > 0 && i <= pagecount) {
				var td = document.createElement("td");
				td.innerHTML = i;
				if (i == curt) {
					td.className = "curtpage";
				} else {
					td.className = "page";
				}
				td.style.cursor = "pointer";
				td.param = new Object();
				td.param.curtpage = i;
				td.param.pagesize = psize;
				td.param.pages = pagecount;
				td.param.el = td;
				td.onclick = function() {
					if (pg.$.OnPageSelect) {
						pg.$.OnPageSelect(this.param);
					}
				}
				tr.appendChild(td);
			}
		}
		renderEl.innerHTML = "";
		renderEl.appendChild(table);
	}
	return pg;
}
/*
//Cross Table Select
var newslist = new Fc.ListViewer({
provider: "DataProvider/DataPortal.aspx"
, alias:"newslist"
, target: "News,BigClass"
, hidepager: 'true'
, pagesize: 5
, curtpage: 1
, pk: 'News.NewsID'
, fields: [
{ field: "News.NewsID", type: "int", display: "none" }
, { field: "News.UpdateTime", type: "string", display: "" }
, { field: "News.bigclassid", type: "int", display: "none" }
, { field: "News.Title", type: "string" }
, { field: "News.editor", type: "string" }
, { field: "News.Content", type: "int" }
, { field: "BigClass.BigClassName", type: "string", display: "none" }
, { field: "BigClass.BigClassID", type: "int" }
]
, conditions: [
"News.bigclassid$=$[BigClass].[BigClassID]"
, "BigClass.BigClassID$=$61"
]
, sequence: [
"News.UpdateTime$desc"
]
});
		
//Normal Select		
var newslist = new Fc.ListViewer({
provider: "DataProvider/DataPortal.aspx"
, alias: "noticelist"
, renderId: "noticelist"
, target: "News"
, hidepager: 'true'
, pagesize: 5
, curtpage: 1
, pk: 'NewsID'
, fields: [
{ field: "NewsID", type: "int", display: "none" }
, { field: "UpdateTime", type: "string", display: "" }
, { field: "bigclassid", type: "int", display: "none" }
, { field: "Title", type: "string" }
, { field: "editor", type: "string" }
, { field: "Content", type: "int" }
]
, conditions: [
"bigclassid$=$147"
]
, sequence: [
"UpdateTime$desc"
]
, OnElUpdate: function(renderEl) {
var table = document.createElement('table');
var tbody = document.createElement('tbody');
table.className = 'content';
table.appendChild(tbody);
renderEl.innerHTML = "";
renderEl.appendChild(table);
return tbody;
}
, OnRowUpdate: function(el, row, pos) {
var tr = document.createElement('tr');
var ctitle = document.createElement('td');
var cdate = document.createElement('td');
var item = document.createElement('a');
var d = new Date(row.updatetime);
item.setAttribute('target', '_blank');
item.setAttribute('href', 'front.aspx?cid=' + row.bigclassid + '&nid=' + row.newsid);
item.innerHTML = row.title;
ctitle.className = 'newstitle';
cdate.className = 'newsdate';
ctitle.appendChild(item);
					
cdate.innerHTML = row.updatetime.substr(0, row.updatetime.indexOf(' '));
tr.appendChild(ctitle);
tr.appendChild(cdate);
el.appendChild(tr);
return tr;
}
});

var newslist = new Fc.ListViewer({
provider: "DataProvider/ChatPortal.aspx"
, alias: "chatlist"
, target: "tb_Messages"
, hidepager: 'true'
, pagesize: 5
, curtpage: 1
, renderId: "chatinfo"
, OnElUpdate: function(renderEl) {
return renderEl;
}
, OnRowUpdate: function(el, row, pos) {
el.innerHTML += Encoder.htmlDecode(row.msgtext) + "<br />";
return;
}
, OnLoadComplete: function(list) {
}
, method: "GetMessages"
, params: [

]
});

*/