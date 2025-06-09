using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Commande: ICrud<Commande>
    {
        private int numCommande;
        private Employe employe;
        private ModeTransport unTransport;
        private Revendeur unRevendeur;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private decimal prixTotal;

        public Commande(int numCommande, DateTime dateCommande, DateTime dateLivraison)
        {
            this.NumCommande = numCommande;
            this.Employe = new Employe();
            this.UnTransport = new ModeTransport();
            this.UnRevendeur = new Revendeur();
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
            this.prixTotal = 0;
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public Employe Employe
        {
            get
            {
                return this.employe;
            }

            set
            {
                this.employe = value;
            }
        }

        public ModeTransport UnTransport
        {
            get
            {
                return this.unTransport;
            }

            set
            {
                this.unTransport = value;
            }
        }

        public Revendeur UnRevendeur
        {
            get
            {
                return this.unRevendeur;
            }

            set
            {
                this.unRevendeur = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public DateTime DateLivraison
        {
            get
            {
                return this.dateLivraison;
            }

            set
            {
                this.dateLivraison = value;
            }
        }

        public decimal PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        public void Create()
        {
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numCommande,numEmploye,numTransport,numRevendeur,dateCommande,dateLivraison) values (@numCommande,@numEmploye,@numTransport,@numRevendeur,@dateCommande,@dateLivraison)"))
            {
                cmdInsert.Parameters.AddWithValue("numProduit", this.NumCommande);
                cmdInsert.Parameters.AddWithValue("numTypePointe", this.Employe.NumEmploye);
                cmdInsert.Parameters.AddWithValue("numType", this.UnTransport.NumTransport);
                cmdInsert.Parameters.AddWithValue("codeProduit", this.UnRevendeur.NumRevendeur);
                cmdInsert.Parameters.AddWithValue("prixVente", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("quantiteStock", this.DateLivraison);
            }
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande  where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commande com JOIN employe emp ON com.numemploye=emp.numemploye JOIN modeTransport mt on com.numtransport = mt.numtransport JOIN revendeur rev on com.numrevendeur = rev.numrevendeur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Commande com = new Commande((Int32)dr["numcommande"], (DateTime)dr["datecommande"], (DateTime)dr["datelivraison"]);
                    lesCommandes.Add(com);
                }
                return lesCommandes;

            }
        }

        public List<Commande> FindBySelection(string criteres)
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

        int ICrud<Commande>.Create()
        {
            throw new NotImplementedException();
        }
    }
}
