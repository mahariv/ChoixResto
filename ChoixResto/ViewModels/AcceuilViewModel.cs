using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class AcceuilViewModel
    {
        [DisplayName("Le message")]
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public bool Connecter { get; set; }

        public int IdSondage { get; set; }

        public AcceuilViewModel()
        {
            Date = DateTime.Now;
            Utilisateur = new Utilisateur();
        }
    }
}