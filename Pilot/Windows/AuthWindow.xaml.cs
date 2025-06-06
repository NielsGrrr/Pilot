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

namespace Pilot.Classes
{
    /// <summary>
    /// Logique d'interaction pour AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public List<Employe> LesEmployes { get; set; }
        public AuthWindow()
        {
            ChargeData();
            InitializeComponent();
        }

        private void ChargeData()
        {
            try
            {
                LesEmployes = new List<Employe>(new Employe().FindAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");
                LogError.Log(ex, "Erreur SQL");
                Application.Current.Shutdown();
            }
        }

        private void butConnexion_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Employe emp = LesEmployes.Find(x => x.Login == txtUtil.Text && x.Password == txtMdp.Password);
            if (emp is null)
            {
                labMsg.Content = "Nom d'utilisateur ou mot de passe incorrect";
            }
            else
            {
                DialogResult = true;
            }

        }
    }
}
