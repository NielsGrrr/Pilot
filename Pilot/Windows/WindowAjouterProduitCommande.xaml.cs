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

        private void butValiderProduitCommande_Click(object sender, RoutedEventArgs e)
        {
            Commande uneCommande = (Commande)dgCommande.SelectedItem;
            Commande laCommande = uneCommande.FindNumCommande();
            //Attention au Parse
            int quantite = int.Parse(tbQuantite.Text);
            laCommande.AjouterProduit(produit, quantite);
            DialogResult = true;
        }
    }
}
