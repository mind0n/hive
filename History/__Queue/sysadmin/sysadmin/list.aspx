<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_list, App_Web_puqoct7y" %>

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
            $('.table tbody tr').click(
                function() {
                    if ($(this).find('input[type="checkbox"]').attr('checked')) {
                        $(this).find('input[type="checkbox"]').removeAttr('checked');
                        $("#chkall").removeAttr('checked');
                    } 
                    else {
                        $(this).find('input[type="checkbox"]').attr('checked','checked');
                    }
                }
            );
        });
        function chkallorcancel()
        {
            var chks = $("input[name='chklist']").attr('checked', $('#chkall').attr('checked'));
        }
        function getIdStr() {
            var ids = "";
            $("input[name='chklist']").each(function() { if($(this).attr("checked")) ids += ";" + $(this).attr("value"); });
            if(ids != "") return ids.substring(1);
            return "";
        }
        function delCheck() {
            var id = getIdStr();
            var name = $("#tzh_tb").val();
            var menu = $("#tzh_mu").val();
            if(id == "") {
                jAlert("warning", "至少需要选中一条记录");
                return;
            } 
            jConfirm("确定要删除选中记录吗？", "询问", function(r){ if(r) location.href = "?n=" + name + "&m=" + menu + "&id=" + id; });
        }
        function del(id) {
            var name = $("#tzh_tb").val();
            var menu = $("#tzh_mu").val();
            jConfirm("确定要删除该条记录吗？", "询问", function(r){ if(r) location.href = "?n=" + name + "&m=" + menu + "&id=" + id; });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 信息列表
        </div>
        <div class="contsub">
            <div class="operdiv">&nbsp;
                <input type="button" class="optbtn" value="删除选中" onclick="delCheck()" />
            </div>
            <table class="table" cellpadding="0" cellspacing="0" style="text-align:left">
                <thead style="text-align:center">
                    <tr>
                        <th style="width:30px;"><input type="checkbox" id="chkall" onclick="chkallorcancel()" /></th>
                        <%ShowTHead(); %>
                        <th style="width:80px;"></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <%Show(15); %>
                </tbody>
            </table>
            <div runat="server" id="pdiv" class="pagediv"></div>
        </div>
    </div>
    </form>
</body>
</html>
