<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test3JS.aspx.cs" Inherits="Portal.Testing._3js.Test3JS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	<a href="/Testing/3js/a.aspx" target="ifm">Default</a><br />
    	<a href="/Testing/3js/b.aspx" target="ifm">J3d Cube</a><br />
    	<a href="/Testing/3js/c.aspx" target="ifm">Real Exp</a><br />
    	<a href="/Testing/3js/d.aspx" target="ifm">TestJ3d</a><br />
    	<a href="/Testing/3js/e.aspx" target="ifm">Interactive J3d</a><br />
	</div>

	<div style="display:block; position:fixed;left:100px;top:0px;right:0px;bottom:0px;">
	<iframe id="ifm" name="ifm" src="about:blank" style="width:100%;height:100%;">INS</iframe>
	</div>

    </form>
</body>
</html>
