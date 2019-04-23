using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;
using System.Data.SqlClient;

namespace webs.Models
{
    public class AlcoolDAO : BaseDAO<Alcool>
    {
        public AlcoolDAO(SqlConnection sqlConn) : base(sqlConn)
        {
        }

        public override bool Ajouter(Alcool obj)
        {
            throw new NotImplementedException();
        }

        public override Alcool Charger(int num)
        {
            throw new NotImplementedException();
        }

        public override List<Alcool> ListerTous()
        {
            List<Alcool> liste = new List<Alcool>();

            SqlCommand sqlCmd = new SqlCommand();

            try
            {
                sqlCmd.CommandText = "SELECT Produit.idProduit, Produit.nom, Produit.quantiteStock, Produit.prix, Produit.nomImage, Alcool.degre, Alcool.gout, Alcool.provenance " +
                                     "FROM Produit, Alcool " +
                                     "WHERE Produit.idProduit = Alcool.idProduit";

                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read() == true)
                {
                    liste.Add(new Alcool(Convert.ToInt32(sqlReader["idProduit"]),
                                          Convert.ToString(sqlReader["nom"]),
                                          Convert.ToInt32(sqlReader["quantiteStock"]),
                                          Convert.ToDouble(sqlReader["prix"]),
                                          Convert.ToString(sqlReader["nomImage"]),
                                          Convert.ToDouble(sqlReader["degre"]),
                                          Convert.ToString(sqlReader["gout"]),
                                          Convert.ToString(sqlReader["provenance"]))
                                          );
                }
                sqlReader.Close();
            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }

            return liste;
        }

        public override bool Modifier(Alcool obj)
        {
            throw new NotImplementedException();
        }

        public override bool Supprimer(int num)
        {
            throw new NotImplementedException();
        }
    }
}