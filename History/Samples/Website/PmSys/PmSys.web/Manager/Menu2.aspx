<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu2.aspx.cs" Inherits="PmSys.web.Menu2" %>

<html>
<head>
    <title>无标题页</title>
    <link rel="stylesheet" href="css/Site_Css.css" type="text/css" />
</head>

<body topmargin="0" leftmargin="0">
<center>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
  <tr>
    <td height="28" background="images/Index/top-b-3-2.gif">
    <img border="0" src="images/Index/top-b-2-1-2.gif" align="left" hspace="0">
     <table border="0" cellpadding="0" cellspacing="0" height="28">
      <tr><%=sb_TopHTMLSrc.ToString() %></tr>
      <tr><%=sb_TopHTMLSrc2.ToString() %></tr>
     </table>
    </td>
  </tr>
  <tr>
    <td style="font-size:9pt; font-family: Arial" align="right" valign="bottom"><%=sb_DownHTMLSrc.ToString() %></td>
  </tr>
</table>
</center>
</body>

<script language="javascript" type="text/javascript">
    var TotalTopMenuCount = <%=TopMenuCount%>;
    var NowClickName="";
    var ItemClickName = "";
    function NowShow(TopMenuName,nowno)
    {
        for(x=1;x<=TotalTopMenuCount;x++)
        {
            document.getElementById(TopMenuName + x).className = "topmenuoff";
            document.getElementById(TopMenuName + x + "i").src = "images/index/top-b-1.gif";
            document.getElementById(TopMenuName + x + "_table").style.display = "none";
        }
        document.getElementById(TopMenuName + nowno).className = "topmenuon";
        document.getElementById(TopMenuName + nowno + "i").src = "images/index/top-b-2-2.gif";
        document.getElementById(TopMenuName + nowno + "_table").style.display = "";
        NowClickName = TopMenuName + nowno;
        
    }
    function ImageOverOROut(iname,ck)
    {
        if (NowClickName!=iname)
        {
            if (ck=="v")
            {
                document.getElementById(iname).className = "topmenuon";
            }
            else if (ck=="o")
            {
                document.getElementById(iname).className = "topmenuoff";
            }
        }
    }
    function xNowShow(iname,links)
    {
        if (ItemClickName!="")
        {
            document.getElementById(ItemClickName).style.backgroundImage = "url(images/index/top-b4-b.gif)";
            document.getElementById(ItemClickName).className = "topmenuoff2";
        }
       ItemClickName = iname;
       document.getElementById(iname).style.backgroundImage = "url(images/index/top-b4-yellow.gif)";
       document.getElementById(iname).className = "topmenuon2";
       window.parent.mainFrame.location.href = links;
    }
    function xImageOverOROut(iname,ck)
    {
        if (ItemClickName != iname)
        {
            if (ck=="v")
            {
                document.getElementById(iname).style.backgroundImage = "url(images/index/top-b4-yellow.gif)";
                document.getElementById(iname).className = "topmenuon2";
            }
            else if (ck=="o")
            {
                document.getElementById(iname).style.backgroundImage = "url(images/index/top-b4-b.gif)";
                document.getElementById(iname).className = "topmenuoff2";
            }
        }
    }
    <%if (TopMenuCount>0){
    %>
    NowShow('menu_',1);
    <%
    } %>
    
</script>
</html>