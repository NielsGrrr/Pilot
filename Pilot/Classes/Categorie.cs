using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke 2 informations
    /// 1 entier : le numéro de la catégorie
    /// 1 chaine : le libillé de la catégorie
    /// </summary>
    public class Categorie
    {
        private int numCategorie;
        private string libelleCategorie;

        /// <summary>
        /// Construit une categorie
        /// </summary>
        /// <param name="numCategorie"></param>
        /// <param name="libelleCategorie"></param>
        public Categorie(int numCategorie, string libelleCategorie)
        {
            this.numCategorie = numCategorie;
            this.libelleCategorie = libelleCategorie;
        }

        /// <summary>
        /// Obtient le numéro de la catégorie
        /// Doit être positif
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> Renvoyée si le numéro de catégorie est négatif</exception>
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

        /// <summary>
        /// Obtient le libellé de la catégorie
        /// </summary>
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
