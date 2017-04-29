Fc.TabController = function(cfgId) {
	var o = new Fc.CtrBase(cfgId);
	o.tabs = new Array();
	o.showTab = function(tab) {
		if (typeof (tab) != "object") {
			tab = o.g(tab).belongTab;
		}
		o.enumTab(function(capEl, cntEl) {
			cntEl.style.display = "none";
			capEl.className = o.$.cssNormal;
		});
		tab.cntEl.style.display = "block";
		tab.capEl.className = o.$.cssActive;
	}
	o.hoverTab = function(tab) {
		o.enumTab(function(capEl, cntEl) {
			capEl.style.cursor = "pointer";
			if (capEl.className != o.$.cssActive) {
				capEl.className = o.$.cssNormal;
			}
		});
		tab.capEl.className = o.$.cssHover;
	}
	o.unhoverTabs = function() {
		o.enumTab(function(capEl) {
			if (capEl.className != o.$.cssActive) {
				capEl.className = o.$.cssNormal;
			}
		});
	}
	o.enumTab = function(callback) {
		for (var i = 0; i < o.tabs.length; i++) {
			var tab = o.tabs[i];
			callback(tab.capEl, tab.cntEl);
		}
	}
	o._init = function() {
		if (o.$.alias) {
			window["$" + o.$.alias] = o;
		}
		if (!o.$.cssActive) {
			o.$.cssActive = "active";
		}
		if (!o.$.cssHover) {
			o.$.cssHover = "hover";
		}
		if (!o.$.cssNormal) {
			o.$.cssNormal = "normal";
		}
		for (var i = 0; i < o.$.tabs.length; i++) {
			var tab = new Object();
			tab.capEl = o.g(o.$.tabs[i].captionid);
			tab.cntEl = o.g(o.$.tabs[i].contentid);
			if (!tab.capEl || !tab.cntEl) {
				alert("Incorrect element id ('" + o.$.tabs[i].captionid + "' or '" + o.$.tabs[i].contentid + "')");
			}
			tab.capEl.belongTab = tab;
			tab.capEl.cntEl = tab.cntEl;
			tab.cntEl.belongTab = tab;
			tab.cntEl.capEl = tab.capEl;
			o.tabs.push(tab);
			if (tab.capEl.id != o.$.activeId) {
				tab.cntEl.style.display = "none";
			}
			tab.capEl.onmouseover = function(evt) {
				o.hoverTab(this.belongTab);
			}
			tab.capEl.onclick = function(evt) {
				o.showTab(this.belongTab);
			}
			tab.capEl.onmouseout = function(evt) {
				o.unhoverTabs();
			}
		}
	}
	o._init();
	return o;
}
