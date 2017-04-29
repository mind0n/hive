<%@ Page Language="C#"  MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="PmSys.web.Module.PmSys.Projects.ProjectList"  %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="项目列表" HeadTitleTxt="项目列表管理">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="项目" ButtonPopedom="New" ButtonUrl="ProjectManager.aspx?CMD=New"
            ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="项目列表">
           
            <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                   <asp:BoundField HeaderText="项目No" DataField="ProjectNo"/>
                   <asp:HyperLinkField HeaderText="项目名称" DataTextField="ProjectName" DataNavigateUrlFields="ProjectId" DataNavigateUrlFormatString="ProjectManager.aspx?S_ID={0}&CMD=Look" />   
                   <asp:BoundField HeaderText="状态" DataField="ProjectStatus"/>
                   <asp:BoundField HeaderText="开始时间" DataField="ProjectStartTime"/>
                   <asp:BoundField HeaderText="经历时间" DataField="ProjectDuration"/>
                   <asp:BoundField HeaderText="简介" DataField="ProjectBrief"/>
                   <asp:BoundField HeaderText="备注" DataField="ProjectComments"/>
                   <asp:BoundField HeaderText="图标" DataField="ProjectIcon"/>
                 </Columns>
            </asp:GridView>
            
            <FrameWorkWebControls:AspNetPager id="Pager" runat="server" OnPageChanged="Pager_PageChanged" ></FrameWorkWebControls:AspNetPager>
           
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
