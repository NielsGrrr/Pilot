using Pilot.Classes;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour UserControlDetailCommande.xaml
    /// </summary>
    public partial class UserControlDetailCommande : UserControl
    {
        public UserControlDetailCommande(Commande commande)
        {
            this.DataContext = commande;
            InitializeComponent();
            labCommande.Content = "Détail de la commande " + commande.NumCommande;
        }
    }
}
