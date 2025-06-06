using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class CouleurProduit
    {
        private Produit unProduit;
        private Couleur uneCouleur;

        public Produit UnProduit
        {
            get
            {
                return this.unProduit;
            }

            set
            {
                this.unProduit = value;
            }
        }

        public Couleur UneCouleur
        {
            get
            {
                return this.uneCouleur;
            }

            set
            {
                this.uneCouleur = value;
            }
        }
    }
}
