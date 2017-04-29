<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_sysNodeList, App_Web_puqoct7y" %>

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
        
        function addNode() {
            location.href = "sysNodeEdit.aspx";
        }
        
        function menuDel(id) {
            jConfirm("确定要删除选中记录吗？", "询问", function(r) {
                if(r) {
                    location.href = "?id=" + id;
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 后台功能结构管理
        </div>
        <div class="operdiv">&nbsp;
            <input type="button" class="optbtn" value="添加功能" onclick="addNode()" />
            <input type="submit" class="optbtn" value=" 保存 " />
        </div>
        <div class="contsub">
            <table class="table" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <th style="width:200px;">功能名</th>
                        <th style="width:150px;">对应功能</th>
                        <th style="width:60px">排序</th>
                        <th style="width:60px">方式</th>
                        <th style="width:80px;">菜单方向</th>
                        <th style="width:80px;">是否显示</th>
                        <th style="width:200px;"></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <%ShowMenuList(); %>
                </tbody>
            </table>
        </div>
        <input type="hidden" value="action" name="action" id="action" />
    </div>
    </form>
</body>
</html>
