using Pilot.Classes;
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
using System.Windows.Shapes;

namespace Pilot.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowAjouterProduit.xaml
    /// </summary>
    public partial class WindowAjouterProduit : Window
    {
        public ObservableCollection<Classes.Type> LesTypes {  get; set; }
        public ObservableCollection<TypePointe> LesPointes { get; set; }
        public WindowAjouterProduit(Produit unProduit, Action action)
        {
            ChargeData();
            InitializeComponent();
            butValider.Content = action;
            comboType.ItemsSource = LesTypes;
            comboPointe.ItemsSource = LesPointes;
        }

        public WindowAjouterProduit(Produit unProduit)
        {
            ChargeData();
            InitializeComponent();
        }

        private void ChargeData()
        {
            LesTypes = new ObservableCollection<Classes.Type>(new Classes.Type().FindAll());
            LesPointes = new ObservableCollection<TypePointe>(new TypePointe().FindAll());
            TypePointe unePointe = new TypePointe(1, "nom pointe");
            Categorie uneCategorie = new Categorie(1, "nom Categorie");
            Classes.Type unType = new Classes.Type(1, uneCategorie, "nom type");
            Produit unProduit = new Produit(1, unePointe, unType, "lecode", "nom produit", 12, 5, true);
            this.DataContext = unProduit;
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            Classes.Type leType = (Classes.Type)comboType.SelectedItem;
            TypePointe pointe = (TypePointe)comboPointe.SelectedItem;
            //Attention au Parse
            Produit produit = new Produit(pointe, leType, txtCodeProduit.Text, txtNomProduit.Text, decimal.Parse(txtPrixVente.Text), int.Parse(txtQuantite.Text), (bool)checkDisponible.IsChecked);
            produit.Create();
            DialogResult = true;
            
        }
    }
}
