<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_listCss, App_Web_puqoct7y" %>

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
            当前位置 >> 信息列表样式控制
        </div>
        <div class="content">
            <fieldset style="width:500px;">
                <legend>信息列表调整</legend>
                <table class="csstable" cellpadding="0" cellspacing="0">
                    <tr style="background-color:#dcdcdc; font-weight:bold;">
                        <td style="width:120px;" align="center">表名</td>
                        <td style="width:160px" align="center">显示字段名</td>
                        <td align="center">列表占宽</td>
                    </tr>
                    <%ShowBody(); %>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <input id="Reset1" type="reset" value="重置" class="btn" />
                            <input id="btnsubmit" type="submit" value="提交" class="btn" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" value="action" name="action" id="action" />
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
