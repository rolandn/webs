using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webs.classesMetier;
using webs.Models;

namespace webs.webforms
{
    public partial class ListerAlcool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("CPHContenu");

                List<ListeAlcool> liste = new List<ListeAlcool>();
                liste = ((FabriqueDAO)Session["FabriqueDAO"]).getInstListeAlcool().ListerTous();
                if (liste.Count == 0)
                {
                    new Tools().RedirigerMessage(
                    "Il n'y a aucune bouteille dans la base de données!");
                    return;
                }
                while (liste.Count > 0)
                {
                    cph.Controls.Add(new LiteralControl(
                                string.Format(MotifHTML,
                                liste[0].Gout,          //0
                                liste[0].DegréAlcool,   //1
                                liste[0].Provenance,    //2    
                                liste[0].Nom,           //3
                                liste[0].Image,         //4
                                liste[0].PrixU,        //5
                                liste[0].IDBout,
                                liste[0].QtéStock)));

                    liste.RemoveAt(0);
                }

            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("ListerAlcools", "Page_load()",
                ex.Message, "Problème de base de données lors du listage des alcools!");
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("ListerAlcool", "Page_load()",
                ex.Message,
                "Problème inattendu lors du listage des alcools!");
            }
        }
    
    }
}