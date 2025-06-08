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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilot.UC
{
    /// <summary>
    /// Logique d'interaction pour UserControlConsulterCommandes.xaml
    /// </summary>
    public partial class UserControlConsulterCommandes : UserControl
    {
        public UserControlConsulterCommandes()
        {
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

        private void tbMotClefCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgCommande != null)
                CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
        }
    }
}
