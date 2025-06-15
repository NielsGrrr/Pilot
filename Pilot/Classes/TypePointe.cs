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
    /// Stocke les informations d'un type de pointe :
    /// 1 entier : le numéro unique du type de pointe
    /// 1 chaîne : le libellé du type de pointe
    /// </summary>
    public class TypePointe: ICrud<TypePointe>
    {
        private int numTypePointe;
        private string libelleTypePointe;

        public TypePointe()
        {
        }

        public TypePointe(int numTypePointe, string libelleTypePointe)
        {
            this.NumTypePointe = numTypePointe;
            this.LibelleTypePointe = libelleTypePointe;
        }

        /// <summary>
        /// Obtient ou définit le numéro unique du type de pointe.
        /// </summary>
        public int NumTypePointe
        {
            get
            {
                return this.numTypePointe;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de pointe ne peut pas être négatif");
                this.numTypePointe = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le libellé du type de pointe.
        /// </summary>
        public string LibelleTypePointe
        {
            get
            {
                return this.libelleTypePointe;
            }

            set
            {
                this.libelleTypePointe = value;
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
        /// Récupère tous les types de pointe depuis la base de données.
        /// </summary>
        /// <returns>Liste de tous les types de pointe.</returns>
        public List<TypePointe> FindAll()
        {
            List<TypePointe> lesPointes = new List<TypePointe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from typepointe;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesPointes.Add(new TypePointe((Int32)dr["numtypepointe"], (String)dr["libelletypepointe"]));
                }

            }
            return lesPointes;
        }

        public List<TypePointe> FindBySelection(string criteres)
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
    }
}
