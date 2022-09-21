using Microsoft.VisualStudio.TestTools.UnitTesting;
using IHM_Sujet2_v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace IHM_Sujet2_v1.Tests
{
    [TestClass()]
    public class ResaTests
    {
        private Resa r;
        [TestMethod()]
        public void CreateTest()
        {
            r = new Resa(new DateTime(2022, 10, 10), new Employe(), new Vehicule(), "");
            r.Create();
            int id= r.IdResa;
            int test=r.FindBySelection("DATERESA = '2022-10-10'")[0].IdResa;
            Assert.AreEqual(test, id, "Test Create");
            
        }

        [TestMethod()]
        public void DeleteTest()
        {
            r = new Resa(new DateTime(2022, 10, 10), new Employe(), new Vehicule(), "");
            r.Create();
            r.Delete();
            int id = r.IdResa;
            Assert.AreEqual(null, id, "Test Delete");
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            ObservableCollection<Resa> testr = r.FindBySelection("IDRESA = 2");
            int testId = testr[0].IdResa;
            Assert.AreEqual(2, testId, "Test de FindBySelection");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            r = new Resa(new DateTime(2022, 10, 10), new Employe(), new Vehicule(), "");
            r.Create();
            r.Update();
            int id = r.IdResa;
            int test = r.FindBySelection("DATERESA = '2022-10-10'")[0].IdResa;
            Assert.AreEqual(test, id, "Test Update");

        }
    }
}