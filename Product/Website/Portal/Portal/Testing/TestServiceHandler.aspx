<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestServiceHandler.aspx.cs" Inherits="Portal.Testing.TestServicePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/joy/joy.js"></script>
    <script type="text/javascript" src="/joy/network/request.js"></script>
    <script type="text/javascript">
    	//joy(function () {
    	//	joy.debug.acitvate();
    	//});
    	function TestJsonRequestCallback(r, s) {
		    joy.debug.log(s);
	    }
    	function testJsRequest() {
    		var r = joy.jsonRequest();
    		r.addMethod('TestJsonRequest');
    		r.addParam('TestJsonRequest', { Name: 'arg', Value: 'This is argument' });
    		r.send('/test/');
    	}

    	joy(function() {

    	});
    </script>
</head>
<body>
    <form id="form1" runat="server">
                   
    <div>
    	<a href="/test/Calc/100/150" target="ifm">Test Routing</a><br />
    	<a href="/test/Calc/150" target="ifm">Test Routing Fail 1</a><br />
    	<a href="/test/xxx/100/150" target="ifm">Test Routing Fail 2</a><br />
    	<a href="/test/Test" target="ifm">Test No Arguments</a><br />
    	<a href="/test/" target="ifm">Test empty action</a><br />
    	<a href="/rest/xxx" target="ifm">Test Restful GET</a><br />
    	<a href="/login" target="ifm">Test Login</a><br />
        <br />
    	<input type="button" value="Send" onclick="javascript:testJsRequest()"/><br />
    	<a href="/Page/xxx" target="ifm">Test Page</a><br />
    </div>

	<div style="display:block; position:fixed;left:300px;top:0px;right:0px;bottom:0px;">

	<iframe id="ifm" name="ifm" src="about:blank" style="width:100%;height:100%;">INS</iframe>
	</div>

    </form>
</body>
</html>
