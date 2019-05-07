using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.classesMetier
{
    public class Produit
    {
        private int NumArticle;
        private string nom;
        private int quantiteStock;
        private decimal prix;
        private string nomImage;
        private bool Active;

        public int qte { get; set; } = 1;

        public Produit(int NumArticle, string nom, int quantiteStock, decimal prix, string nomImage, bool Active)
        {
            this.numArticle = NumArticle;
            this.Nom = nom;
            this.QuantiteStock = quantiteStock;
            this.Prix = prix;
            this.NomImage = nomImage;
            this.active = Active;
        }

        public Produit(int NumArticle)
        {
            this.numArticle = NumArticle;
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

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public int QuantiteStock
        {
            get
            {
                return quantiteStock;
            }

            set
            {
                quantiteStock = value;
            }
        }

        public decimal Prix
        {
            get
            {
                return prix;
            }

            set
            {
                prix = value;
            }
        }

        public string NomImage
        {
            get
            {
                return nomImage;
            }

            set
            {
                nomImage = value;
            }
        }

        public bool active
        {
            get
            {
                return Active;
            }

            set
            {
                Active = value;
            }
        }

    }
}
