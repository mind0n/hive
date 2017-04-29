<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.FieldManager._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        ButtonList-Capacity="4" HeadOPTxt="应用字段列表" HeadTitleTxt="应用字段设定">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonPopedom="New" ButtonUrlType="Href"
            ButtonVisible="True" ButtonName="应用字段" ButtonUrl="FieldvalueManager.aspx?CMD=New" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="应用字段列表">
            <asp:GridView ID="GridView1" runat="server">
                    <Columns>
                        <asp:HyperLinkField HeaderText="应用字段Key" DataTextField="F_Key" DataNavigateUrlFields="FieldID" DataNavigateUrlFormatString="FieldValueManager.aspx?FieldID={0}&CMD=List" />
                        <asp:BoundField HeaderText="应用字段名称" DataField="F_CName"/>
                        <asp:TemplateField HeaderText="字段值数">
                            <ItemTemplate>
                                <%#GetSubCount((string)Eval("F_Key")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
            </FrameWorkWebControls:AspNetPager>    
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
