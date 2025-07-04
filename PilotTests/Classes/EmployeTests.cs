﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes.Tests
{
    [TestClass()]
    public class EmployeTests
    {
        [TestMethod()]
        public void EmployeTest()
        {
            Employe employe = new Employe(1,RoleEmploye.Commercial, "nom", "prenom", "mdp", "login");
            Assert.AreEqual(employe.NumEmploye, 1);
            Assert.AreEqual(employe.Role, RoleEmploye.Commercial);
            Assert.AreEqual(employe.Nom, "nom");
            Assert.AreEqual(employe.Prenom, "prenom");
            Assert.AreEqual(employe.Password, "mdp");
            Assert.AreEqual(employe.Login, "login");
        }
    }
}