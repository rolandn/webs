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
    public partial class ListerLigneCom : System.Web.UI.Page
    {

        static string MotifHTML;

        static ListerLigneCom()
        {
            MotifHTML = Tools.ChargerMotifHTML("motifLigneCom.html");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("CPHContenu");

                List<Ligne_Cmd> liste = new List<Ligne_Cmd>();
                liste = ((FabriqueDAO)Session["FabriqueDAO"]).getInstLigne_CmdDAO().ListerTous();
                if (liste.Count == 0)
                {
                    new Tools().RedirigerMessage(
                    "Le panier est vide !");
                    return;
                }
                for (int i = 0; i < liste.Count; i++)
                {
                    cph.Controls.Add(new LiteralControl(
                                string.Format(MotifHTML,
                                liste[i].Id,              //0
                                liste[i].NumCmd,       //1
                                liste[i].NumArticle,        //2    
                                liste[i].Qte)));               //3
                }

            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("ListerAlcools", "Page_load()",
                ex.Message, "Problème de base de données lors du listage des lignes du panier !");
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("ListerLigneCom", "Page_load()",
                ex.Message,
                "Problème inattendu lors du listage des lignes de commande !");
            }
        }

    }

        
    
}