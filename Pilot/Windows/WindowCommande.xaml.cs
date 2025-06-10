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
using System.Windows.Shapes;

namespace Pilot.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowCommande.xaml
    /// </summary>
    public partial class WindowCommande : Window
    {
        public WindowCommande(Commande uneCommande)
        {
            this.DataContext = uneCommande;
            InitializeComponent();
        }

        public WindowCommande(Commande uneCommande, Action act)
        {
            this.DataContext = uneCommande;
            InitializeComponent();
            butValiderCommande.Content = act;
        }
    }
}
