<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="FieldValueManager.aspx.cs" Inherits="PmSys.web.Module.PmSys.FieldManager.FieldValueManager"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
<link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/css/subModal.css" />
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/common.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/subModal.js"></script>
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        ButtonList-Capacity="4" HeadOPTxt="应用字段管理" HeadTitleTxt="应用字段设定">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="应用字段" ButtonPopedom="List" ButtonUrlType="Href" ButtonUrl="default.aspx"
            ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="应用字段管理">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		    <tr id="TopTr" runat="server">
			    <td class="table_body" style="width: 25%; height: 28px">
                    应用字段Key</td>
			    <td class="table_none" style="height: 28px" colspan="2">
                    <asp:TextBox ID="F_Key" title="请输入应用字段Key~50:!" CssClass="text_input" runat="server"></asp:TextBox>
                    <asp:Label ID="F_Key_Txt" runat="server" Visible="False"></asp:Label></td>
		    </tr>
            <tr runat="server">
                <td class="table_body" style="width: 25%">
                    应用字段名称</td>
                <td class="table_none" colspan="2">
                    <asp:TextBox ID="F_CName" title="请输入应用字段名称~50:!" CssClass="text_input" runat="server"></asp:TextBox>
                    <asp:Label ID="F_CName_Txt" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body" style="width: 25%">
                    应用字段说明</td>
                <td class="table_none" colspan="2">
                    <asp:TextBox ID="F_Remark" title="请输入应用字段说明~200:" runat="server" CssClass="text_input" Width="193px"></asp:TextBox>
                    <asp:Label ID="F_Remark_Txt" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <div id="Disp_Input" runat="server">
	        <tr>
		        <td colspan="3" align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                </td>
		    </tr>
		    </div>
		    <div id="Disp_Sub" runat="server">
            <tr class = "table_title">
		        <td>ID</td>
		        <td style="height: 30px">应用字段值列表(点击字段值进行管理)</td>
		        <td>应用字段编码</td>
		    </tr>	
		    <asp:Repeater ID="Main_Value" runat="server">
		        <ItemTemplate>
		            <tr class="table_none" onmouseout="javascript:this.className='table_none'" onmouseover="javascript:this.className='table_body'">
		                <td><%#Eval("ValueID")%></td>
		                <td title="点击此处进行字段管理" style="cursor:pointer" onclick="javascript:showPopWin('应用字段值修改','FieldValueBox.aspx?CMD=Edit&FieldID=<%=FieldID%>&ValueID=<%#Eval("ValueID") %>&'+rand(10000000), 300, 160, AlertMessageBox,true);">
		                    <%#Eval("V_Text") %>
		                </td>
		                <td><%#Eval("V_Code") %></td>
		            </tr>
		        </ItemTemplate>
		    </asp:Repeater>
		    </div>
		</table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
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
    function AlertMessageBox(Messages)
    {
        alert(Messages);
         setTimeout("reload()",100)
    }
    
        function reload()
    {
        window.location.href = location.href+"?"+rand(1000000);
    }
    
    var gDefaultLogin = "<%=Page.ResolveUrl("~/") %>Manager/inc/finemessbox/loading.html";    
    </script>
</asp:Content>
