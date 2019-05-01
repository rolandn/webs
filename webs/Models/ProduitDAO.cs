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
            return false;
        }

        public override List<Produit> ListerTous()
        {
           
            List<Produit> liste = new List<Produit>();
           try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select NumArticle,nom,quantiteStock,prix,nomImage " +
                "from produit where Active = 1 " +
                "order by NumArticle asc";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read() == true)
                    liste.Add(new Produit(
                       Convert.ToInt32(sqlReader["NumArticle"]),
                       Convert.ToString(sqlReader["nom"]),
                       Convert.ToInt32(sqlReader["quantiteStock"]),
                       Convert.ToDecimal(sqlReader["prix"]),
                       Convert.ToString(sqlReader["nomImage"]),
                       Convert.ToBoolean(sqlReader["Active"])));
                sqlReader.Close();

            }
            
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }

            return liste;
        }

        //        public override List<Produit> ListerTous()
        //        {
        //            return null;
        //        }

        public override Produit Charger(int NumArticle)
        {
            Produit produit = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select NumArticle,nom,nomImage,prix,quantiteStock,Active " +
                "from produit " +
                "where NumArticle = @NumArticle";
                sqlCmd.Connection = SqlConn;
                sqlCmd.Parameters.Add("@NumArticle", SqlDbType.Int).Value = NumArticle;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                if (sqlReader.Read() == true)
                    produit = new Produit(
                       Convert.ToInt32(sqlReader["NumArticle"]),
                       Convert.ToString(sqlReader["nom"]),
                       Convert.ToInt32(sqlReader["quantiteStock"]),
                       Convert.ToDecimal(sqlReader["prix"]),
                       Convert.ToString(sqlReader["nomImage"]),
                       Convert.ToBoolean(sqlReader["Active"]));
                sqlReader.Close();
                return produit;

            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }
           

        }

        public override bool Modifier(Produit obj)
        {
            return false;
        }

        /*
         * modifier la quantité en stock du produit
         * */
        public bool ModifierStock(Produit obj, int qte)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "ajusterStock";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Connection = SqlConn;

            sqlCmd.Parameters.Add("@numProduit", SqlDbType.Int).Value = obj.numArticle;
            sqlCmd.Parameters.Add("@quantiteStock", SqlDbType.Int).Value = qte;
            sqlCmd.Parameters.Add("RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;

            return (sqlCmd.ExecuteNonQuery() == 0) ? false : true;
        }

        public override bool Supprimer(int num)
        {
            return false;
        }
    }

}