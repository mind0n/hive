<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="PmSys.web.Module.PmSys.GroupManager.GroupList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/css/subModal.css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/subModal.js"></script>
    
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadTitleTxt="部门资料管理" HeadOPTxt="部门资料管理" HeadHelpTxt="请点选左栏的部门名称进行部门资料管理">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="部门资料">
        		  <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
						<tr class="table_title">
							<td colspan="2"><asp:Label ID="CatListTitle" runat="server"></asp:Label></td>
						</tr>
						<tr>
							<td class="table_body">
                                部门名称</td>
							<td class="table_none">
								<asp:Label id="CatNameTxt" runat="server"></asp:Label></td>
						</tr>
                      <tr>
                          <td class="table_body">
                              子部门数</td>
                          <td class="table_none">
								<asp:Label id="CatCountTxt" runat="server"></asp:Label></td>
                      </tr>
						
						<tr>
							<td colspan="2" class="table_title">子部门</td>
						</tr>
						<tr class="table_body">
							<td>
                                部门名称</td>
							<td>子部门数</td>
						</tr>
						<asp:repeater id="SubGroup" Runat="server" EnableViewState="False">
							<ItemTemplate>
								<tr class="table_none">
									<td width="50%"><%# Eval("G_CName") %></td>
									<td width="50%"><%# Eval("G_ChildCount") %></td>
								</tr>
							</ItemTemplate>
						</asp:repeater>
						
					</table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
    <script language="javascript" type="text/javascript">
    function AlertMessageBox(file_name)
    {
       	        if (file_name!=undefined)
                window.location.href = file_name;
    }
        function sMove(a)
        {
            //var file_name=window.showModalDialog("Move.aspx?GroupID="+a,'', "dialogHeight=215px;dialogWidth=255px;help=no")
	        //if (file_name!=undefined){
	        //    window.location.href = file_name
	        //}
	        showPopWin("移动部门","Move.aspx?GroupID="+a,215, 255, AlertMessageBox,true,true)
        }
    </script>
</asp:Content>
