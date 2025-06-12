using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    public class Produit: ICrud<Produit>
    {
        private int numproduit;
        private TypePointe laPointe;
        private Type leType;
        private string codeProduit;
        private string nomProduit;
        private decimal prixVente;
        private int quantiteStock;
        private bool disponible;
        private ObservableCollection<Couleur> lesCouleurs = new ObservableCollection<Couleur>();

        public Produit()
        {

        }

        public Produit(int numproduit, string codeProduit, string nomProduit, decimal prixVente, int quantiteStock, bool disponible)
        {
            this.Numproduit = numproduit;
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.PrixVente = prixVente;
            this.QuantiteStock = quantiteStock;
            this.Disponible = disponible;
        }

        public Produit(TypePointe laPointe, Type leType, string codeProduit, string nomProduit, decimal prixVente, int quantiteStock, bool disponible)
        {
            this.LaPointe = laPointe;
            this.LeType = leType;
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.PrixVente = prixVente;
            this.QuantiteStock = quantiteStock;
            this.Disponible = disponible;
        }

        public Produit(int numproduit, TypePointe laPointe, Type leType, string codeProduit, string nomProduit, decimal prixVente, int quantiteStock, bool disponible)
        {
            this.Numproduit = numproduit;
            this.LaPointe = laPointe;
            this.LeType = leType;
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.PrixVente = prixVente;
            this.QuantiteStock = quantiteStock;
            this.Disponible = disponible;
        }

        public Produit(int numproduit, TypePointe laPointe, Type leType, string codeProduit, string nomProduit, decimal prixVente, int quantiteStock, bool disponible, ObservableCollection<Couleur> lesCouleurs) : this(numproduit, laPointe, leType, codeProduit, nomProduit, prixVente, quantiteStock, disponible)
        {
            this.LesCouleurs = lesCouleurs;
        }

        public int Numproduit
        {
            get
            {
                return this.numproduit;
            }

            set
            {
                this.numproduit = value;
            }
        }

        public TypePointe LaPointe
        {
            get
            {
                return this.laPointe;
            }

            set
            {
                this.laPointe = value;
            }
        }

        public Type LeType
        {
            get
            {
                return this.leType;
            }

            set
            {
                this.leType = value;
            }
        }

        public string CodeProduit
        {
            get
            {
                return this.codeProduit;
            }

            set
            {
                this.codeProduit = value;
            }
        }

        public string NomProduit
        {
            get
            {
                return this.nomProduit;
            }

            set
            {
                this.nomProduit = value;
            }
        }

        public decimal PrixVente
        {
            get
            {
                return this.prixVente;
            }

            set
            {
                this.prixVente = value;
            }
        }

        public int QuantiteStock
        {
            get
            {
                return this.quantiteStock;
            }

            set
            {
                this.quantiteStock = value;
            }
        }

        public bool Disponible
        {
            get
            {
                return this.disponible;
            }

            set
            {
                this.disponible = value;
            }
        }

        public ObservableCollection<Couleur> LesCouleurs
        {
            get
            {
                return this.lesCouleurs;
            }

            set
            {
                this.lesCouleurs = value;
            }
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into produit (numTypePointe,numType,codeProduit,nomProduit,prixVente,quantiteStock,disponible ) values (@numTypePointe,@numType,@codeProduit,@nomProduit,@prixVente,@quantiteStock,@disponible) RETURNING numproduit"))
            {
                cmdInsert.Parameters.AddWithValue("numTypePointe", this.LaPointe.NumTypePointe);
                cmdInsert.Parameters.AddWithValue("numType", this.LeType.NumType);
                cmdInsert.Parameters.AddWithValue("codeProduit", this.CodeProduit);
                cmdInsert.Parameters.AddWithValue("nomProduit", this.NomProduit);
                cmdInsert.Parameters.AddWithValue("prixVente", this.PrixVente);
                cmdInsert.Parameters.AddWithValue("quantiteStock", this.QuantiteStock);
                cmdInsert.Parameters.AddWithValue("disponible", this.Disponible);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.Numproduit = nb;
            return nb;
        }

        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand("delete from produit where numproduit = @numproduit ;"))
            {
                cmdDelete.Parameters.AddWithValue("numproduit", this.Numproduit);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }

        public List<Produit> FindAll()
        {
            List<Produit> lesProduits = new List<Produit>();
            List<Couleur> desCouleurs = new List<Couleur>(new Couleur().FindAll());
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produit pr join typepointe tp on pr.numtypepointe = tp.numtypepointe join type ty on pr.numtype = ty.numtype join categorie cat on ty.numcategorie = cat.numcategorie"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                foreach (DataRow dr in dt.Rows)
                {
                    Produit pro = new Produit((Int32)dr["numproduit"], (String)dr["codeproduit"], (String)dr["nomproduit"], (decimal)dr["prixvente"], (Int32)dr["quantitestock"], (bool)dr["disponible"]);
                    Couleur couleur = new Couleur();
                    TypePointe pointe = new TypePointe((Int32)dr["numtypepointe"], (String)dr["libelletypepointe"]);
                    pro.LaPointe = pointe;
                    Categorie cat = new Categorie((Int32)dr["numcategorie"], (String)dr["libellecategorie"]);
                    Type type = new Type((Int32)dr["numtype"], cat, (String)dr["libelletype"]);
                    pro.leType = type;
                    //Ici
                    using (NpgsqlCommand cmdSelect2 = new NpgsqlCommand("select * from couleurproduit cp WHERE cp.numproduit=@id;"))
                    {
                        cmdSelect2.Parameters.AddWithValue("id", pro.Numproduit);
                        DataTable dt2 = DataAccess.Instance.ExecuteSelect(cmdSelect2);
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            couleur = desCouleurs.Find(x => x.NumCouleur == (int)dr2["numcouleur"]);
                            pro.LesCouleurs.Add(couleur);
                        }
                    }
                    //Ici
                    lesProduits.Add(pro);
                }
            }
            return lesProduits;
        }

        public List<Produit> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set numtype=@numtype, numtypepointe=@numtypepointe, codeproduit=@codeproduit, nomproduit=@nomproduit, prixvente=@prixvente, quantitestock=@quantitestock, disponible=@disponible, numproduit=@numproduit WHERE numproduit=@numproduit;"))
            {
                cmdUpdate.Parameters.AddWithValue("numtype", this.LeType.NumType);
                cmdUpdate.Parameters.AddWithValue("numtypepointe", this.LaPointe.NumTypePointe);
                cmdUpdate.Parameters.AddWithValue("codeproduit", this.codeProduit);
                cmdUpdate.Parameters.AddWithValue("nomproduit", this.NomProduit);
                cmdUpdate.Parameters.AddWithValue("prixvente", this.prixVente);
                cmdUpdate.Parameters.AddWithValue("quantitestock", this.QuantiteStock);
                cmdUpdate.Parameters.AddWithValue("disponible", this.Disponible);
                cmdUpdate.Parameters.AddWithValue("numproduit", this.Numproduit);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        int ICrud<Produit>.Create()
        {
            throw new NotImplementedException();
        }
    }
}
