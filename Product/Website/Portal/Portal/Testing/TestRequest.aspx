<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRequest.aspx.cs" Inherits="Portal.Testing.TestRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Joy Request</title>
    <script type="text/javascript" src="/joy/joy.js"></script>
    <script type="text/javascript" src="/joy/network/request.js"></script>
    <script type="text/javascript">
    	function testaCallback(r, s) {
    		joy.debug.log(r);
    	}
    	function testbCallback(r, s) {
    		//alert(r.IsNoException);
    		//joy.debug.log(r);
    	}
    	joy(function () {
    		joy.debug.acitvate();
    		var r = joy.jsonRequest();
    		r.addMethod('testa');
    		r.addParam('testa', { Name: 'arg1\'"name', Value: 'val1' });
    		r.addParam('testa', { Name: 'arg2', Value: 'val2' });
    		r.addMethod('testb');
    		r.addParam('testb', { Name: 'arg1', Value: 'val1' });
    		r.addParam('testb', { Name: 'arg2', Value: 'val2' });
    		r.addParam('testb', { Name: 'arg3', Value: 'val3' });
    		r.send('/testing/testrequestservicepage.aspx');
    	});
    </script>
</head>
<body>
    <form id="mainform" runat="server">
    <div>
		
    </div>
    </form>
</body>
</html>
