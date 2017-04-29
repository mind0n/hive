<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.SystemConfig._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="环境配置" HeadTitleTxt="系统环境配置" ButtonList-Capacity="4">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="环境配置" ButtonPopedom="Edit" ButtonUrl="?CMD=Edit"
            ButtonUrlType="Href" ButtonVisible="True" />
        <FrameWorkWebControls:HeadMenuButtonItem ButtonIcon="back.gif" ButtonName="返回" ButtonPopedom="List"
            ButtonUrl="?" ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="系统配置">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
            <tr>
                <td class="table_body">
                    系统名称</td>
                <td class="table_none">
                    <asp:Label ID="lb_S_Name" runat="server"></asp:Label>
                    <asp:TextBox ID="tb_S_Name" title="请输入系统名称~255:!" CssClass="text_input" Columns="60" runat="server"></asp:TextBox></td>
            </tr>
                <tr>
                    <td class="table_body">
                        系统版本</td>
                    <td class="table_none">
                        <asp:Label ID="lb_S_Version" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_S_Version" title="请输入系统版本~20:!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td class="table_body" style="height: 28px" >
                        是否自动检测新版本 默认为true</td>
                    <td class="table_none" style="height: 28px" >
                        <asp:Label ID="lb_C_CheckUpdate" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddl_C_CheckUpdate" runat="server">
                            <asp:ListItem>False</asp:ListItem>
                            <asp:ListItem>True</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                                
                
                <tr>
                    <td class="table_body" style="height: 28px" >
                        是否启用用户访问日志 默认为false</td>
                    <td class="table_none" style="height: 28px" >
                        <asp:Label ID="lb_C_RequestLog" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddl_C_RequestLog" runat="server">
                            <asp:ListItem>False</asp:ListItem>
                            <asp:ListItem>True</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                
                <tr>
                    <td class="table_body">
                        是否允许GZip压缩输出网页 默认false</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_HttpGZip" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddl_C_HttpGZip" runat="server">
                            <asp:ListItem>False</asp:ListItem>
                            <asp:ListItem>True</asp:ListItem>
                        </asp:DropDownList>
                        <font color="red">使用此功能，可能导致Ajax远程读取数据无效!</font>
                        </td>
                </tr>
                
                <tr>
                    <td class="table_body">
                        IP地址查询链接</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_IPLookUrl" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_IPLookUrl"  title="IP地址查询链接~255:!" CssClass="text_input" runat="server" Columns="60"></asp:TextBox></td>
                </tr>                
                <tr>
                    <td colspan="2" align="right">
                        </td>
                </tr>
            </table>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="上传文件设置">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr>
                    <td class="table_body">
                        上传文件路径 默认为/Manager/Public/</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_UploadPath" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_UploadPath" title="请输入上传文件路径~!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="table_body">
                        上传图片缩图高度 默认120px</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_UpImgHeight" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_UpImgHeight" title="请输入上传图片缩图高度~int!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="table_body">
                        上传图片缩图宽度 默认 180px</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_UpImgWidth" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_UpImgWidth" title="请输入上传图片缩图宽度~int!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td class="table_body">
                        上传文件扩展名 多个以,号分开</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_UploadFileExt" runat="server"></asp:Label>
                        <asp:TextBox Columns="60" ID="tb_C_UploadFileExt" title="请输入上传文件扩展名 多个以,号分开~255:!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td class="table_body">
                        上传文件大小(Kb为单位)</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_UploadSizeKb" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_UploadSizeKb" title="请输入上传文件大小(Kb为单位)~int!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
            </table> 
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem3" runat="server" Tab_Name="登入安全设置">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">

                                <tr>
                    <td class="table_body">
                        允许登入时密码出错次数<br />(默认为5次,同一IP同一用户名)</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_LoginErrorMaxNum" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_LoginErrorMaxNum" title="请输入允许登入时密码出错次数~int!" CssClass="text_input" runat="server"></asp:TextBox></td>
                </tr>
                                <tr>
                    <td class="table_body">
                        禁止登入时间(默认30)分</td>
                    <td class="table_none">
                        <asp:Label ID="lb_C_LoginErrorDisableMinute" runat="server"></asp:Label>
                        <asp:TextBox ID="tb_C_LoginErrorDisableMinute" title="请输入禁止登入时间~int!" CssClass="text_input" runat="server"></asp:TextBox>
                        <font color="red">设定此值需要重启动应用程序</font>
                        </td>
                </tr>


            </table>  
        </FrameWorkWebControls:TabOptionItem>
       
 </FrameWorkWebControls:TabOptionWebControls>
    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
	<tr>
		<td align="right">
            <asp:Button ID="Button1" runat="server" Text="确定" CssClass="button_bak" OnClick="Button1_Click" />
        </td>
	</tr>
</table>


</asp:Content>
