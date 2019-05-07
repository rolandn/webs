<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="panier_bis.aspx.cs" Inherits="webs.webforms.panier_bis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContenu" runat="server">
    <h1>Mon panier</h1>
    <form id="fGVPanier" runat="server">

         <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"
                PageSize="10" AllowPaging="true" >        
        </asp:GridView>
         <br />
            <asp:TextBox ID="TBTotal" MaxLength="10" runat="server" ReadOnly="true"
                CssClass="saisie-texte" Style="width: 85px;" required="required">
            </asp:TextBox>
            <br />
             <asp:Button ID="BCommander" runat="server" Text="Commander"
                OnClick="BCommander_Click" CssClass="bouton" Height="28px" Width="111px" />
     </form>
</asp:Content>
