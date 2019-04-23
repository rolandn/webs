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

        public override Produit Charger(int num)
        {
            Produit prod = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "chargerProduit";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection = SqlConn;
                sqlCmd.Parameters.Add("@numProduit", SqlDbType.Int).Value = num;
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.Read())

                    prod = new Produit(Convert.ToInt32(reader["NumArticle"]),
                                    Convert.ToString(reader["nom"]),
                                    Convert.ToInt32(reader["quantiteStock"]),
                                    Convert.ToDecimal(reader["prix"]),
                                    Convert.ToString(reader["nomImage"])                     
                                    );
                reader.Close();

            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }
            return prod;

        }

        public override List<Produit> ListerTous()
        {
            return null;
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