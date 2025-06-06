using Pilot.Classes;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Authentification();
            AjouterProduit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void Authentification()
        {
            AuthWindow authWindow = new AuthWindow();
            bool? result = authWindow.ShowDialog();
        }

        private void AjouterProduit()
        {
            
            WindowAjouterProduit windowAjout = new WindowAjouterProduit();
            bool? result = windowAjout.ShowDialog();
        }
    }
}