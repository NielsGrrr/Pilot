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
            labT.Content = action.ToString() + labT.Content;
            but.Content = action.ToString();
        }
        private void but_Click(object sender, RoutedEventArgs e)
        {
            int cp;
            if (txtRaisonSociale.Text.Length == 0 || txtAdresse.Text.Length == 0 || txtVille.Text.Length == 0)
                MessageBox.Show("Les champs ne sont pas remplis", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (txtCodePostal.Text.Length != 5 )
                MessageBox.Show("Le code postal doit être de longueur 5", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                bool ok = true;
                foreach (UIElement uie in panelFormRevendeur.Children)
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
