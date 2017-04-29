<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestJson.aspx.cs" Inherits="Portal.Testing.TestXml" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Json</title>
    <script type="text/javascript" src="/joy/joy.js"></script>
    <script type="text/javascript">
        var tmp = {name: 'marquis', age: 27, son: {name: 'small marquis', age:7},children: [{ name: 'chi"ld1', age: 7 },{ name: 'child2', age: 8 },{ name: 'child3', age: 9 }]};
        joy(function () {
        	joy.debug.acitvate();

        	var box = joy('box');
        	if (box.value.trim().length < 1) {
        		var s = joy.toJson(tmp);
        		joy('box').value = s;
        	} else {
        		var o = joy.fromJson(box.value);
        		alert(o);
        	}
        });
    </script>
</head>
<body>
    <form id="mainform" runat="server">
    <div>
		<input type="submit" value="send" />
       <textarea id="box" name="box" style="width:800px;height:400px;" runat="server"></textarea>
    </div>
    </form>
</body>
</html>
