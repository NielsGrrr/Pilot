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
    /// <summary>
    /// Stocke les informations d'une commande :
    /// 1 entier : le numéro de la commande
    /// 1 employé : celui qui a enregistré la commande
    /// 1 transport : mode de transport utilisé
    /// 1 revendeur : client ayant passé la commande
    /// 2 dates : la date de commande et la date de livraison
    /// 1 dictionnaire : les produits commandés et leur quantité
    /// </summary>
    public class Commande: ICrud<Commande>
    {
        private int numCommande;
        private Employe employe;
        private ModeTransport unTransport;
        private Revendeur unRevendeur;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private Dictionary<Produit, int> produitsQuantites;

        /// <summary>
        /// Construit une commande vide.
        /// Initialise un dictionnaire vide pour les produits.
        /// </summary>
        public Commande()
        {
            this.ProduitsQuantites = new Dictionary<Produit, int>();
            this.DateCommande = DateTime.Today;
            this.dateLivraison = DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// Construit une commande avec son numéro et sa date.
        /// </summary>
        /// <param name="numCommande">Numéro de la commande</param>
        /// <param name="dateCommande">Date de la commande</param>
        public Commande(int numCommande, DateTime dateCommande)
        {
            this.NumCommande = numCommande;
            this.Employe = new Employe();
            this.UnTransport = new ModeTransport();
            this.UnRevendeur = new Revendeur();
            this.DateCommande = dateCommande;
            this.DateLivraison = new DateTime();
        }

        /// <summary>
        /// Construit une commande complète (numéro, employé, transport, revendeur, livraison).
        /// La date de commande est automatiquement mise au jour actuel.
        /// </summary>
        /// <param name="numCommande"></param>
        /// <param name="employe"></param>
        /// <param name="unTransport"></param>
        /// <param name="unRevendeur"></param>
        /// <param name="dateLivraison"></param>
        public Commande(int numCommande, Employe employe, ModeTransport unTransport, Revendeur unRevendeur, DateTime dateLivraison)
        {
            this.NumCommande = numCommande;
            this.Employe = employe;
            this.UnTransport = unTransport;
            this.UnRevendeur = unRevendeur;
            this.DateCommande = DateTime.Today;
            this.DateLivraison = dateLivraison;
        }

        /// <summary>
        /// Obtient ou définit le numéro de la commande.
        /// Doit être un entier positif.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Renvoyée si le numéro est négatif</exception>
        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de commande ne peut pas être négatif");
                this.numCommande = value;
            }
        }

        /// <summary>
        /// Obtient ou définit l'employé ayant pris la commande.
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit le mode de transport pour la livraison.
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit le revendeur ayant passé la commande.
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit la date à laquelle la commande a été passée.
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit la date de livraison prévue.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Date de livraison avant la date de création</exception>
        public DateTime DateLivraison
        {
            get
            {
                return this.dateLivraison;
            }

            set
            {
                //if (value < this.DateCommande)
                //    throw new ArgumentOutOfRangeException("La livraison ne peut pas être avant lacommande");
                this.dateLivraison = value;
            }
        }

        /// <summary>
        /// Calcule et retourne le prix total de la commande.
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit le dictionnaire des produits et quantités commandés.
        /// </summary>
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

        /// <summary>
        /// Ajoute un produit à la commande et enregistre l'association en base de données.
        /// </summary>
        /// <param name="produit">Produit à ajouter</param>
        /// <param name="quantite">Quantité commandée</param>
        /// <returns>Numéro de commande généré par la base</returns>
        public int AjouterProduit(Produit produit, int quantite)
        {
            bool present = false;
            foreach (Produit pr in this.ProduitsQuantites.Keys)
            {
                if (pr.CodeProduit == produit.CodeProduit)
                {
                    present = true;
                }
            }

            //A terminer avec un UPDATE et en aditionant les quantités
            if (present)
            {
                int quantiteTotale = quantite;
                this.ProduitsQuantites.Add(produit, quantite);
                int nb = 0;
                using (var cmdUpdate = new NpgsqlCommand("insert into produitcommande (numcommande,numproduit,quantitecommande,prix) values (@numcommande,@numproduit,@quantitecommande,@prix) Returning numcommande"))
                {
                    cmdUpdate.Parameters.AddWithValue("numCommande", this.NumCommande);
                    cmdUpdate.Parameters.AddWithValue("numProduit", produit.Numproduit);
                    cmdUpdate.Parameters.AddWithValue("quantiteCommande", quantite);
                    cmdUpdate.Parameters.AddWithValue("prix", produit.PrixVente);
                    nb = DataAccess.Instance.ExecuteInsert(cmdUpdate);
                }
                return nb;
            }
            else
            {
                this.ProduitsQuantites.Add(produit, quantite);
                int nb = 0;
                using (var cmdInsert = new NpgsqlCommand("insert into produitcommande (numcommande,numproduit,quantitecommande,prix) values (@numcommande,@numproduit,@quantitecommande,@prix) Returning numcommande"))
                {
                    cmdInsert.Parameters.AddWithValue("numCommande", this.NumCommande);
                    cmdInsert.Parameters.AddWithValue("numProduit", produit.Numproduit);
                    cmdInsert.Parameters.AddWithValue("quantiteCommande", quantite);
                    cmdInsert.Parameters.AddWithValue("prix", produit.PrixVente);
                    nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
                }
                return nb;
            }
            
        }

        /// <summary>
        /// Insère la commande en base de données.
        /// </summary>
        /// <returns>Numéro de commande généré</returns>
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numEmploye,numTransport,numRevendeur,dateCommande,dateLivraison) values (@numEmploye,@numTransport,@numRevendeur,@dateCommande,@dateLivraison) Returning numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numEmploye", this.Employe.NumEmploye);
                cmdInsert.Parameters.AddWithValue("numTransport", this.UnTransport.NumTransport);
                cmdInsert.Parameters.AddWithValue("numRevendeur", this.UnRevendeur.NumRevendeur);
                cmdInsert.Parameters.AddWithValue("dateCommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("dateLivraison", this.DateLivraison);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        /// <summary>
        /// Supprime la commande et ses produits associés de la base.
        /// </summary>
        /// <returns>Nombre de lignes supprimées</returns>
        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from produitcommande  where numcommande =@numcommande; delete from commande  where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        /// <summary>
        /// Récupère toutes les commandes depuis la base de données.
        /// </summary>
        /// <returns>Liste des commandes</returns>
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
                    Employe employe = new Employe((Int32)dr["numemploye"], (string)dr["nom"], (string)dr["prenom"], (string)dr["password"], (string)dr["login"]);
                    Commande com = new Commande((Int32)dr["numcommande"], (DateTime)dr["datecommande"]);
                    Revendeur rev = new Revendeur((Int32)dr["numrevendeur"], (String)dr["raisonsociale"], (String)dr["adresserue"], (String)dr["adressecp"], (String)dr["adresseville"]);
                    com.UnRevendeur = rev;
                    com.Employe = employe;
                    com.DateLivraison = (DateTime)dr["datelivraison"];
                    ModeTransport modeTransport = new ModeTransport((Int32)dr["numtransport"], (String)dr["libelletransport"]);
                    com.UnTransport = modeTransport;
                    using (NpgsqlCommand cmdSelect2 = new NpgsqlCommand("select * from produitcommande pc WHERE pc.numcommande=@id;"))
                    {
                        cmdSelect2.Parameters.AddWithValue("id", com.NumCommande);
                        DataTable dt2 = DataAccess.Instance.ExecuteSelect(cmdSelect2);
                        foreach(DataRow dr2 in dt2.Rows)
                        {
                            dict.Add(tousLesProduits.Find(x => x.Numproduit == (int)dr2["numproduit"]), (int)dr2["quantitecommande"]);
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

        /// <summary>
        /// Met à jour les informations de la commande en base.
        /// </summary>
        /// <returns>Nombre de lignes modifiées</returns>
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update commande set numrevendeur=@numrevendeur, numemploye=@numemploye, numtransport=@numtransport, datecommande=@datecommande, datelivraison=@datelivraison WHERE numcommande=@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numrevendeur", this.UnRevendeur.NumRevendeur);
                cmdUpdate.Parameters.AddWithValue("numemploye", this.Employe.NumEmploye);
                cmdUpdate.Parameters.AddWithValue("numtransport", this.UnTransport.NumTransport);
                cmdUpdate.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdUpdate.Parameters.AddWithValue("datelivraison", this.DateLivraison);
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        /// <summary>
        /// Recherche une commande existante ayant le même numéro que l'instance.
        /// </summary>
        /// <returns>Commande trouvée ou une commande vide</returns>
        public Commande FindNumCommande()
        {
            List<Commande> commandes = new Commande().FindAll();
            foreach (Commande com in commandes)
            {
                if (com.NumCommande == this.NumCommande)
                    return com;
            }
            return new Commande();
        }

    }
}
