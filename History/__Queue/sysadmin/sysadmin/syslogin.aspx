<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_syslogin, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/index.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.alerts.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.ui.draggable.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <table class="login" cellpadding="0" cellspacing="0">
            <tr class="top">
                <td class="l"></td>
                <td class="m"><img src="images/i_top1.gif" alt="" /></td>
                <td class="r"></td>
            </tr>
            <tr class="middle">
                <td class="l"></td>
                <td class="m" valign="middle">
                    <table class="logbox" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="ll"><img src="images/logo.jpg" alt="" /></td>
                            <td class="lr">
                                <table class="txtbox" cellpadding="0" cellspacing="0">
                                    <tr class="top">
                                        <td colspan="2"><img src="images/adminsyteam.gif" alt="自由宿主-网站后台管理系统" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr style="height:25px;">
                                                    <td style="width:44px;"><img src="images/id.gif" alt="" /></td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtuid" runat="server" CssClass="input_txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:25px;">
                                                    <td><img src="images/pass.gif" alt="" /></td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtpwd" runat="server" CssClass="input_txt" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="bottom">
                                        <td>
                                            <asp:ImageButton ID="ibtnlogin" runat="server" ImageUrl="~/sysadmin/images/b_login.gif" OnClick="ibtnlogin_Click" OnClientClick="return test()" />
                                            <script type="text/javascript">
                                                function test()
                                                {
                                                    if($.trim($("#txtuid").val()) == "")
                                                    {
                                                        jAlert("warning", "请填写用户名", "系统提示");
                                                        return false;
                                                    }
                                                    if($("#txtpwd").val() == "")
                                                    {
                                                        jAlert("warning", "请填写密码", "系统提示");
                                                        return false;
                                                    }
                                                    return true;
                                                }
                                            </script>
                                        </td>
                                        <td>
                                            <img style="cursor:pointer; _cursor:hand;" onclick="document.form1.reset()" src="images/b_clean.gif" alt="" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="r"></td>
            </tr>
            <tr class="bottom">
                <td class="l"></td>
                <td class="m"></td>
                <td class="r"></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>