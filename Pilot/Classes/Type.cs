using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
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

        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                this.numType = value;
            }
        }

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

        public List<Type> FindAll()
        {
            List<Type> lesTypes = new List<Type>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from chiens ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Categorie laCategorie = new Categorie((Int32)dr["numcategorie"], "test");
                    lesTypes.Add(new Type((Int32)dr["numtype"], laCategorie, (String)dr["libelletype"]));
                }
                    
            }
            return lesTypes;
        }

        public List<Type> FindBySelection(string criteres)
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
