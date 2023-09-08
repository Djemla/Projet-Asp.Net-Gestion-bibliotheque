using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Service.Service
{
    public interface IEmprunteurService
    {
// CRUD
        int AjoutEmprunteur(Emprunteur emp);
        int ModifEmprunteur(Emprunteur emp);
        int SuppEmprunteur(int id);
        Emprunteur GetEmprunteurById(int id);
        List<Emprunteur> GetAllEmprunteurs();
       
        // Liste de tous les livres d'un emprunteur
        List<Livre> GetAllLivreEmprunteurId(int IdEmprunteur); 
        
        // Permettre l’emprunt d’un livre par un emprunteur en enregistrant l’association entre les deux entités.
        int AjoutEmprunteurLivre(Emprunteur emp, Livre liv);



    }
}
