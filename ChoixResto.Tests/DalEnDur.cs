using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto.Tests
{
    public class DalEnDur : IDal
    {
        private List<Restaurant> listeDesRestaurants;
        private List<Utilisateur> listeDesUtilisateurs;
        private List<Sondage> listeDessondages;

        public DalEnDur()
        {
            listeDesRestaurants = new List<Restaurant>
        {
            new Restaurant { Id = 1, Nom = "Resto pinambour", Telephone = "0102030405"},
            new Restaurant { Id = 2, Nom = "Resto pinière", Telephone = "0102030405"},
            new Restaurant { Id = 3, Nom = "Resto toro", Telephone = "0102030405"},
        };
            listeDesUtilisateurs = new List<Utilisateur>();
            listeDessondages = new List<Sondage>();
        }

        public List<Restaurant> ObtientTousLesRestaurants()
        {
            return listeDesRestaurants;
        }

        public void CreerRestaurant(string nom, string telephone)
        {
            int id = listeDesRestaurants.Count == 0 ? 1 : listeDesRestaurants.Max(r => r.Id) + 1;
            listeDesRestaurants.Add(new Restaurant { Id = id, Nom = nom, Telephone = telephone });
        }

        public void ModifierRestaurant(int id, string nom, string telephone)
        {
            Restaurant resto = listeDesRestaurants.FirstOrDefault(r => r.Id == id);
            if (resto != null)
            {
                resto.Nom = nom;
                resto.Telephone = telephone;
            }
        }

        public bool RestaurantExiste(string nom)
        {
            return listeDesRestaurants.Any(resto => string.Compare(resto.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public int AjouterUtilisateur(string nom, string motDePasse)
        {
            int id = listeDesUtilisateurs.Count == 0 ? 1 : listeDesUtilisateurs.Max(u => u.Id) + 1;
            listeDesUtilisateurs.Add(new Utilisateur { Id = id, Prenom = nom, Password = motDePasse });
            return id;
        }

        public Utilisateur Authentifier(string nom, string motDePasse)
        {
            return listeDesUtilisateurs.FirstOrDefault(u => u.Prenom == nom && u.Password == motDePasse);
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return listeDesUtilisateurs.FirstOrDefault(u => u.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return ObtenirUtilisateur(id);
            return null;
        }

        public int CreerUnSondage()
        {
            int id = listeDessondages.Count == 0 ? 1 : listeDessondages.Max(s => s.Id) + 1;
            listeDessondages.Add(new Sondage { Id = id, Date = DateTime.Now, Votes = new List<Vote>() });
            return id;
        }

        public void AjouterVote(int idSondage, int idResto, int idUtilisateur)
        {
            Vote vote = new Vote
            {
                Restaurant = listeDesRestaurants.First(r => r.Id == idResto),
                Utilisateur = listeDesUtilisateurs.First(u => u.Id == idUtilisateur)
            };
            Sondage sondage = listeDessondages.First(s => s.Id == idSondage);
            sondage.Votes.Add(vote);
        }

        public bool ADejaVote(int idSondage, string idStr)
        {
            Utilisateur utilisateur = ObtenirUtilisateur(idStr);
            if (utilisateur == null)
                return false;
            Sondage sondage = listeDessondages.First(s => s.Id == idSondage);
            return sondage.Votes.Any(v => v.Utilisateur.Id == utilisateur.Id);
        }

        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            List<Restaurant> restaurants = ObtientTousLesRestaurants();
            List<Resultats> resultats = new List<Resultats>();
            Sondage sondage = listeDessondages.First(s => s.Id == idSondage);
            foreach (IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Restaurant.Id))
            {
                int idRestaurant = grouping.Key;
                Restaurant resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Resultats { Nom = resto.Nom, Telephone = resto.Telephone, NombreDeVotes = nombreDeVotes });
            }
            return resultats;
        }

        public void Dispose()
        {
            listeDesRestaurants = new List<Restaurant>();
            listeDesUtilisateurs = new List<Utilisateur>();
            listeDessondages = new List<Sondage>();
        }

        public List<Restaurant> ObtenirTousLesRestaurants()
        {
            List<Restaurant> TousLesRestaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Nom = "Resto pinambour", Telephone = "0102030405"},
                new Restaurant { Id = 2, Nom = "Resto pinière", Telephone = "0102030405"},
                new Restaurant { Id = 3, Nom = "Resto toro", Telephone = "0102030405"},
            };
            return TousLesRestaurants;
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            List<Utilisateur> TousLesUtilisateurs = new List<Utilisateur>()
            {
                new Utilisateur{ Id =1,Nom ="Smith",Prenom ="John",Password=EncodeMD5("12345") }
            };
            
            return TousLesUtilisateurs;
        }

        public void CreerRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
