using Dal_Dao.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Dao.Repository
{
    public class EmprunteurRepository : IEmprunteurRepository
    {
        private readonly ApplicationDbContext context;

        public EmprunteurRepository(ApplicationDbContext dcontext) // Enfait l'instanciation fait par le container va être stocké implicitement dans ce champ dContext (context = new ApplicationDbContext();)
        {
            this.context = dcontext; // Et on attribue cette instanciation au context
        }

        public List<Emprunteur> GetAllEmprunteurs()
        {
            List<Emprunteur>  listeEmprunteurs = context.Emprunteurs.ToList();
            return listeEmprunteurs;
        }

        public Emprunteur GetEmprunteurById(int id)
        {
            Emprunteur emp2 = context.Emprunteurs.Find(id);
            return emp2;
        }

        public int AjoutEmprunteur(Emprunteur emp)
        {
            context.Emprunteurs.Add(emp);
            int verif = context.SaveChanges();
            return verif;
        }

        public int ModifEmprunteur(Emprunteur emp)
        {
            int verif = 0;
            Emprunteur emp2 = GetEmprunteurById(emp.Id);
            if (emp2 != null)
            {
                emp2.Nom = emp.Nom;
                emp2.Prenom = emp.Prenom;
                emp2.AnneeNaissance = emp.AnneeNaissance;

                verif = context.SaveChanges(); // Apparement Pas besoin du update. la modif est déjà faite dans le context.
            }

            return verif;
        }



        public int SuppEmprunteur(int id)
        {
            int verif = 0;
            Emprunteur emp2 = GetEmprunteurById(id);
            if (emp2 != null)
            {
                context.Emprunteurs.Remove(emp2);
                verif = context.SaveChanges();
            }
            return verif;
        }


        public List<Livre> GetAllLivreEmprunteurId(int IdEmprunteur)
        {
            List<Livre> listeLivreId = context.Livres.Where(x => x.Emprunteur.Id == IdEmprunteur).ToList();

            return listeLivreId;
        }

        public int AjoutEmprunteurLivre(Emprunteur emp, Livre liv)
        {
            List<Livre> livre = GetAllLivreEmprunteurId(emp.Id);
            livre.Add(liv);

            int verif = context.SaveChanges();

            return verif;
        }
    }
}
