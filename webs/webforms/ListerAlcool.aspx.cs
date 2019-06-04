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

        static string MotifHTML;

        static ListerAlcool()
        {
            MotifHTML = Tools.ChargerMotifHTML("motifAlcool.html");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("CPHContenu");

                List<ListeAlcool> liste = new List<ListeAlcool>();
                liste = ((FabriqueDAO)Session["FabriqueDAO"]).GetListeAlcoolDAO().ListerTous();
                if (liste.Count == 0)
                {
                    new Tools().RedirigerMessage(
                    "Il n'y a aucun alcool dans la base de données!");
                    return;
                }
                while (liste.Count > 0)
                {
                    cph.Controls.Add(new LiteralControl(
                                string.Format(MotifHTML,
                                liste[0].gout,              //0
                                liste[0].degreAlcool,       //1
                                liste[0].provenance,        //2    
                                liste[0].nom,               //3
                                liste[0].nomImage,          //4
                                liste[0].prix,              //5
                                liste[0].numArticle,        //6
                                liste[0].quantiteStock)));  //7

                    liste.RemoveAt(0);
                }

            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("ListerLigneCmd", "Page_load()",
                ex.Message, "Problème de base de données lors du listage des lignes de commande !");
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