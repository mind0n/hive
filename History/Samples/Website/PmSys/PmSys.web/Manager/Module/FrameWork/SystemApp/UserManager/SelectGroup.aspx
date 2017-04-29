<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectGroup.aspx.cs" Inherits="PmSys.web.Module.PmSys.UserManager.SelectGroup" %>


<html >
<head id="Head1">
    <title>选择部门</title>
    <script language="javascript">
 var TreeImgdir = "<%=Page.ResolveClientUrl("~/")%>Manager/images/TreeIcon/";
</script>
<script src="<%=Page.ResolveClientUrl("~/")%>Manager/js/SelectTree.js"></script>
<base target="_self"></base>
<link rel="stylesheet" href="<%=Page.ResolveClientUrl("~/")%>Manager/css/site_css.css" type="text/css">
<style> .FolderStyle { COLOR: #000000; TEXT-DECORATION: none } </style>
</head>
<link rel="stylesheet" href='<%=Page.ResolveClientUrl("~/")%>Manager/css/table/<%=TableSink%>/Css.css' type="text/css">
<body>
    <form id="form1" runat="server">
<table width='100%' cellpadding="0" cellspacing="0" border="0" bgcolor="#6b82a5">
	<tr>
		<td class="table_title">
			选择部门
		</td>
	</tr>
</table>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td width="100%" bgcolor="#ffffff" valign="top" style="PADDING-LEFT:5px">
			<asp:Literal ID="ShowScript" Runat="server" EnableViewState="False"></asp:Literal>
		</td>
	</tr>
</table>


<script language="javascript">
 function xNowMove(xName,xId)
 {
    if (xId!="")
    {
        if (confirm("选择部门【"+ xName +"】 ?\n\n确定吗？"))
        {
            window.returnVal = xName+"||"+xId;
            window.parent.hidePopWin(true);
            //window.close();
        }      
    }
 }
</script>
    </form>
</body>
</html>
