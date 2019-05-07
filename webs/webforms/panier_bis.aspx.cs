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
    public partial class panier_bis : System.Web.UI.Page
    {
        
            DataTable dt;
            decimal montant = 0;


            protected void Page_Load(object sender, EventArgs e)
            {
                if (IsPostBack)
                    return;
                try
                {
                    List<Produit> liste = (List<Produit>)Session["Panier"];

                    if (liste == null)
                    {
                        new Tools().RedirigerMessage("Le panier est vide");
                    }
                    else
                    {
                        // on charge la liste dans le Gdv +                       

                        //on crée la table et on y met les produits
                        DataTable dt = CreateTableStructure();

                        DataRow row;
                        foreach (Produit p in liste)
                        {
                            // on crée la row du tableau
                            row = dt.NewRow();
                            row["Produit #"] = p.numArticle;
                            row["Nom Produit"] = p.Nom;
                            row["Qté"] = p.qte;
                            row["Prix Unitaire"] = p.Prix;
                            row["Prix Total"] = p.qte * p.Prix;

                            dt.Rows.Add(row);
                        }
                        this.GridView1.DataSource = dt;
                        this.GridView1.DataBind();

                        foreach (Produit p in liste)
                        {
                            montant += p.qte * p.Prix;
                        }
                        // afficher le prix dans le textBox
                        TBTotal.Text = Convert.ToString(montant) + " €";

                    }

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
                dt.Columns.Add(CreateColumn("Qté", Type.GetType("System.Int32")));
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


            protected void BCommander_Click(object sender, EventArgs e)
            {
                Client clt = new Client();

                // date et heure dans la commande
                DateTime date = new DateTime();
                date = DateTime.Now;
                DateTime dateCmd = date;
                DateTime heureCmd = date;
                // état de livraison
                string livre = "Non";
                // num du client
                int numClient = (int)Session["user"];

                // on récupère le panier de la Session
                List<Produit> liste = new List<Produit>();
                liste = (List<Produit>)Session["Panier"];

                try
                {
                    // on doit récupérer l'id du clt           
                    clt = ((FabriqueDAO)Session["FabriqueDAO"]).getInstClientDAO().Charger(numClient);
                    // générere un num de commande
                    int idCmd = ((FabriqueDAO)Session["FabriqueDAO"]).getInstCommandeDAO().GénérerCmdID();

                    foreach (Produit p in liste)
                    {
                        montant += p.qte * p.Prix;
                    }

                    // on crée la commande et on envoie en DB
                    Commande cmd = new Commande(idCmd, dateCmd, heureCmd, montant, livre, numClient);
                    if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstCommandeDAO().Ajouter(cmd)) == false)
                        throw new Exception("problème lors de l'ajout de la commande");

                    // on crée une ligne commande pour chaque produit
                    foreach (Produit p in liste)
                    {
                        Ligne_Cmd l_cmd = new Ligne_Cmd(idCmd, p.numArticle, p.qte);
                        if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstLigne_CmdDAO().Ajouter(l_cmd)) == false)
                            throw new Exception("problème lors de l'ajout de la ligne commande");
                        if ((((FabriqueDAO)Session["FabriqueDAO"]).getInstProduitDAO().ModifierStock(p, p.qte)) == false)
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