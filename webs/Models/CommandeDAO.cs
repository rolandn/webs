using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using webs.classesMetier;
using System.Data;

namespace webs.Models
{
    public class CommandeDAO : BaseDAO<Commande>
    {
        public CommandeDAO(SqlConnection sqlConn) : base(sqlConn)
        {

        }

        public override bool Ajouter(Commande obj)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "select max(idCommande) + 1 " +
                "from commande";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
                if (sqlReader[0] == DBNull.Value)
                    obj.IdCmd = 1;
                else
                    obj.IdCmd = Convert.ToInt32(sqlReader[0]);
                sqlReader.Close();
                sqlCmd.CommandText =
                "insert into commande(idCommande,jour,heure,idClient,montant,livre ) " +
               "values(@idCommande,@jour,@heure,@idClient,@montant,@livre)";
                sqlCmd.Parameters.Add("@idCommande", SqlDbType.Int).Value = obj.IdCmd;
                sqlCmd.Parameters.Add("@jour", SqlDbType.Char).Value = obj.DateCmd.ToString("dd-MM-yyyy");
                sqlCmd.Parameters.Add("@heure", SqlDbType.Char).Value = obj.HeureCmd.ToString("hh:mm:ss");
                sqlCmd.Parameters.Add("@montant", SqlDbType.Decimal).Value = obj.Montant;
                sqlCmd.Parameters.Add("@livre", SqlDbType.Char).Value = obj.Livre;
                sqlCmd.Parameters.Add("@idClient", SqlDbType.Int).Value = obj.NumClient;
                
                return (sqlCmd.ExecuteNonQuery() == 0) ? false : true;
            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }
        }

        public override Commande Charger(int num)
        {
            Commande commande = null;

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "select idCommande,jour,heure,idCLient,montant,livre " +
            "from commande " +
            "where idCommande = @idCommande";
            sqlCmd.Connection = SqlConn;
            sqlCmd.Parameters.Add("@idCommande", SqlDbType.Int).Value = num;
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.Read() == true)
                commande = new Commande(
                   Convert.ToInt32(sqlReader["idCommande"]),
                   Convert.ToDateTime(sqlReader["jour"]),
                   Convert.ToDateTime(sqlReader["heure"]),
                   Convert.ToInt32(sqlReader["montant"]),
                   Convert.ToString(sqlReader["livre"]),
                   Convert.ToInt32(sqlReader["idClient"]));
            sqlReader.Close();
            return commande;
        }

        public override List<Commande> ListerTous()
        {
            return null;
        }

        public override bool Modifier(Commande obj)
        {
            return false;
        }

        public override bool Supprimer(int num)
        {
            return false;
        }
        public int GénérerCmdID()
        {
            try
            {
                int retVal = 0;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "select max(idCommande)+1 from commande";
                sqlCmd.Connection = SqlConn;
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.Read())
                    retVal = Convert.ToInt32(reader[0]);
                reader.Close();

                return retVal;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccessDB(ex.Message);
            }
        }

        public List<Commande> chargerCmd(int num)
        {
            List<Commande> liste = new List<Commande>();
            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select idCommande,jour,heure,montant,livre,idClient " +
                "from commande " +
                "order by idCommande asc";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read() == true)
                    liste.Add(new Commande(
                       Convert.ToInt32(sqlReader["idCommande"]),
                       Convert.ToDateTime(sqlReader["jour"]),
                       Convert.ToDateTime(sqlReader["heure"]),
                       Convert.ToInt32(sqlReader["montant"]),
                       Convert.ToString(sqlReader["livre"]),
                       Convert.ToInt32(sqlReader["idClient"])));
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