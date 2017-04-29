<%@ page language="C#" autoeventwireup="true" inherits="tzhsysadmin_sysmain, App_Web_puqoct7y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/index.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/resize.js"></script>
    <script type="text/javascript">
        function frmUrl(url)
        {
            $('#frmbody').attr('src', url);
        }
        function menu(num)
        {
            $('#m' + num).css('display',$('#m' + num).css('display') == 'none' ? '' : 'none');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bodydiv">
        <div class="head">
            <div class="time" id="mytime">
                <img src='images/0.gif' name="tts" /><img src='images/0.gif' name="tts" /><img src='images/colon.gif'/ ><img src='images/0.gif' name="tts" /><img src='images/0.gif' 
                name="tts" /><img src='images/colon.gif' /><img src='images/0.gif' name="tts" /><img src='images/0.gif' name="tts" />
                <script type="text/javascript">
                    var n = new Array();
                    n[1] = new Image(); n[1].src="images/1.gif";
                    n[2] = new Image(); n[2].src="images/2.gif";
                    n[3] = new Image(); n[3].src="images/3.gif";
                    n[4] = new Image(); n[4].src="images/4.gif";
                    n[5] = new Image(); n[5].src="images/5.gif";
                    n[6] = new Image(); n[6].src="images/6.gif";
                    n[7] = new Image(); n[7].src="images/7.gif";
                    n[8] = new Image(); n[8].src="images/8.gif";
                    n[9] = new Image(); n[9].src="images/9.gif";
                    n[0] = new Image(); n[0].src="images/0.gif";
                    function showTime() {
                        var date = new Date();
                        var h = (date.getHours() < 10 ? "0" : "") + date.getHours();
                        var m = (date.getMinutes() < 10 ? "0" : "") + date.getMinutes();
                        var s = (date.getSeconds() < 10 ? "0" : "") + date.getSeconds();
                        var timgs = document.getElementsByName("tts");
                        timgs[0].src = n[h.substring(0, 1)].src;
                        timgs[1].src = n[h.substring(1)].src;
                        
                        timgs[2].src = n[m.substring(0, 1)].src;
                        timgs[3].src = n[m.substring(1)].src;
                        
                        timgs[4].src = n[s.substring(0, 1)].src;
                        timgs[5].src = n[s.substring(1)].src;
                    }
                    
                    setInterval("showTime()", 1000);
                </script>
            </div>
            <div class="hmenu">
                <%LoadHMenu(); %>
                <div class="hm"><a href="javascript:void(0);" onclick="frmUrl('updpass.aspx')">修改密码</a></div>
                <div class="hm"><a href="logout.aspx">退出系统</a></div>
                <div style="float:right;line-height:25px;height:25px;width:320px;">
                    当前用户：<asp:Label ID="lblname" runat="server" Text="admin"></asp:Label>
                </div>
            </div>
        </div>
        <div class="leftmenu" id="lmenu">
            <div class="lhead">管理菜单</div>
            <ul>
	            <%LoadVMenu(true); %>
            </ul>
        </div>
        <div class="rightfrm" id="rfrm">
            <iframe id="frmbody" src="default.aspx" frameborder="0" width="100%" height="100%" scrolling="auto"></iframe>
        </div>
        <div class="floor">
            <span>技术支持：<a href="http://www.tzhtec.com" target="_blank">天智海科技</a> CopyRight <span style="font-family:Arial">&copy;</span> ziyou</span>
        </div>
    </div>
    </form>
</body>
</html>
