<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.SystemState._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="运行状态" HeadTitleTxt="系统运行状态">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="系统信息">
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                    <tr>
                        <td class="table_body">
                            框架版本</td>
                        <td class="table_none">
                            <asp:Label ID="SystemName" runat="server"></asp:Label></td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td class="table_body">
                            框架Licensed</td>
                        <td class="table_none">
                            <asp:Label ID="Licensed_Txt" runat="server"></asp:Label>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('确认要移除当前序列号吗？');" OnClick="LinkButton1_Click">移除序列号</asp:LinkButton>
                            <span runat="server" id = "NoRegsion">
                            <asp:TextBox ID="Licensed_Value" CssClass="text_input" runat="server" Columns="36"></asp:TextBox>
                            <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="注册" OnClick="Button3_Click" />
                            <asp:HyperLink ID="Licensed_RegionUrl" runat="server" Target="_blank">获取序列号</asp:HyperLink>
                            </span></td>
                    </tr>
                    <tr>
                        <td class="table_body">
                            在线用户数据保存环境</td>
                        <td class="table_none">
                            <asp:Label ID="OnlineUseType" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td class="table_body">
                            当前在线用户</td>
                        <td class="table_none">
                            <asp:Label ID="OnlineUser" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="table_body">
                            缓存处理类</td>
                        <td class="table_none">
                            <asp:Label ID="CacheUseClass" runat="server"></asp:Label></td>
                    </tr>   
                    <tr >
                        <td class="table_body">
                            当前使用缓存数</td>
                        <td class="table_none">
                            <asp:Label ID="CacheNum" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="table_body">
                            可用缓存大小</td>
                        <td class="table_none">
                            <asp:Label ID="CacheMax" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="table_body">
                            服务器操作系统</td>
                        <td class="table_none">
                            <asp:Label ID="SystemOsName" runat="server"></asp:Label></td>
                    </tr>                    
                    <tr >
                        <td class="table_body">
                            服务器运行时间</td>
                        <td class="table_none">
                            <asp:Label ID="SystemRunTime" runat="server"></asp:Label></td>
                    </tr>
                    <tr >
                        <td class="table_body">
                            应用运行时间</td>
                        <td class="table_none">
                            <asp:Label ID="AppRunTime" runat="server"></asp:Label></td>
                    </tr>
                                        <tr >
                        <td class="table_body">
                            物理内存使用</td>
                        <td class="table_none">
                            <asp:Label ID="AppRunMemony" runat="server"></asp:Label></td>
                    </tr>
                                                            <tr >
                        <td class="table_body">
                            虚拟内存使用</td>
                        <td class="table_none">
                            <asp:Label ID="AppRunVirtualMemony" runat="server"></asp:Label></td>
                    </tr>
		</table>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="系统维护">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		<tr >
			<td class="table_title" >
                缓存清空</td>
		</tr>	
		<tr>
		    <td class="table_body">
                执行此操作，所有的web缓存将会清空．数据将重启从数据库中读取．<br />
                <asp:Button ID="Button1" CssClass="button_bak" OnClientClick="return confirm('确认要清空当前应用程序缓存吗？');" runat="server" Text="清空" OnClick="Button1_Click" /></td>
		</tr>
		<tr >
			<td class="table_title" >
                重启Web应用程序</td>
		</tr>	
		<tr>
		    <td class="table_body" style="height: 20px">
                你可以进行强制关闭并重启Web应用程序。你需要对重启动作确认。Web应用程序将在第一次请求后重启。<br />
                <b>警告</b>：所有的会话都将丢失，用户操作可能会出错。<br />
                <b>提示</b>：结果只影响到本Web应用程序。<br />
                <asp:Button ID="Button2" CssClass="button_bak" OnClientClick="return confirm('确认要重启当前Web应用程序吗？');" runat="server" Text="重启" OnClick="Button2_Click" /></td>
		</tr>		
		</table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>


</asp:Content>
