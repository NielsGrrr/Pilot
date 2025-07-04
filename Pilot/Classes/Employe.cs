﻿using Npgsql;
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

        /// <summary>
        /// Numéro unique de l'employé.
        /// </summary>
        public Int32 NumEmploye
        {
            get
            {
                return this.numEmploye;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro d'employé ne peut pas être négatif");
                this.numEmploye = value;
            }
        }

        /// <summary>
        /// Rôle de l'employé (Commercial ou Responsable de Production).
        /// </summary>
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

        /// <summary>
        /// Nom de l'employé.
        /// </summary>
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

        /// <summary>
        /// Prénom de l'employé.
        /// </summary>
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

        /// <summary>
        /// Mot de passe de l'employé.
        /// </summary>
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

        /// <summary>
        /// Identifiant de connexion de l'employé.
        /// </summary>
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

        /// <summary>
        /// Récupère tous les employés enregistrés dans la base de données.
        /// </summary>
        /// <returns>Liste d'employés</returns>
        public List<Employe> FindAll()
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employe emp JOIN role r ON emp.numrole=r.numrole;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Employe emp = new Employe((Int32)dr["numemploye"], (String)dr["nom"], (String)dr["prenom"], (String)dr["password"], (String)dr["login"]);
                    if ((string)dr["libellerole"] == Resources.Commercial)
                    {
                        emp.Role = RoleEmploye.Commercial;
                    }
                    else if ((string)dr["libellerole"] == Resources.ResponsableProduction)
                    {
                        emp.Role = RoleEmploye.ResponsableProduction;
                    }
                    lesEmployes.Add(emp);
                }
                return lesEmployes;
            }
        }

        /// <summary>
        /// Recherche les employés en fonction d'un critère SQL personnalisé.
        /// </summary>
        /// <returns>Liste des employés correspondant aux critères</returns>
        public List<Employe> FindBySelection(string criteres)
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employe emp JOIN role r ON emp.numrole=r.numrole WHERE " + criteres + ";"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Employe emp = new Employe((Int32)dr["numemploye"], (String)dr["nom"], (String)dr["prenom"], (String)dr["password"], (String)dr["login"]);
                    if ((string)dr["libellerole"] == Resources.Commercial)
                    {
                        emp.Role = RoleEmploye.Commercial;
                    }
                    else if ((string)dr["libellerole"] == Resources.ResponsableProduction)
                    {
                        emp.Role = RoleEmploye.ResponsableProduction;
                    }
                    lesEmployes.Add(emp);
                }
                return lesEmployes;
            }
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
