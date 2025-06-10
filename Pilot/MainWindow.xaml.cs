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
            //AjouterProduit();
            Authentification();
            
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
                authWindow.Role = Role;
                MessageBox.Show("Authentification réussie", "Authentification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Authentification annulée, fermuture de l'application","Annulation",MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjouterProduit()
        {
            Produit unProduit = new Produit();
            WindowAjouterProduit wProduit = new WindowAjouterProduit(unProduit);
            bool? result = wProduit.ShowDialog();
            if (result == true)
            {
                try
                {
                    unProduit.Create();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Le produit n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            Produit unProduit = new Produit();
            WindowAjouterProduit wProduit = new WindowAjouterProduit(unProduit);
            bool? result = wProduit.ShowDialog();
            if (result == true)
            {
                try
                {
                    unProduit.Create();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Le produit n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        public void DetailCommande(Commande c)
        {
            MainContent.Content = new UserControlDetailCommande(c);
        }

        private void menuProduit2_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlConsulterProduit();
        }
    }
}