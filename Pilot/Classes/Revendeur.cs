using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke les informations d'un revendeur :
    /// 1 entier : le numéro unique du revendeur
    /// 1 chaîne : la raison sociale
    /// 3 chaînes : l'adresse (rue, code postal, ville)
    /// </summary>
    public class Revendeur : ICrud<Revendeur>, INotifyPropertyChanged
    {
        private Int32 numRevendeur;
        private string raisonSociale;
        private string adresseRue;
        private string adresseCP;
        private string adresseVille;

        public Revendeur()
        {
        }
        public Revendeur(string raisonSociale, string adresseRue, string adresseCP, string adresseVille)
        {
            this.RaisonSociale = raisonSociale;
            this.AdresseRue = adresseRue;
            this.AdresseCP = adresseCP;
            this.AdresseVille = adresseVille;
        }
        public Revendeur(Int32 numRevendeur, string raisonSociale, string adresseRue, string adresseCP, string adresseVille)
        {
            this.NumRevendeur = numRevendeur;
            this.RaisonSociale = raisonSociale;
            this.AdresseRue = adresseRue;
            this.AdresseCP = adresseCP;
            this.AdresseVille = adresseVille;
        }

        /// <summary>
        /// Identifiant unique du revendeur.
        /// </summary>
        public Int32 NumRevendeur
        {
            get
            {
                return this.numRevendeur;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de revendeur ne peut pas être négatif");
                this.numRevendeur = value;
            }
        }

        /// <summary>
        /// Raison sociale du revendeur.
        /// </summary>
        public string RaisonSociale
        {
            get
            {
                return this.raisonSociale;
            }

            set
            {
                this.raisonSociale = value;
            }
        }

        /// <summary>
        /// Rue de l’adresse du revendeur.
        /// </summary>
        public string AdresseRue
        {
            get
            {
                return this.adresseRue;
            }

            set
            {
                this.adresseRue = value;
            }
        }

        /// <summary>
        /// Code postal (doit contenir 5 caractères).
        /// </summary>
        public string AdresseCP
        {
            get
            {
                return this.adresseCP;
            }

            set
            {
                if (value.Length != 5)
                    throw new ArgumentException("Le code postal doit contenir 5 caractères");
                this.adresseCP = value;
            }
        }

        /// <summary>
        /// Ville du revendeur.
        /// </summary>
        public string AdresseVille
        {
            get
            {
                return this.adresseVille;
            }

            set
            {
                this.adresseVille = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Crée un revendeur dans la base de données.
        /// </summary>
        /// <returns>Identifiant du revendeur nouvellement créé.</returns>
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into revendeur (raisonsociale,adresserue,adressecp,adresseville) values (@raisonsociale,@adresserue,@adressecp,@adresseville) RETURNING numrevendeur;"))
            {
                cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdInsert.Parameters.AddWithValue("adresserue", this.AdresseRue);
                cmdInsert.Parameters.AddWithValue("adressecp", this.AdresseCP);
                cmdInsert.Parameters.AddWithValue("adresseville", this.AdresseVille);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.numRevendeur = nb;
            return nb;
        }

        /// <summary>
        /// Supprime ce revendeur de la base de données.
        /// </summary>
        /// <returns>Nombre de lignes supprimées.</returns>
        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from revendeur where numrevendeur =@numrevendeur;"))
            {
                cmdUpdate.Parameters.AddWithValue("numrevendeur", this.NumRevendeur);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        /// <summary>
        /// Récupère tous les revendeurs depuis la base de données.
        /// </summary>
        /// <returns>Liste des revendeurs.</returns>
        public List<Revendeur> FindAll()
        {
            List<Revendeur> lesRevendeurs = new List<Revendeur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from revendeur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesRevendeurs.Add(new Revendeur((Int32)dr["numrevendeur"], (string)dr["raisonsociale"], (string)dr["adresserue"], (string)dr["adressecp"], (string)dr["adresseville"]));
                }
            }
            return lesRevendeurs;
        }

        public List<Revendeur> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Met à jour ce revendeur dans la base de données.
        /// </summary>
        /// <returns>Nombre de lignes modifiées.</returns>
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update revendeur set raisonsociale=@raisonsociale, adresserue=@adresserue, adressecp=@adressecp, adresseville=@adresseville WHERE numrevendeur=@numrevendeur;"))
            {
                cmdUpdate.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdUpdate.Parameters.AddWithValue("adresserue", this.AdresseRue);
                cmdUpdate.Parameters.AddWithValue("adressecp", this.AdresseCP);
                cmdUpdate.Parameters.AddWithValue("adresseville", this.AdresseVille);
                cmdUpdate.Parameters.AddWithValue("numRevendeur",this.NumRevendeur);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}
