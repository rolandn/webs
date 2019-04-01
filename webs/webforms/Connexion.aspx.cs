using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webs.Models;
using webs.classesMetier;

namespace webs.webforms
{
    public partial class Connexion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BContinuer_Click(object sender, EventArgs e) // bouton de connexion 
        {
            Client clt = null;
            string hashedTB = Tools.HashPassword(TBPassword.Text);
            try
            {
                clt = ((FabriqueDAO)Session["FabriqueDAO"]).getInstClientDAO().FindClient(TBEmail.Text);

                if (clt == null)
                    LReponse.Text = "Adresse Email inconnue.";

                else
                {
                    if (clt.Password == hashedTB)
                    {
                        Session["user"] = clt.ID_clt;
                        new Tools().Rediriger("Default.aspx");
                    }
                }

            }

            catch (ExceptionAccessDB ex)
            {
                new Tools().RedirigerErreurSQL("Problème de connexion", "Bcontinuer_Click()",
                ex.Message,
                "Problème de base de données lors de l'authentification!");
            }
            catch (ExceptionMetier ex)
            {
                new Tools().RedirigerMessage(ex.Message);
            }
            catch (Exception ex)
            {
                new Tools().RedirigerErreurGen("AjouterClient", "Bcontinuer_Click()",
                ex.Message,
                "Problème inattendu lors de l'ajout du client!");
            }
        }
    }
}