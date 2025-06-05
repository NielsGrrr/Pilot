using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class ModeTransport
    {
        private int numTransport;
        private string libelleTransport;

        public int NumTransport
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
