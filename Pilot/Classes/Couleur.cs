using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke les informations d'une couleur :
    /// 1 entier : le numéro de la commande
    /// 1 chaine de caractères : le libelle de la couleur
    /// </summary>
    public class Couleur:ICrud<Couleur>
    {
        private int numCouleur;
        private string libelleCouleur;

        public Couleur()
        {
        }

        public Couleur(int numCouleur, string libelleCouleur)
        {
            this.NumCouleur = numCouleur;
            this.LibelleCouleur = libelleCouleur;
        }

        public Couleur(string libelleCouleur)
        {
            this.LibelleCouleur = libelleCouleur;
        }

        /// <summary>
        /// Obtient ou définit le numéro de la couleur.
        /// Doit être un entier positif.
        /// </summary>
        public int NumCouleur
        {
            get
            {
                return this.numCouleur;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de couleur ne peut pas être négatif");
                this.numCouleur = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le libellé (nom) de la couleur.
        /// </summary>
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

        /// <summary>
        /// Insère la couleur en base de données.
        /// </summary>
        /// <returns>Identifiant généré</returns>
        public int Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime la couleur de la base de données.
        /// </summary>
        /// <returns>Nombre de lignes supprimées</returns>
        public int Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Récupère toutes les couleurs depuis la base de données.
        /// </summary>
        /// <returns>Liste de toutes les couleurs</returns>
        public List<Couleur> FindAll()
        {
            List<Couleur> lesCouleurs = new List<Couleur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesCouleurs.Add(new Couleur((Int32)dr["numcouleur"], (string)dr["libellecouleur"]));
                }
            }
            return lesCouleurs;
        }

        public List<Couleur> FindBySelection(string numCouleur)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vérifie si la couleur actuelle est déjà présente dans une collection.
        /// </summary>
        /// <returns>True si la couleur est présente, false sinon</returns>
        public bool EstPresent(ObservableCollection<Couleur> lesCouleurs)
        {
            if (lesCouleurs == null)
                return false;
            foreach (Couleur coul in lesCouleurs)
            {
                if (coul.LibelleCouleur == this.LibelleCouleur)
                    return true;
            }
            return false;
        }
    }
}
