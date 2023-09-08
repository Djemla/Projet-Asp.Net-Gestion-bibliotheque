using Dal_Dao.Data;
using Dal_Dao.Repository;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Service.Service
{
    public class EmprunteurService : IEmprunteurService
    {
        // Un champ qui va stocker le IEmprunteurRepository
        private readonly IEmprunteurRepository er;

        public EmprunteurService(IEmprunteurRepository err) // Enfait l'instanciation fait par le container va être stocké implicitement dans ce champ dContext (context = new ApplicationDbContext();)
        {
            this.er = err; // Et on attribue cette instanciation au context
        }

        public int AjoutEmprunteur(Emprunteur emp)
        {
            int verif = er.AjoutEmprunteur(emp);
            return verif;
        }

      

        public List<Livre> GetAllLivreEmprunteurId(int IdEmprunteur)
        {
            List <Livre> listeLivreId = er.GetAllLivreEmprunteurId(IdEmprunteur);
            
            return listeLivreId;
        }

        public List<Emprunteur> GetAllEmprunteurs()
        {
            List<Emprunteur> listeEmprunteurs = er.GetAllEmprunteurs();
            return listeEmprunteurs;
        }

        public Emprunteur GetEmprunteurById(int id)
        {
            Emprunteur emp = er.GetEmprunteurById(id);
            return emp;
        }

        public int ModifEmprunteur(Emprunteur emp)
        {
            int verif = er.ModifEmprunteur(emp);
            return verif;
        }

        public int SuppEmprunteur(int id)
        {
            int verif = er.SuppEmprunteur(id);
            return verif;
        }

        public int AjoutEmprunteurLivre(Emprunteur emp, Livre liv)
        {
            int verif = er.AjoutEmprunteurLivre(emp, liv);
            return verif;
        }
    }
}
