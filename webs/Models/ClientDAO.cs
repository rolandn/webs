using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using webs.classesMetier;
using System.Data;

namespace webs.Models
{
    public class ClientDAO:BaseDAO<Client>
    {
        public ClientDAO(SqlConnection sqlConn) : base(sqlConn)
        {

        }

        public Client FindClient(string email)
        {
            Client clt = null;

            try
            {
                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.CommandText = "select numClient,email,mdp from client where email = @email";

                sqlCmd.Connection = SqlConn;

                sqlCmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.Read())

                    clt = new Client(Convert.ToInt32(reader["numClient"]),
                                     Convert.ToString(reader["email"]),
                                     Convert.ToString(reader["mdp"]));

                reader.Close();

            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }

            return clt;
        }

    }
}