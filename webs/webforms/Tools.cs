using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using webs.classesMetier;
using webs.Models;

namespace webs.webforms
{
    public class Tools
    {
        // variable utilisée pour rendre les accès en écriture séquentiels dans le fichier de trace
        private static readonly object Verrou = new object();
        /**
        * méthode qui ajoute un message d'erreur détaillé relatif à un problème de base de données ...
        * ... dans un fichier de trace, qui crée une nouvelle connexion, ...
        * ... puis qui redirige vers pagemessage.aspx pour afficher le message d'erreur
        * @param classe : la classe dans le programme où est survenue l'erreur
        * @param methode : la méthode dans la classe où est survenue l'erreur
        * @param msgDetail : le message d'erreur détaillé
        * @param msgErreur : le message d'erreur à afficher
        */
        public void RedirigerErreurSQL(string classe, string methode, string msgDetail,
        string msgErreur)
        {
            EcrireFichierTrace(classe, methode, msgDetail);
            ReconnecterSQL();
            RedirigerMessage(msgErreur);
        }
        /**
        * méthode qui ajoute un message d'erreur relatif à un problème de connexion ...
        * ... à la base de données dans un fichier de trace et qui tente de créer ...
        * ... une nouvelle connexion
        * @param classe : la classe dans le programme où est survenue l'erreur
        * @param methode : la méthode dans la classe où est survenue l'erreur
        * @param msgDetail : le message d'erreur détaillé
*/
        public void RedirigerErreurConnSQL(string classe, string methode, string msgDetail)
        {
            EcrireFichierTrace(classe, methode, msgDetail);
            ReconnecterSQL();
        }
        /**
        * méthode qui ajoute un message d'erreur détaillé dans un fichier de trace ...
        * ... puis qui redirige vers pagemessage.aspx pour afficher le message d'erreur
        * @param classe : la classe dans le programme où est survenue l'erreur
        * @param methode : la méthode dans la classe où est survenue l'erreur
        * @param msgDetail : le message d'erreur détaillé
        * @param msgErreur : le message d'erreur à afficher
*/
        public void RedirigerErreurGen(string classe, string methode, string msgDetail,
        string msgErreur)
        {
            EcrireFichierTrace(classe, methode, msgDetail);
            RedirigerMessage(msgErreur);
        }

        /**
        * méthode qui ajoute un message d'erreur détaillé dans un fichier de trace ...
        * ... puis qui affiche le message d'erreur dans une page blanche
        * @param classe : la classe dans le programme où est survenue l'erreur
        * @param methode : la méthode dans la classe où est survenue l'erreur
        * @param msgDetail : le message d'erreur détaillé
        * @param msgErreur : le message d'erreur à afficher
        */
        public void EcrireTexteErr(string classe, string methode, string msgDetail, string msgErreur)
        {
            EcrireFichierTrace(classe, methode, msgDetail);
            HttpContext.Current.Response.Output.Write(msgErreur);
            HttpContext.Current.Response.End();
        }
        /**
        * Méthode qui redirige vers la page pagemessage.aspx pour afficher un message
        * @param msg : le message à afficher
*/
        public void RedirigerMessage(string msg)
        {
            HttpContext.Current.Session["message"] = msg;
            Rediriger("pagemessage.aspx");
        }
        /**
        * Méthode qui redirige vers une page du site
        * @param pageASP : le nom de la page vers laquelle est faite la redirection
*/
        public void Rediriger(string pageASP)
        {
            HttpContext.Current.Response.Redirect("/PresentationLayer/" + pageASP, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /**
        * Méthode qui fournit un motif HTML venant d'un fichier
        * @param fichierMotif : le nom du fichier contenant le motif à charger
*/
        public static string ChargerMotifHTML(string fichierMotif)
        {
            string codeMotif = null;
            try
            {
                codeMotif = File.ReadAllText(HttpContext.Current.Server.MapPath("~") +
                "/PresentationLayer/MotifsHTML/" + fichierMotif);
            }
            catch (Exception ex)
            {
                new Tools().EcrireTexteErr("Tools", "ChargerMotifHTML()", ex.Message,
                "Problème inattendu lors du chargement du motif " +
                fichierMotif + "!");
            }
            return codeMotif;
        }
        /**
        * Méthode qui ajoute un message d'erreur détaillé dans un fichier de trace
        * @param classe : la classe dans le programme où est survenue l'erreur
        * @param methode : la méthode dans la classe où est survenue l'erreur
        * @param msgDetail : le message d'erreur détaillé
        */

        private void EcrireFichierTrace(string classe, string methode, string msgDetail)
        {
            lock (Verrou) // permet un seul accès au fichier à un moment donné
            {
                try
                {
                    File.AppendAllText(HttpContext.Current.Server.MapPath("~") + "trace.log",
                    DateTime.Now.ToString("dd-MM-yyyy HH:mm") + " -- " +
                    "classe " + classe + " -- " +
                    "méthode " + methode + " -- " +
                    msgDetail +
                    Environment.NewLine);
                }
                catch (Exception)
                {
                }
            }
        }
        /**
        * Méthode qui tente d'établir une nouvelle connexion avec la base de données
        */
        private void ReconnecterSQL()
        {
            try
            {
                HttpContext.Current.Session["FabriqueDAO"] = new FabriqueDAO();
                HttpContext.Current.Session.Timeout = 5;
            }
            catch (ExceptionAccessDB ex)
            {
                EcrireTexteErr("Tools", "ReconnecterSQL()", ex.Message,
                "Problème lors de la connexion à la base de données!");
            }
        }

        /**
         * fonction de hashage pour password
         * @ paramètre String
         * resultat: String
         */
        public static string HashPassword(string pass)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] passEnBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(pass);
            byte[] hashedPassEnBytes = sha1.ComputeHash(passEnBytes);
            string result = Convert.ToBase64String(hashedPassEnBytes);

            return result;
        }

    }
}
