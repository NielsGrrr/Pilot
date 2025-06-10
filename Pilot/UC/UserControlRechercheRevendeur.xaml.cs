using Pilot.Classes;
using Pilot.Windows;
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
    /// Logique d'interaction pour UserControlRechercheRevendeur.xaml
    /// </summary>
    public partial class UserControlRechercheRevendeur : UserControl
    {
        public ObservableCollection<Revendeur> LesRevendeurs { get; set; }
        public UserControlRechercheRevendeur()
        {
            ChargeData();
            InitializeComponent();
        }
        public void ChargeData()
        {
            try
            {
                LesRevendeurs = new ObservableCollection<Revendeur>(new Revendeur().FindAll());
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        private void butAjouterRevendeur_Click(object sender, RoutedEventArgs e)
        {
            RevendeurWindow revendeurWindow = new RevendeurWindow();
            bool? result = revendeurWindow.ShowDialog();
        }
    }
}
