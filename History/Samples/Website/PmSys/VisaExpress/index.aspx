<%@ Page Title="" Language="C#" MasterPageFile="~/indexHead.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="secondaryContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
   <div class="tripProject">
   <div class="firstProject">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/China.jpg" />
    </div>
    <div class="firstProject">
    <asp:Image ID="Image2" runat="server" ImageUrl="~/Image/Canada.jpg" />
    </div>
    <div class="firstProject">
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/American.jpg" />
    </div>
    <div class="firstProject">
    <asp:Image ID="Image4" runat="server" ImageUrl="~/Image/India.jpg" />
    </div>
    
    </div>
    
</asp:Content>

