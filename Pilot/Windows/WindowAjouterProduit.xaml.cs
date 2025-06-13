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
        public ObservableCollection<Classes.Type> LesTypes { get; set; }
        public ObservableCollection<TypePointe> LesPointes { get; set; }
        public WindowAjouterProduit(Produit unProduit, Action action)
        {
            ChargeData();
            this.DataContext = unProduit;
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
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodeProduit.Text == null)
            {
                MessageBox.Show("Veuillez saisir uncode produit valide", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
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
                DialogResult = ok;
            }
            

        }
    }
}
