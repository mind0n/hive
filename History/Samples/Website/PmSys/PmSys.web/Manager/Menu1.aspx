<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu1.aspx.cs" Inherits="PmSys.web.Menu1" %>
<html>
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" href="css/Menu1.css" type="text/css" />
</head>

  <frameset rows="<%=sb_FramesetRows.ToString()%>" name="switchrows" TOPMARGIN="0" LEFTMARGIN="0" MARGINHEIGHT="0" MARGINWIDTH="0" FRAMEBORDER="0" BORDERCOLOR="#DDDBB0" BORDER="0">
	<%=sb_HTMLSrc.ToString()%>
  </frameset>
  <noframes>
  <body>

  <p>此网页使用框架，但是您的浏览器并不支援。</p>

  </body>
  </noframes>

<body>
<input type="hidden" name="NowClickID" value="0" />
</body>
</html>
