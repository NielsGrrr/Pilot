using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Type
    {
        private int numType;
        private Categorie laCategorie;
        private string libelleCategorie;

        public Type(int numType, Categorie laCategorie, string libelleCategorie)
        {
            this.NumType = numType;
            this.LaCategorie = laCategorie;
            this.LibelleCategorie = libelleCategorie;
        }

        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                this.numType = value;
            }
        }

        public Categorie LaCategorie
        {
            get
            {
                return this.laCategorie;
            }

            set
            {
                this.laCategorie = value;
            }
        }

        public string LibelleCategorie
        {
            get
            {
                return this.libelleCategorie;
            }

            set
            {
                this.libelleCategorie = value;
            }
        }
    }
}
