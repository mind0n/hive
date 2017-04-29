<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Site.Index" %>

<%@ Register src="Components/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<%@ Register src="Components/TopMenu.ascx" tagname="TopMenu" tagprefix="uc2" %>

<%@ Register src="Components/JoyHeader.ascx" tagname="JoyHeader" tagprefix="uc3" %>

<%@ Register src="Components/RightPanel.ascx" tagname="RightPanel" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<uc3:JoyHeader ID="JoyHeader" runat="server" />
	<style type="text/css">@import url("<%=RootUrl %>Themes/Default/Css/Index.css");</style>
	<script type="text/javascript">
		Joy.ready(function (completed) {
			if (completed) {
				Joy.makeWidgets(page.categridgroup);
			}
		});
	</script>
</head>
<body>
    <form id="formMain" runat="server">
    <div class="bg">
		<div class="sixty">
		<img alt="" src="Themes/Default/Images/60.png" />
		</div>
		<div class="main">
			<div class="topmenuarea" runat="server">
				<uc2:TopMenu ID="TopMenu" runat="server" />
			</div>
			<div class="topimg" id="topimg">
				
			</div>
			<div class="news">
				<div class="leftpanel" id="leftpanel"></div>
				<div class="rightpanel">
					<uc4:RightPanel ID="RightPanelArea" runat="server" />
				</div>
			</div>
			<uc1:Footer ID="Footer" runat="server" />
		</div>
    </div>
    </form>
</body>
</html>
