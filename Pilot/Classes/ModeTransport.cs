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

        /// <summary>
        /// Numéro unique du mode de transport.
        /// </summary>
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

        /// <summary>
        /// Libellé du mode de transport (ex: "Camion", "Train").
        /// </summary>
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

        /// <summary>
        /// Récupère tous les modes de transport présents en base.
        /// </summary>
        /// <returns>Liste de modes de transport</returns>
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
