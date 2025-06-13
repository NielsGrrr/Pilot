using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes.Tests
{
    [TestClass()]
    public class RevendeurTests
    {
        [TestMethod()]
        public void RevendeurTest()
        {
            Revendeur revendeur = new Revendeur(1,"raisonsociale", "rue", "cpost", "ville");
            Assert.AreEqual(1, revendeur.NumRevendeur);
            Assert.AreEqual("raisonsociale", revendeur.RaisonSociale);
            Assert.AreEqual("rue", revendeur.AdresseRue);
            Assert.AreEqual("cpost", revendeur.AdresseCP);
            Assert.AreEqual("ville", revendeur.AdresseVille);
        }
        /*
        [TestMethod()]
        public void CreateTest()
        {
            DataAccess.Username = "stiefvan";
            DataAccess.Password = "dGAxKU";
            Revendeur revendeur = new Revendeur("raisonsociale", "rue", "cpost", "ville");
            int id = revendeur.Create();
            int max = 0;
            foreach (Revendeur rev in new Revendeur().FindAll())
            {
                if (rev.NumRevendeur > max)
                    max = rev.NumRevendeur;
            }
            Assert.AreEqual(max, id);
            revendeur.Delete();
        }*/
    }
}