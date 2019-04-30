using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webs.classesMetier;
using webs.Models;


namespace webs.webforms
{
    public partial class Panier : System.Web.UI.Page
    {

        DataTable dt;
        Decimal prixTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            try
            {
                // on récupère NumArticle = {6}
                int NumArticle = Convert.ToInt32(Request.QueryString["NumArticle"]);
                Produit produit = ((FabriqueDAO)Session["FabriqueDAO"]).getInstProduitDAO().Charger(NumArticle);

                // on met la variable session["Panier"]  dans une liste de produit         
                List<Produit> liste = (List<Produit>)Session["Panier"];

                if (liste == null)
                {
                    Session["Panier"] = liste = new List<Produit>();
                    liste.Add(produit);
                }
                else
                {
                    bool found = false;
                    foreach (Produit p in liste)
                    {
                        if (p.numArticle == NumArticle)
                        {
                            p.Quantite++;
                            found = true;

                            break;
                        }
                    }
                    if (!found)
                    {
                        liste.Add(produit);
                    }
                }
                // calcul du prix total
                foreach (Produit p in liste)
                {
                    prixTotal += p.Quantite * p.Prix;
                    if (prixTotal >= 150)
                    {
                        p.Quantite--;
                        if (p.Quantite == 0)
                        {
                            liste.Remove(p);
                        }
                        new Tools().RedirigerMessage("Votre commande ne peut pas excéder 150€");
                        return;
                    }

                }
                // afficher le prix dans le textBox
                TBTotal.Text = Convert.ToString(prixTotal) + " €";

                //on crée la table et on y met les produits
                DataTable dt = CreateTableStructure();

                DataRow row;
                foreach (Produit p in liste)
                {
                    // on crée la row du tableau
                    row = dt.NewRow();
                    row["Produit #"] = p.numArticle;
                    row["Nom Produit"] = p.Nom;
                    row["Qté"] = p.Quantite;
                    row["Prix Unitaire"] = p.Prix;
                    row["Prix Total"] = p.Quantite * p.Prix;

                    dt.Rows.Add(row);
                }
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();

            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("ChargerProduit", "Page_load()",
                ex.Message, "Problème de base de données lors du listage des alcools!");
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("ChargerProduit", "Page_load()",
                ex.Message,
                "Problème inattendu lors du chargement du panier!");
            }
        }
        // créer la structure du tableau
        private DataTable CreateTableStructure()
        {
            dt = new DataTable();

            dt.Columns.Add(CreateColumn("Produit #", System.Type.GetType("System.Int32")));
            dt.Columns.Add(CreateColumn("Nom Produit", Type.GetType("System.String")));
            dt.Columns.Add(CreateColumn("Qte", Type.GetType("System.Int32")));
            dt.Columns.Add(CreateColumn("Prix Unitaire", Type.GetType("System.Decimal")));
            dt.Columns.Add(CreateColumn("Prix Total", Type.GetType("System.Decimal")));

            return dt;
        }

        private DataColumn CreateColumn(string name, Type type)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = name;
            dc.DataType = type;
            return dc;
        }

        /* bouton pour commander
         * prend la liste du panier et l'envoie en DB avant date/heure/etc 
         * Aller chercher le 
         * numClient ---- ok
         * le montant_cmd --- ok
         * ID_cmd --- ok
         * Date --- ok
         * Time --- ok
         * Quantité --- ok
         */

        protected void BCommander_Click(object sender, EventArgs e)
        {
            Client clt = new Client();

            // date et heure dans la commande
            DateTime date = new DateTime();
            date = DateTime.Now;
            String dateCmd = date.ToShortDateString();
            String hourCmd = date.ToShortTimeString();
            // état de livraison
            string livré = "Non";
            // num du client
            int numClt = (int)Session["user"];

            // on récupère le panier de la Session
            List<Produit> liste = new List<Produit>();
            liste = (List<Produit>)Session["Panier"];

            try
            {
                // on doit récupérer l'id du clt           
                clt = ((FabriqueDAO)Session["FabriqueDAO"]).getInstClientDAO().Charger(numClt);
                // générere un num de commande
                int idcmd = ((FabriqueDAO)Session["FabriqueDAO"]).getInstCommandeDAO().GénérerCmdID();

                foreach (Produit p in liste)
                {
                    prixTotal += p.Quantite * p.Prix;
                }

                // on crée la commande et on envoie en DB
                Commande cmd = new Commande(idcmd, dateCmd, hourCmd, prixTotal, livré, numClt);
                if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstCommandeDAO().Ajouter(cmd)) == false)
                    throw new Exception("problème lors de l'ajout de la commande");

                // on crée une ligne commande pour chaque produit
                foreach (Produit p in liste)
                {
                    Ligne_Cmd l_cmd = new Ligne_Cmd(idcmd, p.numArticle, p.Quantite);
                    if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstLigne_CmdDAO().Ajouter(l_cmd)) == false)
                        throw new Exception("problème lors de l'ajout de la ligne commande");
                    if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstProduitDAO().ModifierStock(p, p.Quantite)) == false)
                        throw new Exception("problème lors d'ajustement du stock");
                }

                // on vide la variable session
                Session.Clear();
                // si tout va bien on revoit une page de confirmation
                new Tools().RedirigerMessage("Votre commande a bien été effectuée. En vous remerciant");
            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("Problème de connexion", "Bcontinuer_Click()",
                ex.Message,
                "Problème de base de données lors du chargement de la commande!");
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("AjouterCommande", "Bcontinuer_Click()",
                ex.Message,
                "Problème inattendu lors de l'ajout de la commande!");
            }

        }
    }
}