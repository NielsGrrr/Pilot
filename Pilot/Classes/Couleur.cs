using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Couleur:ICrud<Couleur>
    {
        private int numCouleur;
        private string libelleCouleur;

        public int NumCouleur
        {
            get
            {
                return this.numCouleur;
            }

            set
            {
                this.numCouleur = value;
            }
        }

        public string LibelleCouleur
        {
            get
            {
                return this.libelleCouleur;
            }

            set
            {
                this.libelleCouleur = value;
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

        public List<Couleur> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Couleur> FindBySelection(string criteres)
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
