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
    public class ListeAlcoolDAO : BaseDAO<ListeAlcool>
    {
        public ListeAlcoolDAO(SqlConnection sqlConn) : base(sqlConn)
        {

        }
    

        public override bool Ajouter(ListeAlcool obj)
        {
            throw new NotImplementedException();
        }

        public override ListeAlcool Charger(int num)
        {
            throw new NotImplementedException();
        }

        public override bool Modifier(ListeAlcool obj)
        {
            throw new NotImplementedException();
        }

        public override bool Supprimer(int num)
        {
            throw new NotImplementedException();
        }

        public override List<ListeAlcool> ListerTous()
        {
            List<ListeAlcool> liste = new List<ListeAlcool>();
            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select alcool.NumArticle,DegreAlcool,Gout,Provenance,nom,nomImage,prix,quantiteStock " +
                "from produit inner join alcool on alcool.NumArticle=produit.NumArticle " +
                "order by NumArticle asc";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read() == true)
                    liste.Add(new ListeAlcool(
                       Convert.ToInt32(sqlReader["NumArticle"]),
                       Convert.ToString(sqlReader["Gout"]),
                       Convert.ToInt32(sqlReader["DegreAlcool"]),
                       Convert.ToString(sqlReader["Provenance"]),
                       Convert.ToString(sqlReader["nom"]),
                       Convert.ToString(sqlReader["nomImage"]),
                       Convert.ToDecimal(sqlReader["prix"]),
                       Convert.ToInt32(sqlReader["quantiteStock"])));
                sqlReader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }

            return liste;
        }

    }
}