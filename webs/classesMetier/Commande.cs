using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.classesMetier
{
    public class Commande
    {
        private int idCmd;
        private DateTime dateCmd;
        private DateTime heureCmd;
        private Decimal montant;
        private String livre;
        private int numClient;

        public int IdCmd
        {
            get
            {
                return idCmd;
            }

            set
            {
                idCmd = value;
            }
        }

        public DateTime DateCmd
        {
            get
            {
                return dateCmd;
            }

            set
            {
                dateCmd = value;
            }
        }

        public DateTime HeureCmd
        {
            get
            {
                return heureCmd;
            }

            set
            {
                heureCmd = value;
            }
        }

        public decimal Montant
        {
            get
            {
                return montant;
            }

            set
            {
                montant = value;
            }
        }

        public string Livre
        {
            get
            {
                return livre;
            }

            set
            {
                livre = value;
            }
        }

        public int NumClient
        {
            get
            {
                return numClient;
            }

            set
            {
                numClient = value;
            }
        }

        public Commande()
        {

        }
        public Commande(Commande C)
        {
            IdCmd = C.IdCmd;
            DateCmd = C.DateCmd;
            HeureCmd = C.DateCmd;
            Montant = C.Montant;
            Livre = C.Livre;
            NumClient = C.NumClient;

        }
        public Commande(int idCmd, DateTime dateCmd, DateTime heureCmd, decimal montant, String livre, int numClient)
        {
            IdCmd = idCmd;
            DateCmd = dateCmd;
            HeureCmd = heureCmd;
            Montant = montant;
            Livre = livre;
            NumClient = numClient;
        }
        public Commande(int int1, DateTime string1)
        {
            IdCmd = int1;
            DateCmd = string1;


        }
        public Commande(int int1, String livré, decimal montant)
        {
            IdCmd = int1;
            Livre = livre;
            Montant = montant;

        }

        public String toString()
        {
            { return "Commande # " + IdCmd; }
        }
    }
}