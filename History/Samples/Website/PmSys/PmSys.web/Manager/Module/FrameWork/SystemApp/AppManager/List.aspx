<%@ Page Language="C#"  MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="PmSys.web.Module.PmSys.AppManager.List"  %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="应用列表" HeadTitleTxt="应用列表管理">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="应用" ButtonPopedom="New" ButtonUrl="AppManager.aspx?CMD=New"
            ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="应用列表">
           
            <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="应用ID" DataField="ApplicationID"/>
                    <asp:HyperLinkField HeaderText="应用名称" DataTextField="A_AppName" DataNavigateUrlFields="ApplicationID" DataNavigateUrlFormatString="AppManager.aspx?S_ID={0}&CMD=Look" />                
                    <asp:BoundField HeaderText="应用介绍" DataField="A_AppDescription"/>
                    <asp:HyperLinkField HeaderText="应用网址" DataTextField="A_AppUrl" DataNavigateUrlFields="A_AppUrl" Target="_blank" DataNavigateUrlFormatString="{0}" />
                </Columns>
            </asp:GridView>
            
            <FrameWorkWebControls:AspNetPager id="Pager" runat="server" OnPageChanged="Pager_PageChanged" ></FrameWorkWebControls:AspNetPager>
           
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>