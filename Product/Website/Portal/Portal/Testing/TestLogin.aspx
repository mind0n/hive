<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestLogin.aspx.cs" Inherits="Portal.Testing.TestLogin" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #txt {
            width: 1200px;
            height: 400px;
        }
    </style>
    <script type="text/javascript" src="/Joy/Joy.js"></script>
    <script type="text/javascript" src="/Joy/Network/Request.js"></script>
    <script type="text/javascript" src="/Joy/Security/AES.js"></script>
    <script type="text/javascript" src="/Joy/Security/Auth.js"></script>
    <script type="text/javascript">
        joy(function () {
            joy.auth();
            joy('login').onclick = function() {
                var url = '/auth/PreAuth';
                joy.auth(url, 'admin', 'pwd');
            };
            joy('logout').onclick = function() {
                var url = '/auth/UnAuth';
                joy.request.send(url, '', function (s, c, t) {
                    alert(c);
                });
            };
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id='login' type="button" value="Login" />
        <input type="text" value="" id="box"/>
        <input id='logout' type="button" value="Logout" />
        <a href="/access">Access</a><br />
        <div><textarea id="txt"></textarea></div>
    </form>
</body>
</html>
