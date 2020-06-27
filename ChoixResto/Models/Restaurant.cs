using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace ChoixResto.Models
{
    public class Restaurant : IValidatableObject
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Le nom du restaurant doit être renseigné"), StringLength(80, ErrorMessage = " 80 caracteres maximum")]
        [Required(ErrorMessage = "le nom doit être renseigné"),RegularExpression(@"^[a-zA-Z0-9 éèêûîôâ]{1,80}$",ErrorMessage = "le nom du restaurant doit contenir au mawimum 80 caracteres" )]
        public string Nom { get; set; }

        // permet d'accepeter des chaines de style contenant 10 chiffres et doit commencer par un zero ou par +33
        // deux saisies sont autorisées avec ou sans les espaces
        //exemple 01 43 34 67 78 ou 0143346778 ou +33143346778 ou +331 43 34 67 78

        //[RegularExpression(@"^(([0][0-9])([ ][0-9]{2}){4})|(([0][0-9])([0-9]{2}){4})$")]
        //[RegularExpression(@"^0[0-9]{9}$")]
        [RegularExpression(@"^(([0][0-9])([ ][0-9]{2}){4})|(([0][0-9])([0-9]{2}){4})|([+][3]{2}[0-9]{9})|([+][3]{2}[0-9]([ ][0-9]{2}){4})$",ErrorMessage = "le numero doit être du format : 01 43 34 67 78 ou 0143346778 ou +33143346778 ou +331 43 34 67 78")]
        public string Telephone { get; set; }

        public String Email { get; set; }

        public string Adresse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Telephone) && string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Vous devez saisir au moins un moyen de contacter le restaurant", new[] { "Telephone", "Email" });
        }
    }
}