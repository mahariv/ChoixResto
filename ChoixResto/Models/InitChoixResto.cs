using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class InitChoixResto : DropCreateDatabaseAlways<BddContext>
    {
        protected override void Seed(BddContext context)
        {
            context.Restaurants.Add(new Restaurant { Id = 1, Nom = "Resto pinambour", Telephone = "0123456562" });
            context.Restaurants.Add(new Restaurant { Id = 2, Nom = "Resto pinière", Telephone = "01 23 45 65 62" });
            context.Restaurants.Add(new Restaurant { Id = 3, Nom = "Resto toro", Telephone = "0123456562" });

            base.Seed(context);
        }
    }
}