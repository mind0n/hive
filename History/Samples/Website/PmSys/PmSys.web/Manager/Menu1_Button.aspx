<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Menu1_Button.aspx.cs" Inherits="PmSys.web.Menu1_Button" %>

<html>
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/Menu1.css" type="text/css" />
</head>
<body topmargin="0" leftmargin="0">
    <center>
        <input type="button" style="cursor: hand" value="<%=APCatCName%>" name="<%=APCatalogueID%>"
            class="<%=ButtonClass%>" onclick="javascript: SwitchWindowRows('menubarbutton_<%=APCatalogueID%>');">
    </center>
</body>

<script language="javascript" type="text/javascript">
    var NowOrderNo = <%=OrderNo%> * 2 - 1;
	var OldValue = 0;
	var timerid ;
    function SwitchWindowRows(menubarbuttonname)
    {    
    	  if (document.all.<%=APCatalogueID%>.className != "style_0_nowmenu_button")
    	  {

	      var SetWindowRowsValue = ""
	      var NowWindowRowsValue = window.parent.switchrows.rows;
	      var RowsArray = NowWindowRowsValue.split(",");

    	     // alert(RowsArray.length);
	      for (x=0;x<RowsArray.length;x++)
	      {
	        if (x==NowOrderNo)
	        {
	            if (RowsArray[x]=="*")
	            {
	                break;
	            }
	            else
	            {
	                SetWindowRowsValue = SetWindowRowsValue + "10";
	            }
	        }
	        else	    
	        {
	            if  (RowsArray[x] == "*")
	            {
	                SetWindowRowsValue = SetWindowRowsValue + "0";
	            }
	            else
	            {
	                SetWindowRowsValue = SetWindowRowsValue + RowsArray[x];
	            }
	        }
            if (x!=RowsArray.length)
            {
                //alert(x);
                SetWindowRowsValue = SetWindowRowsValue + ",";
            }
            if ((x%2==0) && (NowOrderNo-1 != x))
            {
                window.parent.frames(x).MenuButtonClass();
            }
	      }
	      
	      OldValue = 10;	      
	      var Index = SetWindowRowsValue.length;
          if (SetWindowRowsValue.substring(Index-1,Index)==",")
          {
           SetWindowRowsValue = SetWindowRowsValue.substring(0,Index-1);
          }
	      window.parent.switchrows.rows = SetWindowRowsValue;
	      //alert("a");
	      timerid = setInterval("WindowRowsMove()", 10);
	      //alert("b");
	      window.parent.frames(NowOrderNo-1).MenuOnButtonClass();
	      //alert("c");
	      }
    }
    
    function WindowRowsMove()
    {
        
        var NowWindowRowsValue = window.parent.switchrows.rows;
        if (OldValue>20)
        {
        //alert("dfd");
          	NowWindowRowsValue = NowWindowRowsValue.replace(OldValue,"*");
	        window.parent.switchrows.rows = NowWindowRowsValue;
	        clearInterval(timerid); 
        }
        else
        {
            var NowValue = OldValue+20 ;
	        NowWindowRowsValue = NowWindowRowsValue.replace(OldValue,NowValue);
	        window.parent.switchrows.rows = NowWindowRowsValue;
	        OldValue = NowValue ;
        }
    }
    	function MenuButtonClass()
    	{
            document.all.<%=APCatalogueID%>.className = "style_0_menu_button";	
    	}
	 


	function MenuOnButtonClass()
	{	
	 document.all.<%=APCatalogueID%>.className = "style_0_nowmenu_button";
	}

</script>

</html>
