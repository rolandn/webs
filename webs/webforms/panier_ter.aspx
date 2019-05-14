<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="panier_ter.aspx.cs" Inherits="webs.webforms.panier_ter" %>

<asp:Content ID="Content5" ContentPlaceHolderID="CPHContenu" runat="server">
    <h1>Ajouter un article au panier</h1>

    <form id="Panier1" runat="server">

         <asp:GridView ID="GridView1" runat="server" CssClass="gridview"
                PageSize="10" AllowPaging="true" >        
        </asp:GridView>
         <br />
           

         <br />
        <asp:TextBox ID="TBQtite" MaxLength="10" runat="server" ReadOnly="true"
                CssClass="saisie-texte" Style="width: 85px;" required="required">
            </asp:TextBox>
            
        
        <br />
         <asp:Button ID="BAjouter_panier" runat="server" Text="Commander"
                OnClick="BCommander_clic" CssClass="bouton" Height="28px" Width="111px" /> 
     </form>   
</asp:Content>
