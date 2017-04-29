<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_joinTableSet, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="Stylesheet" type="text/css" href="css/sub.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.alerts.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript">
        function request(paras) { 
            var url = location.href; 
            var paraString = url.substring(url.indexOf("?")+1,url.length).split("&"); 
            var paraObj = {};
            for (i=0; j=paraString[i]; i++) { 
                paraObj[j.substring(0,j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=")+1,j.length); 
            } 
            var returnValue = paraObj[paras.toLowerCase()]; 
            if(typeof(returnValue)=="undefined"){ 
                return ""; 
            }else{ 
                return returnValue; 
            } 
        }
        
        var t = request('n');
        function showLinkInfo() {
            var f = $('#dlstLinkField').find('option:selected').val();
            if(f != '') {
                jQuery.post('js/getjoin.aspx', { t: t, f: f }, function (responseText) {
                    $("#lblinfo").attr('innerHTML', responseText);
                });
            } else {
                $("#lblinfo").attr('innerHTML', '');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 表字段外联接设置
        </div>
        <div class="content">
            <fieldset style="width:600px;">
                <legend>外联设置</legend>
                <table cellpadding="0" cellspacing="0" style="width:800px; text-align:left;">
                    <tr>
                        <td style="width:80px" align="right">外联字段：</td>
                        <td>
                            <asp:DropDownList ID="dlstLinkField" runat="server" onchange="showLinkInfo()">
                            </asp:DropDownList>
                            <span id="lblinfo" style="color:Green"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">连接表：</td>
                        <td>
                            <asp:DropDownList ID="dlsttable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dlsttable_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">值字段：</td>
                        <td>
                            <asp:DropDownList ID="dlstvalue" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">文本字段：</td>
                        <td>
                            <asp:DropDownList ID="dlstText" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">查询条件：</td>
                        <td>
                            <asp:TextBox ID="txtWhere" runat="server" TextMode="MultiLine" Height="80px" Width="500px" CssClass="input_txt"></asp:TextBox>
                            <br />
                            <span class="light">如：field1=11 and field2='value2'<br />条件开头不要添加where、and、or等连接词<br />不支持多表关联</span>
                        </td>
                    </tr>
                    <tr style="height:30px;">
                        <td></td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="设置外联" CssClass="btn" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
