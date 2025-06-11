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
    /// </summary>
    public partial class UserControlConsulterProduit : UserControl
    {
        public ObservableCollection<Produit> LesProduits { get; set; }
        public UserControlConsulterProduit()
        {
            ChargeData();
            InitializeComponent();
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
                    unProduit.Create();
                    MessageBox.Show("Produit ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le produit n'a pas pu être ajoutée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "Le produit n'a pas pu être supprimé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
