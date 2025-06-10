using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class ProduitCommande
    {
        private Commande uneCommande;
        private Produit unProduit;
        private int quantiteCommande;

        public Commande UneCommande
        {
            get
            {
                return this.uneCommande;
            }

            set
            {
                this.uneCommande = value;
            }
        }

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

        public int QuantiteCommande
        {
            get
            {
                return this.quantiteCommande;
            }

            set
            {
                this.quantiteCommande = value;
            }
        }

        public decimal PrixTotal
        {
            get
            {
                return this.unProduit.PrixVente * this.quantiteCommande;
            }
        }
    }
}
