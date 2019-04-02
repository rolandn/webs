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
    public partial class Accueil : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /**
            * méthode exécutée à chaque chargement d'une nouvelle page.
            * La fabrique d'objets DAO est créée si la session est utilisée ...
            * ... pour la 1re fois ou si elle a expiré
            */
            try
            {
                if (Session["FabriqueDAO"] == null)
                {
                    Session["FabriqueDAO"] = new FabriqueDAO();

                    Session.Timeout = 5;
                }
            }
            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurConnSQL("Aspect", "Page_Init()", ex.Message);
            }
        }
    }
}