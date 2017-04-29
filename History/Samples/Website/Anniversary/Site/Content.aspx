<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="Site.Content" %>

<%@ Register src="Components/JoyHeader.ascx" tagname="JoyHeader" tagprefix="uc1" %>

<%@ Register src="Components/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<%@ Register src="Components/TopMenu.ascx" tagname="TopMenu" tagprefix="uc3" %>

<%@ Register src="Components/RightPanel.ascx" tagname="RightPanel" tagprefix="uc4" %>

<%@ Register src="Components/ContentViewer.ascx" tagname="ContentViewer" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<uc1:JoyHeader ID="JoyHeader1" runat="server" />
	<style type="text/css">@import url("<%=RootUrl %>Themes/Default/Css/Content.css");</style>

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
		<img src="Themes/Default/Images/60.png" />
		</div>
		<div class="main">
			<div class="topmenuarea" runat="server">
				<uc3:TopMenu ID="TopMenu" runat="server" />
			</div>
			<div class="news">
				<div class="leftpanel" id="leftpanel">
					<div class="history" runat="server" id="history">
						<a href="Index.aspx">首页</a>&nbsp;-&nbsp;<a href="Content.aspx?cid=" runat="server" id="historyCategory" visible="false"></a>
					</div>
					<div runat="server" id="ContentViewerContainer">
						<uc5:ContentViewer ID="ContentViewer" runat="server" />
					</div>
					<div class="catefooter" runat="server" id="pager" visible="false">
						当前第<span runat="server" id="pagenum">1</span>页&nbsp;
						共<span runat="server" id="pagetotal">1</span>页
						<span runat="server" id="pages"></span>
					</div>
				</div>
				<div class="rightpanel">
					<uc4:RightPanel ID="RightPanelArea" runat="server" />
				</div>
			</div>
			<uc2:Footer ID="Footer" runat="server" />
		</div>
    </div>
    </form>

</body>
</html>
