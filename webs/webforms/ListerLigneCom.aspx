<%@ Page Title="" Language="C#" MasterPageFile="~/webforms/Aspect.Master" AutoEventWireup="true" CodeBehind="ListerLigneCom.aspx.cs" Inherits="webs.webforms.ListerLigneCom" %>

<asp:Content ID="Content5" ContentPlaceHolderID="CPHContenu" runat="server">
    <h1>Ajouter un article au panier</h1>

    <form id="Panier1" runat="server">

         <asp:GridView ID="GridView1" runat="server" CssClass="gridview"
                PageSize="10" AllowPaging="true" >        
        </asp:GridView>
         <br />
           

         
     </form>   
</asp:Content>
