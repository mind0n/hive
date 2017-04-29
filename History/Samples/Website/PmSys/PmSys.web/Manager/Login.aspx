<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Login.aspx.cs" Inherits="PmSys.web.Login" %>

<html>
<head>
    <link rel="stylesheet" href="css/Site_Css.css" type="text/css" />
<link rel="shortcut icon" href="images/Icon.ico" type="image/x-icon" />
    <script language="javascript" src="js/checkform.js"></script>

    <title>
        <%=FrameName %>
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body scroll="no" leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
        <tr>
            <td width="100%" height="50" colspan="3" style="border-bottom: 1px solid #000000">
                <table height="49" border="0" cellspacing="0" cellpadding="0" width="100%" class="font_size">
                    <tr>
                        <td style="background-image: url(images/top-gray.gif); background-repeat: no-repeat;
                            background-position: right top">
                            <b>
                                <%=FrameName %>
                            </b>
                            <br />
                            <font size="2" color="#999999" face="Verdana, Arial, Helvetica, sans-serif">
                                <%=FrameNameVer %>
                            </font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
                    <form name="login" method="post" runat="server" defaultfocus="LoginName" onsubmit="javascript:return checkForm(this)">
                        <tr>
                            <td>
                                <table width="431" border="0" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td colspan="2" background="images/Logon/Logon_1.gif" width="431" height="125">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <img src="images/Logon/Logon_2.gif" width="431" height="30" alt="" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="images/Logon/Logon_3.gif" width="194" height="28" alt="" /></td>
                                        <td background="images/Logon/Logon_4.gif" width="237">
                                            <asp:TextBox ID="LoginName" class="text_input" title="请输入帐号~!" runat="server" Style="width: 138px;"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="images/Logon/Logon_5.gif" width="194" height="24" alt="" /></td>
                                        <td background="images/Logon/Logon_6.gif">
                                            <asp:TextBox ID="LoginPass" runat="server" class="text_input" title="请输入密码~!" TextMode="Password"
                                                Style="width: 138px;"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img id="Logincode_op" runat="server" src="images/Logon/Logon_7.gif" width="194" height="25" alt="" /></td>
                                        <td rowspan="3" background="images/Logon/Logon_18.jpg" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <div id="inputcode_op" runat="server">
                                                <tr>
                                                    <td height="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img
                                                            src="" onload="javascript:Open_Submit();" id="ImageCheck" align="absmiddle" style="cursor: pointer"
                                                            width="138" height="35" onclick="javascript:ChangeCodeImg();" title="点击更换验证码图片!" />
                                                    </td>
                                                </tr>
												<tr><td>
												<asp:TextBox ID="code_op" class="text_input" runat="server" Columns="6" title="请输入输入上图中显示的字母~!"></asp:TextBox>
												<font color="red">输入上图中显示的字母。</font></td></tr>
                                                </div>
                                                <tr>
                                                    <td height="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button1" disabled="disabled" runat="server" Text="确定" class="button_bak"
                                                            OnClick="Button1_Click" />
                                                        <input type="reset" value="重填" class="button_bak" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="images/Logon/Logon_9.gif" width="194" height="40" alt="" /></td>
                                    </tr>
                                    <tr>
                                        <td height="39" background="images/Logon/Logon_11.gif">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>提醒 : </b>本管理系统建议议采用Internet Explorer 5.5 (或以上版本) 的浏览器。请开启浏览器的 Cookies 与 JavaScript
                                            功能。</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </form>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="20">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="20">
                    <tr>
                        <td class="down_text">
                            Powered By <a href="http://www.supesoft.com" target="_blank"><font color="#ffffff">Supesoft.com</font></a>
                            Information Technology Logistics Inc.</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>

<script language="JavaScript" type="text/javascript"><!-- 

    // The Central Randomizer 1.3 (C) 1997 by Paul Houle (houle@msc.cornell.edu) 

    // See: http://www.msc.cornell.edu/~houle/javascript/randomizer.html 

    rnd.today=new Date(); 

    rnd.seed=rnd.today.getTime(); 

    function rnd() { 

　　　　rnd.seed = (rnd.seed*9301+49297) % 233280; 

　　　　return rnd.seed/(233280.0); 

    }; 

    function rand(number) { 

　　　　return Math.ceil(rnd()*number); 

    }; 

    // end central randomizer. --> 

</script>

<script language="javascript" type="text/javascript">

    if (document.getElementById("code_op")!=null)
    {
        ChangeCodeImg();
        
    }
    else
    {
        document.getElementById("Button1").disabled = false;
    }
    function ChangeCodeImg()
    {
             a = document.getElementById("ImageCheck");
             a.src = "inc/CodeImg.aspx?"+rand(10000000);
             document.getElementById("Button1").disabled = true;
    }
    
    function Open_Submit()
    {
        document.getElementById("Button1").disabled = "";
    }
    
    if(top!=self)
    {
        top.location.href = "login.aspx";
    }
    //alert(navigator.appVersion);
    if(navigator.appVersion.indexOf("MSIE")   ==   -1   ){   
        //alert("提醒 : 本管理系统建议议采用Internet Explorer 5.5 (或以上版本) 的浏览器。请开启浏览器的 Cookies 与 JavaScript 功能。");   
    }   
    
</script>

