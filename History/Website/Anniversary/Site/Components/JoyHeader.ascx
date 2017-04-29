<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoyHeader.ascx.cs" Inherits="Site.Components.JoyHeader" %>

<%@ Register src="JoyCssDefault.ascx" tagname="JoyCssDefault" tagprefix="uc1" %>

<meta http-equiv="x-ua-compatible" content="ie=7" />
<style type="text/css">
@import url("<%=RootUrl %>Themes/Site.css");
@import url("<%=RootUrl %>Themes/Default/Css/base.css");
@import url("<%=RootUrl %>Themes/Default/Css/topmenu.css");
@import url("<%=RootUrl %>Joy/Theme/Default/flink.css");
@import url("<%=RootUrl %>Themes/Default/Css/rightpanel.css");
</style>
<uc1:JoyCssDefault ID="JoyCssDefault1" runat="server" />
<script src="<%=RootUrl %>Joy/Joy.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Network/Request.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/SimpleMenu.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/DropDown.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/EditableRect.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/RowEditor.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/AdSwitcher.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/Grid.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/Menu.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/ImgList.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/VideoPlayer.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/Coverer.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/LabelControl.js" type="text/javascript"></script>
<script src="<%=RootUrl %>Joy/Controls/FormEditor.js" type="text/javascript"></script>
