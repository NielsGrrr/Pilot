using Pilot.Classes;
using Pilot.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour UserControlConsulterProduit.xaml
    /// Stocke 1 informations :
    /// 1 ObservableCollection<Produit> : Tous les produits de la base de données
    /// Le UC est définit comme DataContext
    /// </summary>
    public partial class UserControlConsulterProduit : UserControl
    {
        public ObservableCollection<Produit> LesProduits { get; set; }
        public UserControlConsulterProduit()
        {
            ChargeData();
            InitializeComponent();
            dgProduits.Items.Filter = RechercherMotCle;
        }

        /// <summary>
        /// Recherche si les produits correspondent aux critères de recherche saisis dans les TextBox
        /// </summary>
        /// /// <returns>Si les produits correspondent aux critères de recherche saisis dans les TextBox</returns>
        private bool RechercherMotCle(object obj)
        {
            if (String.IsNullOrEmpty(tbMotCle.Text) && String.IsNullOrEmpty(tbType.Text) && String.IsNullOrEmpty(tbTypePointe.Text) && String.IsNullOrEmpty(tbCategorie.Text))
                return true;
            Produit produit = obj as Produit;
            return (produit.CodeProduit.StartsWith(tbMotCle.Text, StringComparison.OrdinalIgnoreCase) && produit.LeType.LibelleType.StartsWith(tbType.Text, StringComparison.OrdinalIgnoreCase) && produit.LaPointe.LibelleTypePointe.StartsWith(tbTypePointe.Text, StringComparison.OrdinalIgnoreCase) && produit.LeType.LaCategorie.LibelleCategorie.StartsWith(tbCategorie.Text, StringComparison.OrdinalIgnoreCase));
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
        /// Ouvre la window AjouterProduit lié au produit sélectionné lorsque le bouton AjouterProduit est cliqué
        /// </summary>
        private void butAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            Produit unProduit = new Produit();
            unProduit.LaPointe = new TypePointe();
            unProduit.LeType = new Classes.Type();
            WindowAjouterProduit wProduit = new WindowAjouterProduit(unProduit, Action.Créer);
            bool? result = wProduit.ShowDialog();
            if (result == true)
            {
                try
                {
                    LesProduits.Add(unProduit);
                    unProduit.Create();
                    MessageBox.Show("Produit ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le produit n'a pas pu être ajoutée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la window SupprimerProduit lié au produit sélectionné lorsque le bouton SupprimerProduit est cliqué
        /// </summary>
        private void butSupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem ==  null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Produit produitASupprimer = (Produit)dgProduits.SelectedItem;
                Produit copie = new Produit(produitASupprimer.Numproduit, produitASupprimer.LaPointe, produitASupprimer.LeType, produitASupprimer.CodeProduit, produitASupprimer.NomProduit, produitASupprimer.PrixVente, produitASupprimer.QuantiteStock, produitASupprimer.Disponible);
                try
                {
                    produitASupprimer.Delete();
                    LesProduits.Remove(produitASupprimer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "Le produit n'a pas pu être supprimé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Lorsque le texte de la textbox change, on refresh le contenu du dataGrid dgProduits
        /// </summary>
        private void tbCategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbMotCle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbTypePointe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        private void tbType_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgProduits != null)
                CollectionViewSource.GetDefaultView(dgProduits.ItemsSource).Refresh();
        }

        /// <summary>
        /// Ouvre la window ModifierProduit lié au produit sélectionné lorsque le bouton ModifierProduit est cliqué
        /// </summary>
        private void butModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Produit produitSelectionne = (Produit)dgProduits.SelectedItem;
                Produit copie = new Produit(produitSelectionne.Numproduit, produitSelectionne.LaPointe, produitSelectionne.LeType, produitSelectionne.CodeProduit, produitSelectionne.NomProduit, produitSelectionne.PrixVente, produitSelectionne.QuantiteStock, produitSelectionne.Disponible, produitSelectionne.LesCouleurs, produitSelectionne.CouleursUtilisees);
                WindowAjouterProduit wBox = new WindowAjouterProduit(copie, Action.Modifier);
                bool? result = wBox.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        produitSelectionne.Numproduit = copie.Numproduit;
                        produitSelectionne.LaPointe = copie.LaPointe;
                        produitSelectionne.LeType = copie.LeType;
                        produitSelectionne.CodeProduit = copie.CodeProduit;
                        produitSelectionne.NomProduit = copie.NomProduit;
                        produitSelectionne.PrixVente = copie.PrixVente;
                        produitSelectionne.QuantiteStock = copie.QuantiteStock;
                        produitSelectionne.Disponible = copie.Disponible;
                        produitSelectionne.LesCouleurs = copie.LesCouleurs;
                        produitSelectionne.ImageData = copie.ImageData;
                        produitSelectionne.Update();
                        dgProduits.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le produit n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
