<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_sysNodeEdit, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="Stylesheet" type="text/css" href="css/sub.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.alerts.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 后台功能编辑
        </div>
        <div class="content">
            <fieldset style="width:500px;">
                <legend>编辑功能</legend>
                <table cellpadding="0" cellspacing="0" style="width:800px; text-align:left;">
                    <tr>
                        <td style="width:80px" align="right">功能名称：</td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" CssClass="input_txt" Width="200px" MaxLength="20"></asp:TextBox>
                            <span class="light">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">显示方式：</td>
                        <td>
                            <asp:DropDownList ID="dlstmethod" runat="server">
                                <asp:ListItem Value="0">无</asp:ListItem>
                                <asp:ListItem Value="1">内容编辑</asp:ListItem>
                                <asp:ListItem Value="2">记录列表</asp:ListItem>
                                <asp:ListItem Value="3">页面链接</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right"><span id="nn">对应表</span>：</td>
                        <td>
                            <asp:DropDownList ID="dlsttable" runat="server">
                            </asp:DropDownList><asp:TextBox ID="txtPage" runat="server" CssClass="input_txt" Width="200px" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">排序：</td>
                        <td>
                            <asp:TextBox ID="txtsort" Text="1" runat="server" CssClass="input_txt" Width="40px" MaxLength="4"></asp:TextBox>
                            <span class="remark">[值越大越靠后]</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">方向：</td>
                        <td>
                            <asp:DropDownList ID="dlsthvm" runat="server">
                                <asp:ListItem Value="0">纵向</asp:ListItem>
                                <asp:ListItem Value="1">横向</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">父节点：</td>
                        <td>
                            <asp:DropDownList ID="dlstparent" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">赛选条件：</td>
                        <td>
                            <asp:TextBox ID="txtWhere" runat="server" TextMode="MultiLine" Height="80px" Width="400px" CssClass="input_txt"></asp:TextBox>
                            <br />
                            <span class="light">请给字段名加上固定前缀：tzh1
                                                <br />如：tzh1.field1=11 and tzh1.field2='value2'
                                                <br />条件开头不要添加where、and、or、group by、order by等连接词
                                                <br />不支持多表关联</span>
                        </td>
                    </tr>
                    <tr style="height:30px;">
                        <td></td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="添加功能" CssClass="btn" OnClick="btnSubmit_Click" OnClientClick="return chkData()" />
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    $('#dlstmethod').change(function() {
                        if($(this).get(0).selectedIndex == 3) {
                            $('#nn').attr('innerHTML', '链接页面');
                            $('#dlsttable').css('display', 'none');
                            $('#txtPage').css('display', '');
                        }
                        else {
                            $('#nn').attr('innerHTML', '对应表');
                            $('#dlsttable').css('display', '');
                            $('#txtPage').css('display', 'none');
                        }
                    });
                    function chkData() {
                        if($.trim($("#txtname").val()) == "") {
                            jAlert("warning", "请填写名称");
                            return false;
                        }
                        if(!/^[0-9]*[1-9][0-9]*$/.test($("#txtsort").val())) {
                            jAlert("warning", "排序必须为正整数");
                            return false;
                        }
                        if($('#dlstmethod').get(0).selectedIndex == 3 && $.trim($('#txtPage').val()) == '') {
                            jAlert("warning", "链接页面不能为空");
                            return false;
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
