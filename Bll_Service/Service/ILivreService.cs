using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Service.Service
{
    public interface ILivreService
    {
        int AjoutLivre(Livre liv);
        int SuppLivre(int idLivre);
        int ModifLivre(Livre liv);
        List<Livre> GetAllLivres();
        Livre GetLivresById(int id);
        List<Livre> GetLivresByIdEmprunteur(int id);


        // Permettre le retour d’un livre emprunté par un emprunteur en supprimant l’association entre les deux entités.
        // Je cherche si le livre est existant, et je le supprime par l'Id de l'emprunteur
        //   int DeleteLivreEmprunte(int idEmprunteur, int idLivre); Voir autre méthode ci-dessous (supprimer livre)
        // Vu qu'on peut naviguer entre les pages, plus besoin de int idEmprunteur, car la session va faire naviguer l'idEmprunteur)

        // Je vais faire une autre methode:
        // Option Ajouter livre, modifier(ou supprimer) livre (en plus de modifier emp, supprimer emp) 
        // Ajouter livre: ca va me renvoyer à la vue d'ajouter livre (controller). Je recuperer les données du livre (controller) et j'ajoute dans la liste de livre de livre de l'emprunteur (appel de la bll; Bll à creer au niveau emprunteur (ajoutLivre(livre liv)).
        // Supprimer livre: Ca va me renvoyer à la liste des livres de l'emprunteur(session, controller). Je clic sur supprimer au niveau d'un livre correspondant (comme une suppression normale du livre). Ca supprime et ca reste sur la même page de liste de livre de l'emprunteur.


    }
}
