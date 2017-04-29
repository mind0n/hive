Fc.FormEditor.StandardFormEvents = {
	RenderCmdBtn: function (renderEl, properties) {
		var fe = this.host;
		var input = Fc.c("input");
		//		input.type = "button";
		//		input.value = text;
		//		input.onclick = OnBtnClick;
		input.fe = fe;
		input.ItemList = fe.ItemList;
		input.pk = fe.pk;
		Fc.apply(input, properties);
		if (!input.onclick) {
			input.onclick = function () {
				this.fe.submit(this);
			}
		}
		renderEl.appendChild(input);
	}
	, OnSubmitReturned: function (o, cmdEl) {
		var fe = this.host;
		if (fe.$.OnSubmitReturned) {
			if (!fe.$.OnSubmitReturned(o, cmdEl)) {
				return;
			}
		}
		if (o.content) {
			alert(o.content);
		}
		if (o.error) {
			Fc.log(o.error);
		}
	}
	, OnFormRendering: function (dispEl) {
		var table = Fc.c("table");
		var tbody = Fc.c("tbody");
		table.appendChild(tbody);
		table.className = "formeditor";
		dispEl.appendChild(table);
		return tbody;
	}
	, OnFormRendered: function (fe, dispEl) {
		var btns = this.host.parent.$.formeditor.commands || new Object();
		var div = Fc.c("div");
		dispEl.appendChild(div);
		div.className = "formeditor cmdarea";
		for (var p in btns) {
			this.RenderCmdBtn(div, btns[p]);
		}
	}
	, OnItemRendering: function (updateEl, row, pos) {
		var fe = this.host;
		var tr = Fc.c("tr");
		var la = Fc.c("td");
		var ct = Fc.c("td");
		var masks = fe.parent.$.mask;
		var maskshow = fe.parent.$.maskshow;
		var ctr;

		tr.appendChild(la);
		tr.appendChild(ct);
		tr.className = "item";
		la.className = "label";
		ct.className = "container";
		la.innerHTML = pos;
		var mask = masks[pos];
		if (mask) {
			if (mask.headertext) {
				la.innerHTML = mask.headertext;
			}
			tr.style.display = mask.formdisplay;
		}
		ctr = Fc.c("input");
		ctr.className = "control";
		ctr.colname = pos;
		ctr.defvalue = row[pos];
		ct.appendChild(ctr);

		fe.ItemList.push(ctr);
		if (row) {
			ctr.value = row[pos];
		}
		if (maskshow) {
			if (!masks[pos]) {
				tr.style.display = "none";
			}
		}
		updateEl.appendChild(tr);
	}
}