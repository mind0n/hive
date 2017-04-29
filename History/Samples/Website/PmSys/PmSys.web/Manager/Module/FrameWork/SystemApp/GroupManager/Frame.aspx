<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="PmSys.web.Module.PmSys.GroupManager.Frame" %>
<html>
<head>
<title>
中华人民共和国
</title>
    <style type="text/css">
.navPoint
	{
	font-family: Webdings;
	font-size:9pt;
	color:white;
	cursor:pointer;
	}
p{
	font-size:9pt;
}

.font_size {  font-family: "Verdana", "Arial", "Helvetica", "sans-serif"; font-size: 12px; font-weight: normal; font-variant: normal; text-transform: none}
</style>
</head>

<body scroll="no" leftMargin=0 topMargin=0 marginheight="0" marginwidth="0" onunload="javascript:return window_onunload();" onload="javascript:return window_onload();">
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%" align="center">
<tr>
    <td id="frmTitle" name="frmTitle" nowrap valign="middle" align="center" height="100%"  style="border-right: 1px solid #000000"> 
	<Iframe  name="leftbody" 	style="HEIGHT: 100%; VISIBILITY: inherit; WIDTH: 198; Z-INDEX: 2" 	scrolling=auto frameborder=0 src="CatIndex.aspx" ></Iframe>
</td>
    <td style="width:10pt" bgcolor="#7898A8" width="10"  title="关闭/打开左栏" class="navPoint"> 

	  
	<table border="0" cellpadding="0" cellspacing="0" width="11" height="100%" align="right">
		<tr>
			<td valign="middle" align="right" style="background-color:#FFFFFF;border-right:1px solid #C7C5D9;"><img border="0" src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/close.gif")%>" id="menuimg" alt="隐藏左栏" onmouseover="javascript: menuonmouseover();" onmouseout="javascript: menuonmouseout();"  onclick="javascript:switchSysBar()" style="cursor: hand" width="11" height="76"></td>
		</tr>
	</table>

			  
    </td>
    <td style="width:100%">
	<Iframe src="GroupList.aspx" name="mainbody" style="HEIGHT: 100%; VISIBILITY: inherit; WIDTH: 100%; Z-INDEX: 1" scrolling="auto" frameborder="0"></Iframe>
</td>
  </tr>
</table>
</body>
</html>
<script language="JavaScript" type="text/javascript">
var var_frmTitle = document.getElementById("frmTitle");
var var_menuimg  = document.getElementById("menuimg");
function switchSysBar(){

 	if (var_frmTitle.style.display=="none") {
		var_frmTitle.style.display=""
		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/open.gif")%>";
		var_menuimg.alt="隐藏左栏"
		}
	else {
		var_frmTitle.style.display="none"
		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/close.gif")%>";
		var_menuimg.alt="开启左栏"
	 }
	 
	 

}

 function menuonmouseover(){
 		if (var_frmTitle.style.display=="none") {
 		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/open_on.gif")%>";
 		}
 		else{
 		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/close_on.gif")%>";
 		}
 }
 function menuonmouseout(){
 		if (var_frmTitle.style.display=="none") {
 		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/open.gif")%>";
 		}
 		else{
 		var_menuimg.src="<%=Page.ResolveClientUrl("~/Manager/images/Menu/close.gif")%>";
 		}
 }



var parent_frmTitle = parent.document.getElementById("frmTitle");
var parent_menuimg = parent.document.getElementById("menuimg");
function window_onunload() {

if (parent_frmTitle!=null)
{
		parent_frmTitle.style.display=""
		parent_menuimg.src="<%=Page.ResolveUrl("~/Manager/images/Menu/close.gif")%>";
		parent_menuimg.alt="隐藏左栏"
}
}

function window_onload() {

if (parent_frmTitle!=null)
{
		parent_frmTitle.style.display="none"
		parent_menuimg.src="<%=Page.ResolveUrl("~/Manager/images/Menu/open.gif")%>";
		parent_menuimg.alt="开启左栏"
		}
}
</script>