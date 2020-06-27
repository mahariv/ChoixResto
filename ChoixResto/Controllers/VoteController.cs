using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class VoteController : Controller
    {

        private IDal dal;

        public VoteController() : this(new Dal())
        {
        }

        public VoteController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        // GET: Vote
        [Authorize]
        public ActionResult Index(int? id)
        {
            List<Restaurant> listeRes = dal.ObtenirTousLesRestaurants();

            RestaurantVoteViewModel ListeResto = new RestaurantVoteViewModel();
            string NomEtTel;
            foreach (Restaurant resto in listeRes)
            {
                NomEtTel = $"{resto.Nom} ({resto.Telephone})";
                ListeResto.listeDesResto.Add(new RestaurantCheckBoxViewModel() { Id = resto.Id, NomEtTelephone = NomEtTel, EstSelectione = false });
            }

            return View(ListeResto);
        }


        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(RestaurantVoteViewModel viewModel,int id) // ne jammais oublier de mettre le model en parametre
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            Utilisateur utilisateur = new Utilisateur();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }
            if (utilisateur == null)
            {
                RedirectToAction("Index", "Acceuil");
            }

            int idUtilisateur = utilisateur.Id;
            if (!dal.ADejaVote(id,utilisateur.Prenom))
            {
                List<RestaurantCheckBoxViewModel> listeResto = viewModel.listeDesResto.Where(elt => elt.EstSelectione == true).ToList();
                foreach (RestaurantCheckBoxViewModel restoVote in listeResto)
                {

                    dal.AjouterVote(id, restoVote.Id, idUtilisateur);

                }
            }
                return RedirectToAction("AfficheSondage", "Vote", new { idSondage = id });
                //return RedirectToAction("Index", "Acceuil");
        }

        //[ChildActionOnly]
        [Authorize]
        public ActionResult AfficheSondage(int idSondage)
        {
            List<Resultats> listeResults = dal.ObtenirLesResultats(idSondage);
            return View(listeResults);
        }
    }
}