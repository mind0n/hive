Fc.DataForm = function(cfg) {
	var df = new Object();
	df.$ = cfg;
	cfg.host = df;
	df.resize = function() {
		var context = df.formContext;
		context.labels[0].style.width = "100px";
		var rectRenderEl = context.renderEl.getBoundingClientRect();
		var rectLabelEl = context.labels[0].getBoundingClientRect();
		for (var i = 0; i < context.ctrlist.length; i++) {
			var ctr = context.ctrlist[i];
			ctr.style.width = (rectRenderEl.right - rectRenderEl.left - rectLabelEl.right + rectLabelEl.left - df.$.cropwidth) + "px";
		}
	}
	df.setValue = function(context, fld, val) {
		if (context) {
			context.inputlist[fld].value = val;
		}
	}
	df.makeCmdCell = function(tr, text, onclick) {
		var td = document.createElement("td");
		var input = document.createElement("input");
		input.value = text;
		input.type = "button";
		if (onclick) {
			input.onclick = onclick;
		}
		td.appendChild(input);
		tr.appendChild(td);
		return input;
	}
	df.updateCmdArea = function(el, context) {
		var div = document.createElement("div");
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");
		var tr = document.createElement("tr");
		div.appendChild(table);
		table.appendChild(tbody);
		table.style.marginLeft = "auto";
		table.style.marginRight = "auto";
		tbody.appendChild(tr);
		if (df.$.OnCmdAreaUpdate) {
			df.$.OnCmdAreaUpdate(tr, df);
		} else {
			var btnSend, btnCancel;
			btnSend = df.makeCmdCell(tr, df.$.btnTextSend || "提交", function() {
				df.$.OnSendClick(context);
			});
			btnCancel = df.makeCmdCell(tr, df.$.btnTextCancel || "取消", function() {
				df.$.OnCancelClick(context);
			});
		}
		el.appendChild(div);
	}
	df.update = function(el, cols, row) {
		var context = new Object();
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");
		var i = 0;
		var queue = new Array();
		df.formContext = context;
		table.appendChild(tbody);
		table.className = "dataform";
		table.style.width = "100%";
		table.context = context;
		context.tableEl = table;
		context.renderEl = el;
		context.labels = new Array();
		context.ctrlist = new Array();
		context.inputlist = new Object();
		context.$ = df.$;
		for (var p in cols) {
			var tr = document.createElement("tr");
			var tdLabel = document.createElement("td");
			var tdCtr = document.createElement("td");
			var col = cols[p];
			var fieldStatus = df.$.fields[p];
			i++;
			tr.appendChild(tdLabel);
			tr.appendChild(tdCtr);
			tr.tableEl = table;
			tdLabel.className = "label";
			tdLabel.innerHTML = p;
			tdLabel.style.whiteSpace = "nowrap";
			tdLabel.tableEl = table;
			context.labels.push(tdLabel);
			tdCtr.className = "ctr";
			tdCtr.tableEl = table;

			if (df.$.OnFieldUpdate) {
				var ctr = df.$.OnFieldUpdate(context, col, row);
			} else {
				var ctr = document.createElement("input");
			}
			ctr.type = "text";
			ctr.readonly = true;
			tdCtr.appendChild(ctr);
			//alert(row);
			if (row) {
				ctr.value = row[p];
			} else {
				ctr.value = "";
			}

			// set style
			if (fieldStatus) {
				tr.style.display = fieldStatus.display;
				if (fieldStatus.header) {
					tdLabel.innerHTML = fieldStatus.header;
				}
				if (fieldStatus.readonly) {
					ctr.disabled = true;
				}
				tr.order = fieldStatus.order;
			}
			context.inputlist[p] = ctr;
			context.ctrlist.push(ctr);
			queue.push(tr);
			//tbody.appendChild(tr);
		}
		var order = 0;
		while (order < queue.length) {
			for (var i = 0; i < queue.length; i++) {
				if (queue[i].order == order) {
					tbody.appendChild(queue[i]);
					break;
				}
			}
			order++;
		}
		//el.innerHTML = "";
		el.appendChild(table);
		df.updateCmdArea(el, context);
		df.resize();
		if (df.$.OnUpdated) {
			df.$.OnUpdated(df, context);
		}
	}
	df.remove = function(row) {
		row.$ = df.$;
		df.$.OnSendClick(row);
	}
	return df;
}
