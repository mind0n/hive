<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBookmark.aspx.cs" Inherits="Portal.Views.Bookmarks.ViewBookmark" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<uc1:Include runat="server" id="Include" />
	<style type="text/css">
		span {
			font: 12px arial;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	bookmarks&nbsp;<span>bookmarks</span>
    </div>

    </form>
</body>
</html>

