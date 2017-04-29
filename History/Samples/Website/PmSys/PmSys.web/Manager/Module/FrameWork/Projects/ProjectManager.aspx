<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="ProjectManager.aspx.cs" Inherits="PmSys.Web.Manager.Module.FrameWork.Projects.ProjectManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadTitleTxt="项目模块管理" HeadOPTxt="查看项目">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="应用" ButtonPopedom="List" ButtonUrl="ProjectList.aspx"
            ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="查看项目">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr id="TopTr" runat="server">
                    <td class="table_body">
                        项目NO
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目名称
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectName" runat="server"></asp:Label>
                        <asp:TextBox ID="textBoxProjectName" runat="server" Columns="50" title="请输入项目名称~50:!"
                            CssClass="text_input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目状态
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectStatus" runat="server"></asp:Label>
                        <asp:DropDownList ID="DropDownListProjectStatus" runat="server">
                            <asp:ListItem Text="挂起" Value="0"></asp:ListItem>
                            <asp:ListItem Text="进行" Value="1"></asp:ListItem>
                            <asp:ListItem Text="结束" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        开始时间
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectStartTime" runat="server"></asp:Label>
                        <asp:TextBox ID="textBoxProjectStartTime" runat="server" Columns="50" CssClass="text_input" onfocus="javascript:HS_setDate(this);"
                            title="请输入开始时间~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目经历时间
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectDuration" runat="server"></asp:Label>
                        <asp:TextBox ID="textBoxProjectDuration" runat="server" Columns="10" CssClass="text_input"
                            title="项目经历时间~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目简介
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectBrief" runat="server"></asp:Label>
                        <asp:TextBox ID="textProjectBrief" runat="server" Columns="50" CssClass="text_input"
                            title="请输入项目简介~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目备注
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectComments" runat="server"></asp:Label>
                        <asp:TextBox ID="textProjectComments" runat="server" Columns="50" CssClass="text_input"
                            title="请输入项目备注~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        项目图标地址
                    </td>
                    <td class="table_none">
                        <asp:Label ID="labelProjectIcon" runat="server" ></asp:Label>
                        <asp:TextBox ID="textProjectIcon" runat="server" Columns="50" CssClass="text_input" title="请输入项目备注~50:"></asp:TextBox>
                    </td>
                </tr>
                <tr id="SubmitTr" runat="server">
                    <td colspan="2" align="right">
                        <asp:Button ID="ButtonOK" runat="server" CssClass="button_bak" Text="确定" 
                            onclick="ButtonOK_Click" />
                        <input id="Reset1" class="button_bak" type="reset" value="重填" />&nbsp;
                    </td>
                </tr>
            </table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
