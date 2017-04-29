J.Xml = function(content, callback) {
	var doc, tokens;
	var isXmlStr = false;
	if (content) {
		tokens = content.substr(0, 5);
		if (tokens) {
			tokens = tokens.toLowerCase();
			if (tokens.indexOf('<') == 0) {
				if (tokens != '<?xml') {
					content = J.xmlTitle(content);
				}
				isXmlStr = true;
			}
		}
	}
	function handleXML() {
		nodes = doc.documentElement;
		if (callback) {
			if (document.all) {
				if (doc.readyState == 4) {
					callback(nodes, doc);
				}
			} else {
				callback(nodes, doc);
			}
		}
	}
	if (document.implementation && document.implementation.createDocument) {
		doc = document.implementation.createDocument("", "", null);
		if (content) {
			doc.onload = handleXML;
		}
		if (isXmlStr) {
			var parser = new DOMParser();
			doc = parser.parseFromString(content, 'text/xml');
		}
	}
	else if (window.ActiveXObject) {
		doc = new ActiveXObject('Microsoft.XMLDOM');
		if (content) {
			doc.onreadystatechange = handleXML;
		}
		if (isXmlStr) {
			doc.loadXML(content);
		}
	}
	if (!isXmlStr && content) {
		var isAsync = (callback != null) ? true : false;
		doc.async = isAsync;
		doc.load(content);
	}
	var rlt;
	if (doc && doc.childNodes && doc.childNodes.length > 1) {
		rlt = doc.childNodes[1];
	} else if (doc && doc.childNodes && doc.childNodes.length == 1) {
		rlt = doc.childNodes[0];
	} else {
		rlt = doc;
	}
	J.XmlText = function(node) {
		if (node.text) {
			return node.text;
		} else {
			return node.textContent;
		}
	};
	J.XmlValue = function(node) {
		if (node.xml) {
			return node.xml;
		} else {
			return new XMLSerializer().serializeToString(node);
		}
	};
	return rlt;
};
J.xml = J.Xml;
J.Xml.xml2obj = function(xmlnode, parent) {
	if (xmlnode) {
		var node = xmlnode;
		var rlt = {};
		if (node.attributes) {
			for (var j = 0; j < node.attributes.length; j++) {
				rlt['$' + node.attributes[j].nodeName] = node.attributes[j].value;
			}
		}
		if (!rlt.$) {
			rlt.$ = {};
		}
		rlt.$.name = node.nodeName;
		for (var i = 0; i < node.childNodes.length; i++) {
			var nodeName = node.childNodes[i].nodeName;
			if (!rlt.$) {
				rlt.$ = {};
			}
			if (nodeName == '#text' || nodeName == '#cdata-section') {
				if (!rlt.$.values) {
					rlt.$.values = new Array();
				}
				var content = J.XmlText(node.childNodes[i]);
				if (nodeName == '#cdata-section') {
					rlt.$.values.cdata = true;
					//content = '<![CDATA[' + content + ']]>';
				}
				rlt.$.values.push(content);
			} else if (nodeName.indexOf('!') == 0) {
				//do nothing
			} else {
				if (!rlt[nodeName]) {
					rlt[nodeName] = new Array();
				}
				var child = J.Xml.xml2obj(node.childNodes[i], node);
				if (!child.$) {
					child.$ = {};
				}
				child.$.parent = rlt;
				rlt[nodeName].push(child);
			}
		}
		return rlt;
	}
	return null;
};
J.xml.obj2xmlstr = function(obj, objname, noheader) {
	var rlt = '', vals = null;
	var childs = new Array();

	rlt += '\n<' + objname + ' ';
	for (var i in obj) {
		var name = i;
		if (i.indexOf('$') == 0) {
			if (name == '$') {
				vals = obj[i].values;
			} else {
				name = i.substr(1, i.length - 1);
				rlt += ' ' + name + '="' + obj[i] + '" ';
			}
		} else {
			childs.push({ name: i, obj: obj[i] });
		}
	}
	if (vals || childs.length > 0) {
		rlt += '>';
		if (vals) {
			for (var i = 0; i < vals.length; i++) {
				var content = vals[i];
				if (vals.cdata) {
					content = '<![CDATA[' + content + ']]>';
				}
				rlt += content;
			}
		}
		if (childs.length > 0) {
			for (var i = 0; i < childs.length; i++) {
				var child = childs[i];
				for (var j = 0; j < child.obj.length; j++) {
					rlt += J.xml.obj2xmlstr(child.obj[j], child.name, true);
				}
			}
		}
		rlt += '</' + objname + '>\n';
	} else {
		rlt += ' />\n';
	}
	if (!noheader) {
		rlt = '<?xml version="1.0" encoding="utf-8" ?>\n' + rlt;
	}
	return rlt;
};
J.xmlObject = function(content, callback) {
	var rlt = content;
	rlt = new J.xml(content, callback);
	return J.xml.xml2obj(rlt);
};
J.xmlSafeString = function(obj, rootName) {
	var rlt = J.xml.obj2xmlstr(obj, rootName);
	return rlt.replace(/\</g, '&lt;');
};
J.xmlTitle = function(content) {
	var title = '<?xml version="1.0" encoding="utf-8" ?>\r\n';
	if (content) {
		content = title + content;
		return content;
	}
	return title;
};
