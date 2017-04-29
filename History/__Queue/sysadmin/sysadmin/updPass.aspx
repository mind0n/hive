<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_updPass, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="Stylesheet" type="text/css" href="css/sub.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.alerts.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.ui.draggable.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 修改密码
        </div>
        <div class="content">
            <%ShowInfo(); %>
            <fieldset>
                <legend style="width:550px;">密码修改</legend>
                <table style="width:500px; text-align:left;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height:10px; width:120px;"></td>
                        <td></td>
                        <td style="width:160px"></td>
                    </tr>
                    <tr>
                        <td align="right">旧 密 码：</td>
                        <td>
                            <asp:TextBox ID="txtold" runat="server" TextMode="Password" CssClass="input_txt" Width="210px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtold" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">新 密 码：</td>
                        <td>
                            <asp:TextBox ID="txtnew" runat="server" TextMode="Password" CssClass="input_txt" Width="210px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtnew" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">确认密码：</td>
                        <td>
                            <asp:TextBox ID="txtcmp" runat="server" TextMode="Password" CssClass="input_txt" Width="210px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtcmp" Display="Dynamic"></asp:RequiredFieldValidator><asp:CompareValidator
                                ID="CompareValidator1" runat="server" ControlToCompare="txtnew" ControlToValidate="txtcmp"
                                Display="Dynamic" ErrorMessage="新密码不一致"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnsubmit" runat="server" Text="提交" OnClick="btnsubmit_Click" CssClass="btn" />
                            <input id="Reset1" type="reset" value="重置" class="btn" />&nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="height:10px;"></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>