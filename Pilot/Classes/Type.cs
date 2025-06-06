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
        private string libelleType;

        public Type(int numType, Categorie laCategorie, string libelleType)
        {
            this.NumType = numType;
            this.LaCategorie = laCategorie;
            this.LibelleType = libelleType;
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

        public string LibelleType
        {
            get
            {
                return this.libelleType;
            }

            set
            {
                this.libelleType = value;
            }
        }
    }
}
