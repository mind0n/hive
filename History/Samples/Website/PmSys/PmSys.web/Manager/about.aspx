<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="PmSys.web.about"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
<table border=0 cellpadding=0 cellspacing=0 width="100%">
  <tr> 
    <td height="43" bgcolor="#ffffff" colspan="2" ><b><font size="2" color="#999999" face="Verdana, Arial, Helvetica, sans-serif"><asp:Label ID="SystemName" runat="server"></asp:Label></font></b></td>
  </tr>
  <tr> 
    <td height=1 bgcolor=#000000 colspan="2"></td>
  </tr>
  <tr>       
    <td height=120 colspan="2"> 
      <table border=0 cellpadding=0 cellspacing=10 width="100%">
        <tr> 
          <td width="50%" valign="top">
			<font color="#cccccc">
				<b>
					<font color="#000000" face="Verdana, Arial, Helvetica, sans-serif" size="2">
						联系方式：
					</font>
				</b><br />
			<br />
			<font color="#000000" face="Verdana, Arial, Helvetica, sans-serif" size="2">
                weerspecial@gmail.com
				<br />
				<font size="1">Blair & Mark</font>
			</font>
			</font></td>
			<td width="50%" valign="top"><font color="#000000">
				<b>
				<font face="Verdana, Arial, Helvetica, sans-serif" size="2">产品信息</font>
				</b><br /><br />
				<font >签证管理系统 1.0.7 Release(MsSql)</font>
			</td>
        </tr>
        <tr> 
          <td colspan="2" align="right" style="height: 28px"> &nbsp;<input type=button value="关闭" onClick="window.top.hidePopWin();" name="button" class="button_bak"></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
</asp:Content>
