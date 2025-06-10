using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class ModeTransport
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
    }
}
