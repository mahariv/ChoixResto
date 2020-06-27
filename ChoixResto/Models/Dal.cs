using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        public BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public bool ADejaVote(int idSondage, string idUtilisateur)
        {
            Utilisateur utilisateur = ObtenirUtilisateur(idUtilisateur);
            if (utilisateur == null)
            {
                return false;
            }
            //Sondage sondage = bdd.Sondages.FirstOrDefault(elt => elt.Id == idSondage && elt.Votes.FirstOrDefault(vo => vo.Utilisateur == utilisateur) != null);
            Sondage sondage = bdd.Sondages.FirstOrDefault(elt => elt.Id == idSondage);
            if (sondage != null && sondage.Votes == null)
            {
                return false;
            }
            else
            {
                return sondage.Votes.Any(elt => elt.Utilisateur == utilisateur);
            }
        }

        public int AjouterUtilisateur(string name, string password)
        {
            password = EncodeMD5(password);
            Utilisateur newUtilisateur = new Utilisateur
            {
                Prenom = name,
                Password = password,
                Nom ="a definir"
                
            };
            bdd.Utilisateurs.Add(newUtilisateur);
            bdd.SaveChanges();

            return newUtilisateur.Id;
        }

        public void AjouterVote(int idSondage, int idRestaurant, int idUtilisateur)
        {
            Sondage sondage = bdd.Sondages.FirstOrDefault(elt => elt.Id == idSondage);
            Vote vote = new Vote()
            {
                Restaurant = ObtenirTousLesRestaurants().FirstOrDefault(elt => elt.Id == idRestaurant),
                Utilisateur = ObtenirUtilisateur(idUtilisateur)
            };
            if (sondage.Votes == null)
            {
                sondage.Votes = new List<Vote>();
            }
            sondage.Votes.Add(vote);
            bdd.SaveChanges();
        }

        public Utilisateur Authentifier(string name, string password)
        {
            password = EncodeMD5(password);
            Utilisateur utilisateur = bdd.Utilisateurs.FirstOrDefault(elt => elt.Prenom == name && elt.Password == password);
            return utilisateur;
        }

        public void CreerRestaurant(string nom, string telephone)
        {
            Restaurant newRestaurant = new Restaurant { Nom = nom, Telephone = telephone, Adresse = "non renseignée" };
            bdd.Restaurants.Add(newRestaurant);
            bdd.SaveChanges();
        }

        public void CreerRestaurant(Restaurant restaurant)
        {
            if (restaurant != null)
            {
                bdd.Restaurants.Add(restaurant);
                bdd.SaveChanges();
            }
        }

        public int CreerUnSondage()
        {
            Sondage sondage = new Sondage
            {
                Date = DateTime.Now
            };
            bdd.Sondages.Add(sondage);
            bdd.SaveChanges();
            return sondage.Id;
        }

        public int VerifSondage()
        {

            var date = DateTime.Now;
            var dateAddOneDay = date.AddDays(1);
            Sondage sondage = bdd.Sondages.FirstOrDefault(elt => elt.Date < dateAddOneDay &&  date > elt.Date);
            if (sondage == null)
            {
                return CreerUnSondage();
            }
            return sondage.Id;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public void ModifierRestaurant(int id, string name, string telephone)
        {
            Restaurant restaurantModif = bdd.Restaurants.FirstOrDefault((elt => elt.Id == id));
            if (restaurantModif != null)
            {
                restaurantModif.Nom = name;
                restaurantModif.Telephone = telephone;
                bdd.SaveChanges();
            }

        }


        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            Sondage sondage = bdd.Sondages.FirstOrDefault(elt => elt.Id == idSondage);
            List<Resultats> listeResultats = new List<Resultats>();
            foreach (Restaurant resto in ObtenirTousLesRestaurants())
            {
               
                int count = sondage.Votes.Count(elt => elt.Restaurant.Id == resto.Id);
                if (count > 0)
                {
                    listeResultats.Add(new Resultats()
                    {
                        NombreDeVotes = count,
                        Nom = resto.Nom,
                        Telephone = resto.Telephone
                    });
                }
            }

            return listeResultats;
        }

        public List<Restaurant> ObtenirTousLesRestaurants()
        {
            return bdd.Restaurants.ToList();
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return bdd.Utilisateurs.ToList();
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(elt => elt.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string idString)
        {
            int index;
            List<Utilisateur> listeUtilisateurs = ObtenirTousLesUtilisateurs();
            if (Int32.TryParse(idString, out index))
            {
                return ObtenirUtilisateur(index);
            }
            else
            {
                return listeUtilisateurs.FirstOrDefault(elt => elt.Prenom == idString);
            }
        }

        public bool RestaurantExiste(string name)
        {
            Restaurant restaurant = bdd.Restaurants.FirstOrDefault(elt => elt.Nom == name);
            if (restaurant != null)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}