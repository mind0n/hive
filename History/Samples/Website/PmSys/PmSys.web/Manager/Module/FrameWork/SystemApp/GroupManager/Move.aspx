<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Move.aspx.cs" Inherits="PmSys.web.Module.PmSys.GroupManager.Move" %>



<html >
<head>
    <title>移动部门</title>
    <script language="javascript">
 var TreeImgdir = "<%=Page.ResolveClientUrl("~/")%>Manager/images/TreeIcon/";    
</script>
<script src="<%=Page.ResolveClientUrl("~/")%>Manager/js/MoveTree.js"></script>
<base target="_self"></base>
<style> .FolderStyle { COLOR: #000000; TEXT-DECORATION: none } </style>
<link rel="stylesheet" href="<%=Page.ResolveClientUrl("~/")%>Manager/css/site_css.css" type="text/css">
</head>
<link rel="stylesheet" href='<%=Page.ResolveClientUrl("~/")%>Manager/css/table/<%=TableSink%>/Css.css' type="text/css">
<body>
    <form id="form1" runat="server">
<table width='100%' cellpadding="0" cellspacing="0" border="0" bgcolor="#6b82a5">
	<tr>
		<td class="table_title">
			移动部门
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
    if (xId!=null)
    {
        if (confirm("将此部门移动到【"+ xName +"】部门下 ?\n\n确定吗？"))
        {
            //window.returnValue = "Move.aspx?CMD=Move&GroupID=<%=GroupID %>&ToGroupID="+xId;
            //window.close();
            window.returnVal = "Move.aspx?CMD=Move&GroupID=<%=GroupID %>&ToGroupID="+xId;
            window.parent.hidePopWin(true);
        }      
    }
 }
</script>
    </form>
</body>
</html>
