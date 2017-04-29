<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestHtml.aspx.cs" Inherits="JsLib.TestHtml" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <select id="sel"></select>
	<script>
		var s = document.getElementById("sel");
		var d = [{text:'a',value:'1'},{text:'b', value:'2'},{text:'c',value:'3'}];
		for (var i=0; i<d.length; i++){
			var o = document.createElement("options");
			o.value = d[i].value;
			o.innerHTML = d[i].text;
			s.appendChild(o);
		}
	</script>
    </div>
    </form>
</body>
</html>
