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
            private int numProduit;
            private int qte;

            public int NumCmd
            {
                get { return numCmd; }
                set { numCmd = value; }
            }
            public int NumProduit
            {
                get
                {
                    return numProduit;
                }

                set
                {
                    numProduit = value;
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
                NumProduit = LC.NumProduit;
                Qte = LC.Qte;

            }
            public Ligne_Cmd(int numCmd, int numProduit, int qte)
            {
                NumCmd = numCmd;
                NumProduit = numProduit;
                Qte = qte;
            }

            public String toString()
            {
                return this.NumCmd + " ) " + this.NumProduit + this.Qte;
            }

        }
    }
