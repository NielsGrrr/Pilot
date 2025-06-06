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
    }
}
