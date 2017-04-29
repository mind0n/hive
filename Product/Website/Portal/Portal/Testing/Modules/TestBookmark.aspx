<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TestBookmark.aspx.cs" Inherits="Portal.Testing.Modules.TestBookmark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	<a target="ifm" href="/bookmark/Add/http://stackoverflow.com/questions/3456994/how-to-use-net-reflection-to-determine-method-return-type-including-void-and" target="ifm">Add Bookmark</a><br />
    	<a target="ifm" href="/bookmark/View" target="ifm">View Bookmarks</a><br />
    </div>
	<div style="display:block; position:fixed;left:150px;top:0px;right:0px;bottom:0px;">
		<iframe id="ifm" name="ifm" src="about:blank" style="width:100%;height:100%;">INS</iframe>
	</div>
    </form>
</body>
</html>
