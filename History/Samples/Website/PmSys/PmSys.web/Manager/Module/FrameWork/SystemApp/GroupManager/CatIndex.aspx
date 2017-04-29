<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatIndex.aspx.cs" Inherits="PmSys.web.Module.PmSys.GroupManager.CatIndex" %>

<html >
<head id="Head1">
    <title>无标题页</title>
    <base target="_self"></base>
    <style> .FolderStyle { COLOR: #000000; TEXT-DECORATION: none } </style>
<link rel="stylesheet" href="<%=Page.ResolveClientUrl("~/")%>Manager/css/site_css.css" type="text/css">
</head>
<link rel="stylesheet" href='<%=Page.ResolveClientUrl("~/")%>Manager/css/table/<%=TableSink%>/Css.css' type="text/css">

<body>
    <form id="form1" runat="server">
    <table width='100%' cellpadding="0" cellspacing="0" border="0">
	<tr >
		<td class="table_title">
			<img src="<%=Page.ResolveClientUrl("~/")%>Manager/images/TreeIcon/refurbish.gif" border="0" align="absMiddle" onclick="javascript:window.location.reload();" title="刷新" style="cursor:pointer">&nbsp;<a href="javascript:disp_all();">展开/收起</a>
		</td>
	</tr>
</table>
<script language="javascript">
var TreeImgdir = "<%=Page.ResolveClientUrl("~/")%>Manager/images/TreeIcon/";
</script>
<script src="<%=Page.ResolveClientUrl("~/")%>Manager/js/tree.js"></script>


<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td width="100%" bgcolor="#ffffff" valign="top" style="PADDING-LEFT:5px">
			<asp:Literal ID="ShowScript" Runat="server" EnableViewState="False"></asp:Literal>
		</td>
	</tr>
</table>

<script language="javascript">
var close_id=1
function disp_all()
{
var d_i;
if (close_id==1)
{
close_id=0
dim_src=Fold_id.split(",");
for (d_i = 1; d_i < dim_src.length; d_i=d_i+2)
   {
   if (dim_src[d_i] !='')
     {
      clickOnNode(dim_src[d_i]);
	 }
   }
}
else
{
clickOnNode(0); 
close_id=1 
} 
clickOnNode(0); 
} 
disp_all();
</script>
    </form>
</body>
</html>
