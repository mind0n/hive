<%@ Page Title="" Language="C#" MasterPageFile="~/indexHead.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <p>
    用户注册页面</p>
<p>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="right" height="25" width="30%">
                UserID:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxUserID" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_LoginName:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxU_LoginName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_Password:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxU_Password" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_CName:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxU_CName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_EName:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxU_EName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_Type:</td>
            <td align="left" height="25">
                <asp:DropDownList ID="dropdownListU_Type" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_Status:</td>
            <td align="left" height="25">
                <asp:CheckBox ID="checkBoxU_Status" runat="server" Width="200px" />
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_Licence:</td>
            <td align="left" height="25">
                <asp:TextBox ID="textBoxU_Licence" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" height="25" width="30%">
                U_Sex:</td>
            <td align="left" height="25">
                <asp:RadioButton ID="radioButtonU_Sex" runat="server" Width="200px" />
            </td>
        </tr>
    </table>
</p>
</asp:Content>

