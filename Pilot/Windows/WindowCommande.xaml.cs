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
        public Commande UneCommande { get; set; }

        public WindowCommande(Commande uneCommande, Action act)
        {
            UneCommande = uneCommande;
            ChargeData();
            this.DataContext = this;
            InitializeComponent();
            butValiderCommande.Content = act;
            comboModeLivraison.ItemsSource = LesTransports;
        }

        private void ChargeData()
        {
            try
            {
                LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
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

        private void butValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            Employe employe = (Employe)dgEmploye.SelectedItem;
            Revendeur revendeur = (Revendeur)dgRevendeurs.SelectedItem;
            ModeTransport mt = (ModeTransport)comboModeLivraison.SelectedItem;
            //Attention au Parse
            Commande uneCommande = new Commande(employe, mt, revendeur, DateTime.Parse(txtDateLivraison.Text));
            try
            {
                // Ajouter la commande à la liste des commandes
                MessageBox.Show("Commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("La commande n'a pas pu être ajoutée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogResult = true;
        }
    }
}
