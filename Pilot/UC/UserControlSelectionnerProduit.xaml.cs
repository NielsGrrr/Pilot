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

        private void butAjouterProduitACommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Produit unProduit = (Produit)dgProduits.SelectedItem;
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
                            //A terminer
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
