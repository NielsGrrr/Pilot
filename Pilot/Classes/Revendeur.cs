using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Revendeur : ICrud<Revendeur>
    {
        private Int32 numRevendeur;
        private string raisonSociale;
        private string adresseRue;
        private string adresseCP;
        private string adresseVille;

        public Revendeur()
        {
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
            throw new NotImplementedException();
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
            using (var cmdInsert = new NpgsqlCommand("update revendeur set raisonsociale=@raisonsociale, adresserue=@adresserue, adressecp=@adressecp, adresseville=@adresseville WHERE numrevendeur=@numrevendeur;"))
            {
                cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdInsert.Parameters.AddWithValue("adresserue", this.AdresseRue);
                cmdInsert.Parameters.AddWithValue("adressecp", this.AdresseCP);
                cmdInsert.Parameters.AddWithValue("adresseville", this.AdresseVille);
                cmdInsert.Parameters.AddWithValue("numRevendeur",this.NumRevendeur);
                return DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
        }
    }
}
