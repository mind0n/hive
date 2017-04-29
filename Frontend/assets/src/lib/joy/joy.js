(function () {
	var curtid = 0;
	var mergeprop = '$merge$';
	function bindCusor(r, ced, cs) {
		r.$parent$ = ced;
		if (ced.$isgroup$) {
			r.$group$ = ced;
		} else {
			r.$group$ = cs.group;
		}
	}
	function jbuilder(json, cs) {
		var crs = joy.creators;
		var c = null;
		var key = null;
		for (var i in crs) {
			if (json[i] && typeof (json[i]) == 'string') {
				key = json[i];
				c = crs[i];
			}
		}
		if (!key) {
			alert('Creator not found');
			debugger; // tag not found;
			return null;
		}
		if (!json) {
			return null;
		}
		else if (json.length && json.length > 0) {
			var r = List();
			r.$isdoms$ = true;
			for (var i = 0; i < json.length; i++) {
				var jitem = json[i];
				debugger;   //jbuilder(jitem, c, cs);
				var ri = jbuilder(jitem, cs, c);
				r.add(ri);
			}
			return r;
		}
		var setAttr = c.setAttr;
		if (!cs) {
			cs = { parent: null, group: null, root: null, curt: null };
		}

		// Create top level element
		if (!c.create) {
			debugger;
			return null;
		}
		//if (json.svg == 'ellipse') {
		//    debugger;
		//}
		var ced = c.create(key, json, cs);
		if (!ced) {
			return null;
		}
		// Set cursor information
		if (!ced.$root) {
			ced.$root = cs.root || cs.parent;
		}
		ced.$parent$ = cs.parent;
		if (!cs.root) {
			cs.root = ced;
			cs.group = ced;
		} else if (!cs.group) {
			cs.group = root;
			ced.$group$ = root;
		} else {
			ced.$group$ = cs.group;
		}

		// Set alias information
		if (json.alias) {
			ced.alias = json.alias;
			cs.group['$' + ced.alias] = ced;
		}
		if (ced.$isgroup$) {
			cs.group = ced;
		}
		cs.parent = ced;
		// Process child elements
		if (json.$) {
			var t = typeof (json.$);
			if (t == 'object') {
				if (json.$.length && json.$.length > 0) {
					// Process array element
					for (var i = 0; i < json.$.length; i++) {
						var jitem = json.$[i];
						var r = jbuilder(jitem, cs);
						//bindCusor(r, ced, cs);
						c.append(ced, r);
					}
				} else if (!json.$.length) {
					// Process single element
					var r = jbuilder(json.$, cs);
					//bindCusor(r, ced, cs);
					c.append(ced, r);
				}
			} else {
				// Process value
				c.val(ced, json.$);
			}
		}
		// Called when element created on internal json cfg
		if (c.created) {
			c.created(ced);
		}
		// Called when element built on json cfg
		if (json.built) {
			json.built.call(ced, json);
		}
		return ced;
	}
	function fbuilder(bc) {
		var b = {};
		var cs = { curt: null, parent: null, root: null, group: null };
		b.New = function (name, json, cursor) {
			if (!cursor) {
				cursor = cs;
			}
			if (bc) {
				var r = bc.create(name, json, cursor);
				cursor.curt = r;
				if (r) {
					// Save the cursor
					r.$root$ = cursor.root;
					r.$parent$ = cursor.parent;
					r.$group$ = cursor.group;

					// Update the cursor
					if (cursor.root == null) {
						cursor.root = r;
						cursor.group = r;
						cursor.parent = r;
					} else if (cursor.parent != null) {
						bc.append(cursor.parent, r);
					}

					// Regist alias
					if (json.alias) {
						if (cursor.group) {
							cursor.group['$' + json.alias] = r;
						} else if (cursor.root) {
							cursor.root['$' + json.alias] = r;
						} else {
							debugger; // No group to take alias;
						}
					}
					if (bc.created) {
						bc.created(ced);
					}
					if (r.built) {
						r.built(json);
					}
				}
			}
			return this;
		};
		b.child = function (name, json, cursor) {
			if (!cursor) {
				cursor = cs;
			}
			cursor.parent = cursor.curt;
			if (cursor.curt.$isgroup$) {
				cursor.group = cursor.curt;
			}
			return this.New(name, json, cursor);
		};
		b.group = function (name, json, cursor) {
			if (!cursor) {
				cursor = cs;
			}
			var el = cursor.group || cursor.root;
			cursor.parent = el.$parent$;
			cursor.group = el.$group$;
			cursor.root = el.$root$;
			cursor.curt = el;
			if (json) {
				return this.New(name, json, cursor);
			} else {
				return this;
			}
		}
		b.root = function (name, json, cursor) {
			if (!cursor) {
				cursor = cs;
			}
			cursor.parent = null;
			cursor.group = null;
			cursor.curt = cursor.root;
			if (json) {
				return this.New(name, json, cursor);
			} else {
				return this;
			}
		}
		b.go = function (alias) {
			if (!cursor) {
				cursor = cs;
			}
			if (cursor.curt && cursor.curt.$isgroup$ && cursor.curt['$' + alias]) {
				cursor.parent = cursor.curt['$' + alias];
			} else {
				debugger; // Invalid cursor position;
			}
			return this;
		}
		b.empty = function () {
			if (cs.curt) {
				var svg = cs.curt;
				while (svg.lastChild) {
					svg.removeChild(svg.lastChild);
				}
			}
			return this;
		}
		b.val = function (v) {
			if (cs.curt) {
				bc.val(cs.curt, v);
			}
			return this;
		}
		b.dom = function () {
			var r = cs.root;
			return r;
		}
		return b;
	}


	function invoke(evts, arg, ignoreNull) {
		if (!evts) {
			return true;
		}
		var t = typeof (evts);
		if (t == 'function') {
			return evts(arg);
		}
		else if (evts instanceof Array) {
			for (var i = 0; i < evts.length; i++) {
				var e = evts[i];
				if (e && typeof (e) == 'function') {
					if (!e(arg) && !ignoreNull) {
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}

	// o : object
	// c : external object
	// ec : element creator / processor (eg. objmerge)
	// eid : curt merge id
	// cfg : merge configuration 
	//  { 
	//      excludes: { xxx: true } // exclude the xxx field
	//  }
	function merge(o, c, ec, eid, cfg) {
		if (!c) {
			debugger;
		}
		if (!o || c.nodeName || c.tagName) {
			//debugger;
			return;
		}
		// Indicating obj already merged
		// Prevent infinite cycle merge
		if (!eid) {
			eid = joy.uid(mergeprop);
		}
		if (!c) {
			return o;
		}

		// Replace eid with the latest merge
		if (!c[mergeprop] || c[mergeprop] != eid) {
			c[mergeprop] = eid;
		} else {
			return;  // Cycle merge detected.
		}

		if (!(o.length > 0 && c instanceof Array)) {
			// Merge generic object
			for (var i in c) {
				try {
					if (cfg && cfg.excludes && cfg.excludes[i]) {
						continue;
					}
					var t = typeof (c[i]);
					if (t == 'object' && ec) {
						// Call objmerge
						ec(o, c, i, eid, cfg);
					} else {
						if (cfg && cfg.setAttr && typeof (c[i]) != 'function' && typeof (c[i]) != 'boolean' && typeof (c[i]) != 'object') {
							cfg.setAttr(o, i, c[i]);
						} else {
							o[i] = c[i];
						}
					}
				} catch (e) {
					console.log(i);
					console.log(e);
					debugger;
				}
			}
		} else {
			//Merge Array list
			for (var i = 0; i < c.length; i++) {
				try {
					if (i >= o.length) {
						break;
					}
					var t = typeof (c[i]);
					if (t == 'object' && ec) {
						// Call objmerge
						ec(o[i], c[i], null, eid, cfg);
					} else {
						alert('???');
						o[i] = c[i];
					}

				} catch (e) {
					debugger;
				}
			}
		}
	}
	// o : object
	// c : external object
	// i : curt property
	// eid : curt merge id
	// cfg : merge configuration 
	//  { 
	//      excludes: { xxx: true } // exclude the xxx field
	//  }
	function objmerge(o, c, i, eid, cfg) {
		if (c.tagName || c.nodeName) {
			return;
		}
		if (i) {
			if (!o[i]) {
				o[i] = {};
			}
			merge(o[i], c[i], objmerge, eid, cfg);
		} else {
			merge(o, c, objmerge, eid, cfg);
		}
	}
	function list() {
		var r = new Array();
		r.add = function (o) {
			this[this.length] = o;
		};
		r.clear = function () {
			for (var i = this.length - 1; i >= 0; i--) {
				var it = this[i];
				if (it.dispose) {
					it.dispose();
				}
				delete this.pop();
			}
		};
		return r;
	}

	var joyrlt = {
		uid: function (prefix) {
			var d = new Date();
			if (!prefix) {
				prefix = '';
			}
			var s = prefix + '_' + d.getUTCFullYear() + '_' + d.getUTCMonth() + '_' + d.getUTCDate() + '-' + d.getUTCHours() + '_' + d.getUTCMinutes() + '_' + d.getUTCSeconds() + '_' + d.getUTCMilliseconds();
			curtid++;
			return s + '_' + curtid;
		},
		removeprop: function (o, p) {
			try {
				if (o && p) {
					if (o[p]) {
						o[p] = null;
						delete o[p];
					}
					return true;
				}
			} catch (e) {
				debugger;
			}
			return false;
		}
        , backup: function (o, ps) {
        	var r = {};
        	for (var i in o) {
        		if (ps[i]) {
        			r[i] = o[i];
        		}
        	}
        	return r;
        }, restore: function (o, r, ex) {
        	for (var i in r) {
        		if (ex && ex[i]) {
        			continue;
        		}
        		o[i] = r[i];
        	}
        	return o;
        }
        , fromJson: function (s) {
        	return eval('(' + s + ')');
        }
        , toJson: function (o) {
        	return JSON.stringify(o);
        }
        , invoke: invoke
        , extend: function (o, c, settings) {
        	merge(o, c, objmerge, null, settings);
        }
        , creators: {}
        , list: list
        , fbuilder: fbuilder
        , jbuilder: jbuilder
	};
	window.joy = joyrlt;
})();