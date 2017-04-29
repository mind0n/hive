<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoyEntry.ascx.cs" Inherits="Site.Joy.JoyEntry" %>

<script src="<%=RootUrl %>Scripts/jquery.js" ></script>
<script src="<%=RootUrl %>Scripts/jform.js" ></script>
<script type="text/javascript">
	var RootUrl = '<%=RootUrl%>';
</script>
<script type="text/javascript" src="<%=RootUrl %>Joy/Joy.js"></script>
<script type="text/javascript">
	Joy.rootUrl = RootUrl;
</script>
<style type="text/css">
	@import url("<%=RootUrl %>Joy/Theme/reset.css");
	@import url("<%=RootUrl %>Joy/Theme/Default/Joy.css");
	@import url("<%=RootUrl %>Themes/Admin/admin.css");
</style>
<script type="text/javascript" src="<%=RootUrl %>Joy/Data/Viewer.js"></script>
<script type="text/javascript" src="<%=RootUrl %>Joy/Network/Request.js"></script>
<script type="text/javascript" src="<%=RootUrl %>Joy/Network/FileUploader.js"></script>
