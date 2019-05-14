using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.classesMetier
{
    
        public class Ligne_Cmd
        {
            private int id;
            private int numCmd;
            private int numArticle;
            private int qte;
            //private int idcmd;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

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
                Id = LC.id;
                NumCmd = LC.NumCmd;
                NumArticle = LC.NumArticle;
                Qte = LC.Qte;

            }
            public Ligne_Cmd(int id, int numCmd, int numArticle, int qte)
            {
                Id = id;
                NumCmd = numCmd;
                NumArticle = numArticle;
                Qte = qte;
            }

        public Ligne_Cmd(int idcmd, int numArticle, int qte)
        {
            this.numCmd = idcmd;
            this.numArticle = numArticle;
            this.qte = qte;
        }

        public String toString()
            {
                return this.NumCmd + " ) " + this.NumArticle + this.Qte;
            }

        }
    }
