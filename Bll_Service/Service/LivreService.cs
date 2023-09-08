using Dal_Dao.Repository;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Service.Service
{
    public class LivreService : ILivreService
    {
        // Un champ qui va stocker le IEmprunteurRepository
        private readonly ILivreRepository lr;

        public LivreService(ILivreRepository lrr) // Enfait l'instanciation fait par le container va être stocké implicitement dans ce champ dContext (context = new ApplicationDbContext();)
        {
            this.lr = lrr; // Et on attribue cette instanciation au context
        }
        public int AjoutLivre(Livre liv)
        {
            int verif = lr.AjoutLivre(liv);
            return verif;
        }

        public int SuppLivre(int idLivre)
        {
            int verif = lr.SuppLivre(idLivre);
            return verif;
        }

        public List<Livre> GetAllLivres()
        {
            List<Livre> listeLivres = lr.GetAllLivres();
            return listeLivres;
        }

        public Livre GetLivresById(int id)
        {
            Livre LivreId = lr.GetLivresById(id);
            return LivreId;
        }

        public int ModifLivre(Livre liv)
        {
            int verif = lr.ModifLivre(liv);
            return verif;
        }

        public List<Livre> GetLivresByIdEmprunteur(int id)
        {
            List<Livre> listeLivres = lr.GetLivresByIdEmprunteur(id);
            return listeLivres;
        }
    }
}
