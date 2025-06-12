using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Produit> lesProduits = new ObservableCollection<Produit>();
        private List<int> lesQuantites = new List<int>();
        private Dictionary<Produit, int> produitsQuantites;

        public Commande()
        {

        }

        public Commande(int numCommande, DateTime dateCommande)
        {
            this.NumCommande = numCommande;
            this.Employe = new Employe();
            this.UnTransport = new ModeTransport();
            this.UnRevendeur = new Revendeur();
            this.DateCommande = dateCommande;
            this.DateLivraison = new DateTime();
        }

        public Commande(int numCommande, Employe employe, ModeTransport unTransport, Revendeur unRevendeur, DateTime dateLivraison)
        {
            this.NumCommande = numCommande;
            this.Employe = employe;
            this.UnTransport = unTransport;
            this.UnRevendeur = unRevendeur;
            this.DateCommande = DateTime.Today;
            this.DateLivraison = dateLivraison;
        }

        public Commande(Employe employe, ModeTransport unTransport, Revendeur unRevendeur, DateTime dateLivraison)
        {
            this.Employe = employe;
            this.UnTransport = unTransport;
            this.UnRevendeur = unRevendeur;
            this.DateLivraison = dateLivraison;
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

        public decimal PrixFinal
        {
            get
            {
                decimal res = 0;
                foreach (Produit prod in this.ProduitsQuantites.Keys)
                {
                    res += prod.PrixVente * ProduitsQuantites[prod];
                }
                return res;
            }
        }

        public Dictionary<Produit, int> ProduitsQuantites
        {
            get
            {
                return this.produitsQuantites;
            }

            set
            {
                this.produitsQuantites = value;
            }
        }

        public ObservableCollection<Produit> LesProduits
        {
            get
            {
                return this.lesProduits;
            }

            set
            {
                this.lesProduits = value;
            }
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numCommande,numEmploye,numTransport,numRevendeur,dateCommande,dateLivraison) values (@numCommande,@numEmploye,@numTransport,@numRevendeur,@dateCommande,@dateLivraison) Returning numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numTypePointe", this.Employe.NumEmploye);
                cmdInsert.Parameters.AddWithValue("numType", this.UnTransport.NumTransport);
                cmdInsert.Parameters.AddWithValue("codeProduit", this.UnRevendeur.NumRevendeur);
                cmdInsert.Parameters.AddWithValue("dateCommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("quantiteStock", this.DateLivraison);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from produitcommande  where numcommande =@numcommande; delete from commande  where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();
            List<Produit> tousLesProduits = new List<Produit>(new Produit().FindAll());
            
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commande com JOIN employe emp ON com.numemploye=emp.numemploye JOIN modeTransport mt on com.numtransport = mt.numtransport JOIN revendeur rev on com.numrevendeur = rev.numrevendeur JOIN role ro on emp.numrole = ro.numrole;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<Produit, int> dict = new Dictionary<Produit, int>();
                    Produit produit = new Produit();
                    Employe employe = new Employe((Int32)dr["numemploye"], (string)dr["nom"], (string)dr["prenom"], (string)dr["password"], (string)dr["login"]);
                    Commande com = new Commande((Int32)dr["numcommande"], (DateTime)dr["datecommande"]);
                    Revendeur rev = new Revendeur((Int32)dr["numrevendeur"], (String)dr["raisonsociale"], (String)dr["adresserue"], (String)dr["adressecp"], (String)dr["adresseville"]);
                    com.UnRevendeur = rev;
                    com.Employe = employe;
                    ModeTransport modeTransport = new ModeTransport((Int32)dr["numtransport"], (String)dr["libelletransport"]);
                    com.UnTransport = modeTransport;
                    using (NpgsqlCommand cmdSelect2 = new NpgsqlCommand("select * from produitcommande pc WHERE pc.numcommande=@id;"))
                    {
                        cmdSelect2.Parameters.AddWithValue("id", com.NumCommande);
                        DataTable dt2 = DataAccess.Instance.ExecuteSelect(cmdSelect2);
                        foreach(DataRow dr2 in dt2.Rows)
                        {
                            produit = tousLesProduits.Find(x => x.Numproduit == (int)dr2["numproduit"]);
                            com.LesProduits.Add(produit);
                            dict.Add(produit, (int)dr2["quantitecommande"]);
                        }
                    }
                    com.ProduitsQuantites = dict;
                    lesCommandes.Add(com);
                }
            }
            return lesCommandes;
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

    }
}
