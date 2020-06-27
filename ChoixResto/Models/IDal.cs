using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        List<Restaurant> ObtenirTousLesRestaurants();

        List<Utilisateur> ObtenirTousLesUtilisateurs();

        void CreerRestaurant(string nom, string telephone);

        void CreerRestaurant(Restaurant restaurant);

        void ModifierRestaurant(int id, String name, string telephone);

        bool RestaurantExiste(string name);

        Utilisateur ObtenirUtilisateur(int id);

        Utilisateur ObtenirUtilisateur(string name);

        int AjouterUtilisateur(string name, string password);

        Utilisateur Authentifier(string name, string password);

        bool ADejaVote(int idSondage, string idUtilisateur);

        int CreerUnSondage();

        int VerifSondage();

        void AjouterVote(int idSondage, int idRestaurant, int idUtilisateur);

        List<Resultats> ObtenirLesResultats(int idSondage);


    }
}
