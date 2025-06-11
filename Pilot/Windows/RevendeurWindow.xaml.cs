using Pilot.Classes;
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
using System.Windows.Shapes;

namespace Pilot.Windows
{
    /// <summary>
    /// Logique d'interaction pour RevendeurWindow.xaml
    /// </summary>
    public partial class RevendeurWindow : Window
    {
        public RevendeurWindow(Revendeur copie, Action action)
        {
            this.DataContext = copie;
            InitializeComponent();
            but.Content = action.ToString();
        }
        private void but_Click(object sender, RoutedEventArgs e)
        {
            //Revendeur revendeur = new Revendeur(((ComboBoxItem)cmbFormeJuridique.SelectedItem).Content.ToString() + " " + txtNomRevendeur.Text, txtAdresse.Text, txtCodePostal.Text, txtVille.Text);
            //revendeur.Create();
            //DialogResult = true;
            bool ok = true;
            foreach (UIElement uie in panelFormRevendeur.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
                else if (uie is ComboBox)
                {

                }
                if (Validation.GetHasError(uie))
                    ok = false;
            }
            DialogResult = ok;
        }
    }
}
