using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webs.classesMetier;
using webs.Models;

namespace webs.webforms
{
    public partial class ListerPanier : System.Web.UI.Page
    {
        static string MotifHTML;

        List<KeyValuePair<Produit, int>> listProduitPanier;
        private decimal montantTotal;

        /**
        * Constructeur statique. Il n'est exécuté qu'une seule fois. Il charge le motif HTML
        */
        static ListerPanier()
        {
            MotifHTML = Tools.ChargerMotifHTML("motifproduit.html");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null || (int)Session["userId"] == 0)
                new Tools().RedirigerMessage("Vous ne pouvez accdéder à cette page sans vous connecter !");

            try
            {
                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("CPHContenu");
                List<Produit> produits = ((FabriqueDAO)(Session["FabriqueDAO"])).getInstProduitDAO().ListerTous();

                var panierSession = (List<int>)(Session["panier"]);
                if (panierSession == null)
                    return;

                listProduitPanier = getKVPProduitPanier(panierSession, produits);
                if (listProduitPanier.Count == 0)
                    return;

                montantTotal = getMontantTotale();

                // afficher total
                Label lbTotal = new Label();
                lbTotal.CssClass = "customTotal";
                lbTotal.Text = "Total panier : " + getMontantTotale().ToString() + "€";
                cph.Controls.Add(lbTotal);

                //pour chaque produit du panier, l'afficher
                foreach (var kvp in listProduitPanier)
                {
                    Produit p = kvp.Key;
                    int nombre = kvp.Value;

                  //  string imgDir = (p is Chemise) ? "../../imgs/imgschemise/" : "../../imgs/imgsalcool/";
                    cph.Controls.Add((new LiteralControl(string.Format(MotifHTML,
                      //  imgDir + p.NomImage,
                        p.Nom,
                        p.Prix,
                        nombre
                        ))));
                }

                // ajout du bouton commander
                Button BCommander = new Button();

                BCommander.CssClass = "bouton";
                BCommander.Text = "Commander";
                BCommander.Style["padding"] = "20px";
                BCommander.Click += new EventHandler(click_BCommander);

                cph.Controls.Add(BCommander);

                Button BSupprimerPanier = new Button();

                BSupprimerPanier.CssClass = "bouton";
                BSupprimerPanier.Text = "Supprimer Panier";
                BSupprimerPanier.Style["padding"] = "20px";
                BSupprimerPanier.Click += new EventHandler(click_SupprimerPanier);

                cph.Controls.Add(BSupprimerPanier);


            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("ListerPanier", "Page_load()", ex.Message,
                    "Problème de base de données lors du listage du panier");
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("ListerAlcools", "Page_load()", ex.Message,
                    "Problème inattendu  lors du listage du panier!");
            }


        }

        private void click_SupprimerPanier(object sender, EventArgs e)
        {
            ((List<int>)(Session["panier"])).Clear();
            new Tools().Rediriger("/Membre/ListerPanier.aspx");
        }


        // retoure le panier sous forme de liste kvp avec l'objet produit au lieu de l'idProduit 
        private List<KeyValuePair<Produit, int>> getKVPProduitPanier(List<int> panierSession, List<Produit> produits)
        {
            var panierKVP = panierSession.GroupBy(i => i);
            var list = new List<KeyValuePair<Produit, int>>();

            foreach (var paire in panierKVP)
            {
                foreach (var produit in produits)
                {
                    if (produit.numArticle == paire.Key)
                    {
                        list.Add(new KeyValuePair<Produit, int>(produit, paire.Count()));
                        break;
                    }
                }
            }

            return list;

        }


        private void click_BCommander(Object sender, EventArgs e)
        {
            //test si panier < 150
            if (getMontantTotale() > 150)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('le panier ne doit pas dépasser 150€ !');", true);
                return;
            }

            // creer la commande
            Commande commande = new Commande();
            commande.DateCmd = DateTime.Now;
            commande.HeureCmd = DateTime.Now.ToString("HH'h'mm");
            commande.Livre = false;
            commande.Montant = this.montantTotal;
            commande.NumClient = (int)(Session["userId"]);

            // récupérer l'ID de la commande
            int idCommande = ((FabriqueDAO)(Session["FabriqueDAO"])).getCommandeDAO().AjouterBis(commande);

            // Ajouter les produits à la commande via l'objet Ajout
            foreach (var kvpProduit in this.listProduitPanier)
            {
                Ajout ajout = new Ajout();
                ajout.IdCommande = idCommande;
                ajout.IdProduit = kvpProduit.Key.;
                ajout.Quantite = kvpProduit.Value;
                ((FabriqueDAO)(Session["FabriqueDAO"])).getAjoutDAO().Ajouter(ajout);
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Commande enregistrée !')", true);
            new Tools().Rediriger("/membre/PageDebut.aspx");
        }

        private double getMontantTotale()
        {
            double total = 0;
            foreach (var kvp in this.listProduitPanier)
                total += kvp.Key.Prix * kvp.Value;

            return total;
        }
    }
}
}