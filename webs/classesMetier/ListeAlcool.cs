using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webs.classesMetier
{
    public class ListeAlcool
    {
        private int NumArticle;
        private string Gout;
        private int DegreAlcool; 
        private string Provenance;
        private string Nom;
        private string NomImage;
        private decimal Prix;
        private int QuantiteStock;

        public ListeAlcool()
        {

        }
        public ListeAlcool(int NumArticle, string Gout, int DegreAlcool, string Provenance, string Nom, string NomImage, decimal Prix, int QuantiteStock)
        {
            numArticle = NumArticle;
            gout = Gout;
            degreAlcool = DegreAlcool;
            provenance = Provenance;
            nom = nom;
            NomImage = NomImage;
            Prix = Prix;
            QuantiteStock = QuantiteStock;


        }

        public int numArticle
        {
            get
            {
                return NumArticle;
            }
            set
            {
                NumArticle = value;
            }
        }

        public string gout
        {
            get
            {
                return Gout;
            }

            set
            {
                Gout = value;
            }
        }

        public int degreAlcool
        {
            get
            {
                return DegreAlcool;
            }

            set
            {
                DegreAlcool = value;
            }
        }

        public string provenance
        {
            get
            {
                return Provenance;
            }

            set
            {
                Provenance = value;
            }
        }

        public string nom
        {
            get
            {
                return Nom;
            }

            set
            {
                nom = value;
            }
        }

        public string nomImage
        {
            get
            {
                return NomImage;
            }

            set
            {
                NomImage = value;
            }
        }

        public decimal prix
        {
            get
            {
                return Prix;
            }

            set
            {
                Prix = value;
            }
        }
        public int quantiteStock { get; set; }
    }
}
}