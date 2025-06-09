using Pilot.Classes;
using Pilot.Windows;
using System;
using System.Collections.Generic;
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
        public UserControlSelectionnerProduit()
        {
            InitializeComponent();
        }

        private void butAjouterProduitACommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduits.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Produit unProduit = (Produit)dgProduits.SelectedItem;
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
