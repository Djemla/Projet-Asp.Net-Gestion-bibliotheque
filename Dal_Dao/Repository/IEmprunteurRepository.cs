using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Dao.Repository
{
    public interface IEmprunteurRepository
    {
        // CRUD
        int AjoutEmprunteur(Emprunteur emp);
        int ModifEmprunteur(Emprunteur emp);
        int SuppEmprunteur(int id);
        Emprunteur GetEmprunteurById(int id);
        List<Emprunteur> GetAllEmprunteurs();

        List<Livre> GetAllLivreEmprunteurId(int IdEmprunteur);
        int AjoutEmprunteurLivre(Emprunteur emp, Livre liv);
    }
}
