using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class ModeTransport: ICrud<ModeTransport>
    {
        private Int32 numTransport;
        private string libelleTransport;

        public ModeTransport()
        {
        }

        public ModeTransport(int numTransport, string libelleTransport)
        {
            this.NumTransport = numTransport;
            this.LibelleTransport = libelleTransport;
        }

        public Int32 NumTransport
        {
            get
            {
                return this.numTransport;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de transport ne peut pas être négatif");
                this.numTransport = value;
            }
        }

        public string LibelleTransport
        {
            get
            {
                return this.libelleTransport;
            }

            set
            {
                this.libelleTransport = value;
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

        public List<ModeTransport> FindAll()
        {
            List<ModeTransport> lesTransports = new List<ModeTransport>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from modeTransport mt;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    ModeTransport mt = new ModeTransport((Int32)dr["numtransport"], (String)dr["libelletransport"]);
                    lesTransports.Add(mt);
                }
                return lesTransports;
            }
        }

        public List<ModeTransport> FindBySelection(string criteres)
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
