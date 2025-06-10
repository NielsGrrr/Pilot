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
        public WindowAjouterProduit(Produit unProduit)
        {
            List<Classes.Type> lesTypes = new List<Classes.Type> { new Classes.Type(1, new Categorie(1, "Cat1"), "typ1"), new Classes.Type(2, new Categorie(1, "Cat1"), "typ2"), new Classes.Type(3, new Categorie(1, "Cat4"), "typ3") };
            List<TypePointe> lesPointes = new List<TypePointe> { new TypePointe(1, "Fine"), new TypePointe(2, "Moyenne"), new TypePointe(3, "Large"), new TypePointe(4, "Xtra large") };
            ObservableCollection<Classes.Type>  lesTypes2 = new ObservableCollection<Classes.Type>(new Classes.Type().FindAll());
            ChargeData();
            InitializeComponent();
            comboType.ItemsSource = lesTypes2;
            comboPointe.ItemsSource = lesPointes;
        }

        private void ChargeData()
        {
            TypePointe unePointe = new TypePointe(1, "nom pointe");
            Categorie uneCategorie = new Categorie(1, "nom Categorie");
            Classes.Type unType = new Classes.Type(1, uneCategorie, "nom type");
            Produit unProduit = new Produit(1, unePointe, unType, "lecode", "nom produit", 12, 5, true);
            this.DataContext = unProduit;
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            bool ok = true;
            foreach (UIElement uie in panelFormProduit.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
                if (Validation.GetHasError(uie)) 
                    ok = false;
            }
        }
    }
}
