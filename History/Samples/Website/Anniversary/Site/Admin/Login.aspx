<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Site.Admin.Login" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
	<meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台登陆</title>
	<style type="text/css">@import url("<%=RootUrl %>Themes/Admin/admin.css");</style>
    <script type="text/javascript">
        function refresh() {
            document.getElementById("authImg").src = '../authcode.aspx?' + new Date();
        }
    </script>
</head>
<body class="login" id="bodyBg1">
    <form id="form1" runat="server">
    <div>

					<div class="loginform">
					<div class="homelink"><a href="../index.aspx">回到首页</a></div>
                    <table border="0" cellspacing="0">
                        <tr>
                            <td class="label">
                                用户名：
                            </td>
                            <td class="editor">
                                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
							</td><td>
                                <asp:RequiredFieldValidator ID="rfv_UserName" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="用户名不能为空" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                密 码：
                            </td>
                            <td class="editor">
                                <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
								</td><td>
                                <asp:RequiredFieldValidator ID="rfv_PassWord" runat="server" ControlToValidate="txtPassWord"
                                    ErrorMessage="密码不能为空" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                验证码：
                            </td>
                            <td class="editor short">
                                <asp:TextBox ID="txtChkcode" runat="server" MaxLength="4" />
                                &nbsp;
								<a href="javascript:void(0);" onclick="refresh()" title="看不清？单击此处刷新验证码">
								<img src="../authcode.aspx" alt="看不清？单击此处刷新验证码" id="authImg" border="0" />
								</a>
									</td><td>
                                <asp:RequiredFieldValidator ID="rfv_Chkcode" runat="server" ControlToValidate="txtChkcode"
                                    ErrorMessage="验证码不能为空" />
                            </td>
                        </tr>
						<tr><asp:Label ID="OKInfo" runat="server"></asp:Label></tr>
                        <tr>
							<td></td>
                            <td class="editor">
                                <asp:Button ID="btnLogin" class="btnlogin" runat="server" Text="登　录" OnClick="btnLogin_Click"></asp:Button>
                            </td>
							<td></td>
                        </tr>
                    </table></div>
    </div>
    </form>
</body>
</html>
