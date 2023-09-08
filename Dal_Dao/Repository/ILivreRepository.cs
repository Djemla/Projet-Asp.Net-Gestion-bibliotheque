using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Dao.Repository
{
    public interface ILivreRepository
    {
        // CRUD
        int AjoutLivre(Livre liv);
        int SuppLivre(int idLivre);
        int ModifLivre(Livre liv);
        List<Livre> GetAllLivres();
        Livre GetLivresById(int id);
        List<Livre> GetLivresByIdEmprunteur(int id);
    }
}
