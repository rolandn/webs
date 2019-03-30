using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.Models;

namespace webs.classesMetier
{
    public class Alcool 
    {
        private int NumArticle;
        private int DegreAlcool;
        private string Gout;
        private string Provenance;

        public int numArticle
        {
            get { return NumArticle; }
            set { NumArticle = value; }
        }

        public int degreAlcool
        {
            get { return DegreAlcool; }
            set { DegreAlcool = value; }
        }

        public string gout
        {
            get { return Gout; }
            set { Gout = value; }
        }

        public string provenance
        {
            get { return Provenance; }
            set { Provenance = value; }
        }

        public Alcool(Alcool A)
        {
            numArticle = A.numArticle;
            gout = A.gout;
            degreAlcool = A.degreAlcool;
            provenance = A.provenance;
        }

        public Alcool(int NumArticle, int DegreAlcool, string Gout, string Provenance)
        {
            numArticle = NumArticle;
            gout = Gout;
            degreAlcool = DegreAlcool;
            provenance = Provenance;
        }


    }
}