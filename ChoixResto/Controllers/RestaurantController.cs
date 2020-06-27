using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class RestaurantController : Controller
    {

        private IDal dal;

        public RestaurantController() : this(new Dal())
        {
        }

        public RestaurantController(IDal dalIoc)
        {
            dal = dalIoc;
        }


        // GET: Restaurant
        public ActionResult Index()
        {
            // note l'initialisation de la base de donnée avec des valeurs par défauts ce fait dans dans global.asax qui se lance qu'une fois a l'execution

                List<Restaurant> listeDesRestaurants = dal.ObtenirTousLesRestaurants();
                ViewBag.ListesDesRestos = new SelectList(listeDesRestaurants, "Id", "Nom");
                return View(listeDesRestaurants);


            //return View();
        }

        //old

        #region old

        //public ActionResult ModifierRestaurant(int? id)
        //{
        //    //cette methode permet de recuperer la derniere valeurs contenus dans une URL qui est prefixé par un /
        //    //string id = Request.Url.AbsolutePath.Split('/').Last();
        //    // string idStr = Request.QueryString["id"];
        //    if (id.HasValue)
        //    {
        //        using (IDal dal = new Dal())
        //        {
        //            // l'instruction dans la condition if est seulement appeller lorsqu'on envoit le formulaire
        //            // le controler permet d'afficher le formulaire et de traiter le formulaire
        //            //if (Request.HttpMethod == "POST")
        //            //{
        //            //    string nouveauNom = Request.Form["Nom"];
        //            //    string nouveauTelephone = Request["Telephone"];

        //            //    dal.ModifierRestaurant(id.Value, nouveauNom, nouveauTelephone);
        //            //}
        //            Restaurant resto = new Restaurant();
        //            resto = dal.ObtenirTousLesRestaurants().FirstOrDefault(elt => elt.Id == id);
        //            if (resto == null)
        //            {
        //                return View("Error");
        //            }
        //            else
        //            {
        //                return View(resto);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}

        #endregion old

        //on appelle ce controller lorsqu'on a une requete de type GET
        // il va permettre de definir le model de type Restaurant à partir de l'ID qui lui est envoyé depuis la vue Index de Restaurant
        [HttpGet]
        public ActionResult ModifierRestaurant(int? id)
        {

            if (id.HasValue)
            {

                    Restaurant resto = new Restaurant();
                    resto = dal.ObtenirTousLesRestaurants().FirstOrDefault(elt => elt.Id == id);
                    if (resto == null)
                    {
                        return View("Error");
                    }
                    else
                    {
                        return View(resto);
                    }

            }
            else
            {
                return View("Error");
            }
        }



        // on appele ce controller lorsqu'il y a un formulaire de type Post qui est utilisé.
        // ce formulaire va utiliser le model de definis dans la vue qui est ici de type Restaurant et sera accesible à travers resto !.
        //le model a était precedement initialiser à travers l'instruction GET qui a était lancée depuuis l'index de Restaurant
        // il s'agit du binding de classe
        [HttpPost]
        public ActionResult ModifierRestaurant(Restaurant resto)
        {
            if (!ModelState.IsValid)
            {
                return View(resto);
            }

                dal.ModifierRestaurant(resto.Id, resto.Nom, resto.Telephone);
                return RedirectToAction("Index");

        }

        public ActionResult AjouterRestaurant()
        {
            Restaurant resto = new Restaurant();
            return View(resto);
        }

        [HttpPost]
        public ActionResult AjouterRestaurant(Restaurant resto)
        {



                if (dal.RestaurantExiste(resto.Nom))
                {
                    ModelState.AddModelError("Nom", "ce nom de restaurant existe deja");
                    return View(resto);
                }

                if (!ModelState.IsValid)
                {
                    return View(resto);
                }
                dal.CreerRestaurant(resto);
                return RedirectToAction("Index");

        }


    }
}