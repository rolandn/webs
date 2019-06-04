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
    public class Ligne_CmdDAO : BaseDAO<Ligne_Cmd>
    {
        public Ligne_CmdDAO(SqlConnection sqlConn) : base(sqlConn)
        {

        }

        public override bool Ajouter(Ligne_Cmd obj)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "select max(id) + 1 " +
                "from Ligne_Cmd";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
                if (sqlReader[0] == DBNull.Value)
                    obj.Id = 1;
                else
                    obj.Id = Convert.ToInt32(sqlReader[0]);
                sqlReader.Close();
                sqlCmd.CommandText =
                "insert into Ligne_Cmd(id,numCmd,NumArticle,Qte) " +
               "values(@id,@numCmd,@NumArticle,@qte)";
                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = obj.Id;
                sqlCmd.Parameters.Add("@numCmd", SqlDbType.Int).Value = obj.NumCmd;
                sqlCmd.Parameters.Add("@NumArticle", SqlDbType.Int).Value = obj.NumArticle;
                sqlCmd.Parameters.Add("@qte", SqlDbType.Int).Value = obj.Qte;

                return (sqlCmd.ExecuteNonQuery() == 0) ? false : true;
            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }
        }

        public override Ligne_Cmd Charger(int num)
        {
            return null;
        }

        public override List<Ligne_Cmd> ListerTous()
        {
            List<Ligne_Cmd> liste = new List<Ligne_Cmd>();
            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select id,numCmd,NumArticle,Qte " +
                "from Ligne_Cmd " +
                "order by id asc";
                sqlCmd.Connection = SqlConn;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read() == true)
                    liste.Add(new Ligne_Cmd(
                       Convert.ToInt32(sqlReader["id"]),
                       Convert.ToInt32(sqlReader["numCmd"]),
                       Convert.ToInt32(sqlReader["NumArticle"]),
                       Convert.ToInt32(sqlReader["Qte"])));
                sqlReader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }

            return liste;
        }

        public override bool Modifier(Ligne_Cmd obj)
        {
            return false;
        }

        public override bool Supprimer(int num)
        {
            return false;
        }
    }
}
