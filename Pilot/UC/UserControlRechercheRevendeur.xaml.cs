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
    /// Stocke 1 informations :
    /// 1 ObservableCollection<Revendeur> : Tous les revendeurs de la base de données
    /// Le UC est définit comme DataContext
    /// </summary>
    public partial class UserControlRechercheRevendeur : UserControl
    {
        public ObservableCollection<Revendeur> LesRevendeurs { get; set; }
        public UserControlRechercheRevendeur()
        {
            ChargeData();
            InitializeComponent();
            dgRevendeur.Items.Filter = RechercheMotClefRevendeur;
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

        /// <summary>
        /// Ouvre la window AjouterRevendeur lié au revendeur sélectionné lorsque le bouton AjouterRevendeur est cliqué
        /// </summary>
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

        /// <summary>
        /// Ouvre la window ModifierRevendeur lié au revendeur sélectionné lorsque le bouton ModifierRevendeur est cliqué
        /// </summary>
        private void butModifierRevendeur_Click(object sender, RoutedEventArgs e)
        {
            if (dgRevendeur.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un chien", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Revendeur revendeurSelectionne = (Revendeur)dgRevendeur.SelectedItem;
                Revendeur copie = new Revendeur(revendeurSelectionne.NumRevendeur, revendeurSelectionne.RaisonSociale, revendeurSelectionne.AdresseRue, revendeurSelectionne.AdresseCP, revendeurSelectionne.AdresseVille);
                RevendeurWindow wBox = new RevendeurWindow(copie, Action.Modifier);
                bool? result = wBox.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        revendeurSelectionne.RaisonSociale = copie.RaisonSociale;
                        revendeurSelectionne.AdresseRue = copie.AdresseRue;
                        revendeurSelectionne.AdresseVille = copie.AdresseVille;
                        revendeurSelectionne.AdresseCP = copie.AdresseCP;
                        revendeurSelectionne.Update();
                        dgRevendeur.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le revendeur n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Recherche si les revendeurs correspondent aux critères de recherche saisis dans les TextBox
        /// </summary>
        /// /// <returns>Si les revendeurs correspondent aux critères de recherche saisis dans les TextBox</returns>
        private bool RechercheMotClefRevendeur(object obj)
        {
            if (String.IsNullOrEmpty(tbMotClefRevendeur.Text))
                return true;
            Revendeur rev = obj as Revendeur;
            return (rev.RaisonSociale.StartsWith(tbMotClefRevendeur.Text, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Lorsque le texte de la textbox change, on refresh le contenu du dataGrid dgRevendeur
        /// </summary>
        private void tbMotClefRevendeur_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgRevendeur.ItemsSource).Refresh();
        }

        /// <summary>
        /// Ouvre la window SupprimerRevendeur lié au revendeur sélectionné lorsque le bouton SupprimerRevendeur est cliqué
        /// </summary>
        private void butSupprimerRevendeur_Click(object sender, RoutedEventArgs e)
        {
            if (dgRevendeur.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un revendeur", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Revendeur revendeurASupprimer = (Revendeur)dgRevendeur.SelectedItem;
                Revendeur copie = new Revendeur(revendeurASupprimer.NumRevendeur, revendeurASupprimer.RaisonSociale, revendeurASupprimer.AdresseRue, revendeurASupprimer.AdresseCP, revendeurASupprimer.AdresseVille);
                try
                {
                    revendeurASupprimer.Delete();
                    LesRevendeurs.Remove(revendeurASupprimer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le revendeur n'a pas pu être supprimé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
