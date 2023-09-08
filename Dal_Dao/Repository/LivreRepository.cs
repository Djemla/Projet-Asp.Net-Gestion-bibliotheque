using Dal_Dao.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Dao.Repository
{
    public class LivreRepository : ILivreRepository
    {
        private readonly ApplicationDbContext context;

        public LivreRepository(ApplicationDbContext dcontext)
        {
            this.context = dcontext;
        }

        public List<Livre> GetAllLivres()
        {
            List<Livre> listeLivres = context.Livres.ToList();
            return listeLivres;
        }

        public Livre GetLivresById(int id)
        {
            Livre livre = context.Livres.Find(id);
            return livre;
        }

        public int AjoutLivre(Livre liv)
        {
            context.Livres.Add(liv);
            int verif = context.SaveChanges();
            return verif;
        }

        public int SuppLivre(int idLivre)
        {
            int verif = 0;
            Livre livre = GetLivresById(idLivre);
            if (livre != null)
            {
                context.Livres.Remove(livre);
                verif = context.SaveChanges();
            }
            return verif;
        }



        public int ModifLivre(Livre liv)
        {
            int verif = 0;
            Livre livre = GetLivresById(liv.Id);
            if (livre != null)
            {
                livre.Auteur = liv.Auteur;
                livre.Annee = liv.Annee;

                verif = context.SaveChanges(); // Apparement Pas besoin du update. la modif est déjà faite dans le context.
            }
            return verif;
        }


        public List<Livre> GetLivresByIdEmprunteur(int id)
        {
            List<Livre> listeLivres = context.Livres.Where(x => x.Emprunteur.Id == id).ToList();
            return listeLivres;
        }
    }
}

