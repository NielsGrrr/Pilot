using Npgsql;
using System;
using System.Collections.Generic;
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

        public Produit()
        {

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

        public void Create()
        {
            using (var cmdInsert = new NpgsqlCommand("insert into produit (numProduit,numTypePointe,numType,codeProduit,nomProduit,prixVente,quantiteStock,disponible ) values (@numProduit,@numTypePointe,@numType,@codeProduit,@nomProduit,@prixVente,@quantiteStock,@disponible)"))
            {
                cmdInsert.Parameters.AddWithValue("numProduit", this.Numproduit);
                cmdInsert.Parameters.AddWithValue("numTypePointe", this.LaPointe.NumTypePointe);
                cmdInsert.Parameters.AddWithValue("numType", this.LeType.NumType);
                cmdInsert.Parameters.AddWithValue("codeProduit", this.CodeProduit);
                cmdInsert.Parameters.AddWithValue("prixVente", this.PrixVente);
                cmdInsert.Parameters.AddWithValue("quantiteStock", this.QuantiteStock);
                cmdInsert.Parameters.AddWithValue("disponible", this.Disponible);
            }
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Produit> FindAll()
        {
            List<Produit> lesRevendeurs = new List<Produit>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produit p JOIN typepointe tp ON p.numtypepointe=tp.numtypepointe JOIN type t ON p.numtype=t.numtype JOIN Categorie c ON t.numcategorie=c.numcategorie;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesRevendeurs.Add(new Produit((Int32)dr["numproduit"], new TypePointe((int)dr["numtypepointe"], (string)dr["libelletypepointe"]),
                        new Type((int)dr["numtype"], new Categorie((int)dr["numcategorie"], (string)dr["libellecategorie"]), (string)dr["libelletype"]),
                        (string)dr["codeproduit"], (string)dr["nomproduit"], (decimal)dr["prixvente"], (int)dr["quantitestock"], (bool)dr["disponible"]));
                }
            }
            return lesRevendeurs;
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
            throw new NotImplementedException();
        }

        int ICrud<Produit>.Create()
        {
            throw new NotImplementedException();
        }
    }
}
