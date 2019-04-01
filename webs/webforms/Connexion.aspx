
<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="webs.webforms.Connexion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContenu" runat="server">
   
    
          

            <div class="elementecran" style="text-align: center;">
                <br />
                Email:
                <asp:TextBox ID="TBEmail" MaxLength="50" runat="server" TextMode="Email"
                    CssClass="saisie-texte" Style="width: 175px;" required="required">
                </asp:TextBox>
                <br />
                <br />
                Password:
                <asp:TextBox ID="TBPassword" MaxLength="20" runat="server" TextMode="Password"
                    CssClass="saisie-texte" Style="width: 175px;" required="required">
                </asp:TextBox>
                <br />
                <br />
                <asp:Label ID="LReponse" runat="server" Text=""></asp:Label>
        
                <br />
                <asp:Button ID="BContinuer" runat="server" Text="Se connecter"
                OnClick="BContinuer_Click" CssClass="bouton" Height="28px" Width="111px" />
        
                <button type="button" class="bouton" onclick="location.reload();" >
                Réinitialiser</button>
        
                <br />
                <br />
            </div>
       
</asp:Content>
