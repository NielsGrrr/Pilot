using Microsoft.Win32;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Logique d'interaction pour WindowAjouterProduit.xaml
    /// Stocke 5 informations :
    /// 1 ObservableCollection<Type> : Tous les types de la base de données
    /// 1 ObservableCollection<TypePointe> : Tous les types de pointes de la base de données
    /// 2 ObservableCollection<Couleur> : Toutes les couleurs de la base de données ainsi que celles du produit utilisé
    /// 1 Produit : Le produit sélectionné
    /// Le produit sélectionné est définit comme DataContext
    /// </summary>
    public partial class WindowAjouterProduit : Window
    {
        public ObservableCollection<Classes.Type> LesTypes { get; set; }
        public ObservableCollection<TypePointe> LesPointes { get; set; }
        public ObservableCollection<Couleur> LesCouleurs { get; set; }
        public ObservableCollection<Couleur> ToutesLesCouleurs { get; set; }
        public Produit prodWindow { get; set; }
        public WindowAjouterProduit(Produit unProduit, Action action)
        {
            prodWindow = unProduit;
            ChargeData();
            this.DataContext = unProduit;
            InitializeComponent();
            butValider.Content = action;
            comboType.ItemsSource = LesTypes;
            comboPointe.ItemsSource = LesPointes;
        }

        public WindowAjouterProduit(Produit unProduit)
        {
            ChargeData();
            InitializeComponent();
        }

        private void ChargeData()
        {
            LesTypes = new ObservableCollection<Classes.Type>(new Classes.Type().FindAll());
            LesPointes = new ObservableCollection<TypePointe>(new TypePointe().FindAll());
            ToutesLesCouleurs = new ObservableCollection<Couleur>(new Couleur().FindAll());
            LesCouleurs = prodWindow.LesCouleurs;
        }

        /// <summary>
        /// Vérifie les champs du formulaire et valide le dialogue
        /// </summary>
        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que le code produit n'est pas vide ou null
            if (txtCodeProduit.Text == null)
            {
                MessageBox.Show("Veuillez saisir un code produit valide", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Vérifie que tous les champs obligatoires sont remplis
            else if (String.IsNullOrWhiteSpace(txtCodeProduit.Text) || String.IsNullOrWhiteSpace(txtNomProduit.Text) || String.IsNullOrWhiteSpace(txtPrixVente.Text) || String.IsNullOrWhiteSpace(txtQuantite.Text))
                MessageBox.Show("Veuillez remplir tous les champs obligatoires", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                bool ok = true;
                foreach (UIElement uie in panelFormProduit.Children)
                {
                    if (uie is TextBox && uie != txtCouleurs)
                    {
                        TextBox txt = (TextBox)uie;
                        txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    }
                    if (Validation.GetHasError(uie))
                        ok = false;
                }
                if (ok)
                {
                    prodWindow.CodeProduit = txtCodeProduit.Text;
                }
                DialogResult = ok;
            }
        }

        /// <summary>
        /// Ajoute une couleur à la liste des couleurs du produit
        /// </summary>
        private void butAjoutCouleurs_Click(object sender, RoutedEventArgs e)
        {
            //Vérifie si le champ de couleur est vide ou null
            if (txtCouleurs.Text == null)
                MessageBox.Show("Veuillez saisir une couleur valide", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Couleur couleurAAjouter;
                Couleur uneCouleur = new Couleur(txtCouleurs.Text);
                //Si la couleur est bien une couleur existante
                if (uneCouleur.EstPresent(ToutesLesCouleurs))
                {
                    foreach (Couleur coul in ToutesLesCouleurs)
                    {
                        if (coul.LibelleCouleur == (uneCouleur.LibelleCouleur))
                            uneCouleur = coul;
                    }
                    LesCouleurs.Add(uneCouleur);
                    prodWindow.AjouterCouleur(uneCouleur);
                }
                else
                {
                    MessageBox.Show("La couleur n'existe pas", "Attetion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        /// <summary>
        /// Supprime une couleur de la liste des couleurs du produit
        /// </summary>
        private void butSupprCouleurs_Click(object sender, RoutedEventArgs e)
        {
            //Si aucune couleur n'est sélectionnée, on affiche un message d'erreur
            if (lbCouleurs.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un produit", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Couleur couleurASupprimer = (Couleur)lbCouleurs.SelectedItem;
                Couleur copie = new Couleur(couleurASupprimer.NumCouleur, couleurASupprimer.LibelleCouleur);
                try
                {
                    prodWindow.SupprimerCouleur(couleurASupprimer);
                    LesCouleurs.Remove(couleurASupprimer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le produit n'a pas pu être supprimé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butAjouterImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Sélectionnez une image";
            op.Filter = "Tous les fichiers images|*.jpg;*.jpeg;*.png;*.gif;*.bmp|JPG|*.jpg;*.jpeg|PNG|*.png|GIF|*.gif|BMP|*.bmp";

            if (op.ShowDialog() == true)
            {
                try
                {
                    prodWindow.ImageData = File.ReadAllBytes(op.FileName);
               }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement de l'image : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
