using Pilot.Classes;
using Pilot.UC;
using Pilot.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum Action { Modifier, Créer };

    public partial class MainWindow : Window
    {
        public RoleEmploye Role;
        public MainWindow()
        {

            InitializeComponent();
            Authentification();
            if (Role == RoleEmploye.Commercial)
                MainContent.Content = new UserControlConsulterCommandes();
            else if (Role == RoleEmploye.ResponsableProduction)
                MainContent.Content = new UserControlConsulterProduit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void Authentification()
        {
            AuthentificationWindow authWindow = new AuthentificationWindow();
            bool? result = authWindow.ShowDialog();
            //A modifier
            result = true;
            if (result == true)
            {
                Role = authWindow.Role;
                MessageBox.Show("Authentification réussie", "Authentification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Authentification annulée, fermuture de l'application","Annulation",MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        

        private void menuCommande_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlConsulterCommandes();
        }

        private void menuProduits_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlSelectionnerProduit();
        }

        private void menuRevendeurs_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlRechercheRevendeur();
        }

        private void menuProduit2_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlConsulterProduit();
        }

    }
}