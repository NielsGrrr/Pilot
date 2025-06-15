using Pilot.Classes;
using Pilot.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilot.UC
{
    /// <summary>
    /// Logique d'interaction pour UserControlSelectionnerProduit.xaml
    /// Stocke 1 informations :
    /// 1 ObservableCollection<Produit> : Tous les produits de la base de données
    /// Le UC est définit comme DataContext
    /// </summary>
    public partial class UserControlSelectionnerProduit : UserControl
    {
        public ObservableCollection<Produit> LesProduits { get; set; }
        public UserControlSelectionnerProduit()
        {
            ChargeData();
            InitializeComponent();
            dgProduits.Items.Filter = RechercherMotCle;
        }

        /// <summary>
        /// Ouvre la window AjouterProduitCommande lié au produit sélectionné lorsque le bouton AjouterProduitCommande est cliqué
        /// </summary>
        private void butAjouterProduitACommande_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie si un produit est sélectionné
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Produit unProduit = (Produit)dgProduits.SelectedItem;
                //Vérifie si le produit est disponible
                if (!unProduit.Disponible)
                {
                    MessageBox.Show("Le produit est indisponible", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Produit copie = new Produit(unProduit.Numproduit, unProduit.LaPointe, unProduit.LeType, unProduit.CodeProduit, unProduit.NomProduit, unProduit.PrixVente, unProduit.QuantiteStock, unProduit.Disponible);
                    WindowAjouterProduitCommande wProduit = new WindowAjouterProduitCommande(copie);
                    bool? result = wProduit.ShowDialog();
                    if (result == true)
                    {
                        try
                        {
                            unProduit.Numproduit = copie.Numproduit;
                            unProduit.NomProduit = copie.NomProduit;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Le produit n'a pas pu être ajouté à la commande.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                
            }
        }

        private void ChargeData()
        {
            try
            {
                LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Recherche si les produits correspondent aux critères de recherche saisis dans les TextBox
        /// </summary>
        /// /// <returns>Si les produits correspondent aux critères de recherche saisis dans les TextBox</returns>
        private bool RechercherMotCle(object obj)
        {
            if (String.IsNullOrEmpty(tbMotCle.Text) && String.IsNullOrEmpty(tbType.Text) && String.IsNullOrEmpty(tbTypePointe.Text) && String.IsNullOrEmpty(tbCategorie.Text) && String.IsNullOrEmpty(tbCouleur.Text))
                return true;
            Produit produit = obj as Produit;
            bool couleurPresente = false;
            foreach (Couleur cou in produit.LesCouleurs)
            {
                if (cou != null)
                {
                    if (cou.LibelleCouleur.StartsWith(tbCouleur.Text, StringComparison.OrdinalIgnoreCase))
                        couleurPresente = true;
                }
            }
            return (produit.CodeProduit.StartsWith(tbMotCle.Text, StringComparison.OrdinalIgnoreCase) && produit.LeType.LibelleType.StartsWith(tbType.Text, StringComparison.OrdinalIgnoreCase) && produit.LaPointe.LibelleTypePointe.StartsWith(tbTypePointe.Text, StringComparison.OrdinalIgnoreCase) && produit.LeType.LaCategorie.LibelleCategorie.StartsWith(tbCategorie.Text, StringComparison.OrdinalIgnoreCase) && couleurPresente);
        }

        /// <summary>
        /// Lorsque le texte de la textbox change, on refresh le contenu du dataGrid dgProduits
        /// </summary>
        private void tbMotCle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbType_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbTypePointe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbCategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbCouleur_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }
    }
}
