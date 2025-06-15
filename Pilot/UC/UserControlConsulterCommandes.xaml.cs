using Pilot.Classes;
using Pilot.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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
    /// Stocke 2 informations :
    /// 1 ObservableCollection<Commande> : Toutes les commandes de la base de données
    /// 1 commande : La commande sélectionnée
    /// Le UC est définit comme DataContext
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

        /// <summary>
        /// Recherche si la raison sociale du revendeur débute bien par le mot clé saisi dans la textbox tbMotClefCommande
        /// </summary>
        /// /// <returns>Si la raison sociale du revendeur débute bien par le mot clé saisi</returns>
        private bool RechercheMotClefCommande(object obj)
        {
            if (String.IsNullOrEmpty(tbMotClefCommande.Text))
                return true;
            string motClef = tbMotClefCommande.Text.ToLower();
            Commande uneCommande = obj as Commande;
            return (uneCommande.UnRevendeur.RaisonSociale.StartsWith(tbMotClefCommande.Text, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Chare les données du UC
        /// </summary>
        private void ChargeData()
        {
            try
            {
                //Récupère toutes les commandes de la base de données
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

        /// <summary>
        /// Lorsque le texte de la textbox change, on refresh le contenu du dataGrid dgCommande
        /// </summary>
        private void tbMotClefCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgCommande != null)
                CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
        }

        /// <summary>
        /// Ouvre la window AjouterCommande lié à la commande sélectionnée lorsque le bouton AjouterCommande est cliqué
        /// </summary>
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
                    LesCommandes.Add(uneCommande);
                    MessageBox.Show("Commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être ajoutée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// Ouvre la window ModifierCommande lié à la commande sélectionnée lorsque le bouton ModifierCommande est cliqué
        /// </summary>
        private void butModifierCommande_Click(object sender, RoutedEventArgs e)
        {
            //On modifie uniquement si une commande a été sélectionnée
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
                        commandeSelectionnee.Update();
                        dgCommande.Items.Refresh();
                    }catch (Exception ev)
                    {
                        MessageBox.Show("La commande n'a pas pu être modifiée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Supprime la commande sélectionnée lorsque le bounton supprimé est cliqué
        /// </summary>
        private void butSupprimerCommande_Click(object sender, RoutedEventArgs e)
        {
            //On supprime uniquement si une commande a été sélectionnée
            if (dgCommande.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Commande commandeSelectionnee = (Commande)dgCommande.SelectedItem;
                Commande copie = new Commande(commandeSelectionnee.NumCommande, commandeSelectionnee.Employe, commandeSelectionnee.UnTransport, commandeSelectionnee.UnRevendeur, commandeSelectionnee.DateLivraison);
                try
                {
                    commandeSelectionnee.Delete();
                    LesCommandes.Remove(commandeSelectionnee);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être supprimée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la window DetailCommand lié à la commande sélectionnéelorsque le bouton DetailCommande est cliqué
        /// </summary>
        private void butDetailCommande_Click(object sender, RoutedEventArgs e)
        {
            //On affiche les détails uniquement si une commande a été sélectionnée
            if (dgCommande.SelectedItem is null)
            {
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                commandeSelectionnee = (Commande)dgCommande.SelectedItem;
                UserControlDetailCommande userControlDetailCommande = new UserControlDetailCommande(commandeSelectionnee);
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MainContent.Content = userControlDetailCommande;
                }
            }
        }
    }
}
