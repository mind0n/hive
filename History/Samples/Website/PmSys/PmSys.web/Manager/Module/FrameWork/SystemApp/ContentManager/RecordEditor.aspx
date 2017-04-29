<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="RecordEditor.aspx.cs" Inherits="PmSys.Web.Manager.Module.FrameWork.SystemApp.ContentManager.RecordEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="header" runat="server"
        HeadTitleTxt="应用列表管理" HeadOPTxt="查看应用">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="应用" ButtonPopedom="List" ButtonUrl="List.aspx"
            ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="tabs" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="tabContent" runat="server" Tab_Name="查看应用">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		<tr id="TopTr" runat="server">
			<td class="table_body">
                应用ID</td>
			<td class="table_none">
                <asp:Label ID="ApplicationID_Txt" runat="server"></asp:Label></td>
		</tr>		
		<tr>
			<td class="table_body">
                应用名称</td>
			<td class="table_none">
                <asp:Label ID="A_AppName_Txt" runat="server"></asp:Label>
                <asp:TextBox ID="A_AppName" runat="server" Columns="50" title="请输入应用名称~50:!" CssClass="text_input"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_body">
                应用介绍</td>
			<td class="table_none">
                <asp:Label ID="A_AppDescription_Txt" runat="server"></asp:Label>
                <asp:TextBox ID="A_AppDescription" runat="server" Columns="50" title="请输入应用介绍~200:" CssClass="text_input"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_body">
                应用网址</td>
			<td class="table_none">
                <asp:Label ID="A_AppUrl_Txt" runat="server"></asp:Label>
                <asp:TextBox ID="A_AppUrl" runat="server" Columns="50" CssClass="text_input" title="请输入应用网址~50:"></asp:TextBox></td>
		</tr>						
		<tr id="SubmitTr" runat="server"><td colspan="2" align="right">
            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" />
            <input id="Reset1" class="button_bak" type="reset" value="重填" />&nbsp;
		
		</td></tr>
	   </table>
            </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
