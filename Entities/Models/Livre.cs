using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Livre
    {

        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int Annee { get; set; }

        // Un livre peut être emprunté par un seul emprunteur.
        // La clé étrangère se trouvera dans cette table (n)
        public Emprunteur Emprunteur { get; set; } // dans ce EF (# EFCore, pas besoin de specifier nullable. Il le prend par defaut)




        // Pas besoin de specifier un contructeur ici, car il s'agit de la méthode MVC ici, qui utilise un model pour stocker les données

        // Pas besoin de la methode string ici, car nous n'utilisons pas de console ici. Il s'agit d'une appli web.

    }
}
