using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.classesMetier
{
    
        public class Ligne_Cmd
        {
            private int numCmd;
            private int numArticle;
            private int qte;

            public int NumCmd
            {
                get { return numCmd; }
                set { numCmd = value; }
            }
            public int NumArticle
        {
                get
                {
                    return numArticle;
                }

                set
                {
                numArticle = value;
                }
            }
            public int Qte
            {
                get
                {
                    return qte;
                }

                set
                {
                    qte = value;
                }
            }

            /**
             * Constructeurs
             */
            public Ligne_Cmd()
            {

            }
            public Ligne_Cmd(Ligne_Cmd LC)
            {
                NumCmd = LC.NumCmd;
                NumArticle = LC.NumArticle;
                Qte = LC.Qte;

            }
            public Ligne_Cmd(int numCmd, int numArticle, int qte)
            {
                NumCmd = numCmd;
                NumArticle = numArticle;
                Qte = qte;
            }

            public String toString()
            {
                return this.NumCmd + " ) " + this.NumArticle + this.Qte;
            }

        }
    }
