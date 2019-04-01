<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="pagemessage.aspx.cs" Inherits="webs.webforms.pagemessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContenu" runat="server">
    <h1>Information</h1>
    <p>
    <b><asp:Label ID="LMsg" runat="server" Text="Label" CssClass="msg"></asp:Label></b>
    </p>
    <p class ="msg">Cliquez sur le bouton back pour recommencer ou faites une sélection dans
    la barre des menus...
    </p>
</asp:Content>
