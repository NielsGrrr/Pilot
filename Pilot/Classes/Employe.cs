using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pilot.Classes
{
    public enum RoleEmploye { Commercial, ResponsableProduction };
    public class Employe : ICrud<Employe>
    {
        private Int32 numEmploye;
        private RoleEmploye role;
        private string nom;
        private string prenom;
        private string password;
        private string login;

        public Employe()
        {
        }

        public Employe(int numEmploye, string nom, string prenom, string password, string login)
        {
            this.NumEmploye = numEmploye;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Password = password;
            this.Login = login;
        }

        public Employe(int numEmploye, RoleEmploye role, string nom, string prenom, string password, string login) : this(numEmploye, nom, prenom, password, login)
        {
            this.Role = role;
        }

        public Int32 NumEmploye
        {
            get
            {
                return this.numEmploye;
            }

            set
            {
                this.numEmploye = value;
            }
        }

        public RoleEmploye Role
        {
            get
            {
                return this.role;
            }

            set
            {
                this.role = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
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

        public List<Employe> FindAll()
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employes emp JOIN role r ON emp.numrole=r.numrole;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Employe emp = new Employe((Int32)dr["numemploye"], (String)dr["nom"], (String)dr["prenom"], (String)dr["password"], (String)dr["login"]);
                    if ((string)dr["role"] == Resources.Commercial)
                    {
                        emp.Role = RoleEmploye.Commercial;
                    }
                    else if ((string)dr["role"] == Resources.ResponsableProduction)
                    {
                        emp.Role = RoleEmploye.ResponsableProduction;
                    }
                    lesEmployes.Add(emp);
                }
                return lesEmployes;
            }
        }

        public List<Employe> FindBySelection(string criteres)
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
