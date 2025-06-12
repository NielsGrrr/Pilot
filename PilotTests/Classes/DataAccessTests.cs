using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilot.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        /*
        [TestMethod()]
        public void GetConnectionTest()
        {

        }

        [TestMethod()]
        public void ExecuteSelectTest()
        {

        }

        [TestMethod()]
        public void ExecuteInsertTest()
        {

        }

        [TestMethod()]
        public void ExecuteSetTest()
        {

        }

        [TestMethod()]
        public void ExecuteSelectUneValeurTest()
        {

        }

        [TestMethod()]
        public void CloseConnectionTest()
        {

        }*/

        [TestMethod()]
        public void DataAccessTest()
        {
            Assert.AreEqual(ConnectionState.Open, DataAccess.Instance.GetConnection().State);
            DataAccess.Instance.CloseConnection();
        }
    }
}