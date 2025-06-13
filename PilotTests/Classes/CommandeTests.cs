using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        
        private Commande c;
        private Employe emp;
        private ModeTransport mp;
        private Revendeur rev;
        private DateTime dateLiv;
        [TestMethod()]
        public void CommandeTest()
        {
            
            emp = new Employe(1, "Dog", "Midas", "", "dogmid");
            mp = new ModeTransport(1, "Transport standard");
            rev = new Revendeur(1, "SARL GG", "1 Rue de la Paix", "75000", "Paris");
            dateLiv = DateTime.Today.AddDays(6);
            c = new Commande(1, emp, mp, rev, dateLiv);
            Assert.AreEqual(c.NumCommande, 1);
            Assert.AreEqual(c.Employe, emp);
            Assert.AreEqual(c.UnTransport, mp);
            Assert.AreEqual(c.UnRevendeur, rev);
            Assert.AreEqual(c.DateLivraison, dateLiv);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CommandeDateLivraison_Invalide()
        {
            emp = new Employe(1, "Dog", "Midas", "", "dogmid");
            mp = new ModeTransport(1, "Transport standard");
            rev = new Revendeur(1, "SARL GG", "1 Rue de la Paix", "75000", "Paris");
            dateLiv = new DateTime(1909,12,12);
            c = new Commande(1, emp, mp, rev, dateLiv);

        }

        [TestMethod()]
        public void CreateTest()
        {
            DataAccess.Username = "stiefvan";
            DataAccess.Password = "dGAxKU";
            emp = new Employe(1, "Dog", "Midas", "", "dogmid");
            mp = new ModeTransport(1, "Transport standard");
            rev = new Revendeur(1, "SARL GG", "1 Rue de la Paix", "75000", "Paris");
            dateLiv = new DateTime(1909, 12, 12);
            c = new Commande(48, emp, mp, rev, dateLiv);
            int id = c.Create();
            Assert.AreEqual(48, id);
        }

        

        [TestMethod()]
        public void UpdateTest()
        {
            DataAccess.Username = "stiefvan";
            DataAccess.Password = "dGAxKU";
            emp = new Employe(1, "Dog", "Midas", "", "dogmid");
            mp = new ModeTransport(1, "Transport standard");
            rev = new Revendeur(1, "SARL GG", "1 Rue de la Paix", "75000", "Paris");
            dateLiv = new DateTime(1909, 12, 12);
            c = new Commande(48, emp, mp, rev, dateLiv);
            int id = c.Update();
            Assert.AreEqual(48, id);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            DataAccess.Username = "stiefvan";
            DataAccess.Password = "dGAxKU";
            emp = new Employe(1, "Dog", "Midas", "", "dogmid");
            mp = new ModeTransport(1, "Transport standard");
            rev = new Revendeur(1, "SARL GG", "1 Rue de la Paix", "75000", "Paris");
            dateLiv = new DateTime(1909, 12, 12);
            c = new Commande(48, emp, mp, rev, dateLiv);
            int id = c.Delete();
            Assert.AreEqual(48, id);
        }
        public void FindAllTest()
        {
            //Vérifier le prix total calculé
        }
        public void AjouterProduit()
        {
            //Test d'unicité des produits dans les comandes
        }
    }
}