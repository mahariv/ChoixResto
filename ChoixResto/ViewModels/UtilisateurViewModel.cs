using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class UtilisateurViewModel
    {
        public Utilisateur utilisateur { get; set; }
        public bool authentified { get; set; }

        public UtilisateurViewModel()
        {
            utilisateur = new Utilisateur();
        }
    }
}