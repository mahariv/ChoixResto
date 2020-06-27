using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChoixResto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        private IDal dal;

        public LoginController() : this(new Dal())
        { }

        public LoginController(IDal idalIoc)
        {
            dal = idalIoc;
        }

        public ActionResult Index()
        {
            UtilisateurViewModel viewModel = new UtilisateurViewModel { authentified = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(UtilisateurViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = dal.Authentifier(viewModel.utilisateur.Prenom, viewModel.utilisateur.Password);
                if (utilisateur != null)
                {
                    FormsAuthentication.SetAuthCookie(utilisateur.Id.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");

                }
                ModelState.AddModelError("Utilisateur.Prenom", "le prénom / motde passe n'est pas valide");
                
            }
            return View(viewModel);
        }

        public ActionResult CreerCompte()
        {
            Utilisateur utilisateur = new Utilisateur();
            return View(utilisateur);
        }

        [HttpPost]
        public ActionResult CreerCompte(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = dal.AjouterUtilisateur(utilisateur.Prenom, utilisateur.Password);
                FormsAuthentication.SetAuthCookie(id.ToString(), false);
                return Redirect("/");
            }
            return View(utilisateur);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceuil");
        }
    }
}