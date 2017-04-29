<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.Page" %>

<%@ Register assembly="Fs" namespace="Fs.Web.Client" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
  <head>
<cc1:HeaderControl ID="HeaderControl1" runat="server"></cc1:HeaderControl>
  <script>
  	function init() {
  	}
  	function testCloner() {
  		var t = document.getElementById("tst");
  		t.onmousedown = function() {
  			alert(this.innerHTML);
  		}
  		var tt = FC.CloneNode(t);
  		tt.innerHTML = "duplicate";
  		var list = [{ a: 3, b: 4 }, { a: 5, b: 6 }, { a: 7, b: 8}];
  		var ttt = FC.Clone({ a: 1, b: 2, c: list });
  		list[2].a = "changed";
  		//alert(ttt.c[2].a + "," + list[2].a);
  		document.body.appendChild(tt);
  	}
  	function testDict() {
  		var d = FC.Dict();
  		d.add("key", "val");
  		d.add("key", "val2");
  		d.add("key", "val3");
  		d.add("key2", "val");
  		d.add("key3", "val");
  		alert(d.toSpecifiedString('=', '"', '"', '&', ';'));
  	}
  	function testOpt() {
  		//testCloner();
  		//testDict();
  		var a = 0;
  		for (var i = 1; i <= 32; i++) {
  			a <<= 1;
  			a |= 1;
  			document.write(i + 'a: ' + a + '<br>');
  		}
  		var b = 1;
  		for (var i = 1; i <= 32; i++) {
  			b <<= 1;
  			//b |= 1;
  			document.write(i + 'b: ' + b + '<br>');
  		}

  		alert(a | b);
  	}
  	function testCloner() {
  		var tag = document.getElementById("testCloner");
  		var lst = tag.getElementsByTagName("div");
  		for (var i = 0; i < lst.length; i++) {
  			var item = lst[i];
  			item.onmousedown = function() {
  				alert(this.innerHTML);
  			}
  		}
  		var dup = FC.CloneNode(tag);
  		document.body.appendChild(dup);
  		alert(FC$.BaseUrl);
  	}
  	function testRemoteCall() {
  		FC.QueuedRemoteCall.add({
  			CallBack: function(o, success) {
  				if (!success) {
  					alert(o.error);
  				} else {
  					alert(o.content);
  				}
  			}
			, method: "TestSqlPaging"
			, param: {
				page: 1
				, size: 3
			}
			, target: "/DataProvider/DataViewerHandler.aspx"
  		});
  		FC.QueuedRemoteCall.send();
  	}
  </script>
  </head>
  <body onload = "init()">
      <form id="form1" runat="server">
    <div id="tst" style="border:solid 1px green; background:white;" onclick="alert ('ok');">
    Click
    </div>
    <div id="testCloner" style="border:solid 1px green; background:white;">
		Information
		<div id="Div1" style="border:solid 2px blue; background:silver;">
		1	    
		</div>    
		<div id="Div2" style="border:solid 2px blue; background:silver;">
	    2
		</div>    
		<div id="Div3" style="border:solid 2px blue; background:silver;">
<div id="Div6" style="border:solid 1px green; background:white;">
		Information
	<div id="Div7" style="border:solid 2px blue; background:silver;">
	31	    
	</div>    
	<div id="Div8" style="border:solid 2px blue; background:silver;">
	32
	</div>    
	<div id="Div9" style="border:solid 2px blue; background:silver;">
	33
	</div>    
	<div id="Div10" style="border:solid 2px blue; background:silver;">
	34
	</div>    
	<div id="Div11" style="border:solid 2px blue; background:silver;">
	35
	</div>    
</div>
		Information
		</div>    
		<div id="Div4" style="border:solid 2px blue; background:silver;">
	    4
		</div>    
		<div id="Div5" style="border:solid 2px blue; background:silver;">
	    5
		</div>    
    </div>
  	</form>
  </body>
</html>