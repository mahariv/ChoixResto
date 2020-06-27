using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class AcceuilController : Controller
    {
        // GET: Acceuil

        private IDal dal;
        public AcceuilController() : this(new Dal())
        { 
        }

        public AcceuilController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        public ActionResult Index()
        {
            int? idSondage = dal.VerifSondage();
            AcceuilViewModel viewModel = new AcceuilViewModel { Connecter = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                if (idSondage != null)
                {
                    viewModel.IdSondage = idSondage.Value;
                }
                else
                {
                    viewModel.IdSondage = -1;
                }
            }
            return View(viewModel);

        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            int idSondage = dal.VerifSondage();

            AcceuilViewModel viewModel = new AcceuilViewModel { Connecter = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }


            if (dal.ADejaVote(idSondage,viewModel.Utilisateur.Prenom))
            {
                ViewBag.message = "Vous avez deja participé à ce sondage"; 
                return View(viewModel);
            }


            return RedirectToAction("Index", "Vote", new { id = idSondage});

 
        }



        [ChildActionOnly]
        public ActionResult AfficheListeRestaurant()
        {
            List<Models.Restaurant> listeDesRestos = new List<Restaurant>
            {
                new Restaurant { Id = 1, Nom = "Resto pinambour", Telephone = "1234" },
                new Restaurant { Id = 2, Nom = "Resto tologie", Telephone = "1234" },
                new Restaurant { Id = 5, Nom = "Resto ride", Telephone = "5678" },
                new Restaurant { Id = 9, Nom = "Resto toro", Telephone = "555" },
            };
            return PartialView(listeDesRestos);
        }


        public ActionResult AfficheDate(string id)
        {
            ViewBag.Message = "Bonjour " + id + " !";
            ViewData["Date"] = new DateTime(2012, 4, 28);
            return View("Index");
        }
    }



}