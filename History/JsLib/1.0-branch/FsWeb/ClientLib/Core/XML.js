JLib_v1.Xml = function (content, callback) {
	var doc, tokens;
	var isXmlStr = false;

	if (content) {
		tokens = content.substr(0, 5);
		if (tokens) {
			tokens = tokens.toLowerCase();
			if (tokens.indexOf('<') == 0) {
				if (tokens != '<?xml') {
					content = JLib_v1.xmlTitle(content);
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
	JLib_v1.XmlText = function (node) {
		if (node.text) {
			return node.text;
		} else {
			return node.textContent;
		}
	};
	JLib_v1.XmlValue = function (node) {
		if (node.xml) {
			return node.xml;
		} else {
			return new XMLSerializer().serializeToString(node);
		}
	};
	return rlt;
};
JLib_v1.Xml.header = '<?xml version="1.0" encoding="utf-8" ?>\r\n';
JLib_v1.xml = JLib_v1.Xml;
JLib_v1.Xml.xml2obj = function (xmlnode, parent) {
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
				var content = JLib_v1.XmlText(node.childNodes[i]);
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
				var child = JLib_v1.Xml.xml2obj(node.childNodes[i], node);
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
JLib_v1.xml.obj2xmlstr = function (obj, objname, noheader) {
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
					rlt += JLib_v1.xml.obj2xmlstr(child.obj[j], child.name, true);
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
JLib_v1.xmlObject = function (content, callback) {
	var rlt = content;
	rlt = new JLib_v1.xml(content, callback);
	return JLib_v1.xml.xml2obj(rlt);
};
JLib_v1.xmlSafeString = function (content, rootName) {
	if (typeof (content) == 'object')
		var rlt = JLib_v1.xml.obj2xmlstr(content, rootName);
	else
		var rlt = content;
	return rlt.replace(/\</g, '&lt;');
};
JLib_v1.xmlTitle = function (content) {
	var title = '<?xml version="1.0" encoding="utf-8" ?>\r\n';
	if (content) {
		content = title + content;
		return content;
	}
	return title;
};
JLib_v1.Xml.convert2xml = function (obj, rootName, headerNeeded) {
	var converter = JLib_v1.Xml.convert2xml;
	converter.parseObject = function (node, nodeName) {
		var type = typeof (node);
		if (!node) {
			return '<' + nodeName + ' />';
		}
		if (type == 'string' || type == 'number') {
			return '<' + nodeName + '>' + node + '</' + nodeName + '>';
		} else if (type == 'object') {
			var attr = '', childxml = '', value = '', xml = '';
			if (node instanceof Array) {
				for (var i = 0; i < node.length; i++) {
					var ctype = node[i];
					childxml += converter.parseObject(node[i], nodeName);
				}
				return childxml;
			} else {
				for (var i in node) {
					var ctype = typeof (node[i]);
					if (i == '$') {
						value += node[i];
					} else if (ctype == 'string' || ctype == 'number') {
						attr += ' ' + i + '="' + node[i] + '"';
					} else {
						childxml += converter.parseObject(node[i], i);
					}
				}
				xml = '<' + nodeName;
				if (attr.length > 0) {
					xml += attr;
				}
				if (childxml.length > 0 || value.length > 0) {
					xml += '>' + value + childxml + '</' + nodeName + '>';
				} else {
					xml += ' />';
				}
				return xml;
			}
		}
	};
	if (headerNeeded) {
		return JLib_v1.Xml.header + converter.parseObject(obj, rootName);
	} else {
		return converter.parseObject(obj, rootName);
	}
};
JLib_v1.Xml.convert2object = function (xmlnode, parent) {
	var type = typeof (xmlnode);
	if (type == 'string') {
		xmlnode = JLib_v1.Xml(xmlnode);
	}
	if (xmlnode) {
		var node = xmlnode;
		var rlt = {};
		if (node.attributes) {
			for (var j = 0; j < node.attributes.length; j++) {
				rlt[node.attributes[j].nodeName] = node.attributes[j].value;
			}
		}
		if (!rlt.$) {
			rlt.$ = null;
		}
		for (var i = 0; i < node.childNodes.length; i++) {
			var nodeName = node.childNodes[i].nodeName;
			if (nodeName == '#text' || nodeName == '#cdata-section') {
				var content = JLib_v1.XmlText(node.childNodes[i]);
				if (!rlt.$) {
					rlt.$ = content;
				} else {
					rlt.$ += content;
				}
			} else if (nodeName.indexOf('!') == 0) {
				//do nothing
			} else {
				var child = JLib_v1.Xml.convert2object(node.childNodes[i], node);
				//child.$$ = rlt;
				if (!rlt[nodeName]) {
					rlt[nodeName] = child;
				} else {
					if (rlt[nodeName] instanceof Array) {
						rlt[nodeName].push(child);
					} else {
						var temp = rlt[nodeName];
						rlt[nodeName] = [];
						rlt[nodeName].push(temp);
						rlt[nodeName].push(child);
					}
				}
			}
		}
		return rlt;
	}
	return null;

};