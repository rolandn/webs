using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;
using System.Data.SqlClient;

namespace webs.Models
{
    public class FabriqueDAO
    {
        private SqlConnection SqlConn = null;
        /**
        * Constructeur : il crée une connexion initiale avec la base de données
        */
        public FabriqueDAO()
        {
            try
            {
                if (SqlConn == null)
                {
                    SqlConn = new SqlConnection("Integrated security=false;" +
                   "user id=genial;" +
                   "password=super;" +
                   "Data Source=ROLAND-PC\\SQLEXPRESS;" +
                   "Initial Catalog=WEBSHOP;");
                    SqlConn.Open();
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccessDB(e.Message);
            }
        }
        /**
        * méthode qui fournit une instance de ListeAlcool
        * retour : l'instance de LIsteAlcool
        */
        public ListeAlcoolDAO GetListeAlcoolDAO()
        {
            return new ListeAlcoolDAO(SqlConn);
        }

        public ClientDAO getInstClientDAO()
        {
            return new ClientDAO(SqlConn);
        }

        public Ligne_CmdDAO getInstLigne_CmdDAO()
        {
            return new Ligne_CmdDAO(SqlConn);
        }

        public ProduitDAO getInstProduitDAO()
        {
            return new ProduitDAO(SqlConn);
        }

        public CommandeDAO getInstCommandeDAO()
        {
            return new CommandeDAO(SqlConn);
        }

    }
}