using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.classesMetier
{
    public class Client
    {
        private int iD_clt;
        private String nom;
        private String prenom;
        private String adresse;
        private String email;
        private String password;

        // constructeur

        public Client()
        {

        }
        public Client(Client clt)
        {
            ID_clt = clt.ID_clt;
            Nom = clt.Nom;
            Prenom = clt.Prenom;
            Adresse = clt.Adresse;
            Email = clt.Email;
            Password = clt.Password;
        }

        public Client(int id_clt, string nom, string prenom, string adresse, string email,
        string password)
        {
            ID_clt = id_clt;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Email = email;
            Password = password;
        }
        public Client(int int1, string string1)
        {
            ID_clt = int1;
            Nom = string1;
        }
        public Client(int ID, string email, string pass)
        {
            ID_clt = ID;
            Email = email;
            Password = pass;
        }
        // propriété
        public int ID_clt
        {
            get { return iD_clt; }
            set { iD_clt = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }

            set { prenom = value; }
        }

        public string Adresse
        {
            get { return adresse; }

            set { adresse = value; }
        }

        public string Email
        {
            get { return email; }

            set { email = value; }
        }

        public string Password
        {
            get { return password; }

            set { password = value; }
        }

        public String toString()
        {
            return ID_clt + ")  " + Nom.ToString();
        }



    }

}