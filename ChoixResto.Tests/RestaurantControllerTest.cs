using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChoixResto.Controllers;
using ChoixResto.Models;
using System.Web.Mvc;

namespace ChoixResto.Tests
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestClass]
    public class RestaurantControllerTest
    {
        public RestaurantControllerTest()
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
        public void RestaurantController_Index_LeControleurEstOk()
        {
            using (IDal dal = new DalEnDur())
            {
                RestaurantController testResto = new RestaurantController(dal);
                ViewResult result = (ViewResult)testResto.Index();

                List<Restaurant> model = (List<Restaurant>)result.Model;
                Assert.AreEqual("Resto pinambour", model[0].Nom);
            }
        }

        [TestMethod]
        public void RestaurantController_ModifierRestaurantAvecRestoInvalide_RenvoiVueParDefaut()
        {
            using (IDal dal = new DalEnDur())
            {
                RestaurantController controller = new RestaurantController(dal);
                controller.ModelState.AddModelError("Nom", "Le nom du restaurant doit être saisi"); // on definis une erreur dans notre Model

                ViewResult resultat = (ViewResult)controller.ModifierRestaurant(new Restaurant { Id = 1, Nom = null, Telephone = "0102030405" });

                Assert.AreEqual(string.Empty, resultat.ViewName);
                Assert.IsFalse(resultat.ViewData.ModelState.IsValid); // l'erreur definis invalide la verification de IsValid
            }
        }
    }
}
