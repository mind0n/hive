<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu1_Disp.aspx.cs" Inherits="PmSys.web.Menu1_Disp" %>

<html >
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/Menu1.css" type="text/css" />
</head>
<body topmargin="3" leftmargin="0" class="style_0_menu_body">
<center>
<table border="0" cellpadding="0" cellspacing="0" width="90">
  <tr>
    <td align="right">
<%=sb_HTMLSrc.ToString()%>
    </td>
  </tr>
</table>
</center>
</body>
<script language="javascript" type="text/javascript">
    var NowOrderNo = <%=OrderNo%> * 2 - 1;
    var NowCount = <%=NowCount%>;
    var NowClickID;
    function TableOnMouseOver(xname)
    {
        NowClickID = window.parent.document.all.NowClickID.value;
        if ((NowClickID != xname) )
        {
            document.all[xname].className = "style_0_menu_ctable_on";
        }
    }
    function TableOnMouseOut(xname)
    {
        NowClickID = window.parent.document.all.NowClickID.value;
        
        if ((NowClickID != xname) )
        {
            document.all[xname].className = "style_0_menu_ctable_off";
        }
    }
    function TableOnClick(xname,linkname)
    {
        
        window.parent.document.all.NowClickID.value = xname;
        for(x=1;x<=NowCount;x++)
        {
        //alert(x);
            menuname = "x" + x;
            if (NowClickID != menuname)
            {
                document.all[menuname].className = "style_0_menu_ctable_off";
            }
        }  
        window.parent.parent.mainFrame.location.href = linkname;
	    AllResetCondition();
	    document.all[xname].className = "style_0_menu_ctable_click";
    }
    
    function ResetCondition()
    {
        for(x=1;x<=NowCount;x++)
        {
         	menuname = "x" + x;
		    document.all[menuname].className = "style_0_menu_ctable_off";
        }
    }
    
    function AllResetCondition()
    {
        var NowWindowRowsValue = window.parent.switchrows.rows;
	    var RowsArray = NowWindowRowsValue.split(",");
	    //alert(NowWindowRowsValue);
	    //alert(RowsArray.length);
	        for (xx=0;xx<RowsArray.length;xx++)
	        {
	            if (xx%2==1)
	            {
	                window.parent.frames(xx).ResetCondition();
	            }
	        }
    } 
</script>
</html>
