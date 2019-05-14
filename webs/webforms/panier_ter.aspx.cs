using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webs.classesMetier;
using webs.Models;
using System.IO;
using System.Data;

namespace webs.webforms
{
    public partial class panier_ter : System.Web.UI.Page
    {

        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;

        }

        // créer la structure du tableau
        private DataTable CreateTableStructure()
        {
            dt = new DataTable();

            dt.Columns.Add(CreateColumn("Num Produit", System.Type.GetType("System.Int32")));
            dt.Columns.Add(CreateColumn("Nom Produit", Type.GetType("System.String")));
            dt.Columns.Add(CreateColumn("Qte", Type.GetType("System.Int32")));
            dt.Columns.Add(CreateColumn("Prix Unitaire", Type.GetType("System.Decimal")));
            dt.Columns.Add(CreateColumn("Prix Total", Type.GetType("System.Decimal")));

            return dt;
        }

        private DataColumn CreateColumn(string name, Type type)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = name;
            dc.DataType = type;
            return dc;
        }


        

        protected void BAjouter(object sender, EventArgs e)
        {
            if (Session["panier"] == null)
            {
                new Tools().RedirigerMessage("Session expirée");
            }

            try
            {
                Ligne_Cmd ligne_Cmd = (Ligne_Cmd)Session["ligne_Cmd"];

                ligne_Cmd.Qte = Convert.ToInt32(TBQtite.Text);
                
            }

            catch
            {

            }

            
        }

        protected void BCommander_clic(object sender, EventArgs e)
        {

        }
    }
}