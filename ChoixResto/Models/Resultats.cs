using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Resultats
    {
        public int NombreDeVotes { get; set; }

       [Required]
        public string Nom { get; set; }

        public string Telephone { get; set; }
    }
}