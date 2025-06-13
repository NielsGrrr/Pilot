using Pilot.Classes;
using Pilot.UC;
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
    /// Logique d'interaction pour WindowCommande.xaml
    /// </summary>
    public partial class WindowCommande : Window
    {
        public ObservableCollection<Revendeur> LesRevendeurs { get; set; }
        public ObservableCollection<Employe> LesEmployes { get; set; }
        public ObservableCollection<ModeTransport> LesTransports { get; set; }

        public WindowCommande(Commande uneCommande, Action act)
        {
            ChargeData();
            this.DataContext = uneCommande;
            InitializeComponent();
            butValiderCommande.Content = act;
            
        }

        private void ChargeData()
        {
            try
            {
                LesEmployes = new ObservableCollection<Employe>(new Employe().FindBySelection("libellerole = 'Commercial'"));
                LesRevendeurs = new ObservableCollection<Revendeur>(new Revendeur().FindAll());
                LesTransports = new ObservableCollection<ModeTransport>(new ModeTransport().FindAll());
                comboModeLivraison.ItemsSource = LesTransports;
                dgEmploye.ItemsSource = LesEmployes;
                dgRevendeurs.ItemsSource = LesRevendeurs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedItem == null || dgRevendeurs.SelectedItem == null || comboModeLivraison.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé, un revendeur et un mode de livraison.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Console.WriteLine(this.DataContext);
                bool ok = true;
                foreach (UIElement uie in panelFormCommande.Children)
                {
                    if (uie is TextBox)
                    {
                        TextBox txt = (TextBox)uie;
                        txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    }
                    if (Validation.GetHasError(uie))
                        ok = false;

                }
                //Console.WriteLine(this.DataContext);
                try
                {
                    // Ajouter la commande à la liste des commandes
                    MessageBox.Show("Commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La commande n'a pas pu être ajoutée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = ok;
            }
        }
    }
}
