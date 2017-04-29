<%@ Page Title="" Language="C#" MasterPageFile="~/indexHead.master" AutoEventWireup="true" CodeFile="loginIn.aspx.cs" Inherits="loginIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">

    <p>
        <asp:Label ID="userName" runat="server" Text="用户名："></asp:Label>
        <asp:TextBox ID="userNameTextBox" runat="server" MaxLength="20"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="请输入用户名！" ControlToValidate="userNameTextBox"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="userPWD" runat="server" Text="密码："></asp:Label>
        <asp:TextBox ID="userPWDTextBox" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="请输入密码！" ControlToValidate="userPWDTextBox"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="validateCode" runat="server" Text="请输入验证码："></asp:Label>
        <asp:TextBox ID="checkCodeTextBox" runat="server"></asp:TextBox>
        <img src="checkcode.aspx" alt="图像无法显示，请刷新重试" />
    </p>
<p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="登录" />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>    
</asp:Content>


