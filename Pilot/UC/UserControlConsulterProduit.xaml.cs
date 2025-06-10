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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilot.UC
{
    /// <summary>
    /// Logique d'interaction pour UserControlConsulterProduit.xaml
    /// </summary>
    public partial class UserControlConsulterProduit : UserControl
    {
        public ObservableCollection<Produit> LesProduits { get; set; }
        public UserControlConsulterProduit()
        {
            ChargeData();
            InitializeComponent();
        }

        private void ChargeData()
        {
            try
            {
                LesProduits = new ObservableCollection<Produit>(new Produit().FindAll());
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }
    }
}
