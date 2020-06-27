using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class RestaurantVoteViewModel : IValidatableObject
    {
        public List<RestaurantCheckBoxViewModel> listeDesResto { get; set; }

        public RestaurantVoteViewModel()
        {
            listeDesResto = new List<RestaurantCheckBoxViewModel>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool indicateur = false;
            foreach (RestaurantCheckBoxViewModel resto in listeDesResto)
            {
                if (resto.EstSelectione == true)
                {
                    if (indicateur == true)
                    {
                        //yield return new ValidationResult("Vous devez saisir un seul restaurant", new[] { "listeDesResto"});
                    }
                    else
                    {
                        indicateur = true;
                    }
                }
            }

            if (indicateur == false)
            {
                yield return new ValidationResult("veuillez saisir un restaurant", new[] { "listeDesResto" });
            }

        }
    }
}