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

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

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
