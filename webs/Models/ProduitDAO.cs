using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using webs.classesMetier;
using System.Data.SqlClient;
using webs.Models;

namespace webs.Models
{
   
        public class ProduitDAO : BaseDAO<Produit>
        {
            public ProduitDAO(SqlConnection sqlConn) : base(sqlConn)
            {
            }

            public override bool Ajouter(Produit obj)
            {
                throw new NotImplementedException();
            }

            public override Produit Charger(int num)
            {
                Produit produit = null;

                try
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText =
                    "select * " +
                   "from produit, biere, alcool " +
                   "where NumArticle = @num";
                    sqlCmd.Connection = SqlConn;
                    sqlCmd.Parameters.Add("@num", SqlDbType.Int).Value = num;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read() == true)
                        produit = new Produit(Convert.ToInt32(sqlReader["NumArticle"]),
                                              Convert.ToString(sqlReader["nom"]),
                                              Convert.ToInt32(sqlReader["quantiteStock"]),
                                              Convert.ToDouble(sqlReader["prix"]),
                                              Convert.ToString(sqlReader["nomImage"])
                       );
                    sqlReader.Close();
                    return produit;
                }
                catch (Exception e)
                {
                    throw new ExceptionAccessDB(e.Message);
                }
            }

            public override List<Produit> ListerTous()
            {
                List<Produit> liste = new List<Produit>();

                BiereDAO chemiseDAO = new BiereDAO(SqlConn);
                AlcoolDAO alcoolDAO = new AlcoolDAO(SqlConn);

                liste.AddRange(BiereDAO.ListerTous());
                liste.AddRange(alcoolDAO.ListerTous());

                return liste;
            }

            public List<KeyValuePair<Produit, int>> ListerProduitCommande(int idCommande)
            {
                // la liste kvp avec id des produits de la commande et leur nombre associé
                List<KeyValuePair<int, int>> produitsId = new List<KeyValuePair<int, int>>();

                // tout les produits
                List<Produit> allProduits = new List<Produit>();

                // la liste kvp finale à retourner
                List<KeyValuePair<Produit, int>> produits = new List<KeyValuePair<Produit, int>>();

                try
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = this.SqlConn;

                    sqlcmd.CommandText = "select Produit.idProduit, Ajout.quantite  " +
                                          "from Produit, Ajout, Commande " +
                                          "where Produit.idProduit = Ajout.idProduit " +
                                          "and Ajout.idCommande = Commande.idCommande " +
                                          "and Commande.idCommande = @idCommande ";
                    sqlcmd.Parameters.Add("@idCommande", SqlDbType.Int).Value = idCommande;

                    SqlDataReader sqlReader = sqlcmd.ExecuteReader();

                    while (sqlReader.Read())
                        produitsId.Add(new KeyValuePair<int, int>((Convert.ToInt32(sqlReader[0])), Convert.ToInt32(sqlReader[1])));

                    sqlReader.Close();

                    allProduits = this.ListerTous();

                    foreach (var p in allProduits)
                        foreach (var kvp in produitsId)
                            if (p.IdProduit == kvp.Key)
                                produits.Add(new KeyValuePair<Produit, int>(p, kvp.Value));

                }
                catch (Exception e)
                {
                    throw new ExceptionAccesBD(e.Message);
                }

                return produits;
            }

            public override bool Modifier(Produit obj)
            {
                throw new NotImplementedException();
            }

            public override bool Supprimer(int num)
            {
                throw new NotImplementedException();
            }
        
    }