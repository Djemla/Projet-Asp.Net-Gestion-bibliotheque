using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Emprunteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int AnneeNaissance { get; set; }

        // Chaque emprunteur peut emprunter plusieurs livres. 
        public List<Livre> ListeLivre { get; set; } // dans ce EF (# EFCore, pas besoin de specifier nullable. Il le prend par defaut)
    


    // Pas besoin de specifier un contructeur ici, car il s'agit de la méthode MVC ici, qui utilise un model pour stocker les données

    // Pas besoin de la methode string ici, car nous n'utilisons pas de console ici. Il s'agit d'une appli web.
    }
}
