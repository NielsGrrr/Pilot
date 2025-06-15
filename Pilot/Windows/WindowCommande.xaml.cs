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
    /// Stocke 3 informations :
    /// 1 ObservableCollection<Revendeur> : Tous les revendeurs de la base de données dont le rôle est commercial
    /// 1 ObservableCollection<Employe> : Tous les employés de la base de données
    /// 1 ObservableCollection<Transport> : Tous les transports de la base de données
    /// La commande sélectionnée est définit comme DataContext
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
            labT.Content = act.ToString() + labT.Content;
            butValiderCommande.Content = act;
            comboModeLivraison.ItemsSource = LesTransports;
            dgEmploye.ItemsSource = LesEmployes;
            dgRevendeurs.ItemsSource = LesRevendeurs;
        }

        private void ChargeData()
        {
            try
            {
                LesEmployes = new ObservableCollection<Employe>(new Employe().FindBySelection("libellerole = 'Commercial'"));
                LesRevendeurs = new ObservableCollection<Revendeur>(new Revendeur().FindAll());
                LesTransports = new ObservableCollection<ModeTransport>(new ModeTransport().FindAll());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Vérifie les champs du formulaire et valide le dialogue
        /// </summary>
        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            // Vérification que les champs obligatoires sont remplis
            if (dgEmploye.SelectedItem == null || dgRevendeurs.SelectedItem == null || comboModeLivraison.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé, un revendeur et un mode de livraison.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool ok = true;
                foreach (UIElement uie in panelFormCommande.Children)
                {
                    if (uie is TextBox)
                    {
                        TextBox txt = (TextBox)uie;
                        txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    }
                    if (uie is DatePicker)
                    {
                        DatePicker datePicker = (DatePicker)uie;
                        datePicker.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
                    }
                    if (Validation.GetHasError(uie))
                        ok = false;

                }
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

        private void butAjouterRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Revendeur revendeur = new Revendeur();
            RevendeurWindow revendeurWindow = new RevendeurWindow(revendeur, Action.Créer);
            bool? result = revendeurWindow.ShowDialog();
            if (result == true)
            {
                try
                {
                    revendeur.NumRevendeur = revendeur.Create();
                    LesRevendeurs.Add(revendeur);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le revendeur n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
