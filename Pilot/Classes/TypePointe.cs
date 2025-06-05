using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class TypePointe
    {
        private int numTypePointe;
        private string libelleTypePointe;

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
    }
}
