<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_userLogin, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
            当前位置 >> 登录表设置
        </div>
        <div class="content">
            <div class="operdiv" style="color:Red;">
                &nbsp;1. 以下字段除了扩展组外都必须设置一个相应的字段与之对应，如果表字段不够可以重复设置同一个字段；<br />
                &nbsp;2. 登录密码如果为自定义，则文本框内填写格式为：文件名 类名 方法名，每个名称间用一个空格隔开<br />
                &nbsp;&nbsp;&nbsp;&nbsp;如：Password.dll AddPassword GetPassword<br />
                &nbsp;3. 自定义加密（或解密）函数只支持一个参数的函数方法，且返回值必须是string类型<br />
                &nbsp;4. 加密（或解密）函数调用失败后将不对密码进行任何处理，返回原值<br />
                &nbsp;5. 如果加密（或解密）函数在网站App_Code目录下，则文件名直接填写App_Code即可，否则文件必须要在bin目录下
            </div>
            <fieldset style="width:600px;">
                <legend>设置登录</legend>
                <table style="width:500px; text-align:left;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height:10px; width:120px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right">选择登录表：</td>
                        <td>
                            <asp:DropDownList ID="dlstTable" runat="server" OnSelectedIndexChanged="dlstTable_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <span class="light">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">登录ID：</td>
                        <td>
                            <asp:DropDownList ID="dlstLoginField" runat="server">
                            </asp:DropDownList>
                            <span class="light">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">显示名称：</td>
                        <td>
                            <asp:DropDownList ID="dlstNameField" runat="server">
                            </asp:DropDownList>
                            <span class="light">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">扩展组：</td>
                        <td>
                            <asp:DropDownList ID="dlstGroup" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">登录密码：</td>
                        <td>
                            <asp:DropDownList ID="dlstPassField" runat="server">
                            </asp:DropDownList>
                            <span class="light">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">密码加密：</td>
                        <td>
                            <asp:DropDownList ID="dlstPassFunction" runat="server">
                                <asp:ListItem Value="0" Selected="true">不加密</asp:ListItem>
                                <asp:ListItem Value="1">MD5加密</asp:ListItem>
                                <asp:ListItem Value="2">自定义加密</asp:ListItem>
                                <asp:ListItem Value="3">自定义加密解密</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">加密算法：</td>
                        <td>
                            <asp:TextBox ID="txtEcode" runat="server" CssClass="input_txt" Width="300px" Text="" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">解密算法：</td>
                        <td>
                            <asp:TextBox ID="txtDcode" runat="server" CssClass="input_txt" Width="300px" Text="" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnsubmit" runat="server" Text="提交" OnClick="btnsubmit_Click" CssClass="btn" OnClientClick="return chkData()" />
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
                <script type="text/javascript">
                    $('#dlstPassFunction').change(function() {
                        if($(this).get(0).selectedIndex == 2) {
                            $('#txtEcode').attr('disabled', '');
                            $('#txtDcode').attr('disabled', 'disabled');
                        }
                        else if($(this).get(0).selectedIndex == 3) {
                            $('#txtEcode').attr('disabled', '');
                            $('#txtDcode').attr('disabled', '');
                        }
                        else {
                            $('#txtEcode').attr('disabled', 'disabled');
                            $('#txtDcode').attr('disabled', 'disabled');
                        }
                    });
                    
                    function chkData() {
                        if($('#dlstTable').get(0).selectedIndex < 1) {
                            jAlert('warning', '请选择用于登录的表');
                            return false;
                        }
                        if($('#dlstLoginField').get(0).selectedIndex < 1) {
                            jAlert('warning', '请选择登录ID对应字段');
                            return false;
                        }
                        if($('#dlstNameField').get(0).selectedIndex < 1) {
                            jAlert('warning', '请选择显示名称对应字段');
                            return false;
                        }
                        if($('#dlstPassField').get(0).selectedIndex < 1) {
                            jAlert('warning', '请选择登录密码对应字段');
                            return false;
                        }
                        if($('#dlstPassFunction').get(0).selectedIndex == 2) {
                            if($.trim($('#txtEcode').val()) == '') {
                                jAlert('warning', '请设置自定义加密函数调用命令');
                                return false;
                            }
                        }
                        else if($('#dlstPassFunction').get(0).selectedIndex == 3) {
                            if($.trim($('#txtEcode').val()) == '') {
                                jAlert('warning', '请设置自定义加密函数调用命令');
                                return false;
                            }
                            if($.trim($('#txtDcode').val()) == '') {
                                jAlert('warning', '请设置自定义解密函数调用命令');
                                return false;
                            }
                        }
                        return true;
                    }
                </script>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
