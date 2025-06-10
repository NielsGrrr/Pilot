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
    /// Logique d'interaction pour UserControlConsulterCommandes.xaml
    /// </summary>
    

    public partial class UserControlConsulterCommandes : UserControl
    {
        public ObservableCollection<Commande> LesCommandes { get; set; }
        public Commande commandeSelectionnee { get; set; }
        public UserControlConsulterCommandes()
        {
            ChargeData();
            InitializeComponent();
            dgCommande.Items.Filter = RechercheMotClefCommande;
        }

        private bool RechercheMotClefCommande(object obj)
        {
            if (String.IsNullOrEmpty(tbMotClefCommande.Text))
                return true;
            string motClef = tbMotClefCommande.Text.ToLower();
            Commande uneCommande = obj as Commande;
            return (uneCommande.UnRevendeur.RaisonSociale.StartsWith(tbMotClefCommande.Text, StringComparison.OrdinalIgnoreCase));
        }

        private void ChargeData()
        {
            try
            {
                LesCommandes = new ObservableCollection<Commande>(new Commande().FindAll());
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        private void tbMotClefCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgCommande != null)
                CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
        }

        private void butAjouterCommande_Click(object sender, RoutedEventArgs e)
        {
            Commande uneCommande = new Commande();
            WindowCommande wCommande = new WindowCommande(uneCommande, Action.Créer);
            bool? result = wCommande.ShowDialog();
            if (result == true)
            {
                try
                {
                    // Ajouter la commande à la liste des commandes
                    uneCommande.Create();
                    MessageBox.Show("Commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être ajoutée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butModifierCommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommande.SelectedItem == null) 
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Commande commandeSelectionnee = (Commande)dgCommande.SelectedItem;
                Commande copie = new Commande(commandeSelectionnee.NumCommande, commandeSelectionnee.Employe, commandeSelectionnee.UnTransport, commandeSelectionnee.UnRevendeur, commandeSelectionnee.DateLivraison);
                WindowCommande wCommande = new WindowCommande(copie, Action.Modifier);
                bool? result = wCommande.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        commandeSelectionnee.NumCommande = copie.NumCommande;
                        commandeSelectionnee.Employe = copie.Employe;
                        commandeSelectionnee.UnTransport = copie.UnTransport;
                        commandeSelectionnee.UnRevendeur = copie.UnRevendeur;
                        commandeSelectionnee.DateCommande = copie.DateCommande;
                        commandeSelectionnee.DateLivraison = copie.DateLivraison;

                    }catch (Exception ev)
                    {
                        MessageBox.Show("La commande n'a pas pu être modifiée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void butSupprimerCommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommande.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Commande commandeSelectionnee = (Commande)dgCommande.SelectedItem;
                Commande copie = new Commande(commandeSelectionnee.NumCommande, commandeSelectionnee.Employe, commandeSelectionnee.UnTransport, commandeSelectionnee.UnRevendeur, commandeSelectionnee.DateLivraison);
                try
                {
                    commandeSelectionnee.Delete();
                }catch(Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être supprimée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butDetailCommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommande.SelectedItem is null)
            {
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                commandeSelectionnee = (Commande)dgCommande.SelectedItem;
                UserControlDetailCommande userControlDetailCommande = new UserControlDetailCommande(commandeSelectionnee);
            }
        }

    }
}
