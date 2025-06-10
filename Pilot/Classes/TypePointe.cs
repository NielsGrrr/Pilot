using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
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

        public int NumTypePointe
        {
            get
            {
                return this.numTypePointe;
            }

            set
            {
                this.numTypePointe = value;
            }
        }

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
