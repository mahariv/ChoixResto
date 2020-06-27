using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChoixResto;
using ChoixResto.Controllers;
using System.Web.Mvc;

namespace ChoixResto.Tests
{
    /// <summary>
    /// Description résumée pour ControllerTest
    /// </summary>
    [TestClass]
    public class ControllerTest
    {
        public ControllerTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AccueilController_Index_RenvoiVueParDefaut()
        {
            AcceuilController acceuil = new AcceuilController();

            ViewResult result = (ViewResult)acceuil.Index();

            Assert.AreEqual(String.Empty, result.ViewName);
        }

        [TestMethod]
        public void AccueilController_AfficheDate_RenvoiVueIndexEtViewData()
        {

            AcceuilController acceuil = new AcceuilController();
            string date = "1234";
            ViewResult result = (ViewResult) acceuil.AfficheDate(date);

            Assert.AreEqual("Index", result.ViewName);

            Assert.AreEqual($"Bonjour {date} !", result.ViewBag.Message);
            
            Assert.AreEqual(new DateTime(2012, 4, 28), result.ViewData["Date"]);
        }
    }
}
