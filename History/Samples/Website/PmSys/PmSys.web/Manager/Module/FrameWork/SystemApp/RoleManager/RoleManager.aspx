<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master"
    AutoEventWireup="true" Codebehind="RoleManager.aspx.cs" Inherits="PmSys.web.Module.PmSys.RoleManager.RoleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="角色资料" HeadTitleTxt="角色资料管理" ButtonList-Capacity="4">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonPopedom="List" ButtonUrl="RoleList.aspx"
            ButtonUrlType="Href" ButtonVisible="True" ButtonName="角色" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr id="TopTr" runat="server">
                    <td class="table_body">
                        角色ID</td>
                    <td class="table_none">
                        <asp:Label ID="RoleID_Txt" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="table_body">
                        角色名称</td>
                    <td class="table_none">
                        <asp:Label ID="R_RoleName_Txt" runat="server"></asp:Label>
                        <asp:TextBox ID="R_RoleName" runat="server" Columns="50" title="请输入角色名称~50:!" CssClass="text_input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="table_body">
                        角色介绍</td>
                    <td class="table_none">
                        <asp:Label ID="R_Description_Txt" runat="server"></asp:Label>
                        <asp:TextBox ID="R_Description" runat="server" Columns="50" title="请输入角色介绍~255:"
                            CssClass="text_input"></asp:TextBox></td>
                </tr>
                <tr id="tr_username" runat="server">
                    <td class="table_body" >
                        角色所属用户</td>
                    <td class="table_none">
                        <asp:Label ID="R_UserName" runat="server"></asp:Label></td>
                </tr>
                <tr id="SubmitTr" runat="server">
                    <td colspan="2" align="right">
                        <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                        <input id="Reset1" class="button_bak" type="reset" value="重填" />&nbsp;
                    </td>
                </tr>
            </table>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="设定角色应用权限">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr class="table_title">
                    <td style="height: 30px">
                        应用名称(点击应用名称进行角色应用权限修改)
                    </td>
                    <td style="height: 30px">
                    </td>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="GridView1_RowDataBound">
                    <ItemTemplate>
                        <tr class="table_body">
                            <td>
                                <a href="RolePermissionManager.aspx?RoleID=<%# Eval("A_RoleID") %>&ApplicationID=<%# Eval("A_ApplicationID") %>&CMD=Look">
                                    <asp:Literal ID="AppName" runat="server"></asp:Literal>
                                </a>
                            </td>
                            <td>
                                <a href="RoleManager.aspx?RoleID=<%# Eval("A_RoleID") %>&ApplicationID=<%# Eval("A_ApplicationID") %>&CMD=Move"
                                    onclick="return doConfirm(this.form);">移除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem3" runat="server" Tab_Name="增加角色应用">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr class="table_title">
                    <td colspan="2">
                        增加应用
                    </td>
                </tr>
                <tr>
                    <td class="table_body">
                        应用名称</td>
                    <td class="table_none">
                        <asp:DropDownList ID="NewAppID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="Button2" runat="server" Text="确定" CssClass="button_bak" OnClick="Button2_Click" /></td>
                </tr>
            </table>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem4" Tab_Name="用户列表" runat="server">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr class="table_title">
                    <td style="height: 30px">
                        用户名
                    </td>
                    <td style="height: 30px">
                         
                    </td>
                </tr>
                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <tr class="table_body">
                            <td>
                                <a href="../UserManager/UserManager.aspx?UserID=<%# Eval("R_UserID") %>&CMD=List">
                                    <%#PmSys.BusinessFacade.sys_UserDisp((int)Eval("R_UserID")).U_LoginName %>
                                </a>
                            </td>
                            <td>
                                <a href="RoleManager.aspx?RoleID=<%# Eval("R_RoleID") %>&UserID=<%# Eval("R_UserID") %>&CMD=MoveUser"
                                    onclick="return doConfirm(this.form);">移除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="table_title"><td colspan="2">增加用户</td></tr>
                <tr class="table_body"><td colspan="2"><asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="增加" OnClick="Button3_Click" /></td></tr>
            </table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
