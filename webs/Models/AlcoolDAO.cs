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

            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select alcool.NumArticle,DegreAlcool,Gout,Provenance,nom,nomImage,prix,quantiteStock " +
                "from produit inner join alcool on alcool.NumArticle=produit.NumArticle " +
                "order by NumArticle asc";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read() == true)
                    liste.Add(new Alcool(
                       Convert.ToInt32(sqlReader["NumArticle"]),
                       Convert.ToInt32(sqlReader["DegreAlcool"]),
                       Convert.ToString(sqlReader["Gout"]),
                       Convert.ToString(sqlReader["Provenance"])));
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