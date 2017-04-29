<%@ Page Language="C#" AutoEventWireup="true" Codebehind="left.aspx.cs" Inherits="PmSys.web.left" %>

<html>
<head>
    <title>-后台管理</title>
    <style type="text/css">
                .ttl { CURSOR: pointer; COLOR: #ffffff; PADDING-TOP: 4px }
                A:active{COLOR: #000000;TEXT-DECORATION: none}
                A:hover{COLOR: #000000;TEXT-DECORATION: none}
                A:link{COLOR: #000000;TEXT-DECORATION: none}
                A:visited{COLOR: #000000;TEXT-DECORATION: none}
                TD {
	            FONT-SIZE: 12px; FONT-FAMILY: "Verdana", "Arial", "细明体", "sans-serif"
                }
.table_body {	
BACKGROUND-COLOR: #EDF1F8;
height:18px;
CURSOR: pointer; 
}

.table_none {	
BACKGROUND-COLOR: #FFFFFF;
height:18px;
CURSOR: pointer; 
}

			</style>

    <script language="javascript">
	        function showHide(obj){
		        var oStyle = document.getElementById(obj).style;
		        oStyle.display == "none" ? oStyle.display = "block" : oStyle.display = "none";
	        }
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body bgcolor="#9aadcd" leftmargin="0" topmargin="0">
    <br>
    <asp:Repeater ID="LeftMenu" runat="server" OnItemDataBound="LeftMenu_ItemDataBound">
        <ItemTemplate>
            <table cellspacing="0" cellpadding="0" width="159" align="center" border="0">
                <tr>
                    <td width="23">
                        <img height="25" src="images/Menu/box_topleft.gif" width="23"></td>
                    <td class="ttl" onclick="JavaScript:showHide('M_<%# Eval("ModuleID")%>');" width="129"
                        background="images/Menu/box_topbg.gif">
                        <%# Eval("M_CName")%>
                    </td>
                    <td width="7">
                        <img height="25" src="images/Menu/box_topright.gif" width="7"></td>
                </tr>
            </table>
            <table id="M_<%# Eval("ModuleID")%>" style="display: none" cellspacing="0" cellpadding="0"
                width="159" align="center" border="0">
                <tr>
                    <td background='images/Menu/box_bg.gif' height="0px" width='159' colspan='3'>
                        <table width="157" border="0" cellpadding="2" cellspacing="1">
                            <tbody>
                                <asp:Repeater ID="LeftMenu_Sub" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="table_none" onclick="javascript:NowShow('M_<%# Eval("ModuleID")%>','<%# Eval("M_Directory")%>');" onmousemove="javascript:TDOverORIn('M_<%# Eval("ModuleID")%>');" onmouseout="javascript:TDOverOROut('M_<%# Eval("ModuleID")%>');" id="M_<%# Eval("ModuleID")%>">
                                                <img height='7' hspace='5' src='images/Menu/arrow.gif' width='5' align="bottom">
									            <%# Eval("M_CName")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" width="159" align="center" border="0">
                <tr>
                    <td colspan="3">
                        <img height='10' src='images/Menu/box_bottom.gif' width='159'></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
<script language="javascript">
    var NowClickName="";
    
        function NowShow(TopMenuName,Url)
    {
        document.getElementById(TopMenuName).className  = "table_body";
        if (NowClickName!="" &&NowClickName!=TopMenuName)
            document.getElementById(NowClickName).className  = "table_none"; 
        NowClickName = TopMenuName;
        //var o=window.open(url); 
       window.parent.frames["mainFrame"].location=Url;
       //parment.mainFrame.src=Url;
    }
    
    function TDOverOROut(iname)
    {
        if (NowClickName!=iname)
        {

            document.getElementById(iname).className = "table_none";

        }
    }
        function TDOverORIn(iname)
    {
        if (NowClickName != iname)
        {
            document.getElementById(iname).className = "table_body";
        }
    }
</script>