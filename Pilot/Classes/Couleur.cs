using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Couleur
    {
        private int numCouleur;
        private string libelleCouleur;

        public int NumCouleur
        {
            get
            {
                return this.numCouleur;
            }

            set
            {
                this.numCouleur = value;
            }
        }

        public string LibelleCouleur
        {
            get
            {
                return this.libelleCouleur;
            }

            set
            {
                this.libelleCouleur = value;
            }
        }
    }
}
