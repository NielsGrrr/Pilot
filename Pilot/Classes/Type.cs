using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke les informations d'un type de produit :
    /// 1 entier : le numéro unique du type
    /// 1 catégorie : la catégorie à laquelle appartient ce type
    /// 1 chaîne : le libellé du type
    /// </summary>
    public class Type: ICrud<Type>
    {
        private int numType;
        private Categorie laCategorie;
        private string libelleType;

        public Type()
        {
        }

        public Type(int numType, Categorie laCategorie, string libelleType)
        {
            this.NumType = numType;
            this.LaCategorie = laCategorie;
            this.LibelleType = libelleType;
        }

        /// <summary>
        /// Identifiant unique du type.
        /// </summary>
        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de type ne peut pas être négatif");
                this.numType = value;
            }
        }

        /// <summary>
        /// Catégorie associée à ce type.
        /// </summary>
        public Categorie LaCategorie
        {
            get
            {
                return this.laCategorie;
            }

            set
            {
                this.laCategorie = value;
            }
        }

        /// <summary>
        /// Libellé du type.
        /// </summary>
        public string LibelleType
        {
            get
            {
                return this.libelleType;
            }

            set
            {
                this.libelleType = value;
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

        /// <summary>
        /// Récupère tous les types de produits présents dans la base de données.
        /// </summary>
        /// <returns>Liste des types de produits.</returns>
        public List<Type> FindAll()
        {
            List<Type> lesTypes = new List<Type>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from type JOIN categorie ON type.numcategorie=categorie.numcategorie;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Categorie laCategorie = new Categorie((Int32)dr["numcategorie"], (string)dr["libellecategorie"]);
                    lesTypes.Add(new Type((Int32)dr["numtype"], laCategorie, (String)dr["libelletype"]));
                }
                    
            }
            return lesTypes;
        }

        /// <summary>
        /// Recherche des types correspondant au libellé donné.
        /// </summary>
        /// <returns>Liste des types correspondants.</returns>
        public List<Type> FindBySelection(string criteres)
        {
            List<Type> lesTypes = new Type().FindAll();
            List<Type> retour = new List<Type>();
            foreach (Type type in lesTypes)
            {
                if (type.libelleType == criteres)
                {
                    retour.Add(type);
                }
            }
            return retour;
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
