<%@ page language="C#" autoeventwireup="true" inherits="tzhsysadmin_success, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="Stylesheet" type="text/css" href="css/sub.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="content" style="text-align:center;width:100%;">
            <div style="border:1px solid #80df33; background-color:#e3f9d1;width:300px; height:200px;text-align:left;">
                <span style="margin:0 50px 0 50px; display:block;">
                    <p style="font-size:18px;">您的操作已经成功！</p>
                    <p><%=msg %></p>
                    <p>接下来您想干什么？</p>
                    <p><%ShowButton(); %></p>
                </span>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
