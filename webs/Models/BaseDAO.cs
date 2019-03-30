using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;
using System.Data.SqlClient;

namespace webs.Models
{
    public abstract class BaseDAO<T>
    {
        protected SqlConnection SqlConn = null;
        /**
        * Constructeur
        */
        public BaseDAO(SqlConnection sqlConn)
        {
            SqlConn = sqlConn;
        }
        /**
        * Méthodes 
        */

     

        public virtual T Charger(int num)
        {
            return default(T);
        }
        public virtual bool Ajouter(T obj)
        {
            return false;
        }
        public virtual bool Modifier(T obj)
        {
            return false;
        }
        public virtual bool Supprimer(int num)
        {
            return false;
        }
        public virtual List<T> ListerTous()
        {
            return null;
        }
    }
}