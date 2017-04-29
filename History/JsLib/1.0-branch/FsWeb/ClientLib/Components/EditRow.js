JLib_v1.EditRowConfig = function () {
	var rlt = {
		elements: {
			currentEditingRowEl: null
		}
		, data: {
		}
		, behaviors: {
			MakeEditor: function (cellEl) { }
			, UpdateData: function (config) {
				var xml = JLib_v1.Xml.convert2xml(config.data, "param", true);
				if (!config.hiddenBox && config.hiddenBoxId) {
					config.hiddenBox = JLib_v1('#' + config.hiddenBoxId);
				}
				if (config.hiddenBox) {
					config.hiddenBox.value = xml;
				}
			}
		}
	};
	return rlt;
};

JLib_v1.EditRow = function (rowEl, config) {
	var edr = JLib_v1.EditRow;
	var type = typeof (rowEl);
	if (rowEl) {
		if (type == 'string') {
			rowEl = JLib_v1('#' + rowEl);
			type = typeof (rowEl);
		}
		if (rowEl.editable && rowEl.childNodes && !rowEl.editing) {
			rowEl.editing = true;

			config.elements.currentEditingRowEl = rowEl;
			for (var i = 0; i < rowEl.childNodes.length; i++) {
				var cellEl = rowEl.childNodes[i];
				cellEl.rowEl = rowEl;
				if (cellEl.isprimaryfield) {
					rowEl.primaryCell = cellEl;
				}
				if (cellEl.tagName == 'TD' && cellEl.editable) {
					var editor = config.behaviors.MakeEditor(cellEl, config);
					editor.container = cellEl;
					cellEl.editEl = editor;
					cellEl.innerHTML = "";
					cellEl.appendChild(editor);
					if (cellEl.isprimaryfield) {
						rowEl.primaryCell = cellEl;
					}
				}
			}
		}
	}
};