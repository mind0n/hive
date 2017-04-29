<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Joy.WebApp.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		#box, #xbox, #sbox
		{
			height: 200px;
			width: 807px;
		}
	</style>
	<script src="/Joy/Joy.js" type="text/javascript"></script>
	<script type="text/javascript">
		Joy.ready(function ()
		{
			var sbox = Joy('sbox');
			sbox.value = Joy.cookie('ccj');
		});
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table><tr><td style="vertical-align:top;">
		<a href="Tests/TestFileDb.aspx">Tests/TestFileDb.aspx</a>
		<br />
		<a href="Tests/TestEditableRect.htm">Tests/TestEditableRect.htm</a>	
		<br />
		<a href="Tests/TestDropDown.htm">Tests/TestDropDown.htm</a>
		<br />
		<a href="Tests/TestRowEditor.htm">Tests/TestLineEditor.htm</a>
		<br />
		<a href="Tests/TestXml.htm">Tests/TestXml.htm</a>
		<br />
		<a href="Tests/TestGrid.htm">Tests/TestGrid.htm</a>
		<br />
		<a href="Tests/TestSecurity.htm">Tests/TestSecurity.htm</a> 
		<br />
	</td><td>
	<textarea id="box" runat="server"></textarea>
	<textarea id="xbox" runat="server"></textarea>
	<textarea id="sbox"></textarea>
	</td></tr></table>
    </div>
    </form>
</body>
</html>
