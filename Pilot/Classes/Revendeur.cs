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

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from revendeur where numrevendeur =@numrevendeur;"))
            {
                cmdUpdate.Parameters.AddWithValue("numrevendeur", this.NumRevendeur);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

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
