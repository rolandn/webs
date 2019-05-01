<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="Panier.aspx.cs" Inherits="webs.webforms.Panier" %>





<asp:Content ID="Content4" ContentPlaceHolderID="CPHContenu" runat="server">

    <h1>Mon panier</h1>

    <form id="Panier1" runat="server">

         <asp:GridView ID="GridView1" runat="server" CssClass="gridview"
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


