using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using NpgsqlTypes;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke les informations d'un produit :
    /// 1 entier : le numéro unique du produit
    /// 1 type de pointe : le type de pointe du produit
    /// 1 type : le type du produit
    /// 1 chaîne : le code du produit
    /// 1 chaîne : le nom du produit
    /// 1 decimal : Le prix de vente du produit
    /// 1 entier : la quantité en stock
    /// 1 booléen : indique si le produit est disponible
    /// 1 ObservableCOllection<couleur> : les couleurs du produit
    /// 1 tableau d'octects : stocke l'image du produit
    /// </summary>
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
        private byte[] imageData;
        private ObservableCollection<Couleur> lesCouleurs = new ObservableCollection<Couleur>();
        public ObservableCollection<Couleur> ToutesLesCouleurs { get; set; } = new ObservableCollection<Couleur>(new Couleur().FindAll());
        private Dictionary<Couleur, bool> couleursUtilisees = new Dictionary<Couleur, bool>();
        public Produit()
        {
            this.CouleursUtilisees = new Dictionary<Couleur, bool>();
            foreach (Couleur coul in ToutesLesCouleurs)
            {
                this.CouleursUtilisees.Add(coul, false);
            }
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

        public Produit(int numproduit, TypePointe laPointe, Type leType, string codeProduit, string nomProduit, decimal prixVente, int quantiteStock, bool disponible, ObservableCollection<Couleur> lesCouleurs, Dictionary<Couleur, bool> couleursUtilisees) : this(numproduit, laPointe, leType, codeProduit, nomProduit, prixVente, quantiteStock, disponible, lesCouleurs)
        {
            this.CouleursUtilisees = couleursUtilisees;
        }

        /// <summary>
        /// Numéro unique du produit
        /// </summary>
        public int Numproduit
        {
            get
            {
                return this.numproduit;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le numéro de produit ne peut pas être négatif");
                this.numproduit = value;
            }
        }

        /// <summary>
        /// Type de pointe du produit.
        /// </summary>
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

        /// <summary>
        /// Type du produit.
        /// </summary>
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

        /// <summary>
        /// Code unique à 5 caractères pour identifier le produit.
        /// </summary>
        public string CodeProduit
        {
            get
            {
                return this.codeProduit;
            }

            set
            {
                if (value.Length != 5)
                {
                    MessageBox.Show("Le code produit doit comporter 5 caractères");
                    throw new ArgumentException("Le code produit doit comporter 5 caractères");
                }
                this.codeProduit = value;
            }
        }

        /// <summary>
        /// Nom du produit.
        /// </summary>
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

        /// <summary>
        /// Prix de vente du produit.
        /// </summary>
        public decimal PrixVente
        {
            get
            {
                return this.prixVente;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le prix de vente ne peut pas être négatif");
                this.prixVente = value;
            }
        }

        /// <summary>
        /// Quantité en stock du produit.
        /// </summary>
        public int QuantiteStock
        {
            get
            {
                return this.quantiteStock;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("La quantité en stock ne peut pas être négative");
                this.quantiteStock = value;
            }
        }

        /// <summary>
        /// Indique si le produit est disponible à la vente.
        /// </summary>
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

        /// <summary>
        /// Liste des couleurs associées au produit.
        /// </summary>
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

        /// <summary>
        /// Toutes les couleurs disponibles dans le système et leur utilisation dans le produit
        /// </summary>
        public Dictionary<Couleur, bool> CouleursUtilisees
        {
            get
            {
                return this.couleursUtilisees;
            }

            set
            {
                this.couleursUtilisees = value;
            }
        }

        /// <summary>
        /// Contient les données binaires de l'image du produit. C'est cette donnée qui sera stockée dans la colonne BYTEA de PostgreSQL.
        /// </summary>
        public byte[] ImageData
        {
            get
            {
                return this.imageData;
            }

        
            set
            {
                this.imageData = value;
            }
        }

        /// <summary>
        /// Propriété utilitaire pour l'affichage de l'image dans l'interface utilisateur.
        /// Elle convertit le tableau d'octets (ImageData) en un objet ImageSource affichable.
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (ImageData == null || ImageData.Length == 0)
                    return null;

                try
                {
                    using (MemoryStream ms = new MemoryStream(ImageData))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();
                        return bitmap;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la conversion de l'image : {ex.Message}", "Erreur Image", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Crée un nouveau produit dans la base de données.
        /// </summary>
        /// <returns>Identifiant du produit créé.</returns>
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into produit (numTypePointe,numType,codeProduit,nomProduit,prixVente,quantiteStock,disponible,imagedata) values (@numTypePointe,@numType,@codeProduit,@nomProduit,@prixVente,@quantiteStock,@disponible,@imagedata) RETURNING numproduit"))
            {
                cmdInsert.Parameters.AddWithValue("numTypePointe", this.LaPointe.NumTypePointe);
                cmdInsert.Parameters.AddWithValue("numType", this.LeType.NumType);
                cmdInsert.Parameters.AddWithValue("codeProduit", this.CodeProduit);
                cmdInsert.Parameters.AddWithValue("nomProduit", this.NomProduit);
                cmdInsert.Parameters.AddWithValue("prixVente", this.PrixVente);
                cmdInsert.Parameters.AddWithValue("quantiteStock", this.QuantiteStock);
                cmdInsert.Parameters.AddWithValue("disponible", this.Disponible);
                cmdInsert.Parameters.AddWithValue("imagedata", this.ImageData);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.Numproduit = nb;
            return nb;
        }

        /// <summary>
        /// Supprime ce produit de la base de données.
        /// </summary>
        /// <returns>Nombre de lignes supprimées.</returns>
        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand("delete from produit where numproduit = @numproduit ;"))
            {
                cmdDelete.Parameters.AddWithValue("numproduit", this.Numproduit);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }

        /// <summary>
        /// Récupère tous les produits depuis la base de données.
        /// </summary>
        /// <returns>Liste des produits.</returns>
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
                    if (dr["ImageData"] != DBNull.Value)
                    {
                        pro.ImageData = (byte[])dr["ImageData"];
                    }
                    TypePointe pointe = new TypePointe((Int32)dr["numtypepointe"], (String)dr["libelletypepointe"]);
                    pro.LaPointe = pointe;
                    Categorie cat = new Categorie((Int32)dr["numcategorie"], (String)dr["libellecategorie"]);
                    Type type = new Type((Int32)dr["numtype"], cat, (String)dr["libelletype"]);
                    pro.leType = type;
                    using (NpgsqlCommand cmdSelect2 = new NpgsqlCommand("select * from couleurproduit cp WHERE cp.numproduit=@id;"))
                    {
                        cmdSelect2.Parameters.AddWithValue("id", pro.Numproduit);
                        DataTable dt2 = DataAccess.Instance.ExecuteSelect(cmdSelect2);
                        List<Couleur> couleursProd = new List<Couleur>();
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            couleur = desCouleurs.Find(x => x.NumCouleur == (int)dr2["numcouleur"]);
                            pro.LesCouleurs.Add(couleur);
                            couleursProd.Add(couleur);
                        }
                        foreach (Couleur coul in ToutesLesCouleurs)
                        {
                            bool present = false;
                            foreach (Couleur coul2 in couleursProd)
                                if (coul.NumCouleur == coul2.NumCouleur)
                                {
                                    present = true;
                                }
                            pro.CouleursUtilisees.Add(coul, present);
                        }
                    }
                    
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

        /// <summary>
        /// Mise à jour du produit dans la base de données.
        /// </summary>
        /// <returns>Nombre de lignes affectées.</returns>
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set numtype=@numtype, numtypepointe=@numtypepointe, codeproduit=@codeproduit, nomproduit=@nomproduit, prixvente=@prixvente, quantitestock=@quantitestock, disponible=@disponible, numproduit=@numproduit, imagedata=@imagedata WHERE numproduit=@numproduit;"))
            {
                cmdUpdate.Parameters.AddWithValue("numtype", this.LeType.NumType);
                cmdUpdate.Parameters.AddWithValue("numtypepointe", this.LaPointe.NumTypePointe);
                cmdUpdate.Parameters.AddWithValue("codeproduit", this.codeProduit);
                cmdUpdate.Parameters.AddWithValue("nomproduit", this.NomProduit);
                cmdUpdate.Parameters.AddWithValue("prixvente", this.prixVente);
                cmdUpdate.Parameters.AddWithValue("quantitestock", this.QuantiteStock);
                cmdUpdate.Parameters.AddWithValue("disponible", this.Disponible);
                cmdUpdate.Parameters.Add("imagedata", NpgsqlDbType.Bytea).Value = (object)this.ImageData ?? DBNull.Value;
                cmdUpdate.Parameters.AddWithValue("numproduit", this.Numproduit);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        /// <summary>
        /// Ajoute une couleur au produit dans la base.
        /// </summary>
        public void AjouterCouleur(Couleur uneCouleur)
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into couleurproduit (numproduit,numcouleur) values (@numproduit,@numcouleur) Returning numproduit"))
            {
                cmdInsert.Parameters.AddWithValue("numproduit", this.Numproduit);
                cmdInsert.Parameters.AddWithValue("numcouleur", uneCouleur.NumCouleur);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }            
        }

        /// <summary>
        /// Supprime une couleur du produit dans la base.
        /// </summary>
        public int SupprimerCouleur(Couleur uneCouleur)
        {
            this.LesCouleurs.Remove(uneCouleur);
            int nb = 0;
            using (var cmdDelete = new NpgsqlCommand("delete from couleurproduit  where numcouleur =@numcouleur;"))
            {
                cmdDelete.Parameters.AddWithValue("numcouleur", uneCouleur.NumCouleur);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }
    }
}
