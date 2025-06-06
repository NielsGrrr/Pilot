using Pilot.Classes;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Authentification();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void Authentification()
        {
            AuthWindow authWindow = new AuthWindow();
            bool? result = authWindow.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Authentification réussie", "Authentification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Authentification annulée, fermuture de l'application","Annulation",MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
    }
}