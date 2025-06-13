using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes.Tests
{
    [TestClass()]
    public class ProduitTests
    {
        /*
        [TestMethod()]
        public void ProduitTest()
        {
            DataAccess.Username = "stiefvan";
            DataAccess.Password = "dGAxKU";
            TypePointe laPointe = new TypePointe();
            Classes.Type leType = new Classes.Type();
            ObservableCollection<Couleur> lesCouleurs = new ObservableCollection<Couleur>();
            Produit prod = new Produit(1, laPointe, leType, "codeP", "nomProduit", 17, 22, true, lesCouleurs);
            Assert.AreEqual(1, prod.Numproduit);
            Assert.AreEqual(laPointe, prod.LaPointe);
            Assert.AreEqual(leType, prod.LeType);
            Assert.AreEqual("codeP", prod.CodeProduit);
            Assert.AreEqual("nomProduit", prod.NomProduit);
            Assert.AreEqual(17, prod.PrixVente);
            Assert.AreEqual(22, prod.QuantiteStock);
            Assert.AreEqual(true, prod.Disponible);
            Assert.AreEqual(lesCouleurs, prod.LesCouleurs);
        }*/

        /*
        [TestMethod()]
        public void CreateTest()
        {
            DataAccess.Username = "lanfroym";
            DataAccess.Password = "ItNUrB";
            TypePointe laPointe = new ObservableCollection<TypePointe>(new TypePointe().FindAll())[0];
            Classes.Type leType = new ObservableCollection<Classes.Type>(new Type().FindAll())[0];
            Produit prod = new Produit(1, laPointe, leType, "codeP", "nomProduit", 17, 22, true);
            int id = prod.Create();
            Assert.AreEqual(46, id);
        }*/
    }
}