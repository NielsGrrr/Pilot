using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pilot.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowAjouterProduitCommande.xaml
    /// Stocke 2 informations :
    /// 1 ObservableCollection<Commande> : Toutes les commandes de la base de données
    /// 1 produit : Le produit sélectionné
    /// La window est définit comme DataContext
    /// </summary>
    public partial class WindowAjouterProduitCommande : Window
    {
        public ObservableCollection<Commande> LesCommandes { get; set; }
        public Produit produit { get; set; }
        public WindowAjouterProduitCommande()
        {
            ChargeData();
            InitializeComponent();
        }

        public WindowAjouterProduitCommande(Produit unProduit)
        {
            ChargeData();
            InitializeComponent();
            produit = unProduit;
            labPrixUnitaire.Content = unProduit.PrixVente;
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

        /// <summary>
        /// Vérifie les champs du formulaire et valide le dialogue
        /// </summary>
        private void butValiderProduitCommande_Click(object sender, RoutedEventArgs e)
        {
            Commande uneCommande = (Commande)dgCommande.SelectedItem;
            Commande laCommande = uneCommande.FindNumCommande();
            int quantite;
            // Vérifie si la quantité est un nombre valide sinon affiche un message d'erreur
            if (int.TryParse(tbQuantite.Text, out quantite) == false)
            {
                MessageBox.Show("Veuillez saisir une quantité valide", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // Vérifie si une commande est sélectionnée sinon affiche un message d'erreur
            else if (dgCommande.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Vérifie si la quantité est supérieure à 0 sinon affiche un message d'erreur
            else if (String.IsNullOrWhiteSpace(tbQuantite.Text) || quantite <= 0)
            {
                MessageBox.Show("Veuillez saisir une quantité supérieure à 0", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                laCommande.AjouterProduit(produit, quantite);
                DialogResult = true;
            }
            
        }
    }
}
