<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="EventDisp.aspx.cs" Inherits="PmSys.web.Module.PmSys.EventManager.EventDisp"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        ButtonList-Capacity="4" HeadOPTxt="查看事件日志" HeadTitleTxt="事件日志管理">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrl="default.aspx" ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="查看事件日志">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		<tr id="TopTr" runat="server">
			<td class="table_body">
                ID</td>
			<td class="table_none">
                <asp:Label ID="EventID_Txt" runat="server"></asp:Label></td>
		</tr>	
            <tr runat="server">
                <td class="table_body">
                    事件类型</td>
                <td class="table_none">
                <asp:Label id="E_Type_Txt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    用户名</td>
                <td class="table_none">
                    <asp:Label ID="E_U_LoginName_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    应用名称</td>
                <td class="table_none">
                    <asp:Label ID="E_A_AppName_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body" style="height: 28px">
                    模块名称</td>
                <td class="table_none" style="height: 28px">
                    <asp:Label ID="E_M_Name_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    IP地址</td>
                <td class="table_none">
                    <asp:Label ID="E_IP_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    来源网址</td>
                <td class="table_none">
                    <asp:Label ID="E_From_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    详细描述</td>
                <td class="table_none">
                    <asp:Label ID="E_Record_Txt" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    操作时间</td>
                <td class="table_none">
                    <asp:Label ID="E_DateTime_Txt" runat="server"></asp:Label></td>
            </tr>
		</table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
