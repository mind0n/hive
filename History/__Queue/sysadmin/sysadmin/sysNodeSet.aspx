<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_sysNodeSet, App_Web_puqoct7y" %>

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
    <script type="text/javascript">
        $(document).ready(function(){
            $('.table tbody tr').hover(
                function() {$(this).addClass('odd');},
                function() {$(this).removeClass('odd');}
            );
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 表映射设置
        </div>
        <div class="contsub">
            <div class="operdiv" style="color:Red;">
                &nbsp;1. 如果字段为主键，请将“主键”设置为“是”<br />
                &nbsp;2. 如果主键在数据库中为自动编号或者自动生成，请将“可编辑”设置为“否”，同时“是否使用”设置为“否”，否则主键将会由系统自动生成<br />
                &nbsp;3. 如果是主键，不建议将其显示在页面上<br />
                &nbsp;4. 列表显示如果设置为“是”，则该字段将显示在列表里<br />
                &nbsp;5. 调整字段排序可以改变字段显示的顺序，值越大越靠后<br />
                <br />&nbsp;
                <input type="submit" class="optbtn" value="保存" />
                <input type="button" class="optbtn" value="返回" onclick="location.href='sysNodeList.aspx'" />
            </div>
            <table cellpadding="0" cellspacing="0" class="table">
                <thead>
                    <tr>
                        <th style="width:100px">字段名</th>
                        <th style="width:80px">类型</th>
                        <th style="width:80px">显示名</th>
                        <th style="width:60px">主键</th>
                        <th style="width:60px">可编辑</th>
                        <th style="width:60px">是否使用</th>
                        <th style="width:60px">列表显示</th>
                        <th style="width:60px">列表排序</th>
                        <th style="width:60px">字段排序</th>
                        <th style="width:100px">输入方式</th>
                        <th style="width:60px">控件高</th>
                        <th style="width:60px">控件宽</th>
                        <th style="width:60px">必填</th>
                        <th style="width:150px">备注</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <%ShowTable(); %>
                </tbody>
            </table>
            <input type="hidden" value="action" name="action" id="action" />
        </div>
    </div>
    </form>
</body>
</html>
