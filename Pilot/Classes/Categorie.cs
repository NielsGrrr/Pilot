using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Categorie
    {
        private int numCategorie;
        private string libelleCategorie;

        public Categorie(int numCategorie, string libelleCategorie)
        {
            this.numCategorie = numCategorie;
            this.libelleCategorie = libelleCategorie;
        }

        public int NumCategorie
        {
            get
            {
                return this.numCategorie;
            }

            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException("Le numéro de catégorie ne peut pas être négatif");
                this.numCategorie = value;
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
